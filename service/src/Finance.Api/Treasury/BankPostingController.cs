namespace Finance.Api.Treasury
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Domain.Bank.Aggregates.BankAccountAggregate;
    using Domain.Category.Aggregates.CategoryAggregate;
    using Domain.Creditor.Aggregates.CreditorAggregate;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v{version:apiVersion}/bankposting/")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    public class BankPostingController : BaseController
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankPostingRepository _bankPostingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICreditorRepository _creditorRepository;

        public BankPostingController(
            IBankAccountRepository bankAccountRepository,
            IBankPostingRepository bankPostingRepository,
            ICategoryRepository categoryRepository,
            ICreditorRepository creditorRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _bankPostingRepository = bankPostingRepository;
            _categoryRepository = categoryRepository;
            _creditorRepository = creditorRepository;
        }

        private static string ConnectionString => Environment
            .GetEnvironmentVariable("ConnectionStrings__FinanceConnectionString");

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope<IList<GetBankPostingDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        public async Task<IActionResult> GetBankPosting()
        {
            var sql = @"
                    select
                          bapo.amount
                        , bapo.duedate
                        , bapo.id
                        , bapo.documentDate
                        , bapo.documentNumber
                        , bapo.paymentDate
                        , cred.name as creditor
                        , bapo.description
                        , bapo.type
                    from bankposting bapo
                         inner join creditor cred on bapo.creditorId = cred.Id
                    order by duedate";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = await connection
                    .QueryAsync<GetBankPostingDto>(sql);

                return Ok(result.ToList());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [Consumes("application/json")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterBankPostingDto args)
        {
            if (!Enum.IsDefined(typeof(BankPostingType), //TODO : Review this code.
                args.Type))
                throw new InvalidOperationException();

            var creditor = await _creditorRepository
                .GetAsync(args.CreditorId);

            var bankAccount = await _bankAccountRepository
                .GetAsync(args.BankAccountId);

            var category = await _categoryRepository
                .GetAsync(args.CategoryId);

            var result = BankPostingFactory.Create(
                amount: args.Amount,
                dueDate: args.DueDate,
                documentDate: args.DocumentDate,
                documentNumber: args.DocumentNumber,
                creditor: creditor,
                description: args.Description,
                bankAccount: bankAccount,
                category: category,
                paymentDate: args.PaymentDate,
                type: args.Type);

            if (result.IsSuccess)
                await _bankPostingRepository
                    .AddAsync(result.Value);

            return FromResult(result);
        }
    }
}