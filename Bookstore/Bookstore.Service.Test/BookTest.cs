using Autofac;
using Bookstore.Service.Test.Tools;
using Common.Queryable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhetos;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.TestCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Test
{
    [TestClass]
    public class BookTest
    {
        /// <summary>
        /// BookInfo.NumberOfComments is persisted in a cache table and should be automatically updated
        /// each a comment is added or deleted.
        /// </summary>
        [TestMethod]
        public void AutomaticallyUpdateNumberOfComments()
        {
            using (var scope = TestScope.Create())
            {
                var repository = scope.Resolve<Common.DomRepository>();

                var book = new Book { Title = Guid.NewGuid().ToString() };
                repository.Bookstore.Book.Insert(book);

                int? readNumberOfComments() => repository.Bookstore.BookInfo
                    .Query(bi => bi.ID == book.ID)
                    .Select(bi => bi.NumberOfComments)
                    .Single();

                Assert.AreEqual(0, readNumberOfComments());

                var c1 = new Comment { BookID = book.ID, Text = "c1" };
                var c2 = new Comment { BookID = book.ID, Text = "c2" };
                var c3 = new Comment { BookID = book.ID, Text = "c3" };

                repository.Bookstore.Comment.Insert(c1);
                Assert.AreEqual(1, readNumberOfComments());

                repository.Bookstore.Comment.Insert(c2, c3);
                Assert.AreEqual(3, readNumberOfComments());

                repository.Bookstore.Comment.Delete(c1);
                Assert.AreEqual(2, readNumberOfComments());

                repository.Bookstore.Comment.Delete(c2, c3);
                Assert.AreEqual(0, readNumberOfComments());
            }
        }

        [TestMethod]
        public void CommonMisspellingValidation()
        {
            using (var scope = TestScope.Create())
            {
                var repository = scope.Resolve<Common.DomRepository>();

                var book = new Book { Title = "x curiosity y" };

                TestUtility.ShouldFail<UserException>(
                    () => repository.Bookstore.Book.Insert(book),
                    "You can not insert the book with title Curiosity!");
            }
        }

        [TestMethod]
        public void CommonMisspellingValidation_DirectFilter()
        {
            using (var scope = TestScope.Create())
            {
                var repository = scope.Resolve<Common.DomRepository>();

                var books = new[]
                {
                    new Bookstore_Book { Title = "spirit" },
                    new Bookstore_Book { Title = "opportunity" },
                    new Bookstore_Book { Title = "Curiosity" },
                    new Bookstore_Book { Title = "Curiosity2" }
                }.AsQueryable();

                var invalidBooks = repository.Bookstore.Book.Filter(books, new CommonMisspelling());

                Assert.AreEqual("Curiosity, Curiosity2", TestUtility.DumpSorted(invalidBooks, book => book.Title));

            }
        }

        [TestMethod]
        public void ParallelCodeGeneration()
        {
            DeleteUnitTestBooks(); // This test needs to commit changes, so it is required to clean up any remaining previous test data, in case the test was canceled without ClassCleanup.

            // Prepare test data:

            var books = new[]
            {
                // Using specific prefix to reduce chance of conflicts with any existing data.
                new Book { Code = $"{UnitTestBookCodePrefix}+++", Title = Guid.NewGuid().ToString() },
                new Book { Code = $"{UnitTestBookCodePrefix}+++", Title = Guid.NewGuid().ToString() },
                new Book { Code = $"{UnitTestBookCodePrefix}ABC+", Title = Guid.NewGuid().ToString() },
                new Book { Code = $"{UnitTestBookCodePrefix}ABC+", Title = Guid.NewGuid().ToString() }
            };

            // Insert in parallel:

            for (int retry = 0; retry < 3; retry++) // Running the test multiple times to avoid false positive, since the results are nondeterministic.
            {
                Parallel.ForEach(books, book =>
                {
                    // Each scope represent one web request of the main application, executed in its own separate transaction.
                    // The main application should support parallel web requests.
                    using (var scope = TestScope.Create())
                    {
                        var repository = scope.Resolve<Common.DomRepository>();
                        repository.Bookstore.Book.Insert(book);
                        scope.CommitAndClose(); // Changes are committed to database, to make the test with parallel transactions more realistic.
                    }
                });

                // Review the inserted data:

                using (var scope = TestScope.Create())
                {
                    var repository = scope.Resolve<Common.DomRepository>();
                    var booksFromDb = repository.Bookstore.Book.Load(book => book.Code.StartsWith(UnitTestBookCodePrefix));
                    Assert.AreEqual(
                        $"{UnitTestBookCodePrefix}001, {UnitTestBookCodePrefix}002, {UnitTestBookCodePrefix}ABC1, {UnitTestBookCodePrefix}ABC2",
                        TestUtility.DumpSorted(booksFromDb, book => book.Code));
                }

                DeleteUnitTestBooks();
            }
        }

        private const string UnitTestBookCodePrefix = "UnitTestBooks";

        private void DeleteUnitTestBooks()
        {
            using (var scope = TestScope.Create())
            {
                var repository = scope.Resolve<Common.DomRepository>();
                var testBooks = repository.Bookstore.Book.Load(book => book.Code.StartsWith(UnitTestBookCodePrefix));
                repository.Bookstore.Book.Delete(testBooks);
                scope.CommitAndClose();
            }
        }



    }
}
