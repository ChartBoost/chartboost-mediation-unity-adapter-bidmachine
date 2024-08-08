using System.Collections.Generic;
using Chartboost.Constants;
using Chartboost.Logging;
using Chartboost.Mediation.BidMachine.Common;
using UnityEngine;

namespace Chartboost.Mediation.BidMachine.Android
{
    internal sealed partial class BidMachineAdapter
    {
        private const string BidMachineAdapterConfiguration = "com.chartboost.mediation.bidmachineadapter.BidMachineAdapterConfiguration";

        private const string FunctionGetTestModeEnabled = "getTestModeEnabled";
        private const string FunctionSetTestModeEnabled = "setTestModeEnabled";
        private const string FunctionGetIsLoggingEnabled = "isLoggingEnabled";
        private const string FunctionSetLoggingEnabled = "setLoggingEnabled";
        private const string FunctionSetTargetingParams = "setTargetingParams";
        private const string FunctionSetPublisher = "setPublisher";

        private const string BidMachineGender = "io.bidmachine.utils.Gender";
        private const string BidMachineTargetingParams = "io.bidmachine.TargetingParams";
        private const string FunctionSetUserId = "setUserId";
        private const string FunctionSetGender = "setGender";
        private const string FunctionSetBirthdayYear = "setBirthdayYear";
        private const string FunctionSetDeviceLocation = "setDeviceLocation";
        private const string FunctionSetCountry = "setCountry";
        private const string FunctionSetCity = "setCity";
        private const string FunctionSetZip = "setZip";
        private const string FunctionAddBlockedApplication = "addBlockedApplication";
        private const string FunctionAddBlockedAdvertiserCategory = "addBlockedAdvertiserIABCategory";
        private const string FunctionAddBlockedAdvertiserDomain = "addBlockedAdvertiserDomain";
        private const string FunctionSetStoreUrl = "setStoreUrl";
        private const string FunctionSetStoreCategory = "setStoreCategory";
        private const string FunctionSetPaid = "setPaid";
        private const string FunctionSetExternalUserIds = "setExternalUserIds";
        private const string FunctionSetKeywords = "setKeywords";
        private const string FunctionSetStoreSubCategories = "setStoreSubCategories";
        private const string FunctionFromInt = "fromInt";

        private const string AndroidLocation = "android.location.Location";
        private const string FunctionSetLatitude = "setLatitude";
        private const string FunctionSetLongitude = "setLongitude";

        private const string BidMachinePublisherBuilder = "io.bidmachine.Publisher$Builder";
        private const string FunctionSetId = "setId";
        private const string FunctionSetName = "setName";
        private const string FunctionSetDomain = "setDomain";
        private const string FunctionAddCategory = "addCategory";
        private const string FunctionBuild = "build";
        
        private const string BidMachineExternalUserId = "io.bidmachine.ExternalUserId";

        private static void SetString(ref AndroidJavaObject targetObject, string value, string function)
        {
            if (ValidateString(value))
                targetObject = targetObject.Call<AndroidJavaObject>(function, value);
        }

        private static void SetStringArray(ref AndroidJavaObject targetObject, string[] value, string function)
        {
            if (!ValidateArray(value)) 
                return;
            // Create object array that will be passed to the CallStatic method
            var objParams = new object[1];
            // By assigning item 1 in the object[], you are preventing the compiler from parsing them into the function call, so the Unity code sees the string[] and creates the Java signature properly
            objParams[0] = value;
            targetObject = targetObject.Call<AndroidJavaObject>(function, objParams);
        }

        private static AndroidJavaObject ToNativeGender(Gender gender)
        {
            using var genderNativeClass = new AndroidJavaClass(BidMachineGender);
            var intValue = (int)gender;
            using var nativeInt = intValue.GetNativeInt();
            return genderNativeClass.CallStatic<AndroidJavaObject>(FunctionFromInt, nativeInt);
        }

        private static AndroidJavaObject ToNativeLocation(Location location)
        {
            var nativeLocationObject = new AndroidJavaObject(AndroidLocation, string.Empty);
            nativeLocationObject.Call(FunctionSetLatitude, location.Latitude);
            nativeLocationObject.Call(FunctionSetLongitude, location.Longitude);
            return nativeLocationObject;
        }

        private static void AddParametersFromArray(ref AndroidJavaObject targetObject, IReadOnlyCollection<string> parameters, string addFunction)
        {
            if (!ValidateArray(parameters))
                return;

            foreach (var blockedParameter in parameters)
            {
                if (!ValidateString(blockedParameter))
                    continue;

                targetObject = targetObject.Call<AndroidJavaObject>(addFunction, blockedParameter);
            }
        }

        private static AndroidJavaObject ToExternalIdsNativeList(IReadOnlyDictionary<string, string> source)
        {
            var nativeList = new AndroidJavaObject(SharedAndroidConstants.ClassArrayList);

            if (source == null)
                return nativeList;

            if (source.Count == 0)
                return nativeList;
            
            foreach (var externalUserId in source)
            {
                if (!ValidateString(externalUserId.Key) || !ValidateString(externalUserId.Value))
                {
                    LogController.Log($"Failed to add external id: {externalUserId.Key}/{externalUserId.Value}", LogLevel.Warning);
                    continue;
                }

                using var nativeExternalUserId = new AndroidJavaObject(BidMachineExternalUserId, externalUserId.Key, externalUserId.Value);
                var added = nativeList.Call<bool>(SharedAndroidConstants.FunctionAdd, nativeExternalUserId);
                    
                if (!added)
                    LogController.Log($"Failed to add external id: {externalUserId.Key}/{externalUserId.Value}", LogLevel.Warning);
            }

            return nativeList;
        }

        private static bool ValidateString(string value)
            => !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);

        private static bool ValidateArray<T>(IReadOnlyCollection<T> value) => value != null && value.Count != 0;
    }
}
