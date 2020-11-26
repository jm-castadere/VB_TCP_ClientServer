Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets

Public Class Server
    Dim ServerStatus As Boolean = False
    Dim ServerTrying As Boolean = False
    Dim Server As TcpListener
    Dim Clients As New List(Of TcpClient)

    Public Sub New()
        StartServer()
        ServerLoop()

        Console.WriteLine("Finished server executing.")
        Console.ReadLine()
    End Sub

    Sub ServerLoop()
        While (ServerStatus)
            'show to command 

            Console.Write("$ ")
            Dim str As String = Console.ReadLine
            'Stop enter
            If str.ToUpper = "STOP" Then StopServer()

            'MSG enter
            If str.ToUpper.StartsWith("MSG") Then
                Console.Write("Enter message to clients: ")
                Dim data As String = Console.ReadLine
                Threading.ThreadPool.QueueUserWorkItem(AddressOf SendToClients, data)
            End If
        End While
    End Sub

    Sub StartServer()
        If ServerStatus = False Then
            ServerTrying = True
            Try
                Server = New TcpListener(IPAddress.Any, 65535)
                Server.Start()
                ServerStatus = True

                Threading.ThreadPool.QueueUserWorkItem(AddressOf ClientHandler)

                Console.WriteLine($"IP:{IPAddress.Any} port:{65535} Server Started...")

            Catch ex As Exception
                ServerStatus = False
            End Try

            ServerTrying = False
        End If
    End Sub

    Sub StopServer()
        If ServerStatus Then
            ServerTrying = True

            Try
                For Each Client As TcpClient In Clients
                    Client.Close()
                Next
                Server.Stop()
                ServerStatus = False
                Console.WriteLine("Server Stopped")
            Catch ex As Exception
                StopServer()
            End Try
        End If
    End Sub

    Sub SendToClients(data As String)
        If ServerStatus Then
            If Clients.Count > 0 Then
                Try
                    For Each Client As TcpClient In Clients
                        Dim TX_C As New StreamWriter(Client.GetStream)
                        Dim RX_C As New StreamReader(Client.GetStream)

                        TX_C.WriteLine(data)
                        TX_C.Flush()

                    Next
                Catch ex As Exception
                    SendToClients(data)
                End Try
            Else
                Console.WriteLine("Client not Started..")
            End If
        End If
    End Sub

    Sub ClientHandler()
        Try
            Dim Client As TcpClient = Server.AcceptTcpClient
            If Not ServerTrying Then
                Threading.ThreadPool.QueueUserWorkItem(AddressOf ClientHandler)
            End If
            Clients.Add(Client)
            Console.Write("Client added: " & Client.Client.RemoteEndPoint.ToString & vbCrLf & "$ ")

            Dim TX As New StreamWriter(Client.GetStream)
            Dim RX As New StreamReader(Client.GetStream)

            If RX.BaseStream.CanRead Then
                While RX.BaseStream.CanRead
                    Dim RawData As String = RX.ReadLine
                    Console.Write(Client.Client.RemoteEndPoint.ToString & " >> " & RawData & vbCrLf & "$ ")
                End While
            End If

            If Not RX.BaseStream.CanRead Then
                Client.Close()
                Clients.Remove(Client)
                Console.Write("Client removed: " & Client.Client.RemoteEndPoint.ToString & vbCrLf & "$ ")
            End If

        Catch ex As Exception
            For i As Integer = 0 To Clients.Count
                If i = 0 Then Exit For

                If Not Clients(i).Connected Then
                    Clients(i).Close()
                    Console.Write("Client removed." & vbCrLf & "$ " & vbCrLf)
                    Clients.Remove(Clients(i))
                    Exit For
                End If
            Next

        End Try

    End Sub

End Class
