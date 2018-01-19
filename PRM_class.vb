Imports IrrlichtNETCP
Imports System.IO
Imports Car_Load.Render
Imports Car_Load
Public Class Car_Model

    Public Directory As String
    Public FileName As String
    Public DirectoryName As String
    Public MyModel As New MODEL
    Public PolysReadingProgress, VertexReadingProgress As Double
    Public ScnNode As New SceneNode()
    Public Texture_ As String = ""
    Public VxCount As Single
    Public isMirror As Boolean = False
    Sub New(ByVal filepath As String)

        filepath = Replace(filepath, ",", ".")

        If IO.File.Exists(filepath) = False Then
            Console.Beep(500, 100)
            Settings.TextBox1.AppendText("error! failed at loading model:" & filepath)
            Exit Sub
        End If

        Dim old&

      

        Dim J As New BinaryReader(New FileStream(Replace(filepath, ",", "."), FileMode.Open))
        If J Is Nothing Then Exit Sub


        'Vert/Poly count
        MyModel.polynum = J.ReadInt16()
        MyModel.vertnum = J.ReadInt16()



        ReDim MyModel.polyl(MyModel.polynum)
        For i = 0 To MyModel.polynum - 1

            If old <> Int(100 * i / (MyModel.polynum)) Then
                PolysReadingProgress = Int(100 * i / (MyModel.polynum))
            End If

            old = Int(100 * i / (MyModel.polynum))


            '

            MyModel.polyl(i).type = J.ReadInt16
            MyModel.polyl(i).Tpage = J.ReadInt16


            MyModel.polyl(i).vi0 = J.ReadInt16
            MyModel.polyl(i).vi1 = J.ReadInt16
            MyModel.polyl(i).vi2 = J.ReadInt16
            MyModel.polyl(i).vi3 = J.ReadInt16



            MyModel.polyl(i).c0 = J.ReadInt32
            MyModel.polyl(i).c1 = J.ReadInt32
            MyModel.polyl(i).c2 = J.ReadInt32
            MyModel.polyl(i).c3 = J.ReadInt32

            MyModel.polyl(i).u0 = J.ReadSingle
            MyModel.polyl(i).v0 = J.ReadSingle
            MyModel.polyl(i).u1 = J.ReadSingle
            MyModel.polyl(i).v1 = J.ReadSingle
            MyModel.polyl(i).u2 = J.ReadSingle
            MyModel.polyl(i).v2 = J.ReadSingle
            MyModel.polyl(i).u3 = J.ReadSingle
            MyModel.polyl(i).v3 = J.ReadSingle
        Next

        ReDim MyModel.vexl(MyModel.vertnum)

        For a = 0 To MyModel.vertnum - 1
            If old <> Int(a * 100 / (MyModel.vertnum)) Then
                VertexReadingProgress = Int(a * 100 / (MyModel.vertnum))
            End If

            old = Int(a * 100 / (MyModel.vertnum))
            Dim x, y, z As Single

            x = J.ReadSingle * ZoomFactor
            y = J.ReadSingle * -1 * ZoomFactor
            z = J.ReadSingle * ZoomFactor


            MyModel.vexl(a).Position = New Vector3D(x, y, z)

            x = J.ReadSingle * ZoomFactor
            y = J.ReadSingle * ZoomFactor * -1
            z = J.ReadSingle * ZoomFactor
            MyModel.vexl(a).normal = New Vector3D(x, y, z)


        Next

        J.Close()
        'let's set Directory and also Filename

        Me.FileName = filepath.Split("\").Last
        Me.Directory = Replace(filepath, Me.FileName, "", , , CompareMethod.Text)
        Me.DirectoryName = filepath.Split("\")(UBound(filepath.Split("\")) - 1)

        Models.Add(Me)


    End Sub
    Sub Render()



        If Me.MyModel.polynum = 0 Then Exit Sub


        Dim quads = 0
        Dim Vx As New MeshBuffer(VertexType.Standard)
        Dim vx3d As New Vertex3D()
        Dim polys() = MyModel.polyl 'clone polys (less code will be used)
        Dim vexs() = MyModel.vexl   'clone vertex(s) ( same reason)
        Dim j As Long
        VxCount = 0

        'ok, new vx list...
        '   Dim newVxList() As Vertex3D
        '  Dim maxNewVx As Long

        If IO.File.Exists(Replace(Texture_, ",", ".")) Then
            Dim texa As Bitmap = Bitmap.FromFile(Replace(Texture_, ",", "."))
            texa.MakeTransparent(System.Drawing.Color.Black)
            Dim _tex = Environ("temp") & "\carload\" & Rnd() * 5000 & ".png"
            texa.Save(_tex, Imaging.ImageFormat.Png)
            Dim tex = Car_Load.Render.VideoDriver.GetTexture(_tex)
            Vx.Material.Texture1 = tex 'VideoDriver.GetTexture(Directory & DirectoryName & Chr(65 + k) & ".bmp")
            Car_Load.Render.VideoDriver.SetMaterial(Vx.Material)
        End If


        j = -1



        For i = 0 To MyModel.polynum
            'Vx.Material = Directory & DirectoryName & 

            Vx.SetVertex(System.Threading.Interlocked.Increment(j), New Vertex3D(vexs(polys(i).vi0).Position, _
                                                    vexs(polys(i).vi0).normal, _
                                                    ColorsToRGB(polys(i).c0), _
                                                    New Vector2D(polys(i).u0, polys(i).v0)))



            Vx.SetVertex(System.Threading.Interlocked.Increment(j), New Vertex3D(vexs(polys(i).vi2).Position, _
                                                   vexs(polys(i).vi2).normal, _
                                                   ColorsToRGB(polys(i).c2), _
                                                   New Vector2D(polys(i).u2, polys(i).v2)))

            Vx.SetVertex(System.Threading.Interlocked.Increment(j), New Vertex3D(vexs(polys(i).vi1).Position, _
                                                                    vexs(polys(i).vi1).normal, _
                                                                    ColorsToRGB(polys(i).c1), _
                                                                    New Vector2D(polys(i).u1, polys(i).v1)))
            VxCount += 3


            If polys(i).type Mod 2 = 1 Then
                'it's a quad!!! hey don't panic, I'll split it!

                Vx.SetVertex(System.Threading.Interlocked.Increment(j), New Vertex3D(vexs(polys(i).vi2).Position, _
                                                vexs(polys(i).vi2).normal, _
                                                ColorsToRGB(polys(i).c2), _
                                                New Vector2D(polys(i).u2, polys(i).v2)))


                Vx.SetVertex(System.Threading.Interlocked.Increment(j), New Vertex3D(vexs(polys(i).vi0).Position, _
                                                 vexs(polys(i).vi0).normal, _
                                                 ColorsToRGB(polys(i).c0), _
                                                 New Vector2D(polys(i).u0, polys(i).v0)))



                Vx.SetVertex(System.Threading.Interlocked.Increment(j), New Vertex3D(vexs(polys(i).vi3).Position, _
                                   vexs(polys(i).vi3).normal, _
                                      ColorsToRGB(polys(i).c3), _
                                     New Vector2D(polys(i).u3, polys(i).v3)))
                VxCount += 1

            End If
            'Vx.BoundingBox.AddInternalPoint(vexs(polys(i).vi3).Position)
            Vx.BoundingBox.AddInternalPoint(vexs(polys(i).vi2).Position)
            Vx.BoundingBox.AddInternalPoint(vexs(polys(i).vi1).Position)
            Vx.BoundingBox.AddInternalPoint(vexs(polys(i).vi0).Position)


        Next
        Dim n


        ' n = -1

        For n = 0 To Vx.VertexCount
            Vx.SetIndex(n, n)
            If n > 65535 Then MsgBox("problem")
        Next



        Dim mesh As New Mesh


        mesh.AddMeshBuffer(Vx)
        ' scnNode(k) = ScnMgr.AddOctTreeSceneNode(mesh, ScnMgr.RootSceneNode, -1, 256)

        ScnNode = ScnMgr.AddMeshSceneNode(mesh, Nothing, -1)



        '  scnNode.GetMaterial(0).Texture1 = Material.Texture1



        'scnNode.SetMaterialFlag(MaterialFlag.ZBuffer, True)
        ScnNode.SetMaterialFlag(MaterialFlag.Lighting, False)

        ScnNode.SetMaterialFlag(MaterialFlag.BackFaceCulling, False)
        ScnNode.SetMaterialType(MaterialType.TransparentAlphaChannelRef)
        '   scnNode(k).SetMaterialFlag(MaterialFlag.AnisotropicFilter, True)
        'scnNode(k).SetMaterialType(MaterialType.TransparentAddColor)

        ' scnNode(k).SetMaterialFlag(MaterialFlag.TrilinearFilter, True)





        Models.Add(Me)



    End Sub

    Function mirror() As Car_Model
        Dim newM As New Car_Model(Me.Directory & "\" & Me.FileName)
        For i = 0 To newM.MyModel.vertnum
            newM.MyModel.vexl(i).Position.Y *= -1
        Next i

        For Each MODE As Car_Model In Models
            If MODE.isMirror = True Then ScnMgr.AddToDeletionQueue(MODE.ScnNode)
        Next

        newM.Texture_ = Me.Texture_
        newM.Render()
        newM.ScnNode.Position = Me.ScnNode.Position * New Vector3D(1, -1, 1)


        For j = 0 To newM.MyModel.polynum

        Next
        newM.isMirror = True



        Models.Add(newM)
        Return newM
    End Function




    'Structure

    Public Structure MODEL_POLY_LOAD
        Dim type, Tpage As Int16
        Dim vi0, vi1, vi2, vi3 As Int16
        Dim c0, c1, c2, c3 As Long
        Dim u0, v0, u1, v1, u2, v2, u3, v3 As Single
    End Structure

    Public Structure MODEL_VERTEX_MORPH
        Dim Position As Vector3D
        Dim normal As Vector3D
    End Structure
    Public Structure Sphere
        Dim Center As Vector3D
        Dim radius As Single
    End Structure
    Public Structure BBOX
        Dim minX, maxX As Single
        Dim minY, maxY As Single
        Dim minZ, maxZ As Single
    End Structure




    Public Function ColorsToRGB(ByVal cl As Long) As Color
        'long rgb value, is composed from 0~255 R, G, B
        'according to net: (2^8)^cn
        ' cn: R = 0 , G = 1, B = 2

        If cl < 0 Then Return Color.White ' New Color(255, 255, 255, 255) 'Color.Black
        'simple...
        Dim r = cl \ 65536

        Dim g = (cl Mod 65536) \ 256
        Dim b = (cl Mod 65536) Mod 256


        Return New Color(255, r, g, b)


    End Function
    Public Class MODEL
        Public polynum, vertnum As Short
        Public polyl() As MODEL_POLY_LOAD
        Public vexl() As MODEL_VERTEX_MORPH
    End Class
End Class
Module Public_Models
    Public Models As New List(Of Car_Model)
    Public Models2 As New List(Of Car_Model)
End Module