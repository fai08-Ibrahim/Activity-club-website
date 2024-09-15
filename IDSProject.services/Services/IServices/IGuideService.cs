using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;

namespace IDSProject.services.Services
{
    public interface IGuideService
    {
        Task<Guide?> GetByGuideIdAsync(int guideId);
        Task<IEnumerable<Guide>> GetAllGuides();
        Task AddOrUpdateGuide(Guide guide);
        Task DeleteGuideById(int guideId);
        Task<Guide?> GetGuideByUsername(string username);
    }
}
