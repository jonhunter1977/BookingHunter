using System;

namespace BH.BusinessLayer
{
    /// <summary>
    /// Interface for accessing business logic
    /// </summary>
    public interface IBusinessLogic
    {
        /// <summary>
        /// Access to the customer business logic functions
        /// </summary>
        ICustomerLogic CustomerLogic { get; }

        /// <summary>
        /// Access to the location business logic functions
        /// </summary>
        ILocationLogic LocationLogic { get; }

        /// <summary>
        /// Access to the facility business logic functions
        /// </summary>
        IFacilityLogic FacilityLogic { get; }

    }
}
