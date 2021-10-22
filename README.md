# BasicRedis
A Redis  operation library written in .NET netstandard. You can use it in .net core \ .NET 5 \ .Net Framework 4.x \ xamarin ...

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
