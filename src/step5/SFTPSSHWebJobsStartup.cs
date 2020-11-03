using SFTPSSHBinding.Step5;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Hosting;

[assembly: WebJobsStartup(typeof(SFTPSSHWebJobsStartup))]

namespace SFTPSSHBinding.Step5
{
    public class SFTPSSHWebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddSFTPSSHV3();
        }
    }
}