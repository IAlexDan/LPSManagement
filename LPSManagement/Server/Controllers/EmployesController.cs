using LPSManagement.Server.Data;
using LPSManagement.Server.Repository;
using LPSManagement.Shared.Models;
using LPSManagement.Shared.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LPSManagement.Server.Controllers
{
    [Route("api/employes")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly LPSManagementDbContext _context;
        private readonly IEmployeRepository _repository;

        public EmployesController(LPSManagementDbContext context, IEmployeRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EmployeParameters employeParameters)
        {
            var employes = await _repository.GetEmployes(employeParameters);
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employes.MetaData));

            return Ok(employes);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employe = await _context.Employes.FirstOrDefaultAsync(e => e.Id == id);
            return Ok(employe);
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Post(Employe employe)
        {
            _context.Add(employe);
            await _context.SaveChangesAsync();
            return Ok(employe.Id);

        }

        [HttpPut]
        public async Task<IActionResult> Put(Employe employe)
        {
            _context.Entry(employe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employe = new Employe { Id = id };
            _context.Remove(employe);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
