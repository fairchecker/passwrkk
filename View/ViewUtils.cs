using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace passwrkk.View
{
    public static class ViewUtils
    {
        public static void HideParentElement(object sender, object panel)
        {
            if (sender is DependencyObject element)
            {
                var parent = VisualTreeHelper.GetParent(element);
                ((Panel)panel).Children.Remove((UIElement)parent);
            }
        }

        public static T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild)
                {
                    return typedChild;
                }

                var result = FindChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}
