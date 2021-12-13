using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestBankApp.Models
{
    public partial class Clients
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("SURNAME")]
        public string Surname { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("BIRTHDAY")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Birthday { get; set; }

        [Column("SALARY")]
        public float Salary { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }
    }
}
