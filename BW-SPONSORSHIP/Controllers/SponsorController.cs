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
        if(sponsor.Sepa){
            _context.Sponsors.Add(sponsor);
            await _context.SaveChangesAsync();
            SendWelcomeMailSepa(sponsor.EMail, sponsor.UId);
        }else{
            SendWelcomeMail(sponsor.EMail);
            
        }

        

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
           _context.Sponsors.Remove(sponsor);
           await _context.SaveChangesAsync();
            return BadRequest("Link abgelaufen");
        }
        SendInfoMail(sponsor, true);
        _context.Sponsors.Remove(sponsor);
        await _context.SaveChangesAsync();
        return "Erfolgreich validiert";
    }

    private void SendWelcomeMail(string email){
        var mailMessage = new MailMessage
        {
            From = _mail,
            Subject = "Neuer Spender",
            Body = "<h1>Test</h1>",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        _smtpClient.Send(mailMessage);
    }

    private void SendWelcomeMailSepa(string email, string validationUrl){
        var mailMessage = new MailMessage
        {
            From = _mail,
            Subject = "Neuer Spender",
            Body = "<h1>Test</h1>",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        _smtpClient.Send(mailMessage);
    }

    private void SendInfoMail(Sponsor sponsor, bool acceptedSepa = false){
        var mailMessage = new MailMessage
        {
            From = _mail,
            Subject = "Neuer Förderer",
            Body = sponsor.ToString() + "Sepa akzeptiert: " + (acceptedSepa ? DateTime.Now.ToString() : "Nein"),
            IsBodyHtml = false,
        };
        mailMessage.To.Add(_mail);

        _smtpClient.Send(mailMessage);
    }
}
