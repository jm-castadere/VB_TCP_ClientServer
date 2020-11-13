Imports System.IO
Imports System.Net.Sockets

Public Class Client
    Dim Client As TcpClient
    Dim RX As StreamReader
    Dim TX As StreamWriter
    Dim ServerIP As String

    Public Sub New()
        Connect()
    End Sub

    Sub ClientLoop()
        While True
            Console.Write("$ ")
            Dim str As String = Console.ReadLine

            If str.ToUpper = "DC" Then Disconnect()
            If str.ToUpper.StartsWith("MSG") Then
                Console.Write("Enter message to the server: ")
                Dim data As String = Console.ReadLine
                Threading.ThreadPool.QueueUserWorkItem(AddressOf SendToServer, data)
            End If
        End While
    End Sub

    Sub Connect()
        Console.Write("Enter Server IP: ")
        ServerIP = Console.ReadLine
        Console.WriteLine("Trying to connect...")
        Try
            Client = New TcpClient(ServerIP, 65535)
            If Client.GetStream.CanRead Then
                RX = New StreamReader(Client.GetStream)
                TX = New StreamWriter(Client.GetStream)

                Threading.ThreadPool.QueueUserWorkItem(AddressOf Connected)
                ClientLoop()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub Connected()
        Console.Write("Connected." & vbCrLf & "$ ")

        If RX.BaseStream.CanRead Then
            Try
                While RX.BaseStream.CanRead
                    Dim RawData As String = RX.ReadLine
                    Console.Write("Server >> " & RawData & vbCrLf & "$ ")
                End While
            Catch ex As Exception
                Client.Close()
            End Try
        End If
    End Sub

    Sub Disconnect()
        Try
            Client.Close()
            Console.WriteLine("Connection ended.")
        Catch ex As Exception
        End Try
    End Sub

    Sub SendToServer(data As String)
        Try
            TX.WriteLine(data)
            TX.Flush()
        Catch ex As Exception

        End Try
    End Sub

    Sub MSG(data As String)
        Console.WriteLine("MSG: " & data)
    End Sub
End Class
