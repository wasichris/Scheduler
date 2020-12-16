using Microsoft.AspNetCore.SignalR;
using Scheduler.Dtos;
using Scheduler.Extensions;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler
{
    public class SchedulerHub : Hub
    {
        private QuartzHostedService _quartzHostedService;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="quartzHostedService">Quartz排程服務</param>
        public SchedulerHub (QuartzHostedService quartzHostedService)
        {
            _quartzHostedService = quartzHostedService;
        }

        /// <summary>
        /// 要求取得Job狀態
        /// </summary>
        public async Task RequestJobStatus()
        {
            if (Clients != null)
            {
                var jobs = await _quartzHostedService.GetJobSchedules();
                var jobSummary = jobs.Select(e => 
                        new JobScheduleSummary { 
                            JobName = e.JobName, 
                            CronExpression = e.CronExpression, 
                            JobStatusName = e.JobStatus.GetDescription(), 
                            JobStatusId = e.JobStatus, 
                            JobType = e.JobType.FullName 
                        }
                    );

                await Clients.Caller.SendAsync("ReceiveJobStatus", jobSummary);
            }
        }

        /// <summary>
        /// 通知Job狀態改變
        /// </summary>
        public async Task NotifyJobStatusChange()
        {
            if (Clients != null)
            {
                await Clients.Caller.SendAsync("JobStatusChange");
            }
        }

        /// <summary>
        /// 手動觸發Job執行
        /// </summary>
        public async Task TriggerJob(string jobName)
        {
            await _quartzHostedService.TriggerJobAsync(jobName);
        }

        /// <summary>
        /// 手動中斷Job執行
        /// </summary>
        public async Task InterruptJob(string jobName)
        {
            await _quartzHostedService.InterruptJobAsync(jobName);
        }

        
        /// <summary>
        /// 開啟排程器
        /// </summary>
        public async Task StartScheduler()
        {
            await _quartzHostedService.StartAsync(_quartzHostedService.CancellationToken);
        }

        /// <summary>
        /// 關閉排程器
        /// </summary>
        public async Task StopScheduler()
        {
            await _quartzHostedService.StopAsync(_quartzHostedService.CancellationToken);
        }


        /// <summary>
        /// 用戶連線事件
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await NotifyJobStatusChange();
            await base.OnConnectedAsync();
        }


        /// <summary>
        /// 用戶斷線事件
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
