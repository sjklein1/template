using System;
using System.Threading.Tasks;
namespace Template
{
    public partial class Program
    {
        internal class App
        {
            /// <summary>
            /// Stop the app
            /// </summary>
            /// <param name="state"></param>
            public void Stop(object state)
            {
                logger.Info("All threads stopped, exiting..");
                Environment.Exit(0);
            }

            /// <summary>
            /// Start the app
            /// </summary>
            /// <returns>true if the app successfully started, otherwise false</returns>
            public async Task<bool> StartAsync()
            {
                try
                {
                    logger.Info("Starting..");

                    try
                    {
                        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
                        Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                        // configure extension
                        var success = ConfigureExtension();
                        if (!success)
                        {
                            logger.Error("Error configuring extension");
                            return false;
                        }

                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                        return false;
                    }

                    return true;
                }
                catch (Exception e)
                {
                    logger.Error(e);
                    return false;
                }
            }
        }
    }
}