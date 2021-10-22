Namespace RedisCommands
    Public Class Key
        Public Shared Function Keys(pattern As String) As String
            Return $"Keys {pattern}"
        End Function
        Public Shared Function KeysCount() As String
            Return $"DbSize"
        End Function
        Public Shared Function Dump(key As String) As String
            Return $"Dump {key}"
        End Function
        Public Shared Function RandomKey() As String
            Return $"RandomKey"
        End Function
        Public Shared Function Exists(key As String) As String
            Return $"Exists {key}"
        End Function

        Public Shared Function Rename(key As String, newkey As String, Optional onlyOnNewKeyNotExist As Boolean = True) As String
            If onlyOnNewKeyNotExist Then
                Return $"RenameNx {key} {newkey}"
            Else
                Return $"Rename {key} {newkey}"
            End If
        End Function

        Public Shared Function Type(key As String) As String
            Return $"Type {key}"
        End Function

        Public Shared Function Del(key As String) As String
            Return $"Del {key}"
        End Function

        Enum ExpireFormat
            SecondsTimeSpan
            SecondsTimeStamp
            MilliSecondsTimeSpan
            MilliSecondsTimeStamp
        End Enum
        Public Shared Function Expire(key As String, timeValue As Long, timeFormat As ExpireFormat) As String
            Select Case timeFormat
                Case ExpireFormat.SecondsTimeSpan
                    Return $"Expire {key} {timeValue}"
                Case ExpireFormat.SecondsTimeStamp
                    Return $"ExpireAt {key} {timeValue}"
                Case ExpireFormat.MilliSecondsTimeSpan
                    Return $"PExpire {key} {timeValue}"
                Case ExpireFormat.MilliSecondsTimeStamp
                    Return $"PExpireAt {key} {timeValue}"
                Case Else
                    Return Nothing
            End Select
        End Function

        ''' <summary>
        ''' 移除Expire设置的过期值
        ''' </summary>
        ''' <param name="key"></param>
        ''' <returns></returns>
        Public Shared Function Persist(key As String) As String
            Return $"Persist {key}"
        End Function

        Enum TimeToLiveFormat
            SecondsTimeSpan
            MilliSecondsTimeSpan
        End Enum
        Public Shared Function TimeToLive(key As String, timeFormat As TimeToLiveFormat) As String
            Select Case timeFormat
                Case TimeToLiveFormat.SecondsTimeSpan
                    Return $"TTL {key}"
                Case TimeToLiveFormat.MilliSecondsTimeSpan
                    Return $"PTTL {key}"
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Shared Function MoveToDb(key As String, dbIndex As Integer) As String
            Return $"Move {key} {dbIndex}"
        End Function
    End Class
End Namespace