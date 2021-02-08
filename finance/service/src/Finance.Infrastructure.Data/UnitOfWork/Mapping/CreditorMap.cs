namespace Finance.Infrastructure.Data.UnitOfWork.Mapping
{
    using Domain.Creditor.Aggregates.CreditorAggregate;
    using FluentNHibernate.Mapping;

    internal class CreditorMap : ClassMap<Creditor>
    {
        internal CreditorMap()
        {
            Id(c => c.Id)
                .GeneratedBy
                .GuidComb();

            Component(creditor => creditor.Name, component =>
            {
                component.Map(creditorName => creditorName.Value, "Name")
                    .CustomSqlType("varchar(80)")
                    .Not
                    .Nullable();
            });

            Map(creditor => creditor.Email, "Email")
                .CustomType<string>()
                .Access
                .CamelCaseField(Prefix.Underscore)
                .Nullable();

            Map(creditor => creditor.MobilePhone, "MobilePhone")
                .CustomType<string>()
                .Access
                .CamelCaseField(Prefix.Underscore)
                .Nullable();

            Table("Creditor");
        }
    }
}