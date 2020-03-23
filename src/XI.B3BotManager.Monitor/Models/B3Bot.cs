using System;
using System.Diagnostics;
using Serilog;

namespace XI.B3BotManager.Monitor.Models
{
    internal class B3Bot
    {
        private readonly ILogger _logger;
        private string _configurationFile;
        private Process _process;

        public B3Bot(ILogger logger)
        {
            _logger = logger;
        }

        public string Name
        {
            get
            {
                var parts = _configurationFile.Split("_");
                return $"{parts[0]}{parts[1]}";
            }
        }

        public void Configure(string configurationFile)
        {
            _configurationFile = configurationFile;
        }

        public void CheckStatus()
        {
            _logger.Debug("[{Name}] Checking status of B3Bot", Name);

            if (_process == null)
            {
                CreateProcessInstance();
                return;
            }

            _process?.Refresh();

            if (!_process.HasExited)
                return;

            _logger.Warning("[{Name}] Process has exited, will create a new one", Name);
            CreateProcessInstance();
        }

        private void CreateProcessInstance()
        {
            try
            {
                _process?.Dispose();

                _logger.Information("[{Name}] Starting new instance of the B3Bot process", Name);

                const string b3BotExe = "C:\\Applications\\BigBrotherBot\\b3.exe";
                _process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = b3BotExe,
                        Arguments = $"-c C:\\Applications\\BigBrotherBot\\conf\\{_configurationFile}",
                        UseShellExecute = true
                    }
                };

                _process.Start();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[{Name}] Failed to start process", Name);
                _process = null;
            }
        }

        public void Kill()
        {
            _logger.Information("[{Name}] Killing B3Bot", Name);

            if (_process == null) return;

            try
            {
                _process.Kill();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "[{Name}] Error killing process", Name);
                _process = null;
            }
        }
    }
}