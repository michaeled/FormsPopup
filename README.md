# Xamarin.Forms Popup View

This repository houses an example of using the Xamarin.Forms API to create a popup view.
I chose not to use any platform APIs (Xamarin.Android or Xamarin.iOS) while implementing the view, as I wished to experiment with the framework. That said, it's still a fairly *featureful* implementation.

**Projects**

* FormsPopup (The `Popup` implementation)
* FormsPopup.Examples
* FormsPopup.Droid
* FormsPopup.iOS

## Initializing

The current implementation requires either one of two conditions be met before you can use a popup view within a `Page`:

1. The visible page must extend from the `PopupPage` class.
2. Any visible page that does not extend from `PopupPage` must instantiate an object of type `PopupPageInitializer` before any children have been added to the page. This is easier than it seems:

**Example**

```csharp
public class CodedSimpleExample : ContentPage
{
	public CodedSimpleExample()
	{
		var popup = new Popup
		{
			XPositionRequest = 0.5,
			YPositionRequest = 0.2,
			ContentHeightRequest = 0.1,
			ContentWidthRequest = 0.4,
			Padding = 10,
	
			Body = new ContentView
			{
				BackgroundColor = Color.White,
				Content = new Label
				{
					XAlign = TextAlignment.Center,
					YAlign = TextAlignment.Center,
					TextColor = Color.Black,
					Text = "Hello, World!"
				}
			}
		};

		// Required for the popup to work
		new PopupPageInitializer(this) {popup};
	
		var button = new Button {Text = "Show Popup"};
		button.Clicked += (s, e) => popup.Show();
		
		Content = new StackLayout
		{
			Children = 
			{
				button
			}
		};
	}
}
```

Two examples have been added to the `FormsPopup.Examples` project to demonstrate this point. Reference either `CodedPopupExample.cs` or `XamlPopupExample.xaml/XamlPopupExample.cs`.

## Sizing and Placement

The current implementation relies heavily on proportional sizes. For any `Popup` properties that require a location or size, you should pass in a value between 0 and 1.

**Example**

```csharp
var popup = new Popup
{
	XPositionRequest = 0.5,
	YPositionRequest = 0.5,
	ContentWidthRequest = 0.8,
	ContentHeightRequest = 0.8
};
```

## Events

The following events are invoked during various moments in a popup's life cycle. They're availble to all `Popup` views.

* Initializing (happens once, during the hosting page's `Appearing` event)
* Tapped
* Showing
* Shown
* Hiding
* Hidden

The `Tapped`, `Showing`, and `Hiding` events can be cancelled by using the event argument property:

**Example**

```csharp
private void Popup1_Showing(object sender, PopupShowingEventArgs e)
{
	if (_preventShowing.On)
	{
		e.Cancel = true;
	}
}
```

## Animations

The `Popup.ShowAsync()` and `Popup.HideAsync()` methods can be used to add animations. If you do not wish to include animations, you can use either `Popup.Show()` or `Popup.Hide()`.

**Example**

```csharp
double original;

await popup.ShowAsync(async p =>
{
	original = p.Scale;

	await Task.WhenAll
	(
		/** 
		 *  Since p is the Popup object, scaling it would also affect the overlay
		 *  behind the popup's body. Although it wouldn't be noticeable in this simple example,
		 *  it would be if the overlay's color was set.
		**/
		
		p.SectionContainer.RelScaleTo(0.05, 100, Easing.CubicOut),
		p.SectionContainer.RelScaleTo(-0.05, 105, Easing.CubicOut)
	).ContinueWith(c =>
	{	// reset popup to original size
		p.SectionContainer.Scale = original;
	});
});
```

## Styling

At this moment, there are no default styles for the popup. Your views will inherit whatever styles you have attached to your resource dictionaries.

## Miscellaneous Features

* The left, top, right, and bottom border colors can be individually set
* During the `Tapped` event, you can determine if the user tapped within the header, body, or footer sections.

## Screenshots
![alt text](https://github.com/michaeled/FormsPopup/blob/master/pictures/androidPopup.gif "Android")
![alt text](https://github.com/michaeled/FormsPopup/blob/master/pictures/iOSPopup.gif "iOS")

## FAQ

**Q** Can I use XAML to create the popups?

**A** Of course!

**Q** Can I add control XX in the popup?

**A** I don't see why not. Let me know if you have any problems.
