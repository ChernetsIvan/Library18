using System;
using System.Collections.Generic;
using Library.Model.Models;

namespace Library.Web.Utility
{
    public class BookAuthorComparer : IEqualityComparer<BookAuthor>
    {
        // BookAuthors are equal if their id's are equal.
        public bool Equals(BookAuthor x, BookAuthor y)
        {
            if (Object.Equals(x.BookAuthorId, y.BookAuthorId)) return true;
            return false;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(BookAuthor obj)
        {
            return obj.BookAuthorId.GetHashCode();
        }
    }
}