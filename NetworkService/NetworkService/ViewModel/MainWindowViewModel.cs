using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavigationCommand { get; private set; }
        public MyICommand ShortcutCommand { get; set; }
        private NetworkEntitiesViewModel NEVM = new NetworkEntitiesViewModel();
        private NetworkDisplayViewModel NDVM = new NetworkDisplayViewModel();
        private MeasurementGraphViewModel MGVM = new MeasurementGraphViewModel();     

        string path = @"D:\Zdravko\Faks\Interakcija covek racunar\HCI Projekat II\NetworkService\Log.txt";

        private BindableBase currentViewModel;

        public BindableBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public MainWindowViewModel()
        {
            createListener(); //Povezivanje sa serverskom aplikacijom            
            NavigationCommand = new MyICommand<string>(OnNavigation);
            ShortcutCommand = new MyICommand(Shortcut);
            CurrentViewModel = NEVM;
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 11111);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(NetworkEntitiesViewModel.Temperature.Count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Entitet_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji

                            if (NetworkEntitiesViewModel.Temperature.Count > 0)   //PUKNUCE AKO DELETUJEMO NEKI ENTITET I NE RESTARTUJEMO METERING SISTEM
                            {

                                string entitet = incomming.Split(':')[0];                 //Entitet
                                double vrednost = Double.Parse(incomming.Split(':')[1]);  //Vrednost entiteta4
                                DateTime vreme = DateTime.Now;

                                using (StreamWriter sw = File.AppendText(path))
                                {
                                    sw.WriteLine(DateTime.Now + ": " + entitet + ", " + vrednost);
                                }

                                int index = Int32.Parse(entitet.Split('_')[1]);
                                NetworkEntitiesViewModel.Temperature[index].Temperatura = vrednost;
                                NetworkDisplayViewModel.AzurirajListu(NetworkEntitiesViewModel.Temperature[index]);
                                MeasurementGraphViewModel.AzurirajMerenja(entitet, new Entitet(vrednost, vreme));
                            }  
                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        } 
        
        private void OnNavigation(string dest)
        {
            switch(dest)
            {
                case "NetEntity":
                    CurrentViewModel = NEVM;
                    break;
                case "NetDisplay":
                    CurrentViewModel = NDVM;
                    break;
                case "MesGraph":
                    CurrentViewModel = MGVM;
                    break;
            }
        }

        private void Shortcut()
        {
            if (CurrentViewModel == NDVM)
            {
                CurrentViewModel = MGVM;
            }                
            else if (CurrentViewModel == MGVM)
            {
                CurrentViewModel = NEVM;
            }                
            else if (CurrentViewModel == NEVM)
            {
                CurrentViewModel = NDVM;
            }                
        }
    }
}
