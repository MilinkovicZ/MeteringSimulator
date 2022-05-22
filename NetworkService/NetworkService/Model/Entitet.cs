using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetworkService.Model
{
    public class Entitet : BindableBase
    {
        double vrednost;
        DateTime datum;

        public double Vrednost
        {
            get => vrednost;
            set
            {
                vrednost = value;
                OnPropertyChanged("Vrednost");
                OnPropertyChanged("Boja");                
                OnPropertyChanged("SkracenDatum");
                OnPropertyChanged("IzmerenaVisinaLinija");
            }
        }

        public DateTime Datum
        {
            get => datum;
            set
            {
                datum = value;
                OnPropertyChanged("Datum");
                OnPropertyChanged("SkracenDatum");
            }
        }

        public string SkracenDatum { get => Vrednost == 0 ? "" : Datum.ToString("t"); } //Videti za null

        public double IzmerenaVisinaLinija { get => 85 * Vrednost/100; }

        public Brush Boja
        {
            get
            {
                if (Vrednost == 0)
                    return Brushes.Transparent;
                else if (Vrednost >= 250 && Vrednost <= 350)
                    return Brushes.Green;
                else
                    return Brushes.Red;
            }
        }

        public Entitet()
        {
            Vrednost = 0;
        }

        public Entitet(double v, DateTime d)
        {
            this.Vrednost = v;
            this.Datum = d;
        }
    }
}
