using Board.Domain.Categories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Board.Tests;

public class CategoryIdTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {
                new List<Category>()
                {
                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "first",
                        ParentId = Guid.NewGuid(),
                    },



                    new Category
                    {
                        Id = Guid.Parse("09258252-083B-439A-931E-828E7F1B4F17"),
                        Name = "second",
                        ParentId = Guid.NewGuid(),
                    },


                    new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = "third",
                        ParentId = Guid.NewGuid(),
                    }

                }

        };
    }


    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
}
