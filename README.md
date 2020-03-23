# MediatR.Extensions.FluentBuilder

[![Build Status](https://dev.azure.com/kluhman/MediatR.Extensions.FluentBuilder/_apis/build/status/MediatR.Extensions.FluentBuilder?branchName=master)](https://dev.azure.com/kluhman/MediatR.Extensions.FluentBuilder/_build/latest?definitionId=3&branchName=master)

## Overview

This package provides a set of fluent API's for defining request pipelines and notification listeners using the MediatR library.

## Motivation

[MediatR](https://github.com/jbogard/MediatR) is a great package by Jimmy Bogard for easily implementing the MediatR pattern in your .NET project and has become my go to framework when starting a green field application. However, out of the box, there are some things about it that are just a little too magic-y for my taste.
1. Open Generics - whether they will or won't be instantiated based on my type is unclear, and difficult to trace with no clear typing association. 
2. Execution Order - Out of the box, the order in which pipeline behaviors execute is based upon the order they are added to your DI container. However, this is unclear and if unknown to developers, can lead to unexpected behavior.
3. Discoverability - Because all handlers/behaviors/etc... are registered through DI, it can be difficult to easily discover which behaviors and handlers manage which requests/notifications.

This package aims to solve that problem through explicit pipeline definition and a usage convention which prioritizes discoverability over loose coupling.
