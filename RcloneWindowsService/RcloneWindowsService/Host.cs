using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RcloneWindowsService
{
    /// <summary>
    /// 项目全局管理入口
    /// </summary>
    internal static class Host
    {

        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 命令配置
        /// </summary>
        public static CmdConfig CmdConfig { get; set; } = new CmdConfig();

        /// <summary>
        /// 已经挂起的进程
        /// </summary>
        public static List<Process> ProcessHangUps { get; set; } = new List<Process>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static void Init()
        {
            Read();
        }

        private static void Read()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "cmd.json");
            if (!File.Exists(filePath))
            {
                throw new Exception($"配置文件不存在:{filePath}");
            }
            string json = File.ReadAllText(filePath);
            CmdConfig? cmdConfig = JsonConvert.DeserializeObject<CmdConfig>(json);
            if (cmdConfig == null)
            {
                throw new Exception("配置文件格式不正确!");
            }
            CmdConfig = cmdConfig;
        }
    }
}
