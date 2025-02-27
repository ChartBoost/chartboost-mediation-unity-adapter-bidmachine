# Chartboost Mediation Unity SDK - BidMachine Adapter

Provides a list of externally configurable properties pertaining to the partner SDK that can be retrieved and set by publishers. 

Dependencies for the adapter are now embedded in the package, and can be found at `com.chartboost.mediation.unity.adapter.bidmachine/Editor/BidMachineAdapterDependencies.xml`.

# Installation

## Using the public [npm registry](https://www.npmjs.com/search?q=com.chartboost.mediation.unity.adapter.bidmachine)

In order to add the Chartboost Mediation Unity SDK - BidMachine Adapter to your project using the npm package, add the following to your Unity Project's ***manifest.json*** file. The scoped registry section is required in order to fetch packages from the NpmJS registry.

```json
"dependencies": {
    "com.chartboost.mediation.unity.adapter.bidmachine": "5.1.2",
    ...
},
"scopedRegistries": [
{
    "name": "NpmJS",
    "url": "https://registry.npmjs.org",
    "scopes": [
    "com.chartboost"
    ]
}
]
```

## Using the public [NuGet package](https://www.nuget.org/packages/Chartboost.CSharp.Mediation.Unity.Adapter.BidMachine)

To add the Chartboost Mediation Unity SDK - BidMachine Adapter to your project using the NuGet package, you will first need to add the [NugetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) package into your Unity Project.

This can be done by adding the following to your Unity Project's ***manifest.json***

```json
  "dependencies": {
    "com.github-glitchenzo.nugetforunity": "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity",
    ...
  },
```

Once <code>NugetForUnity</code> is installed, search for `Chartboost.CSharp.Mediation.Unity.Adapter.BidMachine` in the search bar of Nuget Explorer window(Nuget -> Manage Nuget Packages).
You should be able to see the `Chartboost.CSharp.Mediation.Unity.Adapter.BidMachine` package. Choose the appropriate version and install.

# Network Cecurity Configuration

## Android
[Android 9.0 (API 28) blocks cleartext (non-HTTPS) traffic by default](https://developer.android.com/privacy-and-security/security-config), which can prevent ads from being served correctly.

> **Warning** \
> Failure to comply with this configuration may result in lower display rate, fill rate, rendering errors, and as a result - lower revenue.

Add a Network Security Configuration file to your AndroidManifest.xml:

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest>
    <application android:networkSecurityConfig="@xml/network_security_config" />
</manifest>
```

In your `network_security_config.xml` file, add base-config that sets `cleartextTrafficPermitted` to true:

```xml
<?xml version="1.0" encoding="utf-8"?>
<network-security-config>
    <base-config cleartextTrafficPermitted="true">
        <trust-anchors>
            <certificates src="system" />
        </trust-anchors>
    </base-config>
    <debug-overrides>
        <trust-anchors>
            <certificates src="user" />
        </trust-anchors>
    </debug-overrides>
</network-security-config>
```

## iOS

Add this code to the `Info.plist` file:

```plist
<key>NSAppTransportSecurity</key>
  <dict>  
      <key>NSAllowsArbitraryLoads</key><true/>  
  </dict>
</dict>
```

# Usage
The following code block exemplifies usage of the `BidMachineAdapter.cs` configuration class.

## IPartnerAdapterConfiguration Properties

```csharp

// AdapterUnityVersion - The partner adapter Unity version, e.g: 5.0.0
Debug.Log($"Adapter Unity Version: {BidMachineAdapter.AdapterUnityVersion}");

// AdapterNativeVersion - The partner adapter version, e.g: 5.2.6.0.0
Debug.Log($"Adapter Native Version: {BidMachineAdapter.AdapterNativeVersion}");

// PartnerSDKVersion - The partner SDK version, e.g: 2.6.0
Debug.Log($"Partner SDK Version: {BidMachineAdapter.PartnerSDKVersion}");

// PartnerIdentifier - The partner ID for internal uses, e.g: bidmachine
Debug.Log($"Partner Identifier: {BidMachineAdapter.PartnerIdentifier}");

// PartnerDisplayName - The partner name for external uses, e.g: BidMachine
Debug.Log($"Partner Display Name: {BidMachineAdapter.PartnerDisplayName}");
```

## Test Mode
To enable test mode for the BidMachine adapter, the following property has been made available:

```csharp
BidMachineAdapter.TestMode = true;
```

## Verbose Logging
To enable verbose logging for the BidMachine adapter, the following property has been made available:

```csharp
BidMachineAdapter.VerboseLogging = true;
```

## Setting Targeting Parameters

To allow setting setting targeting parameters, the following method has been made available:

```csharp
// This example sets BidMachine's targeting params utilizing sample values.
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

BidMachineAdapter.SetTargetingParams(targetingInfo);
```

More information can be founds in BidMachine's [Android](https://docs.bidmachine.io/docs/in-house-mediation-android#targeting-parameters) and [iOS](https://docs.bidmachine.io/docs/in-house-mediation-ios#targeting-info) documentation.

## Setting Publisher Information

To allow setting publisher information, the following method has been made available:

```csharp
// This example sets BidMachine's publisher info utilizing sample values.
const string publisherId = "chartboost";
const string publisherName = "Chartboost";
const string publisherDomain = "chartboost.com";
var categories = new[] { "ads", "games", "mobile" };

var publisherInfo = new PublisherInfo(publisherId, publisherName, publisherDomain, categories);

BidMachineAdapter.SetPublisherInfo(publisherInfo);
```