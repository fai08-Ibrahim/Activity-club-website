using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;

namespace IDSProject.core.Repositories.IRepositories
{
    public interface IGuideRepository
    {
        Task<IEnumerable<Guide>> GetAllGuides();
        Task<Guide?> GetByIdAsync(int id);
        Task AddOrUpdateGuideAsync(Guide guide);
        Task DeleteGuideByIdAsync(int guideId);
        Task<Guide?> GetGuideByUsernameAsync(string username);
    }
}