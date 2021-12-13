using System;

namespace BestBankApp.Services.Helpers
{
    public class ClientPayload
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public float Salary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
