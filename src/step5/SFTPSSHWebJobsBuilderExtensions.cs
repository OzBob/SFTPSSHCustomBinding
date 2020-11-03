using System;
using SFTPSSHBinding.Step2;
using SFTPSSHBinding.Step4;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace SFTPSSHBinding.Step5
{
    public static class SFTPSSHWebJobsBuilderExtensions
    {
        public static IWebJobsBuilder AddSFTPSSHV3(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            builder.AddExtension<SFTPSSHBindingConfigProvider>();

            builder.Services.AddSingleton<ISFTPSSHBindingCollectorFactory, SFTPSSHBindingCollectorFactory>();

            return builder;
        }
    }
}
