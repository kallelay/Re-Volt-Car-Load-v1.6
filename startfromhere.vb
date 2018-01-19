Imports System.Windows.Forms
Imports IrrlichtNETCP
Imports Car_Load.Upperclass


Public Class startfromhere
    Public ReflectionM As Texture
    Public ScreenSaverMode = False
    Private Sub startfromhere_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Command() <> "" Then
            SaveSetting("Car Load", "settings", "lastrnd", Command)
        End If
    End Sub
    Sub Progress(ByVal n)
        Label3.Text = Format((n / 18) * 100, "0#.##") & "%"
        Label2.Text = Label3.Text
        Label2.Width = (n / 18) * Label3.Width
    End Sub
    Dim n = 0
    Private Sub startfromhere_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        If InStr(Command, "/help", CompareMethod.Text) > 0 Then

            MsgBox("You requested help :) " & vbNewLine & _
                   Chr(9) & "normal run: car_load /switch -renderswitch" & vbNewLine & _
            vbNewLine & _
            "Possible arguments:" & vbNewLine & _
             Chr(9) & "/aero   : Launch Aero mode on (only vista, seven +)" & vbNewLine & _
             Chr(9) & "/config : Reconfiguring the directory" & vbNewLine & _
             Chr(9) & "/ss     : Screensaver mode" & vbNewLine & _
             vbNewLine & _
             "Possible Render switches" & vbNewLine & _
             Chr(9) & "-dx8 : DirectX 8 mode" & vbNewLine & _
             Chr(9) & "-dx9 : DirectX 9 mode (default& vbnewline & _" & vbNewLine & _
             Chr(9) & "-gl  : OpenGL mode" & vbNewLine & _
             Chr(9) & "-sw  : Software mode" & vbNewLine & _
             Chr(9) & "-sw2 : Another Software mode" & vbNewLine, MsgBoxStyle.Information)
            End
        End If


        Label1.Text = "Loading in progress"

        n += 1
        Progress(n)

        Dim AeroOn As Boolean = False
        Application.DoEvents()
        Me.Show()
        Label1.Text = "Checking arguments"
        n += 1
        Progress(n)
        Application.DoEvents()
        If InStr(Command, "/config", CompareMethod.Text) > 0 Then
            Label1.Text = "Configuring Directory mode"
            Application.DoEvents()
            If Directory.ShowDialog() <> Windows.Forms.DialogResult.OK Then End
            Application.DoEvents()
            Me.Show()

        End If
        Application.DoEvents()
        If InStr(Command, "/aero", CompareMethod.Text) > 0 Then
            AeroOn = True
        End If
        Label1.Text = "Extracting RVL logo"
        n += 1
        Progress(n)
        Application.DoEvents()
        My.Resources.rvlive.Save(Environ("temp") & "\rvl.png", Imaging.ImageFormat.Png)

        Dim cmd As String = ""

        Label1.Text = "Getting settings"
        n += 1
        Progress(n)

        If GetSetting("Car Load", "settings", "lastrnd") <> "" Then
            cmd = GetSetting("Car Load", "settings", "lastrnd") <> ""
        End If


        Application.DoEvents()

        If Command() <> "" Then cmd = Command()

        If InStr(Command, "/ss") > 0 Then
            ScreenSaverMode = True
            cmd = Replace(cmd, "/ss", "")

        End If
        Application.DoEvents()
        Label1.Text = "Checking arguments"
        n += 1
        Progress(n)
        If cmd <> "" Then
            If InStr(cmd, "-") > 0 Then
                Select Case Replace(Split(Split(LCase(cmd) & " ", "-")(1), " ")(0), " ", "")
                    Case Is = "dx8"
                        Render.DvType = DriverType.Direct3D8
                    Case Is = "dx9"
                        Render.DvType = DriverType.Direct3D9
                    Case Is = "gl"
                        Render.DvType = DriverType.OpenGL
                    Case Is = "sw"
                        Render.DvType = DriverType.Software
                    Case Is = "sw2"
                        Render.DvType = DriverType.Software2
                End Select
            End If
        End If


        Label1.Text = "Checking directories"
        n += 1
        Progress(n)
        Application.DoEvents()
        If IO.Directory.Exists(Environ("temp") & "\carload\") = False Then _
            MkDir(Environ("temp") & "\carload\")

        Label1.Text = "Cleaning old temporary files"
        n += 1
        Progress(n)
        Application.DoEvents()
        If IO.Directory.GetFiles(Environ("temp") & "\carload\").Length > 0 Then _
        Kill(Environ("temp") & "\carload\*.*")



        Application.DoEvents()

        Label1.Text = "Checking Re-Volt directory"
        n += 1
        Progress(n)
        'Rv Path
        If RvPath = "" Then
            If GetSetting("Car Load", "settings", "dir", "") = "" Then
                If Directory.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    RvPath = Directory.TextBox7.Text
                    SaveSetting("Car Load", "settings", "dir", RvPath)

                End If
            End If



            Application.DoEvents()
            RvPath = GetSetting("Car Load", "settings", "dir", "")
        End If
        'new timer (For opacity)

        Application.DoEvents()

        If ScreenSaverMode Then
            Label1.Text = "Loading screensaver"
            n += 1
            Progress(n)
            Application.DoEvents()
            Render.FullScreen = True
            Render.Width = Screen.PrimaryScreen.Bounds.Width
            Render.Height = Screen.PrimaryScreen.Bounds.Height
            Editor.Show()
            Settings.Show()
            Settings.Location = New Point(500000, 0)
            Editor.Location = New Point(-5000, 0)
            Label1.Text = "Initializing cars"
            n += 1
            Progress(n)
            Application.DoEvents()
            Car_browser.Show()
            Car_browser.Location = New Point(-2000, 0)
            Car_browser.CheckBox1.Checked = True
            n += 1
            Progress(n)

            Label1.Text = "Initializing Render systems"
            Application.DoEvents()
            Render.InitFullScreen()
            Render.InitCamera()
            Car_browser.CheckBox1.Checked = True
            Editor.CheckBox2.Checked = True
            Car_browser.TrackBar1.Value = 22
            n += 1
            Progress(n)
            Label1.Text = "Initializing Car Theory"
            Application.DoEvents()
            CarTheory()

            Car_browser.ListBox2.SelectedIndex = 0
            Car_browser.LoadOneCar()
            Car_browser.NumericUpDown1.Value = 10

            RenderForm.Timer1.Interval = 10 * 1000
            Render.Go()



        End If


        Application.DoEvents()
        Label1.Text = "Initializing opacity"
        n += 1
        Progress(n)
        tim.Interval = 1
        AddHandler tim.Tick, AddressOf opacityDo
        tim.Start()

        If AeroOn Then
            Label1.Text = "Initializing Aero mode"
            Application.DoEvents()
            If Environment.OSVersion.Version.Major >= 6 Then
                Dim v As New Form
                v.Location = New Point(0, 0)
                v.Size = Screen.PrimaryScreen.Bounds.Size
                v.BackColor = Drawing.Color.Black
                v.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                ' v.Show()


                Aero(Car_browser)
                Settings.TrackBar2.Value = 100
                Car_browser.BackColor = Drawing.Color.Black
                Car_browser.ForeColor = Drawing.Color.White

                Car_browser.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
                Car_browser.ControlBox = False

                '  Aero(RenderForm)
                ' RenderForm.BackColor = Drawing.Color.Black
                ' RenderForm.ForeColor = Drawing.Color.White
                ' RenderForm.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D

                Aero(Settings, 0, -0, 5, 0)

                Settings.Label1.Hide()
                'Settings.Label1.BackColor = Drawing.Color.AliceBlue

                Settings.BackColor = Drawing.Color.Black
                Settings.ForeColor = Drawing.Color.White
                Settings.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
                Settings.ControlBox = False

                Aero(Editor)
                Editor.BackColor = Drawing.Color.Black
                Editor.ForeColor = Drawing.Color.White
                Editor.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
                Editor.ControlBox = False



            End If


        End If
        n += 1
        Progress(n)
        Label1.Text = "Initializing Render Form"
        Application.DoEvents()
        'Form1?
        RenderForm.Show()
        RenderForm.Opacity = 0
        RenderForm.Location = New Point(10, 10)
        RenderForm.Width = 900
        RenderForm.Height = 600
        'RenderForm.Height = RenderForm.Label1.Height

        '_Render.Init(False)
        Application.DoEvents()
        n += 1
        Progress(n)
        Label1.Text = "Initializing cars"
        Application.DoEvents()
        'car_browser?
        Car_browser.Show()
        Car_browser.Opacity = 0
        Car_browser.Top = 10
        Car_browser.Left = Screen.PrimaryScreen.Bounds.Width - Car_browser.Width - 10

        n += 1
        Progress(n)
        Label1.Text = "Initializing settings"
        Application.DoEvents()
        'Settings?
        Settings.Show()
        Settings.Opacity = 0
        Settings.Left = 10
        Settings.Top = Screen.PrimaryScreen.Bounds.Height - Settings.Height - 35

        n += 1
        Progress(n)
        Label1.Text = "Initializing Editor"
        Application.DoEvents()
        'edit?
        Editor.Show()
        Editor.Opacity = 0
        Editor.Left = Screen.PrimaryScreen.Bounds.Width - Editor.Width - 10
        Editor.Top = Screen.PrimaryScreen.Bounds.Height - Editor.Height - 35

        n += 1
        Progress(n)
        Label1.Text = "Initializing Irrlicht"
        Application.DoEvents()
        Render.Init()

        n += 1
        Progress(n)
        Label1.Text = "Initializing Car Theory"
        Application.DoEvents()
        CarTheory()




        n += 1
        Progress(n)
        Label1.Text = "Initializing Cameras"
        Application.DoEvents()
        If Initialized = True Then Render.cam.Position = New Vector3D(5, 5, 5)
        Initialized = True

        'me<- hide
        Me.Location -= New Point(5000, 500)
        Me.Hide()

        Settings.TrackBar2.Value -= 1
        Settings.TrackBar2.Value += 1



        Application.DoEvents()
        n += 1
        Progress(n)
        Label1.Text = "Starting rendering"
        Render.Go()
        Label1.Text = "Done"
        n += 1
        Progress(n)
    End Sub
    Dim tim As New System.Windows.Forms.Timer()
    Sub opacityDo()


        If RenderForm.Opacity > 0.79 Then
            Settings.TrackBar2.Value = GetSetting("Car Load", "settings", "Opacity", 80)
            tim.Stop()
        End If

        RenderForm.Opacity += 0.03
        Car_browser.Opacity += 0.03
        Settings.Opacity += 0.03
        Editor.Opacity += 0.03

    End Sub

 
    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub
End Class
