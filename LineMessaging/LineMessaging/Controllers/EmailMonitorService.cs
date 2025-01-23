namespace LineMessaging.Controllers
{
    public class EmailMonitorService : IHostedService, IDisposable
    {
        private EmailMonitor _monitor;
        private readonly ILogger<EmailMonitorService> _logger;
        private readonly IConfiguration _configuration;

        public EmailMonitorService(
            ILogger<EmailMonitorService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _monitor = new EmailMonitor(
                host: "",
                port: int.Parse("1"),
                username: "",
                password:""
            );

            _monitor.NewEmailReceived += OnNewEmail;
            await _monitor.StartMonitoring(60);

            _logger.LogInformation("Email monitoring service started");
        }

        private void OnNewEmail(object sender, EmailEventArgs args)
        {
            _logger.LogInformation(
                "New email received from {From} with subject {Subject}",
                args.From,
                args.Subject
            );

         
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _monitor?.StopMonitoring();
            _logger.LogInformation("Email monitoring service stopped");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _monitor?.Dispose();
        }
    }

}
