using System;
using System.Data.Linq.Mapping;

namespace BH.Domain
{
    /// <summary>
    /// Data for a facility schedule
    /// </summary>
    public class FacilitySchedule : IFacilitySchedule, IDbItentity
    {
        /// <summary>
        /// Customer Id - identity column
        /// </summary>
        [Column
            (
                Name = "Id",
                IsPrimaryKey = true,
                IsDbGenerated = true
            )
        ]
        public int Id { get; set; }

        /// <summary>
        /// FacilityScheduleDescription name column
        /// </summary>
        [Column(Name = "FacilityScheduleDescription")]
        public string FacilityScheduleDescription { get; set; }

        /// <summary>
        /// StartMinuteMonday name column
        /// </summary>
        [Column(Name = "StartMinuteMonday")]
        public int StartMinuteMonday { get; set; }

        /// <summary>
        /// EndMinuteMonday name column
        /// </summary>
        [Column(Name = "EndMinuteMonday")]
        public int EndMinuteMonday { get; set; }

        /// <summary>
        /// MondayFacilityBookLength name column
        /// </summary>
        [Column(Name = "MondayFacilityBookLength")]
        public int MondayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteTuesday name column
        /// </summary>
        [Column(Name = "StartMinuteTuesday")]
        public int StartMinuteTuesday { get; set; }

        /// <summary>
        /// EndMinuteTuesday name column
        /// </summary>
        [Column(Name = "EndMinuteTuesday")]
        public int EndMinuteTuesday { get; set; }

        /// <summary>
        /// TuesdayFacilityBookLength name column
        /// </summary>
        [Column(Name = "TuesdayFacilityBookLength")]
        public int TuesdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteWednesday name column
        /// </summary>
        [Column(Name = "StartMinuteWednesday")]
        public int StartMinuteWednesday { get; set; }

        /// <summary>
        /// EndMinuteWednesday name column
        /// </summary>
        [Column(Name = "EndMinuteWednesday")]
        public int EndMinuteWednesday { get; set; }

        /// <summary>
        /// WednesdayFacilityBookLength name column
        /// </summary>
        [Column(Name = "WednesdayFacilityBookLength")]
        public int WednesdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteThursday name column
        /// </summary>
        [Column(Name = "StartMinuteThursday")]
        public int StartMinuteThursday { get; set; }

        /// <summary>
        /// EndMinuteThursday name column
        /// </summary>
        [Column(Name = "EndMinuteThursday")]
        public int EndMinuteThursday { get; set; }

        /// <summary>
        /// ThursdayFacilityBookLength name column
        /// </summary>
        [Column(Name = "ThursdayFacilityBookLength")]
        public int ThursdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteFriday name column
        /// </summary>
        [Column(Name = "StartMinuteFriday")]
        public int StartMinuteFriday { get; set; }

        /// <summary>
        /// EndMinuteFriday name column
        /// </summary>
        [Column(Name = "EndMinuteFriday")]
        public int EndMinuteFriday { get; set; }

        /// <summary>
        /// FridayFacilityBookLength name column
        /// </summary>
        [Column(Name = "FridayFacilityBookLength")]
        public int FridayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteSaturday name column
        /// </summary>
        [Column(Name = "StartMinuteSaturday")]
        public int StartMinuteSaturday { get; set; }

        /// <summary>
        /// EndMinuteSaturday name column
        /// </summary>
        [Column(Name = "EndMinuteSaturday")]
        public int EndMinuteSaturday { get; set; }

        /// <summary>
        /// SaturdayFacilityBookLength name column
        /// </summary>
        [Column(Name = "SaturdayFacilityBookLength")]
        public int SaturdayFacilityBookLength { get; set; }

        /// <summary>
        /// StartMinuteSunday name column
        /// </summary>
        [Column(Name = "StartMinuteSunday")]
        public int StartMinuteSunday { get; set; }

        /// <summary>
        /// EndMinuteSunday name column
        /// </summary>
        [Column(Name = "EndMinuteSunday")]
        public int EndMinuteSunday { get; set; }

        /// <summary>
        /// SundayFacilityBookLength name column
        /// </summary>
        [Column(Name = "SundayFacilityBookLength")]
        public int SundayFacilityBookLength { get; set; }
    }
}
