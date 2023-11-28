Public Class LOAD
    Dim per
    Private Sub LOAD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Value += 5
        Label3.Text = "Process " & per & " % Complete..."
        per += 5
        If ProgressBar1.Value = 100 Then
            Timer1.Stop()
            Form3.Show()
            Me.Hide()



        End If


    End Sub

    Private Sub LOAD_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub
End Class
