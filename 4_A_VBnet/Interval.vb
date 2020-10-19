Public Class Interval
    Public LowerInclusiveBound As Integer
    Public IntervalStep As Integer
    Public Count As Integer = 0
    Public RelativeFrequency As Double
    Public Percentage As Double

    Public Function containsValue(ByVal v As Double) As Boolean
        Return (v >= LowerInclusiveBound AndAlso v < (LowerInclusiveBound + IntervalStep))
    End Function

End Class
