Imports System.Windows.Forms

Public Class Dialog1

    Private Sub Dialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If _car.Path = "" Then

            Me.Hide()
            Me.Close()
            Exit Sub
        End If

    End Sub
    Dim n = 3
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If RichTextBox1.Text = IO.File.ReadAllText(_car.Path & "\parameters.txt") Then Exit Sub

        n -= 1


        Me.Text = "Editor (Autosave and reload in " & n & " seconds)"
       
            If n = 0 Then
                n = 3
                SaveAll()
            Timer1.Stop()
            End If
    End Sub
    Sub SaveAll()

        If RichTextBox1.Text = IO.File.ReadAllText(_car.Path & "\parameters.txt") Then Exit Sub
        If IO.Directory.Exists(Replace(_car.Path, "\\", "\") & "\" & "parameters") = False Then MkDir(Replace(_car.Path, "\\", "\") & "\" & "parameters")
        FileCopy(Replace(_car.Path, "\\", "\") & "\parameters.txt", Replace(_car.Path, "\\", "\") & "\parameters\" & Now.Day & "_" & Now.Month & "_" & Now.Year & " " & Now.Hour & "_" & Now.Minute & "_" & Now.Second & ".txt")
        IO.File.WriteAllLines(Replace(_car.Path, "\\", "\") & "\" & "parameters.txt", RichTextBox1.Lines)
        Me.Text = "Editor"
        Car_browser.LoadOneCar()
      
    End Sub
    Sub RestartTimer()
        Me.Text = "Editor"
        n = 3
        Timer1.Start()
    End Sub

    Private Sub RichTextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox1.KeyUp
        RestartTimer()
    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Dialog1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        RichTextBox1.Text = IO.File.ReadAllText(_car.Path & "\parameters.txt")
    End Sub
End Class
