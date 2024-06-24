using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Entity
{
    public class MilitaryStatus : BaseEntity
    {
        public string Definition { get; set; }
        public IEnumerable<AdvertisementAppUser> AdvertisementAppUsers { get; set; }
    }
}
