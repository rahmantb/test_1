using JWt_12.Models;

namespace JWt_12.Constant
{
    public static class Datas
    {
        public static List<User> users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Name="name1",
                Surname="surname1",
                Role="admin",
                Password="12345"
            },
            new User()
            {
                Id = 2,
                Name="name2",
                Surname="surname2",
                Role="viewer",
                Password="123456"
            },new User()
            {
                Id = 3,
                Name="name3",
                Surname="surname3",
                Role="operator",
                Password="123457"
            }
        };
    }
}
