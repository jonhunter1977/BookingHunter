using System;
using System.Collections.Generic;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Class for getting facility schedule data from the database
    /// </summary>
    internal class FacilityScheduleRepositorySqlServer : IFacilityScheduleRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public FacilityScheduleRepositorySqlServer(string cfgConnectionString)
        {
            if (cfgConnectionString != null) 
                _dataEngine = new DataQuerySqlServer(cfgConnectionString);

            if (_dataEngine == null) throw new Exception("Cfg Database query engine is null");

            if (!_dataEngine.DatabaseConnected) throw new Exception("Cfg Database query engine is not connected");
        }

        public IList<FacilitySchedule> GetAll()
        {
            var facilityScheduleList = new List<FacilitySchedule>();

            _sqlToExecute = "SELECT * FROM [dbo].[FacilitySchedule]";

            if(!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("FacilitySchedule - GetAll failed");

            while (_dataEngine.Dr.Read())
            {
                FacilitySchedule facilitySchedule = CreateFacilityScheduleFromData();
                facilityScheduleList.Add(facilitySchedule);
            }

            return facilityScheduleList;
        }

        public FacilitySchedule GetById(int id)
        {
            _sqlToExecute = "SELECT * FROM [dbo].[FacilitySchedule] WHERE Id = " + id.ToString();

            if (!_dataEngine.CreateReaderFromSql(_sqlToExecute))
                throw new Exception("FacilitySchedule - GetById failed");

            if (_dataEngine.Dr.Read())
            {
                FacilitySchedule facilitySchedule = CreateFacilityScheduleFromData();
                return facilitySchedule;
            }
            else
            {
                throw new Exception("FacilitySchedule Id " + id.ToString() + " does not exist in database");
            }            
        }

        public void Save(FacilitySchedule saveThis)
        {
            _sqlToExecute = "INSERT INTO [dbo].[FacilitySchedule] ";
            _sqlToExecute += "([FacilityScheduleDescription]";
            _sqlToExecute += ",[StartMinuteMonday]";
            _sqlToExecute += ",[EndMinuteMonday]";
            _sqlToExecute += ",[MondayFacilityBookLength]";
            _sqlToExecute += ",[StartMinuteTuesday]";
            _sqlToExecute += ",[EndMinuteTuesday]";
            _sqlToExecute += ",[TuesdayFacilityBookLength]";
            _sqlToExecute += ",[StartMinuteWednesday]";
            _sqlToExecute += ",[EndMinuteWednesday]";
            _sqlToExecute += ",[WednesdayFacilityBookLength]";
            _sqlToExecute += ",[StartMinuteThursday]";
            _sqlToExecute += ",[EndMinuteThursday]";
            _sqlToExecute += ",[ThursdayFacilityBookLength]";
            _sqlToExecute += ",[StartMinuteFriday]";
            _sqlToExecute += ",[EndMinuteFriday]";
            _sqlToExecute += ",[FridayFacilityBookLength]";
            _sqlToExecute += ",[StartMinuteSaturday]";
            _sqlToExecute += ",[EndMinuteSaturday]";
            _sqlToExecute += ",[SaturdayFacilityBookLength]";
            _sqlToExecute += ",[StartMinuteSunday]";
            _sqlToExecute += ",[EndMinuteSunday]";
            _sqlToExecute += ",[SundayFacilityBookLength])";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "('" + saveThis.FacilityScheduleDescription + "'";
            _sqlToExecute += "," + saveThis.StartMinuteMonday;
            _sqlToExecute += "," + saveThis.EndMinuteMonday;
            _sqlToExecute += "," + saveThis.MondayFacilityBookLength;
            _sqlToExecute += "," + saveThis.StartMinuteTuesday;
            _sqlToExecute += "," + saveThis.EndMinuteTuesday;
            _sqlToExecute += "," + saveThis.TuesdayFacilityBookLength;
            _sqlToExecute += "," + saveThis.StartMinuteWednesday;
            _sqlToExecute += "," + saveThis.EndMinuteWednesday;
            _sqlToExecute += "," + saveThis.WednesdayFacilityBookLength;
            _sqlToExecute += "," + saveThis.StartMinuteThursday;
            _sqlToExecute += "," + saveThis.EndMinuteThursday;
            _sqlToExecute += "," + saveThis.ThursdayFacilityBookLength;
            _sqlToExecute += "," + saveThis.StartMinuteFriday;
            _sqlToExecute += "," + saveThis.EndMinuteFriday;
            _sqlToExecute += "," + saveThis.FridayFacilityBookLength;
            _sqlToExecute += "," + saveThis.StartMinuteSaturday;
            _sqlToExecute += "," + saveThis.EndMinuteSaturday;
            _sqlToExecute += "," + saveThis.SaturdayFacilityBookLength;
            _sqlToExecute += "," + saveThis.StartMinuteSunday;
            _sqlToExecute += "," + saveThis.EndMinuteSunday;
            _sqlToExecute += "," + saveThis.SundayFacilityBookLength + ")";

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("FacilitySchedule - Save failed");
        }

        public void Delete(FacilitySchedule deleteThis)
        {
            _sqlToExecute = "DELETE FROM [dbo].[FacilitySchedule] WHERE Id = " + deleteThis.Id.ToString();

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("FacilitySchedule - Delete failed");
        }

        /// <summary>
        /// Creates the object from the data returned from the database
        /// </summary>
        /// <returns></returns>
        private FacilitySchedule CreateFacilityScheduleFromData()
        {
            var facilitySchedule = new FacilitySchedule
            {
                Id = int.Parse(_dataEngine.Dr["Id"].ToString()),
                FacilityScheduleDescription = _dataEngine.Dr["FacilityScheduleDescription"].ToString(),
                StartMinuteMonday = int.Parse(_dataEngine.Dr["StartMinuteMonday"].ToString()),
                EndMinuteMonday = int.Parse(_dataEngine.Dr["EndMinuteMonday"].ToString()),
                MondayFacilityBookLength = int.Parse(_dataEngine.Dr["MondayFacilityBookLength"].ToString()),
                StartMinuteTuesday = int.Parse(_dataEngine.Dr["StartMinuteTuesday"].ToString()),
                EndMinuteTuesday = int.Parse(_dataEngine.Dr["EndMinuteTuesday"].ToString()),
                TuesdayFacilityBookLength = int.Parse(_dataEngine.Dr["TuesdayFacilityBookLength"].ToString()),
                StartMinuteWednesday = int.Parse(_dataEngine.Dr["StartMinuteWednesday"].ToString()),
                EndMinuteWednesday = int.Parse(_dataEngine.Dr["EndMinuteWednesday"].ToString()),
                WednesdayFacilityBookLength = int.Parse(_dataEngine.Dr["WednesdayFacilityBookLength"].ToString()),
                StartMinuteThursday = int.Parse(_dataEngine.Dr["StartMinuteThursday"].ToString()),
                EndMinuteThursday = int.Parse(_dataEngine.Dr["EndMinuteThursday"].ToString()),
                ThursdayFacilityBookLength = int.Parse(_dataEngine.Dr["ThursdayFacilityBookLength"].ToString()),
                StartMinuteFriday = int.Parse(_dataEngine.Dr["StartMinuteFriday"].ToString()),
                EndMinuteFriday = int.Parse(_dataEngine.Dr["EndMinuteFriday"].ToString()),
                FridayFacilityBookLength = int.Parse(_dataEngine.Dr["FridayFacilityBookLength"].ToString()),
                StartMinuteSaturday = int.Parse(_dataEngine.Dr["StartMinuteSaturday"].ToString()),
                EndMinuteSaturday = int.Parse(_dataEngine.Dr["EndMinuteSaturday"].ToString()),
                SaturdayFacilityBookLength = int.Parse(_dataEngine.Dr["SaturdayFacilityBookLength"].ToString()),
                StartMinuteSunday = int.Parse(_dataEngine.Dr["StartMinuteSunday"].ToString()),
                EndMinuteSunday = int.Parse(_dataEngine.Dr["EndMinuteSunday"].ToString()),
                SundayFacilityBookLength = int.Parse(_dataEngine.Dr["SundayFacilityBookLength"].ToString())
            };

            return facilitySchedule;
        }
    }
}
