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
            string header1 = "Type", header2 = "Color", header3 = "Qty";
            string headerLine1 = "----", headerLine2 = "-----", headerLine3 = "---";

            //once the button is clicked - show the file dialog
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Read in the text file that was selected 
                // Read in the text file that was selected
                txtFile = this.openFileDialog1.FileName;

                // Get the path of the file plus the filename
                dirLocation = Path.GetFullPath(openFileDialog1.FileName);

                // Show the selected file and path in the label
                lblSelectedFile.Text = txtFile;

                // Make sure to make this label visible
                lblSelectedFile.Visible = true;

                // Read all the lines into a one dimensional string array
                string[] lines = File.ReadAllLines(txtFile);

                // Populate a label with the array
                // Make sure the label is cleared out before we start
                lblResults.Text = "";

                // Add in the header
                lblResults.Text = string.Format("{0}{1}{2}\n", header1.PadRight(PadSpace), header2.PadRight(PadSpace), header3.PadRight(PadSpace));
                lblResults.Text += string.Format("{0}{1}{2}\n", headerLine1.PadRight(PadSpace), headerLine2.PadRight(PadSpace), headerLine3.PadRight(PadSpace));

                foreach (string line in lines)
                {
                    // Split each line into an array of elements
                    string[] inventoryList = line.Split(", ");

                    // Iterate through each element in the array
                    // using a for loop instead of foreach loop
                    for (int i = 0; i < inventoryList.Length; i++)
                    {
                        // Display each element using proper spacing
                        lblResults.Text += inventoryList[i].PadRight(PadSpace);
                    }

                    // Need a new line after each iteration to show next line
                    lblResults.Text += "\n";
                }

                // Make sure the label is visible
                lblResults.Visible = true;
            }
        }

        private void lblResults_Click(object sender, EventArgs e)
        {

        }
    }
}
