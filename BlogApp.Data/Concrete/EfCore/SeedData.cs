using System;
using System.Linq;
using BlogApp.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            BlogContext context = app.ApplicationServices.GetRequiredService<BlogContext>();

            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Category 1" },
                    new Category { Name = "Category 2" },
                    new Category { Name = "Category 3" }
                );


                context.SaveChanges();
            }

            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(
                    new Blog { Title = "Blog title 1", Description = "Blog description", Body = "Blog 1 Body", Image = "1.jpg", Date = DateTime.Now.AddDays(-4), IsApproved = true, CategoryId = 1 },
                    new Blog { Title = "Blog title 2", Description = "Blog description", Body = "Blog 2 Body", Image = "2.jpg", Date = DateTime.Now.AddDays(-2), IsApproved = true, CategoryId = 1 },
                    new Blog { Title = "Blog title 3", Description = "Blog description", Body = "Blog 3 Body", Image = "3.jpg", Date = DateTime.Now.AddDays(-5), IsApproved = true, CategoryId = 2 },
                    new Blog { Title = "Blog title 4", Description = "Blog description", Body = "Blog 4 Body", Image = "4.jpg", Date = DateTime.Now.AddDays(-1), IsApproved = true, CategoryId = 1 },
                    new Blog { Title = "Blog title 5", Description = "Blog description", Body = "Blog 5 Body", Image = "5.jpg", Date = DateTime.Now.AddDays(-31), IsApproved = true, CategoryId = 2 },
                    new Blog { Title = "Blog title 6", Description = "Blog description", Body = "Blog 6 Body", Image = "6.jpg", Date = DateTime.Now.AddDays(-16), IsApproved = false, CategoryId = 2 },
                    new Blog { Title = "Blog title 7", Description = "Blog description", Body = "Blog 7 Body", Image = "7.jpg", Date = DateTime.Now.AddDays(-43), IsApproved = true, CategoryId = 2 },
                    new Blog { Title = "Blog title 8", Description = "Blog description", Body = "Blog 8 Body", Image = "8.jpg", Date = DateTime.Now.AddDays(-13), IsApproved = true, CategoryId = 3 },
                    new Blog { Title = "Blog title 9", Description = "Blog description", Body = "Blog 9 Body", Image = "9.jpg", Date = DateTime.Now.AddDays(-5), IsApproved = true, CategoryId = 3 },
                    new Blog { Title = "Blog title 10", Description = "Blog description", Body = "Blog 10 Body", Image = "10.jpg", Date = DateTime.Now.AddDays(-8), IsApproved = false, CategoryId = 3 }
                );

                context.SaveChanges();
            }
        }
    }
}