using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Topshelf;
using Topshelf.Configuration.Dsl;

namespace SolrWindowsService
{
    static class Program
    {
        static void Main(string[] args)
        {
            var config = SolrServiceConfigurationManager.GetSolrServiceConfiguration();
            var displayName = string.Format("{0}.Solr.Service", config.InstanceName);
            var topShelfConfig = RunnerConfigurator.New(x =>
                {
                    x.ConfigureService<SolrService>(s =>
                        {
                            s.Named("solr");
                            s.HowToBuildService(name => new SolrService());
                            s.WhenStarted(solr => solr.Start());
                            s.WhenStopped(solr => solr.Stop());
                        });
                    x.RunAsLocalSystem();
                    x.SetDescription(string.Format("Starts up {0}", displayName));
                    x.SetDisplayName(displayName);
                    x.SetServiceName(displayName);
                });
            Runner.Host(topShelfConfig, args);
        }
    }
}
