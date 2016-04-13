using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegularExpressions
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    control.KeyPress += new KeyPressEventHandler(CheckKeys);
                }
            }
        }

        private void CheckKeys(object sender, KeyPressEventArgs e)

        {
            if (e.KeyChar == (char)13)
            {
                this.btnSubmitForm_Click(sender, null);
                e.Handled = true;
            }
        }

        public void ChangeSize(int width, int height)
        {
            this.Size = new Size(width, height);
        }

        public void menuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void menuFileClear_Click(object sender, EventArgs e)
        {
            lblFomatError.Text = "";
            foreach (Control textBox in this.Controls)
            {
                if (textBox.GetType() == typeof(TextBox))
                {
                    textBox.Text = string.Empty;
                    textBox.BackColor = Color.White;
                }
            }
        }

        public void checkTextBoxFormat(Control textBox)
        {
            var pattern = "";
            switch (textBox.Name)
            {
                case "txtFirstName":
                    pattern = "^[a-zA-Z .'-]{1,25}$";
                    break;
                case "txtLastName":
                    pattern = "^[a-zA-Z .'-]{1,25}$";
                    break;
                case "txtAddress":
                    pattern = @"^\d{1,10}\s[a-zA-Z0-9 .'-]{1,35}$";
                    break;
                case "txtApartment":
                    pattern = "^[a-zA-Z0-9 .-]{1,5}$";
                    break;
                case "txtCity":
                    pattern = "^[a-zA-Z .'-]{1,25}$";
                    break;
                case "txtState":
                    pattern = "^[a-zA-Z]{2}$";
                    break;
                case "txtZipCode":
                    pattern = @"^\d{5}|\d{5}-\d{4}$";
                    break;
                case "txtPhone":
                    pattern = @"\(\d{3}\)\s\d{3}-\d{4}$";
                    break;
                case "txtSSN":
                    pattern = @"^\d{3}-\d{2}-\d{4}$";
                    break;
                default:
                    break;
            }

            if (!(Regex.Match(textBox.Text, pattern)).Success)
            {
                lblFomatError.Text = "Error: 1 or more fields is in the incorrect format.";
                textBox.BackColor = Color.Red;
                textBox.Text = "";
            }
            else
            {
                textBox.BackColor = Color.White;
            }
        }

        public void btnSubmitForm_Click(object sender, EventArgs e)
        {
            ChangeSize(450, 500);
            txtOutput.Text = "";
            lblFomatError.ForeColor = Color.Red;
            txtFirstName.Text = Format.Capitalize(txtFirstName.Text);
            txtLastName.Text = Format.Capitalize(txtLastName.Text);
            txtCity.Text = Format.Capitalize(txtCity.Text);
            txtState.Text = txtState.Text.ToUpper();

            if (txtZipCode.Text != String.Empty)
            {
                if (txtZipCode.Text.Length == 9)
                {
                    txtZipCode.Text = Format.formatZipCode(txtZipCode.Text);
                }
            }

            if (txtPhone.Text != String.Empty)
            {
                txtPhone.Text = Format.formatPhone(txtPhone.Text);
            }

            if (txtSSN.Text != String.Empty)
            {
                txtSSN.Text = Format.formatSSN(txtSSN.Text);
            }

            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    checkTextBoxFormat(control);
                }
            }

            List<string> textBoxWrongFormat = new List<string>(){};
            foreach (Control control in this.Controls)
            {
                if (control.BackColor == Color.Red)
                {
                    textBoxWrongFormat.Add(control.Name);
                }
            }

            textBoxWrongFormat.ForEach(n => txtOutput.Text = (txtOutput.Text + n + Environment.NewLine));

            if (textBoxWrongFormat.Count == 0)
            {
                lblFomatError.Text = "";
                lblFomatError.ForeColor = Color.Green;
                lblFomatError.Text = "Entry captured.";
            }
            textBoxWrongFormat.Clear();
        }
    }
}
