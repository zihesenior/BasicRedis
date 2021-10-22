Namespace RedisCommands
    Public Class Base
        Public Shared Function Ping() As String
            Return $"Ping"
        End Function
        Public Shared Function Echo(str As String) As String
            Return $"Echo {str}"
        End Function

        Public Shared Function Time() As String
            Return $"Time"
        End Function

        Public Shared Function Auth(password As String) As String
            Return $"Auth {password}"
        End Function
        Public Shared Function Save() As String
            Return $"Save"
        End Function

        ''' <summary>
        ''' 关闭连接
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function Quit() As String
            Return $"Quit"
        End Function


        ''' <summary>
        ''' 选择当前使用数据库
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function [SelectDataBase](index As Integer) As String
            Return $"Select {index}"
        End Function
    End Class
End Namespace