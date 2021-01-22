using AlbedoTeam.Sdk.JobWorker;

namespace AlbedoTeam.Accounts.Business
{
    internal static class Program
    {
        private static void Main()
        {
            Worker.Configure<Startup>().Run();
        }
    }
}