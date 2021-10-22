# BasicRedis
A Redis  operation library written in .Net.

How to use:

```vb.net
Dim client As New BasicRedis.RedisClient("xxx.xxx.xxx.xxx", xxxx)'
client.Auth("xxxxxx")
Dim re = client.HashGetAll("xxxxx")
...
```
return dictionary、list and so on.
