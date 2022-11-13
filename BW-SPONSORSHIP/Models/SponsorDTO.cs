namespace BW_SPONSORSHIP.Models
{
    public class SponsorDTO
    {
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
        public string Honeypot {get;set;}

        public SponsorDTO(string Name, string Surname, DateTime DateOfBirth, string Street, string PostalCode, string City, string Phone, string EMail, string Bank, string IBAN, bool Sepa, string Honeypot){
            this.Name = Name;
            this.Surname = Surname;
            this.DateOfBirth = DateOfBirth;
            this.Street = Street;
            this.PostalCode = PostalCode;
            this.City = City;
            this.Phone = Phone;
            this.EMail = EMail;
            this.Bank = Bank;
            this.IBAN = IBAN;
            this.Sepa = Sepa;
            this.Honeypot = Honeypot;
        }
    }
}