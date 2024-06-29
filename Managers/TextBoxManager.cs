namespace AutoRestarter.Managers;

public class TextBoxManager
{
    public TextBoxManager()
    {

    }

    public void SubscribeEvents(List<TextBox> textBoxes)
    {
        RestrictInputToNumbers(textBoxes);
    }

    private void RestrictInputToNumbers(List<TextBox> textBoxes)
    {
        for (int i = 0; i < textBoxes.Count; i++)
        {
            textBoxes[i].KeyPress += NumericTextBox_KeyPress;
        }
    }

    public void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }
}
