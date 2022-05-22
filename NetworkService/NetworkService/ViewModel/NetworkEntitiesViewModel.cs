using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace NetworkService.ViewModel
{
    public class NetworkEntitiesViewModel : BindableBase
    {
        public static ObservableCollection<TemperaturaReaktora> Temperature { get; set; }
        public static ObservableCollection<TemperaturaReaktoraType> Tipovi { get; set; }
        public static ICollectionView Filter { get; set; }

        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        public MyICommand FilterCommand { get; set; }
        public MyICommand ClearFilterCommand { get; set; }
        public MyICommand UndoCommand { get; set; }

        public NetworkEntitiesViewModel()
        {
            DoThis();
            AddCommand = new MyICommand(OnAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            FilterCommand = new MyICommand(OnFilter, CanFilter);
            ClearFilterCommand = new MyICommand(ClearFilter);
            UndoCommand = new MyICommand(DoUndo);
        }

        private void DoThis()
        {
            if(Temperature == null)
            Temperature = new ObservableCollection<TemperaturaReaktora>();

            Filter = CollectionViewSource.GetDefaultView(Temperature);

            Tipovi = new ObservableCollection<TemperaturaReaktoraType>();
            Tipovi.Add(new TemperaturaReaktoraType { ImeTipa = "RTD", IzvorSlike = @"D:\Zdravko\Faks\Interakcija covek racunar\HCI Projekat II\RTD.jpg" });
            Tipovi.Add(new TemperaturaReaktoraType { ImeTipa = "TermoSprega", IzvorSlike = @"D:\Zdravko\Faks\Interakcija covek racunar\HCI Projekat II\TermoSprega.jpg" });
        }
        
        private TemperaturaReaktora temp = new TemperaturaReaktora();

        public TemperaturaReaktora Temp
        {
            get => temp;
            set
            {
                temp = value;
                OnPropertyChanged("Temp");
            }
        }

        private TemperaturaReaktora selected;

        public TemperaturaReaktora Selected
        {
            get => selected;
            set
            {
                selected = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        private bool filterV;

        public bool FilterV
        {
            get => filterV;
            set
            {
                filterV = value;
                OnPropertyChanged("FilterV");
                FilterCommand.RaiseCanExecuteChanged();
            }
        }

        private bool filterM;

        public bool FilterM
        {
            get => filterM;
            set
            {
                filterM = value;
                OnPropertyChanged("FilterM");
                FilterCommand.RaiseCanExecuteChanged();
            }
        }

        private int filterID;

        public int FilterID
        {
            get => filterID;
            set
            {
                filterID = value;
                OnPropertyChanged("FilterID");
                FilterCommand.RaiseCanExecuteChanged();
            }
        }

        private TemperaturaReaktoraType filterT;

        public TemperaturaReaktoraType FilterT
        {
            get => filterT;
            set
            {
                filterT = value;
                OnPropertyChanged("FilterT");
                FilterCommand.RaiseCanExecuteChanged();
            }
        }

        private bool filterInRange;

        public bool FilterInRange
        {
            get => filterInRange;
            set
            {
                filterInRange = value;
                OnPropertyChanged("FilterInRange");
                FilterCommand.RaiseCanExecuteChanged();
            }
        }

        private bool filterOutOfRange;

        public bool FilterOutOfRange
        {
            get => filterOutOfRange;
            set
            {
                filterOutOfRange = value;
                OnPropertyChanged("FilterOutOfRange");
                FilterCommand.RaiseCanExecuteChanged();
            }
        }

        private string duplicateIdError;
        public string DuplicateIdError
        {
            get => duplicateIdError;
            set
            {
                duplicateIdError = value;
                OnPropertyChanged("DuplicateIdError");
            }
        }        

        private void OnAdd()
        {
            Temp.Validate();

            List<int> ids = new List<int>();
            foreach (TemperaturaReaktora tr in Temperature)
            {
                ids.Add(tr.Id);
            }

            if (!ids.Contains(Temp.Id))
            {                
                if(Temp.IsValid)
                    {
                    Temperature.Add(new TemperaturaReaktora
                    {
                        Id = Temp.Id,
                        Naziv = Temp.Naziv,
                        Tip = Temp.Tip,
                        Temperatura = 0
                    });

                    NetworkDisplayViewModel.listaTemperatura.Add(new TemperaturaReaktora
                    {
                        Id = Temp.Id,
                        Naziv = Temp.Naziv,
                        Tip = Temp.Tip,
                        Temperatura = 0
                    });

                    MeasurementGraphViewModel.TemperatureReaktora = Temperature;

                    if(Temp.Tip.ImeTipa == "RTD")
                    {
                        MeasurementGraphViewModel.BrojRTD++;
                    }
                    else if (Temp.Tip.ImeTipa == "TermoSprega")
                    {
                        MeasurementGraphViewModel.BrojTS++;
                    }

                    DuplicateIdError = "";
                    OnPropertyChanged("TemperatureReaktora");
                    OnPropertyChanged("Temperature");
                    OnPropertyChanged("listaTemperatura");                    
                }                
            }
            else
            {
                DuplicateIdError = "Uneti ID postoji!";
            }
            //Temp = new TemperaturaReaktora();  Ako zelimo da se nakon uspesnog unosenja obrisu trenutni podaci.
        }

        private void OnDelete()
        {
            NetworkDisplayViewModel.ObrisiIzListe(Selected);

            

            for (int i = 0; i < 16; i++)
            {
                TemperaturaReaktora trTemp = NetworkDisplayViewModel.Canvasi[i].Temperatura;
                if (trTemp == Selected)
                {
                    foreach (int j in NetworkDisplayViewModel.Canvasi[i].LinijeID)
                    {
                        NetworkDisplayViewModel.ObrisiLiniju(j);
                    }
                }
            }

            if(Selected.Tip.ImeTipa == "RTD")
            {
                MeasurementGraphViewModel.BrojRTD--;
            }
            else if (Selected.Tip.ImeTipa == "TermoSprega")
            {
                MeasurementGraphViewModel.BrojTS--;
            }

            Temperature.Remove(Selected);
            MeasurementGraphViewModel.TemperatureReaktora = Temperature;
            
            OnPropertyChanged("TemperatureReaktora");
            OnPropertyChanged("Temperature");
            OnPropertyChanged("listaTemperatura");            
        }

        private void DoUndo()
        {

            if (filterOutOfRange)
            {
                FilterOutOfRange = false;
                Filter.Filter = FilterPoSvemu;
            }
            else
            {
                int index = Temperature.Count - 1;
                Temperature.RemoveAt(index);
            }
        }

        private bool CanDelete()
        {
            return Selected != null;
        }

        private void OnFilter()
        {
            if (FilterM && FilterID > 0)
            {
                if (FilterT != null)
                {
                    if(FilterInRange)
                    {
                        Filter.Filter = FilterPoMTipIn;
                    }
                    else if (FilterOutOfRange)
                    {
                        Filter.Filter = FilterPoMTipOut;
                    }
                    else
                    {
                        Filter.Filter = FilterPoMTip;
                    }
                }
                else
                {
                    if (FilterInRange)
                    {
                        Filter.Filter = FilterPoMIn;
                    }
                    else if (FilterOutOfRange)
                    {
                        Filter.Filter = FilterPoMOut;
                    }
                    else
                    {
                        Filter.Filter = FilterPoM;
                    }
                }
            }
            else if (FilterV && FilterID > 0)
            {
                if (FilterT != null)
                {
                    if (FilterInRange)
                    {
                        Filter.Filter = FilterPoVTipIn;
                    }
                    else if (FilterOutOfRange)
                    {
                        Filter.Filter = FilterPoVTipOut;
                    }
                    else
                    {
                        Filter.Filter = FilterPoVTip;
                    }
                }
                else
                {
                    if (FilterInRange)
                    {
                        Filter.Filter = FilterPoVIn;
                    }
                    else if (FilterOutOfRange)
                    {
                        Filter.Filter = FilterPoVOut;
                    }
                    else
                    {
                        Filter.Filter = FilterPoV;
                    }
                }
            }
            else
            {
                if (FilterT != null)
                {
                    if (FilterInRange)
                    {
                        Filter.Filter = FilterPoTipIn;
                    }
                    else if (FilterOutOfRange)
                    {
                        Filter.Filter = FilterPoTipOut;
                    }
                    else
                    {
                        Filter.Filter = FilterPoTip;
                    }
                }
                else
                {
                    if (FilterInRange)
                    {
                        Filter.Filter = FilterPoIn;
                    }
                    else if (FilterOutOfRange)
                    {
                        Filter.Filter = FilterPoOut;
                    }
                }
            }
            Filter.Refresh();
        }       

        private bool CanFilter()
        {
            bool canFilter = ((FilterM || FilterV) && (FilterID > 0)) || FilterT != null || FilterInRange || FilterOutOfRange;
            return canFilter;
        }

        private void ClearFilter()
        {
            FilterM = false;
            FilterV = false;
            FilterT = null;
            FilterID = 0;
            FilterInRange = false;
            FilterOutOfRange = false;
            Filter.Filter = FilterPoSvemu;
        }

        //TIP + M/V

        private bool FilterPoTip(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Tip.ImeTipa == FilterT.ImeTipa;
        }

        private bool FilterPoM(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Id < FilterID;
        }

        private bool FilterPoV(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Id > FilterID;
        }

        private bool FilterPoMTip(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Id < FilterID && tr.Tip.ImeTipa == FilterT.ImeTipa;
        }

        private bool FilterPoVTip(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Id > FilterID && tr.Tip.ImeTipa == FilterT.ImeTipa;
        }

        // RANGE

        private bool FilterPoIn(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Temperatura >= 250 && tr.Temperatura <= 350;
        }

        private bool FilterPoOut(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Temperatura < 250 || tr.Temperatura > 350;
        }
        
        // RANGE + TIP

        private bool FilterPoTipIn(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Tip.ImeTipa == FilterT.ImeTipa && tr.Temperatura >= 250 && tr.Temperatura <= 350;
        }

        private bool FilterPoTipOut(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Tip.ImeTipa == FilterT.ImeTipa && (tr.Temperatura < 250 || tr.Temperatura > 350);
        }

        //RANGE + M/V

        private bool FilterPoMIn(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Id < FilterID && tr.Temperatura >= 250 && tr.Temperatura <= 350;
        }

        private bool FilterPoMOut(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Id < FilterID && (tr.Temperatura < 250 || tr.Temperatura > 350);
        }

        private bool FilterPoVIn(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Id > FilterID && tr.Temperatura >= 250 && tr.Temperatura <= 350;
        }

        private bool FilterPoVOut(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Id > FilterID && (tr.Temperatura < 250 || tr.Temperatura > 350);
        }

        //RANGE + TIP + M/V

        private bool FilterPoMTipIn(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Tip.ImeTipa == FilterT.ImeTipa && tr.Id < FilterID && tr.Temperatura >= 250 && tr.Temperatura <= 350;
        }

        private bool FilterPoMTipOut(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Tip.ImeTipa == FilterT.ImeTipa && tr.Id < FilterID && (tr.Temperatura < 250 || tr.Temperatura > 350);
        }

        private bool FilterPoVTipIn(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Tip.ImeTipa == FilterT.ImeTipa && tr.Id > FilterID && tr.Temperatura >= 250 && tr.Temperatura <= 350;
        }

        private bool FilterPoVTipOut(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return tr.Tip.ImeTipa == FilterT.ImeTipa && tr.Id > FilterID && (tr.Temperatura < 250 || tr.Temperatura > 350);
        }

        private bool FilterPoSvemu(object obj)
        {
            TemperaturaReaktora tr = obj as TemperaturaReaktora;
            return true;
        }
    }
}
