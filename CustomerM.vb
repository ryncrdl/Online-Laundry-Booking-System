Imports System.IO
Imports MongoDB.Bson
Imports MongoDB.Driver

Public Class CustomerM
    ' Update the connection string with your MongoDB server details
    Private connectionString As String = "mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority"
    Private mongoClient As MongoClient
    Private mongoDatabase As IMongoDatabase
    Private mongoCollection As IMongoCollection(Of BsonDocument)
    Dim connectionUri As String = "mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority"
    ' Replace the following variables with your MongoDB Atlas connection details
    Private ReadOnly dbName As String = "Admin1"
    Private ReadOnly collectionName As String = "User"
    Dim conclient As MongoClient
    Dim client As New MongoClient(connectionUri)
    Dim database As IMongoDatabase = client.GetDatabase(dbName)
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)
    Private WithEvents openFileDialog As New OpenFileDialog()




    Private Sub CustomerM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        Guna2DataGridView1.Rows.Clear()

        ' Load data from MongoDB and add it to DataGridView
        Dim documents = mongoCollection.Find(New BsonDocument()).ToList()
        For Each doc In documents
            Dim Name = doc("Name").ToString()
            Dim Address = doc("Address").ToString()
            Dim ContactNum = doc("ContactNum").ToString()
            Dim CUser = doc("CUser").ToString()
            Dim Cpass = doc("Cpass").ToString()
            Dim Ccpass = doc("Ccpass").ToString()
            Guna2DataGridView1.Rows.Add(Name, Address, ContactNum, CUser, Cpass, Ccpass)
        Next
    End Sub


    Private Function GetSourceData() As List(Of BsonDocument)
        Dim sourceDatabase As IMongoDatabase = conclient.GetDatabase("Admin1")
        Dim sourceCollection As IMongoCollection(Of BsonDocument) = sourceDatabase.GetCollection(Of BsonDocument)("User")

        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty
        Return sourceCollection.Find(filter).ToList()
    End Function


    Private Sub Guna2DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim rowIndex = e.RowIndex
            TextBox1.Text = Guna2DataGridView1.Rows(rowIndex).Cells(0).Value.ToString()
            TextBox2.Text = Guna2DataGridView1.Rows(rowIndex).Cells(1).Value.ToString()
            TextBox3.Text = Guna2DataGridView1.Rows(rowIndex).Cells(2).Value.ToString()
            TextBox4.Text = Guna2DataGridView1.Rows(rowIndex).Cells(3).Value.ToString()
            TextBox5.Text = Guna2DataGridView1.Rows(rowIndex).Cells(4).Value.ToString()
            TextBox6.Text = Guna2DataGridView1.Rows(rowIndex).Cells(5).Value.ToString()
        End If
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Form12.Show()
        Me.Hide()

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim dataToInsert As New BsonDocument()
        dataToInsert.Add("Name", TextBox1.Text)
        dataToInsert.Add("Address", TextBox2.Text)
        dataToInsert.Add("ContactNum", TextBox3.Text)
        dataToInsert.Add("CUser", TextBox4.Text)
        dataToInsert.Add("Cpass", TextBox5.Text)
        dataToInsert.Add("Ccpass", TextBox6.Text)
        ' Add more fields as needed

        If TextBox2.Text = "" And TextBox3.Text = "" And TextBox4.Text = "" And TextBox5.Text = "" And TextBox6.Text = "" Then
            MsgBox("Please fill out all fields!", vbInformation + vbOKOnly, "Customer Management System")
        ElseIf TextBox1.Text = "" Then
            MsgBox("Please fill out Name!", vbInformation + vbOKOnly, "Customer Management System")
        ElseIf TextBox2.Text = "" Then
            MsgBox("Please fill out Address!", vbInformation + vbOKOnly, "Customer Management System")
        ElseIf TextBox3.Text = "" Then
            MsgBox("Please fill out  ContactNumber!", vbInformation + vbOKOnly, "Customer Management System")
        ElseIf TextBox4.Text = "" Then
            MsgBox("Please fill out Customer User", vbInformation + vbOKOnly, "Customer Management System")
        ElseIf TextBox5.Text = "" Then
            MsgBox("Please fill out Password!", vbInformation + vbOKOnly, "Customer Management System")
        ElseIf TextBox6.Text = "" Then
            MsgBox("Please fill out Confirmation Password!", vbInformation + vbOKOnly, "Customer Management System")

        Else
            Try
                If MsgBox("Do you want to save?.", vbYesNo + vbQuestion, "Customer Management System") = MsgBoxResult.Yes Then
                    collection.InsertOne(dataToInsert)
                    MsgBox("Account Saved!", vbInformation + vbOKOnly, "Success")
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox6.Clear()
                    LoadData()
                End If
            Catch ex As Exception
                MessageBox.Show("Error inserting data: ", vbCritical & ex.Message)
            End Try
        End If

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' There are selected rows, proceed with the update
            Dim rowIndex = Guna2DataGridView1.SelectedRows(0).Index
            Dim currentName = Guna2DataGridView1.Rows(rowIndex).Cells(0).Value.ToString()

            ' Construct an update operation based on the fields you want to update
            Dim update = Builders(Of BsonDocument).Update.
            Set(Of String)("Name", TextBox1.Text).
            Set(Of String)("Address", TextBox2.Text).
            Set(Of String)("ContactNum", TextBox3.Text).
            Set(Of String)("CUser", TextBox4.Text).
            Set(Of String)("Cpass", TextBox5.Text).
            Set(Of String)("Ccpass", TextBox6.Text)

            ' Construct a filter to identify the document to update (in this case, based on the "Name" field)
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("Name", currentName)

            Try
                ' Execute the update operation and capture the result
                Dim updateResult = mongoCollection.UpdateOne(filter, update)

                ' Check the update result
                If updateResult.ModifiedCount > 0 Then
                    MsgBox("Account updated successfully!", vbInformation + vbOKOnly, "Success")
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox6.Clear()
                    LoadData()
                Else
                    MsgBox("No documents were updated.", vbInformation + vbOKOnly, "Fail")
                End If
            Catch ex As Exception
                MsgBox("Error updating documents: " & ex.Message, vbCritical)
            End Try
        Else
            ' No selected rows, show a message
            MsgBox("Please select a data first!", vbInformation + vbOKOnly, "Admin")
        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        If Guna2DataGridView1.SelectedRows.Count = 1 Then
            Dim rowIndex = Guna2DataGridView1.SelectedRows(0).Index
            Dim Name = Guna2DataGridView1.Rows(rowIndex).Cells(0).Value.ToString()
            Dim Address = Guna2DataGridView1.Rows(rowIndex).Cells(1).Value.ToString()
            Dim ContactNum = Guna2DataGridView1.Rows(rowIndex).Cells(2).Value.ToString()
            Dim CUser = Guna2DataGridView1.Rows(rowIndex).Cells(3).Value.ToString()
            Dim Cpass = Guna2DataGridView1.Rows(rowIndex).Cells(4).Value.ToString()
            Dim Ccpass = Guna2DataGridView1.Rows(rowIndex).Cells(5).Value.ToString()

            mongoCollection.DeleteOne(New BsonDocument("Name", Name))
            mongoCollection.DeleteOne(New BsonDocument("Address", Address))
            mongoCollection.DeleteOne(New BsonDocument("ContactNum", ContactNum))
            mongoCollection.DeleteOne(New BsonDocument("CUser", CUser))
            mongoCollection.DeleteOne(New BsonDocument("Cpass", Cpass))
            mongoCollection.DeleteOne(New BsonDocument("Ccpass", Ccpass))

            ' Clear textboxes and reload data
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            LoadData()
        End If
    End Sub

    Private Sub Guna2Button4_Click_1(sender As Object, e As EventArgs)
        RFIDloyaltycard.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        LoadData()
    End Sub

End Class