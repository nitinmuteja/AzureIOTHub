using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarCodeScanning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuMaster : ContentPage
    {
        public ListView ListView;

        
        public void DisplayAlertMessage(string message)
        {

            DisplayAlert("Cloud Message", message, "OK");

        }

        public MasterMenuMaster()
        {
            InitializeComponent();
            BindingContext = new MasterMenuMasterViewModel();
           ListView = MenuItemsListView;
        }

        class MasterMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterMenuMenuItem> MenuItems { get; set; }

            public MasterMenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterMenuMenuItem>(new[]
                {
                    new MasterMenuMenuItem { Id = 0, Title = "Barcode Scanner", TargetType = typeof(ScanBarcode)},
                    new MasterMenuMenuItem { Id = 1, Title = "Barcode Generator" , TargetType = typeof(CreateBarcode)},          
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}