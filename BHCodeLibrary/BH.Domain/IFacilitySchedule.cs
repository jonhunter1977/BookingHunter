using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a facility schedule
    /// </summary>
    public interface IFacilitySchedule : IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// FacilityScheduleDescription name column
        /// </summary>
        string FacilityScheduleDescription { get; set; }

        /// <summary>
        /// StartMinuteMonday name column
        /// </summary>
        int StartMinuteMonday { get; set; }

        /// <summary>
        /// EndMinuteMonday name column
        /// </summary>
        int EndMinuteMonday { get; set; }

        /// <summary>
        /// MondayFacilityBookLength name column
        /// </summary>
        int MondayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteTuesday name column
        /// </summary>
        int StartMinuteTuesday { get; set; }

        /// <summary>
        /// EndMinuteTuesday name column
        /// </summary>
        int EndMinuteTuesday { get; set; }

        /// <summary>
        /// TuesdayFacilityBookLength name column
        /// </summary>
        int TuesdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteWednesday name column
        /// </summary>
        int StartMinuteWednesday { get; set; }

        /// <summary>
        /// EndMinuteWednesday name column
        /// </summary>
        int EndMinuteWednesday { get; set; }

        /// <summary>
        /// WednesdayFacilityBookLength name column
        /// </summary>
        int WednesdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteThursday name column
        /// </summary>
        int StartMinuteThursday { get; set; }

        /// <summary>
        /// EndMinuteThursday name column
        /// </summary>
        int EndMinuteThursday { get; set; }

        /// <summary>
        /// ThursdayFacilityBookLength name column
        /// </summary>
        int ThursdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteFriday name column
        /// </summary>
        int StartMinuteFriday { get; set; }

        /// <summary>
        /// EndMinuteFriday name column
        /// </summary>
        int EndMinuteFriday { get; set; }

        /// <summary>
        /// FridayFacilityBookLength name column
        /// </summary>
        int FridayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteSaturday name column
        /// </summary>
        int StartMinuteSaturday { get; set; }

        /// <summary>
        /// EndMinuteSaturday name column
        /// </summary>
        int EndMinuteSaturday { get; set; }

        /// <summary>
        /// SaturdayFacilityBookLength name column
        /// </summary>
        int SaturdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteSunday name column
        /// </summary>
        int StartMinuteSunday { get; set; }

        /// <summary>
        /// EndMinuteSunday name column
        /// </summary>
        int EndMinuteSunday { get; set; }

        /// <summary>
        /// SundayFacilityBookLength name column
        /// </summary>
        int SundayFacilityBookLength { get; set; }
    }
}
