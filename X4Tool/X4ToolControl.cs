using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using X4Tool;

namespace X4ToolControl
{
    class NPCSkill:TextBox
    {
        private XmlNode npc = null;
        private string skill = "";
        private bool isRead = false;
        public NPCSkill()
        {
            //base.Maximum = 15;
            //base.Minimum = 0;
            this.TextChanged += textChanged;
        }

        private void textChanged(object sender, EventArgs e)
        {
            if (!isRead)
                setSkillValue();
        }

        public void setNPCSkill(XmlNode npc,string skill)
        {
            this.npc = npc;
            this.skill = skill;
            isRead = true;
            getSkillValue();
            isRead = false;

        }

        private void getSkillValue()
        {
            if (npc != null)
            {
                XPathNavigator nav = npc.CreateNavigator();
                XPathNodeIterator iter_a = nav.SelectChildren("traits", "");
                while (iter_a.MoveNext())
                {
                    XPathNodeIterator iter = iter_a.Current.SelectChildren("skill", "");
                    while (iter.MoveNext())
                    {
                        string type = iter.Current.GetAttribute("type", "");
                        if (type == skill)
                        {
                            base.Text = iter.Current.GetAttribute("value", "");
                            //iter.Current.MoveToAttribute
                            return;
                        }
                    }
                }
            }
            base.Text = "0";
        }

        private void setSkillValue()
        {
            SaveFileHandle.setSkillValue(npc, skill, base.Text);
        }
    }
    class KeyShowListItem : ListViewItem
    {
        private object _key;
        private string _show;
        public KeyShowListItem(object key,string show)
        {
            _key = key;
            _show = show;
            base.Text = ShowText;
        }

        public object Key
        {
            get
            {
                return _key;
            }
        }
        public string ShowText
        {
            get
            {
                return _show;
            }
        }

    }
}
