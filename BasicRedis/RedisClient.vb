Imports System.Net.Sockets
Imports System.Text
Imports System.Web

Public Class RedisClient
    Private client As TcpClient
    Private stream As NetworkStream
    Private reply As RedisReply

    Public ReadOnly Property Host As String

    Public ReadOnly Property Port As Integer

    Public ReadOnly Property [Return] As Object
        Get
            Return reply.Value
        End Get
    End Property

    Public Sub New(Optional host As String = "127.0.0.1", Optional port As Integer = 6379)
        If Nothing Is host Then
            Throw New ArgumentNullException(NameOf(host))
        End If
        Me.Host = host
        Me.Port = port
        client = New TcpClient() With {.ReceiveBufferSize = 1024 * 1024}
        Try
            client.Connect(host, port)
            stream = client.GetStream()
        Catch ex As Exception
            Throw New RedisException("An existing connection was forcibly closed by remote host.")
        End Try
    End Sub

#Region "Base"
    Public Function Ping()
        Execute(RedisCommands.Base.Ping)
        Return reply.Value
    End Function
    Public Function Echo(msg As String)
        Execute(RedisCommands.Base.Echo(msg))
        Return reply.Value
    End Function
    Public Function Time() As Date
        Execute(RedisCommands.Base.Time)
        Return #1/1/1970#.AddSeconds(CType(reply.Value, IEnumerable(Of Object))(0)).ToLocalTime()
    End Function
    Public Function Auth(password As String)
        Execute(RedisCommands.Base.Auth(password))
        Return reply.Value
    End Function
    Public Function Save()
        Execute(RedisCommands.Base.Save)
        Return reply.Value
    End Function
    Public Function Quit()
        Execute(RedisCommands.Base.Quit)
        Return reply.Value
    End Function
    Public Function SelectDb(index As Integer)
        Execute(RedisCommands.Base.SelectDataBase(index))
        Return reply.Value
    End Function
#End Region

#Region "Key"

    Public Function Keys(pattern As String)
        Execute(RedisCommands.Key.Keys(pattern))
        Return reply.Value
    End Function
    Public Function KeysCount()
        Execute(RedisCommands.Key.KeysCount)
        Return reply.Value
    End Function
    Public Function Dump(key As String) As Byte()
        Execute(RedisCommands.Key.Dump(key))
        If Nothing Is reply.Value Then
            Return Nothing
        Else
            Return Encoding.UTF8.GetBytes(reply.Value.ToString)
        End If
    End Function
    Public Function Exists(key As String)
        Execute(RedisCommands.Key.Exists(key))
        Return reply.Value
    End Function
    Public Function RandomKey()
        Execute(RedisCommands.Key.RandomKey)
        Return reply.Value
    End Function
    Public Function Type(key As String)
        Execute(RedisCommands.Key.Type(key))
        Return reply.Value
    End Function
    Public Function Del(key As String)
        Execute(RedisCommands.Key.Del(key))
        Return reply.Value
    End Function
    Public Function Expire(key As String, timeValue As Long, timeFormat As RedisCommands.Key.ExpireFormat)
        Execute(RedisCommands.Key.Expire(key, timeValue, timeFormat))
        Return reply.Value
    End Function
    Public Function Persist(key As String)
        Execute(RedisCommands.Key.Persist(key))
        Return reply.Value
    End Function

    Public Function TimeToLive(key As String, timeFormat As RedisCommands.Key.TimeToLiveFormat)
        Execute(RedisCommands.Key.TimeToLive(key, timeFormat))
        Return reply.Value
    End Function

    Public Function MoveToDb(key As String, dbIndex As Integer)
        Execute(RedisCommands.Key.MoveToDb(key, dbIndex))
        Return reply.Value
    End Function
#End Region

