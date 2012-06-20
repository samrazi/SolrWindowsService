using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Topshelf;
using Topshelf.Configuration.Dsl;
using Topshelf.HostConfigurators;

namespace SolrWindowsService
{
    static class Program
    {
        static void Main()
        {
            var config = SolrServiceConfigurationManager.GetSolrServiceConfiguration();
            var displayName = string.Format("{0}.Solr.Service", config.InstanceName);
            HostFactory.Run(x =>
            {
                x.Service<SolrService>(s =>
                {
                    s.SetServiceName("solr");
                    s.ConstructUsing(name => new SolrService());
                    s.WhenStarted(solr => solr.Start());
                    s.WhenStopped(solr => solr.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription(string.Format("Starts up {0}", displayName));
                x.SetDisplayName(displayName);
                x.SetServiceName(displayName);
                x.StartManually();
            });
        }
    }
}
