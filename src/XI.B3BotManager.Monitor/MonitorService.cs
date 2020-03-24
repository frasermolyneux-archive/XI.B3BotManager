using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Serilog;
using XI.B3BotManager.Monitor.Configuration;
using XI.B3BotManager.Monitor.Factory;
using XI.B3BotManager.Monitor.Models;

namespace XI.B3BotManager.Monitor
{
    internal class MonitorService
    {
        private readonly IB3BotConfiguration _b3BotConfiguration;
        private readonly IB3BotFactory _b3BotFactory;
        private readonly ILogger _logger;
        private CancellationTokenSource _cts;

        public MonitorService(ILogger logger, IB3BotConfiguration b3BotConfiguration, IB3BotFactory b3BotFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _b3BotConfiguration = b3BotConfiguration ?? throw new ArgumentNullException(nameof(b3BotConfiguration));
            _b3BotFactory = b3BotFactory ?? throw new ArgumentNullException(nameof(b3BotFactory));
        }

        private List<B3Bot> B3Bots { get; } = new List<B3Bot>();

        public void Start()
        {
            _cts = new CancellationTokenSource();

            KillAllOrphanProcesses();

            foreach (var configFile in _b3BotConfiguration.GetConfigurations())
                B3Bots.Add(_b3BotFactory.CreateInstance(configFile));

            while (!_cts.IsCancellationRequested)
                foreach (var b3Bot in B3Bots.TakeWhile(b3Bot => !_cts.IsCancellationRequested))
                {
                    try
                    {
                        b3Bot.CheckStatus();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "[{Name}] Top-level error checking status of bot", b3Bot.BotTag);
                    }
                    
                    Thread.Sleep(5000);
                }
        }

        public void Stop()
        {
            _cts?.Cancel();

            foreach (var b3Bot in B3Bots) b3Bot.Kill();
        }

        public void Shutdown()
        {
            _cts?.Cancel();

            foreach (var b3Bot in B3Bots) b3Bot.Kill();
        }

        private void KillAllOrphanProcesses()
        {
            var processes = Process.GetProcessesByName("b3");

            foreach (var process in processes) process.Kill();
        }
    }
}