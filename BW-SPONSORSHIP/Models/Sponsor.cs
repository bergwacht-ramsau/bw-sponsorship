using System.ComponentModel.DataAnnotations;

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
        public bool Sepa {get;set;}

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
            this.Sepa = sponsorDTO.Sepa;
            this.UId = UId;
            this.created = DateTime.Now;
        }

    }
}