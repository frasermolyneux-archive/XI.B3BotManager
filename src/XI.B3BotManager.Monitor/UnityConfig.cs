using System;
using Serilog;
using Unity;
using Unity.Lifetime;

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
                .ReadFrom.AppSettings()
                .WriteTo.Console()
                .CreateLogger();
            Log.Logger = logger;

            container.RegisterFactory<ILogger>((ctr, type, name) => logger, new ContainerControlledLifetimeManager());
        }
    }
}