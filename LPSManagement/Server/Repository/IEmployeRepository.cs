using LPSManagement.Server.Paging;
using LPSManagement.Shared.Models;
using LPSManagement.Shared.RequestFeatures;
using System.Threading.Tasks;

namespace LPSManagement.Server.Repository
{
    public interface IEmployeRepository
    {
        Task<PagedList<Employe>> GetEmployes(EmployeParameters productParameters);
    }
}
