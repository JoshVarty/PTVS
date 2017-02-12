using System;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Options {
    abstract class OptionInfo {
        public readonly string Text;
        public readonly string ToolTip;
        public readonly string Key;

        public OptionInfo(string text, string toolTip, string key) {
            Text = text;
            ToolTip = toolTip;
            Key = key;
        }

        public abstract OptionSettingNode CreateNode();
        public abstract string SerializeOptionValue(object value);
        public abstract object DeserializeOptionValue(string value);
        public abstract string GetPreviewText(object optionValue);
        public abstract object DefaultValue {
            get;
        }
    }

    class BooleanOptionInfo : OptionInfo {
        public readonly string PreviewOn;
        public readonly string PreviewOff;
        public readonly bool Default;

        public BooleanOptionInfo(string text, string key, string toolTip, string previewOn, string previewOff, bool defaultValue) :
            base(text, toolTip, key) {
            PreviewOn = previewOn;
            PreviewOff = previewOff;
            Default = defaultValue;
        }

        public override OptionSettingNode CreateNode() {
            return new BooleanCheckBoxNode(Text);
        }

        public override string SerializeOptionValue(object value) {
            return value.ToString();
        }

        public override object DeserializeOptionValue(string value) {
            bool b;
            if (Boolean.TryParse(value, out b)) {
                return b;
            }
            return Default;
        }

        public override string GetPreviewText(object optionValue) {
            return ((bool)optionValue) ? PreviewOn : PreviewOff;
        }

        public override object DefaultValue {
            get {
                return Default;
            }
        }
    }

    class TriStateOptionInfo : OptionInfo {
        public readonly string PreviewOn;
        public readonly string PreviewOff;
        public readonly bool? Default;

        public TriStateOptionInfo(string text, string key, string toolTip, string previewOn, string previewOff, bool? defaultValue) :
            base(text, toolTip, key) {
            PreviewOn = previewOn;
            PreviewOff = previewOff;
            Default = defaultValue;
        }

        public override OptionSettingNode CreateNode() {
            return new TriStateCheckBoxNode(Text);
        }

        public override string SerializeOptionValue(object value) {
            if (value == null) {
                return "-";
            }

            return ((bool?)value).ToString();
        }

        public override object DeserializeOptionValue(string value) {
            bool b;
            if (value == "-") {
                return null;
            } else if (Boolean.TryParse(value, out b)) {
                return b;
            }
            return Default;
        }

        public override string GetPreviewText(object optionValue) {
            if (optionValue == null) {
                return Strings.FormattingOptionPreviewNotAltered.FormatUI(PreviewOn, PreviewOff);
            } else {
                return ((bool)optionValue) ? PreviewOn : PreviewOff;
            }
        }

        public override object DefaultValue {
            get {
                return Default;
            }
        }
    }

    class IntegerOptionInfo : OptionInfo {
        public readonly string PreviewText;
        public readonly int Default;

        public IntegerOptionInfo(string text, string key, string toolTip, string preview, int defaultValue) :
            base(text, toolTip, key) {
            PreviewText = preview;
            Default = defaultValue;
        }

        public override OptionSettingNode CreateNode() {
            return new IntegerNode(Text);
        }

        public override string SerializeOptionValue(object value) {
            return value.ToString();
        }

        public override object DeserializeOptionValue(string value) {
            int i;
            if (Int32.TryParse(value, out i)) {
                return i;
            }
            return Default;
        }

        public override string GetPreviewText(object optionValue) {
            return PreviewText;
        }

        public override object DefaultValue {
            get { return Default; }
        }
    }
}
