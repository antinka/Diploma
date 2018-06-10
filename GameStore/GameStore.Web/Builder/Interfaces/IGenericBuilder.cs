namespace GameStore.Web.Builder.Interfaces
{
    public interface IGenericBuilder<T> where T : class
    {
        T Build();

        T Rebuild(T t);
    }
}
