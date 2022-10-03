namespace BW_SPONSORSHIP.Models
{
    public class Sponsor
    {
        public long Id { get; }
        public string Name { get; }
        public string Surname {get; }
        public DateTime DateOfBirth {get;}
        public string Street {get;}
        public string PostalCode {get;}
        public string City {get;}
        public string Phone {get;}
        public string EMail {get;}
        public string Bank {get;}

        public string IBAN {get;}
        public string UId {get;}

        public Sponsor(){
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

        public Sponsor(SponsorDTO sponsorDTO, string UId){
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
            this.UId = UId;
        }

    }
}