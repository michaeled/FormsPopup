using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsPopup
{
	/// <summary>
	/// Popup view to be displayed over a <see cref="ContentPage"/>.
	/// </summary>
	/// <remarks>
	/// No default styles have been created for this view.
	/// </remarks>
	public class Popup : ContentView
	{
		private readonly AbsoluteLayout _popupView = new AbsoluteLayout();
		private readonly StackLayout _sectionContainer = new StackLayout();

		private readonly ContentView _headerSection = new ContentView();
		private readonly ContentView _bodySection = new ContentView();
		private readonly ContentView _footerSection = new ContentView();

		private readonly BoxView _leftBorder = new BoxView();
		private readonly BoxView _rightBorder = new BoxView();
		private readonly BoxView _topBorder = new BoxView();
		private readonly BoxView _bottomBorder = new BoxView();

		private const double BorderWidthFraction = 0.005;
		private const double BorderLengthFraction = 1;


		#region Events


		public event EventHandler<PopupTappedEventArgs> Tapped;
		public event EventHandler<EventArgs> Initializing;
		public event EventHandler<PopupShowingEventArgs> Showing;
		public event EventHandler<EventArgs> Shown;

		public event EventHandler<PopupHidingEventArgs> Hiding;
		public event EventHandler<EventArgs> Hidden;


		protected virtual void OnPropertyTapped(PopupTappedEventArgs e)
		{
			var handler = Tapped;
			if (handler != null) handler(this, e);
		}


		protected internal virtual void OnInitializing()
		{
			var handler = Initializing;
			if (handler != null) handler(this, EventArgs.Empty);
		}


		protected virtual PopupShowingEventArgs OnShowing()
		{
			var args = new PopupShowingEventArgs();
			var handler = Showing;
			if (handler != null) handler(this, args);

			return args;
		}


		protected virtual void OnShown()
		{
			var handler = Shown;
			if (handler != null) handler(this, EventArgs.Empty);
		}


		protected virtual PopupHidingEventArgs OnHiding()
		{
			var args = new PopupHidingEventArgs();
			var handler = Hiding;
			if (handler != null) handler(this, args);

			return args;
		}


		protected virtual void OnHidden()
		{
			var handler = Hidden;
			if (handler != null) handler(this, EventArgs.Empty);
		}


		#endregion


		#region Dependency Properties


		public static readonly BindableProperty HeaderProperty = BindableProperty.Create<Popup, View>(p => p.Header, null, propertyChanged: OnHeaderPropertyChanged);
		public static readonly BindableProperty BodyProperty = BindableProperty.Create<Popup, View>(p => p.Body, null, propertyChanged: OnBodyPropertyChanged);
		public static readonly BindableProperty FooterProperty = BindableProperty.Create<Popup, View>(p => p.Footer, null, propertyChanged: OnFooterPropertyChanged);

		public static readonly BindableProperty XPositionRequestProperty = BindableProperty.Create<Popup, double>(p => p.XPositionRequest, default(double), propertyChanged: OnPositionNeedsUpdating);
		public static readonly BindableProperty YPositionRequestProperty = BindableProperty.Create<Popup, double>(p => p.YPositionRequest, default(double), propertyChanged: OnPositionNeedsUpdating);

		public static readonly BindableProperty LeftBorderColorProperty = BindableProperty.Create<Popup, Color>(p => p.LeftBorderColor, Color.Transparent);
		public static readonly BindableProperty RightBorderColorProperty = BindableProperty.Create<Popup, Color>(p => p.RightBorderColor, Color.Transparent);
		public static readonly BindableProperty TopBorderColorProperty = BindableProperty.Create<Popup, Color>(p => p.TopBorderColor, Color.Transparent);
		public static readonly BindableProperty BottomBorderColorProperty = BindableProperty.Create<Popup, Color>(p => p.BottomBorderColor, Color.Transparent);

		public static readonly BindableProperty ContentWidthRequestProperty = BindableProperty.Create<Popup, double>(p => p.ContentWidthRequest, default(double), propertyChanged: OnPositionNeedsUpdating);
		public static readonly BindableProperty ContentHeightRequestProperty = BindableProperty.Create<Popup, double>(p => p.ContentHeightRequest, default(double), propertyChanged: OnPositionNeedsUpdating);

		internal static readonly BindableProperty SectionTypeProperty = BindableProperty.CreateAttached<Popup, PopupSectionType>(p => GetSectionTypeProperty(p), PopupSectionType.NotSet);


		public View Header
		{
			get { return (View)GetValue(HeaderProperty); }
			set
			{
				SetValue(HeaderProperty, value);
				_headerSection.Content = value;
			}
		}

		public View Body
		{
			get { return (View)GetValue(BodyProperty); }
			set
			{
				SetValue(BodyProperty, value);
				_bodySection.Content = value;
			}
		}


		public View Footer
		{
			get { return (View)GetValue(FooterProperty); }
			set
			{
				SetValue(FooterProperty, value);
				_footerSection.Content = value;
			}
		}


		public double XPositionRequest
		{
			get { return (double)GetValue(XPositionRequestProperty); }
			set { SetValue(XPositionRequestProperty, value); }
		}


		public double YPositionRequest
		{
			get { return (double)GetValue(YPositionRequestProperty); }
			set { SetValue(YPositionRequestProperty, value); }
		}


		public Color LeftBorderColor
		{
			get { return (Color)GetValue(LeftBorderColorProperty); }
			set { SetValue(LeftBorderColorProperty, value); }
		}


		public Color RightBorderColor
		{
			get { return (Color)GetValue(RightBorderColorProperty); }
			set { SetValue(RightBorderColorProperty, value); }
		}


		public Color TopBorderColor
		{
			get { return (Color)GetValue(TopBorderColorProperty); }
			set { SetValue(TopBorderColorProperty, value); }
		}


		public Color BottomBorderColor
		{
			get { return (Color)GetValue(BottomBorderColorProperty); }
			set { SetValue(BottomBorderColorProperty, value); }
		}


		public double ContentWidthRequest
		{
			get { return (double)GetValue(ContentWidthRequestProperty); }
			set { SetValue(ContentWidthRequestProperty, value); }
		}


		public double ContentHeightRequest
		{
			get { return (double)GetValue(ContentHeightRequestProperty); }
			set { SetValue(ContentHeightRequestProperty, value); }
		}


		#endregion


		public Popup()
		{
			IsVisible = false;
			_sectionContainer.BindingContext = this;
			_bodySection.VerticalOptions = LayoutOptions.FillAndExpand;

			Initializing += OnPopupInitializing;

			_sectionContainer.Spacing = 0;
			_sectionContainer.Children.Add(_headerSection);
			_sectionContainer.Children.Add(_bodySection);
			_sectionContainer.Children.Add(_footerSection);

			_headerSection.SetValue(SectionTypeProperty, PopupSectionType.Header);
			_bodySection.SetValue(SectionTypeProperty, PopupSectionType.Body);
			_footerSection.SetValue(SectionTypeProperty, PopupSectionType.Footer);

			// these are added to the absolute layout, so they can be positioned around the parent content
			_leftBorder.SetBinding(BackgroundColorProperty, Binding.Create((Popup p) => p.LeftBorderColor));
			_rightBorder.SetBinding(BackgroundColorProperty, Binding.Create((Popup p) => p.RightBorderColor));
			_bottomBorder.SetBinding(BackgroundColorProperty, Binding.Create((Popup p) => p.BottomBorderColor));
			_topBorder.SetBinding(BackgroundColorProperty, Binding.Create((Popup p) => p.TopBorderColor));

			_sectionContainer.SetBinding(ContentWidthRequestProperty, Binding.Create((Popup p) => p.ContentWidthRequest));
			_sectionContainer.SetBinding(ContentHeightRequestProperty, Binding.Create((Popup p) => p.ContentHeightRequest));
			_sectionContainer.SetBinding(XPositionRequestProperty, Binding.Create((Popup p) => p.XPositionRequest));
			_sectionContainer.SetBinding(YPositionRequestProperty, Binding.Create((Popup p) => p.YPositionRequest));

			_popupView.Padding = 0;
			_popupView.Children.Add(_sectionContainer);
			_popupView.Children.Add(_leftBorder);
			_popupView.Children.Add(_rightBorder);
			_popupView.Children.Add(_topBorder);
			_popupView.Children.Add(_bottomBorder);

			_popupView.SetValue(SectionTypeProperty, PopupSectionType.Backdrop);

			// sizing section container
			AbsoluteLayout.SetLayoutFlags(_sectionContainer, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(_sectionContainer, new Rectangle(0, 0, 1, 1));

			// left border
			AbsoluteLayout.SetLayoutFlags(_leftBorder, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(_leftBorder, new Rectangle(0, 0, BorderWidthFraction, BorderLengthFraction));

			// right border
			AbsoluteLayout.SetLayoutFlags(_rightBorder, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(_rightBorder, new Rectangle(1, 0, BorderWidthFraction, BorderLengthFraction));

			// top border
			AbsoluteLayout.SetLayoutFlags(_topBorder, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(_topBorder, new Rectangle(0, 0, 1, BorderWidthFraction));

			// bottom border
			AbsoluteLayout.SetLayoutFlags(_bottomBorder, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(_bottomBorder, new Rectangle(0, BorderLengthFraction, 1, BorderWidthFraction));

			Content = _popupView;
		}


		/// <summary>
		/// Show the popup view.
		/// </summary>
		/// <param name="animation">The method is passed the popup as the first argument</param>
		/// <remarks>
		/// This method is not limited adding animations.
		/// </remarks>
		public async Task ShowAsync(Func<Popup, Task> animation)
		{
			if (IsVisible)
			{
				return;
			}

			var parent = ParentView.FindParent<Layout>();

			if (parent != null)
			{
				parent.RaiseChild(_popupView);
			}

			var handlerResponse = OnShowing();

			if (handlerResponse.Cancel)
			{
				return;
			}

			IsVisible = true;

			if (animation == null)
			{
				await Task.FromResult(0);
			}
			else
			{
				// passes a reference to this popup object to the consumer
				await animation(this);
			}

			OnShown();
		}


		/// <summary>
		/// Show the popup view.
		/// </summary>
		public void Show()
		{
			ShowAsync(null);
		}


		/// <summary>
		/// Hide the popup view.
		/// </summary>
		/// <param name="animation">The method is passed the popup as the first argument</param>
		/// <remarks>
		/// This method is not limited adding animations.
		/// </remarks>
		public async Task HideAsync(Func<Popup, Task> animation)
		{
			if (!IsVisible)
			{
				return;
			}

			var handlerResponse = OnHiding();

			if (handlerResponse.Cancel)
			{
				return;
			}

			IsVisible = false;

			if (animation == null)
			{
				await Task.FromResult(0);
			}
			else
			{
				// passes a reference to this popup object to the consumer
				await animation(this);
			}

			OnHidden();
		}


		/// <summary>
		/// Hide the popup view.
		/// </summary>
		public void Hide()
		{
			HideAsync(null);
		}


		private void OnPopupInitializing(object sender, EventArgs e)
		{
			Func<GestureRecognizer> factory = delegate
			{
				var closeOnTap = new TapGestureRecognizer();

				var cmd = new Command(obj =>
				{
					var view = obj as View;

					if (view == null)
					{
						return;
					}

					var evt = PopupTappedEventArgs.Create(this, view);

					OnPropertyTapped(evt);
				});

				closeOnTap.Command = cmd;
				return closeOnTap;
			};

			TapGestureRecognizerVisitor.Visit(_popupView, factory);
		}


		private static void OnHeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var popup = (Popup)bindable;
			popup.Header = (View)newValue;
		}


		private static void OnBodyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var popup = (Popup)bindable;
			popup.Body = (View)newValue;
		}


		private static void OnFooterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var popup = (Popup)bindable;
			popup.Footer = (View)newValue;
		}


		private static PopupSectionType GetSectionTypeProperty(BindableObject bindable)
		{
			return (PopupSectionType)bindable.GetValue(SectionTypeProperty);
		}


		private static void OnPositionNeedsUpdating(BindableObject bindable, double oldValue, double newValue)
		{
			var view = (VisualElement)bindable;
			var popup = view.FindParent<Popup>();

			var rect = new Rectangle(popup.XPositionRequest, popup.YPositionRequest, popup.ContentWidthRequest, popup.ContentHeightRequest);
			view.Layout(rect);
			AbsoluteLayout.SetLayoutBounds(view, rect);
		}
	}
}
