# DotNet.EntityAttributeValue.Converters

This library relies on the 
[Kuchulem.DotNet.EntityAttributeValue.Abstractions lib](https://github.com/Kuchulem/DotNet.EntityAttributeValue.Abstractions)
to provide converters.

## About converters

Converters are services that converts (hence their name) _raw values_ as stored
(ie: in the database) to typed values usable by an application.

### Types of data

By design the _Entity-Attribute-Value model_ stores values as strings that need
to be converted to usable values. In most case a simple cast could be enought
but for most complex structures or objects (like `DateTime`) some more complex
convertion is expected.

Convertion relies on the _Attribute_ part on the model witch defines the type
of data the value should own.

### Reversibility

Converters are reversible. In this lib they provide `Convert(string value)` 
methods that ensure the convertion from _raw value_ to _typed value_ but they
also provide a `ConvertBack(object value)` method that ensure the reversed convertion, from _typed value_ to _raw value_.

This reversibility ensures the proper data is stored in the _Value_ part of
the model.

## How to install

Choose the method that suits your needs.

### Package Manager

```sh
Install-Package Kuchulem.DotNet.EntityAttributeValue.Converters -Version 1.0.0
```

### .net CLI

```sh
dotnet add package Kuschulem.DotNet.EntityAttributeValue.Converters --version 1.0.0
```

### package reference

```xml
<PackageReference Include="Kuschulem.EntityAttributeValue.Converters" Version="1.0.0" />
```

## Usage

### About abstraction

Althougt this library provide implementations of the
[Kuchulem.DotNet.EntityAttributeValue.Abstractions library](https://github.com/Kuchulem/DotNet.EntityAttributeValue.Abstractions),
you won't use it directly.

When manipulating this implementation you will prefere to use
the interfaces or abstract classes from the abstraction library than the 
implemented classes. So when changing implementation you won't have anything 
to change.

See the [README](https://github.com/Kuchulem/DotNet.EntityAttributeValue.Abstractions/blob/dev/README.md)
of the [Kuchulem.DotNet.EntityAttributeValue.Abstractions library](https://github.com/Kuchulem/DotNet.EntityAttributeValue.Abstractions),
to see how to use abstractions.

### Using the library

In you `Program.cs` file (or wherever you manage the dependency injection) you 
can register the `IEavConverter`, the `IEavRawValueConverterProvider` and 
built-in `IEavRawValueConverter` implementations with an extension method.

```csharp
services.AddEavConverter((serviceProvider, provider) => {
    provider.RegisterGenericConverters();
});
```

This will register both implementations and built-in converters.

The `provider` parameter is an instance of 
`EavRawValueConverterProvider` that will allow you to register the converters 
depending on the _Attribute_ part of your model.

It comes with a `RegisterGenericConverters()` method that registers the 
library's built-in converters.

> The built-in converters rely on the `ValueKind` property of the attributes
> that is an enum for common types : `string`, `boolean`, `integer`, `double`
> and `DateTime`.

The `IEavRawValueConverterProvider` interface provides a method to register
your own converters for specific needs.

Example :
```csharp
// EavRawValueToProductConverter.cs
namespace MyApp.EavConverters 
{
    // Defines a converter to convert a raw value that olds an ID to
    // a product from a service. It must implement IEavRawValueConverter
    class EavRawValueToProductConverter : IEavRawValueConverter
    {
        // Will old the injected service
        private readonly ProductService productService;

        // Constructor, is injected with the ProductService 
        public EavRawValueToProductConverter(ProductService productService)
        {
            this.productService = productService;
        }

        // Converts a raw value (string from DB) to a product from
        // the ProductService
        public object Convert(string value)
        {
            // We try to parse the raw value to long and store it
            // in an "id" property
            if (long.TryParse(value, out var id) && id > 0)
            {
                // If an id is found return the product
                return productService.GetById(id);
            }

            // If not we throw an exception
            throw new Exception("Could not get an id from value in converter");
        }

        // Converts back a product to a raw value (the product id as
        // a string)
        public string ConvertBack(object value)
        {
            // We check the value is a product
            if (value is Product product)
            {
                // We return the ID as a string with invariant culture
                // to avoid thousands separators or other cultural
                // variations that will interfere with the convertions
                return product.Id.ToString(CultureInfo.InvariantCulture);
            }

            // If not a product throw an exception
            throw new Exception("Not a Product");
        }
    }
}
```
Now your converter is defined, you will need to register it in the
`IEavRawValueConverterProvider` implementation.

```csharp
// Program.cs
// We register the converter in services as it requires dependency injection
services.AddScoped<EavRawValueToProductConverter>();

// We register the IEavConverter implementation
services.AddEavConverter((serviceProvider, provider) => {
    // We register our custom converter in the converter provider
    provider.Register(
        // The converter will be selected when the attribute name is "Product"
        attribute => attribute.AttributeName === "Product",
        // The provider is fetched from service provider
        serviceProvider.GetService<EavRawValueToProductConverter>()
    );
    // Prefere to register your custom converters before the
    // generic ones. If an attribute matches both your converter
    // and a generic one, you will probably want to get your converter.
    provider.RegisterGenericConverters();
});
```
When that is done, you only need to inject the `IEavConverter` implementation
in your services.

Let's start by defining a bit of model

```csharp
// Sample.cs
namespace MyApp.Entities
{
    // Defines a sample for laboratory that follows the EAV model
    class Sample : IEavEntity
    {
        // The id of the sample
        public long? Id { get; set; }

        // The Values for the sample
        public virtual IEnumerable<SampleValue>? Values { get; set; }

        // Implementation of IEavEntity.GetValues()
        public virtual IEnumerable<SampleValue> GetValue()
        { 
            return Values ?? Enumerable.Empty<SampleValue>(); 
        }
    }
}

// SampleAttribute.cs
namespace MyApp.Entities
{
    // Defines the Attribute part of the EAV model for a Sample
    class SampleAttribute : IEavAttribute
    {
        // ID of the attribute
        public long? Id

        // implentation of IEavAttribute.AttributeName
        public string? AttributeName { get; set; }

        // implentation of IEavAttribute.ValueKind
        public EavValueKind ValueKind { get; set; }

        // implentation of IEavAttribute.ValueListKind
        public EavValueListKind ValueListKind {get; set; }

        // implentation of IEavAttribute.Source
        public string? Source { get; set; }
    }
}

// SampleValue.cs
namespace MyApp.Entities
{
    // Defines the Value part of the EAV model for a Sample
    class SampleValue : IEavValue
    {
        // ID of the value
        public long? Id

        // The Attribute part of the EAV model (ID)
        public virtual long? AttributeId { get; set; }

        // The Attribute part of the EAV model
        public virtual SampleAttribute? Attribute { get; set; }

        // The sample id for witch the value is set
        public long? SampleId { get; set; }

        // The sample
        public Sample? Sample { get; set; }

        // implentation of IEavValue.RawValue contains the value
        public string? RawValue { get; set; }

        // implementation of IEavValue.GetEntity()
        public object GetEntity()
        {
            return Sample ?? throw new Exception("No Sample");
        }

        // implementation of IEavValue.GetEavAttribute()
        public IEavAttribute GetEavAttribute()
        {
            return Attribute ?? throw new Exception("No attribute");
        }
    }
}
```

And finaly the service :

```csharp
// SampleService.cs
namespace MyApp.Services
{
    // Service to manage samples
    class SampleService
    {
        // Holds the IEavConverter implementation
        private readonly IEavConverter eavConverter;
        // The db context for the app
        private readonly MyAppDbContext dbContext;

        // Constructor with DI
        public SampleService(IEavConverter eavConverter, MyAppDbContext dbContext)
        {
            this.eavConverter = eavConverter;
            this.dbContext = dbContext;
        }

        // Method to get all values from a sample and returns a dictionnary
        // of converted values
        public Dictionary<IEavAttribute, object?> GetDataFromSampleId(long sampleId)
        {
            // get the sample from database
            var sample = dbContext.Samples.FirstOrDefault(s => s.Id === sampleId);
            
            // Convert all values from the sample
            // The Sample class must implement the IEavEntity interface
            if (eavConverter.TryConvertEntityValues(sample, out var converted))
            {
                return converted;
            }

            return default;
        }
    }
}
```