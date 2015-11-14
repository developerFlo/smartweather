using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;

namespace Smartweather.Sevices
{
    public class CalendarService : ICalendarService
    {
        public async Task<IList<string>> GetCalendarEntryLocations(int upcommingDays)
        {
            IList<string> locations = new List<string>();

            AppointmentStore store = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AllCalendarsReadOnly);
            if(store != null)
            {
                var appointments = await store.FindAppointmentsAsync(
                    new DateTimeOffset(DateTime.Today), TimeSpan.FromDays(upcommingDays));
                foreach(var appointment in appointments)
                {
                    if(!string.IsNullOrWhiteSpace(appointment.Location) && !locations.Contains(appointment.Location))
                        locations.Add(appointment.Location);
                }
            }

            return locations;

        }
    }
}
