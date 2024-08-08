namespace Chartboost.Mediation.BidMachine.Common
{
    public enum Gender 
    {
        UNKNOWN = 0,
        #if UNITY_IOS
        MALE = 1,
        FEMALE = 2
        #else
        FEMALE = 1,
        MALE = 2
        #endif
    }
}
