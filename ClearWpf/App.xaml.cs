using ClearWpf.Services;
using ClearWpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace ClearWpf
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;

        private static IHost __Host;

        public static IHost Host
        {
            get
            {
                if (__Host == null)
                    __Host = Program.CreateHostBuilder(Environment.GetCommandLineArgs())
                            .ConfigureLogging(logging =>
                            {
                                logging.AddFilter("System", LogLevel.Debug);
                                logging.AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Information);
                                logging.AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Trace);
                                logging.AddFile("Logs/log-{Date}.txt");

                            })
                            .Build();
                return __Host;
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
               .RegisterServices()
               .RegisterViewModels();
        public static string CurrentDirectory => IsDesignMode
          ? Path.GetDirectoryName(GetSourceCodePath())
          : Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;
    }
}
