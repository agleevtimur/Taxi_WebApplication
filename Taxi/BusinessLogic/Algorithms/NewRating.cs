namespace BusinessLogic
{
    public class NewRating : INewRating
    {
        public double GetNewRating(int countOfRates, double rating, int newRating)
        {
            return (countOfRates * rating + newRating) / (countOfRates + 1);
        }

        public int GetNewCountOfRates(int countofRates)
        {
            var newCount = countofRates + 1;
            return newCount;
        }
    }
}
