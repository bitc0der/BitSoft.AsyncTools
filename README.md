# BitSoft.AsyncTools
[![build](https://github.com/bitc0der/BitSoft.AsyncTools/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bitc0der/BitSoft.AsyncTools/actions/workflows/dotnet.yml)

## `WaitAsync` for `WaitHandler`
You can use async await for `WaitHandler`. For example:
```csharp
using var handler = new ManualResetEvent(initialState: false);
...
await handler.WaitAsync(cancellationToken: cts.Token);
```
`AwaitAsync()` is awaiable for `ManualResetEvent`, `AutoResetEvent` etc.