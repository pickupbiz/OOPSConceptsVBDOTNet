Public MustInherit Class DataExporter
    ' Template method (non-overridable high level algorithm)
    Public Sub Export(path As String)
        Dim data = GetData()
        Dim transformed = TransformData(data)
        SaveToFile(path, transformed)
    End Sub

    ' Abstract members (MustOverride)
    Protected MustOverride Function GetData() As IEnumerable(Of String)
    Protected MustOverride Function TransformData(data As IEnumerable(Of String)) As String

    ' Concrete method
    Protected Overridable Sub SaveToFile(path As String, content As String)
        System.IO.File.WriteAllText(path, content)
    End Sub
End Class

Public Class CsvDataExporter
    Inherits DataExporter

    Protected Overrides Function GetData() As IEnumerable(Of String)
        ' Imagine this comes from DB or API
        Return New List(Of String) From {"John,30", "Mary,25", "David,40"}
    End Function

    Protected Overrides Function TransformData(data As IEnumerable(Of String)) As String
        ' Simple join as csv
        Return String.Join(Environment.NewLine, data)
    End Function
End Class

