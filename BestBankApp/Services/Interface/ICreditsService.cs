using BestBankApp.Services.Helpers;

namespace BestBankApp.Services.Interface
{
    public interface ICreditsService
    {
        public object CreditAppliesList(string result);
        public SimpleResponse CheckApply(CreditApplyPayload model);
    }
}
