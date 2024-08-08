#import "CBMDelegates.h"
#import "ChartboostUnityUtilities.h"
#import "CoreLocation/CoreLocation.h"
#import <BidMachine/BidMachine-Swift.h>
#import <ChartboostMediationAdapterBidMachine/ChartboostMediationAdapterBidMachine-Swift.h>

extern "C" {

    const char * _CBMBidMachineAdapterAdapterVersion(){
        return toCStringOrNull([BidMachineAdapterConfiguration adapterVersion]);
    }

    const char * _CBMBidMachineAdapterPartnerSDKVersion(){
        return toCStringOrNull([BidMachineAdapterConfiguration partnerSDKVersion]);
    }

    const char * _CBMBidMachineAdapterPartnerId(){
        return toCStringOrNull([BidMachineAdapterConfiguration partnerID]);
    }

    const char * _CBMBidMachineAdapterPartnerDisplayName(){
        return toCStringOrNull([BidMachineAdapterConfiguration partnerDisplayName]);
    }

    BOOL _CBMBidMachineAdapterGetTestMode(){
        return [BidMachineAdapterConfiguration testMode];
    }

    void _CBMBidMachineAdapterSetTestMode(BOOL testMode){
        [BidMachineAdapterConfiguration setTestMode:testMode];
    }

    BOOL _CBMBidMachineAdapterGetVerboseLogging(){
        return [BidMachineAdapterConfiguration logging] && [BidMachineAdapterConfiguration bidLogging] && [BidMachineAdapterConfiguration eventLogging];
    }

    void _CBMBidMachineAdapterSetVerboseLogging(BOOL verboseLogging){
        [BidMachineAdapterConfiguration setLogging:verboseLogging];
        [BidMachineAdapterConfiguration setBidLogging:verboseLogging];
        [BidMachineAdapterConfiguration setEventLogging:verboseLogging];
    }

    void _CBMBidMachineAdapterSetTargetingParams(const char * userId, int gender, int yearOfBirth,
                                                 double latitude, double longitude, const char * country, const char * city, const char * zip, const char * keywordsJson, const char ** blockedAppsArray, int blockedAppsCount, const char ** blockedCategoriesArray,
                                                 int blockedCategoriesCount, const char ** blockedAdvertisersArray, int blockedAdvertisersCount, const char * storeUrl,
                                                 const char * storeId, const char * storeCategory, const char ** storeSubCategoriesArray, int storeSubCategoriesCount, int paid,
                                                 const char * externalUserIdsJson){
        [[[BidMachineSdk shared] targetingInfo] populate:^(id<BidMachineTargetingInfoBuilderProtocol> _Nonnull builder){

            NSString* nativeUserId = toNSStringOrEmpty(userId);
            [builder withUserId:nativeUserId];

            BidMachineUserGender nativeGender = (BidMachineUserGender)gender;
            [builder withUserGender:nativeGender];

            if (yearOfBirth != -1)
                [builder withUserYOB:(UInt32)yearOfBirth];


            if (latitude != -1 && longitude != -1)
            {
                CLLocation* nativeLocation = [[CLLocation alloc] initWithLatitude:latitude longitude:longitude];
                [builder withUserLocation:nativeLocation];
            }

            NSString* nativeCountry = toNSStringOrEmpty(country);
            [builder withCountry:nativeCountry];

            NSString* nativeCity = toNSStringOrEmpty(city);
            [builder withCountry:nativeCity];

            NSString* nativeZip = toNSStringOrEmpty(zip);
            [builder withCountry:nativeZip];

            NSString* nativeKeywords = toNSStringOrEmpty(keywordsJson);
            [builder withKeywords:nativeKeywords];

            NSMutableArray* nativeBlockedApps = toNSMutableArray(blockedAppsArray, blockedAppsCount);
            [builder withBlockedApps:nativeBlockedApps];

            NSMutableArray* nativeBlockedCategories = toNSMutableArray(blockedCategoriesArray, blockedAppsCount);
            [builder withBlockedApps:nativeBlockedCategories];

            NSMutableArray* nativeBlockedAdvertisers = toNSMutableArray(blockedAdvertisersArray, blockedAdvertisersCount);
            [builder withBlockedAdvertisers:nativeBlockedAdvertisers];

            NSString* nativeStoreUrl = toNSStringOrEmpty(storeUrl);
            [builder withStoreURL:nativeStoreUrl];

            NSString* nativeStoreId = toNSStringOrEmpty(storeId);
            [builder withStoreId:nativeStoreId];

            NSString* nativeStoreCategory = toNSStringOrEmpty(storeCategory);
            [builder withStoreCategory:nativeStoreCategory];

            NSMutableArray* nativeStoreSubCategories = toNSMutableArray(storeSubCategoriesArray, storeSubCategoriesCount);
            [builder withStoreSubCategories:nativeStoreSubCategories];

            if (paid != -1)
                [builder withPaid:(BOOL)paid];

            NSDictionary* externalUserIds = toNSDictionary(externalUserIdsJson);
            for(NSString* key in externalUserIds) {
                NSString* value = [externalUserIds objectForKey:key];
                [builder appendExternalId:key :value];
            }
        }];
    }

    void _CBMBidMachineAdapterSetPublisherInfo(const char* publisherId, const char* name, const char* domain, const char** categoriesArray, int categoriesCount){
        [[[BidMachineSdk shared] publisherInfo] populate:^(id<BidMachinePublisherInfoBuilderProtocol> _Nonnull builder) {
            NSString* nativeId = toNSStringOrEmpty(publisherId);
            [builder withId:nativeId];

            NSString* nativeName = toNSStringOrEmpty(name);
            [builder withName:nativeName];

            NSString* nativeDomain = toNSStringOrEmpty(domain);
            [builder withDomain:nativeDomain];

            NSMutableArray* nativeCategories = toNSMutableArray(categoriesArray, categoriesCount);
            [builder withCategories:nativeCategories];
        }];
    }
}
