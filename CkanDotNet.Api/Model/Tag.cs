using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkanDotNet.Api.Model
{
    public class Tag : IEquatable<Tag>, IComparable<Tag>
    {
        public string Label { get; set; }
        public int Count { get; set; }
        public int Scale { get; set; }

        public Tag(string tag)
        {
            Label = tag;
            Count = 1;
            Scale = 12;
        }

        public bool Equals(Tag tag)
        {
            // If parameter is null return false:
            if ((object)tag == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Label == tag.Label);
        }

        public override int GetHashCode()
        {
            return Label.GetHashCode();
        }

        public int CompareTo(Tag other)
        {
            return String.Compare(this.Label, other.Label);
        }

        public override string ToString()
        {
            return Label;
        }
    }
}