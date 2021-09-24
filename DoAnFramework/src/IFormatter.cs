namespace DoAnFramework
{
    public interface IFormatter
    {
        IFormatterService<T> Get<T>();
    }
}
