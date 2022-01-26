using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RcloneWindowsService
{
    /// <summary>
    /// Cmd命令配置
    /// </summary>
    internal class CmdConfig
    {
        /// <summary>
        /// 程序开始运行的时候执行的命令
        /// </summary>
        public List<CommandInfo> Starting { get; set; } = new List<CommandInfo>();

        /// <summary>
        /// 程序在运行过程中执行的命令
        /// </summary>
        public List<CommandInfo> Runing { get; set; } = new List<CommandInfo>();

        /// <summary>
        /// 程序停止运行的时候执行的命令
        /// </summary>
        public List<CommandInfo> Stoping { get; set; } = new List<CommandInfo>();
    }
}
