﻿using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    public class NSwagOptionsPage : DialogPage, INSwagOptions
    {
        public const string Name = "NSwag";

        [Category(Name)]
        [DisplayName("Inject HttpClient")]
        [Description("Set this to TRUE to generate the constructor that accepts HttpClient")]
        public bool InjectHttpClient { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate Interfaces")]
        [Description("Set this to TRUE to generate client interfaces")]
        public bool GenerateClientInterfaces { get; set; } = true;

        [Category(Name)]
        [DisplayName("Generate DTO types")]
        [Description("Set this to TRUE to generate DTO types")]
        public bool GenerateDtoTypes { get; set; } = true;

        [Category(Name)]
        [DisplayName("Use Base URL")]
        [Description("Set this to TRUE to include a base URL for every HTTP request")]
        public bool UseBaseUrl { get; set; } = false;

        [Category(Name)]
        [DisplayName("C# Class Style")]
        [Description("POCO (Plain Old C# Objects), Inpc (Implements INotifyPropertyChanged), Prism (Prism base class), Records (readonly POCO)")]
        public CSharpClassStyle ClassStyle { get; set; }
    }
}