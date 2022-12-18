namespace ProjetDotNet.Models
{
    public class Student : User
    {
        public Student(int id, string name, string email, string password, string phone, string address) : base(id, name, email, password, phone, address)
        {
        }
    }
}
