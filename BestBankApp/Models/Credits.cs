using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestBankApp.Models
{
    public class Credits
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("CLIENT_ID")]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Clients CLIENTS { get; set; }

        [Column("AMOUNT_OF_CREDIT")]
        public float AmountOfCredit { get; set; }

        [Column("TERMS_IN_MONTHS")]
        public int TermsInMonths { get; set; }

        [Column("RESULT")]
        public bool Result { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }
    }
}
