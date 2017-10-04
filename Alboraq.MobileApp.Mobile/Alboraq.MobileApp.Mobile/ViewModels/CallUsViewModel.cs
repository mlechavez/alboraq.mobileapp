using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class CallUsViewModel : INotifyPropertyChanged
    {        
        public CallUsViewModel()
        {     
            GetLocations();
        }
        public INavigation Navigation { get; set; }
        public Page Page { get; set; }

        private List<BranchLocation> _branchLocations;
        public List<BranchLocation> BranchLocations
        {
            get { return _branchLocations ?? (_branchLocations = new List<BranchLocation>()); }
            set {
                _branchLocations = value;
                OnPropertyChanged("BranchLocations");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CallLocationCommand
        {
            get
            {
                return new Command(()=> 
                {
                    Page.DisplayAlert("Test", "inside the viewmodels", "Ok", "Cance");
                });
            }
        }

        void GetLocations()
        {
            BranchLocations = new List<BranchLocation>
            {
                new BranchLocation { BranchName = "Porsche Centre Doha", Address = "Al Medina, Pearl", BranchCode = "sh", PhoneNumber = "+97444599666" },
                new BranchLocation { BranchName = "Porsche Quick Service", Address = "Al Medina, Pearl", BranchCode = "qs", PhoneNumber = "+97444599733" },
                new BranchLocation { BranchName = "Porsche Bodyshop", Address = "Al Medina, Pearl", BranchCode = "st27", PhoneNumber = "+97444599776" },
                new BranchLocation { BranchName = "Porsche Service Centre", Address = "Al Medina, Pearl", BranchCode = "st16", PhoneNumber = "+97444599800" }
            };
        }
    }
}
