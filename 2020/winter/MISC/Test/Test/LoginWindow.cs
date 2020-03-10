using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Client
{
    public class LoginWindow : Form
    {
        public bool CookieEnabled
        {get; private set;}
        protected override void OnShown(EventArgs e)
        {
            if(ClientFunctions.cookieInfo != "")
                ClientFunctions.Send
                    (ClientFunctions.Login(
                        ClientFunctions.cookieInfo.Split('\n')[0],
                        ClientFunctions.cookieInfo.Split('\n')[1]));
            else
                base.OnShown(e);
        }
        public LoginWindow()
        {
            var controlList = new List<Control>();
            controlList.Add(new Label 
            { 
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width / 2, 30),
                Text = "Nickname",
            });

            controlList.Add(new TextBox
            {
                Location = new Point(0, controlList.Last().Bottom),
                Size = controlList.Last().Size,
            });

            controlList.Add(new Label
            {
                Location = new Point(0, controlList.Last().Bottom),
                Size = new Size(ClientSize.Width / 2, 30),
                Text = "Password",
            });

            controlList.Add(new TextBox
            {
                Location = new Point(0, controlList.Last().Bottom),
                Size = controlList.Last().Size,
                UseSystemPasswordChar = true,
            });

            controlList.Add(new CheckBox
            {
                Location = new Point(controlList.Last().Right + 10, controlList[2].Bottom),
                Size = new Size(30, 30),
                Text = "Remember Me",
                AutoCheck = false,
                Name = "CookieCheckBox",
            });
            ((CheckBox)controlList.Last()).Click += (sender, args) =>
            {
                var check = (CheckBox)controlList[4];
                if (check.Checked)
                {
                    CookieEnabled = false;
                    check.Checked = false;
                }
                else
                {
                    CookieEnabled = true;
                    check.Checked = true;
                }
                    
            };

            controlList.Add(new Button
            {
                Location = new Point(0, controlList[3].Bottom),
                Size = new Size(controlList[3].Size.Width / 2,
                                controlList[3].Size.Height),
                Text = "Login",
            });
            ((Button)controlList.Last()).Click += (sender, args) =>
            {
                ClientFunctions
                .Send(ClientFunctions
                    .Login(controlList[1].Text, controlList[3].Text));
            };

            controlList.Add(new Button
            {
                Location = new Point(controlList.Last().Right, controlList[3].Bottom),
                Size = new Size(controlList[3].Size.Width / 2,
                                controlList[3].Size.Height),
                Text = "Register",
            });
            ((Button)controlList.Last()).Click += (sender, args) =>
            {
                ClientWindows.CurrentWindow = new RegistrationForm();
            };
            foreach (var ctrl in controlList)
            {
                Controls.Add(ctrl);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LoginWindow
            // 
            this.ClientSize = new System.Drawing.Size(600, 800);
            this.Name = "LoginWindow";
            this.ResumeLayout(false);

        }
    }
}
