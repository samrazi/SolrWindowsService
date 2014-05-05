using Topshelf;

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
                x.SetServiceName("solr");
                x.Service<SolrService>(s =>
                {
                    s.ConstructUsing(name => new SolrService());
                    s.WhenStarted(solr => SolrService.Start());
                    s.WhenStopped(solr => SolrService.Stop());
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
