# BasicRedis
A Redis  operation library written in .Net.

How to use:

    C#:
```c#
RedisClient client = new RedisClient("xxx.xxx.xxx.xxx", xxxx);
client.Auth("xxxxxx");
var re = client.HashGetAll("xxxxx");
...
```

    VB.NET:
```vb.net
Dim client As New RedisClient("xxx.xxx.xxx.xxx", xxxx)'
client.Auth("xxxxxx")
Dim re = client.HashGetAll("xxxxx")
...
```
return dictionary、list and so on.
