Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.RichTextBox1.Text = "A Sample Text :)"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.RichTextBox1.Text = ""
    End Sub

    Private Sub RichTextBox1_MouseEnter(sender As Object, e As EventArgs) Handles RichTextBox1.MouseEnter
        Dim rnd As New Random
        Me.RichTextBox1.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))
    End Sub

    Private Sub RichTextBox1_MouseLeave(sender As Object, e As EventArgs) Handles RichTextBox1.MouseLeave
        Me.RichTextBox1.BackColor = Color.White
    End Sub
End Class
