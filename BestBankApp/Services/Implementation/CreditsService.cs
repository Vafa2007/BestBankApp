using AutoMapper;
using BestBankApp.Models;
using BestBankApp.Repository;
using BestBankApp.Services.Helpers;
using BestBankApp.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BestBankApp.Services.Implementation
{
    public class CreditsService : ICreditsService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Credits> _credits; 
        public CreditsService(IMapper mapper, IRepository<Credits> credits)
        {
            _mapper = mapper;
            _credits = credits;
        }

        public object CreditAppliesList(string result)
        {
            if (result == Enum.GetName(typeof(CreditResult), CreditResult.Approved))
                return _credits.AllQuery.Include(x => x.CLIENTS).Where(x => x.Result == true);
            else
                return _credits.AllQuery.Include(x => x.CLIENTS).Where(x => x.Result == false);
        }
        public SimpleResponse CheckApply(CreditApplyPayload model)
        {
            SimpleResponse sr = new SimpleResponse();
            try
            {
                Credits credit = _mapper.Map<CreditApplyPayload, Credits>(model);
                int client_age = DateTime.Today.Year - Convert.ToDateTime(model.Birthday).Year;
                if ((client_age < (int)CreditTerms.AgeMin || client_age > (int)CreditTerms.AgeMax) ||
                    (model.Salary < (int)CreditTerms.SalaryMin) ||
                    (model.AmountOfCredit / model.TermsInMonths > model.Salary / 2))
                    credit.Result = false;
                else
                    credit.Result = true;

                credit.CreatedAt = DateTime.Now;
                _credits.Insert(credit);
                _credits.Save();
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
    }
}
