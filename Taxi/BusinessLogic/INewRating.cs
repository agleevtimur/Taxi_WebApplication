using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface INewRating
    {
        double GetNewRating(int countOfRates, double rating, int newRating);
        int GetNewCountOfRates(int countofRates);
    }
}
