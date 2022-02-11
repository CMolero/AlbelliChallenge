using System.ComponentModel;

namespace AlbelliChallenge.Business.Models
{
    public class Enums
    {
        public enum ProductTypes
        {
            [Description("19mm")]
            PhotoBook = 1,
            [Description("10mm")]
            Calendar = 2,
            [Description("16mm")]
            Canvas = 3,
            [Description("4.7mm")]
            Cards = 4,
            [Description("94mm")]
            Mug = 5
        }
    }
}