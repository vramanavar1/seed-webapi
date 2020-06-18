using ServiceLayer.Contracts.DomainModels;
using System.Collections.Generic;

namespace ServiceLayer.Contracts
{
    public interface IDiamondService
    {
        void AddDiamond(Diamond diamond);
        IEnumerable<Diamond> GetDiamonds();
    }
}
