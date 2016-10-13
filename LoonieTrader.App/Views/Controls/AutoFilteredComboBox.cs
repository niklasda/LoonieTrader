using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace LoonieTrader.App.Views.Controls
{
    public class AutoFilteredComboBox : ComboBox
    {
        private bool _ignoreTextChanged;
        private string _currentText;

        /// <summary>
        /// The <see cref="DependencyProperty"/> object of the <see cref="IsCaseSensitive" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsCaseSensitiveProperty =
            DependencyProperty.Register("IsCaseSensitive", typeof(bool), typeof(AutoFilteredComboBox), new UIPropertyMetadata(false));

        /// <summary>
        /// Gets or sets the way the combo box treats the case sensitivity of typed text.
        /// </summary>
        /// <value>The way the combo box treats the case sensitivity of typed text.</value>
        [Description("The way the combo box treats the case sensitivity of typed text.")]
        [Category("AutoFiltered ComboBox")]
        [DefaultValue(true)]
        public bool IsCaseSensitive
        {
            [DebuggerStepThrough] get { return (bool)base.GetValue(IsCaseSensitiveProperty); }
            [DebuggerStepThrough] set { base.SetValue(IsCaseSensitiveProperty, value); }
        }

        /// <summary>
        /// The <see cref="DependencyProperty"/> object of the <see cref="DropDownOnFocus" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DropDownOnFocusProperty =
            DependencyProperty.Register("DropDownOnFocus", typeof(bool), typeof(AutoFilteredComboBox), new UIPropertyMetadata(false));

        /// <summary>
        /// Gets or sets the way the combo box behaves when it receives focus.
        /// </summary>
        /// <value>The way the combo box behaves when it receives focus.</value>
        [Description("The way the combo box behaves when it receives focus.")]
        [Category("AutoFiltered ComboBox")]
        [DefaultValue(false)]
        public bool DropDownOnFocus
        {
            [DebuggerStepThrough] get { return (bool)base.GetValue(DropDownOnFocusProperty); }
            [DebuggerStepThrough] set { base.SetValue(DropDownOnFocusProperty, value); }
        }

        /// <summary>
        /// Invoked whenever an unhandled <see cref="UIElement.GotFocus" /> event
        /// reaches this element in its route.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (base.ItemsSource != null && DropDownOnFocus)
            {
                base.IsDropDownOpen = true;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(OnTextChanged));
            KeyUp += AutoFilteredComboBox_KeyUp;
            base.IsTextSearchEnabled = false;
        }

        void AutoFilteredComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (base.IsDropDownOpen)
                {
                    // Ensure that focus is given to the dropdown list
                    if (Keyboard.FocusedElement is TextBox)
                    {
                        Keyboard.Focus(this);
                        if (base.Items.Count > 0)
                        {
                            if (base.SelectedIndex == -1 || base.SelectedIndex == 0)
                                base.SelectedIndex = 0;
                        }
                    }
                }
            }

            if (Keyboard.FocusedElement is TextBox)
            {
                if (e.OriginalSource is TextBox)
                {
                    // Avoid the automatic selection of the first letter (As next letter will cause overwrite)
                    TextBox textBox = e.OriginalSource as TextBox;
                    if (textBox.Text.Length == 1 && textBox.SelectionLength == 1)
                    {
                        textBox.SelectionLength = 0;
                        textBox.SelectionStart = 1;
                    }
                }
            }
        }

        private void RefreshFilter()
        {
            if (base.ItemsSource != null)
            {
                //Action<string> filterList = FilterList;
                //if (filterList != null)
                //{
                //    filterList(_currentText);
                //}
                //else
                //{
                    ICollectionView view = CollectionViewSource.GetDefaultView(base.ItemsSource);
                    view.Refresh();
                //}
                base.SelectedIndex = -1; // Prepare so arrow down selects first
                base.IsDropDownOpen = true;
            }
        }

        private bool FilterPredicate(object value)
        {
            // We don't like nulls.
            if (value == null)
                return false;

            // If there is no text, there's no reason to filter.
            if (string.IsNullOrEmpty(_currentText))
                return true;

           // Func<object, string, bool> filterItem = FilterItem;
           // if (filterItem != null)
           //     return filterItem(value, _currentText);

            if (IsCaseSensitive)
                return value.ToString().Contains(_currentText);
            else
                return value.ToString().ToUpper().Contains(_currentText.ToUpper());
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            try
            {
                _ignoreTextChanged = true; // Ignore the following TextChanged
                base.OnSelectionChanged(e);
            }
            finally
            {
                _ignoreTextChanged = false;
            }
        }

        /// <summary>
        /// Called when the source of an item in a selector changes.
        /// </summary>
        /// <param name="oldValue">Old value of the source.</param>
        /// <param name="newValue">New value of the source.</param>
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(newValue);
               // if (FilterList == null)
                    view.Filter += FilterPredicate;
            }

            if (oldValue != null)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(oldValue);
                view.Filter -= FilterPredicate;
            }
            base.OnItemsSourceChanged(oldValue, newValue);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_ignoreTextChanged)
                return;

            _currentText = Text;

            if (!base.IsTextSearchEnabled)
            {
                RefreshFilter();
            }
        }
    }
}