using System.Collections.Generic;

namespace Chartboost.Mediation.BidMachine.Common
{
    public struct PublisherInfo
    {
        public readonly string Id;

        public readonly string Name;

        public readonly string Domain;

        public readonly IReadOnlyCollection<string> Categories;

        public PublisherInfo(string id, string name, string domain, IReadOnlyCollection<string> categories)
        {
            Id = id;
            Name = name;
            Domain = domain;
            Categories = categories;
        }
    }
}
