using Mediagram.DTOs;
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
            var response = await _publisherService.GetAllPublishersAsync();

            return response.Success ? Ok(response) : NotFound(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var response = await _publisherService.GetPublisherByIdAsync(id);

            return response.Success ? Ok(response) : NotFound(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherDto dto)
        {
            var response = await _publisherService.AddPublisherAsync(dto);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherDto dto)
        {
            var response = await _publisherService.UpdatePublisherAsync(id, dto);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var response = await _publisherService.DeletePublisherAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }

}
