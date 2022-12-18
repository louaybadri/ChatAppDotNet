using ProjetDotNet.Models;

namespace ProjetDotNet.Data
{
	public interface IUserFactory
	{
		public User CreateUser(User user, string type);
	}
}
