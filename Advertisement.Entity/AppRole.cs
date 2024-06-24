using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Entity
{
    public class AppRole : BaseEntity
    {
        public string Definition { get; set; }
        public IEnumerable<AppUserRole> AppUserRoles { get; set; }
    }
}
