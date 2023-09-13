using CommandSims.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace CommandSims.Service
{
    public class TimeService : BackgroundService
    {
        private Timer _timer;

        private readonly ILogger _logger;

        public TimeService(ILogger<TimeService> logger, IHostApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStarted.Register(OnStarted);
            applicationLifetime.ApplicationStopped.Register(OnStopped);
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            ExecuteAsync(cancellationToken);
            return Task.CompletedTask;
        }


        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("TimeService started");
        }

        private void OnStopped()
        {
            _logger.LogInformation("TimeService stopped");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _timer = new Timer(TimeWork, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30));
            }, stoppingToken);
        }

        private void TimeWork(object? state)
        {
            if (!UI.IsBusy())
            {
                // 数据流转，不做任何UI操作，需玩家自信触发下一步操作，防止与ReadCommand冲突
                UI.PrintGrayLine("有什么事情发生了... [blue]look[/]查看详情");

            }
        }
    }
}
