using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVSschuh.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVSschuh.Data.Entities.Seed
{
    public class Seed
    {
        public static void SeedData(IServiceProvider services, IHostingEnvironment env, IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();
                //Seed1(services);
                //Seed2(services);
                //Seed3(services);
                //Seed4(services);
            }

        }
        public static void Seed1(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();
                var roleName = "Admin";
                var roleName2 = "User";
                if (!context.Roles.Any(r => r.Name == roleName2))
                {
                    var result2 = managerRole.CreateAsync(new Role
                    {
                        Name = roleName2
                    }).Result;
                }
                if (!context.Roles.Any(r => r.Name == roleName))
                {

                    var result = managerRole.CreateAsync(new Role
                    {
                        Name = roleName
                    }).Result;

                }
                UserProfile userProfile = new UserProfile
                {
                    FirstName = "Ivan",
                    MiddleName = "Rishala",
                    LastName = "Lvivskiy",
                    Number = "+380503453478",
                   
                    RegistrationDate = DateTime.Now
                };

                User user = new User
                {
                    Email = "karbazar@ivan.com",
                    UserName = "RiShalA",                   
                    UserProfile = userProfile
                };


                UserProfile userProfile1 = new UserProfile
                {
                    FirstName = "Petro",
                    MiddleName = "Loshara",
                    Number = "+380508461286",
                    LastName = "Petrovich",
                    RegistrationDate = DateTime.Now
                };
                User user1 = new User
                {
                    Email = "karbazar@petro.com",
                    UserName = "Vasyok",
                    UserProfile = userProfile1
                };
                var result02 = manager.CreateAsync(user1, "Qweerty-1").Result;
                result02 = manager.AddToRoleAsync(user1, roleName2).Result;
                var result01 = manager.CreateAsync(user, "Qwerty-1").Result;
                result01 = manager.AddToRoleAsync(user, roleName).Result;
            }
        }
        public static void Seed2(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();

                Brend brend = new Brend
                {
                    BrendName = "Gachi Muchi"
                };
                Brend brend1 = new Brend
                {
                    BrendName = "Converse"
                };
                Brend brend2 = new Brend
                {
                    BrendName = "Anime"
                };
                context.Brend.Add(brend);
                context.Brend.Add(brend1);
                context.Brend.Add(brend2);
                Trend trend = new Trend
                {
                    TrendName = "Retro Trainers"
                };
                context.Trend.Add(trend);
                Category category = new Category
                {
                    CategName = "Man"
                };
                Category category1 = new Category
                {
                    CategName = "Woman"
                };
                Category category2 = new Category
                {
                    CategName = "Kids"
                };
                context.Category.Add(category);
                context.Category.Add(category1);
                context.Category.Add(category2);
                context.SaveChanges();
            }
        }
        public static void Seed3(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();
                Product product = new Product
                {
                    Name = "Gachi",
                    Size = 48,
                    Sale = 0,
                    Like = 20,
                    Dislike = 5,
                    Price = 150,
                    Available = true,
                    Count = 3243,
                    TrendId = 1,
                    BrendId = 1,
                    CategoryId = 1
                };
                context.Product.Add(product);
                context.SaveChanges();
            }
        }
        public static void Seed4(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();
                Order order = new Order
                {
                    UserId = "64aa9a12-c945-43df-a122-35d2a49866c0",
                    ProductId = 1
                };
                context.Order.Add(order);
                context.SaveChanges();
            }
        }
    }

}
