Imports System
Imports System.Net

Module Program
    Sub Main(args As String())

        Dim hostName As String = System.Net.Dns.GetHostName()
        Dim addresses As IPAddress() = Dns.GetHostEntry(hostName).AddressList()
       
        For Each hostAdr As IPAddress In addresses
            Console.WriteLine("Name: " & hostName & " IP Address: " & hostAdr.ToString())
            If hostAdr.AddressFamily.ToString() = "InterNetwork" Then
                Console.WriteLine("    [Potential host IP]")
            End If
        Next

        Console.Write(vbCrLf & "Server/Client [S/C]: ")
        Dim res As String = Console.ReadLine()

        Select Case Char.ToUpper(res(0))
            Case "S"
                Console.WriteLine("Server Selected!")
                Dim server As New Server
            Case "C"
                Console.WriteLine("Client Selected!")
                Dim client As New Client
        End Select

        Console.ReadLine()
    End Sub
End Module
