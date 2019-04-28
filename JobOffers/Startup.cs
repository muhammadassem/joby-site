using JobOffers.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobOffers.Startup))]
namespace JobOffers
{
    public partial class Startup
    {
        private ApplicationDbContext _context;

        public Startup()
        {
            _context = new ApplicationDbContext();
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            CreateRoleAndUserAndAddUserToRole(); // me
        }

        public void CreateRoleAndUserAndAddUserToRole()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            if(! roleManager.RoleExists("Admins"))
            {
                var role = new IdentityRole {
                    Name = "Admins"
                };
                roleManager.Create(role);


                var user = new ApplicationUser
                {
                    UserName = "apoasem",
                    Email = "mohamedasemsyam@outlook.com"
                };

                var userCreated = userManager.Create(user, "A666665");

                if(userCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, role.Name);
                }

            }
        }
    }
}
