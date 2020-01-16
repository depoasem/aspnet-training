using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoApps.Models;

namespace DemoApps.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private static List<GuestResponse> listGuest = new List<GuestResponse>();
        // private static int id = 0;

        public GuestResponse AddRsvp(GuestResponse item)
        {
            Participant dtParticipant = new Participant();
            dtParticipant.NamaLengkap = item.NamaLengkap;
            dtParticipant.Email = item.Email;
            dtParticipant.Handphone = item.Handphone;
            dtParticipant.WillAttend = item.WillAttend;
            dtParticipant.MasterEventId = item.MasterEventID;

            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                ctx.Participants.Add(dtParticipant);
                ctx.SaveChanges();
            }

            return item;
        }

        public bool DeleteRsvp(int id)
        {
            bool result = true;

            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                var dtParticipant = ctx.Participants.Where(x => x.id == id).FirstOrDefault();
                if (dtParticipant != null)
                {
                    ctx.Participants.Remove(dtParticipant);
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

        public IEnumerable<GuestResponse> GetAllRsvp()
        {
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                listGuest = (from a in ctx.Participants
                             select new GuestResponse
                             {
                                 ID = a.id,
                                 NamaLengkap = a.NamaLengkap,
                                 Email = a.Email,
                                 Handphone = a.Handphone,
                                 WillAttend = a.WillAttend,
                                 MasterEventID = a.MasterEventId
                             }).ToList();
            }

            return listGuest;
        }

        public GuestResponse GetRsvps(int id)
        {
            GuestResponse res = new GuestResponse();
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                res = ctx.Participants.Where(x => x.id == id).Select(y => new GuestResponse
                {
                    ID = y.id,
                    NamaLengkap = y.NamaLengkap,
                    Email = y.Email,
                    Handphone = y.Handphone,
                    WillAttend = y.WillAttend,
                    MasterEventID = y.MasterEventId
                }).FirstOrDefault();
            }
            return res;
        }

        public bool UpdateRsvp(GuestResponse item)
        {
            Participant dtParticipant = new Participant();
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                dtParticipant = ctx.Participants.Where(x => x.id == item.ID).FirstOrDefault();
                if(dtParticipant != null)
                {
                    dtParticipant.NamaLengkap = item.NamaLengkap;
                    dtParticipant.Email = item.NamaLengkap;
                    dtParticipant.Handphone = item.Handphone;
                    dtParticipant.WillAttend = item.WillAttend;
                    dtParticipant.MasterEventId = item.MasterEventID;

                    ctx.Participants.Attach(dtParticipant);

                    ctx.Entry(dtParticipant).Property("NamaLengkap").IsModified = true;
                    ctx.Entry(dtParticipant).Property("Email").IsModified = true;
                    ctx.Entry(dtParticipant).Property("Handphone").IsModified = true;
                    ctx.Entry(dtParticipant).Property("WillAttend").IsModified = true;
                    ctx.Entry(dtParticipant).Property("NamaLengkap").IsModified = true;

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