Friend Structure RedisReply
    Friend Property Type As ReplyType

    Friend Property Value As Object

    Friend Sub New(type As ReplyType, value As Object)
        Me.Type = type
        Me.Value = value
    End Sub
End Structure

Friend Enum ReplyType
    Array
    BulkString
    [Error]
    [Integer]
    SimpleString
End Enum