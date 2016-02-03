using System.Collections.Generic;
using System.Linq;
using MyToolkit.Collections;
using MyToolkit.Mvvm;
using PainLogUWP.Enums;
using PainLogUWP.Models;
using PainLogUWP.Repositories;

namespace PainLogUWP.ViewModels
{
    public class PainViewModel : ViewModelBase 
    {

        private Pain _selectedPain;
        private List<Pain> _painList;
        private Repository<Pain> _repo;
        private string _filter;

        public PainViewModel()
        {
            AllElements = new MtObservableCollection<Pain>();
            LoadList();
            FilteredElements = new ObservableCollectionView<Pain>(AllElements);
        }
        public MtObservableCollection<Pain> AllElements { get; private set; }

        private async void LoadList()
        {
            _repo = new PainRepository();
            PainList = await _repo.GetAll();
            if (!PainList.Any())
                MockPainList();

            AllElements.Initialize(PainList);
        }

        public ObservableCollectionView<Pain> FilteredElements { get; private set; }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (Set(ref _filter, value))
                {
                    FilteredElements.Filter = entry =>
                        string.IsNullOrEmpty(_filter) ||
                        entry.BodyPart.ToString().Contains(_filter);
                }
            }
        }
        public Pain SelectedPain
        {
            get { return _selectedPain; }
            set { Set(ref _selectedPain, value); }
        }

        public List<Pain> PainList
        {
            get { return _painList; }
            set { Set(ref _painList, value); }
        }

        private void MockPainList()
        {
            for (int i = 0; i < 50; i++)
            {
                var pain = new Pain
                {
                    BodyPart = "Head",
                    PainType = PainType.Pulsing
                };

                _painList.Add(pain);
               // _repo.AddNew(pain);
            }
        }
    }
}