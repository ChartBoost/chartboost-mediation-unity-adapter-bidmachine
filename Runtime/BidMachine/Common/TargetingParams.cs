using System.Collections.Generic;

namespace Chartboost.Mediation.BidMachine.Common
{
    #nullable enable
    public struct TargetingParams
    {
        /// <summary>
        /// Vendor-specific ID for the user.
        /// </summary>
        public string? UserId;

        /// <summary>
        /// Gender, one of following: Female, Male, Unknown.
        /// </summary>
        public Gender? Gender;

        /// <summary>
        /// Year of birth as a 4-digit integer (e.g. 1990).
        /// </summary>
        public uint? YearOfBirth;

        /// <summary>
        /// Location of the user's home base (i.e., not necessarily their current location).
        /// </summary>
        public Location? Location;

        /// <summary>
        /// Country of the user's home base (i.e., not necessarily their current location).
        /// </summary>
        public string? Country;

        /// <summary>
        /// City of the user's home base (i.e., not necessarily their current location).
        /// </summary>
        public string? City;

        /// <summary>
        /// Zip of the user's home base (i.e., not necessarily their current location).
        /// </summary>
        public string? Zip;

        /// <summary>
        /// List of keywords, interests, or intents.
        /// </summary>
        public string[]? Keywords;

        /// <summary>
        /// Block list of apps where ads are disallowed. These should be bundle or package names (e.g., “com.foo.mygame”) and should NOT be app store IDs (e.g., not iTunes store IDs).
        /// </summary>
        public string[]? BlockedApps;

        /// <summary>
        /// Block list of content categories using IDs.
        /// </summary>
        public string[]? BlockedCategories;

        /// <summary>
        /// Block list of advertisers by their domains (e.g. example.com).
        /// </summary>
        public string[]? BlockedAdvertisers;

        /// <summary>
        /// Store URL for an installed App; for IQG 2.1 compliance.
        /// </summary>
        public string? StoreUrl;

        /// <summary>
        /// Application identifier in App Store ( numeric string e.q. "1111")
        /// </summary>
        public string? StoreId;
        
        /// <summary>
        /// Sets App store category definitions (e.g. - "games").
        /// </summary>
        public string? StoreCategory;

        /// <summary>
        /// Sets App Store Subcategory definitions. The array is always capped at 3 strings.
        /// </summary>
        public string[]? StoreSubCategories;

        /// <summary>
        /// Determines, if the app version is free or paid version of the app.
        /// </summary>
        public bool? Paid;

        /// <summary>
        /// Set external user ID list.
        /// </summary>
        public Dictionary<string, string>? ExternalUserIds;
    }
}
