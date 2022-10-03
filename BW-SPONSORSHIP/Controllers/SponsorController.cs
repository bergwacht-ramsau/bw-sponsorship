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
    private readonly MailAddress _mail;

    public SponsorController(ILogger<SponsorController> logger, IConfiguration config, SponsorContext context)
    {
        _logger = logger;
        _context = context;
        _smtpClient = new SmtpClient(config["SMTPSERVER"])
        {
            Port = 587,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(config["SMTPMAIL"], config["SMTPPASSWORD"]),
            EnableSsl = true,
        };
        _mail = new MailAddress(config["SMTPMAIL"]);
    }

    [HttpPost]
    public async Task<ActionResult<Sponsor>> PostSponsor(SponsorDTO sponsorDTO)
    {
        string UId = Guid.NewGuid().ToString();
        Sponsor sponsor = new Sponsor(sponsorDTO, UId);

        _context.Sponsors.Add(sponsor);
        await _context.SaveChangesAsync();

        var mailMessage = new MailMessage
        {
            From = _mail,
            Subject = "Neuer Spender",
            Body = "<h1>Test</h1>",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(sponsor.EMail);

        _smtpClient.Send(mailMessage);

        return CreatedAtAction(nameof(PostSponsor), sponsor);
    }

    [HttpGet("{UId}")]
    public async Task<ActionResult<Sponsor>> ValidateSponsor(string uid)
    {
        try
        {
            var sponsor = _context.Sponsors.Where(s => s.UId == uid).First();
            return sponsor;
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
    }
}
