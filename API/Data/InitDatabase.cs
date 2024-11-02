
using Microsoft.AspNetCore.Identity;
using Core.Autentication;

namespace Microsoft.AspNetCore.Builder
{

    public static class InitDbExtensions
    {
        public static IApplicationBuilder InitDb(this WebApplication app)
        {

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetService<UserManager<User>>();

                var roleManager = services.GetService<RoleManager<Role>>();

                //var userTypeManager = services.GetService<RoleManager<UserType>>();

                if (userManager.Users.Count() == 0)
                {
                    Task.Run(() => InitRoles(roleManager)).Wait();
                    Task.Run(() => InitUsers(userManager)).Wait();
                    //Task.Run(() => InitUserTypes(userTypeManager)).Wait();
                }

            }
            return app;
        }

        private static async Task InitRoles(RoleManager<Role> roleManager)
        {
            try
            {
                var role = new Role("Admin");
                await roleManager.CreateAsync(role);

                role = new Role("ContentModerator");
                await roleManager.CreateAsync(role);

                role = new Role("Default");
                await roleManager.CreateAsync(role);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /*private static async Task InitUserTypes(RoleManager<UserType> userTypeManager)
        {
            try
            {
                var userType = new UserType() { UserTypeName = "Premium" };
                await userTypeManager.CreateAsync(userType);

                //new UserType("Banned")
                userType = new UserType() { UserTypeName = "Basic" };
                await userTypeManager.CreateAsync(userType);

                userType = new UserType() { UserTypeName = "Banned" };
                await userTypeManager.CreateAsync(userType);
            }
            catch (Exception ex)
            {

                throw;
            }

        }*/

        private static async Task InitUsers(UserManager<User> userManager)
        {
            var user = new User() { UserName = "admin@.com", 
                Email = "admin@.com", 
                LastName="Admi",
                MiddleName="Admin",
                Name = "Admin",
                PhoneNumber = "123456789"
            };
            await userManager.CreateAsync(user, "Krasivaya123+");
            await userManager.AddToRoleAsync(user, "Admin");

            var contentModeratorUser = new User()
            {
                UserName = "moderator@.com",
                Email = "moderator@.com",
                LastName = "Moderator",
                MiddleName = "Mod",
                Name = "ContentModerator",
                PhoneNumber = "987654321"
            };
            await userManager.CreateAsync(contentModeratorUser, "Krasivaya123+");
            await userManager.AddToRoleAsync(contentModeratorUser, "ContentModerator");

            var normalUser = new User()
            {
                UserName = "normal@.com",
                Email = "normal@.com",
                LastName = "User",
                MiddleName = "Norm",
                Name = "NormalUser",
                PhoneNumber = "555555555"
            };
            await userManager.CreateAsync(normalUser, "Krasivaya123+");
            await userManager.AddToRoleAsync(normalUser, "Default");

        }
    }
}
