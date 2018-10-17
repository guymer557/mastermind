using System.Drawing;

namespace Mindmaster
{
    public class GameColors
    {
        private static readonly Color[] sr_ListOfColor = new Color[]
        {
            Color.Purple,
            Color.Red,
            Color.Chartreuse,
            Color.Aquamarine,
            Color.Blue,
            Color.Yellow,
            Color.Maroon,
            Color.White
        };

        public static Color GetColorName(int i_ColorNum)
        {
            return sr_ListOfColor[i_ColorNum];
        }

        public static int GetColorNumber(Color i_ColorName)
        {
            int index = 0;
            foreach(Color color in sr_ListOfColor)
            {
                if (color.Equals(i_ColorName))
                {
                    break;
                }

                index++;
            }

            return index + 1;
        }
    }
}
