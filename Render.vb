Imports IrrlichtNETCP

Public Class Render

    Public Shared _cc As New Color(255, 128, 192, 255)
    '  Public Shared _cc As New Color(255, 0, 0, 0)
    Public Shared ZoomFactor = 1 / 10

    Public Shared Device As IrrlichtDevice
    Public Shared VideoDriver As VideoDriver
    Public Shared ScnMgr As SceneManager
    Public Shared guiEnv As GUIEnvironment
    Public Shared mEvent As IrrlichtNETCP.Event

    Public Shared cam As CameraSceneNode
    Public Shared strFont As GUIFont

    Public Shared Width = 1024, Height = 768
    Public Shared DvType As DriverType = DriverType.OpenGL
    Public Shared bits = 32
    Public Shared FullScreen As Boolean = False
    Public Shared Vsync As Boolean = True
    Public Shared Stencil As Boolean = False
    Public Shared AntiAlias As Boolean = True

    Public Shared Sub Init()

        My.Resources.StrFont.Save(Environ("temp") & "\strfont.png", Imaging.ImageFormat.Png)


        'ok, what about init?
        Device = New IrrlichtDevice(DvType, New Dimension2D(Width, Height), bits, FullScreen, Stencil, Vsync, AntiAlias, RenderForm.PictureBox1.Handle)
        VideoDriver = Device.VideoDriver
        ScnMgr = Device.SceneManager
        Device.CursorControl.Visible = True
        AddHandler Device.OnEvent, AddressOf onEvent
        guiEnv = Device.GUIEnvironment
        Device.WindowCaption = "nVolt"
        InitCamera()
        strFont = Device.GUIEnvironment.GetFont(Environ("temp") & "\strfont.png")

    End Sub
    Public Shared Sub InitFullScreen()

        My.Resources.StrFont.Save(Environ("temp") & "\strfont.png", Imaging.ImageFormat.Png)


        'ok, what about init?
        Device = New IrrlichtDevice(DvType, New Dimension2D(Width, Height), bits, FullScreen, Stencil, Vsync, AntiAlias)
        VideoDriver = Device.VideoDriver
        ScnMgr = Device.SceneManager
        Device.CursorControl.Visible = True
        AddHandler Device.OnEvent, AddressOf onEvent
        guiEnv = Device.GUIEnvironment
        Device.WindowCaption = "nVolt"
        InitCamera()
        strFont = Device.GUIEnvironment.GetFont(Environ("temp") & "\strfont.png")

    End Sub
    Public Shared alpha As Double = 0
    Public Shared cte = 17
    Public Shared firsttime = 1
    Public Shared lastpos
    Public Shared pass
    Public Shared lastHandledPos As Position2D
    Public Shared Activated As Boolean = False
    Public Shared Function onEvent(ByVal Ev As IrrlichtNETCP.Event) As Boolean
        If firsttime = 1 Then lastpos = Ev.MousePosition
        firsttime += 1


   
        If startfromhere.ScreenSaverMode Then
            If Ev.Type = EventType.KeyInputEvent Or Ev.Type = EventType.MouseInputEvent Then
                If lastpos <> Ev.MousePosition Then
                    pass += 1
                End If
                If Ev.Type = EventType.KeyInputEvent And Ev.KeyCode > 9 Then pass = 5
            End If
        End If
        lastpos = Ev.MousePosition

        If pass = 5 Then
            Dim pro As New Process
            pro = Process.GetCurrentProcess()
            pro.Kill()

        End If

    End Function
    Public Shared terr As TerrainSceneNode
    Public Shared Sub Go()
        'For Start rendering & should be the last thing
        'Try
        'Device.Run()

        '  Try
        Try
            Do Until Device.Run = False
                RenderForm.Label1.Text = "Render Window"     'fps?
                VideoDriver.BeginScene(True, True, _cc)    'clear buffer
                'VideoDriver.BeginScene(True, True, Color.Black)    'clear buffer

                _t += 60.0F
                If _car.Theory.Spinner IsNot Nothing Then
                    If _Spinner IsNot Nothing And _car.Theory.Spinner.angVel <> 0 Then
                        _Spinner.ScnNode.Rotation = New IrrlichtNETCP.Vector3D(_car.Theory.Spinner.angVel * _car.Theory.Spinner.Axis.X * _t, _car.Theory.Spinner.angVel * _car.Theory.Spinner.Axis.Y * _t, _car.Theory.Spinner.angVel * _car.Theory.Spinner.Axis.Z * _t)
                        ' End If

                    End If
                End If


                If startfromhere.ScreenSaverMode Then
                    VideoDriver.Draw2DRectangle(New Rect(-1, -1, Width, Height), _cc, _cc, Color.White, Color.White)

                Else
                    If Settings.CheckBox14.Checked Then VideoDriver.Draw2DRectangle(New Rect(-1, -1, RenderForm.Width + 2, RenderForm.Height + 2), _cc, _cc, Color.White, Color.White)

                End If


                If _car.Theory IsNot Nothing Then _
      If Settings.CheckBox13.Checked Then Car_Load.Render.strFont.Draw(_car.Theory.MainInfos.Name, New Position2D(0, 0), Color.White, False, False)

                Dim logopos = New Position2D(RenderForm.Width - 200, 0)

                If startfromhere.ScreenSaverMode Then
                    logopos = New Position2D(Width - 200, 0)
                End If
                If Settings.CheckBox9.Checked Then VideoDriver.Draw2DImage(VideoDriver.GetTexture(Environ("temp") & "\rvl.png"), logopos, New Rect(0, 0, 200, 50), Color.White, True)
                '              WatScn.Update()

                ScnMgr.ActiveCamera = cam

                ScnMgr.DrawAll()
                guiEnv.DrawAll()

                ' NP.UpdateAbsolutePosition()

                'draw everything
                VideoDriver.EndScene()

            Loop
        Catch ex As Exception

            Application.DoEvents()
            Settings.TextBox1.AppendText("ERROR: Internal problem has occured")

            Application.DoEvents()
            ' ' Console.Beep(900, 100)
            ' Console.Beep(800, 200)
        
            Render.Go()
        End Try

    End Sub

    Public Shared Sub InitCamera()
        cam = ScnMgr.AddCameraSceneNodeMaya(ScnMgr.RootSceneNode, 50, 100, 0, -1)
        'cam = ScnMgr.AddCameraSceneNodeFPS(ScnMgr.RootSceneNode, 50, 100, False)
        ' cam = ScnMgr.AddCameraSceneNode(ScnMgr.RootSceneNode)
        ' cam.Position = New Vector3D(-10, -10, -10)
        cam.Position = New Vector3D(-5, 5, 10)
        cam.Target = New Vector3D(0, 0, 0)
        '  cam.Position = New Vector3D(cam.Target.X + -Math.Cos(alpha) * cte, cam.Position.Y, cam.Target.Z + Math.Sin(alpha) * cte)

        ' If startfromhere.ScreenSaverMode Then cam.Position += New Vector3D(5, 5, 5)
        cam.AutomaticCulling = CullingType.Off
        VideoDriver.SetTransform(TransformationState.View, cam.AbsoluteTransformation)
        cam.NearValue = 0.01
        cam.FarValue = 32768
        cam.UpVector = New Vector3D(0, 1, 0)


    End Sub

          


End Class
