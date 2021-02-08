namespace Finance.Infrastructure.Data.UnitOfWork.Mapping
{
    using Domain.Bank.Aggregates.BankAccountAggregate;
    using FluentNHibernate.Mapping;

    internal class BankAccountMap : ClassMap<BankAccount>
    {
        internal BankAccountMap()
        {
            Id(bankAccount => bankAccount.Id)
                .GeneratedBy
                .GuidComb();

            Component(bankAccount => bankAccount.AccountNumber, component =>
            {
                component.Map(accountNumber => accountNumber.Value, "AccountNumber")
                    .CustomSqlType("varchar(20)")
                    .Not.Nullable();
            });

            Map(bankAccount => bankAccount.Bank)
                .CustomType<Bank>()
                .Not
                .Nullable();

            Table("BankAccount");
        }
    }
}