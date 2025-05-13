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
public interface IProductRepository : IProductRepository
{
    // Your custom repository methods
}

public interface IOrderRepository : IOrderRepository
{
    // Your custom repository methods
}
```
### Implementing repositories
For a multi-context application you must inject DbContext base class.

For a single-context application you can inject the concrete DbContext implementation (not mandatory).
```csharp
public interface ProductRepository(DbContext context) : BaseRepository(context), IProductRepository
{
    // Implements the repository method
}

public interface IOrderRepository(DbContext context) : BaseRepository(context), IOrderRepository
{
    // Implements the repository method
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
Once everything is set up, you can inject the UnitOfWork and use your repositories anywhere in your application:
```csharp
public class MyService(IUnitOfWork unitOfWork)
{
    private readonly IProductRepository _productRepository = unitOfWork.Repository<IProductRepository>();
    private readonly IOrderRepository _orderRepository = unitOfWork.Repository<IOrderRepository>();

    public void DoSomething()
    {
        // Use your repositories here
        var products = _productRepository.GetAll();
        var orders = _orderRepository.GetAll();
    }
    
    public async Task DoSomethingMore(int id)
    {
        // Use your repositories here
        _productRepository.Delete(id);
        // Use to save the changes in the unit of work
        await unitOfWork.SaveChangesAsync();
    }
}
```
Enjoy!