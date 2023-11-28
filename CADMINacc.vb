Imports System.IO
Imports System.IO.TextWriter
Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class CADMINacc
    Private connectionString As String = "mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority"
    Private mongoClient As MongoClient
    Private mongoDatabase As IMongoDatabase
    Private mongoCollection As IMongoCollection(Of BsonDocument)
    Dim connectionUri As String = "mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority"
    ' Replace the following variables with your MongoDB Atlas connection details
    Private ReadOnly dbName As String = "Admin1"
    Private ReadOnly collectionName As String = "Admin"
    Dim conclient As MongoClient
    Dim client As New MongoClient(connectionUri)
    Dim database As IMongoDatabase = client.GetDatabase(dbName)
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)
    Private WithEvents openFileDialog As New OpenFileDialog()

    Private Function GetSourceData() As List(Of BsonDocument)
        Dim sourceDatabase As IMongoDatabase = conclient.GetDatabase("Admin1")
        Dim sourceCollection As IMongoCollection(Of BsonDocument) = sourceDatabase.GetCollection(Of BsonDocument)("Admin")

        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty
        Return sourceCollection.Find(filter).ToList()
    End Function

    Public Sub LoadData()
        ' Clear DataGridView first
        Guna2DataGridView1.Rows.Clear()

        ' Load data from MongoDB and add it to DataGridView
        Dim documents = mongoCollection.Find(New BsonDocument()).ToList()
        For Each doc In documents
            Dim adminuser = doc("adminuser").ToString()
            Dim password = doc("password").ToString()
            Dim UserTYPE = doc("UserTYPE").ToString()
            Guna2DataGridView1.Rows.Add(adminuser, password, UserTYPE)
        Next
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Form12.Show()
        Me.Hide()

    End Sub



    Private Sub CADMINacc_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ' Initialize MongoDB client and database
            mongoClient = New MongoClient(connectionString)
            mongoDatabase = mongoClient.GetDatabase("Admin1")
            mongoCollection = mongoDatabase.GetCollection(Of BsonDocument)("Admin")

            ' Load data from MongoDB and display it in DataGridView
            LoadData()
        Catch ex As Exception
            MessageBox.Show("Error connecting to MongoDB: " & ex.Message)
        End Try
    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        createAccADMIN.Show()
    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        If Guna2DataGridView1.Columns(e.ColumnIndex).Name = "Delete" Then
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                If Guna2DataGridView1.SelectedRows.Count > 0 Then
                    Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index
                    Dim uniqueID As String = Guna2DataGridView1.Rows(rowIndex).Cells("_id").Value.ToString()

                    ' Delete the row from the DataGridView.
                    Guna2DataGridView1.Rows.RemoveAt(rowIndex)

                    ' Delete the corresponding document from the database using the unique identifier.
                    Dim filter = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", ObjectId.Parse(uniqueID))
                    mongoCollection.DeleteOne(filter)
                End If
                LoadData()
            End If
        End If

        ' Optionally, you can hide the _id column if you don't want to display it
        ' dataGridView1.Columns("_id").Visible = False

    End Sub
End Class