Public Interface IRepository(Of T)
    Function GetById(id As Integer) As T
    Function GetAll() As IEnumerable(Of T)
    Sub Add(entity As T)
    Sub Remove(entity As T)
End Interface

Public Class InMemoryRepository(Of T As Class)
    Implements IRepository(Of T)

    Private ReadOnly _data As New List(Of T)()

    Public Function GetById(id As Integer) As T Implements IRepository(Of T).GetById
        ' Assuming entities have property "Id"
        Dim prop = GetType(T).GetProperty("Id")
        If prop Is Nothing Then
            Throw New InvalidOperationException("T must have an Id property.")
        End If

        Return _data.FirstOrDefault(Function(item) CInt(prop.GetValue(item)) = id)
    End Function

    Public Function GetAll() As IEnumerable(Of T) Implements IRepository(Of T).GetAll
        Return _data.ToList()
    End Function

    Public Sub Add(entity As T) Implements IRepository(Of T).Add
        _data.Add(entity)
    End Sub

    Public Sub Remove(entity As T) Implements IRepository(Of T).Remove
        _data.Remove(entity)
    End Sub
End Class

