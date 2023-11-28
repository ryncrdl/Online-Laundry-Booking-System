Imports System.IO
Imports System.IO.TextWriter
Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class createAccADMIN
    Dim connectionUri As String = "mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority"
    ' Replace the following variables with your MongoDB Atlas connection details
    Private ReadOnly dbName As String = "Admin1"
    Private ReadOnly collectionName As String = "Admin"
    Dim client As New MongoClient(connectionUri)
    Dim database As IMongoDatabase = client.GetDatabase(dbName)
    Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub createAccADMIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList ' Make it non-editable
        ComboBox1.SelectedIndex = 0 ' Set the default selection
        Me.Controls.Add(ComboBox1)



    End Sub

    Private Sub Guna2TextBox1_Enter(sender As Object, e As EventArgs) Handles Guna2TextBox1.Enter
        If Guna2TextBox1.Text = "Name" Then
            Guna2TextBox1.Text = ""
            Guna2TextBox1.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Guna2TextBox1_Leave(sender As Object, e As EventArgs) Handles Guna2TextBox1.Leave
        If Guna2TextBox1.Text = "" Then
            Guna2TextBox1.Text = "Name"
            Guna2TextBox1.ForeColor = Color.FromArgb(125, 137, 149)
        End If
    End Sub

    Private Sub Guna2TextBox2_Enter(sender As Object, e As EventArgs) Handles Guna2TextBox2.Enter
        If Guna2TextBox2.Text = "Password" Then
            Guna2TextBox2.Text = ""
            Guna2TextBox2.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Guna2TextBox2_Leave(sender As Object, e As EventArgs) Handles Guna2TextBox2.Leave
        If Guna2TextBox2.Text = "" Then
            Guna2TextBox2.Text = "Password"
            Guna2TextBox2.ForeColor = Color.FromArgb(125, 137, 149)
        End If
    End Sub

    Private Sub Guna2TextBox3_Leave(sender As Object, e As EventArgs) Handles Guna2TextBox3.Leave
        If Guna2TextBox3.Text = "" Then
            Guna2TextBox3.Text = "Password"
            Guna2TextBox3.ForeColor = Color.FromArgb(125, 137, 149)
        End If
    End Sub

    Private Sub Guna2TextBox3_Enter(sender As Object, e As EventArgs) Handles Guna2TextBox3.Enter
        If Guna2TextBox3.Text = "Password" Then
            Guna2TextBox3.Text = ""
            Guna2TextBox3.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Me.Hide()
    End Sub

    Private Sub Guna2TextBox3_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox3.TextChanged
        If Guna2TextBox2.Text <> "" And Guna2TextBox2.Text = Guna2TextBox3.Text Then
            ErrorProvider1.SetError(Guna2TextBox3, "")
        Else
            ErrorProvider1.SetError(Guna2TextBox3, "Password or Confirm password does not matched.")
        End If
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            Guna2TextBox2.PasswordChar = ""
            Guna2TextBox3.PasswordChar = ""
        Else
            Guna2TextBox2.PasswordChar = "*"
            Guna2TextBox3.PasswordChar = "*"

        End If



    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim dataToInsert As New BsonDocument()
        dataToInsert.Add("adminuser", Guna2TextBox1.Text)
        dataToInsert.Add("password", Guna2TextBox2.Text)
        dataToInsert.Add("Conpassword", Guna2TextBox3.Text)
        dataToInsert.Add("UserTYPE", ComboBox1.Text)

        ' Add more fields as needed
        If Guna2TextBox1.Text = "" And Guna2TextBox2.Text = "" And Guna2TextBox3.Text = "" And ComboBox1.Text = "" Then
            MsgBox("Please fill out all fields!", vbInformation + vbOKOnly, "Admin Management System")
        ElseIf Guna2TextBox1.Text = "" Then
            MsgBox("Please fill out Admin User!", vbInformation + vbOKOnly, "Admin Management System")
        ElseIf Guna2TextBox2.Text = "" Then
            MsgBox("Please fill out Password!", vbInformation + vbOKOnly, "Admin Management System")
        ElseIf Guna2TextBox3.Text = "" Then
            MsgBox("Please fill out  Confirmation Password!", vbInformation + vbOKOnly, "Admin Management System")
        ElseIf ComboBox1.Text = "" Then
            MsgBox("Please Select User Type!", vbInformation + vbOKOnly, "Admin Management System")


        Else
            Try
                If MsgBox("Do you want to save?.", vbYesNo + vbQuestion, "Customer Management System") = MsgBoxResult.Yes Then
                    collection.InsertOne(dataToInsert)
                    MsgBox("Admin Account Saved!!", vbInformation + vbOKOnly, "Success")
                    Guna2TextBox1.Clear()
                    Guna2TextBox2.Clear()
                    Guna2TextBox3.Clear()
                    ComboBox1.Text = ""
                    CADMINacc.LoadData()
                    Me.Hide()
                End If
            Catch ex As Exception
                MessageBox.Show("Error inserting data: ", vbCritical & ex.Message)
            End Try
        End If
    End Sub
    Private Function GetDataFromMongoDB() As DataTable
        Dim dataTable As New DataTable()

        ' Fetch all documents from the collection
        Dim documents = collection.Find(New BsonDocument()).ToList()

        ' Create columns in the DataTable
        dataTable.Columns.Add("adminuser", GetType(String))
        dataTable.Columns.Add("password", GetType(String))
        dataTable.Columns.Add("Conpassword", GetType(String))
        dataTable.Columns.Add("UserTYPE", GetType(String))
        ' Add more columns as needed

        ' Populate the DataTable with data from MongoDB
        For Each doc As BsonDocument In documents
            Dim adminuser As String = doc.GetValue("adminuser").AsString
            Dim password As String = doc.GetValue("password").AsString
            Dim Conpassword As String = doc.GetValue("Conpassword").AsString
            Dim UserTYPE As String = doc.GetValue("UserTYPE").AsString

            ' Retrieve other fields as needed

            dataTable.Rows.Add(adminuser, password, Conpassword, UserTYPE)
        Next
        Return dataTable
    End Function
End Class