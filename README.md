<p align="center">
  <img src="assets/logo.png" alt="UnitOfWorkinator logo" width="200"/>
</p>

<p align="center"><b>Repositories on autopilot. Unit of Work without the paperwork.</b></p>

## What is UnitOfWorkinator?

UnitOfWorkinator is a .NET library that takes the pain out of configuring and managing repositories using the Unit of Work pattern.

Just define your repositories, and it will:
- Discover and register them automatically
- Inject the correct DbContext
- Keep everything clean, testable, and free of boilerplate

Designed for developers who like clean architecture and hate repetitive setup.

## Installation

You can install UnitOfWorkinator via NuGet:
```
Install-Package UnitOfWorkinator
```
## Example Usage
### Defining Repositories

With UnitOfWorkinator, you only need to define your repositories. Here's how you can do it:
```csharp
public interface IProductRepository : IRepository, IProductRepository
{
    // Your custom repository methods
}

public interface IOrderRepository : IRepository, IOrderRepository
{
    // Your custom repository methods
}
```
### Configuring UnitOfWorkinator
Now, configure UnitOfWorkinator to automatically discover and register your repositories:
```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Configure your DbContext
        services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        
        // Automatically register repositories and de UnitOfWork
        services.AddUnitOfWorkinator<MyDbContext>();
    }
}
```
### Using Repositories
Once everything is set up, you can inject and use your repositories anywhere in your application:
```csharp
public class MyService
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public MyService(IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public void DoSomething()
    {
        // Use your repositories here
        var products = _productRepository.GetAll();
        var orders = _orderRepository.GetAll();
    }
}
```
