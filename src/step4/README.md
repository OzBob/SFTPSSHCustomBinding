# Step 4 - The glue that binds it together

This step is brings **everything that was built in the previous steps** together and **wires it up**.

We define an [implementation of `IExtensionConfigProvider`](./SFTPSSHBindingConfigProvider.cs), on which we need to implement the `Initialize` method. The `IExtensionConfigProvider` is the class that defines what types does your binding works with and how to handle those types.

In other words, if the consumer writes `[YourBinding(yourFirstParameter,...)] SomeType output` in the Function signature, how `SomeType` should be handled and which types are supported.

Since we are building an Output Binding, we want to allow the consumer to leverage the `IAsyncCollector<T>` type, and we want that type to resolve to our custom Collector we defined in [Step 3](../step3/README.md).

So in the `IExtensionConfigProvider.Initialize` sample, you will see:

    var rule = context.AddBindingRule<SFTPSSHAttribute>();
    rule.BindToCollector<SFTPSSHBindingOpenType>(typeof(SFTPSSHBindingConverter<>), this);

Which basically says: For the `SFTPSSHAttribute` (consumer using `[SFTPSSH]`), I want to bind a Collector (`rule.BindToCollector`), and I want to use a `SFTPSSHBindingConverter` to convert from the Attribute to an `IAsyncCollector`, and I want its constructor, to receive `this` (the Config Provider) as parameter.

You will notice `<SFTPSSHBindingOpenType>` in the same line, which basically applies a filter on the possible `T` types the user can use when binding to `IAsyncCollector<T>`. The implementation of the filter (and how you can apply your own) is also in the [sample](./SFTPSSHBindingConfigProvider.cs#L69).

The `IExtensionConfigProvider` implementation will receive a `ISFTPSSHBindingCollectorFactory` instance in the constructor, which will use to create new connector or service instances when it's needed:

    internal SFTPSSHBindingContext CreateContext(SFTPSSHAttribute attribute)
    {
        CosmosClient client = GetService(attribute.ConnectionStringSetting, attribute.CurrentRegion);

        return new SFTPSSHBindingContext
        {
            CosmosClient = client,
            ResolvedAttribute = attribute,
        };
    }
    internal CosmosClient GetService(string connectionString, string region)
    {
        string cacheKey = BuildCacheKey(connectionString, region);
        return CollectorCache.GetOrAdd(cacheKey, (c) => this.cosmosBindingCollectorFactory.CreateClient(connectionString, region));
    }

You could simply return new instances of your connector or service, but in this sample, and following Azure Cosmos DB's [performance tips](https://docs.microsoft.com/azure/cosmos-db/performance-tips), we use a cache to maintain and reuse, one instance per pair of Connection String and Region.

Finally, we have a [Converter](./SFTPSSHBindingConverter.cs), his only job is to receive a `SFTPSSHAttribute` and convert it to an `IAsyncCollector`.

## How it all plays out

So, all this wiring works like this:

1. The consumer writes his Function and uses your binding, for example, `[SFTPSSH("myDatabase", "myContainer", ConnectionStringSetting="connectionstringsetting")] IAsyncCollector<MyClass> outputCollector`.
2. The `IExtensionConfigProvider` has a rule that for `IAsyncCollector`, we want to invoke `SFTPSSHBindingConverter`.
3. `SFTPSSHBindingConverter` takes the attribute, creates a `SFTPSSHBindingContext` and creates a new `SFTPSSHBindingAsyncCollector` to inject in the Function.
4. When the consumer calls `outputCollector.AddAsync`, the `SFTPSSHBindingAsyncCollector` will invoke it's `AddAsync` implementation and, in the sample, we save the item in the consumer's Azure Cosmos container.
5. When the Function finishes, `outputCollector.FlushAsync` is invoked (this can also be done manually by the consumer), and whatever implementation we defined, will be invoked.

## Actions in this step

1. [Implement your `IExtensionConfigProvider`](./SFTPSSHBindingConfigProvider.cs), and define the rules and converters you want to apply.
2. Optionally, filter the Types you want the binding to apply by inheriting and overriding `OpenType.Poco`, like `SFTPSSHBindingOpenType`.
3. Define your [`IConverter`](./SFTPSSHBindingConverter.cs) and wire up the creation of your `IAsyncCollector` implementation.
