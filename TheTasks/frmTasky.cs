using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheTasks
{
    public partial class frmTasky : Form
    {
        public frmTasky()
        {
            InitializeComponent();
        }
        struct stAccountInFo
        {
            public string User;
            public string Password;
            public string Photo;

        }

        stAccountInFo AccountInFo;

        string GetUSer()
        {
            return txtUserNameSignUp.Text;
        }
        string GetPassword()
        {
            return txtPasswordSignUp.Text;
        }

        bool CheckUserIsEmpty()
        {
            if (string.IsNullOrEmpty(txtPasswordSignUp.Text))
            {
                return false;
            }
            return true;
        }
        bool CheckPasswordBigggerThan6()
        {
            if (txtPasswordSignUp.Text.Length > 6)
            {
                return false;
            }
            return true;
        }
        bool Updatesign(stAccountInFo accountInFo)
        {
            accountInFo.User = GetUSer();
            accountInFo.Password = GetPassword();

            if (!CheckUserIsEmpty())
            {
                lblInvalidAdminSignUp.Visible = true;
                txtUserNameSignUp.Focus();
                return false;
            }


            if (!CheckPasswordBigggerThan6())
            {
                lblInvalidPasswordSignUp.Visible = true;
                txtPasswordSignUp.Focus();
                return false;
            }

            lblInvalidAdminSignUp.Visible = false;
            lblInvalidPasswordSignUp.Visible = false;
            return true;
        }
        void UploadPhoto()
        {



            OpenFileDialog OpenFileDi = new OpenFileDialog();
            OpenFileDi.InitialDirectory = @"E:\";
            OpenFileDi.Title = "Avatar Select";
            OpenFileDi.Filter = "(All Images Files)|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG";
            OpenFileDi.FilterIndex = 3;

            if (OpenFileDi.ShowDialog() == DialogResult.OK)
            {
                picturUpdate.Image = Image.FromFile(OpenFileDi.FileName);
                AccountInFo.Photo = OpenFileDi.FileName;
            }


        }
        bool CheckUserName()
        {
            return txtUserNameSignIn.Text == AccountInFo.User;
        }
        bool CheckPassword()
        {
            return txtPasswordSignIn.Text == AccountInFo.Password;
        }
        bool SignIn()
        {
            bool isUser = CheckUserName();
            bool isPassword = CheckPassword();

            if (!isUser)
                lblInvalidAdminSignin.Visible = true;
            else
                lblInvalidAdminSignin.Visible = false;

            if (!isPassword)
                lblInvalidPasswordSignIn.Visible = true;
            else
                lblInvalidPasswordSignIn.Visible = false;


            if (isUser && isPassword)
            {
                frmHome frm = new frmHome(AccountInFo.User, AccountInFo.Photo);
                frm.ShowDialog();
                return true;
            }
            return false;
        }
        void loadInformationFromUpdate()
        {
            AccountInFo.User = txtUserNameSignUp.Text;
            AccountInFo.Password = txtPasswordSignUp.Text;
        }
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            SignIn();

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!Updatesign(AccountInFo))
                return;

            loadInformationFromUpdate();
            MessageBox.Show("Sign Up Success", "Sign Up", MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
        }

        private void btnUpdatePhoto_Click(object sender, EventArgs e)
        {
            UploadPhoto();

        }
        stAccountInFo LoadDefaultUsserAndPassword()
        {
            AccountInFo.User = "Admin";
            AccountInFo.Password = "12345";
            return AccountInFo;
        }

        private void frmTasky_Load(object sender, EventArgs e)
        {
            LoadDefaultUsserAndPassword();

        }
    }
}
