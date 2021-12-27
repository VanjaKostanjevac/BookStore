<Query Kind="Program">
  <Reference Relative="..\bin\Autofac.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Autofac.dll</Reference>
  <Reference Relative="..\bin\EntityFramework.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\EntityFramework.dll</Reference>
  <Reference Relative="..\bin\EntityFramework.SqlServer.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\EntityFramework.SqlServer.dll</Reference>
  <Reference Relative="..\bin\Bookstore.Service.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Bookstore.Service.dll</Reference>
  <Reference>..\bin\Generated\ServerDom.Orm.dll</Reference>
  <Reference>..\bin\Generated\ServerDom.Repositories.dll</Reference>
  <Reference Relative="..\bin\NLog.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\NLog.dll</Reference>
  <Reference Relative="..\bin\Oracle.ManagedDataAccess.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Oracle.ManagedDataAccess.dll</Reference>
  <Reference>..\bin\Rhetos.AspNetFormsAuth.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Dom.DefaultConcepts.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Dom.DefaultConcepts.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Dom.DefaultConcepts.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Dom.DefaultConcepts.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Dsl.DefaultConcepts.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Dsl.DefaultConcepts.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Processing.DefaultCommands.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Processing.DefaultCommands.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Configuration.Autofac.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Configuration.Autofac.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Dom.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Dom.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Dsl.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Dsl.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Logging.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Logging.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Persistence.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Persistence.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Processing.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Processing.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Security.Interfaces.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Security.Interfaces.dll</Reference>
  <Reference Relative="..\bin\Rhetos.TestCommon.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.TestCommon.dll</Reference>
  <Reference Relative="..\bin\Rhetos.Utilities.dll">C:\Users\vkostanjevac\Desktop\Day1-Rhetos\Bookstore\Bookstore.Service\bin\Rhetos.Utilities.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.AccountManagement.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Namespace>Oracle.ManagedDataAccess.Client</Namespace>
  <Namespace>Rhetos.Configuration.Autofac</Namespace>
  <Namespace>Rhetos.Dom</Namespace>
  <Namespace>Rhetos.Dom.DefaultConcepts</Namespace>
  <Namespace>Rhetos.Dsl</Namespace>
  <Namespace>Rhetos.Dsl.DefaultConcepts</Namespace>
  <Namespace>Rhetos.Logging</Namespace>
  <Namespace>Rhetos.Persistence</Namespace>
  <Namespace>Rhetos.Security</Namespace>
  <Namespace>Rhetos.Utilities</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.DirectoryServices</Namespace>
  <Namespace>System.DirectoryServices.AccountManagement</Namespace>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Reflection</Namespace>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
  <Namespace>System.Text</Namespace>
  <Namespace>System.Xml</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
  <Namespace>Autofac</Namespace>
  <Namespace>Rhetos.TestCommon</Namespace>
  <Namespace>Rhetos</Namespace>
</Query>

void Main()
{
	string applicationFolder = Path.GetDirectoryName(Util.CurrentQueryPath);
	ConsoleLogger.MinLevel = EventType.Info; // Use EventType.Trace for more detailed log.
	
	using (var scope = ProcessContainer.CreateScope(applicationFolder))
    {
        var context = scope.Resolve<Common.ExecutionContext>();
        var repository = context.Repository;
		
		//Load Data
		
		var allBooks = repository.Bookstore.Book.Load();
		allBooks.Dump();

		var someBooks = repository.Bookstore.Book.Load(book => book.Title.StartsWith("The"));
		someBooks.Dump();
		

		//Load data assignment
		
		var Books = repository.Bookstore.Book.Load();
		
		List<string> BooksAndAuthors = new List<string>();
		
		foreach (var book in Books)
		{
		var persons = repository.Bookstore.Person.Load(x => x.ID == book.AuthorID );
		persons.Dump("Persons");
		}


		//Action concept assignment
		
		var actionParameter = new Bookstore.InsertManyBooks
		{
			NumberOfBooks = 7,
			TitlePrefix = "A Song of Ice and Fire"
		};
		repository.Bookstore.InsertManyBooks.Execute(actionParameter);


		//Query assignment
		
		var queryAssignment = repository.Bookstore.Book.Query();

		var queryAssignment2 = queryAssignment.Select(a => new {a.NumberOfPages, a.Author.Name} ); 
		queryAssignment2.Dump("Query assignment");
		
		
		//Query assignment ToString()
		
		var items2 = queryAssignment2.ToString();
		items2.Dump("ToString() method to print SQL query");
		
		
		//Query Data
		
		var query = repository.Bookstore.Book.Query();

		var query2 = query
		    .Where(b => b.Title.StartsWith("B"))
		    .Select(b => new { b.Title, b.Author.Name });


		//ComposableFilterBy LongBooks

		var filterParameter = new Bookstore.LongBooks();
		var queryLongBooks = repository.Bookstore.Book.Query(filterParameter);
		queryLongBooks.ToString().Dump("ComposableFilterBy"); // Print the SQL query.
		queryLongBooks.ToSimple().Dump("ComposableFilterBy"); // Load and print the books.

		
		//FilterBy 
		
		
		
		
		// Entity Framework overrides ToString to return the generated SQL query.
		
		query.ToString().Dump("Generated SQL (query)");
		query2.ToString().Dump("Generated SQL (query2)");


		// ToList will force Entity Framework to load the data from the database.
		var items = query2.ToList();
		items.Dump();
		
		var query3 = repository.Common.Claim.Query()
		.Where(c => c.ClaimResource.StartsWith("Common.") && c.ClaimRight == "New");

		query3.ToString().Dump("Claims query SQL");
		query3.ToList().Dump("Claims query items"); // With navigation properties.

		query3.ToSimple().ToString().Dump("Claims ToSimple SQL"); // Same as above.
		query3.ToSimple().ToList().Dump("Claims ToSimple items"); // Without navigation properties.
		
		
		//Query Data
            
        Console.WriteLine("Done.");
		
		//scope.CommitAndClose(); // Database transaction is rolled back by default.
    }
}