#Region "Simple"
    Public Function [Set](key As String, value As String, Optional expireTime As TimeSpan? = Nothing, Optional override As Boolean? = Nothing)
        Execute(RedisCommands.Simple.Set(key, value, expireTime, override))
        Return reply.Value
    End Function
    Public Function SetIfNotExist(key As String, value As String)
        Execute(RedisCommands.Simple.SetIfNotExist(key, value))
        Return reply.Value
    End Function
    Public Function SetWithExpire(key As String, expire As Long, value As String, Optional expireFormat As RedisCommands.Simple.ExpireFormat = RedisCommands.Simple.ExpireFormat.SecondsTimeSpan)
        Execute(RedisCommands.Simple.SetWithExpire(key, expire, value, expireFormat))
        Return reply.Value
    End Function

    Public Function [Get](key As String)
        Execute(RedisCommands.Simple.Get(key))
        Return reply.Value
    End Function
    Public Function GetSet(key As String, newValue As String) As String
        Execute(RedisCommands.Simple.GetSet(key, newValue))
        Return reply.Value
    End Function
    Public Function SetRange(key As String, offset As Long, value As String)
        Execute(RedisCommands.Simple.SetRange(key, offset, value))
        Return reply.Value
    End Function
    Public Function GetRange(key As String, starOffset As Long, endOffset As Long)
        Execute(RedisCommands.Simple.GetRange(key, starOffset, endOffset))
        Return reply.Value
    End Function

    Public Function MutiSet(keyValues As Dictionary(Of String, String), Optional onlyOnAllKeysNotExist As Boolean = False)
        Execute(RedisCommands.Simple.MutiSet(keyValues, onlyOnAllKeysNotExist))
        Return reply.Value
    End Function
    Public Function MutiGet(keys As String())
        Execute(RedisCommands.Simple.MutiGet(keys))
        Return reply.Value
    End Function
    Public Function SetBit(key As String, offset As Long, value As Boolean)
        Execute(RedisCommands.Simple.SetBit(key, offset, value))
        Return reply.Value
    End Function
    Public Function GetBit(key As String, offset As Long)
        Execute(RedisCommands.Simple.GetBit(key, offset))
        If Nothing Is reply.Value Then Return Nothing
        If {"0", "1"}.Contains(reply.Value.ToString) Then
            Return If(reply.Value.ToString = "1", True, False)
        Else
            Return Nothing
        End If
    End Function

    Public Function Incr(key As String)
        Execute(RedisCommands.Simple.Incr(key))
        Return reply.Value
    End Function
    Public Function Increate(key As String, increment As Double)
        Execute(RedisCommands.Simple.Increase(key, increment))
        Return reply.Value
    End Function
    Public Function Decr(key As String)
        Execute(RedisCommands.Simple.Decr(key))
        Return reply.Value
    End Function
    Public Function Decreate(key As String, increment As Double)
        Execute(RedisCommands.Simple.Decrease(key, increment))
        Return reply.Value
    End Function
    Public Function Append(key As String, value As String)
        Execute(RedisCommands.Simple.Append(key, value))
        Return reply.Value
    End Function
#End Region

#Region "List"
    Public Function ListMutiGet(key As String, startIndex As Integer, endIndex As Integer)
        Execute(RedisCommands.List.MutiGet(key, startIndex, endIndex))
        Return reply.Value
    End Function
    Public Function ListAppend(key As String, value As String)
        Execute(RedisCommands.List.Append(key, value))
        Return reply.Value
    End Function

    Public Function ListRemove(key As String, value As String)
        Execute(RedisCommands.List.Remove(key, value))
        Return reply.Value
    End Function
#End Region

