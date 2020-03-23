using System;
using Serilog;
using Unity;
using Unity.Lifetime;
using XI.B3BotManager.Monitor.Configuration;
using XI.B3BotManager.Monitor.Factory;

namespace XI.B3BotManager.Monitor
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> container =
            new Lazy<IUnityContainer>(() =>
            {
                var container = new UnityContainer();
                RegisterTypes(container);
                return container;
            });

        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            Log.Logger = logger;

            container.RegisterFactory<ILogger>((ctr, type, name) => logger, new ContainerControlledLifetimeManager());

            container.RegisterType<IB3BotConfiguration, B3BotConfiguration>();
            container.RegisterType<IB3BotFactory, B3BotFactory>();
        }
    }
}