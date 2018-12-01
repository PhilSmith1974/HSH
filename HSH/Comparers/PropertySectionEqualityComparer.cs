//using HSH.Models;
using System.Collections.Generic;
using HSH.Models;


namespace HSH.Comparers
{
    public class PropertySectionEqualityComparer : IEqualityComparer<PropertySection> 
    {
        public bool Equals(PropertySection section1, PropertySection section2)
        {
            return section1.Id.Equals(section2.Id);
        }

        public int GetHashCode(PropertySection section)
        {
            return (section.Id).GetHashCode();
        }
    }
}