#Region "Hash"
    Public Function HashDel(key As String, fields As String())
        Execute(RedisCommands.Hash.Del(key, fields))
        Return reply.Value
    End Function
    Public Function HashFields(key As String)
        Execute(RedisCommands.Hash.Fields(key))
        Return reply.Value
    End Function
    Public Function HashValues(key As String)
        Execute(RedisCommands.Hash.Values(key))
        Return reply.Value
    End Function
    Public Function HashGet(key As String, field As String)
        Execute(RedisCommands.Hash.Get(key, field))
        Return reply.Value
    End Function
    Public Function HashMutiGet(key As String, fields As String())
        Execute(RedisCommands.Hash.MutiGet(key, fields))
        Return reply.Value
    End Function
    Public Function HashGetAll(key As String) As Dictionary(Of String, String)
        Dim re As New Dictionary(Of String, String)
        Execute(RedisCommands.Hash.GetAll(key))
        If Nothing Is reply.Value OrElse Not reply.Value.GetType Is GetType(List(Of String)) Then Return re
        Dim r As List(Of String) = reply.Value
        Dim k, v As String
        For i = 0 To r.Count - 1
            If i Mod 2 = 0 Then k = r(i)
            If i Mod 2 = 1 Then v = r(i) : re.Add(k, v)
        Next
        Return re
    End Function
    Public Function HashSet(key As String, field As String, value As String)
        Execute(RedisCommands.Hash.Set(key, field, value.Replace(vbCrLf, "\r\n")))
        Return reply.Value
    End Function
    Public Function HashSetIfNotExist(key As String, field As String, value As String)
        Execute(RedisCommands.Hash.SetIfNotExist(key, field, value))
        Return reply.Value
    End Function
    Public Function HashMutiSet(key As String, fieldValues As Dictionary(Of String, String))
        Execute(RedisCommands.Hash.MutiSet(key, fieldValues))
        Return reply.Value
    End Function
    Public Function HashIncreate(key As String, field As String, increment As Integer)
        Execute(RedisCommands.Hash.Increase(key, field, increment))
        Return reply.Value
    End Function
    Public Function HashIncreate(key As String, field As String, increment As Double)
        Execute(RedisCommands.Hash.Increase(key, field, increment))
        Return reply.Value
    End Function
    Public Function HashLenth(key As String)
        Execute(RedisCommands.Hash.Lenth(key))
        Return reply.Value
    End Function
    Public Function HashScan(key As String, curser As Integer， pattern As String, count As Integer) As Dictionary(Of String, String)
        Execute(RedisCommands.Hash.Scan(key, curser, pattern, count))
        Dim r As List(Of String) = reply.Value
        Dim re As New Dictionary(Of String, String)
        Dim k, v As String
        For i = 0 To r.Count - 1
            If i Mod 2 = 0 Then k = r(i)
            If i Mod 2 = 1 Then v = r(i) : re.Add(k, v)
        Next
        Return re
    End Function

#End Region

#Region "SortedList"
    Public Function SortedListAdd(key As String, score As Double, value As String) As Integer
        Execute(RedisCommands.SortedList.Add(key, score, value))
        Return reply.Value
    End Function
    Public Function SortedListRemove(key As String, value As String) As Integer
        Execute(RedisCommands.SortedList.Remove(key, value))
        Return reply.Value
    End Function
    Public Function SortedListGetAllCount(key As String) As Integer
        Execute(RedisCommands.SortedList.GetAllCount(key))
        Return reply.Value
    End Function
    Public Function SortedListGetScoreRangeCount(key As String, minScore As Double, maxScore As Double) As Integer
        Execute(RedisCommands.SortedList.GetScoreRangeCount(key, minScore, maxScore))
        Return reply.Value
    End Function
    Public Function SortedListGetRange(key As String, startOffset As Integer, stopOffset As Integer, Optional withscores As Boolean = False) As Object
        Execute(RedisCommands.SortedList.GetRange(key, startOffset, stopOffset, withscores))
        Return reply.Value
    End Function
    Public Function SortedListGetRangeByScore(key As String, minScore As Double, maxScore As Double, Optional withscores As Boolean = False) As Object
        Execute(RedisCommands.SortedList.GetRangeByScore(key, minScore, maxScore, withscores))
        Return reply.Value
    End Function

