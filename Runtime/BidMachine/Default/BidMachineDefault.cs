using Chartboost.Mediation.BidMachine.Common;

namespace Chartboost.Mediation.BidMachine.Default
{
    internal class BidMachineDefault : IBidMachineAdapter
    {
        /// <inheritdoc/>
        public string AdapterNativeVersion => BidMachineAdapter.AdapterUnityVersion;

        /// <inheritdoc/>
        public string PartnerSDKVersion => BidMachineAdapter.AdapterUnityVersion;
        
        /// <inheritdoc/>
        public string PartnerIdentifier => "bidmachine";
        
        /// <inheritdoc/>
        public string PartnerDisplayName => "BidMachine";

        /// <inheritdoc/>
        public bool TestMode { get; set; }
        
        /// <inheritdoc/>
        public bool VerboseLogging { get; set; }
        
        /// <inheritdoc/>
        public void SetTargetingParams(TargetingParams targetingParams)
        {
            // Do nothing
        }

        /// <inheritdoc/>
        public void SetPublisherInfo(PublisherInfo publisherInfo)
        {
            // Do nothing
        }
    }
}
