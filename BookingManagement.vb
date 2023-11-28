Imports System.IO
Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class BookingManagement
    ' Update the connection string with your MongoDB server details
    Private connectionString As String = "mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority"
    Private mongoClient As MongoClient
    Private mongoDatabase As IMongoDatabase
    Private mongoCollection As IMongoCollection(Of BsonDocument)

    Private Sub BookingManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize MongoDB client and database
            mongoClient = New MongoClient(connectionString)
            mongoDatabase = mongoClient.GetDatabase("Admin1")
            mongoCollection = mongoDatabase.GetCollection(Of BsonDocument)("User")

            ' Load data from MongoDB and display it in DataGridView
            LoadData()
        Catch ex As Exception
            MessageBox.Show("Error connecting to MongoDB: " & ex.Message)
        End Try
    End Sub
    Private Sub LoadData()
        ' Clear DataGridView first
        DataGridView1.Rows.Clear()

        ' Load data from MongoDB and add it to DataGridView
        Dim documents = mongoCollection.Find(New BsonDocument()).ToList()
        For Each doc In documents
            Dim package = doc("package").ToString()
            Dim KiloGram = doc("KiloGram").ToString()
            Dim service = doc("service").ToString()
            Dim delivery = doc("delivery").ToString()
            Dim payment = doc("payment").ToString()
            Dim modeofpayment = doc("modeofpayment").ToString()


            DataGridView1.Rows.Add(package, service, delivery, payment, KiloGram)
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Delete selected document from MongoDB
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim rowIndex = DataGridView1.SelectedRows(0).Index
            Dim package = DataGridView1.Rows(rowIndex).Cells(0).Value.ToString()
            Dim KiloGram = DataGridView1.Rows(rowIndex).Cells(1).Value.ToString()
            Dim service = DataGridView1.Rows(rowIndex).Cells(2).Value.ToString
            Dim delivery = DataGridView1.Rows(rowIndex).Cells(3).Value.ToString()
            Dim payment = DataGridView1.Rows(rowIndex).Cells(4).Value.ToString()
            Dim modeofpayment = DataGridView1.Rows(rowIndex).Cells(5).Value.ToString()


            mongoCollection.DeleteOne(New BsonDocument("package", package))
            mongoCollection.DeleteOne(New BsonDocument("KiloGram", KiloGram))
            mongoCollection.DeleteOne(New BsonDocument("service", service))
            mongoCollection.DeleteOne(New BsonDocument("delivery", delivery))
            mongoCollection.DeleteOne(New BsonDocument("payment", payment))
            mongoCollection.DeleteOne(New BsonDocument("modeofpayment", modeofpayment))

            ' Clear textboxes and reload data

            TextBox1.Clear()

            LoadData()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class