Public Class BankAccount
    ' Private fields (encapsulation)
    Private _accountNumber As String
    Private _balance As Decimal

    ' Auto-implemented property
    Public Property OwnerName As String

    ' Read-only property
    Public ReadOnly Property AccountNumber As String
        Get
            Return _accountNumber
        End Get
    End Property

    ' Property with validation
    Public Property Balance As Decimal
        Get
            Return _balance
        End Get
        Private Set(value As Decimal)
            If value < 0D Then
                Throw New ArgumentException("Balance cannot be negative.")
            End If
            _balance = value
        End Set
    End Property

    ' Constructor
    Public Sub New(accountNumber As String, ownerName As String, Optional initialBalance As Decimal = 0D)
        If String.IsNullOrWhiteSpace(accountNumber) Then
            Throw New ArgumentException("Account number is required.")
        End If

        _accountNumber = accountNumber
        OwnerName = ownerName
        Balance = initialBalance
    End Sub

    ' Public method as part of behavior
    Public Sub Deposit(amount As Decimal)
        If amount <= 0D Then
            Throw New ArgumentException("Deposit amount must be positive.")
        End If
        Balance += amount
    End Sub

    Public Sub Withdraw(amount As Decimal)
        If amount <= 0D Then
            Throw New ArgumentException("Withdraw amount must be positive.")
        End If
        If amount > Balance Then
            Throw New InvalidOperationException("Insufficient funds.")
        End If
        Balance -= amount
    End Sub

    Public Overrides Function ToString() As String
        Return $"{OwnerName} - {AccountNumber} - Balance: {Balance:C}"
    End Function
End Class
