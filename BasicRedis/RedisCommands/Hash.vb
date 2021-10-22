Namespace RedisCommands
    Public Class [Hash]
        Public Shared Function Del(key As String, fields As String()) As String
            Return $"HDel {key} {fields.Aggregate(Function(i, j) $"{i} {j}")}"
        End Function

        Public Shared Function Fields(key As String) As String
            Return $"HKeys {key}"
        End Function

        Public Shared Function Values(key As String) As String
            Return $"HVals {key}"
        End Function

        Public Shared Function Exists(key As String, filed As String) As String
            Return $"HExists {key} {filed}"
        End Function

        Public Shared Function [Get](key As String, field As String) As String
            Return $"HGet {key} {field}"
        End Function

        Public Shared Function MutiGet(key As String, fields As String()) As String
            Return $"HMGet {key} {fields.Aggregate(Function(i, j) i & " " & j)}"
        End Function
        Public Shared Function GetAll(key As String) As String
            Return $"HGetAll {key}"
        End Function

        Public Shared Function [Set](key As String, field As String, value As String) As String
            Return $"HSet {key} {field} '{value}'"
        End Function

        Public Shared Function [SetIfNotExist](key As String, field As String, value As String) As String
            Return $"HSetNx {key} {field} {value}"
        End Function

        Public Shared Function MutiSet(key As String, fieldValues As Dictionary(Of String, String)) As String
            Dim fvstring = (Function()
                                Dim s As String = ""
                                For Each i In fieldValues
                                    s &= i.Key & " """ & i.Value & """ "
                                Next
                                Return s
                            End Function).Invoke
            Return $"HMSet {key} {fvstring}"
        End Function

        Public Shared Function Increase(key As String, field As String, increment As Integer) As String
            Return $"HIncrBy {key} {field} {increment }"
        End Function
        Public Shared Function Increase(key As String, field As String, increment As Double) As String
            Return $"HIncrByFloat {key} {field} {increment }"
        End Function

        ''' <summary>
        ''' 字段数量
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        Public Shared Function Lenth(key As String) As String
            Return $"HLen {key}"
        End Function

        Public Shared Function Scan(key As String, curser As Integer， pattern As String, count As Integer) As String
            Return $"HScan {key} {curser} match {pattern} count {count}"
        End Function

    End Class
End Namespace