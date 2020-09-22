using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace EBook.Core.Infrastructure
{
    public class AppSettingConfig
    {
        private static string AppSettingValue([CallerMemberName] string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string DefaultReturnUrl => AppSettingValue();

        public static string BaseAddress => AppSettingValue();

        public static string EBookConnectionString => AppSettingValue();

        public static string TransportType => AppSettingValue();

        public static string BaseQueueName => AppSettingValue();

        public static string RabbitMqBaseUri => AppSettingValue();

        public static string RabbitMqUserName => AppSettingValue();

        public static string RabbitMqUserPassword => AppSettingValue();

        public static string RabbitMqIdentityHeaderKey => AppSettingValue();

        public static string ViewDomain => AppSettingValue();

        public static string CookieDomain => AppSettingValue();

        public static bool RedisCachingEnabled
        {
            get
            {
                bool.TryParse(AppSettingValue(), out var redisCachingEnabled);

                return redisCachingEnabled;
            }
        }

        public static string SeqSinkServerAddress => AppSettingValue();

        public static string AuthorizeEndpointPath => AppSettingValue();

        public static string AuthorizationServerAddress => AppSettingValue();

        public static string ClientID => AppSettingValue();

        public static string ClientSecret => AppSettingValue();

        public static string RedirectUri => AppSettingValue();

        public static string BearerToken => AppSettingValue();

        public static string AttachmentBasePath => AppSettingValue();

        public static bool SslRequirement => Convert.ToBoolean(AppSettingValue());

        public static string IMReference => AppSettingValue();

        public static string RequestAuthorizationCodeAddress => AuthorizationServerAddress + AuthorizeEndpointPath + "?client_id=" + ClientID + "&redirect_uri=" + RedirectUri + "&response_type=code";
    }
}

