using Chartboost.Constants;
using Chartboost.Mediation.BidMachine.Common;
using UnityEngine;

namespace Chartboost.Mediation.BidMachine.Android
{
    internal sealed partial class BidMachineAdapter : IBidMachineAdapter
    {
        [RuntimeInitializeOnLoadMethod]
        private static void RegisterInstance()
        {
            if (Application.isEditor)
                return;
            BidMachine.BidMachineAdapter.Instance = new BidMachineAdapter();
        }
        
        /// <inheritdoc/>
        public string AdapterNativeVersion
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
                return adapterConfiguration.Call<string>(SharedAndroidConstants.FunctionGetAdapterVersion);
            }
        }
        
        /// <inheritdoc/>
        public string PartnerSDKVersion 
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
                return adapterConfiguration.Call<string>(SharedAndroidConstants.FunctionGetPartnerSdkVersion);
            }
        }
        
        /// <inheritdoc/>
        public string PartnerIdentifier
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
                return adapterConfiguration.Call<string>(SharedAndroidConstants.FunctionGetPartnerId);
            }
        }
        
        /// <inheritdoc/>
        public string PartnerDisplayName 
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
                return adapterConfiguration.Call<string>(SharedAndroidConstants.FunctionGetPartnerDisplayName);
            }
        }

        /// <inheritdoc/>
        public bool TestMode
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
                return adapterConfiguration.Call<bool>(FunctionGetTestModeEnabled);
            }
            set
            {
                using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
                adapterConfiguration.Call(FunctionSetTestModeEnabled, value);
            }
        }

        /// <inheritdoc/>
        public bool VerboseLogging
        {
            get
            {
                using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
                return adapterConfiguration.Call<bool>(FunctionGetIsLoggingEnabled);
            }
            set
            {
                using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
                adapterConfiguration.Call(FunctionSetLoggingEnabled, value);
            }
        }
        
        /// <inheritdoc/>
        public void SetTargetingParams(TargetingParams targetingParams)
        {
            var nativeTargetingParams = new AndroidJavaObject(BidMachineTargetingParams);

            SetString(ref nativeTargetingParams, targetingParams.UserId, FunctionSetUserId);
            
            // Gender enum
            if (targetingParams.Gender.HasValue)
            {
                using var nativeGender = ToNativeGender(targetingParams.Gender.Value);
                nativeTargetingParams = nativeTargetingParams.Call<AndroidJavaObject>(FunctionSetGender, nativeGender);
            }

            // uint
            if (targetingParams.YearOfBirth.HasValue)
            {
                using var nativeYearOfBirth = ((int)targetingParams.YearOfBirth.Value).GetNativeInt();
                nativeTargetingParams = nativeTargetingParams.Call<AndroidJavaObject>(FunctionSetBirthdayYear, nativeYearOfBirth);
            }

            // Location struct
            if (targetingParams.Location.HasValue)
            {
                using var nativeLocation = ToNativeLocation(targetingParams.Location.Value);
                nativeTargetingParams = nativeTargetingParams.Call<AndroidJavaObject>(FunctionSetDeviceLocation, nativeLocation);
            }
            
            SetString(ref nativeTargetingParams, targetingParams.Country, FunctionSetCountry);
            SetString(ref nativeTargetingParams, targetingParams.City, FunctionSetCity);
            SetString(ref nativeTargetingParams, targetingParams.Zip, FunctionSetZip);

            SetStringArray(ref nativeTargetingParams, targetingParams.Keywords, FunctionSetKeywords);

            AddParametersFromArray(ref nativeTargetingParams, targetingParams.BlockedApps, FunctionAddBlockedApplication);
            AddParametersFromArray(ref nativeTargetingParams, targetingParams.BlockedCategories, FunctionAddBlockedAdvertiserCategory);
            AddParametersFromArray(ref nativeTargetingParams, targetingParams.BlockedAdvertisers, FunctionAddBlockedAdvertiserDomain);
            
            SetString(ref nativeTargetingParams, targetingParams.StoreUrl, FunctionSetStoreUrl);
            SetString(ref nativeTargetingParams, targetingParams.StoreCategory, FunctionSetStoreCategory);
            
            SetStringArray(ref nativeTargetingParams, targetingParams.StoreSubCategories, FunctionSetStoreSubCategories);
            
            // bool
            if (targetingParams.Paid.HasValue)
            {
                using var nativePaid = targetingParams.Paid.Value.GetNativeBool();
                nativeTargetingParams = nativeTargetingParams.Call<AndroidJavaObject>(FunctionSetPaid, nativePaid);
            }

            // ExternalUserIds array
            if (ValidateArray(targetingParams.ExternalUserIds))
            {
                using var nativeExternalIds = ToExternalIdsNativeList(targetingParams.ExternalUserIds);
                nativeTargetingParams = nativeTargetingParams.Call<AndroidJavaObject>(FunctionSetExternalUserIds, nativeExternalIds);
            }

            using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
            adapterConfiguration.Call(FunctionSetTargetingParams, nativeTargetingParams);
            nativeTargetingParams.Dispose();
        }

        /// <inheritdoc/>
        public void SetPublisherInfo(PublisherInfo publisherInfo)
        {
            var publisherBuilder = new AndroidJavaObject(BidMachinePublisherBuilder);

            SetString(ref publisherBuilder, publisherInfo.Id, FunctionSetId);
            SetString(ref publisherBuilder, publisherInfo.Name, FunctionSetName);
            SetString(ref publisherBuilder, publisherInfo.Domain, FunctionSetDomain);
            AddParametersFromArray(ref publisherBuilder, publisherInfo.Categories, FunctionAddCategory);

            using var nativePublisherObject = publisherBuilder.Call<AndroidJavaObject>(FunctionBuild);
            publisherBuilder.Dispose();
            
            using var adapterConfiguration = new AndroidJavaObject(BidMachineAdapterConfiguration);
            adapterConfiguration.Call(FunctionSetPublisher, nativePublisherObject);
        }
    }
}
