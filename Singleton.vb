Public Class Singletons
    Private Shared newStr As String
    Sub New(ByVal FilePath As String)
        newStr = IO.File.ReadAllText(FilePath)
        Clean()
    End Sub
    Sub Clean()
        Dim temp = Split(newStr, vbNewLine)
        Dim CleanStr As String = ""
        For i = 0 To UBound(temp)
            If InStr(temp(i), ";") > 0 Then

                CleanStr &= Split(temp(i), ";")(0) & vbNewLine
            Else

                CleanStr &= temp(i) & vbNewLine
            End If
        Next

        Do Until InStr(CleanStr, vbNewLine & vbNewLine) < 1
            CleanStr = Replace(CleanStr, vbNewLine & vbNewLine, vbNewLine)
        Loop

        newStr = CleanStr
    End Sub
    Public Function getAllSingletons() As String()
        Dim temp() = newStr.Split(vbNewLine)
        Dim header As String = ""
        For i = LBound(temp) To UBound(temp)
            If InStr(temp(i), "{") > 0 Then
                header &= Replace(Replace(Split(temp(i), "{")(0), " " & vbTab, ""), vbTab, "") & ","
            End If
        Next

        Return header.Split(",")
    End Function
    Public Sub SaveToFile(ByVal path As String)
        IO.File.WriteAllText(path, newStr)
    End Sub

    Public Function getSingleton(ByVal header) As SingletonItem
        If InStr(newStr, header) < 1 Then
            Return SingletonItem.Null
        End If


        Dim temp As String = ""
        If header = "" Or header = " " Then
            temp = Split(newStr, "{")(1)

        Else
            temp = Split(Split(newStr, header)(1), "{")(1)
        End If

        If InStr(Split(temp, "}")(0), "{") < 1 Then
            'lucky us!
            Dim l = Split(temp, "}")(0)
            Dim torep = Split(l, vbNewLine).Last

            Return SingletonItem.ToSingletonItem(Replace(l, torep, ""))


        Else
            'how unlucky...
            Dim tmp As String = temp
            Do Until InStr(tmp, "{") = 0
                Dim splt = Split(Split(Split(tmp, "{")(0), vbNewLine)(1), "}")(0)
                tmp = Replace(tmp, splt, "")
            Loop
            Return SingletonItem.ToSingletonItem(tmp)
        End If
        Return SingletonItem.Null
    End Function

End Class
Public Class SingletonItem
    Private Shared items() As String
    Public Shared Null = Nothing

    Public Shared Function ToSingletonItem(ByVal str As String) As SingletonItem
        Dim nSing As New SingletonItem
        Dim splitted() = Split(str, vbNewLine)
        ReDim items(splitted.Length)
        SingletonItem.items = splitted
        SingletonItem.items = splitted
        Return nSing
    End Function

    Public Function getValue(ByVal key)

        For i = LBound(items) To UBound(items)

            If InStr(items(i), key) > 0 Then
                Dim tmp = Replace(Split(items(i), key)(1), " " & vbTab, "") ', ".", ",")

                If InStr(CSng(2.15), ",") <> 0 Then
                    tmp = Replace(tmp, ".", ",")
                    Return tmp

                End If

                Return tmp

            End If
        Next
        Return Nothing
    End Function
    Public Function getAllKeys()
        Dim allVal(items.Length) As String
        For i = LBound(items) To UBound(items)
            If InStr(items(i), " " & vbTab) > 0 Then
                allVal(i) = Split(items(i), " " & vbTab)(0)
            Else
                allVal(i) = Split(items(i), vbTab)(0)
            End If



        Next
        Return allVal
    End Function
    Public Sub setValue(ByVal key As String, ByVal value As String)
        For i = LBound(items) To UBound(items)

            If InStr(items(i), key) > 0 Then

                items(i) = Replace(items(i), Split(items(i), key)(1), value)

                Exit Sub
            End If
        Next
        Dim nItems(items.Length + 1)

        For i = LBound(items) To UBound(items)
            nItems(i) = items(i)
        Next

        ReDim items(items.Length + 1)

        For i = LBound(items) To UBound(items) - 1
            items(i) = nItems(i)
        Next
        items(UBound(items)) = key & " " & vbTab & value

    End Sub
    Public Function GetEverything() As String()
        Return items
    End Function


End Class