using BayraktarlarWebsite.UI.Helpers.Abstract;
using System;

namespace BayraktarlarWebsite.UI.Helpers.Concrete
{
    public class LetTimeCalculator : ILetTimeCalculator
    {
      
        public int CalculateLet(int year)
        {
            if (year >= 1 &&  year <= 5)
            {
                return 14;
            }
            else if (year > 5 && year <= 15)
            {
               return 20;
            }
            else if (year > 15)
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
