using System.Runtime.CompilerServices;
using Chartboost.Mediation.BidMachine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
[assembly: InternalsVisibleTo(AssemblyInfo.BidMachineAssemblyInfoAndroid)]
[assembly: InternalsVisibleTo(AssemblyInfo.BidMachineAssemblyInfoIOS)]

namespace Chartboost.Mediation.BidMachine
{
    internal class AssemblyInfo
    {
        public const string BidMachineAssemblyInfoAndroid = "Chartboost.Mediation.BidMachine.Android";
        public const string BidMachineAssemblyInfoIOS = "Chartboost.Mediation.BidMachine.IOS";
    }
}
