Public Class Readme_generator

    Private Sub Readme_generator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        With Car_browser
            Dim n As String = Replace(_car.Theory.MainInfos.Name, Chr(9), "")
            Do Until Convert.ToInt16(n(0)) >= 65
                n = n.Substring(1)
                Application.DoEvents()
            Loop
            TextBox3.Text = n
            If .Label22.Visible Then TextBox4.Text = .Label22.Text
            TextBox8.Text = Replace(.Label11.Text, Chr(9), "")
            TextBox7.Text = .Label15.Text
            TextBox6.Text = Replace(.Label17.Text, vbNewLine, " (") & ")"
            TextBox11.Text = .Label4.Text
            TextBox9.Text = .Label5.Text.Replace(vbNewLine, " ")

        End With
    End Sub
    Function Generate_Readme() As String
        Dim FULL$ = GenerateSpecial(typu.line)
        Dim Capname$ = TextBox3.Text.ToUpper
        For i = 0 To 41 - 4 - Len(TextBox3.Text)
            Capname = " " & Capname
        Next
        Capname = "+" & Capname
        Capname &= " +" & vbNewLine
        FULL &= Capname
        FULL &= GenerateSpecial(typu.line)

        FULL &= GenerateSpecial(typu.title, "Main Infos")
        FULL &= GenerateOneLine("Name", TextBox3.Text)
        FULL &= GenerateOneLine("Creator", TextBox4.Text)
        FULL &= GenerateOneLine("Type", TextBox5.Text)
        FULL &= GenerateSpecial(typu.empty)
        FULL &= GenerateSpecial(typu.line)
        FULL &= GenerateSpecial(typu.title, "Technicals")
        FULL &= GenerateOneLine("Class", TextBox8.Text)
        FULL &= GenerateOneLine("Rating", TextBox7.Text)
        FULL &= GenerateOneLine("Top speed", TextBox6.Text)
        FULL &= GenerateSpecial(typu.empty)
        FULL &= GenerateSpecial(typu.line)
        FULL &= GenerateSpecial(typu.title, "Models")
        FULL &= GenerateOneLine("Body tris*", TextBox11.Text)
        FULL &= GenerateOneLine("Wheel tris*", TextBox9.Text)
        FULL &= GenerateSpecial(typu.comment, Space(3) & "~tris: triangles")
        FULL &= GenerateSpecial(typu.empty)
        FULL &= GenerateSpecial(typu.line)
        FULL &= GenerateSpecial(typu.title, "Contact")
        FULL &= GenerateOneLine("E-mail", TextBox15.Text)
        FULL &= GenerateOneLine("Site", TextBox14.Text)
        FULL &= GenerateSpecial(typu.empty)
        FULL &= GenerateSpecial(typu.line)
        FULL &= GenerateSpecial(typu.title, "Comments")
        FULL &= TreatTextBox(TextBox10)
        FULL &= GenerateSpecial(typu.empty)
        FULL &= GenerateSpecial(typu.line)
        FULL &= GenerateSpecial(typu.title, "Copyrights")
        FULL &= TreatTextBox(TextBox12)
        FULL &= GenerateSpecial(typu.empty)
        FULL &= GenerateSpecial(typu.line)
        FULL &= vbNewLine
        FULL &= GenerateCopy()
        Return FULL
    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            IO.File.WriteAllText(sfd.FileName, Generate_Readme())
        End If
    End Sub
    Function GenerateOneLine(ByVal Key$, ByVal value$) As String
        Dim n$ = "+ "
        n$ &= Key$

        n &= Space(16 - n.Length)

        n &= ": " & value$
        If n.Length > 40 Then n = n.Substring(0, 40)

        n &= Space(40 - n.Length) & "+"
        Return n & vbNewLine

    End Function
    Function TreatTextBox(ByVal textbox As TextBox) As String
        Dim c = textbox.Text
        Dim buffer$, processed$, final$
        final = ""
        processed = ""
        For i = 0 To c.Split(vbNewLine).Length - 1

            buffer = " " & c.Split(vbNewLine)(i)


            Do Until buffer.Length <= 37
                processed &= buffer.Substring(0, buffer.Substring(0, 37).LastIndexOf(" ")) & Space(39 - buffer.Substring(0, 37).LastIndexOf(" ")) & vbNewLine
                buffer = buffer.Substring(buffer.Substring(0, 37).LastIndexOf(" "))
                Application.DoEvents()
            Loop
            processed &= buffer & Space(38 - Len(buffer)) & vbNewLine


        Next
        processed = "+" & Replace(processed, vbNewLine, "+ " & vbNewLine & "+") & " +" & vbNewLine

        Return processed

    End Function
    Function GenerateSpecial(ByVal T As typu) As String
        Return GenerateSpecial(T, "")
    End Function
    Function GenerateSpecial(ByVal T As typu, ByVal value$)
        If T = typu.blank Then Return vbNewLine
        If T = typu.line Then Return "+-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-+" & vbNewLine
        If T = typu.empty Then Return "+" & Space(39) & "+" & vbNewLine
        Dim tit = value
        If tit.Length > 10 Then tit = tit.Substring(0, 10)
        tit = " " & tit
        tit = tit & Space(10 - tit.Length + 1)
        If T = typu.title Then Return "+   ____________                        +" & vbNewLine & _
                                        "+  [" & tit & ":]                       +" & vbNewLine & _
                                        "+   ¨¨¨¨¨¨¨¨¨¨¨¨			+" & vbNewLine
        If T = typu.comment Then
            Return "+ " & value & Space(41 - 4 - value.Length) & " +" & vbNewLine
        End If
        Return ""
    End Function
    Function GenerateCopy() As String
        Return ".___________________________." & vbNewLine & _
                "| Generated using Car::Load |" & vbNewLine & _
                "¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨" & vbNewLine & _
        Now.ToLongDateString & ", " & Now.ToLongTimeString

    End Function
    Enum typu
        line
        empty
        blank
        title
        comment
    End Enum

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Clipboard.SetText(Generate_Readme())
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class