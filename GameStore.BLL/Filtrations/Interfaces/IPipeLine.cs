namespace GameStore.BLL.Filtration.Interfaces
{
    public interface IPipeLine<T> where T : class
    {
        T Execute(T input);
    }
}
