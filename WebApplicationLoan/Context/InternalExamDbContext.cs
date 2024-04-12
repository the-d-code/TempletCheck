using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplicationLoan.Models;

namespace WebApplicationLoan.Context
{
    public class InternalExamDbContext : DbContext
    {
        public InternalExamDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Loan> Loans { get; set; }
    }
}
