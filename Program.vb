Imports System

Module Program
    Sub Main(args As String())
        Console.Write("Server/Client [S/C]: ")
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
