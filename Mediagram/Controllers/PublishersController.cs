using Mediagram.Common;
using Mediagram.DTOs;
using Mediagram.Models;
using Mediagram.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mediagram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _publisherService;
        public PublishersController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPublishers()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();

            if (publishers == null || !publishers.Any())
            {
                return NotFound(new ApiResponse(ErrorMessages.NotFound));
            }

            return Ok(new ApiResponse(true, "Publishers retrieved successfully.", publishers));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);

            if (publisher == null)
            {
                return NotFound(new ApiResponse(ErrorMessages.NotFound));
            }

            return Ok(new ApiResponse(true, "Publisher retrieved successfully", publisher));
        }


        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse(ErrorMessages.InvalidData));
            }

            var publisher = await _publisherService.AddPublisherAsync(dto);

            return Ok(new ApiResponse(true, "Publisher added successfully.", publisher));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse(ErrorMessages.InvalidData));
            }

            var publisher = await _publisherService.UpdatePublisherAsync(id, dto);

            if (publisher == null)
            {
                return NotFound(new ApiResponse(ErrorMessages.NotFound));
            }

            return Ok(new ApiResponse(true, "Publisher updated successfully.", publisher));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisherDeleted = await _publisherService.DeletePublisherAsync(id);

            return publisherDeleted ? 
                Ok(new ApiResponse(true, "Publisher deleted successfully."))
                :
                NotFound(new ApiResponse(ErrorMessages.NotFound));
        }


        [HttpPost("upload-logo/{id}")]
        public async Task<IActionResult> UploadPublisherLogo(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new ApiResponse(ErrorMessages.InvalidData);
            }

            var publisher = await _publisherService.UploadPublisherLogoAsync(id, file);

            if(publisher == null)
            {
                return NotFound(new ApiResponse(ErrorMessages.NotFound));
            }

            return Ok(new ApiResponse(true, "Logo uploaded successfully.", publisher));
        }


        [HttpDelete("delete-logo/{id}")]
        public async Task<IActionResult> DeletePublisherLogo(int id)
        {
            var logoDeleted = await _publisherService.DeletePublisherLogoAsync(id);

            return logoDeleted ?
                Ok(new ApiResponse(true, "Logo deleted successfully."))
                :
                NotFound(new ApiResponse(ErrorMessages.NotFound));
        }
    }
}
