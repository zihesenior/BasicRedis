Namespace RedisCommands
    Public Class SortedList
        Public Shared Function [Add](key As String, score As Double, value As String) As String
            Return $"Zadd {key} {score} {value}"
        End Function
        Public Shared Function Remove(key As String, value As String) As String
            Return $"Zrem  {key} {value}"
        End Function
        Public Shared Function GetAllCount(key As String) As String
            Return $"Zcard {key}"
        End Function
        Public Shared Function GetScoreRangeCount(key As String, minScore As Double, maxScore As Double) As String
            Return $"Zcount {key} {minScore} {maxScore}"
        End Function
        Public Shared Function GetRange(key As String, startOffset As Integer, stopOffset As Integer, Optional withscores As Boolean = False) As String
            Return $"Zrange {key} {startOffset} {stopOffset} { If(withscores, "withscores", "")}"
        End Function
        Public Shared Function GetRangeByScore(key As String, minScore As Double, maxScore As Double, Optional withscores As Boolean = False) As String
            Return $"Zrange {key} {minScore} {maxScore} {If(withscores, "withscores", "")}"
        End Function

    End Class
End Namespace