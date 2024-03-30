using AGV.Models;

namespace AGV.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        Task<List<RequestModel>> GetAllRequests();

        Task<RequestModel> GetRequestForId(int id);

        Task<RequestModel> Add(RequestModel request);

        Task<RequestModel> Update(RequestModel request, int id);

        Task<RequestModel> ChangeStatus(int id, int status);

        Task<bool> Delete(int id);
    }
}
