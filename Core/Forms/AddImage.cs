
using AutoRestarter.Managers;

namespace AutoRestarter.Core.Forms;

public partial class AddImage : Form
{
    private readonly TextBoxManager textBoxManager;
    public string? ImageName { get; private set; }
    public double? Threshold { get; private set; }
    public int? X { get; private set; }
    public int? Y { get; private set; }
    public new int? Width { get; private set; }
    public new int? Height { get; private set; }
    public bool IncludeSelected => IncludeCheckBox.Checked;
    public bool ExcludeSelected => ExcludeCheckBox.Checked;

    public List<TextBox> TextBoxes { get; private set; }

    public AddImage()
    {
        InitializeComponent();

        textBoxManager = new();
        TextBoxes = [XBox, YBox, WidthBox, HeightBox];

        textBoxManager.SubscribeEvents(TextBoxes);
    }

    private void AddImage_Load(object sender, EventArgs e)
    {

    }

    private void ImageSelectButton_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
        openFileDialog.Title = "Select an Image";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string selectedFilePath = openFileDialog.FileName;
            PlaceHolderLabel.Text = Path.GetFileName(selectedFilePath);
            ImageName = Path.GetFileName(selectedFilePath);

            // Load image into PictureBox
            PictureBox.Image = Image.FromFile(selectedFilePath);

            // Update Width and Height properties
            using (var image = new Bitmap(selectedFilePath))
            {
                Width = image.Width;
                Height = image.Height;
            }

            // Update TextBox values if needed
            WidthBox.Text = Width.ToString();
            HeightBox.Text = Height.ToString();
        }
    }


    private void SubmitButton_Click_1(object sender, EventArgs e)
    {
        Threshold = double.Parse(ThresholdTextBox.Text);
        X = int.Parse(XBox.Text);
        Y = int.Parse(YBox.Text);
        Width = int.Parse(WidthBox.Text);
        Height = int.Parse(HeightBox.Text);



        DialogResult = DialogResult.OK;
        Close();
    }
}



