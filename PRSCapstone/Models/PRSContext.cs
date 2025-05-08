using Microsoft.EntityFrameworkCore;
using System;

namespace PRSCapstone.Models
{
    public class PRSContext : DbContext
    {
        

        public PRSContext() 
        {
        }

        public PRSContext(DbContextOptions<PRSContext> options) : base(options)
        {
        }


        public virtual DbSet<Users> User { get; set; }
        public virtual DbSet<Requests> Request { get; set; }
        public virtual DbSet<Vendors> Vendor { get; set; }
        public virtual DbSet<LineItems> LineItem { get; set; }
        public virtual DbSet<Products> Product { get; set; }

    }
}
