namespace DISS_2.BackEnd.Core;

public class EventCalendar
{
    public PriorityQueue<Event, int> Events { get; set; } = new();

    public void PlanNewEvent(Event newEvent)
    {
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
}