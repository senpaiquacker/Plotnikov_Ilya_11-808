using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
namespace Client
{
    public class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            var controlList = new List<Control>();
            controlList.Add(new Label
            {
                Text = "Nickname",
                Location = new Point(ClientSize.Width / 2, 0),
                Size = new Size(ClientSize.Width / 2, 30),
            });

            controlList.Add(new TextBox
            {
                Location = new Point(ClientSize.Width / 2, controlList.Last().Bottom),
                Size = controlList.Last().Size,
            });

            controlList.Add(new Label
            {
                Text = "E-mail",
                Location = new Point(ClientSize.Width / 2, controlList.Last().Bottom),
                Size = controlList.Last().Size,
            });

            controlList.Add(new TextBox
            {
                Location = new Point(ClientSize.Width / 2, controlList.Last().Bottom),
                Size = controlList.Last().Size,
            });

            controlList.Add(new Label
            {
                Text = "Password",
                Location = new Point(ClientSize.Width / 2, controlList.Last().Bottom),
                Size = controlList.Last().Size,
            });

            controlList.Add(new TextBox
            {
                Location = new Point(ClientSize.Width / 2, controlList.Last().Bottom),
                Size = controlList.Last().Size,
                UseSystemPasswordChar = true,
            });
            controlList.Add(new Button
            {
                Text = "Submit",
                Location = new Point(ClientSize.Width / 2, controlList.Last().Bottom),
                Size = controlList.Last().Size,
            });
            ((Button)controlList.Last()).Click += (sender, args) =>
            {
                ClientFunctions.Send(ClientFunctions.Register
                    (controlList[1].Text,
                     controlList[3].Text,
                     controlList[5].Text
                    ));
            };
            foreach (var ctrl in controlList)
            {
                Controls.Add(ctrl);
            }
        }
    }
}
