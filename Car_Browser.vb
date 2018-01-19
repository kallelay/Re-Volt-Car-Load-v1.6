Imports IrrlichtNETCP

Public Class Car_browser
    Dim LastLoc As Point
    Dim carList As New ListBox
    Public Sub setProgress(ByVal percent)

        Panel4.Width = (percent / 100) * Panel2.Width
    End Sub
    ' Drop Shadow around Form


    Private Sub Car_browser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      

        Me.CreateParams.ClassStyle += &H20000


        setProgress(0)
        Panel2.Hide()
        Dim carPath As String = RvPath & "\cars"

        Dim cars() = IO.Directory.GetDirectories(carPath)
        For i = LBound(cars) To UBound(cars)
            If IO.File.Exists(cars(i) & "\parameters.txt") Then
                Dim sing As New Singletons(cars(i) & "\parameters.txt")
                Dim name = Replace(sing.getSingleton("").getValue("Name"), vbTab, "")
                Do Until name(0) = Chr(34)
                    name = Mid(name, 2)
                Loop

                name = name.Replace(Chr(34), "")

                Dim Pname = Replace(cars(i).Split("\").Last, vbTab, "")

                ListBox2.Items.Add(" " & vbTab & name & "   (" & Pname & ")")
            End If
        Next

        For i = 0 To ListBox2.Items.Count - 1
            Application.DoEvents()
            startfromhere.Label1.Text = "Activating Search engine"

            carList.Items.Add(ListBox2.Items(i).ToString)
        Next i

    End Sub

    Sub ListBox2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox2.DoubleClick
        _t = 0
        Dialog1.Close()

        '  On Error Resume Next
        If ListBox2.SelectedIndex = -1 Then Exit Sub

        LoadOneCar()
        ' RenderForm.Focus()
        ' RenderForm.PictureBox1.Focus()
        '  RenderForm.PictureBox1.Focus()
        Car_Init = True
        ' _Render.nInit()

        Settings.RadioButton1_CheckedChanged(sender, e)
        Settings.RadioButton2_CheckedChanged(sender, e)
        Settings.RadioButton3_CheckedChanged(sender, e)
        Settings.RadioButton4_CheckedChanged(sender, e)
        Render.cam.Target = _car.Theory.Body.Offset
        TrackBar1_ValueChanged(sender, e)

        Settings.EnDis()


        Stats()
        If _car.Theory.RealInfos.TopSpeed < 200 Then
            Editor.TrackBar1.Maximum = Int(_car.Theory.RealInfos.TopSpeed)
        Else
            Editor.TrackBar1.Maximum = 200
        End If




    End Sub
    Enum classes
        Electric = 0
        Glow = 1
        Special = 2

    End Enum
    Enum rating

        Rookie = 0
        Amateur = 1
        Advanced = 2
        Semi_Pro = 3
        Pro = 4
    End Enum
    Enum Obtain
        Carnival_Only = -1
        Anytime = 0
        Championship = 1
        Timetrial = 2
        Practice = 3
        Winning_Single_Races = 4
        Training = 5

 

    End Enum
    Sub Stats()
        Label5.Text = ""
        If cBODY IsNot Nothing Then Label4.Text = Int(cBODY.VxCount / 3)
        For i = 0 To 3
            If _Wheel(i) IsNot Nothing Then
                Label5.Text &= Int(_Wheel(i).VxCount / 3) & ", "
            Else
                Label5.Text &= "-, "
            End If
            If i = 1 Then Label5.Text &= vbNewLine
        Next i

        Label5.Text = Label5.Text.Substring(0, Len(Label5.Text) - 2)


        Label7.Text = _car.Theory.MainInfos.SELECTABLE
        Label9.Text = _car.Theory.MainInfos.BESTTIME

        Label11.Text = DirectCast(_car.Theory.MainInfos.car_class, classes).ToString
        Label13.Text = Replace(DirectCast(_car.Theory.MainInfos.obtain, Obtain).ToString, "_", " ")
        Label15.Text = Replace(DirectCast(_car.Theory.MainInfos.Rating, rating).ToString, "_", "-")

        Label17.Text = _car.Theory.RealInfos.TopSpeed & " mph" & vbNewLine & Int(_car.Theory.RealInfos.TopSpeed * 1.6) & "km/h"



    End Sub
    Sub LoadOneCar()
        Application.DoEvents()
        Panel2.Show()
        setProgress(0)
        If Models.Count <> 0 Then
            For Each prma As Car_Model In Models
                Render.ScnMgr.AddToDeletionQueue(prma.ScnNode)
            Next
            Models.Clear()

        End If

        Application.DoEvents()
        setProgress(5)


        Dim mycar = Split(Split(ListBox2.SelectedItem, "(")(1), ")")(0)

        _car = New Car(RvPath & "\cars\" & mycar)

        _car.Load()


        If IO.Directory.GetFiles(RvPath & "\cars\" & mycar, "*read*me*").Length > 0 Then
            Label21.Visible = True
            Label22.Visible = True
            Dim rm = IO.File.ReadAllText(IO.Directory.GetFiles(RvPath & "\cars\" & mycar, "*read*me*")(0))
            Dim auth$ = ""
            If InStr(rm, "---", CompareMethod.Text) > 0 And InStr(rm, "citywalker", CompareMethod.Text) > 0 Then
                auth = "Citywalker"

            ElseIf InStr(rm, "author name", CompareMethod.Text) > 0 Then
                auth = Split(rm, "author name", -1, CompareMethod.Text)(1).Split(vbNewLine)(0)
            ElseIf InStr(rm, "author", CompareMethod.Text) > 0 Then
                auth = Split(rm, "author", -1, CompareMethod.Text)(1).Split(vbNewLine)(0)
            ElseIf InStr(rm, "created by", CompareMethod.Text) > 0 Then
                auth = Split(rm, "created by", -1, CompareMethod.Text)(1).Split(vbNewLine)(0)
            ElseIf InStr(rm, "creator", CompareMethod.Text) > 0 Then
                auth = Split(rm, "creator", -1, CompareMethod.Text)(1).Split(vbNewLine)(0)
            ElseIf InStr(rm, "by", CompareMethod.Text) > 0 Then
                auth = Split(rm, "by", -1, CompareMethod.Text)(1).Split(vbNewLine)(0)
            Else
                auth = "Unknown"
            End If
            'Debugger.Break()
            If InStr(_car.Theory.MainInfos.Name, "Halogaland", CompareMethod.Text) > 0 Then
                auth = "Halogaland"
            End If
          
            Do Until auth.Length = 0 Or (Convert.ToInt16(auth(0)) > 64 And Convert.ToInt16(CChar(auth(0))) < 123)
                auth = auth.Substring(1)
                Application.DoEvents()
                If auth.Length = 0 Then GoTo xFail
            Loop


            Do Until Convert.ToInt16(CChar(auth(auth.Length - 1))) > 64 And Convert.ToInt16(CChar(auth(auth.Length - 1))) < 123 Or Convert.ToInt16(CChar(auth(auth.Length - 1))) = 41

                auth = auth.Substring(0, auth.Length - 1)

                Application.DoEvents()

            Loop
xFail:

            Label22.Text = auth




        Else
            Label21.Visible = False
            Label22.Visible = False
        End If


        Application.DoEvents()
        setProgress(20)

        Dim ftex = (Replace(RvPath & "\" & _car.Theory.MainInfos.Tpage, ",", "."))
        IO.File.AppendAllText(Environ("temp") & "\carload.log", "ftex:" & ftex & vbNewLine)
        '  CARBMP.MakeTransparent(System.Drawing.Color.FromArgb(0, 0, 0))
        '  MkDir(Environ("temp") & "\carload\")
        '  Randomize()
        '
        '   Dim ftex = Environ("temp") & "\carload\" & _car.Theory.MainInfos.Tpage.Split("\").Last & "cartx" & Int(Rnd() * 50000) & ".png"
        '
        '    CARBMP.Save(ftex)
        '   CARBMP.Dispose()

        'Render.ScnMgr.Dispose(True)
        Application.DoEvents()
        setProgress(22)

        '  On Error Resume Next
        If (_car.Theory.Body.modelNumber) <> -1 Then
            If IO.File.Exists(Replace(Replace(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.Body.modelNumber), Chr(34), ""), ",", ".")) = True Then
                cBODY = New Car_Model(Replace(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.Body.modelNumber), Chr(34), ""))
                cBODY.Texture_ = ftex
                cBODY.Render()
                Try
                    cBODY.ScnNode.Position = _car.Theory.Body.Offset ' - _car.Theory.RealInfos.COM / 2
           
                Catch
                End Try

                Settings.TextBox1.AppendText(vbNewLine & "loaded body:" & cBODY.VxCount & " vertex")
                Settings.TextBox1.AppendText(vbNewLine & "texture:" & ftex)
                Settings.TextBox1.AppendText(vbNewLine)
            End If
        Else
            Settings.TextBox1.AppendText("~~Error: MODEL(" & _car.Theory.Body.modelNumber & ") doesn't exist" & vbNewLine)
            Debugger.Break()
        End If

        Application.DoEvents()
        setProgress(35)

        For i = 0 To 3
            If _car.Theory.wheel(i).modelNumber <> -1 Then
                If IO.File.Exists(Replace(RvPath & "\" & Replace(_car.Theory.MainInfos.Model(_car.Theory.wheel(i).modelNumber), Chr(34), ""), ",", ".")) = True Then
                    _Wheel(i) = New Car_Model(RvPath & "\" & Replace(_car.Theory.MainInfos.Model(_car.Theory.wheel(i).modelNumber), Chr(34), ""))
                    If _Wheel(i) IsNot Nothing Then
                        _Wheel(i).Texture_ = ftex
                        _Wheel(i).Render()
                        '  _Wheel(i).ScnNode.DebugObject = True
                        '  _Wheel(i).ScnNode.DebugDataVisible = DebugSceneType.BoundingBox
                        _Wheel(i).ScnNode.Position = _car.Theory.wheel(i).Offset(1) '+ _car.Theory.RealInfos  '+ _car.Theory.wheel(i).Offset(2) '- _car.Theory.Body.Offset



                        Settings.TextBox1.AppendText(vbNewLine & "loaded wheel " & i & " :" & _Wheel(i).VxCount & " vertex")
                        Settings.TextBox1.AppendText(vbNewLine & "texture:" & ftex)
                        Settings.TextBox1.AppendText(vbNewLine)

                    End If
                Else
                    Settings.TextBox1.AppendText("~~Error: MODEL(" & _car.Theory.wheel(i).modelNumber & ") doesn't exist" & vbNewLine)
                End If

            End If
        Next

        Application.DoEvents()
        setProgress(45)
        For i = 0 To 3
            If _car.Theory.Spring(i).modelNumber <> -1 Then

                _Spring(i) = New Car_Model(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.Spring(i).modelNumber).Replace(Chr(34), ""))
                _Spring(i).Texture_ = ftex
                _Spring(i).Render()

                '_Spring(i).ScnNode.Scale = _car.Theory.Spring(i).Length '(, 1)
                _Spring(i).ScnNode.Position = _car.Theory.Spring(i).Offset
                _Spring(i).ScnNode.Scale.SetLength(_car.Theory.Spring(i).Length)



                Settings.TextBox1.AppendText(vbNewLine & "loaded spring " & i & " :" & _Spring(i).VxCount & " vertex")
                Settings.TextBox1.AppendText(vbNewLine & "texture:" & ftex)
                Settings.TextBox1.AppendText(vbNewLine)


            End If


        Next

        Application.DoEvents()
        setProgress(55)

        For i = 0 To 3
            If _car.Theory.PIN(i).modelNumber <> -1 Then
                'If _Pin(i) IsNot Nothing Then
                _Pin(i) = New Car_Model(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.PIN(i).modelNumber).Replace(Chr(34), ""))
                _Pin(i).Texture_ = ftex
                _Pin(i).Render()
                _Pin(i).ScnNode.Position = _car.Theory.PIN(i).offSet

                _Pin(i).ScnNode.Scale *= _car.Theory.PIN(i).Length


                Settings.TextBox1.AppendText(vbNewLine & "loaded PIN " & i & " :" & _Pin(i).VxCount & " vertex point")
                Settings.TextBox1.AppendText(vbNewLine & "texture:" & ftex)
                Settings.TextBox1.AppendText(vbNewLine)

                'End If
            End If
        Next

        Application.DoEvents()
        setProgress(65)

        For i = 0 To 3
            If _car.Theory.Axle(i).modelNumber <> -1 Then


                _axle(i) = New Car_Model(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.Axle(i).modelNumber).Replace(Chr(34), ""))
                _axle(i).Texture_ = ftex

                _axle(i).Render()
                _axle(i).ScnNode.Position = _car.Theory.Axle(i).offSet

                Dim tcalc = GetLength(_car.Theory.Axle(i).offSet, _car.Theory.wheel(i).Offset(1))
                _axle(i).ScnNode.Scale = New Vector3D(1, 1, tcalc / (_car.Theory.Axle(i).Length * Render.ZoomFactor))   ' New Vector3D(1, _car.Theory.Axle(i).Length *, 1)
                '  Dim ax = AxleFit(_car.Theory.Axle(i).offSet, _car.Theory.wheel(i).Offset(1)) * 180
                '   _axle(i).ScnNode.Position = New Vector3D(0, 0, 0)
    
                '  _axle(i).ScnNode.Rotation -= (_car.Theory.wheel(i).Offset(1) - _car.Theory.Axle(i).offSet).Normalize

                Dim Mat = LookAt(_car.Theory.wheel(i).Offset(1), _car.Theory.Axle(i).offSet, New Vector3D(0, 0, -1))
                If i = 1 Or i = 0 Then _axle(i).ScnNode.Rotation = Mat.RotationDegrees + New Vector3D(0, 0, -180)
                If i = 2 Or i = 3 Then _axle(i).ScnNode.Rotation = Mat.RotationDegrees ' + New Vector3D(0, 0, 180)
                ' If i = 1 Or i = 0 Then _axle(i).ScnNode.Rotation = -_axle(i).ScnNode.Rotation + New Vector3D(0, 1, 0) * 180
                _axle(i).ScnNode.Position += Mat.Translation

                Debug.WriteLine(i & "->" & (_car.Theory.wheel(i).Offset(1) - _car.Theory.Axle(i).offSet).Normalize.X & "," & (_car.Theory.wheel(i).Offset(1) - _car.Theory.Axle(i).offSet).Normalize.Y & "," & (_car.Theory.wheel(i).Offset(1) - _car.Theory.Axle(i).offSet).Normalize.Z)

                '   _axle(i).ScnNode.Rotation -= New Vector3D(0, ax, 0)
                '   _axle(i).ScnNode.Position += (ax / Math.Abs(ax)) * New Vector3D((_car.Theory.Axle(i).offSet.X - _car.Theory.wheel(i).Offset(1).X) / 2, 0, 0)

                '_axle(i).ScnNode.Rotation.RotateXZBy(AxleFit(_car.Theory.Axle(i).offSet, _car.Theory.wheel(i).Offset(1)) * 180, _car.Theory.Axle(i).offSet)




                '          _4rot = setRotate(_car.Theory.Axle(i).offSet, AxleFit(_car.Theory.Axle(i).offSet, _car.Theory.wheel(i).Offset(1)))

                '            _axle(i).ScnNode.Rotation += _4rot.Rotation
                ' _axle(i).ScnNode.Position += _4rot.Position
                '_axle(i).ScnNode.Rotation '+= New Vector3D(0, 180, 0)




                Settings.TextBox1.AppendText(vbNewLine & "loaded Axle " & i & " :" & _axle(i).VxCount & " vertex point")
                Settings.TextBox1.AppendText(vbNewLine & "texture:" & ftex)
                Settings.TextBox1.AppendText(vbNewLine)


            End If
        Next

        Application.DoEvents()
        setProgress(75)

        If _car.Theory.Spinner.modelNumber <> -1 Then
            _Spinner = New Car_Model(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.Spinner.modelNumber).Replace(Chr(34), ""))

            _Spinner.Texture_ = ftex
            _Spinner.Render()
            '  MsgBox(_Spinner.PolysReadingProgress)
            _Spinner.ScnNode.Position = _car.Theory.Spinner.offSet

            ' _car.Theory.Spinner.Axis()
            '_Spinner.ScnNode.Scale = _car.Theory.Spinner.Axis 

            Settings.TextBox1.AppendText(vbNewLine & "loaded spinner:" & _Spinner.VxCount & " vertex")
            Settings.TextBox1.AppendText(vbNewLine & "texture:" & ftex)
            Settings.TextBox1.AppendText(vbNewLine)


        End If

        Application.DoEvents()
        setProgress(85)

        If _car.Theory.Aerial.ModelNumber <> -1 Then
            If IO.File.Exists(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.Aerial.ModelNumber).Replace(Chr(34), "")) = False Then
                Settings.TextBox1.AppendText("~~Error: MODEL(" & _car.Theory.Aerial.ModelNumber & ") doesn't exist" & vbNewLine)
            End If
            _Aerial = New Car_Model(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.Aerial.ModelNumber).Replace(Chr(34), ""))
            If _Aerial IsNot Nothing Then

                _Aerial.Texture_ = RvPath & "\gfx\fxpage1.bmp"
                _Aerial.Render()
                _Aerial.ScnNode.Scale = New Vector3D(1, _car.Theory.Aerial.length * 3, 1)
                _Aerial.ScnNode.Position = _car.Theory.Aerial.offset
                _Aerial.ScnNode.Scale += _car.Theory.Aerial.Direction
                _Aerial.ScnNode.Scale.SetLength(_car.Theory.Aerial.length)

                Settings.TextBox1.AppendText(vbNewLine & "loaded Aerial:" & _Aerial.VxCount & " vertex")
                Settings.TextBox1.AppendText(vbNewLine & "texture:" & RvPath & "\gfx\fxpage1.bmp")
                Settings.TextBox1.AppendText(vbNewLine)
            End If
        End If

        Application.DoEvents()
        setProgress(95)

        If _car.Theory.Aerial.TopModelNumber <> -1 Then
            Dim aerialtop As New Car_Model(RvPath & "\" & _car.Theory.MainInfos.Model(_car.Theory.Aerial.TopModelNumber).Replace(Chr(34), ""))
            If aerialtop IsNot Nothing Then
                aerialtop.Texture_ = RvPath & "\gfx\fxpage1.bmp"
                aerialtop.Render()
                '   aerialtop.ScnNode.Scale *= 5
                ' aerialtop.ScnNode.Position *= _car.Theory.Aerial.Direction.Y
                aerialtop.ScnNode.Position += _car.Theory.Aerial.offset  '+ _Aerial.ScnNode.Scale
                aerialtop.ScnNode.Position += _car.Theory.Aerial.Direction
                '  aerialtop.ScnNode.Position += _car.Theory.Aerial.offset '+  ' ) '+' Aerial.ScnNode.BoundingBox.MaxEdge

                Settings.TextBox1.AppendText(vbNewLine & "loaded Aerial Top:" & aerialtop.VxCount & " vertex")
                Settings.TextBox1.AppendText(vbNewLine & "texture:" & RvPath & "\gfx\fxpage1.bmp")
                Settings.TextBox1.AppendText(vbNewLine)

            End If
        End If
        For Each PRMmodel As Car_Model In Models

            If PRMmodel.ScnNode IsNot Nothing And _Wheel(1) IsNot Nothing Then
                ' Debug.WriteLine("<-" & _Wheel(1).ScnNode.BoundingBox.MinEdge.Y * Render.ZoomFactor)

                If _Wheel(1).VxCount > 0 Then PRMmodel.ScnNode.Position += New Vector3D(0, -_Wheel(1).ScnNode.BoundingBox.MinEdge.Y * Render.ZoomFactor + 0.2, 0)
            End If



        Next
        Settings.EnDis()
        Application.DoEvents()
        setProgress(100)
        Panel2.Hide()
    End Sub

    Private Sub ListBox2_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles ListBox2.DrawItem
        'CODE FOUND FROM THE INTERNET
        If e.State And DrawItemState.Selected Then

            Dim TextLength As Single = TextRenderer.MeasureText(ListBox2.SelectedItem, ListBox2.Font).Width
            '  If wrong Then
            e.Graphics.FillRectangle(Brushes.LightSkyBlue, New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
            'Else
            '    e.Graphics.FillRectangle(If(e.Index Mod 2 = 1, Brushes.AliceBlue, Brushes.White), New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
            'End If
        Else
            'TODO failed

            e.Graphics.FillRectangle(If(e.Index Mod 2 = 1, Brushes.AliceBlue, Brushes.White), e.Bounds)
        End If
        'If InStr(Invalid, ListBox2.Items(e.Index).ToString.Split(Chr(9)).First) > 0 Then
        'e.Graphics.DrawString(ListBox2.Items(e.Index).ToString, ListBox2.Font, Brushes.DarkRed, e.Bounds)
        ' Else
        e.Graphics.DrawString(ListBox2.Items(e.Index).ToString, ListBox2.Font, Brushes.Black, e.Bounds)
        'End If

    End Sub
    Private Sub Label1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Location += MousePosition - LastLoc
            LastLoc = MousePosition
        Else
            LastLoc = MousePosition
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        'Settings.Button5_Click(sender, e)
        ListBox2_DoubleClick(sender, e)
        '  
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Shell("explorer.exe http://z3.invisionfree.com/Revolt_Live/", AppWinStyle.NormalFocus)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Settings.Timer1.Interval = NumericUpDown1.Value * 1000
            Settings.Timer1.Start()
            NumericUpDown1.Maximum = ListBox2.Items.Count - 1
        ElseIf CheckBox1.Checked = False Then
            Settings.Timer1.Stop()

        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        Settings.Timer1.Interval = NumericUpDown1.Value * 1000
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Application.DoEvents()
        If Me.Width = 587 Then
            Do Until Panel3.Left <= 12
                Application.DoEvents()
                Panel3.Left -= 2
                ' Button2.Left -= 2
                Me.Width -= 2
            Loop
        Else
            Do Until Me.Width = 587
                Application.DoEvents()
                Panel3.Left += 2
                ' Button2.Left += 2
                Me.Width += 2
            Loop
        End If
    End Sub


    Private Sub TrackBar1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar1.ValueChanged
        If _Wheel(0) Is Nothing Or _Wheel(1) Is Nothing Then Exit Sub
        If TrackBar1.Value = 0 Then Exit Sub
        _Wheel(0).ScnNode.Rotation = New IrrlichtNETCP.Vector3D(0, -TrackBar1.Value, 0)
        '  _Wheel(1).ScnNode.Render()
        _Wheel(1).ScnNode.Rotation = New IrrlichtNETCP.Vector3D(0, -TrackBar1.Value, 0)


    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll

    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.Checked = False Then
            Editor.Timer1.Stop()
        Else
            Editor.LastCampos = Render.cam.Position
            Editor.Timer1.Start()


        End If

    End Sub

    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label17_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.GotFocus
        If TextBox1.Text = "Search" Then TextBox1.Text = ""

        TextBox1.ForeColor = Drawing.Color.Black
        '    TextBox1.Font = New Drawing.Font(Drawing.FontFamily.GenericSansSerif, 8.25, FontStyle.Regular, GraphicsUnit.Point)
        Do Until TextBox1.Width >= 160
            TextBox1.Width += 1
            TextBox1.Left -= 1
            NumericUpDown1.Top += 1
            CheckBox1.Top += 1
            Application.DoEvents()
        Loop

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Escape Then
            ListBox2.Focus()
        End If

    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    End Sub

    Private Sub TextBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.LostFocus
        If TextBox1.Text = "" Then

            TextBox1.Text = "Search"
            ListBox2.Items.AddRange(carList.Items())
            '   TextBox1.Font = New Drawing.Font(Drawing.FontFamily.GenericSansSerif, 8.25, FontStyle.Italic, GraphicsUnit.Point)

            TextBox1.ForeColor = Drawing.Color.Gray
            Do Until TextBox1.Width <= 90
                TextBox1.Width -= 1
                TextBox1.Left += 1
                Application.DoEvents()
                NumericUpDown1.Top -= 1
                CheckBox1.Top -= 1
            Loop

        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "Search" Then
            ListBox2.Items.AddRange(carList.Items())
        End If
        If Not Initialized Then Exit Sub
        ListBox2.Items.Clear()
        For i = 0 To carList.Items.Count - 1
            If InStr(carList.Items(i), TextBox1.Text, CompareMethod.Text) Then
                ListBox2.Items.Add(carList.Items(i))
                Application.DoEvents()
            End If
        Next
    End Sub

    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Car_browser_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim g As Drawing.Graphics = Me.CreateGraphics
        g.DrawArc(Pens.Black, 0, 0, 18, 18, 180, 90)


    End Sub
End Class