#End Region


    'Public Function GetHashValueByField(key As String, field As String) As String
    '    Execute(RedisCommands.Hash.Get(key, field))
    '    Return reply.Value
    'End Function
    'Public Function GetHashValuesByFields(key As String, ParamArray fields As String()) As List(Of String)
    '    Execute(RedisCommands.Hash.MutiGet(key, fields))
    '    Return reply.Value
    'End Function
    'Public Function GetHashAll(key As String) As List(Of String)
    '    Execute(RedisCommands.Hash.Get(key, Nothing))
    '    Return reply.Value
    'End Function
    'Public Function GetHashAllValues(key As String) As List(Of String)
    '    Execute(RedisCommands.Hash.Values(key))
    '    Return reply.Value
    'End Function
    'Public Function GetHashAllFields(key As String) As List(Of String)
    '    Execute(RedisCommands.Hash.Fields(key))
    '    Return reply.Value
    'End Function
    'Public Function GetHashFields(key As String, fields As String()) As List(Of String)
    '    Execute(RedisCommands.Hash.MutiGet(key, fields))
    '    Return reply.Value
    'End Function
    'Public Function DelFromHash(key As String, Field As String) As Object
    '    Execute(RedisCommands.Hash.Del(key, {Field}))
    '    Return reply.Value
    'End Function


    'Public Function [Set](key As String, value As Object, Optional expireTime As TimeSpan? = Nothing, Optional override As Boolean? = Nothing)
    '    Execute(RedisCommands.Simple.Set(key, value, expireTime, override))
    '    Return reply.Value
    'End Function
    'Public Function SetHashField(key As String, fieldValue As KeyValuePair(Of String, String))
    '    Execute(RedisCommands.Hash.Set(key, fieldValue.Key, fieldValue.Value))
    '    Return reply.Value
    'End Function
    'Public Function SetHashFields(key As String, fieldValues As Dictionary(Of String, String))
    '    Execute(RedisCommands.Hash.MutiSet(key, fieldValues))
    '    Return reply.Value
    'End Function


    Private Sub Execute(command As String)
        Dim bytes() As Byte = Encoding.UTF8.GetBytes(command & vbCrLf)
        Try
            stream.Write(bytes, 0, bytes.Length)
            stream.Flush()
            ReDim bytes(client.ReceiveBufferSize)
            stream.Read(bytes, 0, bytes.Length)
            Dim result = Encoding.UTF8.GetString(bytes)
            Debug.WriteLine(result)
            Select Case result(0)
                Case "$"
                    Dim length = Convert.ToInt32(result.Substring(1, result.IndexOf(vbCrLf) - 1))
                    If length = -1 Then
                        reply = New RedisReply(ReplyType.BulkString, Nothing)
                    Else
                        reply = New RedisReply(ReplyType.BulkString, result.Substring(result.IndexOf(vbCrLf) + 2, length))
                    End If
                Case "+"
                    reply = New RedisReply(ReplyType.SimpleString, result.Substring(1, result.IndexOf(vbCrLf) - 1))
                Case ":"
                    reply = New RedisReply(ReplyType.Integer, Convert.ToInt32(result.Substring(1, result.IndexOf(vbCrLf) - 1)))
                Case "-"
                    reply = New RedisReply(ReplyType.Error, result.Substring(1, result.IndexOf(vbCrLf) - 1))
                    Throw New RedisException(reply.Value)
                Case "*"
                    Dim count = Convert.ToInt32(result.Substring(1, result.IndexOf(vbCrLf) - 1))
                    Dim items = result.Split(New Char() {vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries).ToList()
                    items.RemoveAt(0)
                    items.RemoveAll(Function(i) i.StartsWith("$"))
                    items.RemoveAt(items.Count - 1)
                    reply = New RedisReply(ReplyType.Array, items)
            End Select
            Debug.WriteLine($"{reply.Type}:{reply.Value}")
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub
End Class