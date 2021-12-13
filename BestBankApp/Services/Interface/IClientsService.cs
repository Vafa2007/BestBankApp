using BestBankApp.Services.Helpers;

namespace BestBankApp.Services.Interface
{
    public interface IClientsService
    {
        public SimpleResponse CreateClient(ClientPayload model);
        public EditClientPayload ClientUpdate(int id);
        public SimpleResponse ClientUpdate(int id, EditClientPayload model);
    }
}
