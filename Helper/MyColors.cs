using System.Drawing;
using System.Windows.Forms;

namespace PartyHax.Helper.MenuStrip
{
    public class Overrides : ToolStripProfessionalRenderer
    {
        public Overrides() : base(new MenuColors()) { }
    }
    public class MenuColors : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(192,0,0); }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Color.Maroon; }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return Color.Maroon; }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return Color.Maroon; }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.Orange; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.Maroon; }
        }
        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(192, 0, 0); }
        }
        public override Color MenuBorder
        {
            get { return Color.Maroon; }
        }

    }
}
