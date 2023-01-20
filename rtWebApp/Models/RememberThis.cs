using System;

namespace rtWebApp.Models
{
    public class rtItem : IComparable<rtItem>, IEquatable<rtItem>
    {
        public int rtId { get; set; }        
        public string? rtUser { get; set; } //= null;
        public string? rtDescription { get; set; } //= null;
        public string? rtLocation { get; set; }
        public DateTime rtDateTime { get; set; }
        public string? rtImagePath { get; set; } //= string.Empty;        

         public bool Equals(rtItem? other)
        {
            if (other == null) return false;
            return (this.rtId.Equals(other.rtId));
        }
        public int CompareTo(rtItem? compareItem)
        {
            // A null value means that this object is greater.
            if (compareItem == null)
                return 1;

            else
                return this.rtId.CompareTo(compareItem.rtId);
        }       

        public override string ToString()
        {
            return "Id: " + rtId + ", User: " + rtUser + ", Desc: " + rtDescription + ", Location: " + rtLocation + ", When: " + rtDateTime;
        }


    }

}