using Chartboost.Mediation.Adapters;

namespace Chartboost.Mediation.BidMachine.Common
{
    /// <summary>
    /// The Chartboost Mediation BidMachine adapter.
    /// </summary>
    internal interface IBidMachineAdapter : IPartnerAdapterConfiguration
    {
        /// <summary>
        /// Init flag for starting up BidMachine SDK in test mode.
        /// </summary>
        public bool TestMode { get; set; }

        /// <summary>
        /// Enable/disable logging for the BidMachine Ads SDK.
        /// </summary>
        public bool VerboseLogging { get; set; }

        /// <summary>
        /// Globally set targeting parameters.
        /// </summary>
        public void SetTargetingParams(TargetingParams targetingParams);
        
        /// <summary>
        /// Globally set Publisher information.
        /// </summary>
        public void SetPublisherInfo(PublisherInfo publisherInfo);
    }
}
