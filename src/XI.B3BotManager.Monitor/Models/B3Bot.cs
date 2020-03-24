using System;
using System.Diagnostics;
using System.IO;
using Serilog;

namespace XI.B3BotManager.Monitor.Models
{
    internal class B3Bot
    {
        private const string InstallPath = "C:\\Applications\\BigBrotherBot";
        private readonly ILogger _logger;
        private B3BotConfig _config;
        private Process _process;

        public B3Bot(ILogger logger)
        {
            _logger = logger;
        }

        public string BotTag => _config.BotTag;

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

            if (_process.HasExited)
            {
                _logger.Warning("[{Name}] Process has exited, will create a new one", _config.BotTag);
                CreateProcessInstance();
                return;
            }

            CheckLogFileStatus();
        }

        private void CheckLogFileStatus()
        {
            try
            {
                var logPath = $"{InstallPath}\\logs\\{_config.LogName}";
                if (!File.Exists(logPath))
                {
                    _logger.Warning("[{Name}] Log file does not exist at {logPath}", _config.BotTag, logPath);
                    return;
                }

                string data;
                using (var fileStream = new FileStream(logPath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite))
                {
                    using var streamReader = new StreamReader(fileStream);
                    data = streamReader.ReadToEnd();
                }

                if (!data.Contains("MySQL server has gone away")) return;

                _logger.Warning("[{Name}] MySQL found in log file, killing process", _config.BotTag);
                Kill();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[{Name}] Error checking status of log file", _config.BotTag);
            }
        }

        private void CreateProcessInstance()
        {
            try
            {
                _process?.Dispose();

                _logger.Information("[{Name}] Starting new instance of the B3Bot process", _config.BotTag);

                _process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = $"{InstallPath}\\b3.exe",
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