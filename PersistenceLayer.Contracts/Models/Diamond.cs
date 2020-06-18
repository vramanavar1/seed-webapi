using System;

namespace PersistenceLayer.Contracts.Models
{
    public class Diamond : BaseEntity
    {
        public String Name { get; set; }
        public String Country { get; set; }
    }
}
