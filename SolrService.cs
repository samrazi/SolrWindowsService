using System;
using System.Diagnostics;

namespace SolrWindowsService
{
    internal class SolrService
    {
        static readonly Process process = new Process();

        public void Start()
        {
            Log("starting service");
            try
            {
                var config = SolrServiceConfigurationManager.GetSolrServiceConfiguration();
                process.StartInfo.FileName = config.JavaExecutable;
                process.StartInfo.WorkingDirectory = config.WorkingDirectory;
                process.StartInfo.Arguments = config.GetprocessStartArguments();
                process.StartInfo.UseShellExecute = config.ShowConsole;
                Log(process.ToString());
                var result = process.Start();
                Log("result of batch start: " + result);
                //process.WaitForExit();
            }
            catch (Exception ex)
            {
                Log("An error occurred: " + ex.Message);
                throw;
            }
            
        }

        public void Stop()
        {
            Log("stopping service");
            process.Kill();
            process.Dispose();
        }

        protected void Log(string message)
        {
            const string source = "SolrService";
            const string log = "Application";

            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, log);
            
            EventLog.WriteEntry(source, message);
        }
    }
}
