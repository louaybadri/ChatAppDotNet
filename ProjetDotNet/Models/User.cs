using System.ComponentModel.DataAnnotations;

namespace ProjetDotNet.Models
{
	public class User
	{

		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Type { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string Image { get; set; }
		

		public User(string name, string email, string password, string type, string phone, string address, string image)
		{
			Name = name;
			Email = email;
			Password = password;
			Type = type;
			Phone = phone;
			Address = address;
			Image = image;
		}
		override
		public string ToString()
		{
			return $"Name : {Name}, Email : {Email}, Password : {Password}, Phone : {Phone} Address : {Address}, Image : {Image}";
		}
	}
}
