﻿using System;
using System.Diagnostics;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Logging;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options.General
{
    public class CustomPathOptions 
        : OptionsBase<IGeneralOptions, GeneralOptionPage>, IGeneralOptions
    {
        public CustomPathOptions(IGeneralOptions options = null)
        {
            try
            {
                if (options == null)
                    options = GetFromDialogPage();

                JavaPath = options.JavaPath;
                NpmPath = options.NpmPath;
                NSwagPath = options.NSwagPath;
                SwaggerCodegenPath = options.SwaggerCodegenPath;
                OpenApiGeneratorPath = options.OpenApiGeneratorPath;
                InstallMissingPackages = options.InstallMissingPackages;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                
                JavaPath = PathProvider.GetJavaPath();
                NpmPath = PathProvider.GetNpmPath();
                NSwagPath = PathProvider.GetNSwagStudioPath();
                SwaggerCodegenPath = PathProvider.GetSwaggerCodegenPath();
                OpenApiGeneratorPath = PathProvider.GetOpenApiGeneratorPath();
                InstallMissingPackages = true;

                Trace.WriteLine(e);
                Trace.WriteLine(Environment.NewLine);
                Trace.WriteLine("Error reading user options. Reverting to default values");
                Trace.WriteLine($"JavaPath = {JavaPath}");
                Trace.WriteLine($"NpmPath = {NpmPath}");
                Trace.WriteLine($"NSwagPath = {NSwagPath}");
                Trace.WriteLine($"SwaggerCodegenPath = {SwaggerCodegenPath}");
                Trace.WriteLine($"OpenApiGeneratorPath = {OpenApiGeneratorPath}");
                Trace.WriteLine($"InstallMissingPackages = {InstallMissingPackages}");
            }
        }

        public string JavaPath { get; set; }
        public string NpmPath { get; set; }
        public string NSwagPath { get; set; }
        public string SwaggerCodegenPath { get; set; }
        public string OpenApiGeneratorPath { get; set; }
        public bool? InstallMissingPackages { get; }
    }
}
