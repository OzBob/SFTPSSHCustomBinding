using SFTPSSHBinding.Step1;
using SFTPSSHBinding.Step3;
using Microsoft.Azure.WebJobs;

namespace SFTPSSHBinding.Step4
{
    internal class SFTPSSHBindingConverter<T> : IConverter<SFTPSSHAttribute, IAsyncCollector<T>>
    {
        private readonly SFTPSSHBindingConfigProvider configProvider;

        public SFTPSSHBindingConverter(SFTPSSHBindingConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public IAsyncCollector<T> Convert(SFTPSSHAttribute attribute)
        {
            SFTPSSHBindingContext context = this.configProvider.CreateContext(attribute);
            return new SFTPSSHBindingAsyncCollector<T>(context);
        }
    }
}