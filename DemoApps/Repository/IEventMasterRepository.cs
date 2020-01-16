using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApps.Models;

namespace DemoApps.Repository
{
    public interface IEventMasterRepository
    {
        EventsMasterResponse AddEvent(EventsMasterResponse item);
        bool DeleteEvent(int id);
        IEnumerable<EventsMasterResponse> GetAllEvent();
        EventsMasterResponse GetEvents(int id);
        bool UpdateEvent(EventsMasterResponse item);

    }
}
