Public Class Employee
    Public Property Id As Integer
    Public Property Name As String

    Public Sub New(id As Integer, name As String)
        Me.Id = id
        Me.Name = name
    End Sub

    ' Overridable method (runtime polymorphism)
    Public Overridable Function CalculateMonthlySalary() As Decimal
        ' Default implementation
        Return 0D
    End Function

    Public Overrides Function ToString() As String
        Return $"{Id} - {Name}"
    End Function
End Class

Public Class PermanentEmployee
    Inherits Employee

    Public Property MonthlyBasic As Decimal
    Public Property Allowances As Decimal

    Public Sub New(id As Integer, name As String, monthlyBasic As Decimal, allowances As Decimal)
        MyBase.New(id, name)
        Me.MonthlyBasic = monthlyBasic
        Me.Allowances = allowances
    End Sub

    Public Overrides Function CalculateMonthlySalary() As Decimal
        Return MonthlyBasic + Allowances
    End Function
End Class

Public Class ContractEmployee
    Inherits Employee

    Public Property HourlyRate As Decimal
    Public Property HoursWorked As Integer

    Public Sub New(id As Integer, name As String, hourlyRate As Decimal, hoursWorked As Integer)
        MyBase.New(id, name)
        Me.HourlyRate = hourlyRate
        Me.HoursWorked = hoursWorked
    End Sub

    Public Overrides Function CalculateMonthlySalary() As Decimal
        Return HourlyRate * HoursWorked
    End Function
End Class

Public Module PayrollModule
    Public Sub PrintSalaries(employees As IEnumerable(Of Employee))
        For Each emp In employees
            Console.WriteLine($"{emp}: Salary = {emp.CalculateMonthlySalary():C}")
        Next
    End Sub
End Module

