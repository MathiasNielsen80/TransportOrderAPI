using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests
{
    public class FakeOutbox : IOutbox
    {
        public List<object> OutboxedEvents = new List<object>();

        public void OutboxEvents(List<IDomainEvent> uncommittedEvents)
        {
            OutboxedEvents.AddRange(uncommittedEvents);
            uncommittedEvents.Clear();
        }
    }
}
