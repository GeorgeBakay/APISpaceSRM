using APISpaceSRM.Data.Interfaces;
using APISpaceSRM.Data.Models;

namespace APISpaceSRM.Data.Repository
{
    public class ClientRepository : IClient
    {
        DataContext db;
        public ClientRepository(DataContext db)
        {
            this.db = db;
        }
        public async void AddClient(Client? client)
        {
            if(client == null)
            {
                throw new NullReferenceException();
            }
            db.clients.Add(
                    new Client
                    {
                        Name = client.Name,
                        SurName = client.SurName,
                        Phone = client.Phone,
                    });
            await db.SaveChangesAsync();
        }
    }
}
