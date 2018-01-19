Public Class Settings
    Dim L As New Timer
    Dim D As New Timer
    Dim K As New Timer
    Dim CurPos As Point
    Dim _form As New Form
    Dim lbl As New Label

    Public Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = False Then Exit Sub
        For Each prm As Car_Model In Models
            prm.ScnNode.SetMaterialType(IrrlichtNETCP.MaterialType.TransparentAlphaChannelRef)
            prm.ScnNode.SetMaterialFlag(IrrlichtNETCP.MaterialFlag.Wireframe, False)
            prm.ScnNode.SetMaterialFlag(IrrlichtNETCP.MaterialFlag.PointCloud, False)
        Next
    End Sub

    Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = False Then Exit Sub
        For Each prm As Car_Model In Models
            prm.ScnNode.SetMaterialType(IrrlichtNETCP.MaterialType.TransparentAlphaChannelRef)
            prm.ScnNode.SetMaterialFlag(IrrlichtNETCP.MaterialFlag.Wireframe, True)
            prm.ScnNode.SetMaterialFlag(IrrlichtNETCP.MaterialFlag.PointCloud, False)
        Next
    End Sub
    Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = False Then Exit Sub
        For Each prm As Car_Model In Models
            prm.ScnNode.SetMaterialType(IrrlichtNETCP.MaterialType.TransparentAlphaChannelRef)
            prm.ScnNode.SetMaterialFlag(IrrlichtNETCP.MaterialFlag.PointCloud, True)
            prm.ScnNode.SetMaterialFlag(IrrlichtNETCP.MaterialFlag.Wireframe, False)
        Next
    End Sub

    Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = False Then Exit Sub
        For Each prm As Car_Model In Models
            prm.ScnNode.SetMaterialType(IrrlichtNETCP.MaterialType.TransparentAddColor)
            prm.ScnNode.SetMaterialFlag(IrrlichtNETCP.MaterialFlag.Wireframe, False)
            prm.ScnNode.SetMaterialFlag(IrrlichtNETCP.MaterialFlag.PointCloud, False)

        Next
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub



    Private Sub RadioButton10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton10.CheckedChanged

        If Initialized = False Then Exit Sub
        If RadioButton10.Checked = False Then Exit Sub
        Shell("car_load.exe -dx9", AppWinStyle.NormalFocus)
        End

    End Sub


    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
   
        ' Render.cam.Position = New IrrlichtNETCP.Vector3D(Render.cam.Position.X, Render.cte, Render.cam.Position.Z)

        ' For Each prm As Car_Model In Models

        '  Next
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += MousePosition - CurPos
            CurPos = MousePosition
        Else
            CurPos = MousePosition
        End If
    End Sub



    Private Sub Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim p As New Drawing2D.GraphicsPath()
        p.StartFigure()

        p.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        p.AddLine(20, 0, Button6.Width - 20, 0)
        p.AddArc(New Rectangle(Button6.Width - 20, 0, 20, 20), -90, 90)
        p.AddLine(Button6.Width, 20, Button6.Width, Button6.Height - 20)
        p.AddArc(New Rectangle(Button6.Width - 20, Button6.Height - 20, 20, 20), 0, 90)
        p.AddLine(Button6.Width - 20, Button6.Height, 20, Button6.Height)
        p.AddArc(New Rectangle(0, Button6.Height - 20, 20, 20), 90, 90)
        p.CloseFigure()
        Button6.Region = New Region(p)

        If GetSetting("Car Load", "settings", "zoom", 0) > 0 Then TrackBar1.Value = GetSetting("Car Load", "settings", "zoom", 1)
        ComboBox1.Text = GetSetting("Car Load", "settings", "ChangeEach", "60 seconds (1 minute)")



        'sett!
        Me.Height = 266
        Me.Width = _3Dsettings.Left + _3Dsettings.Width + RadioButton5.Width + 5 '438 '372

        'init panels
        _Apparences.Location = _3Dsettings.Location
        _Console.Location = _3Dsettings.Location
        _Screenshot.Location = _3Dsettings.Location
        _View.Location = _3Dsettings.Location
        'Dire.Location = _3Dsettings.Location


        _Apparences.Hide()
        _3Dsettings.Show()
        _Console.Hide()
        _Screenshot.Hide()
        _View.Hide()
        '  Dire.Hide()

        Select Case Render.DvType
            Case IrrlichtNETCP.DriverType.OpenGL
                RadioButton7.Checked = True
            Case IrrlichtNETCP.DriverType.Direct3D8
                RadioButton9.Checked = True
            Case IrrlichtNETCP.DriverType.Direct3D9
                RadioButton10.Checked = True
            Case IrrlichtNETCP.DriverType.Software
                RadioButton8.Checked = True
            Case IrrlichtNETCP.DriverType.Software2
                RadioButton6.Checked = True
        End Select
    End Sub

    Private Sub RadioButton9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton9.CheckedChanged
        If Initialized = False Then Exit Sub
        If RadioButton9.Checked = False Then Exit Sub
        Shell("car_load.exe -dx8", AppWinStyle.NormalFocus)
        End

    End Sub

    Private Sub RadioButton7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton7.CheckedChanged
        If Initialized = False Then Exit Sub
        If RadioButton7.Checked = False Then Exit Sub
        Shell("car_load.exe -gl", AppWinStyle.NormalFocus)
        End
    End Sub

    Private Sub RadioButton8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton8.CheckedChanged
        If Initialized = False Then Exit Sub
        If RadioButton8.Checked = False Then Exit Sub
        Shell("car_load.exe -sw", AppWinStyle.NormalFocus)
        End

    End Sub

    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        If Initialized = False Then Exit Sub
        If RadioButton6.Checked = False Then Exit Sub
        Shell("car_load.exe -sw2", AppWinStyle.NormalFocus)
        End

    End Sub

    Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveSetting("Car Load", "settings", "Opacity", TrackBar2.Value)
        SaveSetting("Car Load", "settings", "zoom", TrackBar1.Value)
        SaveSetting("Car Load", "settings", "ChangeEach", ComboBox1.Text)
        Render.Device.Dispose()
        Process.GetCurrentProcess.Kill()

        End

    End Sub

    Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        Try
            Dim image As IrrlichtNETCP.Image = Render.VideoDriver.CreateScreenShot()
        

            Dim name = _car.Theory.MainInfos.Name
            Do Until name(0) <> Chr(9) And name(0) <> " "
                name = Mid(name, 2)
            Loop

            Dim tmp = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) & "\" & name & ".png"
            'Dim tmp = "C:\Users\kallel\pics" & "\" & name.Replace("*", "_") & ".png"
            Render.VideoDriver.WriteImageIntoFile(image, tmp)

            image.Dispose()

            _form.BackColor = Color.Black
            _form.FormBorderStyle = Windows.Forms.FormBorderStyle.None

            _form.Size = New Point(RenderForm.Width / 2, RenderForm.Height / 2)

            _form.StartPosition = FormStartPosition.CenterScreen
            _form.Opacity = 0


            K.Interval = 10
            AddHandler K.Tick, AddressOf tickMe
            K.Start()


            lbl.AutoSize = False
            lbl.Width = RenderForm.Width / 2
            lbl.Height = 500
            lbl.TextAlign = ContentAlignment.TopCenter
            lbl.BackColor = Color.Transparent
            lbl.ForeColor = Color.DarkBlue
            lbl.Font = New Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point)

            _form.Controls.Add(lbl)
            lbl.Text = vbNewLine & "saved to... " & vbNewLine & tmp

            _form.TopMost = True
            _form.BackgroundImage = Drawing.Image.FromFile(tmp)
            _form.BackgroundImageLayout = ImageLayout.Stretch

            _form.Show()




            L.Interval = 10
            AddHandler L.Tick, AddressOf TickMeAgain


            D.Interval = 3000
            AddHandler D.Tick, AddressOf BeginNTimer
            D.Start()
        Catch

        End Try


    End Sub
    Sub tickMe()
        _form.Opacity += 0.03
        If _form.Opacity > 0.9 Then
            K.Stop()
        End If
    End Sub
    Sub TickMeAgain()
        _form.Opacity -= 0.03
        If _form.Opacity < 0.05 Then
            L.Stop()
            _form.Hide()
            _form.BackgroundImage.Dispose()

        End If
    End Sub
    Sub BeginNTimer()
        D.Stop()
        L.Start()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = False Then Exit Sub
        _Apparences.Hide()
        _3Dsettings.Show()
        _Console.Hide()
        _Screenshot.Hide()
        _View.Hide()
        '_Dire.Hide()
    End Sub

    Private Sub RadioButton11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton11.CheckedChanged
        If RadioButton11.Checked = False Then Exit Sub
        _Apparences.Show()
        _3Dsettings.Hide()
        _Console.Hide()
        _Screenshot.Hide()
        _View.Hide()
        ' _Dire.Hide()
    End Sub


    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        If InStr(ComboBox1.Text, " ") = 0 Then Exit Sub
        If Split(ComboBox1.Text, " ")(0) * 1000 <> 0 Then

            RenderForm.Timer1.Start()
            RenderForm.Timer1.Interval = Split(ComboBox1.Text, " ")(0) * 1000
        Else
            RenderForm.Timer1.Stop()
        End If


    End Sub

    Private Sub TrackBar2_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar2.ValueChanged
        If Initialized <> True Then Exit Sub
        Me.Opacity = TrackBar2.Value / 100
        RenderForm.Opacity = Me.Opacity
        Car_browser.Opacity = Me.Opacity
        Editor.Opacity = Me.Opacity
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ColorDialog1.Color = Render._cc.dotNETColor
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Render._cc = New IrrlichtNETCP.Color(ColorDialog1.Color.A, ColorDialog1.Color.R, ColorDialog1.Color.G, ColorDialog1.Color.B)
        End If

    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadioButton12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton12.CheckedChanged
        If RadioButton12.Checked = False Then Exit Sub
        _Console.Show()
        _3Dsettings.Hide()
        _Apparences.Hide()
        _Screenshot.Hide()
        _View.Hide()
        '_Dire.Hide()
    End Sub


    Private Sub RadioButton13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton13.CheckedChanged
        If RadioButton13.Checked = False Then Exit Sub
        _Console.Hide()
        _3Dsettings.Hide()
        _Apparences.Hide()
        _Screenshot.Show()
        _View.Hide()
        '_Dire.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        RenderForm.WindowState = FormWindowState.Minimized
        Me.WindowState = FormWindowState.Minimized
        Car_browser.WindowState = FormWindowState.Minimized
        Directory.WindowState = FormWindowState.Minimized
        Editor.WindowState = FormWindowState.Minimized
    End Sub


    Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        PlusOne()
    End Sub
    Sub PlusOne()

        If Car_browser.ListBox2.SelectedIndex + 1 = Car_browser.ListBox2.Items.Count Then Car_browser.ListBox2.SelectedIndex = -1
        Car_browser.ListBox2.SelectedIndex += 1
        'Car_browser.ListBox2_DoubleClick(sender, e)
    End Sub
    Sub MinusOne()

        If Car_browser.ListBox2.SelectedIndex <= 0 Then
            Car_browser.ListBox2.SelectedIndex = Car_browser.ListBox2.Items.Count - 1
            Exit Sub
        End If

        Car_browser.ListBox2.SelectedIndex -= 1
        'Car_browser.ListBox2_DoubleClick(sender, e)
    End Sub

    Private Sub _View_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radiobutton14.CheckedChanged
        If radiobutton14.Checked = False Then Exit Sub
        _Apparences.Hide()
        _3Dsettings.Hide()
        _Console.Hide()
        _Screenshot.Hide()
        _View.Show()

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Initialized = False Then Exit Sub
        If cBODY Is Nothing Then Exit Sub
        EnDis()
    End Sub
    Sub EnDis()
        If _car.Theory Is Nothing Then Exit Sub
        If cBODY IsNot Nothing Then
            cBODY.ScnNode.Visible = CheckBox1.Checked

        End If



        For n = 0 To 3
            If _car.Theory.wheel(n).modelNumber <> -1 And _Wheel(n) IsNot Nothing Then _Wheel(n).ScnNode.Visible = CheckBox2.Checked
            If _car.Theory.Spring(n).modelNumber <> -1 Then _Spring(n).ScnNode.Visible = CheckBox3.Checked
            If _car.Theory.PIN(n).modelNumber <> -1 Then _Pin(n).ScnNode.Visible = CheckBox4.Checked
            If _car.Theory.Axle(n).modelNumber <> -1 Then _axle(n).ScnNode.Visible = CheckBox5.Checked
        Next n
        If _car.Theory.Spinner.modelNumber <> -1 Then _Spinner.ScnNode.Visible = CheckBox6.Checked
        If _car.Theory.Aerial.ModelNumber <> -1 Then _Aerial.ScnNode.Visible = CheckBox7.Checked


    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If Initialized = False Then Exit Sub
        EnDis()
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If Initialized = False Then Exit Sub
        EnDis()
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If Initialized = False Then Exit Sub
        EnDis()
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If Initialized = False Then Exit Sub
        EnDis()
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If Initialized = False Then Exit Sub
        EnDis()
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If Initialized = False Then Exit Sub
        EnDis()
        'addhandler could be better...
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Shell("car_load " & Command() & " /ss", AppWinStyle.NormalFocus)
        Dim pro As New Process
        pro = Process.GetCurrentProcess()
        pro.Kill()

    End Sub

    Private Sub RadioButton14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton14.CheckedChanged
        If RadioButton14.Checked = False Then Exit Sub
        _Apparences.Hide()
        _3Dsettings.Hide()
        _Console.Hide()
        _Screenshot.Hide()
        _View.Show()
        '_Dire.Hide()
    End Sub



    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Shell("car_load /config")
        End
    End Sub

    Private Sub Button1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.MouseLeave
        Button1.Image = My.Resources.fileclose
    End Sub

    Private Sub Button1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseMove
        Button1.Image = My.Resources.window_close
    End Sub


    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        About.Show()
    End Sub


    Private Sub TrackBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar1.ValueChanged
        Render.cte = TrackBar1.Value
        If Not Initialized Then Exit Sub
        '     Render.cam.Position = New IrrlichtNETCP.Vector3D(Render.cam.Target.X + -Math.Cos(Render.alpha) * Render.cte, Render.cam.Position.Y, Render.cam.Target.Z + Math.Sin(Render.alpha) * Render.cte)

    End Sub


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged

    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If Initialized = False Then Exit Sub
        EnDis()
    End Sub

    Private Sub Settings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub
End Class