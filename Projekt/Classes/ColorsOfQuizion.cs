using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Projekt
{
    class ColorsOfQuizion
    {
        private SolidColorBrush primary;
        private SolidColorBrush primaryVariant;
        private SolidColorBrush secondary;
        private SolidColorBrush secondaryVariant;
        private SolidColorBrush onSecondary;
        private SolidColorBrush onPrimary;
        private SolidColorBrush warning;
        private SolidColorBrush alert;
        private SolidColorBrush fine;
        private SolidColorBrush white;
        private SolidColorBrush black;

        public ColorsOfQuizion()
        {
            this.primary = (SolidColorBrush)new BrushConverter().ConvertFrom("#50508E");
            this.primaryVariant = (SolidColorBrush)new BrushConverter().ConvertFrom("#211A52");
            this.secondary = (SolidColorBrush)new BrushConverter().ConvertFrom("#7985C1");
            this.secondaryVariant = (SolidColorBrush)new BrushConverter().ConvertFrom("#5B6AB0");
            this.onSecondary = (SolidColorBrush)new BrushConverter().ConvertFrom("#4053A0");
            this.onPrimary = (SolidColorBrush)new BrushConverter().ConvertFrom("#E8E7F5");
            this.warning = (SolidColorBrush)new BrushConverter().ConvertFrom("#BA0100");
            this.alert = (SolidColorBrush)new BrushConverter().ConvertFrom("#BAA100");
            this.fine = (SolidColorBrush)new BrushConverter().ConvertFrom("#1CBA00");
            this.white = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFFFF");
            this.black = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF000000");
            
           
        }
        public SolidColorBrush Primary { get => primary; set => primary = value; }
        public SolidColorBrush PrimaryVariant { get => primaryVariant; set => primaryVariant = value; }
        public SolidColorBrush Secondary { get => secondary; set => secondary = value; }
        public SolidColorBrush SecondaryVariant { get => secondaryVariant; set => secondaryVariant = value; }
        public SolidColorBrush OnSecondary { get => onSecondary; set => onSecondary = value; }
        public SolidColorBrush OnPrimary { get => onPrimary; set => onPrimary = value; }
        public SolidColorBrush Warning { get => warning; set => warning = value; }
        public SolidColorBrush Alert { get => alert; set => alert = value; }
        public SolidColorBrush Fine { get => fine; set => fine = value; }
        public SolidColorBrush White { get => white; set => white = value; }
        public SolidColorBrush Black { get => black; set => black = value; }
    }
}
