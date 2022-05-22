using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class MeasurementGraphViewModel : BindableBase
    {
        string path = @"D:\Zdravko\Faks\Interakcija covek racunar\HCI Projekat II\NetworkService\Log.txt";
        public static ObservableCollection<TemperaturaReaktora> TemperatureReaktora { get; set; }       
        public static ObservableCollection<Merenja> MerenjaTemperatura { get; set; }
        public MyICommand ShowGraphCommand { get; set; }

        static TemperaturaReaktora saveSelectedTemp = null;
        static Merenja saveSelectedMerenje = null;

        private TemperaturaReaktora selectedTemp;
        public TemperaturaReaktora SelectedTemp
        {
            get => selectedTemp;
            set
            {
                selectedTemp = value;
                saveSelectedTemp = value;
                OnPropertyChanged("SelectedTemp");
                ShowGraphCommand.RaiseCanExecuteChanged();
            }
        }

        private Merenja selectedMerenje;
        public Merenja SelectedMerenje
        {
            get => selectedMerenje;
            set
            {
                selectedMerenje = value;
                saveSelectedMerenje = value;
                OnPropertyChanged("SelectedMerenje");
            }
        }

        public MeasurementGraphViewModel()
        {
            if(TemperatureReaktora==null)
            TemperatureReaktora = new ObservableCollection<TemperaturaReaktora>();

            if (MerenjaTemperatura == null)
            {
                MerenjaTemperatura = new ObservableCollection<Merenja>();
                ReadFile();
            }

            ShowGraphCommand = new MyICommand(ButtonClick, CanClick);

            if (saveSelectedTemp != null)
                selectedTemp = saveSelectedTemp;
            if (saveSelectedMerenje != null)
                selectedMerenje = saveSelectedMerenje;
        }    
        
        private void ReadFile()
        {
            var podaci = File.ReadAllLines(path).Reverse();
            foreach(string s in podaci)
            {
                string ime = "";
                double vrednost = 0;
                string[] splitted = s.Split(' ');
                if(splitted.Length == 5)
                {
                    splitted[1] = splitted[1].Substring(0, splitted[1].Length - 1);
                    splitted[3] = splitted[3].Substring(0, splitted[3].Length - 1);
                    ime = splitted[3];
                    vrednost = double.Parse(splitted[4]);
                }
                else
                {
                    splitted[1] = splitted[1].Substring(0, splitted[1].Length - 1);
                    splitted[2] = splitted[2].Substring(0, splitted[2].Length - 1);
                    ime = splitted[2];
                    vrednost = double.Parse(splitted[3]);
                }


                if(!PostojiMerenje(ime))
                {
                    MerenjaTemperatura.Add(new Merenja(ime, new ObservableCollection<Entitet>()));
                }

                
                DateTime vreme = DateTime.Parse(splitted[0] + " " + splitted[1]);
                DodajEntitet(ime, new Entitet(vrednost, vreme), true);
            }

            for (int i = 0; i < MerenjaTemperatura.Count; i++)
            {
                MerenjaTemperatura[i].Entiteti = new ObservableCollection<Entitet>(MerenjaTemperatura[i].Entiteti.Reverse().ToList());
            }
        }

        private static bool PostojiMerenje(string ime)
        {
            foreach(Merenja m in MerenjaTemperatura)
            {
                if(m.ImeEntiteta == ime)
                {
                    return true;
                }
            }
            return false;
        }

        private static void DodajEntitet(string ime, Entitet e, bool b)
        {
            for (int i = 0; i < MerenjaTemperatura.Count; i++)
            {
                if (MerenjaTemperatura[i].ImeEntiteta == ime)
                {
                    if (MerenjaTemperatura[i].Entiteti.Count >= 5 && b)
                    {
                        return;
                    }
                    MerenjaTemperatura[i].Entiteti.Add(e);
                }
            }
        }

        private string ConvertEntityId()
        {
            for (int i = 0; i < NetworkEntitiesViewModel.Temperature.Count; i++)
            {
                if(NetworkEntitiesViewModel.Temperature[i].Id == SelectedTemp.Id)
                {
                    return "Entitet_" + i;
                }
            }
            return null;
        }

        private Merenja SelectedMerenjeById()
        {
            string id = ConvertEntityId();
            foreach (Merenja m in MerenjaTemperatura)
            {
                if(m.ImeEntiteta == id)
                {
                    return m;
                }
            }
            return null;
        }

        private void ButtonClick()
        {
            SelectedMerenje = SelectedMerenjeById();
        }

        private bool CanClick()
        {
            return SelectedTemp != null;
        }

        //REALTIME MENJANJE

        private static void IzbaciPoslednjeg(string ime)
        {
            for (int i = 0; i < MerenjaTemperatura.Count; i++)
            {
                if (MerenjaTemperatura[i].ImeEntiteta == ime && MerenjaTemperatura[i].Entiteti.Count > 5)
                {
                    MerenjaTemperatura[i].Entiteti.RemoveAt(0);
                    return;
                }
            }
        }

        public static void AzurirajMerenja(string ime, Entitet e)
        {
            if(PostojiMerenje(ime))
            {
                DodajEntitet(ime, e, false);
                IzbaciPoslednjeg(ime);
            }
            else
            {
                MerenjaTemperatura.Add(new Merenja(ime, new ObservableCollection<Entitet>()));
                DodajEntitet(ime, e, true);
            }
        }

        //2. GRAF

        private static int brojRTD = 0;
        private static int brojTS = 0;

        public static int BrojRTD { get => brojRTD; set => brojRTD = value; }
        public static int BrojTS { get => brojTS; set => brojTS = value; }

        public int BrojUkupno
        {
            get => BrojRTD + BrojTS;
        }

        public double RTD
        {
            get => (double)BrojRTD / BrojUkupno * 285;
        }

        public double TS
        {
            get => (double)BrojTS / BrojUkupno * 285;
        }

        public string RTDString
        {
            get => BrojRTD == 0 ? "" : BrojRTD.ToString();
        }

        public string TSString
        {
            get => BrojTS == 0 ? "" : BrojTS.ToString();
        }

    }
}
