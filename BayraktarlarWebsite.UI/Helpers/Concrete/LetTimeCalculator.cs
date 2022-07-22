using BayraktarlarWebsite.UI.Helpers.Abstract;
using System;

namespace BayraktarlarWebsite.UI.Helpers.Concrete
{
    public class LetTimeCalculator : ILetTimeCalculator
    {
      
        public int CalculateLet(int year)
        {
            if (DateTime.Now.Year - year >= 1 && DateTime.Now.Year - year <= 5)
            {
                return 14;
            }
            else if (DateTime.Now.Year - year > 5 && DateTime.Now.Year - year <= 15)
            {
               return 20;
            }
            else if (DateTime.Now.Year - year > 15)
            {
                return 26;
            }
            else
            {
               return 0;
            }
        }
    }
}
