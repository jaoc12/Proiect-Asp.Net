using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Proiect_DAW.Models;

[assembly: OwinStartupAttribute(typeof(Proiect_DAW.Startup))]
namespace Proiect_DAW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminAndUserRoles();
        }

        private void CreateAdminAndUserRoles()
        {
            var ctx = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(ctx));
            var userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(ctx));

            // adaugam rolurile pe care le poate avea un utilizator
            // din cadrul aplicatiei
            if (!roleManager.RoleExists("Admin"))
            {
                // adaugam rolul de administrator
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                // se adauga utilizatorul administrator
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                var adminCreated = userManager.Create(user, "Admin2020!");
                if (adminCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Critic"))
            {
                // adaugam rolul userului normal
                var role = new IdentityRole();
                role.Name = "Critic";
                roleManager.Create(role);

                // se adauga userului
                var user = new ApplicationUser();
                user.UserName = "critic@critic.com";
                user.Email = "critic@critic.com";

                var criticCreated = userManager.Create(user, "Critic2020!");
                if (criticCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Critic");
                }
            }
        }

    }
}
