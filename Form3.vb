Imports System.IO
Imports System.IO.TextWriter
Imports MongoDB.Driver
Imports MongoDB.Bson

Public Class Form3
    Dim connectionUri As String = "mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority"
    ' Replace the following variables with your MongoDB Atlas connection details
    Private ReadOnly dbName As String = "Admin1"
    Private ReadOnly collectionName As String = "Admin"

    Private Function AuthenticateUser(adminuser As String, password As String) As Boolean
        Try
            Dim client As New MongoClient(connectionUri)
            Dim database As IMongoDatabase = client.GetDatabase(dbName)
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

            Dim filterUsername = Builders(Of BsonDocument).Filter.Eq(Of String)("adminuser", adminuser)
            Dim filterPassword = Builders(Of BsonDocument).Filter.Eq(Of String)("password", password)

            Dim filter = Builders(Of BsonDocument).Filter.And(filterUsername, filterPassword)

            ' Debug: Output the filter to check if it's correctly formed
            Console.WriteLine(filter.ToJson())

            Dim count = collection.CountDocuments(filter)

            ' Debug: Output the count to check if it's greater than 0
            Console.WriteLine("Document count: " & count)

            Return count > 0
        Catch ex As Exception
            MessageBox.Show("Error connecting to MongoDB: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function



    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            TextBox2.UseSystemPasswordChar = True
        Else
            TextBox2.UseSystemPasswordChar = False
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim settings = MongoClientSettings.FromConnectionString(connectionUri)
        settings.ServerApi = New ServerApi(ServerApiVersion.V1)
        Dim client = New MongoClient(settings)
        Dim adminuser As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()


        If AuthenticateUser(adminuser, password) Then
            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TextBox1.Clear()
            TextBox2.Clear()
            Form12.Show()
            Me.Hide()
        Else
            MessageBox.Show("Invalid credentials. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Application.Exit()
        Else
            e.Cancel = True
        End If
    End Sub
End Class