using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARTJ.Apresentacao.Repository
{
    public class UserRepository
    {
        public static Model.User Get(string userName, string password)
        {
            var users = new List<Model.User>
            {
                new Model.User {Id = 1, UserName = "Laura", Password = "124", Role = "manager"},
                new Model.User {Id = 2, UserName = "Suzany", Password = "123", Role = "employee"}
        };
            return (Model.User)users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password);
        }
    }
}
