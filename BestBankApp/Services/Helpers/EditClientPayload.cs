using System;

namespace BestBankApp.Services.Helpers
{
    public class EditClientPayload
    {

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public float Salary { get; set; }
    }
}
