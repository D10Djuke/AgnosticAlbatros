using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models.Api
{
    public class UserDetailData
    {
        public User User { get; set; }

        public IEnumerable<Kitchen> Kitchens { get; set; }
        public IEnumerable<UserTitle> UserTitles { get; set; }

        public string NewKitchenName { get; set; }
        public string NewKitchenDescription { get; set; }
        public string NewUserTitle { get; set; }
        public string NewUserTitleDescription { get; set; }
    }
}
