﻿using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;

namespace Rapicgen.CLI.Commands.CSharp
{
    [ExcludeFromCodeCoverage]
    public class CSharpCommand : Command<CSharpCommand.Settings>
    {
        public class Settings : CommandSettings
        {
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            // This will be handled by subcommands
            return 0;
        }
    }
}