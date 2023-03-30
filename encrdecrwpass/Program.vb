Imports System.Security.Cryptography
Imports System.Text

'made by snaipdefix#1288 on discord

Module Module1
    Sub Main()
ChPass:
        PrintDefault("Enter password:")
        Dim password = Console.ReadLine()
        Console.Clear()
MMenu:
        PrintColored("Choose an operation:", ConsoleColor.Green)
        PrintDefault("1. Encrypt a string")
        PrintDefault("2. Decrypt a string")
        PrintDefault("3. Change password")
        Console.WriteLine()
        Console.Write("Current password: ")
        PrintColored(password, ConsoleColor.Blue)
        Console.WriteLine()
        Console.Write(">")
        Dim choice = Console.ReadLine()
        Console.Clear()
        Select Case choice
            Case "1"
                PrintDefault("Enter the string to encrypt:")
                Dim input = Console.ReadLine()
                Dim encrypted = EncryptString(input, password)
                Console.Clear()
                Console.Write("Encrypted string: ")
                PrintColored(encrypted, ConsoleColor.DarkBlue)
            Case "2"
                PrintDefault("Enter the string to decrypt:")
                Dim input = Console.ReadLine()
                Dim decrypted = DecryptString(input, password)
                Console.Clear()
                Console.Write("Decrypted string: ")
                PrintColored(decrypted, ConsoleColor.DarkBlue)
            Case "3"
                GoTo ChPass
            Case Else
                PrintDefault("Invalid choice")
        End Select
        Console.WriteLine()
        GoTo MMenu
    End Sub

    Private Sub PrintDefault(str As String)
        Console.ForegroundColor = ConsoleColor.White
        Console.WriteLine(str)
    End Sub

    Private Sub PrintColored(str As String, color As ConsoleColor)
        Console.ForegroundColor = color
        Console.WriteLine(str)
        Console.ForegroundColor = ConsoleColor.White
    End Sub

    Public Function EncryptString(ByVal input As String, ByVal password As String) As String
        Dim aes As Aes = Aes.Create()
        Dim hashedPass As Byte() = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password))

        aes.Key = hashedPass
        aes.Mode = CipherMode.ECB

        Dim encryptor As ICryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV)
        Dim buffer As Byte() = Encoding.UTF8.GetBytes(input)
        Dim encryptedBytes As Byte() = encryptor.TransformFinalBlock(buffer, 0, buffer.Length)

        Return Convert.ToBase64String(encryptedBytes)
    End Function

    Public Function DecryptString(ByVal input As String, ByVal password As String) As String
        Dim aes As Aes = Aes.Create()
        Dim hashedPass As Byte() = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password))

        aes.Key = hashedPass
        aes.Mode = CipherMode.ECB

        Dim decryptor As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)
        Dim buffer As Byte() = Convert.FromBase64String(input)
        Dim decryptedBytes As Byte() = decryptor.TransformFinalBlock(buffer, 0, buffer.Length)

        Return Encoding.UTF8.GetString(decryptedBytes)
    End Function
End Module