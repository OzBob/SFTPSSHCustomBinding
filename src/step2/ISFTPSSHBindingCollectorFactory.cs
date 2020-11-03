using System;
using Microsoft.Azure.Cosmos;

namespace SFTPSSHBinding.Step2
{
    public interface ISFTPSSHBindingCollectorFactory
    {
        CosmosClient CreateClient(
            string connectionString,
            string currentRegion = null);
    }
}
