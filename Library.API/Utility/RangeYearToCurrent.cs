using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Utility
{
    internal class RangeYearToCurrent : RangeAttribute
    {
        public RangeYearToCurrent(int from) 
            : base(from, DateTime.Today.Year) { }  //Определение конструктора базового класса, который должен вызываться при создании экземпляров производного класса.
    }
}