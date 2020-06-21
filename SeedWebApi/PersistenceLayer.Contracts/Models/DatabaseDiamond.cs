using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceLayer.Contracts.Models
{
    public class DatabaseDiamond : BaseEntity
    {

        public String Name { get; set; }
        public String Country { get; set; }
    }
}
