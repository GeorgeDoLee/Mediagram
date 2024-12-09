using Mediagram.Common;
using Mediagram.DTOs;
using Mediagram.FileManagement;
using Mediagram.Models;
using Mediagram.Repositories;

namespace Mediagram.Services
{
    public class PublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FileManager _fileManager;
        public PublisherService(IUnitOfWork unitOfWork, FileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
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

            if(publisher.Logo != null)
            {
                var logoDeleted = _fileManager.DeleteImage(publisher.Logo);
            }

            await _unitOfWork.Publishers.RemoveAsync(publisher);
            await _unitOfWork.Complete();

            return true;
        }


        public async Task<Publisher> UploadPublisherLogoAsync(int id, IFormFile file)
        {
            var publisher = await GetPublisherByIdAsync(id);

            if(publisher == null)
            {
                return null;
            }

            var logoPath = await _fileManager.UploadImageAsync(file);
            publisher.Logo = logoPath;
            await _unitOfWork.Complete();

            return publisher;
        }


        public async Task<bool> DeletePublisherLogoAsync(int id)
        {
            var publisher = await GetPublisherByIdAsync(id);

            if(publisher != null && publisher.Logo != null)
            {
                return _fileManager.DeleteImage(publisher.Logo);
            }

            return false;
        }
    }
}
