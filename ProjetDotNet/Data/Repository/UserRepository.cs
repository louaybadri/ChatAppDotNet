using ProjetDotNet.Data.Context;
using ProjetDotNet.Models;
using System.Linq.Expressions;

namespace ProjetDotNet.Data.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(ChatAppContext _applicationDbContext) : base(_applicationDbContext)
		{
		}
		public User UpdateUser(int id, User user)
		{
			User UserToUpdate = Get(id);

			UserToUpdate.Name = user.Name;
			UserToUpdate.Email = user.Email;
			UserToUpdate.Password = user.Password;
			UserToUpdate.Phone = user.Phone;
			UserToUpdate.Address = user.Address;
			_applicationDbContext.User.Update(UserToUpdate);
			return user;
		}
	}
}