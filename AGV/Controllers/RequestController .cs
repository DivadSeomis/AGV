using AGV.Models;
using AGV.Repositories;
using AGV.Repositories.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AGV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository _requestRepository;

        public RequestController(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestModel>>> GetAllRequests()
        {
            List<RequestModel> requestsList = await _requestRepository.GetAllRequests();
            return Ok(requestsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestModel>> GetRequestForId(int id)
        {
            RequestModel request = await _requestRepository.GetRequestForId(id);
            return Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<RequestModel>> AddRequest([FromBody] RequestModel requestModel)
        {
            RequestModel request = await _requestRepository.Add(requestModel);
            return Ok(request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RequestModel>> UpdateRequest([FromBody] RequestModel requestModel, int id)
        {
            requestModel.Id = id;
            RequestModel request = await _requestRepository.Update(requestModel, id);
            return Ok(request);
        }

        [HttpPut("/changeStatus/{id}")]
        public async Task<ActionResult<RequestModel>> UpdateStatus(int id, int status)
        {
            RequestModel request = await _requestRepository.ChangeStatus(id, status);
            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestModel>> DeleteRequest(int id)
        {
            bool delete = await _requestRepository.Delete(id);
            return Ok(delete);
        } 
    }
}
