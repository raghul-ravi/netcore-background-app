using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SampleBackgroundApp
{
    public class BackgroundScheduledTask : IHostedService, IDisposable
    {
        private bool _stopping;
        private Task _backgroundTask;
        private TimeSpan _interval;

        public BackgroundScheduledTask()
        {
            _interval = TimeSpan.FromMinutes(1);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Scheduled Task Initialized - Will run every {_interval.Minutes} minute");
            _backgroundTask = BackgroundTask();
            return Task.CompletedTask;
        }

        private async Task BackgroundTask()
        {
            while (!_stopping)
            {
                await Task.Delay(_interval);
                Console.WriteLine($"Scheduled Task Executed at {DateTime.UtcNow}");
            }

            Console.WriteLine("Scheduled Background task is stopping.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Scheduled Task Stop Initiated.");
            _stopping = true;
            if (_backgroundTask != null)
            {
                // TODO: cancellation
                await _backgroundTask;
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Scheduled Task Resources Disposed");
        }
    }
}
