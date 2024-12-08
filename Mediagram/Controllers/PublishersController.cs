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
                return NotFound(new ApiResponse("No publishers found."));
            }

            return Ok(new ApiResponse(true, "Publishers retrieved successfully.", publishers));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);

            if (publisher == null)
            {
                return NotFound(new ApiResponse("Publisher not found."));
            }

            return Ok(new ApiResponse(true, "Publisher retrieved successfully", publisher));
        }


        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse("Invalid publisher data."));
            }

            var publisher = await _publisherService.AddPublisherAsync(dto);

            return Ok(new ApiResponse(true, "Publisher added successfully.", publisher));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new ApiResponse("invalid publisher data."));
            }

            var publisher = await _publisherService.UpdatePublisherAsync(id, dto);

            if (publisher == null)
            {
                return NotFound(new ApiResponse("Publisher not found."));
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
                NotFound(new ApiResponse("Publisher not found,"));
        }
    }

}
