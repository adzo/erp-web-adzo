using Tsi.Erp.Shared;


namespace Tsi.Erp.FirstApi.Features.ModePaiement.Create.Rules
{
    [RuleOn(ExecuteRuleWhen.BeforeInsert | ExecuteRuleWhen.BeforeUpdate)]
    public class CodeUniqueRule : IRule<Gouvernorat>
    {
        public int Order => 0;

        public string ErrorMessage => _errorMessage;

        private string _errorMessage = string.Empty;

        private readonly IRepository<Gouvernorat> _gouvernoratRepository;

        public CodeUniqueRule(IRepository<Gouvernorat> gouvernoratRepository)
        {
            _gouvernoratRepository = gouvernoratRepository;
        }

        public async Task<bool> ValidateAsync(Gouvernorat entity, CancellationToken cancellationToken = default)
        {
            _errorMessage = $"Code provided is not unique ({entity.Code})";

            var entityWithSameCode = await _gouvernoratRepository.GetAsync(g => g.Code.Equals(entity.Code), cancellationToken);

            if (entityWithSameCode is not null && entityWithSameCode.Uid != entity.Uid)
            {
                return false;
            }

            return true;
        }
    }
}
