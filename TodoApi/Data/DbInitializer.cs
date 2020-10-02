using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(TodoContext context)
        {
            // Delete the database, if it already exists. I do this because an
            // existing database may not be compatible with the entity model,
            // if the entity model was changed since the database was created.
            // This operation has no effect for an in-memory database.
            context.Database.EnsureDeleted();

            // Create the database, if it does not already exists. This operation
            // has no effect for an in-memory database.
            context.Database.EnsureCreated();

            // Look for any TodoItems
            if (context.TodoItems.Any())
            {
                return;   // DB has been seeded
            }

            List<TodoItem> items = new List<TodoItem>
            {
                new TodoItem { IsComplete=true, Name="Make homework"},
                new TodoItem { IsComplete=false, Name="Sleep"}
            };

            // Create two users with hashed and salted passwords
            List<User> users = new List<User>
            {
                new User {
                    Username = "UserJoe",
                    Password = "1234",
                    IsAdmin = false
                },
                new User {
                    Username = "AdminAnn",
                    Password = "1234",
                    IsAdmin = true
                }
            };

            context.TodoItems.AddRange(items);
            context.Users.AddRange(users);
            context.SaveChanges();
        }

    }
}
