using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Dtos
{
    /// <summary>
    /// Job執行狀態
    /// </summary>
    public enum JobStatus : byte
    {
        [Description("初始化")]
        Init = 0,
        [Description("已排程")]
        Scheduled = 1,
        [Description("執行中")]
        Running = 2,
        [Description("已停止")]
        Stopped = 3,
    }
}
