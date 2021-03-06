using AlbedoTeam.Sdk.JobWorker;

namespace Accounts.Business
{
    internal static class Program
    {
        private static void Main()
        {
            Worker.Configure<Startup>().Run();
        }
    }
}