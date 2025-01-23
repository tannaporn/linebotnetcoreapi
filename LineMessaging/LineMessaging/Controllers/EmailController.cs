using Microsoft.AspNetCore.Mvc;

namespace LineMessaging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly EmailMonitorService _emailService;

        public EmailController(
            ILogger<EmailController> logger,
            EmailMonitorService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            // Add logic to check monitoring status
            return Ok(new { status = "Email monitoring is active" });
        }
    }
}
