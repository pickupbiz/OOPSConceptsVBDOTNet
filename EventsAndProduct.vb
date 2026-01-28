Public Delegate Sub PriceChangedHandler(oldPrice As Decimal, newPrice As Decimal)

Public Class Product
    Public Event PriceChanged As PriceChangedHandler

    Public Property Id As Integer
    Public Property Name As String

    Private _price As Decimal
    Public Property Price As Decimal
        Get
            Return _price
        End Get
        Set(value As Decimal)
            If value <> _price Then
                Dim old = _price
                _price = value
                RaiseEvent PriceChanged(old, _price)
            End If
        End Set
    End Property
End Class

Public Class InventoryService
    Public Sub Subscribe(product As Product)
        AddHandler product.PriceChanged, AddressOf OnPriceChanged
    End Sub

    Private Sub OnPriceChanged(oldPrice As Decimal, newPrice As Decimal)
        Console.WriteLine($"Price changed from {oldPrice:C} to {newPrice:C}")
        ' Here you might update DB, notify users, etc.
    End Sub
End Class

