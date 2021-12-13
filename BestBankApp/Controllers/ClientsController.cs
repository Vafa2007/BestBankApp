using AutoMapper;
using BestBankApp.Models;
using BestBankApp.Repository;
using BestBankApp.Repository.Context;
using BestBankApp.Services.Helpers;
using BestBankApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace BestBankApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        private readonly IMapper _mapper;
        private readonly IRepository<Clients> _clients;
        private readonly IClientsService _clientsService;
        public ClientsController(IMapper mapper, IRepository<Clients> clients, IClientsService creditsService)
        {
            _mapper = mapper;
            _clients = clients;
            _clientsService = creditsService;
        }
        public ActionResult Index()
        {
            return View(_clients.All);
        }

        [HttpPost]
        public ActionResult Index(string surname)
        {
            try
            {
                if (surname == null || surname == "")
                    return RedirectToAction("Index");
                else
                    return View(_clients.All.Where(x => x.Surname == surname));
            }
            catch (Exception e)
            {
                return View(e);
            }
        }
        public ActionResult NewClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewClient(ClientPayload model)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                sr = _clientsService.CreateClient(model);
                if (sr.ErrorCode == 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View(sr);
            }
            else
            {
                sr.ErrorCode = 1;
                sr.Result = "Model is invalid!";
                return View(sr);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                return View(_clientsService.ClientUpdate(id));
            }
            catch(Exception e)
            {
                return View(e);
            }            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditClientPayload model)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                sr = _clientsService.ClientUpdate(id, model);
                if (sr.ErrorCode == 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View(sr);
            }
            else
            {
                sr.ErrorCode = 1;
                sr.Result = "Model is invalid!";
                return View(sr);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Clients client = db.CLIENTS.Find(id);
                db.CLIENTS.Remove(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(e);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
