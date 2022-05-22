using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class TemperaturaReaktora : ValidationBase
    {
        int id;
        string naziv;
        TemperaturaReaktoraType tip;
        double temperatura;

        public TemperaturaReaktora()
        {

        }

        public TemperaturaReaktora(int id, string naziv, TemperaturaReaktoraType tip, double temperatura)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Tip = tip;
            this.Temperatura = temperatura;
        }

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Naziv
        {
            get => naziv;
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }
        public TemperaturaReaktoraType Tip
        {
            get => tip;
            set
            {
                tip = value;
                OnPropertyChanged("Tip");
            }
        }
        public double Temperatura
        {
            get => temperatura;
            set
            {
                if(temperatura != value)
                {
                    temperatura = value;
                    OnPropertyChanged("Temperatura");
                }                
            }
        }

        protected override void ValidateSelf()
        {
            if(this.Id <= 0)
            {
                this.ValidationErrors["Id"] = "Nevalidan ID!";
            }
            if(string.IsNullOrWhiteSpace(this.Naziv))
            {
                this.ValidationErrors["Naziv"] = "Naziv ne moze biti prazan!";
            }
            
        }

        public override string ToString()
        {
            string s = "ID: " + Id + " Ime: " + Naziv;
            return s;
        }
    }
}
