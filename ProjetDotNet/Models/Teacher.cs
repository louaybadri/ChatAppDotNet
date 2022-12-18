namespace ProjetDotNet.Models
{
	public class Teacher : User
	{
		public Teacher(int id, string name, string email, string password, string phone, string address) : base(id, name, email, password, phone, address)
		{
		}

	}
}