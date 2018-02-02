using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class DeliContext : DbContext
    {
        public DeliContext(DbContextOptions<DeliContext> options): base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-IL0S50Q\\SQLEXPRESS;Database=DeliCore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public virtual DbSet<Buffet> Buffets { get; set; }
        public virtual DbSet<BuffetDishCoupling> BuffetDishCouplings { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<DishComponent> DishComponents { get; set; }
        public virtual DbSet<DishComponentCoupling> DishComponentCouplings { get; set; }
        public virtual DbSet<DishComponentIngredientCoupling> DishComponentIngredientCouplings { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Kitchen> Kitchens { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderBuffetCoupling> OrderBuffetCouplings { get; set; }
        public virtual DbSet<OrderDishCoupling> OrderDishCouplings { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<UserTitle> UserTitles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}