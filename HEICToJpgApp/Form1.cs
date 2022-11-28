using ImageMagick;
using System.Drawing;

namespace HEICToJpgApp
{
    public partial class HEICConverter : Form
    {
        public HEICConverter()
        {
            InitializeComponent();
            
            comboBox1.SelectedIndex = 0;
            
        }
        
        IDictionary<int, string> filePaths = new Dictionary<int, string>();

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                filePaths.Clear();
                list1.Items.Clear();
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;
                list1.ScrollAlwaysVisible = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < dlg.FileNames.Length; i++)
                    {
                        filePaths.Add(i, dlg.FileNames[i]);
                        //using (var image = new MagickImage(SampleFiles.SnakewarePng)) ;

                    }

                }
                btnSetFilePath.Enabled = true;
                btnConvert.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex, "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            listImageSource();
        }
      
        private void listImageSource()
        {
          

            try
            {
                if (filePaths.Count != 0)
                {
                    list1.Visible = true;

                    foreach (var filePath in filePaths)
                    {
                        list1.Items.Add(filePath.Value.Trim());
                    }


                }
            }
            catch (Exception ex)
            {

                
            }
        }

        private void convertImages(string type, string path)
        {

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 100 / filePaths.Count;
            string saveFilePath = null;

            try
            {
                if (list1 != null)
                {
                    if(folderBrowserDialog1.SelectedPath != null )
                    {
                        saveFilePath = folderBrowserDialog1.SelectedPath;
                    }

                    foreach (var filePath in filePaths)
                    {
                        using (var image = new MagickImage(filePath.Value.ToString()))
                           

                            image.Write(filePath.Value.ToString(), MagickFormat.Jpeg);
                            progressBar1.PerformStep();
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("error" + ex, "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            convertImages(null, null);
        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnSetFilePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
           
        }
    }
}