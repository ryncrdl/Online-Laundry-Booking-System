Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class Form9
    Private reservedDates As New List(Of Date)()
    Private mongoClient As MongoClient
    Private mongoDatabase As IMongoDatabase
    Private mongoCollection As IMongoCollection(Of BsonDocument)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize MongoDB connection
        mongoClient = New MongoClient("mongodb+srv://OLBS1:OLBS123@cluster0.5benygr.mongodb.net/?retryWrites=true&w=majority")
        mongoDatabase = mongoClient.GetDatabase("Admin1")
        mongoCollection = mongoDatabase.GetCollection(Of BsonDocument)("ReservedDates")
    End Sub
    Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        Dim selectedDate As Date = MonthCalendar1.SelectionStart.Date
        If Not reservedDates.Contains(selectedDate) Then
            reservedDates.Add(selectedDate)
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Hide()
        Form12.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form12.Show()
    End Sub
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub


    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged

        TextBox2.Text = MonthCalendar1.SelectionRange.Start
        TextBox3.Text = MonthCalendar1.SelectionRange.End

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        ' Save reserved dates to MongoDB
        For Each dateToReserve In reservedDates
            Dim document As New BsonDocument()
            document("ReservedDate") = dateToReserve
            mongoCollection.InsertOne(document)
        Next

        ' Clear the reserved dates list
        reservedDates.Clear()

        ' You can add additional logic here, such as updating UI, displaying a message, etc.
    End Sub
End Class
