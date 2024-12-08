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

        public async Task<ApiResponse> GetAllPublishersAsync()
        {
            var publishers = await _unitOfWork.Publishers.GetAllAsync();

            if (publishers == null || !publishers.Any())
            {
                return new ApiResponse("No publishers found.");
            }

            return new ApiResponse(true, "Publishers retrieved successfully.", publishers);
        }


        public async Task<ApiResponse> GetPublisherByIdAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetAsync(id);

            if (publisher == null)
            {
                return new ApiResponse("Publisher not found.");
            }

            return new ApiResponse(true, "Publisher retrieved successfully", publisher);
        }


        public async Task<ApiResponse> AddPublisherAsync(PublisherDto dto)
        {
            if (dto == null)
            {
                return new ApiResponse("Invalid publisher data.");
            }

            var publisher = new Publisher
            {
                Name = dto.Name,
                Bias = dto.Bias,
            };

            await _unitOfWork.Publishers.AddAsync(publisher);
            await _unitOfWork.Complete();

            return new ApiResponse(true, "Publisher added successfully.", publisher);
        }


        public async Task<ApiResponse> UpdatePublisherAsync(int id, PublisherDto dto)
        {
            if (dto == null)
            {
                return new ApiResponse("invalid publisher data.");
            }

            var existingPublisher = await _unitOfWork.Publishers.GetAsync(id);

            if (existingPublisher == null)
            {
                return new ApiResponse("Publisher not found.");
            }

            existingPublisher.Name = dto.Name;
            existingPublisher.Bias = dto.Bias;

            await _unitOfWork.Complete();

            return new ApiResponse(true, "Publisher updated successfully.", existingPublisher);
        }


        public async Task<ApiResponse> DeletePublisherAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetAsync(id);

            if (publisher == null)
            {
                return new ApiResponse("Publisher not found.");
            }

            await _unitOfWork.Publishers.RemoveAsync(publisher);
            await _unitOfWork.Complete();

            return new ApiResponse(true, "Publisher deleted successfully.");
        }
    }
}
