using LPSManagement.Server.Data;
using LPSManagement.Server.Paging;
using LPSManagement.Server.Repository.RepositoryExtension;
using LPSManagement.Shared.Models;
using LPSManagement.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPSManagement.Server.Repository
{
    public class EmployRepository : IEmployeRepository
    {
        private readonly LPSManagementDbContext _context;

        public EmployRepository(LPSManagementDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Employe>> GetEmployes(EmployeParameters employeParameters)
        {
            var employe = await _context.Employes
                .Search(employeParameters.SearchTerm)
                .Sort(employeParameters.OrderBy)
                .ExpDate(employeParameters.ExpDateSearch)
                .ToListAsync();

            return PagedList<Employe>
                .ToPagedList(employe, employeParameters.PageNumber, employeParameters.PageSize);
        }
    }
}
