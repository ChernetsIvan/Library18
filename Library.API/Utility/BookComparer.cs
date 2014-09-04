using System;
using System.Collections.Generic;
using Library.Domain.Models;

namespace Library.API.Utility
{
    public class BookComparer : IEqualityComparer<BookModel>
    {
        public bool Equals(BookModel x, BookModel y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.BookId.Equals(y.BookId);
        }

        public int GetHashCode(BookModel obj)
        {
            return obj.BookId.GetHashCode();
        }
    }
}