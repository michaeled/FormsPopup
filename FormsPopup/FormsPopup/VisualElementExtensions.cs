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
		public static T FindParent<T>(this VisualElement visual) where T : VisualElement
		{
			var obj = visual;
			while ((obj as T) == null)
			{
				if (obj.ParentView == null)
				{
					return null;
				}

				obj = obj.ParentView;
			}

			return (T)obj;
		}


		/// <summary>
		/// Search the element hierarchy for a parent that meets the supplied condition.
		/// </summary>
		/// <param name="visual">The starting location of the search</param>
		/// <param name="condition">The condition that is recursively called for each parent view in the hierarchy</param>
		/// <returns></returns>
		public static VisualElement FindParent(this VisualElement visual, Predicate<VisualElement> condition)
		{
			if (visual.ParentView == null)
			{
				return null;
			}

			var current = visual.ParentView;

			while (current != null)
			{
				var result = condition(current);

				if (result)
				{
					return current;
				}

				current = current.ParentView;
			}

			return null;
		}
	}
}
