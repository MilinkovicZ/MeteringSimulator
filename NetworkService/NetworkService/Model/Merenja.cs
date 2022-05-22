using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Merenja : BindableBase
    {
        private string imeEntiteta;
        private ObservableCollection<Entitet> entiteti;

        public string ImeEntiteta
        {
            get => imeEntiteta;
            set
            {
                imeEntiteta = value;
                OnPropertyChanged("ImeEntiteta");
            }
        }

        public ObservableCollection<Entitet> Entiteti
        {
            get => entiteti;
            set
            {
                entiteti = value;
                OnPropertyChanged("Entiteti");
            }
        }

        public Merenja()
        {
            ImeEntiteta = "";
            Entiteti = new ObservableCollection<Entitet>();
        }

        public Merenja(string imeEntiteta, ObservableCollection<Entitet> entiteti)
        {
            this.ImeEntiteta = imeEntiteta;
            this.Entiteti = entiteti;
        }
    }
}
