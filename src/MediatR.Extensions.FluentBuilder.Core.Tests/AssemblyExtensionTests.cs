using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests
{
    public class AssemblyExtensionTests
    {
        [Fact]
        public void GetRequestModules_ShouldReturnAllRequestModulesInAssembly()
        {
            var requestModules = typeof(AssemblyExtensionTests)
                .Assembly
                .GetRequestModulesAs<object>();

            Assert.IsType<TestRequest.Module>(requestModules.Single());
        }

        [Fact]
        public void GetNotificationModules_ShouldReturnAllNotificationModulesInAssembly()
        {
            var notificationModules = typeof(AssemblyExtensionTests)
                .Assembly
                .GetNotificationModulesAs<object>();

            Assert.IsType<TestNotification.Module>(notificationModules.Single());
        }
    }
}