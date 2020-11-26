Imports System
Imports System.Net

Module Program
    Sub Main(args As String())

        'StartTCP()

        StartProcess()

    End Sub


    Private Sub StartProcess()

        'Get host name local PC
        Dim hostName As String = Dns.GetHostName()
        'get local pc IP
        Dim addresses As IPAddress() = Dns.GetHostEntry(hostName).AddressList()

        For Each hostAdr As IPAddress In addresses
            Console.WriteLine("Name: " & hostName & " IP Address: " & hostAdr.ToString())
            If hostAdr.AddressFamily.ToString() = "InterNetwork" Then
                Console.WriteLine("    [Potential host IP]")
            End If
        Next

        'Show query
        Console.Write(vbCrLf & "Selected Server or Client [S/C]: ")
        'Get repsonse
        Dim response As String = Console.ReadLine()

        Select Case Char.ToUpper(response(0))
            'If server
            Case "S"
                Console.WriteLine("Server created")
                Dim server As New Server
            'if client
            Case "C"
                Console.WriteLine("Client created")
                Dim client As New Client
        End Select

        Console.ReadLine()

    End Sub


    Sub StartTCP()

        Console.WriteLine("Server Created")
        Dim server As New CreateServer()

        Console.WriteLine("Client Created")
        Dim client As New CreateClient()

        Console.ReadLine()

    End Sub


End Module
