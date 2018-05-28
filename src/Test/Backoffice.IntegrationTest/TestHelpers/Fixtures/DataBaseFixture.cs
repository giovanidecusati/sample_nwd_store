using BackOffice.Sales.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.IO;

namespace Backoffice.Sales.IntegrationTest.TestHelpers.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        private static readonly string _testPath;
        private readonly SalesDbContext _workDbContext;
        private readonly IDbContextTransaction _transaction;

        public SalesDbContext WorkDbContext
        {
            get { return _workDbContext; }
        }

        static DatabaseFixture()
        {
            _testPath = Path.GetDirectoryName(typeof(DatabaseFixture).Assembly.CodeBase.Replace("file:///", ""));

            // For localdb connection string
            AppDomain.CurrentDomain.SetData("DataDirectory", _testPath);

            var options = GetDbContextOptions();

            using (var salesContext = new SalesDbContext(options))
            {
                salesContext.Database.Migrate();
            }
        }

        public DatabaseFixture()
        {
            var options = GetDbContextOptions();
            _workDbContext = new SalesDbContext(options);
            _workDbContext.Database.OpenConnection();
            _transaction = _workDbContext.Database.BeginTransaction();
        }

        private static DbContextOptions<SalesDbContext> GetDbContextOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>()
                .UseSqlServer($"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BackofficeDb.Tests;Integrated Security=True;AttachDBFilename={_testPath}\\BackofficeDb.Tests.mdf");
            return optionsBuilder.Options;
        }

        public void Dispose()
        {
            // Discard any inserts since we didn't commit
            _transaction.Dispose();
            _workDbContext.Dispose();
        }
    }
}
