namespace DISS_2.BackEnd.Core.Objects;

public class FifoQueue<T>
{
    private Queue<T> _queue = new();
    protected SimCore Sim;
    public Dictionary<string, int> TypeCounts = new();

    public FifoQueue(SimCore core)
    {
        Sim = core;
    }

    public void Enqueue(T item)
    {
        BeforeEnqueue(item);
        _queue.Enqueue(item);
        if (TypeCounts.ContainsKey(item?.GetType().Name!))
        {
            TypeCounts[item?.GetType().Name!]++;
        }
        else
        {
            TypeCounts.Add(item?.GetType().Name!, 1);
        }
        AfterEnqueue(item);
    }

    public T Dequeue()
    {
        BeforeDequeue();
        T item = _queue.Dequeue();

        if (TypeCounts[item?.GetType().Name!] <= 1)
        {
            TypeCounts.Remove(item?.GetType().Name!);
        }
        else
        {
            TypeCounts[item?.GetType().Name!]--;
        }

        AfterDequeue(item);
        return item;
    }

    public void Clear()
    {
        _queue.Clear();
        TypeCounts.Clear();
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

    public bool IsEmpty()
    {
        return _queue.Count <= 0;
    }

    public int GetCount()
    {
        return _queue.Count;
    }
}