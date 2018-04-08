using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace WebAPIServices.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加个人资料数据，若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // 在此处添加自定义用户声明
            return userIdentity;
        }


        [Display(Name = "来源系统")]
        public string SystemCode { get; set; }

        [Display(Name = "活跃用户")]
        public bool Active { get; set; }

        [Display(Name = "备注")]
        public string Remark { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<OMSG> OMSG { get; set; }
        public DbSet<MSG1> MSG1 { get; set; }

        public DbSet<OMSGLog> OMSGLog { get; set; }
        public DbSet<MSG1Log> MSG1Log { get; set; }

        public DbSet<MSG2> MSG2 { get; set; }
        public DbSet<MSG2Log> MSG2Log { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<WebAPIServices.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}