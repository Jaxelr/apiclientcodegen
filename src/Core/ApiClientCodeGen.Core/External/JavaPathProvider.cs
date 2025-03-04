﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Rapicgen.Core.Exceptions;
using Rapicgen.Core.Generators;
using Rapicgen.Core.Logging;
using Rapicgen.Core.Options.General;

namespace Rapicgen.Core.External
{
    public class JavaPathProvider
    {
        private readonly IGeneralOptions options;
        private readonly IProcessLauncher processLauncher;

        public JavaPathProvider(IGeneralOptions options, IProcessLauncher processLauncher)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
        }

        public string GetJavaExePath()
        {
            var javaPath = options.JavaPath;
            if (!string.IsNullOrWhiteSpace(javaPath) &&
                (File.Exists(javaPath) || javaPath != "java") &&
                CheckJavaVersion(javaPath))
                return javaPath;

            if (string.IsNullOrWhiteSpace(options.JavaPath))
                javaPath = PathProvider.GetIncludedJavaPath();

            if (CheckJavaVersion(javaPath))
                return javaPath;

            javaPath = PathProvider.GetInstalledJavaPath();
            if (CheckJavaVersion(javaPath))
                return javaPath;

            if (CheckJavaVersion("java"))
                return "java";

            throw new MissingJavaRuntimeException("Unable to find Java executable");
        }

        [ExcludeFromCodeCoverage]
        private bool CheckJavaVersion(string javaPath)
        {
            try
            {
                Logger.Instance.WriteLine("Checking Java version");
                processLauncher.Start(javaPath, "-version");
                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.TrackError(e);
                Logger.Instance.WriteLine($"Unable to start Java from path: {javaPath}");
            }

            return false;
        }
    }
}