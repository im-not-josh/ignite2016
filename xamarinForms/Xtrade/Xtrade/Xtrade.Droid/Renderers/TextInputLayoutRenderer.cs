using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.ComponentModel;
using Android.Support.Design.Widget;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xtrade.Droid.Renderers;
using Resource = Xtrade.Droid.Resource;
using TextChangedEventArgs = Xamarin.Forms.TextChangedEventArgs;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(Entry), typeof(TextInputLayoutRenderer))]
namespace Xtrade.Droid.Renderers
{
    using TextChangedEventArgs = Android.Text.TextChangedEventArgs;

    public class TextInputLayoutRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<Entry, View>
    {
        private TextInputLayout _nativeView;

        private TextInputLayout NativeView
        {
            get { return _nativeView ?? (_nativeView = InitializeNativeView()); }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var ctrl = CreateNativeControl();
                SetNativeControl(ctrl);

                SetText();
                SetHintText();
                SetBackgroundColor();
                SetTextColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
            {
                SetHintText();
            }

            if (e.PropertyName == Entry.TextColorProperty.PropertyName)
            {
                SetTextColor();
            }

            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            {
                SetBackgroundColor();
            }

            if (e.PropertyName == Entry.TextProperty.PropertyName)
            {
                SetText();
            }
        }

        private void SetText()
        {
            NativeView.EditText.Text = Element.Text;
        }

        public void SetBackgroundColor()
        {
            NativeView.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
        }

        private void SetHintText()
        {
            NativeView.Hint = Element.Placeholder;
        }

        private void SetTextColor()
        {
            if (Element.TextColor == Color.Default)
            {
                NativeView.EditText.SetTextColor(NativeView.EditText.TextColors);
            }
            else
            {
                NativeView.EditText.SetTextColor(Element.TextColor.ToAndroid());
            }
        }

        private TextInputLayout InitializeNativeView()
        {
            var view = FindViewById<TextInputLayout>(Resource.Id.textInputLayout);
            view.EditText.TextChanged += EditTextOnTextChanged;
            return view;
        }

        private void EditTextOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            string newText = textChangedEventArgs.Text.ToString();
            
            NativeView.EditText.TextChanged -= EditTextOnTextChanged;
            Element.Text = newText;
            NativeView.EditText.SetSelection(Element.Text.Length);
            NativeView.EditText.TextChanged += EditTextOnTextChanged;
        }

        protected override View CreateNativeControl()
        {
            return LayoutInflater.From(Context).Inflate(Resource.Layout.text_input_layout, null);
        }
    }
}