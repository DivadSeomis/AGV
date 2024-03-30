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
            request.Status = (Enums.StatusRequest)1;
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

        public async Task<RequestModel> ChangeStatus(int id, int status)
        {
            RequestModel requestForId = await GetRequestForId(id);

            if (requestForId == null)
            {
                throw new Exception($"Rquest with ID: {id} not found!");
            }

            if(status < 1 || status > 4)
            {
                throw new Exception($"Status ID: {status} invalid!");
            }

            if(((Enums.StatusRequest)status == (Enums.StatusRequest)2 && requestForId.Status != (Enums.StatusRequest)1) ||
                ((Enums.StatusRequest)status == (Enums.StatusRequest)3 && requestForId.Status != (Enums.StatusRequest)2) ||
                ((Enums.StatusRequest)status == (Enums.StatusRequest)4 && requestForId.Status != (Enums.StatusRequest)3))
            {
                throw new Exception($"Can't update request for that Status ID: {status}! Atual status: {requestForId.Status}");
            }

            if((Enums.StatusRequest)status == (Enums.StatusRequest)2)
            {
                int newStock = requestForId.Goods.Stock - requestForId.Quantity;
                if(newStock < 0)
                {
                    throw new Exception($"Insufficient Stock!");
                }

                requestForId.Goods.Stock = newStock;
                _dbContext.Goods.Update(requestForId.Goods);
                await _dbContext.SaveChangesAsync();

            }

            
            requestForId.Status = (Enums.StatusRequest)status;

            _dbContext.Requests.Update(requestForId);
            await _dbContext.SaveChangesAsync();

            return requestForId;
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
