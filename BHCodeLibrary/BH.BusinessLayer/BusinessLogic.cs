using System;
using BH.Domain;
using BH.DataAccessLayer;

namespace BH.BusinessLayer
{
    public class BusinessLogic : IBusinessLogic
    {
        private Lazy<CustomerLogic> _customerLogic = new Lazy<CustomerLogic>();
        private Lazy<LocationLogic> _locationLogic = new Lazy<LocationLogic>();
        private Lazy<FacilityLogic> _facilityLogic = new Lazy<FacilityLogic>();
        private Lazy<FacilityScheduleLogic> _facilityScheduleLogic = new Lazy<FacilityScheduleLogic>();
        
        public ICustomerLogic CustomerLogic
        {
            get { return _customerLogic.Value; }
        }
      
        public ILocationLogic LocationLogic
        {
            get { return _locationLogic.Value; }
        }

        public IFacilityLogic FacilityLogic
        {
            get { return _facilityLogic.Value; }
        }

        public IFacilityScheduleLogic FacilityScheduleLogic
        {
            get { return _facilityScheduleLogic.Value; }
        }
    }
}
