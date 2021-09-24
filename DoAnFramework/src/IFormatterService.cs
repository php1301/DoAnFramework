

namespace DoAnFramework
{
    public interface IFormatterService<T>
    {
        string Format(T obj);
    }
}
