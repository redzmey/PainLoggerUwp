using System;
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
        private string _filter;
        private List<Pain> _painList;
        private Repository<Pain> _repo;

        private Pain _selectedPain;
        private List<string> _suggestedValues;

        public PainViewModel()
        {
            AllElements = new MtObservableCollection<Pain>();
            LoadList();
            FilteredElements = new ObservableCollectionView<Pain>(AllElements);
        }

        public MtObservableCollection<Pain> AllElements { get; private set; }

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
                    SuggestedValues = PainList.Select(x => x.BodyPart).Distinct().Where(x => x.ToLower().Contains(_filter.ToLower())).ToList();
                }
            }
        }

        public ObservableCollectionView<Pain> FilteredElements { get; private set; }

        public List<Pain> PainList
        {
            get { return _painList??new List<Pain>(); }
            set { Set(ref _painList, value); }
        }

        public Pain SelectedPain
        {
            get { return _selectedPain; }
            set { Set(ref _selectedPain, value); }
        }

        public List<string> SuggestedValues
        {
            get { return _suggestedValues; }
            set { Set(ref _suggestedValues, value); }
        }

        private async void LoadList()
        {
            _repo = new PainRepository();
            PainList = await _repo.GetAll();
            if (PainList==null || !PainList.Any())
                MockPainList();

            AllElements.Initialize(PainList);
        }

        private async void MockPainList()
        {
            string[] parts = {"Head", "Body", "Leg", "Stomach"};
            Random rnd = new Random();
            for (int i = 0; i < 500; i++)
            {
                Pain pain = new Pain
                {
                    BodyPart = parts[rnd.Next(parts.Length)],
                    PainType = PainType.Pulsing
                };

               // _painList.Add(pain);
               await _repo.AddNew(pain);
            }
        }
    }
}