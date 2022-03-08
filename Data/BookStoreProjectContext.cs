#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreProject.Models;

namespace BookStoreProject.Data
{
    public class BookStoreProjectContext : DbContext
    {
        public BookStoreProjectContext (DbContextOptions<BookStoreProjectContext> options)
            : base(options)
        {
        }

        public DbSet<BookStoreProject.Models.book> book { get; set; }

        public DbSet<BookStoreProject.Models.usersaccounts> usersaccounts { get; set; }

        public DbSet<BookStoreProject.Models.orders> orders { get; set; }

        public DbSet<BookStoreProject.Models.report> report { get; set; }


    }
}
