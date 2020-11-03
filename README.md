# SFTPSSHCustomBinding
An Azure WebJob and Azure Functions Custom Binding that supports similar actions to the (Azure Logic app sFTP connector)[].

Uses the [SSH.Net library](https://github.com/sshnet/SSH.NET), which is an open-source Secure Shell (SSH) library that supports .NET.




https://docs.microsoft.com/en-us/azure/connectors/connectors-sftp-ssh

# How to create your own Azure Function bindings!

Watch the recording! 👇

[![Watch the On Dotnet episode](http://img.youtube.com/vi/vKrUn9qiUI8/0.jpg)](http://www.youtube.com/watch?v=vKrUn9qiUI8 "On Dotnet Episode")

This repository acts as a live learning sample of how you can create your own custom **Azure Functions Output Binding** in **5 easy steps**.

Each step is represented by one project that references the previous one, so any change you make in one, will be picked up by the next one.

> The code sample shows how to create an Output Binding for the [Azure Cosmos DB V3 SDK](https://github.com/Azure/azure-cosmos-dotnet-v3), but it is only for learning purposes, **this is not the official Azure Cosmos DB Output Binding for the V3 SDK**.

The guide is comprised of the following steps:

* [Step 1](./src/step1) - Define your attribute
* [Step 2](./src/step2) - Create your connector or service
* [Step 3](./src/step3) - Define your Collector
* [Step 4](./src/step4) - The glue that binds it together
* [Step 5](./src/step5) - Activating the extension

Each step contains the smallest piece of code possible to understand the base concepts required to build your own Output Binding.

At the end, there is a [sample Azure Functions project](./src/sample) that consumes the result of *Step 5* and can be used for testing.


# How to fork and create your own:

* Fork the above repo
* (Rename the repo in github)[https://docs.github.com/en/free-pro-team@latest/github/administering-a-repository/renaming-a-repository#:~:text=1%20On%20GitHub%2C%20navigate%20to%20the%20main%20page,of%20your%20repository.%204%20Click%20Rename.%20You%27re%20done%21]
* Change the 'SFTPSSHBinding' in the below script to your name
* cd to your folder
* Run the script in Git Bash, if you can: copy and paste the whole script.


cd src/sample/
git mv "CosmosDBBinding.Sample.csproj" "SFTPSSHBinding.Sample.csproj"
git mv "CosmosDBSample.cs" "SFTPSSHSample.cs"
cd ../../src/step1/
git mv "CosmosDBAttribute.cs" "SFTPSSHAttribute.cs"
git mv "CosmosDBBinding.Step1.csproj" "SFTPSSHBinding.Step1.csproj"
cd ../../src/step2/
git mv "CosmosDBBinding.Step2.csproj" "SFTPSSHBinding.Step2.csproj"
git mv "CosmosDBBindingCollectorFactory.cs" "SFTPSSHBindingCollectorFactory.cs"
git mv "ICosmosDBBindingCollectorFactory.cs" "ISFTPSSHBindingCollectorFactory.cs"
cd ../../src/step3/
git mv "CosmosDBBinding.Step3.csproj" "SFTPSSHBinding.Step3.csproj"
git mv "CosmosDBBindingAsyncCollector.cs" "SFTPSSHBindingAsyncCollector.cs"
git mv "CosmosDBBindingContext.cs" "SFTPSSHBindingContext.cs"
cd ../../src/step4/
git mv "CosmosDBBinding.Step4.csproj" "SFTPSSHBinding.Step4.csproj"
git mv "CosmosDBBindingConfigProvider.cs" "SFTPSSHBindingConfigProvider.cs"
git mv "CosmosDBBindingConverter.cs" "SFTPSSHBindingConverter.cs"
cd ../../src/step5/
git mv "CosmosDBBinding.Step5.csproj" "SFTPSSHBinding.Step5.csproj"
git mv "CosmosDBWebJobsBuilderExtensions.cs" "SFTPSSHWebJobsBuilderExtensions.cs"
git mv "CosmosDBWebJobsStartup.cs" "SFTPSSHWebJobsStartup.cs"

