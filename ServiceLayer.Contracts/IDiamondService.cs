using ServiceLayer.Contracts.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts
{
    public interface IDiamondService
    {
        Task AddDiamond(Diamond diamond);
        Task<IEnumerable<Diamond>> GetDiamonds();
        Task UpdateDiamond(Diamond diamond);
        Task DeleteDiamond(int Id);
        Task<Diamond> GetDiamond(int Id);
    }
}
