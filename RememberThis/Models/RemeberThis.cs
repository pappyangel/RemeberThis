using System;

namespace RememberThis.Models
{
    public class Item : IComparable<Item>, IEquatable<Item>
    {
        public int rtId { get; set; }        
        public string? rtUser { get; set; } //= null;
        public string? rtDescription { get; set; } //= null;
        public string? rtLocation { get; set; }
        public DateOnly rtDate { get; set; }
        public string? rtImagePath { get; set; } //= string.Empty;

         public bool Equals(Item? other)
        {
            if (other == null) return false;
            return (this.rtId.Equals(other.rtId));
        }
        public int CompareTo(Item? compareItem)
        {
            // A null value means that this object is greater.
            if (compareItem == null)
                return 1;

            else
                return this.rtId.CompareTo(compareItem.rtId);
        }       

        public override string ToString()
        {
            return "Id: " + rtId + ", User: " + rtUser + ", Desc: " + rtDescription + ", Location: " + rtLocation + ", When: " + rtDate;
        }


    }

}