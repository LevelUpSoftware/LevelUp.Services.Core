# LevelUp.CrudServiceBuilder.Core
 A .Net Core CRUD Service Framework for .Net 6.0+

## Usage

There are several base classes you can derive from depending on what CRUD methods you want to implement.
- <strong>ServiceBase</strong>
- <strong>ReadServiceBase</strong>
- <strong>CreateServiceBase</strong>
- <strong>UpdateServiceBase</strong>
- <strong>CrudServiceBase</strong>
</br>
</br>
</br>

### ServiceBase

ServiceBase is the lowest level class from which all services are derived. It has the option to inject an IConfiguration instance, making the application configuration available in any service.

Usage Sample:

<i>With Configuration</i>

```
public class CustomerService : ServiceBase
{
    public CustomerService(IConfiguration configuration) : base(configuration)
    {

    }

    //Implement Methods
}
```

<i>Without Configuration</i>

```
public class CustomerService : ServiceBase
{
    public CustomerService()
    {

    }

    //Implement Methods
}
```

All services derived from   ServiceBase have the option to inject   IConfiguration in the constructor. If a service is implemented without a configuration and   Service.Configuration is called,
     ConfigurationNullException will be thrown.

</br>

### ReadServiceBase

Methods
</br>
- GetAllAsync()
- GetByIdAsync()
- GetPagedAsync()

Type Parameters
</br>
```
public abstract class ReadServiceBase<TRepository, TEntityId, TEntity, TDisplayModel>
```
- TRepository: The data repository type. <strong>Must derive from IReadRepository.</strong></br>
- TEntityId: The type of the    Id property on the database entity.</br>
- TEntity: The primary database entity type.</br>
- TDisplayModel: The DTO/Display model type.</br>
</br>
</br>
    
Usage Sample:

```
public class CustomerService : ReadServiceBase<CustomerSqlRepository, long, DB_Customer, DisplayCustomer>, ICustomerService
{
    public CustomerService(IConfiguration configuration, CustomerSqlRepository repository, IMapper mapper) : base(configuration, repository, mapper)
    {

    }

    public override async Task<DisplayCustomer> GetByIdAsync(long id)
    {
        var dbResults = await Repository.GetByIdAsync(id);
        var mappedResults = Mapper.Map<DisplayCustomer>(dbResults);
        return mappedResults;
    } 

    //Override or add any other methods here as desired.
}
```
</br>

### CreateServiceBase

Methods
</br>
- CreateAsync()


Type Parameters
</br>

```
public abstract class CreateServiceBase<TRepository, TEntityId, TEntity, TDisplayModel, TCreateModel, TCreateValidator>
```

- TRepository: The data repository type. <strong>Must derive from ICreateRepository.</strong></br>
- TEntityId: The type of the    Id property on the database entity.</br>
- TEntity: The primary database entity type.</br>
- TDisplayModel: The DTO/Display model type.</br>
- TCreateModel: The DTO/Create model type.</br>
- TCreateValidator: The create model validator type. <strong>Must derive from AbstractValidator</strong></br>
</br>
</br>

Usage Sample:

```
public class CustomerService : CreateService<CustomerSqlRepository, long, DB_Customer, DisplayCustomer, CreateCustomer, CreateCustomerValidator>
{
    public CustomerService(
        IConfiguration configuration, 
        CustomerSqlRepository repository, 
        IMapper mapper, 
        CreateCustomerValidator createValidator) 
        : base(configuration, 
        repository, 
        mapper, 
        createValidator)
        {
        }

        public override async Task<ServiceActionResult<DisplayCustomer>> CreateAsync(CreateCustomer createItem)
        {
            var validationResults = await CreateValidator.ValidateAsync(createItem);
            if(!validationResults.Success)
            {
                return new ServiceActionResult<DisplayCustomer>(validationResults.Errors.Select(x => x.ErrorMessage));
            }

            var newDbItem = Mapper.Map<DB_Customer>(createItem);

            var createResults = await Repository.CreateAsync(newDbItem);
            var mappedResults = Mapper.Map<DisplayCustomer>(createResults);
            return new ServiceActionResult<DisplayCustomer>(mappedResults);
        }

        //Override or add any other methods here as desired.
}
```

