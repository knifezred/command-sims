using CommandSims.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Service
{
    public class GameService : BackgroundService
    {
        private Timer _timer;

        private readonly ILogger _logger;

        public GameService(ILogger<GameService> logger, IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            applicationLifetime.ApplicationStarted.Register(OnStarted);
            applicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Sims.StartInit();
            ExecuteAsync(cancellationToken);
            return Task.CompletedTask;
        }


        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("GameService started");
        }

        private void OnStopped()
        {
            _logger.LogInformation("GameService stopped");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _timer = new Timer(TimeWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));
            }, stoppingToken);
        }

        private void TimeWork(object? state)
        {
            // 释放timmer
            _timer.Dispose();
            // 加载Login页面
            UI.LoadStartPanel();
            // 释放UI
            UI.SetFree();
            Sims.Game.ReadCommand(Console.ReadLine(), 1);
        }
    }
}
