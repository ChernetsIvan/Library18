using System;
using System.Collections.Generic;
using Library.Model.Models;

namespace Library.API.Utility
{
    public class BookComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            return x != null && y != null && x.BookId.Equals(y.BookId);
        }

        public int GetHashCode(Book obj)
        {
            return obj.BookId.GetHashCode();
        }
    }
}