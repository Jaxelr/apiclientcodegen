﻿using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.nswag")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagStudioCodeGeneratorBuildTests
    {
        private static Mock<IGeneralOptions> optionsMock;
        private static IGeneralOptions options;
        private static string code;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
            options = optionsMock.Object;

            var contents = NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    File.ReadAllText(Path.GetFullPath("Swagger.json")),
                    "https://petstore.swagger.io/v2/swagger.json")
                .GetAwaiter()
                .GetResult();

            File.WriteAllText("Petstore.nswag", contents);
            new NSwagStudioCodeGenerator(Path.GetFullPath("Petstore.nswag"), options)
                .GenerateCode(new Mock<IVsGeneratorProgress>().Object)
                .Should()
                .BeNull();

            code = File.ReadAllText(Path.GetFullPath("PetstoreClient.cs"));
        }

        [TestMethod]
        public void GeneratedCode_Can_Build_In_NetCoreApp() 
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetCoreApp, code, SupportedCodeGenerator.NSwagStudio);

        [TestMethod]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary() 
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetStandardLibrary, code, SupportedCodeGenerator.NSwagStudio);
    }
}