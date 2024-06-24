using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Entity
{
    public class Gender : BaseEntity
    {
        public string Definition { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
    }
}
