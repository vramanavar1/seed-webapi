using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceLayer.Contracts.Models
{
    public class DatabaseRetailer : BaseEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Country { get; set; }
        public DatabaseDiamond Diamond { get; set; }
    }
}
