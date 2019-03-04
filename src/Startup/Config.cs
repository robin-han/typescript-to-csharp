﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GrapeCity.CodeAnalysis.TypeScript.Converter
{
    class Config
    {
        private static readonly string DEAULT_CONFIGFILE_NAME = "tscconfig.json";

        public Config()
        {
            this.Init();
        }

        private void Init()
        {
            this.Include = new List<string>();
            this.Exclude = new List<string>();
            this.Output = string.Empty;
            this.FlatOutput = false;

            this.Namespace = string.Empty;
            this.Usings = new List<string>();
            this.NamespaceMappings = new List<string>();
        }

        public List<string> Include
        {
            get;
            private set;
        }


        public List<string> Exclude
        {
            get;
            private set;
        }

        public string Output
        {
            get;
            private set;
        }

        public bool FlatOutput
        {
            get;
            private set;
        }

        public string Namespace
        {
            get;
            private set;
        }

        public List<string> Usings
        {
            get;
            private set;
        }

        public List<string> NamespaceMappings
        {
            get;
            private set;
        }

        public string Read()
        {
            return Read(DEAULT_CONFIGFILE_NAME);
        }
        public string Read(string configPath)
        {
            return this.ReadConfigFile(configPath);
        }

        private string ReadConfigFile(string configFile)
        {
            if (!File.Exists(configFile))
            {
                return string.Format("Cannot find config file {0}", configFile);
            }

            JObject jsonConfig = JObject.Parse(File.ReadAllText(configFile));
            this.Init();

            //output
            JToken jsonOutput = jsonConfig["output"];
            if (jsonOutput != null)
            {
                this.Output = jsonOutput.ToObject<string>();
            }

            //flatOutput
            JToken jsonFlatten = jsonConfig["flatOutput"];
            if (jsonFlatten != null)
            {
                this.FlatOutput = jsonFlatten.ToObject<bool>();
            }

            //namesapce
            JToken jsonNamespace = jsonConfig["namespace"];
            if (jsonNamespace != null)
            {
                this.Namespace = jsonNamespace.ToObject<string>();
            }

            //usings
            JToken jsonUsings = jsonConfig["usings"];
            if (jsonUsings != null)
            {
                foreach (JToken item in jsonUsings)
                {
                    this.Usings.Add(item.ToObject<string>());
                }
            }

            //namesapce mappings
            JToken jsonNSMappings = jsonConfig["namespaceMappings"];
            if (jsonNSMappings != null)
            {
                foreach (JToken item in jsonNSMappings)
                {
                    this.NamespaceMappings.Add(item.ToObject<string>());
                }
            }

            //exclude
            JToken jsonExclude = jsonConfig["exclude"];
            if (jsonExclude != null)
            {
                foreach (JToken item in jsonExclude)
                {
                    this.Exclude.Add(item.ToObject<string>());
                }
            }

            //include
            JToken jsonInclude = jsonConfig["include"];
            if (jsonInclude != null)
            {
                foreach (JToken item in jsonInclude)
                {
                    string include = item.ToObject<string>();
                    List<string> files = Utils.GetTsJsonFiles(include);
                    if (files == null)
                    {
                        return string.Format("Cannot find include file or directory {0}", include);
                    }
                    this.Include.AddRange(files);
                }
            }

            return string.Empty;
        }

    }
}