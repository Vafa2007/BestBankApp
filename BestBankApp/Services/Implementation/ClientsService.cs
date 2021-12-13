using AutoMapper;
using BestBankApp.Models;
using BestBankApp.Repository;
using BestBankApp.Services.Helpers;
using BestBankApp.Services.Interface;
using System;
using System.Linq;

namespace BestBankApp.Services.Implementation
{
    public class ClientsService : IClientsService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Clients> _clients;
        public ClientsService(IMapper mapper, IRepository<Clients> clients)
        {
            _mapper = mapper;
            _clients = clients;
        }


        public SimpleResponse CreateClient(ClientPayload model)
        {
            SimpleResponse sr = new SimpleResponse();
            try
            {
                Clients clients = _mapper.Map<ClientPayload, Clients>(model);
                clients.CreatedAt = DateTime.Now;
                _clients.Insert(clients);
                _clients.Save();
                sr.ErrorCode = 0;
                sr.Result = "Operation was successfully completed!";
                return sr;
            }
            catch
            {
                sr.ErrorCode = 1;
                sr.Result = "Error!";
                return sr;
            }
        }
        public EditClientPayload ClientUpdate(int id)
        {
            var edit = _clients.AllQuery.Single(x => x.Id == id);
            EditClientPayload res = _mapper.Map<Clients, EditClientPayload>(edit);
            return res;
        }

        public SimpleResponse ClientUpdate(int id, EditClientPayload model)
        {
            SimpleResponse sr = new SimpleResponse();
            try
            {
                var edit = _clients.AllQuery.Single(x => x.Id == id);
                if (edit == null)
                {
                    sr.ErrorCode = 1;
                    sr.Result = "Error!";
                }
                else
                {
                    _mapper.Map(model, edit);
                    _clients.Update(edit);
                    _clients.Save();
                    sr.ErrorCode = 0;
                    sr.Result = "Operation was successfully completed!";
                }
                return sr;
            }
            catch
            {
                sr.ErrorCode = 1;
                sr.Result = "Error!";
                return sr;
            }
        }
    }
}
