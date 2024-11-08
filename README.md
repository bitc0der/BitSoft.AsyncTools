# BitSoft.AsyncTools
[![build](https://github.com/bitc0der/BitSoft.AsyncTools/actions/workflows/build.yml/badge.svg)](https://github.com/bitc0der/BitSoft.AsyncTools/actions/workflows/build.yml)

Yet another one threading tools library for .NET.

## `WaitAsync` for `WaitHandler`
You can use async await for `WaitHandler`. For example:
```csharp
using var handler = new ManualResetEvent(initialState: false);
...
await handler.WaitAsync(cancellationToken: cts.Token);
```
`AwaitAsync()` is awaiable for `ManualResetEvent`, `AutoResetEvent` etc.