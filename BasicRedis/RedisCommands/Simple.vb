Namespace RedisCommands
    Public Class Simple
        Public Shared Function [Set](key As String, value As String, Optional ExpireTime As TimeSpan? = Nothing, Optional Override As Boolean? = Nothing) As String
            If ExpireTime.HasValue Then
                If Override.HasValue Then
                    Return $"Set {key} {value} EX {ExpireTime.Value.TotalSeconds} {If(Override, "XX", "NX")}"
                Else
                    Return $"Set {key} {value} EX {ExpireTime.Value.TotalSeconds}"
                End If
            Else
                If Override.HasValue Then
                    Return $"Set {key} {value} {If(Override, "XX", "NX")}"
                Else
                    Return $"Set {key} {value}"
                End If
            End If
        End Function
        Public Shared Function SetIfNotExist(key As String, value As String) As String
            Return $"SetNx {key} {value}"
        End Function

        Enum ExpireFormat
            SecondsTimeSpan
            MilliSecondsTimeSpan
        End Enum
        Public Shared Function SetWithExpire(key As String, expire As Long, value As String, Optional expireFormat As ExpireFormat = ExpireFormat.SecondsTimeSpan) As String
            If expireFormat = ExpireFormat.MilliSecondsTimeSpan Then Return $"PSetEx {key} {expire} {value}"
            Return $"SetEx {key} {expire} {value}"
        End Function

        Public Shared Function [Get](key As String) As String
            Return $"Get {key}"
        End Function

        ''' <summary>
        ''' 设置新值并返回旧值
        ''' </summary>
        ''' <param name="key"></param>
        ''' <param name="newValue"></param>
        ''' <returns></returns>
        Public Shared Function GetSet(key As String, newValue As String) As String
            Return $"GetSet {key} {newValue}"
        End Function

        Public Shared Function SetRange(key As String, offset As Long, value As String) As String
            Return $"GetRange {key} {offset} {value}"
        End Function

        Public Shared Function GetRange(key As String, starOffset As Long, endOffset As Long) As String
            Return $"GetRange {key} {starOffset} {endOffset}"
        End Function
        Public Shared Function GetStrLenght(key As String) As String
            Return $"StrLen {key}"
        End Function

        Public Shared Function MutiGet(keys As String()) As String
            Return $"MGet {keys.Aggregate(Function(i, j) i & " " & j)}"
        End Function

        Public Shared Function MutiSet(keyValues As Dictionary(Of String, String), Optional onlyOnAllKeysNotExist As Boolean = False) As String
            Dim kvstring = (Function()
                                Dim s As String = ""
                                For Each i In keyValues
                                    s &= i.Key & " """ & i.Value & """ "
                                Next
                                Return s
                            End Function).Invoke
            If onlyOnAllKeysNotExist = True Then Return $"MSetNx {kvstring}"
            Return $"MSet {kvstring}"
        End Function

        Public Shared Function GetBit(key As String, offset As Long) As String
            Return $"GetBit {key} {offset}"
        End Function
        Public Shared Function SetBit(key As String, offset As Long, value As Boolean) As String
            Return $"SetBit {key} {offset} {If(value, 1, 0)}"
        End Function

        Public Shared Function Incr(key As String) As String
            Return $"Incr {key}"
        End Function

        Public Shared Function Increase(key As String, increment As Integer) As String
            Return $"IncrBy {key} {increment }"
        End Function

        Public Shared Function Increase(key As String, increment As Double) As String
            Return $"IncrByFloat {key} {increment }"
        End Function
        Public Shared Function Decr(key As String) As String
            Return $"Decr {key}"
        End Function
        Public Shared Function Decrease(key As String, decrement As Integer) As String
            Return $"DecrBy {key} {decrement }"
        End Function

        ''' <summary>
        ''' 如果 key 已经存在并且是一个字符串， APPEND 命令将 value 追加到 key 原来的值的末尾
        ''' </summary>
        ''' <param name="key"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Shared Function Append(key As String, value As String) As String
            Return $"Append {key} {value}"
        End Function

    End Class
End Namespace