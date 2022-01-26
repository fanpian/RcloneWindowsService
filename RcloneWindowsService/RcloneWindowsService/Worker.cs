using System.Diagnostics;

namespace RcloneWindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }

        /// <summary>
        /// Æô¶¯·þÎñ
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("³ÌÐòÆô¶¯");
            ExecuteCmd(Host.CmdConfig.Starting);
            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// Í£Ö¹¸¶
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("³ÌÐòÍ£Ö¹");
            foreach (Process p in Host.ProcessHangUps) 
            {
                p.Dispose();
            }
            ExecuteStopingCmd(Host.CmdConfig.Stoping);
            return base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// Ö´ÐÐÃüÁî
        /// </summary>
        /// <param name="commandInfos"></param>
        private void ExecuteCmd(IEnumerable<CommandInfo> commandInfos)
        {
            string? temp = Environment.GetEnvironmentVariable("path");
            foreach (CommandInfo cmd in commandInfos)
            {
                Process p = new Process();
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = string.IsNullOrWhiteSpace(cmd.FileName) ? "cmd.exe" : cmd.FileName;
                    startInfo.WorkingDirectory = "";
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardInput = true;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.CreateNoWindow = cmd.HangUp;
                    startInfo.Arguments = cmd.Arguments;
                    p.StartInfo = startInfo;
                    p.Start();
                    if (!string.IsNullOrWhiteSpace(cmd.CmdText)) 
                    {
                        p.StandardInput.WriteLine(cmd.CmdText);
                    }
                    cmd.AreadyTrTime += 1;
                    cmd.IsExec = true;
                    //string output = p.StandardOutput.ReadToEnd();
                    //_logger.LogInformation(output);
                }
                catch (Exception ex)
                {
                    p.Close();
                    p.Dispose();
                    _logger.LogError(ex, ex.Message);
                }
                finally 
                {
                    if (cmd.HangUp)
                    {
                        Host.ProcessHangUps.Add(p);   
                    }
                    else if(!p.HasExited)
                    {
                        p.Close();
                        p.Dispose();
                    }
                }
            }
        }

        private void ExecuteStopingCmd(IEnumerable<CommandInfo> commandInfos) 
        {
            foreach (CommandInfo cmd in commandInfos)
            {
                Process p = new Process();
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = string.IsNullOrWhiteSpace(cmd.FileName) ? "cmd.exe" : cmd.FileName;
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardInput = true;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.CreateNoWindow = cmd.HangUp;
                    startInfo.Arguments = cmd.Arguments;
                    p.StartInfo = startInfo;
                    p.Start();
                    if (!string.IsNullOrWhiteSpace(cmd.CmdText))
                    {
                        p.StandardInput.WriteLine(cmd.CmdText);
                    }
                    cmd.AreadyTrTime += 1;
                    cmd.IsExec = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                finally
                {
                    p.Close();
                    p.Dispose();
                }
            }
        }
    }
}