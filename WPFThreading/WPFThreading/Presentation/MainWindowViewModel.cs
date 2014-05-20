using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary;
using System.Windows.Input;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using RandomItemsGenerator.Business;
using System.Threading.Tasks;
using WPFThreading.Business;

namespace WPFThreading.Presentation
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Button button;
        private IDispatcherWrapper dispatcherWrapper;
        private Dispatcher dispatcher;

        private string thread1Data;
        public string Thread1Data 
        { 
            get {return thread1Data; } 
            set
            {
                if (thread1Data != value)
                {
                    thread1Data = value;
                    RaisePropertyChanged<string>(() => this.Thread1Data);
                }
            }
        }

        private string thread2Data;
        public string Thread2Data
        {
            get { return thread2Data; }
            set
            {
                if (thread2Data != value)
                {
                    thread2Data = value;
                    RaisePropertyChanged<string>(() => this.Thread2Data);
                }
            }
        }

        private Dictionary<int, Item> itemsDictionary = new Dictionary<int, Item>();
        public ObservableCollection<Item> Items { get; private set; }

        private readonly MainWindowModel model;

        public MainWindowViewModel()
        {
            dispatcherWrapper = new DispatcherWrapper();
            dispatcher = dispatcherWrapper.Dispatcher;

            ButtonItemCommand = new DelegateCommand(ExecuteButtonItemCommand);
            ButtonCommand = new DelegateCommand(ExecuteButtonCommand);
            Items = new ObservableCollection<Item>();

            model = new MainWindowModel();
            model.ConsumerEvent += OnConsumerEvent;
        }

        public ICommand ButtonCommand { get; private set; }
        public void ExecuteButtonCommand(object parameter)
        {
            var d1 = ((System.Windows.Controls.Button)parameter).Dispatcher;
            var d2 = App.Current.Dispatcher;
            var d3 = Dispatcher.CurrentDispatcher;

            button = parameter as Button;

            new Thread(() => Thread1Data = "5000").Start();
            new Thread(() => Thread2Data = "8000").Start();

            //Items.Add("Fourth");

            model.StartDataGeneration();
        }

        private void OnConsumerEvent(object sender, ConsumerEventArgs e)
        {
            if (App.Current != null)
                App.Current.Dispatcher.Invoke(() => GUIUpdater(e.Data));
        }

        private void GUIUpdater(object data)
        {
            //Refresh
            Item item = (Item)data;
            itemsDictionary[item.ID] = item;
            Item oldItem = Items.FirstOrDefault(i => i.ID == item.ID);
            if (oldItem == null)
            {
                Items.Add(item);
            }
            else
            {
                int oldItemIdex = Items.IndexOf(oldItem);
                Items[oldItemIdex] = item;
            }
            //Items = new ObservableCollection<Item>(itemsDictionary.Values);
            //RaisePropertyChanged("Items");

            //Items.Insert(0, item);
            //if (Items.Count > 10)
            //    Items.RemoveAt(Items.Count - 1);
        }

        public ICommand ButtonItemCommand { get; private set; }
        public void ExecuteButtonItemCommand(object parameter)
        {
            Debug.WriteLine("ButtonItem pressed!");
        }
        

    }
}
