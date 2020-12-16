using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Scheduler.Dtos;
using Scheduler.Factorys;
using Scheduler.Jobs;
using Scheduler.Listener;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();


            //向DI容器註冊Quartz服務
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<IJobListener, JobListener>();
            services.AddSingleton<ISchedulerListener, SchedulerListener>();

            //向DI容器註冊Job
            services.AddSingleton<ReportJob>();

            //向DI容器註冊JobSchedule
            services.AddSingleton(new JobSchedule(jobName: "111", jobType: typeof(ReportJob), cronExpression: "0/30 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobName: "222", jobType: typeof(ReportJob), cronExpression: "0/52 * * * * ?"));

            //向DI容器註冊Host服務
            services.AddSingleton<QuartzHostedService>();
            services.AddHostedService(provider => provider.GetService<QuartzHostedService>());

            // 想用 ASP.NET 執行定期排程，有一個前題是「需確保網站永遠處於執行狀態」
            // 要注意在IIS會自動收回應用程式的執行而造成 scheduler 中斷
            // 這時候要設定 Preload Enable = true, Start Mode = AlwaysRunning
            // https://docs.microsoft.com/zh-tw/archive/blogs/vijaysk/iis-8-whats-new-website-settings
            // https://docs.microsoft.com/zh-tw/archive/blogs/vijaysk/iis-8-whats-new-application-pool-settings
            // https://blog.darkthread.net/blog/hangfire-recurringjob-notes/


            // 註冊DB容器schedulerHub實體
            services.AddSingleton<SchedulerHub>();

            // 設定 SignalR 服務
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // 設定 signalR 的 router
                endpoints.MapHub<SchedulerHub>("/schedulerHub");
            });


         
        }
    }
}
