using Microsoft.AspNetCore.Hosting;
using System;

namespace EnvironmentExtensions
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsFrontDevMode(this IWebHostEnvironment env)
        {
            return env.EnvironmentName == StandardEnvironment.FrontDevMode;
        }
        public static bool IsDevelopment(this IWebHostEnvironment env)
        {
            return env.EnvironmentName == StandardEnvironment.Development;
        }
    }

    public static class StandardEnvironment
    {
        public const string FrontDevMode = "FrontDevMode";
        public const string Development = "Development";
        public const string Test = "Test";
        public const string Staging = "Staging";
        public const string Production = "Production";
        public const string DevExt = "DevExt";
        public const string TestExt = "TestExt";
        public const string StagingExt = "StagingExt";
    }
}