### UpdateServiceBase

Methods
</br>
- UpdateAsync()

Type Parameters
</br>
```
public abstract class UpdateServiceBase<TRepository, TEntityId, TEntity, TDisplayModel, TCreateModel, TCreateValidator,
        TUpdateModel, TUpdateValidator>
```

- TRepository: The data repository type. <strong>Must derive from ICreateRepository.</strong></br>
- TEntityId: The type of the    Id property on the database entity.</br>
- TEntity: The primary database entity type.</br>
- TDisplayModel: The DTO/Display model type.</br>
- TCreateModel: The DTO/Create model type.</br>
- TCreateValidator: The create model validator type. <strong>Must derive from AbstractValidator.</strong></br>
- TUpdateValidator: The create model validator type. <strong>Must derive from UpdateValidatorBase.</strong></br>
</br>
</br>

Usage Sample:

```
public class CustomerService : UpdateServiceBase<CustomerSqlRepository, long, DB_Customer, DisplayCustomer, CreateCustomer, CreateCustomerValidator, UpdateCustomer, UpdateCustomerValidator>
{
    public CustomerService(IConfiguration configuration, 
        CustomerSqlRepository repository, 
        IMapper mapper, 
        CreateCustomerValidator createValidator,
        UpdateCustomerValidator updateValidator) 
        : base(configuration, 
        repository, 
        mapper, 
        createValidator,
        updateValidator)
        {
        }

    public override async Task<ServiceActionResult>UpdateAsync(UpdateCustomer updateItem, long id)
    {
        //Allows access to the current entity's Id value from within the validator. 
        UpdateValidator.Id = id;
        var validationResults = await UpdateValidator.ValidateAsync(updateItem);
        if(!validationResults.Success)
        {
            return new ServiceActionResult<DisplayCustomer>(validationResults.Errors.Select(x => x.ErrorMessage))
        }

        var dbUpdateEntity = Mapper.Map<DB_Customer>(updateItem);
        var updateResults = await Repository.UpdateAsync(dbUpdateEntity, id);
        var mappedResults = Mapper.Map<DisplayCustomer>(updateResults);
        return new ServiceActionResult<DisplayCustomer>(mappedResults);
    }

    //Override or add any other methods here as desired.

}
```

### CrudServiceBase

Implements methods from all the other CRUD base services plus DeleteAsync().

Methods
</br>
- DeleteAsync()

Type Parameters
</br>
```
public abstract class CrudServiceBase<CustomerSqlRepository, long, DB_Customer, DisplayCustomer, CreateCustomer, CreateCustomerValidator, UpdateCustomer, UpdateCustomerValidator>
```

- TRepository: The data repository type. <strong>Must derive from ICreateRepository.</strong></br>
- TEntityId: The type of the    Id property on the database entity.</br>
- TEntity: The primary database entity type.</br>
- TDisplayModel: The DTO/Display model type.</br>
- TCreateModel: The DTO/Create model type.</br>
- TCreateValidator: The create model validator type. <strong>Must derive from AbstractValidator.</strong></br>
- TUpdateValidator: The create model validator type. <strong>Must derive from UpdateValidatorBase.</strong></br>
</br>
</br>

Usage Sample:

```
public class CustomerService : CrudServiceBase<CustomerSqlRepository, long, DB_Customer, DisplayCustomer, CreateCustomer, CreateCustomerValidator, UpdateCustomer, UpdateCustomerValidator>
{
    public CustomerService(IConfiguration configuration, 
        CustomerSqlRepository repository, 
        IMapper mapper, 
        CreateCustomerValidator createValidator,
        UpdateCustomerValidator updateValidator) 
        : base(configuration, 
        repository, 
        mapper, 
        createValidator,
        updateValidator)
        {
        }

    public override async Task DeleteAsync(long id)
    {
        //Insert business logic here.
    }

    //Override or add any other methods here as desired.
}
```