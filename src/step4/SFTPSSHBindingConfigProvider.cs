using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SFTPSSHBinding.Step1;
using SFTPSSHBinding.Step2;
using SFTPSSHBinding.Step3;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;

namespace SFTPSSHBinding.Step4
{
    [Extension("SFTPSSHV3")]
    public class SFTPSSHBindingConfigProvider : IExtensionConfigProvider
    {
        private readonly ISFTPSSHBindingCollectorFactory cosmosBindingCollectorFactory;

        private ConcurrentDictionary<string, CosmosClient> CollectorCache { get; } = new ConcurrentDictionary<string, CosmosClient>();

        public SFTPSSHBindingConfigProvider(ISFTPSSHBindingCollectorFactory cosmosBindingCollectorFactory)
        {
            this.cosmosBindingCollectorFactory = cosmosBindingCollectorFactory;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var rule = context.AddBindingRule<SFTPSSHAttribute>();
            rule.AddValidator(ValidateConnection);
            rule.BindToCollector<SFTPSSHBindingOpenType>(typeof(SFTPSSHBindingConverter<>), this);
        }

        internal SFTPSSHBindingContext CreateContext(SFTPSSHAttribute attribute)
        {
            CosmosClient client = GetService(attribute.ConnectionStringSetting, attribute.CurrentRegion);

            return new SFTPSSHBindingContext
            {
                CosmosClient = client,
                ResolvedAttribute = attribute,
            };
        }

        private CosmosClient GetService(string connectionString, string region)
        {
            string cacheKey = BuildCacheKey(connectionString, region);
            return CollectorCache.GetOrAdd(cacheKey, (c) => this.cosmosBindingCollectorFactory.CreateClient(connectionString, region));
        }

        internal void ValidateConnection(SFTPSSHAttribute attribute, Type paramType)
        {
            if (string.IsNullOrEmpty(attribute.ConnectionStringSetting))
            {
                string attributeProperty = $"{nameof(SFTPSSHAttribute)}.{nameof(SFTPSSHAttribute.ConnectionStringSetting)}";
                throw new InvalidOperationException(
                    $"The SFTPSSH connection string must be set via the {attributeProperty} property.");
            }
        }

        private static string BuildCacheKey(string connectionString, string region) => $"{connectionString}|{region}";

        private class SFTPSSHBindingOpenType : OpenType.Poco
        {
            public override bool IsMatch(Type type, OpenTypeMatchContext context)
            {
                if (type.IsGenericType
                    && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return false;
                }

                if (type.FullName == "System.Object")
                {
                    return true;
                }

                return base.IsMatch(type, context);
            }
        }
    }
}
