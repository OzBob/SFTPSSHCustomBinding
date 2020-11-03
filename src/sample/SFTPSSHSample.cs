using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SFTPSSHBinding.Step1;
using System.Threading.Tasks;

namespace SFTPSSHBinding.Sample
{
    public static class SFTPSSHSample
    {
        [FunctionName("SFTPSSHSample")]
        public static async Task Run(
            [TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, 
            [SFTPSSH("%SFTPSSHDatabase%", "%SFTPSSHContainer%", ConnectionStringSetting = "SFTPSSHConnectionString")] IAsyncCollector<MyClass> SFTPSSHCollector,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Create some random item
            MyClass item = new MyClass()
            {
                id = Guid.NewGuid().ToString(),
                SomeData = "some random data"
            };

            // Send it to the collector
            await SFTPSSHCollector.AddAsync(item);
        }
    }

    public class MyClass
    {
        public string id { get; set; }
        public string SomeData { get; set; }
    }
}
