namespace DISS_2.BackEnd.Core.Objects;

public abstract class Sink<T>(SimCore core)
{
    protected SimCore Core = core;

    public void SinkItem(T item)
    {
        BeforeSink(item);
        AfterSink(item);
    }

    protected virtual void AfterSink(T item)
    {
    }

    protected virtual void BeforeSink(T item)
    {
    }
}