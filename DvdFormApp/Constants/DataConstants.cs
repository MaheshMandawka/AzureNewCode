using System.Collections.Generic;

namespace DvdFormApp.Constants
{
    public static class DataConstants
    {
        public static class ItemConstants
        {
            public const string DvD = "DvD";
            public const string Book = "Book";

            public static readonly List<string> ItemTypes = new List<string>
            {
                DvD,
                Book,
            };
        }
    }
}
