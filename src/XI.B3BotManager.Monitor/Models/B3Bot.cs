using System;
using System.Diagnostics;
using Serilog;

namespace XI.B3BotManager.Monitor.Models
{
    internal class B3Bot
    {
        private readonly ILogger _logger;
        private B3BotConfig _config;
        private Process _process;

        public B3Bot(ILogger logger)
        {
            _logger = logger;
        }

        public void Configure(B3BotConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void CheckStatus()
        {
            _logger.Debug("[{Name}] Checking status of B3Bot", _config.BotTag);

            if (_process == null)
            {
                CreateProcessInstance();
                return;
            }

            _process?.Refresh();

            if (!_process.HasExited)
                return;

            _logger.Warning("[{Name}] Process has exited, will create a new one", _config.BotTag);
            CreateProcessInstance();
        }

        private void CreateProcessInstance()
        {
            try
            {
                _process?.Dispose();

                _logger.Information("[{Name}] Starting new instance of the B3Bot process", _config.BotTag);

                const string b3BotExe = "C:\\Applications\\BigBrotherBot\\b3.exe";
                _process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = b3BotExe,
                        Arguments = $"-c C:\\Applications\\BigBrotherBot\\conf\\{_config.ConfigName}",
                        UseShellExecute = true
                    }
                };

                _process.Start();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[{Name}] Failed to start process", _config.BotTag);
                _process = null;
            }
        }

        public void Kill()
        {
            _logger.Information("[{Name}] Killing B3Bot", _config.BotTag);

            if (_process == null) return;

            try
            {
                _process.Kill();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[{Name}] Error killing process", _config.BotTag);
                _process = null;
            }
        }
    }
}