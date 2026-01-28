Public Class User
    Public Property Id As Integer
    Public Property UserName As String
    Public Property Email As String

    Public Sub New()
        ' Default constructor
        Me.New(0, "Guest", "guest@example.com")
    End Sub

    Public Sub New(id As Integer, userName As String)
        Me.New(id, userName, $"{userName}@example.com")
    End Sub

    Public Sub New(id As Integer, userName As String, email As String)
        Me.Id = id
        Me.UserName = userName
        Me.Email = email
    End Sub

    Public Overrides Function ToString() As String
        Return $"{Id} - {UserName} ({Email})"
    End Function
End Class

