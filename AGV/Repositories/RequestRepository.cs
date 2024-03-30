using AGV.Data;
using AGV.Models;
using AGV.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AGV.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AGVDBContext _dbContext;
        public RequestRepository(AGVDBContext agvDBContext) {
            _dbContext = agvDBContext;
        }

        public async Task<List<RequestModel>> GetAllRequests()
        {
            return await _dbContext.Requests
                .Include(x => x.Goods)
                .ToListAsync();
        }

        public async Task<RequestModel> GetRequestForId(int id)
        {
            return await _dbContext.Requests
                .Include(x => x.Goods)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RequestModel> Add(RequestModel request)
        {
            await _dbContext.Requests.AddAsync(request);
            await _dbContext.SaveChangesAsync();

            return request;
        }

        public async Task<RequestModel> Update(RequestModel request, int id)
        {
            RequestModel requestForId = await GetRequestForId(id);

            if(requestForId == null)
            {
                throw new Exception($"Rquest with ID: {id} not found!");
            }

            requestForId.Quantity = request.Quantity;
            requestForId.Description = request.Description;
            requestForId.Status =  request.Status;
            requestForId.GoodsId = request.GoodsId;

            _dbContext.Requests.Update(requestForId);
            await _dbContext.SaveChangesAsync();

            return request;
        }

        public async Task<bool> Delete(int id)
        {
            RequestModel requestForId = await GetRequestForId(id);

            if (requestForId == null)
            {
                throw new Exception($"Request with ID: {id} not found!");
            }

            _dbContext.Requests.Remove(requestForId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        
    }
}
