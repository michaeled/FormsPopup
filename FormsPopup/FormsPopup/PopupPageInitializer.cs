using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FormsPopup
{
    /// <summary>
    /// Initialize the <see cref="Popup"/> views that are shown from a <seealso cref="ContentPage"/>
    /// </summary>
    public sealed class PopupPageInitializer : IEnumerable<Popup>
    {
        private readonly List<Popup> _popups = new List<Popup>(); 
        private readonly AbsoluteLayout _absContent = new AbsoluteLayout();

        public ContentPage ParentPage { get; set; }
        public IEnumerable<Popup> Popups { get { return _popups; } }


        /// <summary>
        /// Instantiate <see cref="PopupPageInitializer"/>
        /// </summary>
        /// <param name="parentPage">The page that contains the <see cref="Popup"/> views</param>
        public PopupPageInitializer(ContentPage parentPage)
        {
            if (parentPage == null) throw new ArgumentNullException("parentPage");
            
            ParentPage = parentPage;
            parentPage.ChildAdded += ParentPage_ChildAdded;
            parentPage.Appearing += ParentPage_Appearing;
        }


        /// <summary>
        /// This method must be called before the <seealso cref="ContentPage.Content"/> property is set.
        /// </summary>
        /// <param name="popup">The popup to be initialized</param>
        public void Add(Popup popup)
        {
            if (popup == null) throw new ArgumentNullException("popup");
            _popups.Add(popup);
        }


        public IEnumerator<Popup> GetEnumerator()
        {
            return _popups.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private void ParentPage_ChildAdded(object sender, ElementEventArgs e)
        {
            if (ParentPage.Content == e.Element && e.Element != _absContent)
            {
                var oldContent = ParentPage.Content;
                _absContent.Children.Add(oldContent);

                AbsoluteLayout.SetLayoutFlags(oldContent, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(oldContent, new Rectangle(0, 0, 1, 1));

                ParentPage.Content = _absContent;
            }
        }


        private void ParentPage_Appearing(object sender, EventArgs e)
        {
            foreach (var popup in Popups)
            {
                _absContent.Children.Add(popup, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
                popup.OnInitializing();
            }
        }
    }
}