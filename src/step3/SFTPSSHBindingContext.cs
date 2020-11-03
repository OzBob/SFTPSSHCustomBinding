using SFTPSSHBinding.Step1;
using Microsoft.Azure.Cosmos;

namespace SFTPSSHBinding.Step3
{
    public class SFTPSSHBindingContext
    {
        public SFTPSSHAttribute ResolvedAttribute { get; set; }

        public CosmosClient CosmosClient { get; set; }
    }
}
