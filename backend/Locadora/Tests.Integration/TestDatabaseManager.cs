using Infrastructure.Database;
using Infrastructure.Database.Common;
using Infrastructure.Database.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Linq;

namespace Tests.Integration
{
    public static class TestDatabaseManager
    {
        public static void RebuildDatabase(IServiceProvider serviceProvider)
        {
            var databaseContext = serviceProvider.GetService<DatabaseContext>();
            databaseContext!.Database.EnsureDeleted();
            databaseContext.Database.Migrate();
        }

        public static void TruncateAllTables(IServiceProvider serviceProvider)
        {
            var databaseContext = serviceProvider.GetService<DatabaseContext>();

            var tableNames = GetTableNames();
            foreach (var tableName in tableNames)
            {
                databaseContext!.Database.ExecuteSqlRaw($"SET FOREIGN_KEY_CHECKS = 0; \n TRUNCATE TABLE {tableName}; \n SET FOREIGN_KEY_CHECKS = 1;");
            }
        }

        private static IEnumerable GetTableNames()
        {
            var tableNames = typeof(ClienteMapping).Assembly
                .GetTypes()
                .Where(x => x.IsSubclassOfRawGeneric(typeof(BaseMapping<>)))
                .Where(x => x.IsAbstract is false)
                .Select(x => Activator.CreateInstance(x))
                .Select(x => x.GetType().GetProperty(nameof(ClienteMapping.TableName)).GetValue(x))
                .ToList();
            return tableNames;
        }
    }

    public static class TypeExtensions
    {
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}
