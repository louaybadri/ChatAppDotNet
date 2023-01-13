﻿using ProjetDotNet.Data.Context;
using ProjetDotNet.Models;
using System.Linq.Expressions;

namespace ProjetDotNet.Data.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(ChatAppContext _applicationDbContext) : base(_applicationDbContext)
		{
		}
		public User UpdateUser(User user)
		{
			_applicationDbContext.User.Update(user);
			return user;
		}
	}
}