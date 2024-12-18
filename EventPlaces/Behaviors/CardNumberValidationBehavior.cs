namespace EventPlaces.Behaviors
{
    public class CardNumberValidationBehavior : Behavior<Entry>
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

            // Eliminar espacios
            string text = e.NewTextValue.Replace(" ", string.Empty);

            if (text.Length > 16)
                text = text.Substring(0, 16);

            // Aplicar máscara (xxxx xxxx xxxx xxxx)
            string formattedText = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    formattedText += " ";
                }
                formattedText += text[i];
            }

            entry.Text = formattedText;
        }
    }
}
