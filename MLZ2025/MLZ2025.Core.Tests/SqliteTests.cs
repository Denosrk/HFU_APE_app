using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLZ2025.Core.Model;
using MLZ2025.Core.Services;

namespace MLZ2025.Core.Tests
{
    [TestFixture]
    internal class SqliteTests
    {
        [Test]
        public void TestGetAndAddData()
        {
            var serviceProvider = CreateServiceProvider();
            using (var dataAccess = serviceProvider.GetRequiredService<DataAccess<DatabaseAddress>>())
            {
                dataAccess.DeleteAll();

                var addressCount = dataAccess.Table().Count();

                Assert.That(addressCount, Is.EqualTo(0));

                var max = dataAccess.Insert(new DatabaseAddress
                {
                    FirstName = "Max",
                    LastName = "Mustermann"
                });

                addressCount = dataAccess.Table().Count();

                Assert.That(addressCount, Is.EqualTo(1));
            }
        }
    }
}
