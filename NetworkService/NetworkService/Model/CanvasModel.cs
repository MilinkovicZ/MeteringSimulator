using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetworkService.Model
{
    public class CanvasModel : INotifyPropertyChanged
    {
        private TemperaturaReaktora temperatura;
        private bool zauzet;
        private Brush pozadina;
        private ObservableCollection<int> linijeID;
        private int pozicijaX, pozicijaY;

        public TemperaturaReaktora Temperatura
        {
            get => temperatura;
            set
            {
                temperatura = value;
                RaisePropertyChanged("Temperatura");
                RaisePropertyChanged("Foreground");
                RaisePropertyChanged("Text");
            }
        }

        public bool Zauzet
        {
            get => zauzet;
            set
            {
                zauzet = value;
                RaisePropertyChanged("Zauzet");
            }
        }

        public Brush Pozadina
        {
            get => pozadina;
            set
            {
                pozadina = value;
                RaisePropertyChanged("Pozadina");
            }
        }


        public int PozicijaX
        {
            get => pozicijaX;
            set
            {
                pozicijaX = value;
                RaisePropertyChanged("PozicijaX");
            }
        }

        public int PozicijaY
        {
            get => pozicijaY;
            set
            {
                pozicijaY = value;
                RaisePropertyChanged("PozicijaY");
            }
        }

        public ObservableCollection<int> LinijeID
        {
            get => linijeID;
            set
            {
                linijeID = value;
                RaisePropertyChanged("LinijeID");
            }
        }

        public CanvasModel(int id)
        {
            Pozadina = Brushes.GhostWhite;
            Zauzet = false;
            Temperatura = new TemperaturaReaktora();
            PozicijaX = 120 + (id / 4) * 108;  //POCETNO ODSTOJANJE + {0,1,2,3} * 108 U ZAVINOSTI KOJI RED
            PozicijaY = 220 + (id % 4 - 1) * 125;
            LinijeID = new ObservableCollection<int>();
        }

        public CanvasModel(TemperaturaReaktora temperatura, bool zauzet, Brush pozadina, int id)
        {
            Temperatura = temperatura;
            Zauzet = zauzet;
            Pozadina = pozadina;
            PozicijaX = 120 + (id / 4) * 108;
            PozicijaY = 220 + (id % 4 - 1) * 125;                //0-4-8-12 0
                                                                 //1-5-9-13 125
                                                                 //2-6-10-14 250
                                                                 //3-7-11-15 375
            LinijeID = new ObservableCollection<int>();
        }

        public string Text { get => Temperatura.Naziv != null ? "ID: " + Temperatura.Id + " Ime: " + Temperatura.Naziv : ""; }
        public string Foreground { get => (Temperatura.Temperatura >=250 && Temperatura.Temperatura <= 350) ? "Green" : "Red"; }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
