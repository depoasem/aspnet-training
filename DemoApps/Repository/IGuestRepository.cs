using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApps.Models;

namespace DemoApps.Repository
{
    public interface IGuestRepository
    {
        GuestResponse AddRsvp(GuestResponse item);
        bool DeleteRsvp(int id);
        IEnumerable<GuestResponse> GetAllRsvp();
        GuestResponse GetRsvps(int id);
        bool UpdateRsvp(GuestResponse item);

    }
}
