using System.ComponentModel.DataAnnotations;

namespace ProjetDotNet.Models
{
	public class User
	{
		public enum UserType
		{
			Student,
			Teacher
		}

		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		//public UserType UserType { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }

		public User(int id, string name, string email, string password, string phone, string address)
		{
			Id = id;
			Name = name;
			Email = email;
			Password = password;
			Phone = phone;
			Address = address;
		}
	}
}
