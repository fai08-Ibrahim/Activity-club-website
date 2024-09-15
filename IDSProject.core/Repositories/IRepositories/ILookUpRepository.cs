using IDSProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDSProject.Repositories
{
    public interface ILookUpRepository
    {
        Task<IEnumerable<LookUp>> GetAllLookUpsAsync();
        Task<LookUp?> GetLookUpByCodeAsync(int code);
        Task AddLookUpAsync(LookUp lookUp);
        Task UpdateLookUpAsync(LookUp lookUp);
        Task DeleteLookUpAsync(int code);
    }
}
