using Mediagram.Common;
using Mediagram.DTOs;
using Mediagram.Models;
using Mediagram.Repositories;

namespace Mediagram.Services
{
    public class PublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PublisherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            var publishers = await _unitOfWork.Publishers.GetAllAsync();

            return publishers;
        }


        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetAsync(id);

            return publisher;
        }


        public async Task<Publisher> AddPublisherAsync(PublisherDto dto)
        {
            var publisher = new Publisher
            {
                Name = dto.Name,
                Bias = dto.Bias,
            };

            await _unitOfWork.Publishers.AddAsync(publisher);
            await _unitOfWork.Complete();

            return publisher;
        }


        public async Task<Publisher> UpdatePublisherAsync(int id, PublisherDto dto)
        {
            

            var existingPublisher = await _unitOfWork.Publishers.GetAsync(id);

            if (existingPublisher == null)
            {
                return null;
            }

            existingPublisher.Name = dto.Name;
            existingPublisher.Bias = dto.Bias;

            await _unitOfWork.Complete();

            return existingPublisher;
        }


        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetAsync(id);

            if (publisher == null)
            {
                return false;
            }

            await _unitOfWork.Publishers.RemoveAsync(publisher);
            await _unitOfWork.Complete();

            return true;
        }
    }
}
