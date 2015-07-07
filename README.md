# Xamarin.Forms Popup View

This repository houses an example of using the Xamarin.Forms API to create a popup view.
I chose not to use any native APIs while implementing the view, as I wished to use this as a learning experience. That said, it's still a fairly *featureful* implementation.

**Projects**

* FormsPopup (The `Popup` implementation)
* FormsPopup.Examples
* FormsPopup.Droid
* FormsPopup.iOS

## Initializing

The current implementation requires either one of two conditions be met before you can use a popup view within a `Page`:

1. The visible page must extend the `PopupPage` type.
2. The visible page must instantiate a `PopupPageInitializer` before any children have been added to the page:

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

The following events are invoked during various moments in a popup's life cycle. Their purpose should be self-evident.

* Initializing (happens once, during the hosting page's `Appearing` event)
* Tapped
* Showing
* Shown
* Hiding
* Hidden

The `Tapped`, `Showing`, and `Hiding` events can all be cancelled by using the event argument properties:

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

The `Popup.ShowAsync()` and `Popup.HideAsync()` methods can be used to add animations to either event. If you do not wish to include animations, you can use either `Popup.Show()` or `Popup.Hide()`.

**Example**

```csharp
await popup1.ShowAsync(async p =>
{
	await p.RelScaleTo(0.05, 90, Easing.CubicOut);
	await p.RelScaleTo(-0.05, 80, Easing.CubicOut);
});
```

## Styling

At this moment, there are no default styles for the popup. Your views will inherit whatever styles you have attached to your resource dictionaries.

## Miscellaneous Features

* The left, top, right, and bottom border colors can be individually set
* During the `Tapped` event, you can determine if the user tapped within the header, body, or footer sections.

## Screenshots
![alt text](https://github.com/michaeled/FormsPopup/blob/master/pictures/droid.png "Android")
