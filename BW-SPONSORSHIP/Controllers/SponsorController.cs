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
    public async Task<ActionResult<String>> ValidateSponsor(string uid)
    {
        var sponsor = await _context.Sponsors.FindAsync(uid);
        if (sponsor == null)
        {
            return NotFound();
        }
        if (DateTime.Compare(sponsor.created.AddHours(24), DateTime.Now) < 0){
            return BadRequest("Link abgelaufen");
        }
        var mailMessage = new MailMessage
        {
            From = _mail,
            Subject = "Neuer Förderer",
            Body = sponsor.ToString(),
            IsBodyHtml = false,
        };
        mailMessage.To.Add("info@bergwacht-ramsau.de");

        _smtpClient.Send(mailMessage);
        return "Erfolgreich validiert";
    }
}
