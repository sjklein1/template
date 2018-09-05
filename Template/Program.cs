using System;
using System.IO;
using System.Reflection;
using System.Threading;
using log4net;
using log4net.Config;
using Upskill.Skylight.Client;
using Upskill.Skylight.Mqtt;


namespace Template
{
    static partial class Program
    {
        private static ApiClient _apiClient;                                        // API client for extension
        private static MessagingClient _messagingClient;                            // mqtt client for extension

        static void Main(string[] args)
        {
            {
                // configure log file
                XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(ConfigLogConfigFile));

                // start app
                Console.WriteLine("Starting");
                var app = new App();
                var success = app.StartAsync().Result;
                if (success)
                {
                    logger.Info("The app is still running...");
					SpinWait.SpinUntil(() => false);
                }
                else
                {
                    logger.Error("The app did not run successfully; the app is stopping...");
                    ThreadPool.QueueUserWorkItem(app.Stop);
                }
            }
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            logger.Error(e);
        }

    }
}