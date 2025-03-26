namespace DISS_2.BackEnd.Core;

public class FifoQueue<T>
{
    private Queue<T> _queue = new();
    private SimCore _sim;

    public FifoQueue(SimCore core)
    {
        _sim = core;
    }

    public void Enqueue(T item)
    {
        BeforeEnqueue(item);
        _queue.Enqueue(item);
        AfterEnqueue(item);
    }

    public T Dequeue()
    {
        BeforeDequeue();
        T item = _queue.Dequeue();
        AfterDequeue(item);
        return item;
    }

    public void Clear()
    {
        _queue.Clear();
    }

    protected virtual void BeforeEnqueue(T item)
    {
    }

    protected virtual void AfterEnqueue(T item)
    {
    }

    protected virtual void BeforeDequeue()
    {
    }

    protected virtual void AfterDequeue(T item)
    {
    }
}