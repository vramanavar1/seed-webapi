using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.Contracts.Models
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
