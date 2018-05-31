namespace GameStore.BLL.Filters.GameFilters.Interfaces
{
    public interface IPipeLine<T> where T : class
    {
        T Execute(T input);
    }
}
