using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.Contracts.Models
{
    public interface IEntity
    {
        DateTime CreatedAt { get; set; }
    }
}
