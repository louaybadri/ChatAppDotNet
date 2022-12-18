using ProjetDotNet.Models;

namespace ProjetDotNet.Data
{
    public interface UserFactory : IUserFactory
    {
        public User? CreateUser(int id, string name, string email, string password, string phone, string address, string type)
        {

            if (type == "Student")
            {
                return new Student(id, name, email, password, phone, address);
            }
            else if (type == "Teacher")
            {
                return new Teacher(id, name, email, password, phone, address);
            }
            return null;
        }
    }
}
