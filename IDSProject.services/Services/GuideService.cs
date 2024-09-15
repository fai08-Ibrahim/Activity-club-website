using System.Collections.Generic;
using System.Threading.Tasks;
using IDSProject.Models;
using IDSProject.core.Repositories.IRepositories;

namespace IDSProject.services.Services
{
    public class GuideService : IGuideService
    {
        private readonly IGuideRepository _guideRepository;

        public GuideService(IGuideRepository guideRepository)
        {
            _guideRepository = guideRepository;
        }

        public async Task<Guide?> GetByGuideIdAsync(int guideId)
        {
            return await _guideRepository.GetByIdAsync(guideId);
        }

        public async Task<IEnumerable<Guide>> GetAllGuides()
        {
            return await _guideRepository.GetAllGuides();
        }

        public async Task AddOrUpdateGuide(Guide guide)
        {
            await _guideRepository.AddOrUpdateGuideAsync(guide);
        }

        public async Task DeleteGuideById(int guideId)
        {
            await _guideRepository.DeleteGuideByIdAsync(guideId);
        }

        public async Task<Guide?> GetGuideByUsername(string username)
        {
            return await _guideRepository.GetGuideByUsernameAsync(username);
        }
    }
}
