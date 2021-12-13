using System;

namespace BestBankApp.Services.Helpers
{
    public class CreditApplyPayload
    {
        //public int Id { get; set; }
        public int ClientId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public float Salary { get; set; }
        public float AmountOfCredit { get; set; }
        public int TermsInMonths { get; set; }
    }
}
