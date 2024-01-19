using Microsoft.EntityFrameworkCore;
using SMSApi.Models;

namespace SMSApi.Data
{
    public class SMSDbContext : DbContext
    {
        public SMSDbContext(DbContextOptions<SMSDbContext> options) : base(options)
        {

        }

        public DbSet<BasicSMS> BasicSMSes { get; set; }

    }
}
