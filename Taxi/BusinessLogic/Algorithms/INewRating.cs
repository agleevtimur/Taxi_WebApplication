namespace BusinessLogic
{
    public interface INewRating
    {
        double GetNewRating(int countOfRates, double rating, int newRating);
        int GetNewCountOfRates(int countofRates);
    }
}
