using System;
using Xamarin.Forms;

namespace FormsPopup
{
	/// <summary>
	/// VisualElement utility methods
	/// </summary>
	public static class VisualElementExtensions
	{
		/// <summary>
		/// Search the element hierarchy for an ancestor of type <typeparam name="T"></typeparam> and return it.
		/// </summary>
		/// <param name="visual">The starting location of the search</param>
		public static T FindParent<T>(this Element visual) where T : Element
		{
			var obj = visual;
			while ((obj as T) == null)
			{
				if (obj.Parent == null)
				{
					return null;
				}

				obj = obj.Parent;
			}

			return (T)obj;
		}


		/// <summary>
		/// Search the element hierarchy for a parent that meets the supplied condition.
		/// </summary>
		/// <param name="visual">The starting location of the search</param>
		/// <param name="condition">The condition that is recursively called for each parent view in the hierarchy</param>
		/// <returns></returns>
		public static Element FindParent(this Element visual, Predicate<Element> condition)
		{
			if (visual.Parent == null)
			{
				return null;
			}

			var current = visual.Parent;

			while (current != null)
			{
				var result = condition(current);

				if (result)
				{
					return current;
				}

				current = current.Parent;
			}

			return null;
		}
	}
}
