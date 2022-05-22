using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    public class NetworkDisplayViewModel : BindableBase
    {        
        public static ObservableCollection<TemperaturaReaktora> listaTemperatura { get; set; }
        public static ObservableCollection<CanvasModel> Canvasi { get; set; }
        public static ObservableCollection<Linija> LinijeCanvasa { get; set; }

        public MyICommand<ListView> SelectionChangedCommand { get; set; }
        public MyICommand MouseLeftButtonUpCommand { get; set; }
        public MyICommand<Canvas> MouseLeftButtonDownCommand { get; set; }
        public MyICommand<Canvas> ButtonCommand { get; set; }
        public MyICommand<Canvas> DragOverCommand { get; set; }
        public MyICommand<Canvas> DropCommand { get; set; }

        private bool dragging = false;

        public NetworkDisplayViewModel()
        {
            if(listaTemperatura == null)
            {
                listaTemperatura = new ObservableCollection<TemperaturaReaktora>();
            }
            
            if (Canvasi == null)
            {
                Canvasi = new ObservableCollection<CanvasModel>();    
                
                for (int i = 0; i < 16; i++)
                {
                    Canvasi.Add(new CanvasModel(i));
                }                
            }

            if(LinijeCanvasa == null)
            {
                LinijeCanvasa = new ObservableCollection<Linija>();
            }
                
            SelectionChangedCommand = new MyICommand<ListView>(SelectionChanged);            
            MouseLeftButtonUpCommand = new MyICommand(MouseLeftButtonUp);
            MouseLeftButtonDownCommand = new MyICommand<Canvas>(MouseLeftButtonDown);
            ButtonCommand = new MyICommand<Canvas>(ButtonFreeCommand);
            DragOverCommand = new MyICommand<Canvas>(DragOver);
            DropCommand = new MyICommand<Canvas>(Drop);
        }
               
        private TemperaturaReaktora selectedTemperatura;

        public TemperaturaReaktora SelectedTemperatura
        {
            get => selectedTemperatura;
            set
            {
                selectedTemperatura = value;
                OnPropertyChanged("SelectedTemperatura");
            }
        }

        private TemperaturaReaktora currentTemperatura;

        public TemperaturaReaktora CurrentTemperatura
        {
            get => currentTemperatura;
            set
            {
                currentTemperatura = value;
                OnPropertyChanged("CurrentTemperatura");
            }
        }

        private CanvasModel selectedUCanvasu;
        public CanvasModel SelectedUCanvasu
        {
            get => selectedUCanvasu;
            set
            {
                selectedUCanvasu = value;
                OnPropertyChanged("SelectedUCanvasu");
            }
        }

        private void SelectionChanged(ListView lista)
        {
            if (!dragging)
            {
                dragging = true;
                CurrentTemperatura = SelectedTemperatura;
                DragDrop.DoDragDrop(lista, CurrentTemperatura, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void ButtonFreeCommand(Canvas c)
        {
            int idCanvasa = int.Parse(c.Name.Substring(6));
            if (Canvasi[idCanvasa].Zauzet)
            {
                foreach (int i in Canvasi[idCanvasa].LinijeID)
                {
                    ObrisiLiniju(i);
                }
                listaTemperatura.Add(Canvasi[idCanvasa].Temperatura);
                Canvasi[idCanvasa] = new CanvasModel(idCanvasa);
            }            
        }

        private void MouseLeftButtonUp()
        {
            CurrentTemperatura = null;
            SelectedTemperatura = null;
            SelectedUCanvasu = null;
            dragging = false;
        }

        private void MouseLeftButtonDown(Canvas c)
        {
            int idCanvasa = int.Parse(c.Name.Substring(6));
            if (Canvasi[idCanvasa].Zauzet)
            {
                SelectedUCanvasu = Canvasi[idCanvasa];
                if(!dragging)
                {
                    dragging = true;
                    DragDrop.DoDragDrop(c, SelectedUCanvasu, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private void DragOver(Canvas c)
        {
            int idCanvasa = int.Parse(c.Name.Substring(6));
            if(!Canvasi[idCanvasa].Zauzet)
            {
                c.AllowDrop = false;
            }
            else
            {
                c.AllowDrop = true;
            }
        }        

        private void Drop(Canvas c)
        {
            if (SelectedTemperatura != null)
            {
                int idCanvasa = int.Parse(c.Name.Substring(6));
                if (!Canvasi[idCanvasa].Zauzet)
                {
                    BitmapImage slika = new BitmapImage();
                    slika.BeginInit();
                    slika.UriSource = new Uri(SelectedTemperatura.Tip.IzvorSlike);
                    slika.EndInit();
                    Canvasi[idCanvasa] = new CanvasModel(SelectedTemperatura, true, new ImageBrush(slika),idCanvasa);
                    listaTemperatura.Remove(SelectedTemperatura);
                }
                SelectedTemperatura = null;
                dragging = false;
            }
            else if (SelectedUCanvasu != null)
            {
                int idCanvasa = int.Parse(c.Name.Substring(6));
                if (!Canvasi[idCanvasa].Zauzet)
                {
                    BitmapImage slika = new BitmapImage();
                    slika.BeginInit();
                    slika.UriSource = new Uri(SelectedUCanvasu.Temperatura.Tip.IzvorSlike);
                    slika.EndInit();                    

                    for (int i = 0; i < 16; i++)
                    {
                        if(Canvasi[i].Temperatura == SelectedUCanvasu.Temperatura)
                        {
                            Canvasi[i] = new CanvasModel(i);
                            break;
                        }                        
                    }
                    Canvasi[idCanvasa] = new CanvasModel(SelectedUCanvasu.Temperatura, true, new ImageBrush(slika), idCanvasa);

                    foreach (int i in SelectedUCanvasu.LinijeID)
                    {
                        DodajLiniju(i, SelectedUCanvasu.PozicijaX, SelectedUCanvasu.PozicijaY, Canvasi[idCanvasa].PozicijaX, Canvasi[idCanvasa].PozicijaY);
                        Canvasi[idCanvasa].LinijeID.Add(i);
                    }
                }

                else
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (Canvasi[i].Temperatura == SelectedUCanvasu.Temperatura)
                        {
                            Linija l = new Linija(Canvasi[i].PozicijaX, Canvasi[idCanvasa].PozicijaX, Canvasi[i].PozicijaY, Canvasi[idCanvasa].PozicijaY);
                            LinijeCanvasa.Add(l);
                            Canvasi[i].LinijeID.Add(l.Id);
                            Canvasi[idCanvasa].LinijeID.Add(l.Id);
                            break;
                        }
                    }
                }

                SelectedTemperatura = null;
                SelectedUCanvasu = null;
                dragging = false;
            }
        }

        public static void ObrisiIzListe(TemperaturaReaktora t)
        {
            foreach (TemperaturaReaktora tr in listaTemperatura)
            {
                if (tr.Id == t.Id)
                {
                    listaTemperatura.Remove(tr);
                    return;
                }
            }
            for (int i = 0; i < 16; i++)
            {
                if (Canvasi[i].Temperatura.Id == t.Id)
                {
                    Canvasi[i] = new CanvasModel(i);
                    return;
                }
            }
        }

        //REALTIME

        public static void AzurirajListu(TemperaturaReaktora tr)
        {
            for (int i = 0; i < listaTemperatura.Count; i++)
            {
                if (listaTemperatura[i].Id == tr.Id)
                {
                    listaTemperatura[i].Temperatura = tr.Temperatura;
                    return;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                if (Canvasi[i].Temperatura.Id == tr.Id)
                {
                    Canvasi[i].Temperatura = tr;
                    return;
                }
            }
        }

        //LINIJA POKUSAJ 2

        public static void ObrisiLiniju(int idL)
        {
            for (int i = 0; i < LinijeCanvasa.Count; i++)
            {
                if(LinijeCanvasa[i].Id == idL)
                {
                    LinijeCanvasa.RemoveAt(i);
                    return;
                }
            }
        }

        public void DodajLiniju(int idL, int pozX, int pozY, int noviX, int noviY)
        {
            for (int i = 0; i < LinijeCanvasa.Count; i++)
            {
                if(LinijeCanvasa[i].Id == idL)
                {
                    if(LinijeCanvasa[i].X1 == pozX && LinijeCanvasa[i].Y1 == pozY)
                    {
                        LinijeCanvasa[i].X1 = noviX;
                        LinijeCanvasa[i].Y1 = noviY;
                    }
                    else
                    {
                        LinijeCanvasa[i].X2 = noviX;
                        LinijeCanvasa[i].Y2 = noviY;
                    }
                }
            }
        }

        //POKUSAVANJE LINIJA

        //private int linija0visina;
        //private int linija0duzina;

        //public int Linija0visina
        //{
        //    get => linija0visina;
        //    set
        //    {
        //        linija0visina = value;
        //        OnPropertyChanged("Linija0visina");
        //    }
        //}

        //public int Linija0duzina
        //{
        //    get => linija0duzina;
        //    set
        //    {
        //        linija0duzina = value;
        //        OnPropertyChanged("Linija0duzina");
        //    }
        //}

        //private void Linija()
        //{
        //    if(Canvasi[0].Zauzet)
        //    {
        //        if(Canvasi[1].Zauzet)
        //        {
        //            Linija0visina = 120;
        //            Linija0duzina = 5;
        //        }
        //        else if (Canvasi[2].Zauzet)
        //        {
        //            Linija0visina = 240;
        //            Linija0duzina = 5;
        //        }
        //        else if (Canvasi[3].Zauzet)
        //        {
        //            Linija0visina = 370;
        //            Linija0duzina = 5;
        //        }
        //        else if (Canvasi[4].Zauzet)
        //        {
        //            Linija0visina = 5;
        //            Linija0duzina = 100;
        //        }
        //        else if (Canvasi[8].Zauzet)
        //        {
        //            Linija0visina = 5;
        //            Linija0duzina = 215;
        //        }
        //        else if (Canvasi[12].Zauzet)
        //        {
        //            Linija0visina = 5;
        //            Linija0duzina = 325;
        //        }
        //        else
        //        {
        //            Linija0visina = 0;
        //            Linija0duzina = 0;
        //        }
        //    }
        //    else
        //    {
        //        Linija0visina = 0;
        //        Linija0duzina = 0;
        //    }
        //}
    }
}
