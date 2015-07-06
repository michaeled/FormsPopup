# Xamarin.Forms Popup View

![alt text](https://github.com/michaeled/FormsPopup/blob/master/documentation/droid.png "Android")

This repository houses an example of using the Xamarin.Forms API to create a popup view.
I chose not to use any native APIs while implementing the view, as I wished to use this as a learning experience. That said, it's still a fairly *featureful* implementation.

## Initializing

The current implementation requires either one of two conditions to be met before you can use a popup view within a `Page`:

1. The visible page must extend the `PopupPage` type.
2. The visible page must instantiate a `PopupPageInitializer` before any children have been added to the page.

```csharp
public Example()
{
	var layout = new StackLayout { ... }
	var popup = new Popup { .. }
	
	var popupInit = new PopupPageInitializer(this) { popup };
	
	Content = layout;
}
```

Two examples have been added to the repository to illustrate this point. Reference either `CodedPopupExample.cs` or `XamlPopupExample.cs`.

## Sizing and Placement

The current implementation relies heavily on proportional sizes. For any properties that require a location or size, you should pass in a value between 0 and 1.

E.g.

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

### Example

```csharp
private void Popup1_Showing(object sender, PopupShowingEventArgs e)
{
	if (_preventShowing.On)
	{
		e.Cancel = true;
	}
}
```

## Misc Features

* The left, top, right, and bottom border colors can be individually set
* During the `Tapped` event, you can determine if the user tapped within the header, body, or footer sections.
