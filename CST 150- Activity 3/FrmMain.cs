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
        //Declare and initialize
        //Class level variable scope
        string[] lines;
        string txtFile = "";

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

            //Make sure the comboBox is not visible
            cmbSelectRow.Visible = false;
            lblSelectRow.Visible = false;
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
            //Use this int to dynamically populate the comboBox
            int numberRows = 1; 

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
                lines = File.ReadAllLines(txtFile);

                // Populate a label with the array
                // Make sure the label is cleared out before we start
                lblResults.Text = "";


                //Display Header
                DisplayHeader();

                cmbSelectRow.Items.Clear();
                // Add in the header
                //lblResults.Text = string.Format("{0}{1}{2}\n", header1.PadRight(PadSpace), header2.PadRight(PadSpace), header3.PadRight(PadSpace));
                //lblResults.Text += string.Format("{0}{1}{2}\n", headerLine1.PadRight(PadSpace), headerLine2.PadRight(PadSpace), headerLine3.PadRight(PadSpace));

                foreach (string line in lines)
                {
                    //Dynamically populate the comboBox
                    cmbSelectRow.Items.Add(numberRows);
                    //Inc to next row
                    numberRows++;
                    // Split each line into an array of elements
                    string[] inventoryList = line.Split(", ");

                    // Iterate through each element in the array
                    // using a for loop instead of foreach loop
                    for (int i = 0; i < inventoryList.Length; i++)
                    {
                        // Display each element using proper spacing
                        ConvertLowerCase(inventoryList[i]);
                    }

                    // Need a new line after each iteration to show next line
                    lblResults.Text += "\n";
                }

                // Make sure the label is visible
                lblResults.Visible = true;
                cmbSelectRow.Visible = true;
                lblSelectRow.Visible = true;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
        //First Method
        //---------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Convert input string to all lower case characters
        /// Then send the results to be displayed
        /// </summary>
        /// <param name="textToConvert"></param>
        private void ConvertLowerCase(string textToConvert)
        {
            ResultsToLabel(textToConvert.ToLower());
        }
        /// <summary>
        /// Print resluts to label
        /// </summary>
        /// <param name="Results"></param>
        private void ResultsToLabel(string results)
        {
            //Declare and Initialize Constant
            const int PadSpace = 20;

            // Display each element using proper spacing
            lblResults.Text += results.PadRight(PadSpace);
        }

        private void lblResults_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// When the comboBox drop down closes triggfer this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectRowToInc(object sender, EventArgs e)
        {
            // Declare and Initialize
            int rowSelected = -1;
            int qtyValue = -1;

            // Get the selected index of the comboBox
            // -1 means no value was selected
            rowSelected = cmbSelectRow.SelectedIndex;

            //Only inc a qty if a row was selected
            if (rowSelected >= 0)
            {
                //get the qty from the row selected
                qtyValue = GetQty(lines, rowSelected);

                //Now we can inc the qty and store it back to the file
                IncDisplayQty(lines, rowSelected, qtyValue, txtFile);
            }
        }
        /// <summary>
        /// Get the Qty value from selected Row
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="selectedRow"></param>
        /// <returns></returns>
        private int GetQty(string[] lines, int selectedRow)
        {
            // Declare and initialize
            int qty = -1; // This way we know if there was an error

            // Iterate through the array until the selected row is found.
            // Since we know the exact number of times to iterate through the array
            // which loop is the best one to use?
            for (int x = 1; x < lines.Length; x++)
            {
                // Now only pull out the row we need
                if (x == selectedRow)
                {
                    string[] invRow = lines[x].Split(",");

                    // Now pull out the qty.
                    // Use exception handling to parse string to int
                    try
                    {
                        // Convert string representation of a number to its
                        // signed integer.
                        qty = int.Parse(invRow[2].Trim());
                        return qty;
                    }
                    catch (FormatException e)
                    {
                        // Show an exception message
                        lblResults.Text = e.Message;
                    }
                }
            }

            // If there are any exceptions return -1
            return qty;
        }
        /// <summary>
        /// Inc qty value, build the string for file, save to file
        /// </summary>
        /// <param name="Lines"></param>
        /// <param name="invRowToUpdate"></param>
        /// <param name="qty"></param>
        /// <param name="txtFile"></param>
        private void IncDisplayQty(string[] Lines, int invRowToUpdate, int qty, string txtFile)
        {
            // Declare and Initialize
            string updateLine = "";

            // First inc qty
            qty++;

            // Now we need to update the qty in the array
            // First we need to split up the row so we can update the array
            string[] invRow = Lines[invRowToUpdate].Split(',');

            // Then we can update the element in the string array
            invRow[2] = qty.ToString();

            // We need to build the string to store in the Lines array
            updateLine = invRow[0].Trim() + ", " + invRow[1].Trim() + ", " + invRow[2].Trim();
            // Now update the lines array
            Lines[invRowToUpdate] = updateLine;

            // Now update the text file
            File.WriteAllLines(txtFile, Lines);
        }
        /// <summary>
        /// Display the entire inventory
        /// </summary>
        private void DisplayInv()
        {
            // First need to clear out the label
            lblResults.Text = "";

            // Then display the header;
            DisplayHeader();

            // Iterate through the inventory one line at a time
            foreach (string line in lines)
            {
                // Split each line into an array of elements
                string[] inventoryList = line.Split(",");

                // Iterate through each element in the array
                // using a for loop instead of foreach loop
                for (int i = 0; i < inventoryList.Length; i++)
                {
                    // Send to display
                    ResultsToLabel(inventoryList[i]);
                }

                // Need a new line after each iteration to show next line
                lblResults.Text += "\n";
            }
        }
        /// <summary>
        /// Display the inventory header
        /// </summary>
        private void DisplayHeader()
        {
            const int PadSpace = 20;
            string header1 = "Type", header2 = "Color", header3 = "Qty";
            string headerLine1 = "----", headerLine2 = "-----", headerLine3 = "---";

            // Add in the header
            lblResults.Text = string.Format("{0}{1}{2}\n", header1.PadRight(PadSpace), header2.PadRight(PadSpace), header3.PadRight(PadSpace));
            lblResults.Text += string.Format("{0}{1}{2}\n", headerLine1.PadRight(PadSpace), headerLine2.PadRight(PadSpace), headerLine3.PadRight(PadSpace));
        }
    }
}
