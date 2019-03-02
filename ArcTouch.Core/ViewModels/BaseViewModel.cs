using ArcTouch.Core.Services;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcTouch.Core.ViewModels
{
    public class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
    {
        
        protected IApis Apis { get; set; }

        protected readonly IMvxNavigationService NavigationService;
        public BaseViewModel()
        {
            NavigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            Apis = Mvx.IoCProvider.Resolve<IApis>();
        }

        private bool _loading = false;
        public bool Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                RaisePropertyChanged(() => Loading);
                RaisePropertyChanged(() => ShowEmptyState);
            }
        }
        
        private bool _showEmptyState = false;
        public bool ShowEmptyState
        {
            get => _showEmptyState && !_loading;
            set
            {
                _showEmptyState = value;
                RaisePropertyChanged(() => ShowEmptyState);
            }
        }

        private string _emptyTitle = "Ooops! No results found :(";
        public string EmptyTitle
        {
            get => _emptyTitle;
            set
            {
                _emptyTitle = value;
                RaisePropertyChanged(() => EmptyTitle);
            }
        }

        private string _emptySuggestion = "Don't worry, just update your search and try again or swipe down to refresh";
        public string EmptySuggestion
        {
            get => _emptySuggestion;
            set
            {
                _emptySuggestion = value;
                RaisePropertyChanged(() => EmptySuggestion);
            }
        }

        public override void Prepare(TParameter parameter)
        {

        }
    }
}
