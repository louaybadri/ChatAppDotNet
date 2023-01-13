using Microsoft.AspNetCore.SignalR;
namespace ProjetDotNet.Hubs

{
	public class ChatHub : Hub
	{
		public async Task SendMessage(int id, string msg)
		{
			await Clients.All.SendAsync("ReceiveMessage", id, msg);
		}
	}
}
