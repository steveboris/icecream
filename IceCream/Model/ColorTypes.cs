using IceCream.Interfaces;

namespace IceCream.Model
{
    public class ColorTypes : IColor
    {
        public string Black { get => BLACK; }
        public string Red { get => RED; }
        public string Green { get => GREEN; }

        private const string RED = "#FF0000";
        private const string GREEN = "#00FF00";
        private const string BLACK = "#3e3e3e";
    }
}
