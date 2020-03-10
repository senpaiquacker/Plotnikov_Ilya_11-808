using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace Client
{
    public class MainWindow : Form
    {
        public string LoggedNickname { get; internal set; }
        public List<string> LinkList { get; internal set; }
        protected override void OnClosing(CancelEventArgs e)
        {
            ClientFunctions.LogOut(LoggedNickname);
            base.OnClosing(e);
        }
        public MainWindow()
        {
            Size = new Size(1024, 768);
            var linksList = new CheckedListBox
            {
                Location = new Point(5, 10),
                Size = new Size(ClientSize.Width / 2 - 5, ClientSize.Height),
                SelectionMode = SelectionMode.None,
            };
            linksList.ItemCheck += (sender, args) =>
            {
                if (args.CurrentValue == CheckState.Unchecked ||
                args.CurrentValue == CheckState.Indeterminate)
                    args.NewValue = CheckState.Checked;
                else
                    args.NewValue = CheckState.Unchecked;
            };
            var downloadButton = new Button
            {
                Text = "Download",
                Location = new Point(linksList.Right + 5, 10),
                Size = new Size(100, 20),
            };
            downloadButton.Click += (sender, args) =>
            {
                ClientFunctions
                .Download(LinkList
                    .Where(a => !linksList
                        .CheckedItems
                        .Contains(a))
                    .ToArray());
                foreach (var i in LinkList
                .Where(a => ClientFunctions
                    .IsDownloaded(a
                        .Split('/')
                        .Last())))
                    linksList.SetItemChecked(linksList.Items.IndexOf(i), true);
            };
            var getButton = new Button
            {
                Text = "Get Links",
                Location = new Point
                (downloadButton.Location.X,
                downloadButton.Bottom + 5),
                Size = downloadButton.Size,
            };
            getButton.Click += (sender, args) =>
            {
                if(LinkList != null)
                    linksList.Items.Clear();
                ClientFunctions.Send(ClientFunctions.DownloadLinks(LoggedNickname));
                linksList.Items.AddRange(LinkList.ToArray());
            };
            Controls.Add(linksList);
            Controls.Add(downloadButton);
            Controls.Add(getButton);
            Load += (sender, args) =>
            {
                ClientWindows.CurrentWindow = new LoginWindow();
            };
            Shown += (sender, args) =>
            {
                if (ClientWindows.CurrentWindow is LoginWindow)
                    Hide();
                else
                    ClientFunctions.Send(ClientFunctions.DownloadLinks(LoggedNickname));
            };
        }

    }
}
