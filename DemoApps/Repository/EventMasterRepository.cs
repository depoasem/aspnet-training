using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoApps.Models;

namespace DemoApps.Repository
{
    public class EventMasterRepository : IEventMasterRepository
    {
        private static List<EventsMasterResponse> listEvent = new List<EventsMasterResponse>();
        // private static int ID = 0;

        public EventMasterRepository()
        {
            #region oldCode
            //if (listEvent.Count == 0)
            //{
            //    AddEvent(new EventsMasterResponse { Name = "Event 1", Description = "Event Desc 1", EventDate = Convert.ToDateTime("01/13/2020"), EventExpiredDate = Convert.ToDateTime("01/14/2020") });

            //    AddEvent(new EventsMasterResponse { Name = "Event 2", Description = "Event Desc 2", EventDate = Convert.ToDateTime("01/15/2020"), EventExpiredDate = Convert.ToDateTime("01/16/2020") });

            //    AddEvent(new EventsMasterResponse { Name = "Event 3", Description = "Event Desc 3", EventDate = Convert.ToDateTime("01/17/2020"), EventExpiredDate = Convert.ToDateTime("01/18/2020") });
            //}

            #endregion
        }

        public EventsMasterResponse AddEvent(EventsMasterResponse item)
        {
            // EventsMasterResponse result = new EventsMasterResponse();
            #region oldCode
            // item.ID = id++;
            // listEvent.Add(item);
            // return item;
            #endregion
            MasterEvent masterEvent = new MasterEvent();
            masterEvent.EventName = item.Name;
            masterEvent.EventDescription = item.Description;
            masterEvent.EventDate = item.EventDate;
            masterEvent.EventDateExpired = item.EventExpiredDate;

            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                ctx.MasterEvents.Add(masterEvent);
                ctx.SaveChanges();
            }

            return item;

        }

        public bool DeleteEvent(int id)
        {
            bool result = true;

            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                var masterEvent = ctx.MasterEvents.Where(x => x.Id == id).FirstOrDefault();
                if(masterEvent != null)
                {
                    ctx.MasterEvents.Remove(masterEvent);
                    ctx.SaveChanges();
                }
                else
                {
                    result = false;
                }
            }

            return result;
            #region oldCode
            // listEvent.RemoveAll(x => x.ID == id);
            // return result;
            #endregion
        }

        public IEnumerable<EventsMasterResponse> GetAllEvent()
        {
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                listEvent = (from a in ctx.MasterEvents
                             // join b in ctx.Participants on a.Id equals b.MasterEventId
                             // where a.Id = id
                             select new EventsMasterResponse
                             {
                                 ID = a.Id,
                                 Name = a.EventName,
                                 Description = a.EventDescription,
                                 EventDate = a.EventDate,
                                 EventExpiredDate = a.EventDateExpired
                             }).ToList();

            }

            return listEvent;
        }

        public EventsMasterResponse GetEvents(int id)
        {

            // return listEvent.FirstOrDefault(x => x.ID == id);
            EventsMasterResponse res = new EventsMasterResponse();
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                res = ctx.MasterEvents.Where(x => x.Id == id).Select(y => new EventsMasterResponse
                {
                    ID = y.Id,
                    Name = y.EventName,
                    Description = y.EventDescription,
                    EventDate = y.EventDate,
                    EventExpiredDate = y.EventDateExpired
                }).FirstOrDefault();
            }
            return res;
        }

        public bool UpdateEvent(EventsMasterResponse item)
        {
            #region oldCode
            //int index = listEvent.FindIndex(p => p.ID == item.ID);
            //if (index == -1)
            //{
            //    return false;
            //}
            //listEvent.RemoveAt(index);
            //listEvent.Add(item);
            //return true;
            #endregion
            MasterEvent masterEvent = new MasterEvent();
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                masterEvent = ctx.MasterEvents.Where(x => x.Id == item.ID).FirstOrDefault();
                if (masterEvent != null)
                {
                    masterEvent.EventName = item.Name;
                    masterEvent.EventDescription = item.Description;
                    masterEvent.EventDate = item.EventDate;
                    masterEvent.EventDateExpired = item.EventExpiredDate;

                    ctx.MasterEvents.Attach(masterEvent);

                    ctx.Entry(masterEvent).Property("EventName").IsModified = true;
                    ctx.Entry(masterEvent).Property("EventDescription").IsModified = true;
                    ctx.Entry(masterEvent).Property("EventDate").IsModified = true;
                    ctx.Entry(masterEvent).Property("EventDateExpired").IsModified = true;

                    ctx.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}