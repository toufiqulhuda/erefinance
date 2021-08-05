using Microsoft.EntityFrameworkCore;
using Erefinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erefinance.Data
{
    public class ApplicationDBContext : DbContext
    {
        //internal object DistrictModel;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
        public DbSet<StatusEntryModel> statusEntry { get; set; }
        public DbSet<DistrictModel> district { get; set; }
        public DbSet<FundModel> fund { get; set; }
        public DbSet<CategoryModel> category { get; set; }
        public DbSet<SectorModel> sector { get; set; }

    }
}
