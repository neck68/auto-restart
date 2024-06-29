using AutoRestarter.Managers;
using AutoRestarter.Util;

namespace AutoRestarter.Core.Forms
{
    public partial class ImageEditor : Form
    {


        private ColumnHeader? imageColumn;
        private ColumnHeader? thresholdColumn;
        private ColumnHeader? xColumn;
        private ColumnHeader? yColumn;
        private ColumnHeader? widthColumn;
        private ColumnHeader? heightColumn;
        private ColumnHeader? groupColumn;
        private TextBox? editBox;
        private int editSubItemIndex;

        public ImageEditor()
        {
            InitializeComponent();

        }

        private void Image_Load(object sender, EventArgs e)
        {
            imageColumn = new ColumnHeader();
            thresholdColumn = new ColumnHeader();
            xColumn = new ColumnHeader();
            yColumn = new ColumnHeader();
            widthColumn = new ColumnHeader();
            heightColumn = new ColumnHeader();
            groupColumn = new ColumnHeader();

            ImageListView.Columns.AddRange([
            imageColumn,
            thresholdColumn,
            xColumn,
            yColumn,
            widthColumn,
            heightColumn,
            groupColumn]);

            ImageListView.FullRowSelect = true;
            ImageListView.GridLines = false;
            ImageListView.HideSelection = false;
            ImageListView.Location = new Point(12, 12);
            ImageListView.MultiSelect = true;
            ImageListView.UseCompatibleStateImageBehavior = false;
            ImageListView.View = View.Details;
            ImageListView.LabelEdit = true;
            ImageListView.AllowDrop = true;
            ImageListView.ShowGroups = false;
            imageColumn.Text = "Image Name";
            imageColumn.Width = 150;

            thresholdColumn.Text = "Threshold";
            thresholdColumn.Width = 75;

            xColumn.Text = "X";
            xColumn.Width = 50;

            yColumn.Text = "Y";
            yColumn.Width = 50;

            widthColumn.Text = "Width";
            widthColumn.Width = 75;

            heightColumn.Text = "Height";
            heightColumn.Width = 75;

            groupColumn.Text = "Group";
            groupColumn.Width = 75;

            SettingManager.LoadImageData(ImageListView);

            ImageListView.DoubleClick += ImageListView_DoubleClick;
            ImageListView.ItemDrag += new ItemDragEventHandler(AccountBox_ItemDrag);
            ImageListView.DragEnter += new DragEventHandler(AccountBox_DragEnter);
            ImageListView.DragOver += new DragEventHandler(AccountBox_DragOver);
            ImageListView.DragDrop += new DragEventHandler(AccountBox_DragDrop);
        }

        private void AddListViewItem(string imageName, string threshold, string x, string y, string width, string height, CheckType checkType)
        {
            string groupType = "";
            if (checkType == CheckType.Include)
            {
                groupType = "Include";
            }

            if (checkType == CheckType.Exclude)
            {
                groupType = "Exclude";
            }

            if (checkType == CheckType.Both)
            {
                groupType = "Both";
            }

            ListViewItem item = new(new string[]
            {
                imageName,
                threshold,
                x,
                y,
                width,
                height,
                groupType,
            })
            {
            };
            ImageListView.Items.Add(item);
        }
        private void AddImage_Click(object sender, EventArgs e)
        {
            using AddImage addImageForm = new();
            if (addImageForm.ShowDialog() == DialogResult.OK)
            {
                string imageName = addImageForm.ImageName ?? "nil";
                double? threshold = addImageForm.Threshold ?? 0;
                int? x = addImageForm.X ?? 0;
                int? y = addImageForm.Y ?? 0;
                int? width = addImageForm.Width ?? 380;
                int? height = addImageForm.Height ?? 380;

                CheckType checkType;

                if (addImageForm.IncludeSelected && addImageForm.ExcludeSelected)
                {
                    checkType = CheckType.Both;
                    AddToGroup("Both", imageName, threshold, x, y, width.ToString(), height.ToString(), checkType);
                }
                else if (addImageForm.IncludeSelected)
                {
                    checkType = CheckType.Include;
                    AddToGroup("Include", imageName, threshold, x, y, width.ToString(), height.ToString(), checkType);
                }
                else if (addImageForm.ExcludeSelected)
                {
                    checkType = CheckType.Exclude;
                    AddToGroup("Exclude", imageName, threshold, x, y, width.ToString(), height.ToString(), checkType);
                }

                Logger.SendMessage("" + ImageManager.GetReferenceImages().Count);
                SettingManager.SaveImageData(ImageListView);
            }
        }
        private void AddToGroup(string groupName, string imageName, double? threshold, int? x, int? y, string width, string height, CheckType checkType)
        {
            ListViewGroup? group = null;

            foreach (ListViewGroup existingGroup in ImageListView.Groups)
            {
                if (existingGroup.Header == groupName)
                {
                    group = existingGroup;
                    break;
                }
            }

            if (group == null)
            {
                group = new ListViewGroup(groupName, HorizontalAlignment.Left);
                ImageListView.Groups.Add(group);
            }

            AddListViewItem(imageName, threshold.ToString(), x.ToString(), y.ToString(), width, height, checkType);

            ImageManager.AddReferenceImage(imageName, threshold, new Rectangle(x.Value, y.Value, int.Parse(width), int.Parse(height)), checkType);
        }
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (ImageListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem selectedItem in ImageListView.SelectedItems)
                {
                    ImageListView.Items.Remove(selectedItem);
                }

