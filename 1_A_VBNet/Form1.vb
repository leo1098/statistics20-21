Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AllowDrop = True
    End Sub
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

    Private Sub ListBox1_DragEnter(sender As Object, e As DragEventArgs) Handles ListBox1.DragEnter
        'If it contains a String type
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            'Copy
            e.Effect = DragDropEffects.Copy
        Else
            'Otherwise undo the action
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ListBox1_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox1.DragDrop
        'Get strings in memory
        Dim S() As String = e.Data.GetData(DataFormats.FileDrop)
        'Put the file's path in the list box
        For Each path In S
            Me.ListBox1.Items.Add(path)
        Next

    End Sub


End Class
