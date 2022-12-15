namespace Tsi.Erp.FirstApi.Features.ModePaiement.Create.Rules
{
    [RuleOn(ExecuteRuleWhen.BeforeInsert | ExecuteRuleWhen.BeforeUpdate)]
    public class CodeUnique2 : IRule<Gouvernorat>
    {
        public int Order => 2;

        public string ErrorMessage => "No error";

        public Task<bool> ValidateAsync(Gouvernorat entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }
    }
}
