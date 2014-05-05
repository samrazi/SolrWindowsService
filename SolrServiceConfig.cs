using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SolrWindowsService
{
    static class HashtableExtension
    {
        public static T GetValueOrDefault<T>(this Hashtable ht, string key, T defaultValue)
        {
            if (!ht.ContainsKey(key)) return defaultValue;
            return (T)Convert.ChangeType(ht[key], typeof(T));
        }
    }
    public class SolrServiceConfig
    {
        private const string ARGS = "CommandLineArgs";
        private const string JAVAEXEC = "JavaExecutable";
        private const string WORKINGDIRECTORY = "WorkingDirectory";
        private const string SOLRHOME = "SolrHome";
        private const string PORT = "Port";
        private const string INSTANCENAME = "InstanceName";
        private const string SHOWCONSOLE = "ShowConsole";

        public static SolrServiceConfig FromSectionHashTable(Hashtable ht)
        {
            var workingDirectory = ht.GetValueOrDefault(WORKINGDIRECTORY, "");
            return new SolrServiceConfig
                {
                    JavaExecutable = ht.GetValueOrDefault(JAVAEXEC, @"C:\Program Files\Java\jre7\bin\java.exe"),
                    WorkingDirectory = workingDirectory,
                    JarFilePath = GetJarFilePath(workingDirectory),
                    SolrHome = ht.GetValueOrDefault(SOLRHOME, "solr"),
                    CommandLineArgs = ht.GetValueOrDefault(ARGS, ""),
                    Port = ht.GetValueOrDefault(PORT, 8389),
                    InstanceName = ht.GetValueOrDefault(INSTANCENAME, ""),
                    ShowConsole = ht.GetValueOrDefault(SHOWCONSOLE, false)
                };
        }

        public string JarFilePath { get; set; }
        public string JavaExecutable { get; set; }
        public string WorkingDirectory { get; set; }
        public string SolrHome { get; set; }
        public string CommandLineArgs { get; set; }
        public int Port { get; set; }
        public string InstanceName { get; set; }
        public bool ShowConsole { get; set; }

        public string GetprocessStartArguments()
        {
            var jarFilePath = GetJarFilePath(WorkingDirectory);
            return string.Format(@"-Dsolr.solr.home={0} -Djetty.port={3} {1} -jar {2}", SolrHome, CommandLineArgs,
                                 jarFilePath, Port);
        }

        private static string GetJarFilePath(string workingDirectory)
        {
            string jarFilePath = string.Format(@"{0}\start.jar", workingDirectory);
            if (!File.Exists(jarFilePath))
                throw new ConfigurationErrorsException("Couldn't find the start.jar file at " + jarFilePath);
            return jarFilePath;
        }
    }



}
