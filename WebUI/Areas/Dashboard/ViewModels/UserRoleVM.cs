using Entities.Concrete;
using WebUI.Models;

namespace WebUI.Areas.Dashboard.ViewModels
{
    public class UserRoleVM
    {
		public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
