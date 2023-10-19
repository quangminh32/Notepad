using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Notepad_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Short_Cut);
        }
        private string currentFile = null;

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                currentFile = openFileDialog1.FileName;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFile == null)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                File.WriteAllText(currentFile, richTextBox1.Text);
            }
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".txt";
            saveFileDialog1.Filter = "Text File|*.txt|PDF file|*.pdf|Word File|*.doc";
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                currentFile = saveFileDialog1.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = System.DateTime.Now.ToString();
        }

        private void wordWarpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWarpToolStripMenuItem.Checked)
            {
                richTextBox1.WordWrap = true;
            }
            else
            {
                richTextBox1.WordWrap = false;
            }

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked)
            {
                statusStrip1.Visible = true;
            }
            else
            {
                statusStrip1.Visible = false;
            }
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                var label = new Label() { Text = "Find what:" };
                var textBox = new System.Windows.Forms.TextBox() { Left = label.Right, Width = 200 };
                var findNextButton = new System.Windows.Forms.Button() { Text = "Find Next" };
                findNextButton.Click += (sender, e) => FindNext(textBox.Text);
                var previousButton = new System.Windows.Forms.Button() { Text = "Find Previous", Left = findNextButton.Right };
                previousButton.Click += (sender, e) => FindPrevious(textBox.Text);
                findNextButton.SetBounds(10, 100, 100, 30);
                previousButton.SetBounds(150, 100, 100, 30);
                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(findNextButton);
                form.Controls.Add(previousButton);
                form.ShowDialog();
            }
        }
        private int currentSearchIndex = 0;

        private void FindNext(string searchText)
        {
            int startIndex = richTextBox1.SelectionStart + richTextBox1.SelectionLength;
            int index = richTextBox1.Text.IndexOf(searchText, startIndex);

            if (index != -1)
            {
                richTextBox1.Select(index, searchText.Length);
                richTextBox1.ScrollToCaret();
                currentSearchIndex = index + searchText.Length;
            }
            else
            {

                index = richTextBox1.Text.IndexOf(searchText, 0);
                if (index != -1)
                {
                    richTextBox1.Select(index, searchText.Length);
                    richTextBox1.ScrollToCaret();
                    currentSearchIndex = index + searchText.Length;
                }
                else
                {
                    MessageBox.Show("None.");
                }
            }
        }

        private void FindPrevious(string searchText)
        {
            int startIndex = richTextBox1.SelectionStart - 1;
            if (startIndex < 0) startIndex = richTextBox1.Text.Length;
            int index = richTextBox1.Text.LastIndexOf(searchText, startIndex);

            if (index != -1)
            {
                richTextBox1.Select(index, searchText.Length);
                richTextBox1.ScrollToCaret();
                currentSearchIndex = index;
            }
            else
            {

                index = richTextBox1.Text.LastIndexOf(searchText);
                if (index != -1)
                {
                    richTextBox1.Select(index, searchText.Length);
                    richTextBox1.ScrollToCaret();
                    currentSearchIndex = index;
                }
                else
                {
                    MessageBox.Show("None.");
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Short_Cut(object sender, KeyEventArgs e)
        {

        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float zoom = richTextBox1.ZoomFactor;
            if (zoom * 2 < 64)
                richTextBox1.ZoomFactor = zoom * 2;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float zoom = richTextBox1.ZoomFactor;
            if (zoom / 2 > 0.015625)
                richTextBox1.ZoomFactor = zoom / 2;
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor = 1.0f;
        }
    }
}