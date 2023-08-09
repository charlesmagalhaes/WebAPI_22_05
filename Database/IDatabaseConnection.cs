namespace Database
{
    public interface IDatabaseConnection<T>
    {
        T GetConnection();
    }
    
}
