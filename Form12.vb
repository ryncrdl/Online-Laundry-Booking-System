Public Class Form12
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With CustomerM
            .TopLevel = False
            Panel5.Controls.Add(CustomerM)
            .BringToFront()
            .Show()
        End With
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        With RFIDloyaltycard
            .TopLevel = False
            Panel5.Controls.Add(RFIDloyaltycard)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        With CADMINacc
            .TopLevel = False
            Panel5.Controls.Add(CADMINacc)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Form3.Show()
        Me.Hide()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        With SMS
            .TopLevel = False
            Panel5.Controls.Add(SMS)
            .BringToFront()
            .Show()
        End With
    End Sub
End Class