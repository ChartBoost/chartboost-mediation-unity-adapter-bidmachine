using System.Collections.Generic;
using Chartboost.Json;
using Chartboost.Logging;
using Chartboost.Mediation.BidMachine;
using Chartboost.Mediation.BidMachine.Common;
using Chartboost.Tests.Runtime;
using NUnit.Framework;

namespace Chartboost.Tests
{
    internal class BidMachineAdapterTests
    {
        [SetUp]
        public void SetUp()
            => LogController.LoggingLevel = LogLevel.Debug;

        [Test]
        public void AdapterNativeVersion()
            => TestUtilities.TestStringGetter(() => BidMachineAdapter.AdapterNativeVersion);

        [Test]
        public void PartnerSDKVersion()
            => TestUtilities.TestStringGetter(() => BidMachineAdapter.PartnerSDKVersion);

        [Test]
        public void PartnerIdentifier()
            => TestUtilities.TestStringGetter(() => BidMachineAdapter.PartnerIdentifier);

        [Test]
        public void PartnerDisplayName()
            => TestUtilities.TestStringGetter(() => BidMachineAdapter.PartnerDisplayName);

        [Test]
        public void TestMode()
            => TestUtilities.TestBooleanAccessor(() => BidMachineAdapter.TestMode, value => BidMachineAdapter.TestMode = value);
        
        [Test]
        public void VerboseLogging()
            => TestUtilities.TestBooleanAccessor(() => BidMachineAdapter.VerboseLogging, value => BidMachineAdapter.VerboseLogging = value);
        
        [Test]
        public void SetTargetingInfo()
        {
            var targetingInfo = new TargetingParams();

            targetingInfo.UserId = "TEST_USER_ID";
            targetingInfo.Gender = Gender.MALE;
            targetingInfo.YearOfBirth = 1990;
            // Austin, TX
            targetingInfo.Location = new Location(30.2672, 97.7431);
            targetingInfo.Country = "United States";
            targetingInfo.City = "Austin";
            targetingInfo.Zip = "73301";
            targetingInfo.Keywords = new[] { "whale", "mobile", "gaming" };
            targetingInfo.BlockedApps = new[] { "com.test.id", "com.test.id2" };
            // https://support.aerserv.com/hc/en-us/articles/207148516-List-of-IAB-Categories
            targetingInfo.BlockedCategories = new[] { "IAB2", "IAB2-3" };
            targetingInfo.BlockedAdvertisers = new[] { "tes.id.2", "test.id.1" };
            targetingInfo.StoreUrl = "https://play.google.com/store/apps/details?id=com.android.chrome";
            targetingInfo.StoreId = "com.android.chrome";
            targetingInfo.StoreCategory = "Utilities";
            targetingInfo.StoreSubCategories = new []{ "Internet", "Browser"};
            targetingInfo.Paid = false;
            targetingInfo.ExternalUserIds = new Dictionary<string, string>{ {"Meta", "META_USER_ID"}, {"PLAYFAB", "PLAYFAB_USER_ID"} };
            
            LogController.Log(JsonTools.SerializeObject(targetingInfo), LogLevel.Debug);
            BidMachineAdapter.SetTargetingParams(targetingInfo);
        }

        [Test]
        public void SetPublisherInfo()
        {
            const string publisherId = "chartboost";
            const string publisherName = "Chartboost";
            const string publisherDomain = "chartboost.com";
            var categories = new[] { "ads", "games", "mobile" };
            
            var publisherInfo = new PublisherInfo(publisherId, publisherName, publisherDomain, categories);
            
            LogController.Log(JsonTools.SerializeObject(publisherInfo), LogLevel.Debug);
            BidMachineAdapter.SetPublisherInfo(publisherInfo);
        }
    }
}
