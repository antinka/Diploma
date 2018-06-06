namespace GameStore.BLL.Filters.Interfaces
{
    public interface IPipeLine<T> where T : class
    {
        T Execute(T input);
    }
}
