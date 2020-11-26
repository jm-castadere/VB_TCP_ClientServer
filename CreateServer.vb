Imports System.IO
Imports System.Net
Imports System.Net.Sockets

Public Class CreateServer

    Const valIp As String = "127.0.0.1"
    Const valPort As Int32 = 1234


    Dim netStream As NetworkStream
    Dim brReader As BinaryReader
    Dim brWriter As BinaryWriter
    Dim socketObj As Socket


    Public Sub New()
        StartServer()
        Console.ReadLine()
    End Sub



    Private Sub StartServer()

        Dim ipAdresss As IPAddress = IPAddress.Parse(valIp)
        'create TCP listener
        Dim tcpList As TcpListener = New TcpListener(ipAdresss, valPort)
        'Start server
        tcpList.Start()
        Console.WriteLine("Server Started”)

        socketObj = tcpList.AcceptSocket
        netStream = New NetworkStream(socketObj)
        brWriter = New BinaryWriter(netStream)
        brReader = New BinaryReader(netStream)

        'Read a value from client 
        Dim valClient As String = brReader.ReadString

        'write a value to client 
        brWriter.Write(valClient)



    End Sub


End Class
