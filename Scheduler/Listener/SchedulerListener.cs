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
    public class SchedulerListener : ISchedulerListener
    {
        private readonly ILogger<SchedulerListener> _logger;

        private readonly IServiceProvider _serviceProvider = null;  // 要用這個來產生 schedulerHub 實體，避免直接注入造成循環參考



        public SchedulerListener(ILogger<SchedulerListener> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }



        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - SchedulerShutdown");
            var schedulerHub = _serviceProvider.GetRequiredService<SchedulerHub>();
            await schedulerHub.NotifyJobStatusChange();
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - SchedulerShuttingdown");
            var schedulerHub = _serviceProvider.GetRequiredService<SchedulerHub>();
            await schedulerHub.NotifyJobStatusChange();
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - SchedulerStarted");
            var schedulerHub = _serviceProvider.GetRequiredService<SchedulerHub>();
            await schedulerHub.NotifyJobStatusChange();
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"@{DateTime.Now:HH:mm:ss} - SchedulerStarting");
            var schedulerHub = _serviceProvider.GetRequiredService<SchedulerHub>();
            await schedulerHub.NotifyJobStatusChange();
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
