Public Class Calculator
    ' Overloaded methods (same name, different signatures)
    Public Function Add(a As Integer, b As Integer) As Integer
        Return a + b
    End Function

    Public Function Add(a As Decimal, b As Decimal) As Decimal
        Return a + b
    End Function

    Public Function Add(a As Integer, b As Integer, c As Integer) As Integer
        Return a + b + c
    End Function

    Public Function Add(values As IEnumerable(Of Integer)) As Integer
        Return values.Sum()
    End Function
End Class

