Imports System.IO
Imports System.IO.TextWriter
Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class RFIDloyaltycard
    ' Define your MongoDB connection settings
    Dim connectionUri As String = "mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority"
    ' Replace the following variables with your MongoDB Atlas connection details
    Private ReadOnly dbName As String = "Admin1"
    Private ReadOnly collectionName As String = "RFID"
    Dim client As New MongoClient(connectionUri)
    Dim database As IMongoDatabase = client.GetDatabase(dbName)
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        CustomerM.Show()
        Me.Hide()

    End Sub

    Private Sub RFIDloyaltycard_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rfidTag As Double = Double.Parse(TextBox1.Text)
        Dim points As Integer = Integer.Parse(TextBox2.Text)
        Dim Name As String = TextBox3.Text.Trim()
        Dim Address As String = TextBox4.Text.Trim()
        Dim ContactNum As Double = Double.Parse(TextBox5.Text)
        Dim CUser As String = TextBox6.Text.Trim()
        Dim Cpass As String = TextBox7.Text.Trim()

        ' Create a BsonDocument to store RFID data and points
        Dim dataDocument As BsonDocument = New BsonDocument() From {
            {"rfid_tag", rfidTag},
            {"points", points},
            {"Name", Name},
            {"Address", Address},
            {"ContactNum", ContactNum},
            {"Username", CUser},
            {"Password", Cpass}
        }


        Try

            ' Insert the data into the collection
            collection.InsertOne(dataDocument)

            ' Show success message or perform any other actions as needed
            MessageBox.Show("Data saved successfully.")
        Catch ex As Exception
            ' Handle any exceptions that may occur during the database interaction
            MessageBox.Show("Error saving data: " + ex.Message)
        End Try
    End Sub
    Private Function CalculatePoints(rfidTag As String) As Integer
        ' Your logic to calculate points goes here
        ' Example: Return 10
        Return 0
    End Function


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim rowIndex = e.RowIndex
            TextBox1.Text = DataGridView1.Rows(rowIndex).Cells(0).Value.ToString()
            TextBox2.Text = DataGridView1.Rows(rowIndex).Cells(1).Value.ToString()
            TextBox3.Text = DataGridView1.Rows(rowIndex).Cells(2).Value.ToString()
            TextBox4.Text = DataGridView1.Rows(rowIndex).Cells(3).Value.ToString()
            TextBox5.Text = DataGridView1.Rows(rowIndex).Cells(4).Value.ToString()
            TextBox6.Text = DataGridView1.Rows(rowIndex).Cells(5).Value.ToString()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
    End Sub
End Class