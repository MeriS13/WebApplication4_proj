using Board.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData
{
    public class ForbiddenWordsService : IForbiddenWordsService
    {
        public string[] GetForbiddenWords()
        {
            return new[] {"слово" };  
        }
    }


}
