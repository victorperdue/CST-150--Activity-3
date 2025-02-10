/*
 Victor Perdue
CST-150
Activity 3
2/9/2025
 */
namespace CST_150__Activity_3
{
    public partial class FrmMain : Form
    {
        

        public FrmMain()
        {
            InitializeComponent();

            // Set the properties for the selectFileDialog control
            // Define the initial directory that is shown
            openFileDialog1.InitialDirectory = Application.StartupPath + @"Data";

            // Set the title of open file dialog
            openFileDialog1.Title = "Browse Txt Files";

            // DefaultExt is only used when "All Files" is selected 
            // from the filter box and no extension is specified
            // by the user.
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            lblResults.Visible = false;
            lblSelectedFile.Visible = false;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        /// <summary>
        /// Click Event Handler To Read the File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadFileClickEvent(object sender, EventArgs e)
        {
            //Declare and initialize variables
            string txtFile = "";
            string dirLocation = "";
            const int PadSpace = 20;

            //once the button is clicked - show the file dialog
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Read in the text file that was selected 
            }
        }
    }
}
