using ProjetDotNet.Models;

namespace ProjetDotNet.Data.FactoryMethod
{
	public interface UserFactory : IUserFactory
	{
		public static User? CreateUser(string name, string email, string password, string type, string phone, string address, string image)
		{
			//if (type == "Student")
			//{
			//	return new Student(name, email, password, phone, address, image);
			//}
			//else if (type == "Teacher")
			//{
			//	return new Teacher(name, email, password, phone, address, image);
			//}
			return null;
		}
	}
}

