namespace EventPlaces.Behaviors
{
    public class CardDateValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            string text = e.NewTextValue;

            if (text.Length > 5)
                text = text.Substring(0, 5);

            if (text.Length == 2 && !text.Contains("/"))
                text += "/";

            entry.Text = text;
        }
    }
}
