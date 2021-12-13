using AutoMapper;
using BestBankApp.Models;
using BestBankApp.Repository;
using BestBankApp.Repository.Context;
using BestBankApp.Services.Helpers;
using BestBankApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BestBankApp.Controllers
{
    public class CreditApplicationsController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        private readonly IMapper _mapper;
        private readonly IRepository<Clients> _clients;
        private readonly IRepository<Credits> _credits;
        private readonly ICreditsService _creditsService;
        public CreditApplicationsController(IMapper mapper, IRepository<Clients> clients, IRepository<Credits> credits, ICreditsService creditsService)
        {
            _mapper = mapper;
            _clients = clients;
            _credits = credits;
            _creditsService = creditsService;
        }
        public ActionResult Index()
        {
            try
            {
                return View(_credits.AllQuery.Include(x => x.CLIENTS));
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        [HttpPost]
        public ActionResult Index(string result)
        {
            try
            {
                if (result == null) return RedirectToAction("Index");
                return View(_creditsService.CreditAppliesList(result));
            }
            catch (Exception e)
            {
                return View(e);
            }
                
        }
      
        public ActionResult CreditApply(int id)
        {
            try
            {
                var client = _clients.AllQuery.Single(x => x.Id == id);
                CreditApplyPayload credit_apply = _mapper.Map<Clients, CreditApplyPayload>(client);
                return View(credit_apply);

            }catch(Exception e)
            {
                return View(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreditApply(CreditApplyPayload model)
        {
            SimpleResponse sr = new SimpleResponse();
            if (ModelState.IsValid)
            {
                sr = _creditsService.CheckApply(model);
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

    }
}
