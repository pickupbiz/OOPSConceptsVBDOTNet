Public Interface ILogger
    Sub LogInfo(message As String)
    Sub LogError(message As String, ex As Exception)
End Interface

Public Interface IAuditable
    Sub Audit(action As String, performedBy As String)
End Interface

Public Class ConsoleLogger
    Implements ILogger, IAuditable

    Public Sub LogInfo(message As String) Implements ILogger.LogInfo
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine($"[INFO] {DateTime.Now}: {message}")
        Console.ResetColor()
    End Sub

    Public Sub LogError(message As String, ex As Exception) Implements ILogger.LogError
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine($"[ERROR] {DateTime.Now}: {message} - Exception: {ex.Message}")
        Console.ResetColor()
    End Sub

    Public Sub Audit(action As String, performedBy As String) Implements IAuditable.Audit
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine($"[AUDIT] {DateTime.Now}: {performedBy} performed {action}")
        Console.ResetColor()
    End Sub
End Class

