using PersistenceLayer.Contracts;
using ServiceLayer.Contracts;
using ServiceLayer.Contracts.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using PersistenceLayer.Contracts.Models;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class DiamondService : IDiamondService
    {
        private IRepository<DatabaseDiamond> _diamondRepository;

        public DiamondService(IRepository<DatabaseDiamond> diamondRepository)
        {
            if (diamondRepository == null) throw new ArgumentException("Invalid argument diamondRepository!");
            _diamondRepository = diamondRepository;
        }

        public async Task AddDiamond(Diamond diamond)
        {
            /* NOTE: 
             * 1. Convert Domain Model into DBModel and pass it on to the persistence layer. Eg. AutoMapper could be used here
             * 2. All logic and if validataions any can be carried out here; 
             * 3. FOR Simplicity; mapping Business Model to DB Model manually
             */
            var newDiamond = new DatabaseDiamond
            {
                Id = 10,
                Name = diamond.Name,
                Country = "India",
                CreatedAt = DateTime.UtcNow
            };

            await _diamondRepository.Insert(newDiamond);
        }

        public async Task<IEnumerable<Diamond>> GetDiamonds()
        {
            var diamonds = (await _diamondRepository.GetAll()).ToList();
            return diamonds.Select(t => new Diamond { Name = t.Name, Country = t.Country });
        }
    }
}
