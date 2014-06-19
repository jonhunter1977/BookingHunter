using System;

namespace BH.Domain
{
    /// <summary>
    /// Data for a facility schedule
    /// </summary>
    public struct FacilitySchedule : IFacilitySchedule, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FacilityScheduleDescription name column
        /// </summary>
        public string FacilityScheduleDescription { get; set; }

        /// <summary>
        /// StartMinuteMonday name column
        /// </summary>
        public int StartMinuteMonday { get; set; }

        /// <summary>
        /// EndMinuteMonday name column
        /// </summary>
        public int EndMinuteMonday { get; set; }

        /// <summary>
        /// MondayFacilityBookLength name column
        /// </summary>
        public int MondayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteTuesday name column
        /// </summary>
        public int StartMinuteTuesday { get; set; }

        /// <summary>
        /// EndMinuteTuesday name column
        /// </summary>
        public int EndMinuteTuesday { get; set; }

        /// <summary>
        /// TuesdayFacilityBookLength name column
        /// </summary>
        public int TuesdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteWednesday name column
        /// </summary>
        public int StartMinuteWednesday { get; set; }

        /// <summary>
        /// EndMinuteWednesday name column
        /// </summary>
        public int EndMinuteWednesday { get; set; }

        /// <summary>
        /// WednesdayFacilityBookLength name column
        /// </summary>
        public int WednesdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteThursday name column
        /// </summary>
        public int StartMinuteThursday { get; set; }

        /// <summary>
        /// EndMinuteThursday name column
        /// </summary>
        public int EndMinuteThursday { get; set; }

        /// <summary>
        /// ThursdayFacilityBookLength name column
        /// </summary>
        public int ThursdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteFriday name column
        /// </summary>
        public int StartMinuteFriday { get; set; }

        /// <summary>
        /// EndMinuteFriday name column
        /// </summary>
        public int EndMinuteFriday { get; set; }

        /// <summary>
        /// FridayFacilityBookLength name column
        /// </summary>
        public int FridayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteSaturday name column
        /// </summary>
        public int StartMinuteSaturday { get; set; }

        /// <summary>
        /// EndMinuteSaturday name column
        /// </summary>
        public int EndMinuteSaturday { get; set; }

        /// <summary>
        /// SaturdayFacilityBookLength name column
        /// </summary>
        public int SaturdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteSunday name column
        /// </summary>
        public int StartMinuteSunday { get; set; }

        /// <summary>
        /// EndMinuteSunday name column
        /// </summary>
        public int EndMinuteSunday { get; set; }

        /// <summary>
        /// SundayFacilityBookLength name column
        /// </summary>
        public int SundayFacilityBookLength { get; set; }
    }
}
