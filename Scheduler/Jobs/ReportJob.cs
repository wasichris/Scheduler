using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Scheduler.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class ReportJob : IJob
    {
        private readonly ILogger<ReportJob> _logger;

        private readonly IServiceProvider _provider;


        public ReportJob(ILogger<ReportJob> logger, IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public Task Execute(IJobExecutionContext context)
        {

            // 可取得自定義的 JobSchedule 資料, 可根據 JobSchedule 提供的內容建立不同 report 資料
            var schedule = context.JobDetail.JobDataMap.Get("Payload") as JobSchedule;
            var jobName = schedule.JobName;

            using (var scope = _provider.CreateScope())
            {
                // 如果要使用到 DI 容器中定義為 Scope 的物件實體時，由於 Job 定義為 singleton
                // 因此無法直接取得 Scope 的實體，此時就需要於 CreateScope 在 scope 中產生該實體
                // ex. var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
            }


            _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - job{jobName} - start");
            for (int i = 0; i < 5; i++)
            {

                // 自己定義當 job 要被迫被被中斷時，哪邊適合結束
                // 如果沒有設定，當作業被中斷時，並不會真的中斷，而會整個跑完
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                System.Threading.Thread.Sleep(1000);
                _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - job{jobName} - working{i}");

            }


            _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - job{jobName} - done");
            return Task.CompletedTask;
        }
    }
}
