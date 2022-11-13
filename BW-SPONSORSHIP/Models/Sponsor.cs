using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BW_SPONSORSHIP.Models
{
    public class Sponsor
    {
        [Key]
        public string UId { get; set; }
        public DateTime created { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string Bank { get; set; }
        public string IBAN { get; set; }
        public bool Sepa { get; set; }

        public Sponsor()
        {
            //Needed for EF
            this.Name = "";
            this.Surname = "";
            this.Street = "";
            this.PostalCode = "";
            this.City = "";
            this.Phone = "";
            this.EMail = "";
            this.Bank = "";
            this.IBAN = "";
            this.UId = "";
        }

        public Sponsor(SponsorDTO sponsorDTO, string UId)
        {
            this.Name = sponsorDTO.Name;
            this.Surname = sponsorDTO.Surname;
            this.DateOfBirth = sponsorDTO.DateOfBirth;
            this.Street = sponsorDTO.Street;
            this.PostalCode = sponsorDTO.PostalCode;
            this.City = sponsorDTO.City;
            this.Phone = sponsorDTO.Phone;
            this.EMail = sponsorDTO.EMail;
            this.Bank = sponsorDTO.Bank;
            this.IBAN = sponsorDTO.IBAN;
            this.Sepa = string.Equals(sponsorDTO.Sepa.ToLower(), "on");
            this.UId = UId;
            this.created = DateTime.Now;
        }

        public string ToMailString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Nachname: " + Surname);
            sb.Append("\n");
            sb.Append("Vorname: " + Name);
            sb.Append("\n");
            sb.Append("Geburtsdatum: " + DateOfBirth.ToString("dd.MM.yyyy"));
            sb.Append("\n");
            sb.Append("Straße: " + Street);
            sb.Append("\n");
            sb.Append("PLZ: " + PostalCode);
            sb.Append("\n");
            sb.Append("Ort: " + City);
            sb.Append("\n");
            sb.Append("Telefon: " + Phone);
            sb.Append("\n");
            sb.Append("E-Mail: " + EMail);
            sb.Append("\n");
            sb.Append("Fördererantrag gestellt am: " + created.ToString("dd.MM.yyyy"));
            sb.Append("\n");
            if (Sepa)
            {
                sb.Append("Bank: " + Bank);
                sb.Append("\n");
                sb.Append("IBAN: " + IBAN);
                sb.Append("\n");
            }
            else
            {
                sb.Append("Kein SEPA Mandat erteilt");
                sb.Append("\n");
            }
            return sb.ToString();
        }

    }
}