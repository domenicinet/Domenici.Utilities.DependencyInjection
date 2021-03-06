﻿using System;
using Newtonsoft.Json;

namespace Domenici.Utilities.DependencyInjection.Plugins
{
    public class AssemblySignatureItem
    {
        [JsonProperty("key"), JsonRequired]
        public string Key { get; protected set; }

        [JsonProperty("signature")]
        public string Signature { get; protected set; }

        [JsonProperty("namespace")]
        public string Namespace { get; protected set; }

        [JsonProperty("path")]
        public string Path { get; protected set; }
    }
}