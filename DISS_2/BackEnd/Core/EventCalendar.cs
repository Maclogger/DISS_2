using System.Text;

namespace DISS_2.BackEnd.Core;

public class EventCalendar
{
    public SimState SimState { get; }
    public PriorityQueue<Event, int> Events { get; set; } = new();

    public EventCalendar(SimState simState)
    {
        SimState = simState;
    }

    public void PlanNewEvent(Event newEvent)
    {
        string readableCurrentTime = TimeHandler.ToReadableTime(SimState.CurrentSimTime);
        Console.WriteLine($"[{readableCurrentTime}]: {newEvent}");

        Events.Enqueue(newEvent, newEvent.StartTime);
    }

    public Event PopEvent()
    {
        return Events.Dequeue();
    }

    public bool IsEmpty()
    {
        return Events.Count == 0;
    }

    public override string ToString()
    {
        List<Event> events = new();
        foreach (var item in Events.UnorderedItems)
        {
            events.Add(item.Element);
        }

        events.Sort();

        StringBuilder sb = new();
        foreach (Event eventVar in events)
        {
            sb.Append($"{eventVar}");
        }

        return sb.ToString();
    }

    public List<Event> GetSortedEvents()
    {
        List<Event> events = new();
        foreach (var item in Events.UnorderedItems)
        {
            events.Add(item.Element);
        }
        events.Sort();
        return events;
    }
}