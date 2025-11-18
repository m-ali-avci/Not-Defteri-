using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace NotDefteriApp
{
    public partial class Form1 : Form
    {
        string currentFilePath = "";
        bool isTextChanged = false;

        public Form1()
        {
            InitializeComponent();
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            string fileName = string.IsNullOrEmpty(currentFilePath) ? "Adsız" : Path.GetFileName(currentFilePath);
            this.Text = fileName + " - Not Defteri";
        }

        private void txtMain_TextChanged(object sender, EventArgs e)
        {
            isTextChanged = true;
        }

        private bool ConfirmSave()
        {
            if (!isTextChanged) return true;

            DialogResult dr = MessageBox.Show(
                "Değişiklikler kaydedilsin mi?",
                "Not Defteri",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            );

            if (dr == DialogResult.Yes)
                return SaveFile();
            if (dr == DialogResult.No)
                return true;

            return false;
        }

        private bool SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
                return SaveFileAs();

            File.WriteAllText(currentFilePath, txtMain.Text);
            isTextChanged = false;
            UpdateTitle();
            return true;
        }

        private bool SaveFileAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Metin Dosyaları (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = sfd.FileName;
                return SaveFile();
            }
            return false;
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ConfirmSave()) return;

            txtMain.Clear();
            currentFilePath = "";
            isTextChanged = false;
            UpdateTitle();
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ConfirmSave()) return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Metin Dosyaları (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = ofd.FileName;
                txtMain.Text = File.ReadAllText(currentFilePath);
                isTextChanged = false;
                UpdateTitle();
            }
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void farklıKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmSave())
                this.Close();
        }

        private void geriAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtMain.CanUndo) txtMain.Undo();
        }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Cut();
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Copy();
        }

        private void yapıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.Paste();
        }

        private void tümünüSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.SelectAll();
        }

        private void saatTarihToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtMain.AppendText(Environment.NewLine + DateTime.Now.ToString());
        }

        private void sözcükKaydırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sözcükKaydırmaToolStripMenuItem.Checked = !sözcükKaydırmaToolStripMenuItem.Checked;
            txtMain.WordWrap = sözcükKaydırmaToolStripMenuItem.Checked;
        }

        private void yazıTipiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = txtMain.Font;
            if (fd.ShowDialog() == DialogResult.OK)
                txtMain.Font = fd.Font;
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Defteri (Nisa + ChatGPT)", "Hakkında");
        }
    }
}
