using log4net;
using System;
using System.Reflection;
using Upskill.Skylight.Client;
using Upskill.Skylight.Mqtt;

namespace Template
{
        static partial class Program
        {
            // Skylight group
            private static string ConfigWorkerGroup => _config["Group"];

            // Log file config
            private static string ConfigLogConfigFile => _config["LogConfigFile"];

            // URL for Skylight API
            private static string ApiUrl => _config["ApiUrl"];

            // URL for Skylight MQTT
            private static string MqttUrl => _config["MqttUrl"];

            // Log file
            private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            // Config settings
            private static System.Collections.Specialized.NameValueCollection _config = System.Configuration.ConfigurationManager.AppSettings;

            /// <summary>
            /// Configure extension
            /// </summary>
            /// <returns>true if extension authenticates to server; false if there is an error</returns>
            private static Boolean ConfigureExtension()
            {
                var config = _config;
                string username = config["Username"];
                string password = config["Password"];
                string userId = config["UserId"];
                string domain = config["Domain"];

                var connectionInfo = new ConnectionInfo(username, password, domain, ApiUrl, MqttUrl);
                var authenticationManager = new ApiAuthenticationManager(connectionInfo);

                _apiClient = new ApiClient(authenticationManager);
                _messagingClient = new MessagingClient(authenticationManager);

                var success = _messagingClient.StartListeningAsync().Result;
                if (!success)
                {
                    logger.Error("Failed to connect to mqtt");
                    return false;
                }

                return true;
            }
        }
    }
