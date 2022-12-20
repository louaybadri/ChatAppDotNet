using ProjetDotNet.Data.Repository;

public interface IUnitOfWork
{
	IUserRepository Users { get; }
	IMessageRepository Messages { get; }

	bool Complete();
}