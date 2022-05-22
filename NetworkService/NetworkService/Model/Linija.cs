using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Linija : BindableBase
    {
        private int x1;
        private int x2;
        private int y1;
        private int y2;
        private static int i = 0;
        private int id;

        public Linija(int x1, int x2, int y1, int y2)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
            Id = i++;
        }

        public int X1
        {
            get => x1;
            set
            {
                x1 = value;
                OnPropertyChanged("X1");
            }
        }

        public int X2
        {
            get => x2;
            set
            {
                x2 = value;
                OnPropertyChanged("X2");
            }
        }

        public int Y1
        {
            get => y1;
            set
            {
                y1 = value;
                OnPropertyChanged("Y1");
            }
        }

        public int Y2
        {
            get => y2;
            set
            {
                y2 = value;
                OnPropertyChanged("Y2");
            }
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
    }
}
