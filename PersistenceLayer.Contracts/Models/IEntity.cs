using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.Contracts.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