                SettingManager.SaveImageData(ImageListView);
            }
        }

        private void ImageListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ImageListView_DoubleClick(object? sender, EventArgs e)
        {
            if (ImageListView.SelectedItems.Count == 1)
            {
                var selectedItem = ImageListView.SelectedItems[0];
                var mousePosition = MousePosition;

                int subItemIndex = GetSubItemIndex(mousePosition);

                // Prevent editing if the group column is selected
                if (subItemIndex != -1 && subItemIndex != groupColumn.Index)
                {
                    editSubItemIndex = subItemIndex;

                    Rectangle subItemBounds = selectedItem.SubItems[subItemIndex].Bounds;

                    int columnWidth = ImageListView.Columns[subItemIndex].Width;

                    Point textBoxLocation = new(
                        ImageListView.Left + subItemBounds.Left,
                        ImageListView.Top + subItemBounds.Top
                    );
                    Size textBoxSize = new(columnWidth, subItemBounds.Height);

                    editBox = new TextBox
                    {
                        Location = textBoxLocation,
                        Size = textBoxSize,
                        Text = selectedItem.SubItems[subItemIndex].Text,
                        TextAlign = HorizontalAlignment.Left
                    };

                    editBox.LostFocus += EditBox_LostFocus;
                    editBox.KeyPress += EditBox_KeyPress;

                    Controls.Add(editBox);
                    editBox.BringToFront();
                    editBox.Focus();
                }
            }
        }
        private int GetSubItemIndex(Point mousePosition)
        {
            Point listViewMousePosition = ImageListView.PointToClient(mousePosition);
            int x = 0;

            for (int i = 0; i < ImageListView.Columns.Count; i++)
            {
                int columnWidth = ImageListView.Columns[i].Width;

                if (listViewMousePosition.X >= x && listViewMousePosition.X < x + columnWidth)
                {
                    return i;
                }

                x += columnWidth;
            }

            return -1;
        }
        private void EditBox_LostFocus(object? sender, EventArgs e)
        {
            SaveChanges();
            SettingManager.SaveImageData(ImageListView);
        }
        private void EditBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SaveChanges();
            }
        }
        private void SaveChanges()
        {
            if (editBox != null)
            {
                var selectedItem = ImageListView.SelectedItems[0];
                if (editSubItemIndex != -1)
                {
                    selectedItem.SubItems[editSubItemIndex].Text = editBox.Text;
                }
                editBox.LostFocus -= EditBox_LostFocus;
                editBox.KeyPress -= EditBox_KeyPress;
                ImageListView.Controls.Remove(editBox);
                editBox.Dispose();
                editBox = null;
            }
        }

        private void AccountBox_ItemDrag(object? sender, ItemDragEventArgs e)
        {
            ImageListView.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void AccountBox_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void AccountBox_DragOver(object? sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.Move;

                Point cp = ImageListView.PointToClient(new Point(e.X, e.Y));
                ListViewItem hoverItem = ImageListView.GetItemAt(cp.X, cp.Y);

                hoverItem?.EnsureVisible();
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void AccountBox_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                Point cp = ImageListView.PointToClient(new Point(e.X, e.Y));
                ListViewItem hoverItem = ImageListView.GetItemAt(cp.X, cp.Y);

                if (hoverItem != null)
                {
                    ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                    int hoverIndex = hoverItem.Index;
                    int dragIndex = draggedItem.Index;

                    if (dragIndex != hoverIndex)
                    {
                        // Remove the item first
                        ImageListView.Items.Remove(draggedItem);

                        // Insert the item at the hovered location
                        ImageListView.Items.Insert(hoverIndex, draggedItem);

                        // Optionally select the moved item
                        draggedItem.Selected = true;

                        SettingManager.SaveAccountData(ImageListView);
                    }
                }
                else
                {
                    // If the drop location is beyond the last item, append the item at the end
                    ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                    ImageListView.Items.Remove(draggedItem);
                    ImageListView.Items.Add(draggedItem);
                }
            }
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Width/Height NEEDS TO BE CONSISTENT. (ex. 380x380 picture, 380x380 emulator");
        }
    }
}   
