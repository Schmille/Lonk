using Lonk.Models;

namespace Lonk.Services
{
    public class CleanupService : IHostedService
    {
        private Timer Trigger;
        private readonly ILinkCleaner LinkCleaner;

        public CleanupService(ILinkCleaner linkCleaner)
        {
            LinkCleaner = linkCleaner;
            Trigger = new Timer(OnTimerTrigger, null, Timeout.InfiniteTimeSpan, TimeSpan.FromMinutes(1));
        }

        public async void OnTimerTrigger(object? state)
        {
            await LinkCleaner.CleanAsync(DateTime.Now.Subtract(TimeSpan.FromHours(12)));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Trigger.Change(TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Trigger.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            return Task.CompletedTask;
        }
    }
}
