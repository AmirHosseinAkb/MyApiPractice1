using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pluralize;
using Pluralize.NET;

namespace Common.Utilities
{
    public static class ModelBuilderExtensions
    {

        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder,params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(t => !t.IsAbstract && t.IsClass && t.IsPublic && t.IsAssignableFrom(typeof(BaseType)));
            foreach (var type in types)
            {
                modelBuilder.Entity(type);
            }
        }

        public static void AddRestrictDeleteBehavior(this ModelBuilder modelBuilder)
        {
            IEnumerable<IMutableForeignKey> fks = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var foreignKey in fks)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public static void SetSequentialGuidConvention(this ModelBuilder modelBuilder)
        {
            AddDefaultValueSqlConvention(modelBuilder,typeof(Guid),"Id","NEWSEQUENTIALID()");
        }

        public static void AddDefaultValueSqlConvention(this ModelBuilder modelBuilder, Type propertyType, string propertyName,
            string defaultValue)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                IMutableProperty property = entityType.GetProperties()
                    .SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase) && p.ClrType==propertyType);
                if (property != null)
                    property.SetDefaultValueSql(defaultValue);
            }
        }

        public static void AddPluralizingTableName(this ModelBuilder modelBuilder)
        {
            var pluralizer = new Pluralizer();
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.GetTableName();
                var pluralizedTableName = pluralizer.Pluralize(tableName);

                entityType.SetTableName(pluralizedTableName);
            }
        }
    }
}
