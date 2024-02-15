using Data_Access_Layer.Models.CategoryModel;
using Data_Access_Layer.Models.UserModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data
{
    public class Context : DbContext
    {

        //Here, I have created a constructor for a class called Context, inheriting from DbContext. It accepts options to configure its interaction with the database.
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        //Here, I have created a property called Users, which represents a collection of UserApi entities in a database.

        public DbSet<UserApi> Users { get; set; }

        //Here, I have created a DbSet property called Categories in a class. This property represents a collection of CategoryApi entities in a database, allowing you to interact with them using Entity Framework in your .NET application.
        public DbSet<CategoryApi> Categories { get; set; }
    }
}
