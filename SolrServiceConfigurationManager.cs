using System.Collections;
using System.Configuration;

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