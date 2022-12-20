using ProjetDotNet.Data;
using ProjetDotNet.Data.Context;
using ProjetDotNet.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
	private readonly ChatAppContext _applicationDbContext;
	public IUserRepository Users { get; private set; }
	public IMessageRepository Messages { get; private set; }


	public UnitOfWork(ChatAppContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
		Users = new UserRepository(applicationDbContext);
		Messages = new MessageRepository(applicationDbContext);
	}

	public bool Complete()
	{
		try
		{
			int result1 = _applicationDbContext.SaveChanges();
			if (result1 > 0)
			{
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

}
