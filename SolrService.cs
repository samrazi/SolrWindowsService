using System;
using System.Diagnostics;

namespace SolrWindowsService
{
    internal class SolrService
    {
        static readonly Process Process = new Process();

        public static void Start()
        {
            Log("starting service");
            try
            {
                var config = SolrServiceConfigurationManager.GetSolrServiceConfiguration();
                Process.StartInfo.FileName = config.JavaExecutable;
                Process.StartInfo.WorkingDirectory = config.WorkingDirectory;
                Process.StartInfo.Arguments = config.GetprocessStartArguments();
                Process.StartInfo.UseShellExecute = config.ShowConsole;
                Log(Process.ToString());
                var result = Process.Start();
                Log("result of batch start: " + result);
                //process.WaitForExit();
            }
            catch (Exception ex)
            {
                Log("An error occurred: " + ex.Message);
                throw;
            }
            
        }

        public static void Stop()
        {
            Log("stopping service");
            Process.Kill();
            Process.Dispose();
        }

        private static void Log(string message)
        {
            const string source = "SolrService";
            const string log = "Application";

            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, log);
            
            EventLog.WriteEntry(source, message);
        }
    }
}
