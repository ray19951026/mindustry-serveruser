Imports System.Net.Sockets
Imports System.Net
Imports System.Text
Imports System.Threading
Public Class Form1
    Dim ReceiveThread As New Thread(AddressOf listen, 0)
    Dim t2 = 0
    Dim udpx = 1
    Sub listen()

        Dim udp As New UdpClient(6655)

        Dim RemoteIpEndPoint As New IPEndPoint(IPAddress.Any, 0)
        Do While udpx = 1
            Dim receiveBytes As [Byte]() = udp.Receive(RemoteIpEndPoint)
            Dim returnData As String = Encoding.ASCII.GetString(receiveBytes)
            BeginInvoke(New EventHandler(AddressOf send), returnData)
        Loop

    End Sub

    Private Sub send(sender As Object, e As EventArgs)
        TextBox1.Text = sender
        Try
            Dim bytes(1024) As Byte
            Dim s = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            Dim localEndPoint As New IPEndPoint(IPAddress.Parse("127.0.0.1"), 6859)
            s.Connect(localEndPoint)
            s.Send(Encoding.ASCII.GetBytes(sender))
            s.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReceiveThread.Start()
    End Sub
    Private Sub Form1_close(sender As Object, e As EventArgs) Handles MyBase.Closed
        Try
            ReceiveThread.Abort()
        Catch
        End Try
        udpx = 0
        Dim udpClient As New UdpClient()
        Dim ipAddress As IPAddress = Dns.Resolve("127.0.0.1").AddressList(0)
        Dim ipEndPoint As New IPEndPoint(ipAddress, 6655)
        Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes("Is anybody there?")
        udpClient.Send(sendBytes, sendBytes.Length, ipEndPoint)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AppActivate("C:\Windows\System32\cmd.exe")
        Thread.Sleep(100)
        SendKeys.Send("config socketInput true")
        Thread.Sleep(100)
        SendKeys.Send("{enter}")
        Thread.Sleep(100)
        SendKeys.Send("config socketInputPort 6859")
        Thread.Sleep(100)
        SendKeys.Send("{enter}")

    End Sub
End Class
