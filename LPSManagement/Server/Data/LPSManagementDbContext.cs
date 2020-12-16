using LPSManagement.Server.Models;
using LPSManagement.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPSManagement.Server.Data
{
    public class LPSManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public LPSManagementDbContext(DbContextOptions<LPSManagementDbContext> options) : base(options)
        {

        }

        public DbSet<Employe> Employes { get; set; }

    }
}
