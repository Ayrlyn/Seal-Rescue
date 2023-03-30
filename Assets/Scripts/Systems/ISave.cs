public interface ISave<T>
{
    public T Save();
    public void Load(T state);
}
