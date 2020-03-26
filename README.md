# MediatR.Extensions.FluentBuilder

[![Build Status](https://dev.azure.com/kluhman/MediatR.Extensions.FluentBuilder/_apis/build/status/MediatR.Extensions.FluentBuilder?branchName=master)](https://dev.azure.com/kluhman/MediatR.Extensions.FluentBuilder/_build/latest?definitionId=3&branchName=master)
[![Nuget](https://img.shields.io/nuget/v/MediatR.Extensions.FluentBuilder.Core)](https://www.nuget.org/packages/MediatR.Extensions.FluentBuilder.Core/)

## Overview

This package provides a set of fluent API's for defining request pipelines and notification listeners using the MediatR library.

## Why use this package?

[MediatR](https://github.com/jbogard/MediatR) is a great package by Jimmy Bogard for easily implementing the Mediator pattern in your .NET project and has become my go to framework when starting a green field application. However, out of the box, there are some things about it that are just a little too magic-y for my taste.
1. Open Generics - whether they will or won't be instantiated based on my type is unclear, and difficult to trace with no clear typing association. 
2. Execution Order - Out of the box, the order in which pipeline behaviors execute is based upon the order they are added to your DI container. However, this is unclear and if unknown to developers, can lead to unexpected behavior.
3. Discoverability - Because all handlers/behaviors/etc... are registered through DI, it can be difficult to easily discover which behaviors and handlers manage which requests/notifications.

This package aims to solve these problem through explicit pipeline definition and a usage convention which prioritizes discoverability over loose coupling.

## Installation 

The package can be installed using the dotnet CLI or via the Package Manager. This package currently supports both the standard Microsoft dependency injection system as well as the Autofac package. If you'd like to use a different DI system, you can easily extend the same functionality by implementing the interfaces in the Core package. 

**.NET CLI**
```
dotnet add package MediatR.Extensions.FluentBuilder.Core

dotnet add package MediatR.Extensions.FluentBuilder.Microsoft

dotnet add package MediatR.Extensions.FluentBuilder.Autofac
```

**Package Manager**

```
Install-Package MediatR.Extensions.FluentBuilder.Core

Install-Package MediatR.Extensions.FluentBuilder.Microsoft

Install-Package MediatR.Extensions.FluentBuilder.Autofac
```

## Usage

In whatever project you are housing all of your MediatR requests in, simply add the following class.

```
public class MediatRModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddMediatR();
        services.AddRequestModules(typeof(MediatRModule).Assembly);
        services.AddNotificationModules(typeof(MediatRModule).Assembly);    
    }
}

// Startup.cs
services.AddModule<MediatRModule>();
```

This will add all of the required MediatR classes to your DI container as well as scan the assembly for all the modules used to define your request pipelines and notification handlers.

For each request, you simply create an additional `RequestModule` which defines the pipeline. This gives a great overview for a developer to come and see exactly which code is executing in which order for a specific pipeline.

```
public class MyRequest : IRequest<MyResponse>
{
    public class Handler : IRequestHandler<MyRequest, MyResponse>
    {
        ...
    }

    public class Module : RequestModule<MyRequest, MyResponse>
    {
        public override IExceptionPipelineBuilder<MyRequest, MyResponse> BuildPipeline(IPipelineBuilder<MyRequest, MyResponse> builder)
        {
            return builder
                .AddPreProcessor<MyPreProcessor>() //optional
                .AddBehavior<MyBehavior>() //optional
                .AddHandler<Handler>()
                .AddPostProcessor<MyPostProcessor>() //optional
                .AddExceptionAction<MyException, MyExceptionAction>() //optional
                .AddExceptionHandler<MyException, MyExceptionHandler>(); //optional
        }
    }
}
```

The only thing that is required for every request module is a Handler, but all the other pipeline components can be omitted. The pipeline builder interfaces also enforce adding each component in the order which it would execute in the pipeline, helping to provide a quick overview of everything happening for a single request. 

Notifications provide a similar, but simpler mechanism since they do not support full pipelines.

```
public class MyNotification : INotification
{
    public class Module : NotificationModule<MyNotification>
    {
        public override void RegisterHandlers(INotificationRegistry<MyNotification> registry)
        {
            registry
                .AddHandler<MyHandler>()
                .AddHandler<MyOtherHandler>();
        }
    }
}
```