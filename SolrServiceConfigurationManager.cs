using System.Collections;
using System.Configuration;
using Topshelf.Configuration;
using Topshelf.Configuration.Dsl;

namespace SolrWindowsService
{
    public static class SolrServiceConfigurationManager
    {
        public static SolrServiceConfig GetSolrServiceConfiguration()
        {
            return SolrServiceConfig.FromSectionHashTable((Hashtable)ConfigurationManager.GetSection("solrService"));
        }
    }
}