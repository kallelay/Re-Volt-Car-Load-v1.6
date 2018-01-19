Imports SoundsSharpNameSpace
Public Class About
    Dim Snd As SoundsSharp.Music
    Dim SndMgr As New SoundsSharpNameSpace.SoundsSharp.SoundManager()

    Dim CenterPoint As New Point(Me.Width / 2, 120) ' Me.Height / 2)
    Dim cur = -1
    Public Class Credits
        Public Header As String
        Public Label As String

        Public Sub Add(ByVal H, ByVal L)
            Header = H
            Label = L
        End Sub
        Public Sub _Update()
            About._Big.Text = Header
            About._small.Text = Label
        End Sub
    End Class
    Dim Credit(7) As Credits

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadAbout()
        Change()
        _Big.Top = Me.Height
      
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        _Big.Top -= 2
        _small.Top -= 2
        If Math.Abs(CenterPoint.Y - _Big.Top) = 0 Then
            Timer2.Start()
            Timer1.Stop()
        End If

        If _small.Top <= -_small.Height Then
            Change()
            _Big.Top = Me.Height
            _small.Top = _Big.Top + _Big.Height
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        _Big.Top -= 2
        _small.Top -= 2
        Timer2.Stop()
        Timer1.Start()
    End Sub

    Sub Change()
        cur += 1
        If cur > 6 Then cur = 0
        Credit(cur)._Update()
    End Sub

    Sub LoadAbout()
        For i = 0 To 6
            Credit(i) = New Credits
        Next
        Credit(0).Add("Project by", "Re-Volt Live Forum")
        Credit(1).Add("Main programmer", "KDL 'kfalcon' [Kallel A.Y]")
        Credit(2).Add("Ideas makers and Helpers", "Zipperrulez" & vbNewLine & "Burner94")
        Credit(3).Add("Graphics", "KDL 'Kfalcon'")
        Credit(4).Add("Icons", "Mix of KDE Oxygen and Crystal [xp]")
        Credit(5).Add("Music", "Koori No Ue Tatsu Yo Ni [instru.]")
        Credit(6).Add("Copyrights", "All rights reserved at RVL forum and Kallel© 2010" & vbNewLine & "Licensed under GNU GPL")

    End Sub

    Private Sub About_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Me.Close()
    End Sub

    Private Sub _small_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub _Big_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Big.Click
        Me.Close()
    End Sub
End Class
