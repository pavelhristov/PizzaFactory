namespace PizzaFactory.WebClient.Helpers.Contracts
{
    public interface ICacheProvider
    {
        object GetItem(string key);

        void SetItem(string key, object obj, double minDuration = 10);
    }
}
