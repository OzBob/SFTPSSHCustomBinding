using System;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace SFTPSSHBinding.Step2
{
    public class SFTPSSHBindingCollectorFactory : ISFTPSSHBindingCollectorFactory
    {
        public CosmosClient CreateClient(
            string connectionString,
            string currentRegion = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(connectionString);
            if (!string.IsNullOrEmpty(currentRegion))
            {
                clientBuilder.WithApplicationRegion(currentRegion);
            }

            return clientBuilder.Build();
        }
    }
}
