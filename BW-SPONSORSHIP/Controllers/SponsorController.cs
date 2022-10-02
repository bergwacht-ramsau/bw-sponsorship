using Microsoft.AspNetCore.Mvc;
using BW_SPONSORSHIP.Models;
using System.Net.Mail;
using System.Net;

namespace BW_SPONSORSHIP.Controllers;

[ApiController]
[Route("[controller]")]
public class SponsorController : ControllerBase
{

    private readonly ILogger<SponsorController> _logger;
    private readonly SponsorContext _context;
    private readonly SmtpClient _smtpClient;

    public SponsorController(ILogger<SponsorController> logger, SponsorContext context)
    {
        _logger = logger;
        _context = context;
        _smtpClient = new SmtpClient("smtp.office365.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("", ""),
            EnableSsl = true,
        };
    }

    [HttpPost]
    public async Task<ActionResult<Sponsor>> PostSponsor(Sponsor sponsor)
    {
        _context.Sponsors.Add(sponsor);
        await _context.SaveChangesAsync();

        var mailMessage = new MailMessage
        {
            From = new MailAddress(""),
            Subject = "Neuer Spender",
            Body = "<h1>Test</h1>",
            IsBodyHtml = true,
        };
        mailMessage.To.Add("");

        _smtpClient.Send(mailMessage);

        return CreatedAtAction(nameof(PostSponsor), sponsor);
    }
}
