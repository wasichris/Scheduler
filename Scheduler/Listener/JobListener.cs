using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Listener
{
    /// <summary>
    /// Job 監聽器
    /// </summary>
    public class JobListener : IJobListener
    {

        private readonly ILogger<JobListener> _logger;

        private readonly IServiceProvider _serviceProvider = null;  // 要用這個來產生 schedulerHub 實體，避免直接注入造成循環參考



        string IJobListener.Name => "Jobs Listener";



        public JobListener(ILogger<JobListener> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }



        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            // 工作將被執行 (目前Job狀態尚未進入Executing清單)

            var jobName = context.JobDetail.Key.Name;
            _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - job{jobName} - JobToBeExecuted");

            var schedulerHub = _serviceProvider.GetRequiredService<SchedulerHub>();
            await schedulerHub.NotifyJobStatusChange();
         
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            // 工作執行完畢 (目前Job狀態尚未移出Executing清單)

            var jobName = context.JobDetail.Key.Name;
            _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - job{jobName} - JobWasExecuted");


            var schedulerHub = _serviceProvider.GetRequiredService<SchedulerHub>();
            await schedulerHub.NotifyJobStatusChange();

        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

    }
}
