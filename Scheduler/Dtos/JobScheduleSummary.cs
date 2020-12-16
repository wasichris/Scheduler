using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Dtos
{
    /// <summary>
    /// Job排程中間物件 View
    /// </summary>
    public class JobScheduleSummary
    {

        /// <summary>
        /// Job識別名稱
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// Job類型
        /// </summary>
        /// 
        public string JobType { get; set; }

        /// <summary>
        /// Cron表示式
        /// </summary>
        /// 
        public string CronExpression { get; set; }

        /// <summary>
        /// Job狀態名
        /// </summary>
        /// 
        public string JobStatusName { get; set; }

        /// <summary>
        /// Job狀態碼
        /// </summary>
        /// 
        public JobStatus JobStatusId { get; set; }

    }

}
