Imports System.Runtime.InteropServices
Imports IrrlichtNETCP

Module Upperclass

    Public _t As Double = 0
    Public _Render As New Render
    Public _car As New Car("")
    Public RvPath As String
    Public Initialized As Boolean = False
    Public Car_Init = False


    Public cBODY As Car_Model
    Public _Wheel(4) As Car_Model
    Public _Spring(4) As Car_Model
    Public _Pin(4) As Car_Model
    Public _axle(4) As Car_Model
    Public _Spinner As Car_Model
    Public _Aerial As Car_Model
    Public Sub DoWrite(ByVal str As String)
#If DEBUG Then
        IO.File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\car_load.log", str)
#End If

    End Sub
    Sub CarTheory()

        _car.Theory = New Car_theory


        _car.Theory.MainInfos.Name = vbTab & vbTab & " Car::Load v1.6"
    End Sub


    Public Function AxleFit(ByVal Axle As IrrlichtNETCP.Vector3D, ByVal Wheel As IrrlichtNETCP.Vector3D) As Double
        Dim Alpha As Double
        Alpha = Math.Asin((Wheel.Z - Axle.Z) / (Math.Sqrt((Wheel.X - Axle.X) ^ 2 + (Wheel.Z - Axle.Z) ^ 2)))
        Return Alpha
    End Function
    Public Function GetLength(ByVal Axle As IrrlichtNETCP.Vector3D, ByVal Wheel As IrrlichtNETCP.Vector3D) As Double
        Return (Math.Sqrt((Wheel.X - Axle.X) ^ 2 + (Wheel.Z - Axle.Z) ^ 2))
    End Function
    Public Function setRotate(ByVal origin As IrrlichtNETCP.Vector3D, ByVal Angle As IrrlichtNETCP.Vector3D) As IrrlichtNETCP.SceneNode
        Dim node As New IrrlichtNETCP.SceneNode
        Dim pos = node.AbsolutePosition - origin
        Dim newrot = node.Rotation + Angle
        Dim distance = pos.Length
        Dim newpos As New IrrlichtNETCP.Vector3D(0, 0, distance)

        Dim m As New IrrlichtNETCP.Matrix4
        m.RotationDegrees = newrot
        m.RotateVect(newpos)
        newpos += origin
        node.Rotation = newrot

        Return node

    End Function
    Function LookAt(ByVal vPos As Vector3D, ByVal vLookAt As Vector3D, ByVal vWorldUp As Vector3D) As Matrix4
        Dim vRight As Vector3D = New Vector3D(0, 0, 0)
        Dim vUp As Vector3D = New Vector3D(0, 0, 0)
        Dim resMat(16) As Single
        Dim vDir = vPos - vLookAt
        Dim Mat As New Matrix4

        vDir.Normalize()
        vRight = vWorldUp.CrossProduct(vDir)

        resMat(0) = vRight.X
        resMat(1) = vRight.Z
        resMat(2) = vRight.Y
        resMat(3) = 0
        resMat(4) = vUp.X
        resMat(5) = vUp.Z
        resMat(6) = vUp.Y
        resMat(7) = 0
        resMat(8) = vDir.X
        resMat(9) = vDir.Z
        resMat(10) = vDir.Y
        resMat(11) = 0
        resMat(12) = 0
        resMat(13) = 0
        resMat(14) = 0
        resMat(15) = 1

        Mat = Matrix4.FromUnmanaged(resMat)
        Return Mat
    End Function
    Function VectorPlusScalarVec(ByVal vleft As Vector3D, ByVal scalar As Single, ByVal vright As Vector3D) As Vector3D
        Return vleft + scalar * vright
    End Function
#Region "Aero"

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MARGINS
        Public cxLeftWidth As Integer
        Public cxRightWidth As Integer
        Public cyTopHeight As Integer
        Public cyButtomheight As Integer
    End Structure

    <DllImport("dwmapi.dll")> _
    Public Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarinset As MARGINS) As Integer
    End Function

    Public Sub Aero(ByVal Form_ As Form, ByVal left As Integer, ByVal right As Integer, ByVal top As Integer, ByVal bottom As Integer)
        On Error Resume Next
        Dim margins As MARGINS = New MARGINS
        margins.cxLeftWidth = left
        margins.cxRightWidth = right
        margins.cyTopHeight = top
        margins.cyButtomheight = bottom
        'set all the four value -1 to apply glass effect to the whole window
        'set your own value to make specific part of the window glassy.
        Dim hwnd As IntPtr = Form_.Handle
        DwmExtendFrameIntoClientArea(hwnd, margins)
    End Sub
    Public Sub Aero(ByVal Form_ As Form)
        Aero(Form_, -1, -1, -1, -1)

    End Sub
    Private Declare Function RemoveMenu Lib "user32" (ByVal hMenu As IntPtr, ByVal nPosition As Integer, ByVal wFlags As Long) As IntPtr
    Private Declare Function GetSystemMenu Lib "user32" (ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As IntPtr
    Private Declare Function GetMenuItemCount Lib "user32" (ByVal hMenu As IntPtr) As Integer
    Private Declare Function DrawMenuBar Lib "user32" (ByVal hwnd As IntPtr) As Boolean
    Private Const MF_BYPOSITION = &H400
    Private Const MF_REMOVE = &H1000
    Private Const MF_DISABLED = &H2

    Public Sub DisableCloseButton(ByVal hwnd As IntPtr)
        Dim hMenu As IntPtr
        Dim menuItemCount As Integer
        hMenu = GetSystemMenu(hwnd, False)
        menuItemCount = GetMenuItemCount(hMenu)
        Call RemoveMenu(hMenu, menuItemCount - 1, _
        MF_DISABLED Or MF_BYPOSITION)
        Call RemoveMenu(hMenu, menuItemCount - 2, _
        MF_DISABLED Or MF_BYPOSITION)
        Call DrawMenuBar(hwnd)
    End Sub
#End Region
End Module