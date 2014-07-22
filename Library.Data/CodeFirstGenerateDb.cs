using System.Collections.Generic;
using System.Data.Entity;
using Library.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.Data
{
    public class CodeFirstGenerateDb : DropCreateDatabaseIfModelChanges<LibraryEntities>
    {
        protected override void Seed(LibraryEntities context)
        {        
            new List<Author>
            {
                new Author { AuthorId = "test", Name = "test", LastName = "test"}
            }.ForEach(a=> context.Authors.Add(a));

            context.Commit();
        }
    }
}
