using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class TemperaturaReaktoraType
    {
        string imeTipa;
        string izvorSlike;

        public TemperaturaReaktoraType()
        {

        }

        public TemperaturaReaktoraType(string imeTipa, string izvorSlike)
        {
            this.imeTipa = imeTipa;
            this.izvorSlike = izvorSlike;
        }

        public string ImeTipa
        {
            get => imeTipa;
            set
            {
                imeTipa = value;                
            }
        }
        public string IzvorSlike
        {
            get => izvorSlike;
            set
            {
                izvorSlike = value;
            }
        }        
    }
}
