using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.VisualStudioTools.Wpf {
    sealed class ConfigurationTextBoxWithHelp : Control {
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(ConfigurationTextBoxWithHelp), new PropertyMetadata());
        public static readonly DependencyProperty HelpTextProperty = DependencyProperty.Register("HelpText", typeof(string), typeof(ConfigurationTextBoxWithHelp), new PropertyMetadata());
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ConfigurationTextBoxWithHelp), new PropertyMetadata());
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ConfigurationTextBoxWithHelp), new PropertyMetadata());
        public static readonly DependencyProperty BrowseButtonStyleProperty = DependencyProperty.Register("BrowseButtonStyle", typeof(Style), typeof(ConfigurationTextBoxWithHelp), new PropertyMetadata());
        public static readonly DependencyProperty BrowseCommandParameterProperty = DependencyProperty.Register("BrowseCommandParameter", typeof(object), typeof(ConfigurationTextBoxWithHelp), new PropertyMetadata());

        public string Watermark {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public string HelpText {
            get { return (string)GetValue(HelpTextProperty); }
            set { SetValue(HelpTextProperty, value); }
        }

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool IsReadOnly {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public Style BrowseButtonStyle {
            get { return (Style)GetValue(BrowseButtonStyleProperty); }
            set { SetValue(BrowseButtonStyleProperty, value); }
        }

        public object BrowseCommandParameter {
            get { return (object)GetValue(BrowseCommandParameterProperty); }
            set { SetValue(BrowseCommandParameterProperty, value); }
        }
    }

    sealed class ConfigurationComboBoxWithHelp : Control {
        public static readonly DependencyProperty HelpTextProperty = DependencyProperty.Register("HelpText", typeof(string), typeof(ConfigurationComboBoxWithHelp), new PropertyMetadata());
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(ConfigurationComboBoxWithHelp), new PropertyMetadata());
        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register("Values", typeof(IList<string>), typeof(ConfigurationComboBoxWithHelp), new PropertyMetadata());

        public string HelpText {
            get { return (string)GetValue(HelpTextProperty); }
            set { SetValue(HelpTextProperty, value); }
        }

        public string Value {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public IList<string> Values {
            get { return (IList<string>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }
    }
}