# Changelog
All notable changes to this project will be documented in this file using the standards as defined at [Keep a Changelog](https://keepachangelog.com/en/1.0.0/). This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0).

### Version 5.0.0 *(2024-08-08)*

First version of the Chartboost Mediation Unity SDK - BidMachine Adapter.

#Added
- Support for the following `BidMachine` dependencies. Notice adapter dependencies are optimistic and any patches and hot-fixes will be automatically picked up.:
    * Android: `com.chartboost:chartboost-mediation-adapter-bidmachine:5.3.0.+`
    * iOS: `ChartboostMediationAdapterBidMachine ~> 5.2.7.0`
    
- `BidMachineAdapter.cs` with Configuration Properties for `BidMachine`.
- The following properties have been added in `BidMachineAdapter.cs`
    * `string AdapterUnityVersion`
    * `string AdapterNativeVersion`
    * `string PartnerSDKVersion`
    * `string PartnerIdentifier`
    * `string PartnerDisplayName`
    * `bool TestMode`
    * `bool VerboseLogging`
- The following methods have been added in `BidMachineAdapter.cs`
    * `void SetTargetingParams(TargetingParams targetingParams)`
    * `void SetPublisherInfo(PublisherInfo publisherInfo)`