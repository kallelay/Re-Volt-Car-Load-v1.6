Public Class Directory
    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        If IO.Directory.Exists(TextBox7.Text) = True Then
            ListView1.Items.Clear()
            ChDir(TextBox7.Text)
            Dim Dirs() As String
            Dirs = IO.Directory.GetDirectories(CurDir)

            '    If Not Mid(TextBox7.Text, 2) = ":\" Then _
            '  ListView1.Items.Add("..", 0)

            For i = LBound(Dirs) To UBound(Dirs)
                Dim LV As New ListViewItem
                LV.ImageIndex = 0
                LV.Text = Dirs(i).Split("\")(UBound(Dirs(i).Split("\")))
                ListView1.Items.Add(LV)


            Next


        End If

        If IO.Directory.Exists(CurDir() & "\cars\") = True Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If


    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedItems.Count = 0 Then Exit Sub
        If ListView1.SelectedItems(0).Text <> "" Or ListView1.SelectedItems(0).Text <> Nothing Then
            Try
                ChDir(ListView1.SelectedItems(0).Text)
                ListView1.Items.Clear()
                TextBox7.Text = CurDir()
            Catch

            End Try
        End If
    End Sub

    Private Sub ListView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListView1.KeyPress
        If e.KeyChar = Chr(13) Then
            ListView1_DoubleClick(sender, e)
        ElseIf e.KeyChar = Chr(8) Then
            Button2_Click(sender, e)
        End If
    End Sub

    Private Sub Me_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If GetSetting("Car Load", "settings", "dir", "") = "" Then
            ChDir(Environ("programfiles") & "\..\")
        Else
            ChDir(GetSetting("Car Load", "settings", "dir", ""))
        End If
        TextBox7.Text = CurDir()
        Dim Dirs() As String
        Dirs = IO.Directory.GetDirectories(CurDir)

        ListView1.Items.Add("..", 0)




        '   For i = LBound(Dirs) To UBound(Dirs)
        'Dim LV2 As New ListViewItem
        '    LV2.ImageIndex = 0
        '   LV2.Text = Dirs(i).Split("\")(UBound(Dirs(i).Split("\")))
        '   ListView1.Items.Add(LV2)
        '   Next

        If GetSetting("Car Load", "settings", "dir", "") <> "" Then
            TextBox7.Text = GetSetting("Car Load", "settings", "dir", "")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RvPath = TextBox7.Text
        If CheckBox1.Checked = False Then
            SaveSetting("Car Load", "settings", "dir", RvPath)
        End If


        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Directory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        startfromhere.Location = New Point(0, -5000)
        Dim drv = IO.Directory.GetLogicalDrives()
        For i = 0 To drv.Count - 1
            ComboBox1.Items.Add(drv(i))
        Next
        With ComboBox2.Items
            .Add(Environment.SpecialFolder.CommonApplicationData)
            .Add(Environment.SpecialFolder.CommonProgramFiles)
            .Add(Environment.SpecialFolder.Desktop)
            .Add(Environment.SpecialFolder.MyDocuments)
            .Add(Environment.SpecialFolder.MyMusic)
            .Add(Environment.SpecialFolder.MyPictures)
            .Add(Environment.SpecialFolder.Personal)
            .Add(Environment.SpecialFolder.ProgramFiles)
            .Add(Environment.SpecialFolder.System)
        End With


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ChDir("..\")
        TextBox7.Text = CurDir()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If IO.Directory.Exists(ComboBox1.Text) Then
            ChDir(ComboBox1.Text)
            TextBox7.Text = CurDir()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

        ChDir(Environment.GetFolderPath(ComboBox2.SelectedItem))
        TextBox7.Text = CurDir()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox7_TextChanged(sender, e)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class