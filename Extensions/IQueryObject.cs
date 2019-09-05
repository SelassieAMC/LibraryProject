namespace Biblioteca.Extensions
{
    public interface IQueryObject
    {
        int Page { get; set; }
        byte PageSize { get; set; }
    }
}