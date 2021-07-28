using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace X4Tool
{
    public partial class AboutMe : Form
    {
        public AboutMe()
        {
            InitializeComponent();
        }

        private void btn_close_about_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutMe_Load(object sender, EventArgs e)
        {
            this.Text = LanguageHelp.aboutMe;
            label_coded.Text = LanguageHelp.codedText;
            link_download.Text = LanguageHelp.download;
            btn_close_about.Text = LanguageHelp.close_about;
            link_download.Visible = !CommonDef.IsEmpty(CommonDef.download_url);
        }

        private void link_download_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("http\\shell\\open\\command\\");
            string input = registryKey.GetValue("").ToString();
            Regex regex = new Regex("\"([^\"]+)\"");
            MatchCollection matchCollection = regex.Matches(input);
            string text = "";
            if (matchCollection.Count > 0)
            {
                text = matchCollection[0].Groups[1].Value;
                Process.Start(text, CommonDef.download_url);
            }
        }
    }
}
