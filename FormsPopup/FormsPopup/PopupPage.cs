using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace FormsPopup
{
    /// <summary>
    /// One of two methods must be taken to utilize the <see cref="Popup"/> view.
    /// 1) The visible page must inherit from <see cref="PopupPage"/>
    /// 2) The visible page must instantiate an object of <see cref="PopupPageInitializer"/> in page's constructor
    /// </summary>
	public abstract class PopupPage : ContentPage
	{
	    private readonly PopupPageInitializer _popupInit;
	    public ObservableCollection<Popup> Popups { get; protected set; }


	    protected PopupPage ()
		{
            _popupInit = new PopupPageInitializer(this);
            Popups = new ObservableCollection<Popup>();
            Popups.CollectionChanged += Popups_CollectionChanged;
		}


        private void Popups_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Popup item in e.NewItems)
                {
                    _popupInit.Add(item);
                }
            }
        }
	}
}