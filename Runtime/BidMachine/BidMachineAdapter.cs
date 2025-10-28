using Chartboost.Mediation.Adapters;
using Chartboost.Mediation.BidMachine.Common;
using Chartboost.Mediation.BidMachine.Default;

namespace Chartboost.Mediation.BidMachine
{
    /// <inheritdoc cref="IBidMachineAdapter"/>
    public static class BidMachineAdapter 
    {
        internal static IBidMachineAdapter Instance = new BidMachineDefault();
        
        /// <summary>
        /// The partner adapter Unity version.
        /// </summary>
        public const string AdapterUnityVersion = "5.1.8";
        
        /// <inheritdoc cref="IPartnerAdapterConfiguration.AdapterNativeVersion"/>
        public static string AdapterNativeVersion => Instance.AdapterNativeVersion;
        
        /// <inheritdoc cref="IPartnerAdapterConfiguration.PartnerSDKVersion"/>
        public static string PartnerSDKVersion => Instance.PartnerSDKVersion;
        
        /// <inheritdoc cref="IPartnerAdapterConfiguration.PartnerIdentifier"/>
        public static string PartnerIdentifier => Instance.PartnerIdentifier;
        
        /// <inheritdoc cref="IPartnerAdapterConfiguration.PartnerDisplayName"/>
        public static string PartnerDisplayName => Instance.PartnerDisplayName;

        /// <inheritdoc cref="IBidMachineAdapter.TestMode"/>
        public static bool TestMode
        {
            get => Instance.TestMode;
            set => Instance.TestMode = value;
        }

        /// <inheritdoc cref="IBidMachineAdapter.VerboseLogging"/>
        public static bool VerboseLogging
        {
            get => Instance.VerboseLogging;
            set => Instance.VerboseLogging = value;
        }

        /// <inheritdoc cref="IBidMachineAdapter.SetTargetingParams"/>
        public static void SetTargetingParams(TargetingParams targetingParams) 
            => Instance.SetTargetingParams(targetingParams);

        
        /// <inheritdoc cref="IBidMachineAdapter.SetPublisherInfo"/>
        public static void SetPublisherInfo(PublisherInfo publisherInfo) 
            => Instance.SetPublisherInfo(publisherInfo);
    }
}
