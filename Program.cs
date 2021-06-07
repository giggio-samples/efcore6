using System;
using System.Linq;
using efconsole1;
using Microsoft.EntityFrameworkCore;
using static Spectre.Console.AnsiConsole;

using var db = new BloggingContext();
db.Database.Migrate();

// Create
MarkupLine("[blue]Inserting a new blog[/]");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
db.SaveChanges();
MarkupLine("[green]Blog inserted.[/]");

// Read
MarkupLine("[blue]Querying for a blog[/]");
var blog = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();
MarkupLine("[green]Got a blog.[/]");

// Update
MarkupLine("[blue]Updating the blog and adding a post[/]");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
db.SaveChanges();
MarkupLine("[green]Blog updated, post created.[/]");
MarkupLine("[blue]Adding another post, but it will fail:[/]");
var tooLongPost = new Post { Title = new String('x', 200), Content = "I wrote an app using EF Core!" };
blog.Posts.Add(tooLongPost);
try
{
    db.SaveChanges();
}
catch (DbUpdateException ex)
{
    MarkupLine($"Error: [red]{ex.Message}[/]");
    MarkupLine($"Inner Error: [red]{ex.InnerException?.Message}[/]");
    blog.Posts.Remove(tooLongPost);
}

// Delete
MarkupLine("[blue]Deleting the blog...[/]");
db.Remove(blog);
db.SaveChanges();
MarkupLine("[green]Blog deleted.[/]");