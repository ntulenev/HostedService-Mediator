# HostedService + Mediator example
Example how to use [MediatR](https://github.com/jbogard/MediatR) from ASP.NET Core HostedService

Using Mediator in HostedService to split consumed data between handlers. 
Scope factory is used to set lifetime for handlers only per item processing scope. (Default behaviour is lifetime per hosted service lifetime).

```C#
await foreach (var request in _consumer.GetDataAsync(ct))
{
    using var scope = _scopeFactory.CreateScope();

    var mediator = scope.ServiceProvider.GetService<IMediator>();

    _logger?.LogInformation("Consume {@request}", request);

    await mediator.Publish(request);
}
```

Output

```
Consume RequestB { Id: RequestId { Id: 1 } }
 >>> Request B Handled - RequestB { Id: RequestId { Id: 1 } }
 Dispose handler B
Consume RequestB { Id: RequestId { Id: 2 } }
 >>> Request B Handled - RequestB { Id: RequestId { Id: 2 } }
 Dispose handler B
Consume RequestA { Id: RequestId { Id: 3 } }
 >>> Request A Handled - RequestA { Id: RequestId { Id: 3 } }
 Dispose handler A
```

With classic approach when we inject IMediator in constuctor we will dispose all handlers only when app will be stopped

```
The service is stopped
 Dispose handler A
 Dispose handler A
 Dispose handler B
 Dispose handler A
 Dispose handler B
 Dispose handler B
 Dispose handler B
 Dispose handler B
 Dispose handler A
```
