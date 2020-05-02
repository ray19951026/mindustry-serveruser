Imports System.Net.Sockets
Imports System.Net
Imports System.Text

Public Class Form1
    Dim udpClient As New UdpClient()
    Dim ipAddress As IPAddress = Dns.Resolve(My.Settings.ip1).AddressList(0)
    Dim ipEndPoint As New IPEndPoint(ipAddress, 6655)
    Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes("Is anybody there?")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sendBytes = Encoding.ASCII.GetBytes("host")
        udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        sendBytes = Encoding.ASCII.GetBytes("stop")
        udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sendBytes = Encoding.ASCII.GetBytes("gameover")
        udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        sendBytes = Encoding.ASCII.GetBytes("fillitems")
        udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        sendBytes = Encoding.ASCII.GetBytes("admin add " + TextBox1.Text)
        udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        sendBytes = Encoding.ASCII.GetBytes("save " + TextBox2.Text)
        udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        sendBytes = Encoding.ASCII.GetBytes("load " + TextBox2.Text)
        udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint)
    End Sub
    Dim a = 0
    Dim a1
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        a += 1
        If a > 3 Then
            a = 0
            Dim x = InputBox("现在地址:" + My.Settings.ip1, "设置服务器地址", "106.52.195.218",,)
            If x = "" Then
                Exit Sub
            End If
            '检查IP地址是否合法函数
            Dim boolIsIP, intLoop As Integer
            Dim arrIP
            boolIsIP = 0  '函数初始值为true  
            arrIP = Split(x, ".") '将输入的IP用"."分割为数组，数组下标从0开始，所以有效IP分割后的数组上界必须为3  

            If UBound(arrIP) <> 3 Then
                MsgBox("ip错误:" + x)
                boolIsIP = 1
            Else
                For intLoop = 0 To UBound(arrIP)
                    If Not IsNumeric(arrIP(intLoop)) Then       '检查数组元素中各项是否为数字，如果不是则不是有效IP  
                        MsgBox("ip错误:" + x)
                        boolIsIP = 1
                    Else
                        If arrIP(intLoop) > 255 Or arrIP(intLoop) < 0 Then       '检查IP数字是否满足IP的取值范围  
                            MsgBox("ip错误:" + x)
                            boolIsIP = 1
                        End If
                    End If
                Next
            End If
            If boolIsIP = 0 Then
                My.Settings.ip1 = x
                Application.Restart()
            End If
        End If

    End Sub

    Private Sub Timer1_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles Timer1.Elapsed
        If a = a1 Then
            a = 0
        ElseIf Not a = 0 Then
            a1 = a

        End If


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
