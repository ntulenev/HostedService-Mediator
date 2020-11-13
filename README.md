# HostedService + Mediator example
Example how to use MediatR from ASP.NET Core HostedService

Using Mediator in HostedService to split consumed data between to handlers

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
Consume RequestB { Id: RequestId { Id: 2 } }
 >>> Request B Handled - RequestB { Id: RequestId { Id: 2 } }
```
