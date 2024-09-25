using DVLD.Classes;
using DVLD_Bisnesess;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        static string OriginalPassword { get; set; }
        static string SubKey {  get; set; }

        static string Password;
        static string UserName;
        public frmLogin()
        {
            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ResetPassword()
        {
            OriginalPassword = "";
            txtPassword.Text = "";
            Password = "";
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren() || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtUserName.Text)) 
            {
                //Here we don't continue because the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the Error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            // test is Password true when Enter from Text Box 
            Password = txtPassword.Text == SubKey ? OriginalPassword : Password;
            
            // check is the current user in remembered because the password is hashing in the windows registry 
            Password = Password.Length != 64 ? clsGlobal.ComputeHash(txtPassword.Text) : Password;
            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), Password);           

            if (user != null) 
            {
                if (chkRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), Password);

                }
                else
                {
                    //store empty username and password
                  //  clsGlobal.RememberUsernameAndPassword("", "");

                }

                //incase the user is not active
                if (!user.IsActive )
                {

                    txtUserName.Focus();
                    MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                 clsGlobal.CurrentUser = user;
                 this.Hide();
                 frmMain frm = new frmMain(this);
                 frm.ShowDialog();


            } else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Reset Password 
            ResetPassword();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                OriginalPassword = Password;
                txtUserName.Text = UserName;
                txtPassword.Text = Password.Substring(0, 10);
                SubKey = txtPassword.Text; 
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

        }

        private void ValidateEmptyTextBox(object sender,CancelEventArgs e)
        {
            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            
            TextBox temp = (TextBox)sender ;
            if (string.IsNullOrEmpty(temp.Text.Trim()) || temp.Text == null) 
            {
               // e.Cancel = true;
                errorProvider1.SetError(temp, "This failed is required");
            }
            else
                errorProvider1.SetError(temp, "");

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            Password = txtPassword.Text;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
