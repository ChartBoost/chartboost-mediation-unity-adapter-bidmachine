using System;
using System.Runtime.InteropServices;
using Chartboost.Constants;
using Chartboost.Json;
using Chartboost.Mediation.BidMachine.Common;
using UnityEngine;

namespace Chartboost.Mediation.BidMachine.IOS
{
    internal sealed class BidMachineAdapter : IBidMachineAdapter
    {
        [RuntimeInitializeOnLoadMethod]
        private static void RegisterInstance()
        {
            if (Application.isEditor)
                return;
            BidMachine.BidMachineAdapter.Instance = new BidMachineAdapter();
        }

        /// <inheritdoc/>
        public string AdapterNativeVersion => _CBMBidMachineAdapterAdapterVersion();

        /// <inheritdoc/>
        public string PartnerSDKVersion => _CBMBidMachineAdapterPartnerSDKVersion();

        /// <inheritdoc/>
        public string PartnerIdentifier => _CBMBidMachineAdapterPartnerId();

        /// <inheritdoc/>
        public string PartnerDisplayName => _CBMBidMachineAdapterPartnerDisplayName();

        /// <inheritdoc/>
        public bool TestMode
        {
            get => _CBMBidMachineAdapterGetTestMode();
            set => _CBMBidMachineAdapterSetTestMode(value);
        }

        /// <inheritdoc/>
        public bool VerboseLogging
        {
            get => _CBMBidMachineAdapterGetVerboseLogging();
            set => _CBMBidMachineAdapterSetVerboseLogging(value);
        }

        public void SetTargetingParams(TargetingParams targetingParams)
        {
            var userId = targetingParams.UserId;
            var gender = (int)(targetingParams.Gender ?? Gender.UNKNOWN);
            var yearOfBirth = targetingParams.YearOfBirth.HasValue ? (int)targetingParams.YearOfBirth.Value : -1;
            var latitude = targetingParams.Location?.Latitude ?? -1;
            var longitude = targetingParams.Location?.Longitude ?? -1;
            var country = targetingParams.Country;
            var city = targetingParams.City;
            var zip = targetingParams.Zip;

            var keywordsJson = JsonTools.SerializeObject(targetingParams.Keywords);

            var blockedAppsArray = targetingParams.BlockedApps ?? Array.Empty<string>();
            var blockedAppsCount = targetingParams.BlockedApps?.Length ?? 0;

            var blockedCategoriesArray = targetingParams.BlockedCategories ?? Array.Empty<string>();
            var blockedCategoriesCount = targetingParams.BlockedCategories?.Length ?? 0;

            var blockedAdvertisersArray = targetingParams.BlockedAdvertisers ?? Array.Empty<string>();
            var blockedAdvertisersCount = targetingParams.BlockedAdvertisers?.Length ?? 0;

            var storeUrl = targetingParams.StoreUrl;
            var storeId = targetingParams.StoreId;
            var storeCategory = targetingParams.StoreCategory;

            var storeSubCategoriesArray = targetingParams.StoreSubCategories ?? Array.Empty<string>();
            var storeSubCategoriesCount = targetingParams.StoreSubCategories?.Length ?? 0;

            var paid = targetingParams.Paid.HasValue ? Convert.ToInt32(targetingParams.Paid.Value) : -1;

            var externalUserIdsJson = JsonTools.SerializeObject(targetingParams.ExternalUserIds);
            
            _CBMBidMachineAdapterSetTargetingParams(userId, gender, yearOfBirth, latitude, longitude, country, city,
                zip, keywordsJson, blockedAppsArray, blockedAppsCount, blockedCategoriesArray,
                blockedCategoriesCount, blockedAdvertisersArray, blockedAdvertisersCount, storeUrl, storeId,
                storeCategory, storeSubCategoriesArray, storeSubCategoriesCount, paid, externalUserIdsJson);
        }

        public void SetPublisherInfo(PublisherInfo publisherInfo)
        {
            var publisherId = publisherInfo.Id;
            var publisherName = publisherInfo.Name;
            var publisherDomain = publisherInfo.Domain;
            var publisherCategoriesArray = publisherInfo.Categories ?? Array.Empty<string>();
            var publisherCategoriesCount = publisherInfo.Categories?.Count ?? 0;

            _CBMBidMachineAdapterSetPublisherInfo(publisherId, publisherName, publisherDomain, (string[])publisherCategoriesArray, publisherCategoriesCount);
        }

        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBMBidMachineAdapterAdapterVersion();

        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBMBidMachineAdapterPartnerSDKVersion();

        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBMBidMachineAdapterPartnerId();

        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBMBidMachineAdapterPartnerDisplayName();

        [DllImport(SharedIOSConstants.DLLImport)] private static extern bool _CBMBidMachineAdapterGetTestMode();

        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMBidMachineAdapterSetTestMode(bool testMode);

        [DllImport(SharedIOSConstants.DLLImport)] private static extern bool _CBMBidMachineAdapterGetVerboseLogging();

        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMBidMachineAdapterSetVerboseLogging(bool verboseLogging);

        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMBidMachineAdapterSetTargetingParams(string userId, int gender, int yearOfBirth,
            double latitude, double longitude, string country, string city, string zip, string keywordsJson,
            string[] blockedAppsArray, int blockedAppsCount, string[] blockedCategoriesArray,
            int blockedCategoriesCount, string[] blockedAdvertisersArray, int blockedAdvertisersCount, string storeUrl,
            string storeId, string storeCategory, string[] storeSubCategoriesArray, int storeSubCategoriesCount, int paid,
            string externalUserIdsJson);

        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBMBidMachineAdapterSetPublisherInfo(string publisherId, string name, string domain, string[] categoriesArray, int categoriesCount);
    }
}
