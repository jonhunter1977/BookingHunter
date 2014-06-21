using System;
using System.Collections.Generic;
using BH.Domain;

namespace BH.DataAccessLayer.SqlServer
{
    /// <summary>
    /// Class for getting facility schedule data from the database
    /// </summary>
    internal class FacilityScheduleRepository : IFacilityScheduleRepository
    {
        private readonly DataQuerySqlServer _dataEngine;
        private string _sqlToExecute;

        public FacilityScheduleRepository(string cfgConnectionString)
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
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", id.ToString());

            _sqlToExecute = "SELECT * FROM [dbo].[FacilitySchedule] WHERE Id = " + _dataEngine.GetParametersForQuery();

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

        public int Insert(FacilitySchedule saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@FacilityBookAheadDays", saveThis.FacilityScheduleDescription);
            _dataEngine.AddParameter("@StartMinuteMonday", saveThis.StartMinuteMonday.ToString());
            _dataEngine.AddParameter("@EndMinuteMonday", saveThis.EndMinuteMonday.ToString());
            _dataEngine.AddParameter("@MondayFacilityBookLength", saveThis.MondayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteTuesday", saveThis.StartMinuteTuesday.ToString());
            _dataEngine.AddParameter("@EndMinuteTuesday", saveThis.EndMinuteTuesday.ToString());
            _dataEngine.AddParameter("@TuesdayFacilityBookLength", saveThis.TuesdayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteWednesday", saveThis.StartMinuteWednesday.ToString());
            _dataEngine.AddParameter("@EndMinuteWednesday", saveThis.EndMinuteWednesday.ToString());
            _dataEngine.AddParameter("@WednesdayFacilityBookLength", saveThis.WednesdayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteThursday", saveThis.StartMinuteThursday.ToString());
            _dataEngine.AddParameter("@EndMinuteThursday", saveThis.EndMinuteThursday.ToString());
            _dataEngine.AddParameter("@ThursdayFacilityBookLength", saveThis.ThursdayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteFriday", saveThis.StartMinuteFriday.ToString());
            _dataEngine.AddParameter("@EndMinuteFriday", saveThis.EndMinuteFriday.ToString());
            _dataEngine.AddParameter("@FridayFacilityBookLength", saveThis.FridayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteSaturday", saveThis.StartMinuteSaturday.ToString());
            _dataEngine.AddParameter("@EndMinuteSaturday", saveThis.EndMinuteSaturday.ToString());
            _dataEngine.AddParameter("@SaturdayFacilityBookLength", saveThis.SaturdayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteSunday", saveThis.StartMinuteSunday.ToString());
            _dataEngine.AddParameter("@EndMinuteSunday", saveThis.EndMinuteSunday.ToString());
            _dataEngine.AddParameter("@SundayFacilityBookLength", saveThis.SundayFacilityBookLength.ToString());

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
            _sqlToExecute += ",[SundayFacilityBookLength]) ";
            _sqlToExecute += "OUTPUT INSERTED.Id ";
            _sqlToExecute += "VALUES ";
            _sqlToExecute += "(";
            _sqlToExecute += _dataEngine.GetParametersForQuery();
            _sqlToExecute += ")";

            int insertedRowId = 0;

            if (!_dataEngine.ExecuteSql(_sqlToExecute, out insertedRowId))
                throw new Exception("FacilitySchedule - Save failed");

            return insertedRowId;
        }

        public void Update(FacilitySchedule saveThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@FacilityBookAheadDays", saveThis.FacilityScheduleDescription);
            _dataEngine.AddParameter("@StartMinuteMonday", saveThis.StartMinuteMonday.ToString());
            _dataEngine.AddParameter("@EndMinuteMonday", saveThis.EndMinuteMonday.ToString());
            _dataEngine.AddParameter("@MondayFacilityBookLength", saveThis.MondayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteTuesday", saveThis.StartMinuteTuesday.ToString());
            _dataEngine.AddParameter("@EndMinuteTuesday", saveThis.EndMinuteTuesday.ToString());
            _dataEngine.AddParameter("@TuesdayFacilityBookLength", saveThis.TuesdayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteWednesday", saveThis.StartMinuteWednesday.ToString());
            _dataEngine.AddParameter("@EndMinuteWednesday", saveThis.EndMinuteWednesday.ToString());
            _dataEngine.AddParameter("@WednesdayFacilityBookLength", saveThis.WednesdayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteThursday", saveThis.StartMinuteThursday.ToString());
            _dataEngine.AddParameter("@EndMinuteThursday", saveThis.EndMinuteThursday.ToString());
            _dataEngine.AddParameter("@ThursdayFacilityBookLength", saveThis.ThursdayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteFriday", saveThis.StartMinuteFriday.ToString());
            _dataEngine.AddParameter("@EndMinuteFriday", saveThis.EndMinuteFriday.ToString());
            _dataEngine.AddParameter("@FridayFacilityBookLength", saveThis.FridayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteSaturday", saveThis.StartMinuteSaturday.ToString());
            _dataEngine.AddParameter("@EndMinuteSaturday", saveThis.EndMinuteSaturday.ToString());
            _dataEngine.AddParameter("@SaturdayFacilityBookLength", saveThis.SaturdayFacilityBookLength.ToString());
            _dataEngine.AddParameter("@StartMinuteSunday", saveThis.StartMinuteSunday.ToString());
            _dataEngine.AddParameter("@EndMinuteSunday", saveThis.EndMinuteSunday.ToString());
            _dataEngine.AddParameter("@SundayFacilityBookLength", saveThis.SundayFacilityBookLength.ToString());

            _sqlToExecute = "UPDATE [dbo].[FacilitySchedule] SET ";
            _sqlToExecute += "[FacilityScheduleDescription] = @FacilityBookAheadDays";
            _sqlToExecute += ",[StartMinuteMonday] = @StartMinuteMonday";
            _sqlToExecute += ",[EndMinuteMonday] = @EndMinuteMonday";
            _sqlToExecute += ",[MondayFacilityBookLength] = @MondayFacilityBookLength";
            _sqlToExecute += ",[StartMinuteTuesday] = @StartMinuteTuesday";
            _sqlToExecute += ",[EndMinuteTuesday] = @EndMinuteTuesday";
            _sqlToExecute += ",[TuesdayFacilityBookLength] = @TuesdayFacilityBookLength";
            _sqlToExecute += ",[StartMinuteWednesday] = @StartMinuteWednesday";
            _sqlToExecute += ",[EndMinuteWednesday] = @EndMinuteWednesday";
            _sqlToExecute += ",[WednesdayFacilityBookLength] = @WednesdayFacilityBookLength";
            _sqlToExecute += ",[StartMinuteThursday] = @StartMinuteThursday";
            _sqlToExecute += ",[EndMinuteThursday] = @EndMinuteThursday";
            _sqlToExecute += ",[ThursdayFacilityBookLength] = @ThursdayFacilityBookLength";
            _sqlToExecute += ",[StartMinuteFriday] = @StartMinuteFriday";
            _sqlToExecute += ",[EndMinuteFriday] = @EndMinuteFriday";
            _sqlToExecute += ",[FridayFacilityBookLength] = @FridayFacilityBookLength";
            _sqlToExecute += ",[StartMinuteSaturday] = @StartMinuteSaturday";
            _sqlToExecute += ",[EndMinuteSaturday] = @EndMinuteSaturday";
            _sqlToExecute += ",[SaturdayFacilityBookLength] = @SaturdayFacilityBookLength";
            _sqlToExecute += ",[StartMinuteSunday] = @StartMinuteSunday";
            _sqlToExecute += ",[EndMinuteSunday] = @EndMinuteSunday";
            _sqlToExecute += ",[SundayFacilityBookLength] = @SundayFacilityBookLength ";
            _sqlToExecute += "WHERE [Id] = " + saveThis.Id;

            if (!_dataEngine.ExecuteSql(_sqlToExecute))
                throw new Exception("FacilitySchedule - Update failed");
        }

        public void Delete(FacilitySchedule deleteThis)
        {
            _dataEngine.InitialiseParameterList();
            _dataEngine.AddParameter("@Id", deleteThis.Id.ToString());

            _sqlToExecute = "DELETE FROM [dbo].[FacilitySchedule] WHERE Id = " + _dataEngine.GetParametersForQuery();

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
