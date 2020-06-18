using ServiceLayer.Contracts.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts
{
    public interface IDiamondService
    {
        Task AddDiamond(Diamond diamond);
        Task<IEnumerable<Diamond>> GetDiamonds();
    }
}
