using System;
using Xamarin.Forms;

namespace FormsPopup
{
	internal static class TapGestureRecognizerVisitor
	{
		/// <summary>
		/// Add a GestureRecognizer to the <paramref name="view"/> and any children it may contain.
		/// </summary>
		/// <param name="view">The view to add the behavior to.</param>
		/// <param name="factoryMethod">A method that returns a GestureRecognizer</param>
		/// <seealso cref="TapGestureRecognizer"/>
		public static void Visit(View view, Func<GestureRecognizer> factoryMethod)
		{
			var behavior = factoryMethod();
			var tapBehavior = behavior as TapGestureRecognizer;

			if (tapBehavior == null)
			{
				throw new InvalidOperationException("The factoryMethod must return a TapGestureRecognizer");
			}

			tapBehavior.CommandParameter = view;
			view.GestureRecognizers.Add(behavior);
			VisitChildren(view as ILayoutController, factoryMethod);
		}


		private static void VisitChildren(ILayoutController controller, Func<GestureRecognizer> factoryMethod)
		{
			if (controller == null || controller.Children.Count == 0)
			{
				return;
			}

			foreach (var child in controller.Children)
			{
				/**
				 * All controls should get a TapGestureRecognizer, in the event they are touched.
				*/

				if (child is ILayoutController)
				{
					VisitChildren(child as ILayoutController, factoryMethod);
				}

				var view = child as View;
				if (view == null)
				{
					return;
				}

				var behavior = factoryMethod();
				var tapBehavior = behavior as TapGestureRecognizer;

				if (tapBehavior != null)
				{
					tapBehavior.CommandParameter = view;
				}

				view.GestureRecognizers.Add(behavior);
			}
		}
	}
}

