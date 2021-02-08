namespace Finance.Infrastructure.Data.UnitOfWork.Mapping
{
    using System;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using FluentNHibernate.Mapping;

    internal class BankPostingMap : ClassMap<BankPosting>
    {
        internal BankPostingMap()
        {
            Id(c => c.Id)
                .GeneratedBy
                .GuidComb();

            Component(bankPosting => bankPosting.Amount, component =>
            {
                component.Map(bankPostingAmount => bankPostingAmount.Value, "Amount")
                    .CustomSqlType("decimal(7,2)")
                    .Not.Nullable();
            });

            Component(bankPosting => bankPosting.DueDate, component =>
            {
                component.Map(bankPostingDueDate => bankPostingDueDate.Value, "DueDate")
                    .CustomSqlType("datetime")
                    .Not.Nullable();
            });

            Map(bankPosting => bankPosting.PaymentDate, "PaymentDate")
                .CustomType<DateTime>()
                .Access
                .CamelCaseField(Prefix.Underscore)
                .Nullable();

            Component(bankPosting => bankPosting.Description, component =>
            {
                component.Map(bankPostingDescription => bankPostingDescription.Value, "Description")
                    .CustomSqlType("varchar(80)")
                    .Not.Nullable();
            });

            Map(bankPosting => bankPosting.DocumentDate, "DocumentDate")
                .CustomType<DateTime>()
                .Access
                .CamelCaseField(Prefix.Underscore)
                .Nullable();

            Map(bankPosting => bankPosting.DocumentNumber, "DocumentNumber")
                .CustomType<string>()
                .Access
                .CamelCaseField(Prefix.Underscore)
                .Nullable();

            Map(bankkingPosting => bankkingPosting.Type)
                .CustomType<BankPostingType>()
                .Not
                .Nullable();

            Table("BankPosting");

            References(bankkingPosting => bankkingPosting.Creditor)
                .ForeignKey("FK_BAPO_CRED_01")
                .Column("CreditorId")
                .Not
                .Nullable();

            References(bankkingPosting => bankkingPosting.BankAccount)
                .ForeignKey("FK_BAPO_BAAC_01")
                .Column("BankAccountId")
                .Not
                .Nullable();

            References(bankkingPosting => bankkingPosting.Cateogry)
                .ForeignKey("BAPO_CATE_01")
                .Column("CategoryId")
                .Not
                .Nullable();
        }
    }
}