using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.Contexts
{
    public class MyContext : DbContext
    {
        public MyContext() : base("myConnection")
        {

        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}