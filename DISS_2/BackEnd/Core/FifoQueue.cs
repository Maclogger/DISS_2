namespace DISS_2.BackEnd.Core;

public class FifoQueue<T>
{
    private Queue<T> _queue = new();

    public void Enqueue(T item, SimCore sim)
    {
        BeforeEnqueue(item, sim);
        _queue.Enqueue(item);
        AfterEnqueue(item, sim);
    }

    public T Dequeue(SimCore sim)
    {
        BeforeDequeue(sim);
        T item = _queue.Dequeue();
        AfterDequeue(item, sim);
        return item;
    }

    protected virtual void BeforeEnqueue(T item, SimCore sim)
    {
    }

    protected virtual void AfterEnqueue(T item, SimCore sim)
    {
    }

    protected virtual void BeforeDequeue(SimCore sim)
    {
    }

    protected virtual void AfterDequeue(T item, SimCore sim)
    {
    }
}