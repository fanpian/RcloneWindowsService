using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RcloneWindowsService
{
    /// <summary>
    /// 命令参数
    /// </summary>
    internal class CommandInfo
    {
        /// <summary>
        /// 需要执行的文件路径
        /// 默认为cmd.exe程序
        /// </summary>
        public string FileName { get; set; } = "cmd.exe";

        /// <summary>
        /// 程序参数
        /// </summary>
        public string Arguments { get;set; } = string.Empty;

        /// <summary>
        /// 程序启动后执行的命令
        /// </summary>
        public string? CmdText { get; set; }

        /// <summary>
        /// 命令在执行后是挂起，还是结束
        /// 默认在命令执行后结束命令
        /// </summary>
        public bool HangUp { get;set; } = false;

        /// <summary>
        /// 重试次数
        /// </summary>
        public int TryTime { get;set; } = 3;

        /// <summary>
        /// 已经重试的次数
        /// 在运行中使用
        /// </summary>
        public int AreadyTrTime { get; set; }

        /// <summary>
        /// 命令是否已经执行
        /// </summary>
        public bool IsExec { get; set; }
    }
}
