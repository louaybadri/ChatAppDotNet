using ProjetDotNet.Models;

namespace ProjetDotNet.Data.FactoryMethod
{
	public interface IUserFactory
	{
		public User CreateUser(User user, string type);
	}
}
