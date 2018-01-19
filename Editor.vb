Public Class Editor
    Dim mpos As Point


    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += MousePosition - mpos
            mpos = MousePosition
        Else
            mpos = MousePosition
        End If
    End Sub
    Public LastCampos As IrrlichtNETCP.Vector3D
 
    Dim X_ As Single = 0
    Dim Y_ As Single = 0

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Render.alpha += 0.01
        Dim alpha = Render.alpha
        Render.cam.Position = New IrrlichtNETCP.Vector3D(Render.cam.Target.X + -Math.Cos(alpha) * Render.cte, Render.cam.Position.Y, Render.cam.Target.Z + Math.Sin(alpha) * Render.cte)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Car_browser.ListBox2_DoubleClick(sender, e)
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = False Then
            Timer2.Stop()
        Else
            CheckBox3.Checked = False
            Timer2.Start()


        End If

    End Sub


    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Not Initialized Then Exit Sub
        If _car.Theory.wheel(0) Is Nothing Then Exit Sub

        For i = 0 To 3
            If _car.Theory.wheel(i).modelNumber <> -1 And _car.Theory.wheel(i).IsPowered = True Then
                _Wheel(i).ScnNode.Rotation += New IrrlichtNETCP.Vector3D(TrackBar1.Value, 0, 0)
            End If
        Next i
        '    Render.cam.Position = New IrrlichtNETCP.Vector3D(X_, 7.5, Y_)

    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = False Then
            Timer3.Stop()
        Else
            CheckBox2.Checked = False
            Timer3.Start()


        End If

    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        If _car.Theory.wheel(0) Is Nothing Then Exit Sub

        For i = 0 To 3
            If _car.Theory.wheel(i).modelNumber <> -1 And _car.Theory.wheel(i).IsTurnable = True Then
                _Wheel(i).ScnNode.Rotation += New IrrlichtNETCP.Vector3D(TrackBar1.Value, 0, 0)
            End If
        Next i
        '    Render.cam.Position = New IrrlichtNETCP.Vector3D(X_, 7.5, Y_)

    End Sub

    Private Sub Editor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Size = New Size(470, 325)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            Timer4.Stop()
        Else
            Timer4.Start()


        End If
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        If _car.Theory.wheel(0) Is Nothing Then Exit Sub

        For i = 0 To 3
            If _car.Theory.wheel(i).modelNumber <> -1 Then
                _Wheel(i).ScnNode.Rotation += New IrrlichtNETCP.Vector3D(TrackBar1.Value, 0, 0)
            End If
        Next i
        '    Render.cam.Position = New IrrlichtNETCP.Vector3D(X_, 7.5, Y_)

    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Readme_generator.ShowDialog()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If _car.Path = "" Then Exit Sub
        Dialog1.Show() 'Dialog1 is parameters editor
      
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If _car.Path = "" Then Exit Sub
        Shell("explorer.exe " & Chr(34) & Replace(_car.Path, "\\", "\") & Chr(34), AppWinStyle.NormalFocus)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If _car.Theory.MainInfos.Tpage = "" Then Exit Sub
        Dim X As New Form
        X.Size = New Size(256, 256)
        X.StartPosition = FormStartPosition.CenterScreen

        X.BackgroundImage = Image.FromFile(RvPath & "\" & Replace(Replace(Replace(_car.Theory.MainInfos.Tpage, "'", ""), ",", "."), Chr(9), ""))
        X.BackgroundImageLayout = ImageLayout.Stretch
        X.Text = "Preview Bitmap"
        X.TopMost = True

        X.Show()



    End Sub
End Class