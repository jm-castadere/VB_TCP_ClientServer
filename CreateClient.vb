Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets

Public Class CreateClient

    Const valIp As String = "127.0.0.1"
    Const valPort As Int32 = 1234

    Dim netStream As NetworkStream
    Dim brReader As BinaryReader
    Dim brWriter As BinaryWriter

    Public Sub New()

        StartClient("jm castadere")

    End Sub



    Public Sub StartClient(valToWrite As String)

        'Intanze TCP client
        Dim tcpClient As TcpClient = New TcpClient()
        'Connect to server
        tcpClient.Connect(valIp, valPort)

        netStream = tcpClient.GetStream

        brReader = New BinaryReader(netStream)
        brWriter = New BinaryWriter(netStream)

        ' Write a value to server
        brWriter.Write(valToWrite)

        ' Read a value from server with message box
        Console.WriteLine(brReader.ReadString)


    End Sub



End Class
