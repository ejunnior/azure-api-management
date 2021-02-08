namespace Finance.Infrastructure.Data.UnitOfWork.Mapping
{
    using Domain.Category.Aggregates.CategoryAggregate;
    using FluentNHibernate.Mapping;

    internal class CategoryMap : ClassMap<Category>
    {
        internal CategoryMap()
        {
            Id(c => c.Id)
                .GeneratedBy
                .GuidComb();

            Component(category => category.Name, component =>
            {
                component.Map(categoryName => categoryName.Value, "Name")
                    .CustomSqlType("varchar(80)")
                    .Not.Nullable();
            });

            Map(category => category.Type)
                .CustomType<CategoryType>()
                .Not
                .Nullable();

            Table("Category");
        }
    }
}