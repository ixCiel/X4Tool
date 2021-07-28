using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using X4ToolControl;

namespace X4Tool
{
    public partial class X4Tool : Form
    {
        public delegate void BOOLDelegate(bool f);

        public delegate void ContorlEnable(Control control,bool f);

        public delegate void TextBoxSetTextDelegate(TextBox textBox,string text);
        public delegate void ControlItemAdd(object comboBox, object item);

        SaveFileHandle handle;
        public X4Tool()
        {
            InitializeComponent();
        }

        public void EnableContorl(Control control, bool f)
        {
            if (!base.InvokeRequired)
            {
                control.Enabled = f;

            }
            else
            {
                Invoke(new ContorlEnable(EnableContorl), f);
            }
        }

        public void EnableControls(bool f)
        {
            if (!base.InvokeRequired)
            {
                infoTab.Enabled = f;
                npc_panel.Enabled = f;
                ship_panel.Enabled = f;
                stations_panel.Enabled = f;
                modTab.Text = "";
                //group_relation.Enabled = f;
                
            }
            else
            {
                Invoke(new BOOLDelegate(EnableControls), f);
            }
        }

        public void AddItem2ComboBox(object comboBox,object item)
        {
            if (!base.InvokeRequired)
            {
                if (item != null)
                    ((ComboBox)comboBox).Items.Add(item);
                else
                {
                    ((ComboBox)comboBox).SelectedItem = null;
                    ((ComboBox)comboBox).Items.Clear();
                }
                //group_relation.Enabled = f;

            }
            else
            {
                Invoke(new ControlItemAdd(AddItem2ComboBox), new object[] { comboBox,item});
            }
        }

        public void AddItem2ListView(object listView, object item)
        {
            if (!base.InvokeRequired)
            {
                if (item != null)
                    ((ListView)listView).Items.Add((ListViewItem)item);
                else
                {
                    ((ListView)listView).Items.Clear();
                    ((ListView)listView).SelectedItems.Clear();
                }
                //group_relation.Enabled = f;

            }
            else
            {
                Invoke(new ControlItemAdd(AddItem2ListView), new object[] { listView, item });
            }
        }

        public void SetTextBoxText(TextBox textBox,string text)
        {
            if (!base.InvokeRequired)
            {
                textBox.Text = text;

            }
            else
            {
                Invoke(new TextBoxSetTextDelegate(SetTextBoxText), new object[] { textBox,text});
            }
        }

        private void changeFormLanguage(int lgCode)
        {
            LanguageHelp.languageCode = lgCode;
            fileMenuItem.Text = LanguageHelp.menu_file;
            openMenuItem.Text = LanguageHelp.menu_open;
            closeMenuItem.Text = LanguageHelp.menu_close;
            exitMenuItem.Text = LanguageHelp.menu_exit;
            saveMenuItem.Text = LanguageHelp.menu_save;
            laguageMenuItem.Text = LanguageHelp.menu_language;
            chineseMenuItem.Text = LanguageHelp.menu_zh;
            englishMenuItem.Text = LanguageHelp.menu_en;
            helpMenuItem.Text = LanguageHelp.menu_help;
            aboutMenuItem.Text = LanguageHelp.menu_about;

            infoTab.Text = LanguageHelp.infoTab;
            groupSaveFile.Text = LanguageHelp.group_save_file;
            label_save_name.Text = LanguageHelp.save_file_name;
            label_save_time.Text = LanguageHelp.save_time;
            label_save_ver.Text = LanguageHelp.save_ver;
            label_play_time.Text = LanguageHelp.game_time;
            groupPlayer.Text = LanguageHelp.group_player;
            label_player_name.Text = LanguageHelp.player_name;
            label_player_location.Text = LanguageHelp.player_location;
            label_player_money.Text = LanguageHelp.player_money;
            label_player_id.Text = LanguageHelp.player_id;

            relationTab.Text = LanguageHelp.tab_relation;
            label_faction_list.Text = LanguageHelp.label_faction;
            label_relation.Text = LanguageHelp.label_relation;
            label_relation_time.Text = LanguageHelp.label_relation_time;
            check_relation_modify.Text = LanguageHelp.check_relation_modify;

            label_name.Text = LanguageHelp.label_name;
            label_boarding.Text = LanguageHelp.label_boarding;
            label_engineering.Text = LanguageHelp.label_engineering;
            label_management.Text = LanguageHelp.label_management;
            label_morale.Text = LanguageHelp.label_morale;
            label_piloting.Text = LanguageHelp.label_piloting;

            shipTab.Text = LanguageHelp.tab_ship;
            btn_get_the_ship.Text = LanguageHelp.get_the_ship;
            btn_massacre_ship.Text = LanguageHelp.massacre_ship;

            stationTab.Text = LanguageHelp.tab_station;
            btn_station_lock.Text = LanguageHelp.station_lock;
            btn_station_exchange.Text = LanguageHelp.station_exchange;
            base.Text = LanguageHelp.X4Tool + " V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        
        private void saveFileHandleConsole(string text,int progress)
        {
            if (!base.InvokeRequired)
            {
                statusLabel.Visible = !CommonDef.IsEmpty(text);
                if (!CommonDef.IsEmpty(text))
                {
                    statusLabel.Text = text;
                }
                progressBar.Visible = progress >= 0;
                if (progress < 0)
                    progress = 0;
                if (progress > 100)
                    progress = 100;
                progressBar.Value = progress;
            }
            else
            {
                Invoke(new ConsoleOut(saveFileHandleConsole), new object[]{ text,progress});
            }
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GZIP Save Files (*.gz)|*.gz";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.InitialDirectory = Path.Combine(openFileDialog.InitialDirectory, "Egosoft");
            openFileDialog.InitialDirectory = Path.Combine(openFileDialog.InitialDirectory, "X4");
            string[] paths=Directory.GetDirectories(openFileDialog.InitialDirectory);

            if(paths.Length >0)
            {
                if(paths.Length == 1)
                {
                    openFileDialog.InitialDirectory = Path.Combine(openFileDialog.InitialDirectory, paths[0]);
                    openFileDialog.InitialDirectory = Path.Combine(openFileDialog.InitialDirectory, "save");
                }
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
                handle = new SaveFileHandle(openFileDialog.FileName);
                handle.consoleUpdate = saveFileHandleConsole;
                new Thread(LoadingSave).Start();
            }
        }

        XmlNode player = null;

        private void LoadingSave()
        {
            EnableControls(f: false);
            saveFileHandleConsole(LanguageHelp.startLoading, 0);
            if (handle.Loadding())
            {
                LoadedSave();
                EnableControls(f: true);

                saveFileHandleConsole(LanguageHelp.loadSuccess, -1);
            }
            else
            {
                handle = null;
                saveFileHandleConsole(LanguageHelp.loadFailed, -1);
            }
        }

        private void setXmlPath()
        {
            xml_save_name.setKey("/savegame/info/save", "name");
            xml_save_ver.setKey("/savegame/info/game", "version");
            xml_save_time.setKey("/savegame/info/save", "date");
            xml_play_time.setKey("/savegame/info/game", "time");

            xml_player_name.setKey("/savegame/info/player", "name");
            xml_player_money.setKey("/savegame/info/player", "money");
            xml_player_location.setKey("/savegame/info/player", "location");

        }

        private void setXmlGetHanle()
        {
        }

        private void setXmlSetHandle()
        {
        }

        public string getXmlValueText(string path,string key)
        {
            if (handle != null)
                return handle.getXmlValueText(path, key);
            return "";
        }

        public void setXmlValueText(string path,string key,string value)
        {
            if(handle != null)
            {
                handle.setXmlValueText(path, key, value);
            }
        }

        public string player_money_changed(string money)
        {
            if(handle != null)
            {
                handle.setAccountAmount(money);
            }
            return money;
        }

        private void LoadedSave()
        {
            saveFileHandleConsole(LanguageHelp.loadInformation, 15);
            xml_save_name.Read();
            xml_save_ver.Read();
            xml_save_time.Read();
            xml_play_time.Read();

            xml_player_name.Read();
            xml_player_money.Read();
            xml_player_location.Read();

            SetTextBoxText(text_player_id, handle.player_id);


            saveFileHandleConsole(LanguageHelp.loadRelation, 20);
            LinkedList<string> factions = handle.getFactions();
            AddItem2ComboBox(combo_factions_for_relations, null);
            foreach (string faction in factions)
            {
                KeyShowItem item = new KeyShowItem(faction, LanguageHelp.getFactionName(faction));
                AddItem2ComboBox(combo_factions_for_relations, item);
            }
            AddItem2ListView(list_relations, null);

            saveFileHandleConsole(LanguageHelp.loadNPC, 30);
            LinkedList<string> owners = new LinkedList<string>();
            XmlNodeList nodes = handle.selectPath(string.Format("//component[@class='npc']/@owner"));
            foreach (XmlNode node in nodes)
            {
                if (node.InnerText.StartsWith("visitor"))
                    continue;
                if (!owners.Contains(node.InnerText))
                {
                    owners.AddLast(node.InnerText);
                    KeyShowItem item = new KeyShowItem(node.InnerText, LanguageHelp.getFactionName(node.InnerText));
                    AddItem2ComboBox(combo_owners_for_npc, item);
                }
            }
            owners.Clear();
            string[] ship_types = new string[] {"ship_xs", "ship_s", "ship_m", "ship_l", "ship_xl" };
            foreach(string ship_type in ship_types)
            {
                nodes = handle.selectPath(string.Format("//component[@class='{0}']/@owner",ship_type));
                foreach (XmlNode node in nodes)
                {
                    if (node.InnerText.StartsWith("visitor"))
                        continue;
                    if (!owners.Contains(node.InnerText))
                    {
                        owners.AddLast(node.InnerText);
                        KeyShowItem item = new KeyShowItem(node.InnerText, LanguageHelp.getFactionName(node.InnerText));
                        AddItem2ComboBox(combo_owners_for_ship, item);
                    }
                }
            }

            owners.Clear();
            string[] station_types = new string[] { "station" };
            foreach (string station_type in station_types)
            {
                nodes = handle.selectPath(string.Format("//component[@class='{0}']/@owner", station_type));
                foreach (XmlNode node in nodes)
                {
                    if (node.InnerText.StartsWith("visitor"))
                        continue;
                    if (!owners.Contains(node.InnerText))
                    {
                        owners.AddLast(node.InnerText);
                        KeyShowItem item = new KeyShowItem(node.InnerText, LanguageHelp.getFactionName(node.InnerText));
                        AddItem2ComboBox(combo_owners_for_station, item);
                    }
                }
            }

            player = handle.selectNode(string.Format("//component[@class='player']"));
            //LinkedList<string> skills = new LinkedList<string>();
            //nodes = handle.selectPath(string.Format("//skill/@type"));
            //foreach (XmlNode node in nodes)
            //{
            //    if(!skills.Contains(node.InnerText))
            //    {
            //        skills.AddLast(node.InnerText);
            //    }
            //}
        }

        private void X4Tool_Resize(object sender, EventArgs e)
        {
            tabControl.Size = new Size(this.Size.Width - 16, this.Size.Height - 92);
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {

            if (handle != null)
            {
                new Thread(Saving).Start();
            }
        }

        private void Saving()
        {
            handle.save();
        }

        public string relation_changed(string relation)
        {
            if (handle != null && check_relation_modify.Checked)
            {
                KeyShowListItem listItem = (KeyShowListItem)list_relations.SelectedItems[0];
                int s = ((string)listItem.Key).IndexOf("id='") + 4;
                int e = ((string)listItem.Key).IndexOf("'", s);
                string faction = ((string)listItem.Key).Substring(s, e - s);

                s = ((string)listItem.Key).IndexOf("faction='") + 9;
                e = ((string)listItem.Key).IndexOf("'", s);
                string id = ((string)listItem.Key).Substring(s, e - s);
                setXmlValueText(string.Format("/savegame/universe/factions/faction[@id='{0}']/relations/booster[@faction='{1}']",id,faction), "relation", relation);
                setXmlValueText(string.Format("/savegame/universe/factions/faction[@id='{0}']/relations/relation[@faction='{1}']", id, faction), "relation", relation);
            }
            return relation;
        }

        private void X4Tool_Load(object sender, EventArgs e)
        {
            XmlTextBox.GetXmlValueText = getXmlValueText;
            XmlTextBox.SetXmlValueText = setXmlValueText;
            setXmlPath();
            xml_player_money.XmlValueChanged = player_money_changed;
            xml_relation.XmlValueChanged = relation_changed;
            changeFormLanguage(86);
            EnableControls(false);
            SuperBuffer(false);
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutMe me = new AboutMe();
            me.StartPosition = FormStartPosition.CenterScreen;
            me.ShowDialog(this);
        }

        private void chineseMenuItem_Click(object sender, EventArgs e)
        {
            changeFormLanguage(86);
        }

        private void englishMenuItem_Click(object sender, EventArgs e)
        {
            changeFormLanguage(49);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            handle = null;
            EnableControls(false);
        }

        private void combo_factions_for_relations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(combo_factions_for_relations.SelectedItem == null)
            {
                AddItem2ListView(list_relations, null);

            }
            else
            {
                KeyShowItem item = (KeyShowItem)combo_factions_for_relations.SelectedItem;
                string faction = (string)item.Key;

                AddItem2ListView(list_relations, null);
                group_relation.Enabled = false;
                XmlNodeList nodes = handle.selectPath(string.Format("/savegame/universe/factions/faction[@id='{0}']/relations/booster/@faction", faction));
                foreach(XmlNode node in nodes)
                {
                    if (node.InnerText.StartsWith("visitor"))
                        continue;
                    KeyShowListItem listItem = new KeyShowListItem(string.Format("/savegame/universe/factions/faction[@id='{0}']/relations/booster[@faction='{1}']", faction, node.InnerText), LanguageHelp.getFactionName(node.InnerText));
                    AddItem2ListView(list_relations, listItem);
                }
                nodes = handle.selectPath(string.Format("/savegame/universe/factions/faction[@id='{0}']/relations/relation/@faction", faction));
                foreach (XmlNode node in nodes)
                {
                    if (node.InnerText.StartsWith("visitor"))
                        continue;
                    KeyShowListItem listItem = new KeyShowListItem(string.Format("/savegame/universe/factions/faction[@id='{0}']/relations/relation[@faction='{1}']", faction, node.InnerText), LanguageHelp.getFactionName(node.InnerText));
                    AddItem2ListView(list_relations, listItem);
                }
            }
        }

        private void list_relations_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if(list_relations.SelectedItems.Count == 0)
            {
                group_relation.Enabled = false;
            }
            else
            {
                KeyShowListItem listItem = (KeyShowListItem)list_relations.SelectedItems[0];
                xml_relation.setKey((string)listItem.Key,"relation");
                xml_relation.Read();
                if (((string)listItem.Key).Contains("booster"))
                {
                    xml_relation_time.setKey((string)listItem.Key, "time");
                    xml_relation_time.Read();
                    xml_relation.Enabled = true;
                    xml_relation_time.Enabled = true;
                    xml_relation_time.ReadOnly = false;
                }
                else
                {
                    xml_relation_time.setKey("", "");
                    xml_relation_time.SetTextWithoutWrite("0");
                    xml_relation.Enabled = true;
                    xml_relation_time.Enabled = false;
                    xml_relation_time.ReadOnly = true;
                }
                group_relation.Enabled = true;

            }
        }

        LinkedList<XmlNode> peoples = new LinkedList<XmlNode>();

        private void combo_owners_for_npc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_owners_for_npc.SelectedItem == null)
            {
                AddItem2ListView(list_npcs, null);

            }
            else
            {
                KeyShowItem item = (KeyShowItem)combo_owners_for_npc.SelectedItem;
                faction_npc = (string)item.Key;
                EnableControls(false);
                group_npc.Enabled = false;
                new Thread(loading_npcs).Start();
            }
        }

        private void loading_npcs()
        {
            AddItem2ListView(list_npcs, null);
            XmlNodeList nodes = handle.selectPath(string.Format("//component[@class='npc'][@owner='{0}']", faction_npc));
            peoples.Clear();
            saveFileHandleConsole("loading npc", 0);
            int total = nodes.Count;
            int n = 0;
            foreach (XmlNode node in nodes)
            {
                //string name = handle.getXmlValueText(string.Format("//component[@class='npc'][@owner='{0}'][@id='{1}']", faction, node.InnerText), "name") + node.InnerText;
                string name = node.Attributes["name"].InnerText + node.Attributes["id"].InnerText;
                KeyShowListItem listItem = new KeyShowListItem(node, name);
                AddItem2ListView(list_npcs, listItem);
                peoples.AddLast(node);
                saveFileHandleConsole("loading npc", ++n * 100 / total);

            }
            saveFileHandleConsole("loading finish", -1);
            EnableControls(true);
        }

        XmlNode npc = null;

        private void list_npcs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (list_npcs.SelectedItems.Count == 0)
            {
                group_npc.Enabled = false;
                npc = null;
            }
            else
            {
                KeyShowItem item = (KeyShowItem)combo_owners_for_npc.SelectedItem;
                //string faction = (string)item.Key;
                KeyShowListItem listItem = (KeyShowListItem)list_npcs.SelectedItems[0];
                npc = (XmlNode)listItem.Key;
                text_npc_id.Text = npc.Attributes["id"].InnerText;
                text_npc_name.Text = npc.Attributes["name"].InnerText;
                group_npc.Enabled = true;
                //"management" "morale" "piloting" "engineering" "boarding"
                skill_management.setNPCSkill(npc, "management");
                skill_morale.setNPCSkill(npc, "morale");
                skill_piloting.setNPCSkill(npc, "piloting");
                skill_engineering.setNPCSkill(npc, "engineering");
                skill_boarding.setNPCSkill(npc, "boarding");

            }
        }

        private void text_npc_name_TextChanged(object sender, EventArgs e)
        {
            npc.Attributes["name"].InnerText = text_npc_name.Text;
        }

        string faction_npc = "";
        string faction_ship = "";
        string faction_station = "";
        private void combo_owners_for_ship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_owners_for_ship.SelectedItem == null)
            {
                AddItem2ListView(list_ships, null);
                group_ship.Enabled = false;
            }
            else
            {
                EnableControls(false);
                KeyShowItem item = (KeyShowItem)combo_owners_for_ship.SelectedItem;
                faction_ship = (string)item.Key;
                new Thread(loading_ships).Start();
            }
        }

        private void loading_ships()
        {
            group_ship.Enabled = false;
            AddItem2ListView(list_ships, null);
            XmlNodeList nodes = null;
            string[] ship_types = new string[] { "ship_xl", "ship_l", "ship_m", "ship_s", "ship_xs" };
            foreach (string ship_type in ship_types)
            {
                nodes = handle.selectPath(string.Format("//component[@class='{0}'][@owner='{1}']", ship_type, faction_ship));
                saveFileHandleConsole("loading " + ship_type, 0);
                int total = nodes.Count;
                int n = 0;
                foreach (XmlNode node in nodes)
                {
                    string name = ship_type.Substring(5).ToUpper()+":";
                    //string name = handle.getXmlValueText(string.Format("//component[@class='npc'][@owner='{0}'][@id='{1}']", faction, node.InnerText), "name") + node.InnerText;
                    string id = node.Attributes["id"].InnerText;
                    try
                    {
                        string rename = node.Attributes["name"].InnerText;
                        name += rename;
                    }
                    catch
                    {
                    }

                    try
                    {
                        string code = node.Attributes["code"].InnerText;
                        name += "[" + code + "]";
                    }
                    catch (Exception ex)
                    {
                        saveFileHandleConsole(ex.Message, -1);
                    }
                    //name += id;
                    //XmlNode p = node.ParentNode;
                    //while(p!=null)
                    //{
                    //    string pName = p.LocalName;//component class = zone sector cluster 
                    //    p = p.ParentNode;

                    //}
                    KeyShowListItem listItem = new KeyShowListItem(node, name);
                    AddItem2ListView(list_ships, listItem);
                    saveFileHandleConsole("loading " + ship_type, ++n * 100 / total);
                }
            }
            saveFileHandleConsole("loading finish",-1);
            EnableControls(true);

        }

        XmlNode ship = null;

        private void list_ships_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (list_ships.SelectedItems.Count == 0)
            {
                group_ship.Enabled = false;
                ship = null;
            }
            else
            {
                KeyShowItem item = (KeyShowItem)combo_owners_for_ship.SelectedItem;
                string faction = (string)item.Key;
                KeyShowListItem listItem = (KeyShowListItem)list_ships.SelectedItems[0];
                ship = (XmlNode)listItem.Key;
                group_ship.Enabled = true;
                text_ship_class.Text = ship.Attributes["class"].InnerText;
                text_ship_macro.Text = ship.Attributes["macro"].InnerText;
                try
                {
                    text_ship_name.Text = ship.Attributes["name"].InnerText;
                }
                catch
                {
                    text_ship_name.Text = "";
                }
            }
        }
        private void text_ship_name_TextChanged(object sender, EventArgs e)
        {
            if (ship != null)
            {
                string new_name = text_ship_name.Text;
                XmlAttribute name = null;
                foreach(XmlAttribute attr in ship.Attributes)
                {
                    if(attr.LocalName == "name")
                    {
                        name = attr;
                        break;
                    }
                }
                if (CommonDef.IsEmpty(new_name))
                {
                    if (name != null)
                    {
                        ship.RemoveChild(name);
                    }
                }
                else
                {
                    if(name == null)
                    {
                        name = ship.OwnerDocument.CreateAttribute("name");
                        ship.Attributes.Append(name);
                    }
                    name.InnerText = new_name;
                }
            }
        }

        private static bool isReading = false;

        private void btn_ship_buff_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if(btn != null)
            {
                string[] a = btn.Name.Split('_');
                switch(a[2])
                {
                    case "mass":
                        text_ship_mass_mass.Text = CommonDef.getRandomDouble(0.7,1.0).ToString();
                        text_ship_mass_maxhull.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        text_ship_mass_drag.Text = CommonDef.getRandomDouble(0.7, 1.0).ToString();
                        text_ship_mass_countermeasurecapacity.Text = CommonDef.getRandomInt(1, 4).ToString();
                        text_ship_mass_deployablecapacity.Text = CommonDef.getRandomInt(1, 4).ToString();
                        text_ship_mass_unitcapacity.Text = CommonDef.getRandomInt(1, 4).ToString();
                        text_ship_mass_missilecapacity.Text = CommonDef.getRandomInt(1, 4).ToString();
                        text_ship_mass_radarrange.Text = CommonDef.getRandomDouble(1.0, 1.2).ToString();
                        break;
                    case "engine":
                        text_ship_engine_forwardthrust.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        text_ship_engine_strafethrust.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        text_ship_engine_rotationthrust.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        text_ship_engine_travelthrust.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        text_ship_engine_travelstartthrust.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        text_ship_engine_travelattacktime.Text = CommonDef.getRandomDouble(0.7, 1).ToString();
                        text_ship_engine_travelchargetime.Text = CommonDef.getRandomDouble(0.7, 1).ToString();
                        text_ship_engine_boostthrust.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        text_ship_engine_boostduration.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        break;
                    case "shield":
                        text_ship_shield_capacity.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        text_ship_shield_rechargedelay.Text = CommonDef.getRandomDouble(0.8, 1).ToString();
                        text_ship_shield_rechargerate.Text = CommonDef.getRandomDouble(1, 1.3).ToString();
                        break;
                    case "weapon":
                        text_ship_weapon_damage.Text = CommonDef.getRandomDouble(1, 1.2).ToString();
                        text_ship_weapon_cooling.Text = CommonDef.getRandomDouble(1, 1.2).ToString();
                        text_ship_weapon_reload.Text = CommonDef.getRandomDouble(1, 1.2).ToString();
                        text_ship_weapon_sticktime.Text = CommonDef.getRandomDouble(1, 1.2).ToString();
                        text_ship_weapon_speed.Text = CommonDef.getRandomDouble(1, 1.2).ToString();
                        text_ship_weapon_rotationspeed.Text = CommonDef.getRandomDouble(1, 1.2).ToString();
                        text_ship_weapon_chargetime.Text = CommonDef.getRandomDouble(0.8, 1).ToString();
                        text_ship_weapon_lifetime.Text = CommonDef.getRandomDouble(1, 1.2).ToString();
                        text_ship_weapon_beamlength.Text = CommonDef.getRandomDouble(1, 1.2).ToString();
                        break;
                }
            }
        }
        private void text_ship_buff_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null || isReading || ship == null)
                return;
            if (ship.Attributes["class"].InnerText == "ship_l")
            {
                saveFileHandleConsole("Can't buff ship_l",-1);
                return;
            }
            string[] a = tb.Name.Split('_');
            List<XmlNode> modifications = null;
            string localName = "engine";
            string mod = "mod_engine_travelthrust_01_mk3";
            switch (a[2])
            {
                case "mass":
                case "engine":
                    {
                        if (a[2] == "mass")
                        {
                            localName = "ship";
                            mod = "mod_ship_mass_01_mk3";
                        }
                        else if (ship.Attributes["class"].InnerText == "ship_xl")
                        {
                            saveFileHandleConsole($"Can't buff ship_l for {a[2]}", -1);
                            return;
                        }

                        do
                        {
                            List<XmlNode> modification = SaveFileHandle.findChildNodes(ship, "modification", "", "", false, false);

                            if (modification.Count == 0)
                            {
                                ship.CreateNavigator().AppendChild("<modification />");
                            }
                            else if (modification.Count == 1)
                            {
                                do
                                {
                                    modifications = SaveFileHandle.findChildNodes(modification[0], localName);
                                    if (modifications.Count > 0)
                                        break;
                                    if(localName == "engine")
                                    {
                                        modification[0].CreateNavigator().PrependChild($"<{localName} ware=\"{mod}\"/>");
                                    }
                                    else
                                        modification[0].CreateNavigator().AppendChild($"<{localName} ware=\"{mod}\"/>");
                                } while (true);
                                break;
                            }
                            return;
                        } while (true);
                    }
                    break;
                case "shield":
                    {
                        localName = "modification";
                        mod = "mod_shield_capacity_02_mk3";
                        XmlNode shields = null;
                        List<XmlNode> groups = null;
                        shields = SaveFileHandle.findChildNode(ship, "shields", "", "", false);
                        if(shields == null)
                        {
                            shields = ship.OwnerDocument.CreateNode(XmlNodeType.Element, "shields", null);
                            XmlNode group = ship.OwnerDocument.CreateNode(XmlNodeType.Element, "group", null);
                            shields.AppendChild(group);
                            saveFileHandleConsole("Can't found shields", -1);
                            XmlNode ammunition = SaveFileHandle.findChildNode(ship, "ammunition", "", "", false);
                            if (ammunition != null)
                            {
                                ship.InsertBefore(shields, ammunition);

                            }else
                            {
                                XmlNode people = SaveFileHandle.findChildNode(ship, "people", "", "", false);
                                if(people != null)
                                {
                                    ship.InsertAfter(shields, people);
                                }
                                else
                                {
                                    ship.AppendChild(shields);
                                }
                            }
                            //ship.CreateNavigator().AppendChild("<shields ><group /></shields>");
                            shields = SaveFileHandle.findChildNode(ship, "shields", "", "", false);
                        }
                        if (shields == null)
                        {
                            shields = SaveFileHandle.findChildNode(ship, "shields", "", "", false);
                            return;
                        }
                        groups = SaveFileHandle.findChildNodes(shields, "group", "", "", false);
                        if (groups.Count == 0)
                        {
                            saveFileHandleConsole("Can't found shield group", -1);
                            return;
                        }
                        modifications = new List<XmlNode>();
                        foreach(XmlNode group in groups)
                        {
                            while (true)
                            {
                                XmlNode mo = SaveFileHandle.findChildNode(group, localName, "", "", false);
                                if (mo != null)
                                {
                                    modifications.Add(mo);
                                    break;
                                }
                                group.CreateNavigator().AppendChild($"<{localName} ware=\"{mod}\"/>");
                            }
                        }
                    }
                    break;
                case "weapon":
                    {
                        if (ship.Attributes["class"].InnerText == "ship_xl")
                        {
                            saveFileHandleConsole($"Can't buff ship_l for {a[2]}", -1);
                            return;
                        }
                        localName = "modification";
                        mod = "mod_weapon_damage_02_mk3";
                        modifications = new List<XmlNode>();
                        List<XmlNode> weapons = SaveFileHandle.findChildNodes(ship, "component", "class", "weapon");
                        foreach(XmlNode weapon in weapons)
                        {
                            while (true)
                            {
                                XmlNode mo = SaveFileHandle.findChildNode(weapon, localName, "", "", false);
                                if (mo != null)
                                {
                                    modifications.Add(mo);
                                    break;
                                }
                                weapon.CreateNavigator().AppendChild($"<{localName} ware=\"{mod}\"/>");
                            }
                        }

                        weapons = SaveFileHandle.findChildNodes(ship, "component", "class", "turret");
                        foreach (XmlNode weapon in weapons)
                        {
                            while (true)
                            {
                                XmlNode mo = SaveFileHandle.findChildNode(weapon, localName, "", "", false);
                                if (mo != null)
                                {
                                    modifications.Add(mo);
                                    break;
                                }
                                weapon.CreateNavigator().AppendChild($"<{localName} ware=\"{mod}\"/>");
                            }
                        }

                        weapons = SaveFileHandle.findChildNodes(ship, "component", "class", "missilelauncher");
                        foreach (XmlNode weapon in weapons)
                        {
                            while (true)
                            {
                                XmlNode mo = SaveFileHandle.findChildNode(weapon, localName, "", "", false);
                                if (mo != null)
                                {
                                    modifications.Add(mo);
                                    break;
                                }
                                weapon.CreateNavigator().AppendChild($"<{localName} ware=\"{mod}\"/>");
                            }
                        }
                    }
                    break;
            }
            foreach(XmlNode node in modifications)
            {
                SaveFileHandle.setAttribute(node, a[3], tb.Text);
            }
        }

        private void btn_add_ware_to_ship_Click(object sender, EventArgs e)
        {
            if(CommonDef.IsEmpty(text_ware_for_add.Text))
            {
                saveFileHandleConsole("ware is null", -1);
                return;
            }
            int num = 1;
            try
            {
                num = int.Parse(text_ware_count.Text);
            }
            catch
            {

            }
            //if(num < 0)
            //{
            //    saveFileHandleConsole("Can't drup ware", -1);
            //    return;
            //}
            XmlNode the_ware = null;
            List<XmlNode> list = SaveFileHandle.findChildNodes(ship, "component", "class", "storage");
            if(list.Count == 0)
            {
                saveFileHandleConsole("Can't found storage", -1);
                return;
            }
            do
            {
                List<XmlNode> ware = SaveFileHandle.findChildNodes(list, "ware", "ware", text_ware_for_add.Text);
                if (ware.Count > 0)
                {
                    the_ware = ware[0];
                    break;
                }
                else
                {
                    while (true)
                    {
                        List<XmlNode> cargo = SaveFileHandle.findChildNodes(list, "cargo");
                        if (cargo.Count > 0)
                        {
                            cargo[0].CreateNavigator().AppendChild($"<ware ware=\"{text_ware_for_add.Text}\" amount=\"0\"/>");
                            break;
                        }
                        else
                        {
                            list[0].CreateNavigator().AppendChild($"<cargo/>");
                        }
                    }
                }
            } while (true);
            XmlAttribute amount = null;
            int old_num = 0;
            try
            {
                amount = the_ware.Attributes["amount"];
            }
            catch
            {
                amount = ship.OwnerDocument.CreateAttribute("amount");
                amount.InnerText = "0";
                the_ware.Attributes.Prepend(amount);
            }
            try { old_num = int.Parse(amount.InnerText); }catch{ }
            int new_num = old_num + num;
            if(new_num <= 0)
            {
                the_ware.ParentNode.RemoveChild(the_ware);
            }else if(new_num == 1)
            {
                the_ware.Attributes.Remove(amount);
            }
            else
                amount.InnerText = old_num + num + "";

        }

        private void btn_massacre_ship_Click(object sender, EventArgs e)
        {
            foreach(XmlNode connections in ship.ChildNodes)
            {
                if(connections.LocalName != "connections" )
                {
                    if(connections.LocalName != "control")
                        continue;
                    foreach(XmlNode post in connections.ChildNodes)
                    {
                        if (post.LocalName != "post")
                            continue;
                        foreach(XmlAttribute attr in post.Attributes)
                        {
                            if (attr.LocalName != "component")
                                continue;
                            post.Attributes.Remove(attr);
                            break;
                        }
                    }
                    continue;
                }
                foreach (XmlNode con_cockpit in connections.ChildNodes)
                {
                    if (con_cockpit.LocalName != "connection")
                    {
                        continue;
                    }
                    foreach (XmlNode cockpit in con_cockpit.ChildNodes)
                    {
                        if (cockpit.LocalName != "component")
                        {
                            continue;
                        }
                        foreach(XmlNode cs in cockpit.ChildNodes)
                        {
                            if (cs.LocalName != "connections")
                                continue;
                            LinkedList<XmlNode> ps = new LinkedList<XmlNode>();
                            foreach(XmlNode entities in cs.ChildNodes)
                            {
                                if (entities.LocalName != "connection")
                                    continue;
                                foreach(XmlNode p in entities.ChildNodes)
                                {
                                    if (p.LocalName != "component")
                                        continue;
                                    try
                                    {
                                        if(p.Attributes["class"].InnerText == "npc" || p.Attributes["class"].InnerText == "computer")
                                        {
                                            ps.AddFirst(entities);
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                            foreach(XmlNode p in ps)
                            {
                                cs.RemoveChild(p);
                            }
                        }
                    }

                }
            }
        }

        private void btn_five_star_Click(object sender, EventArgs e)
        {
            EnableControls(false);
            new Thread(five_star).Start();
        }

        private void five_star()
        {
            int i = 0;
            saveFileHandleConsole("", 0);
            foreach (XmlNode people in peoples)
            {
                five_star(people);
                saveFileHandleConsole("level up npc", ++i * 100 / list_npcs.Items.Count);
            }
            //KeyShowItem item = (KeyShowItem)combo_owners_for_npc.SelectedItem;
            //string faction = (string)item.Key;
            XmlNodeList nodes = null;
            string[] ship_types = new string[] { "ship_xl", "ship_l", "ship_m", "ship_s", "ship_xs" };
            foreach (string ship_type in ship_types)
            {
                nodes = handle.selectPath(string.Format("//component[@class='{0}'][@owner='{1}']", ship_type, faction_npc));
                saveFileHandleConsole("level up " + ship_type, 0);
                int n = 0;
                int total = nodes.Count;
                foreach (XmlNode node in nodes)
                {
                    saveFileHandleConsole("level up " + ship_type, n++ * 100 / total);
                    XmlNode people = SaveFileHandle.findChildNode(node, "people", "", "", false);
                    if (people == null)
                        continue;
                    List<XmlNode> persons = SaveFileHandle.findChildNodes(people, "person");
                    foreach(XmlNode person in persons)
                    {
                        SaveFileHandle.setChildNode(person, "skill", "type", "management", new string[] { "value" }, new string[] { "15" },true);
                        SaveFileHandle.setChildNode(person, "skill", "type", "morale", new string[] { "value" }, new string[] { "15" }, true);
                        SaveFileHandle.setChildNode(person, "skill", "type", "piloting", new string[] { "value" }, new string[] { "15" }, true);
                        SaveFileHandle.setChildNode(person, "skill", "type", "engineering", new string[] { "value" }, new string[] { "15" }, true);
                        SaveFileHandle.setChildNode(person, "skill", "type", "boarding", new string[] { "value" }, new string[] { "15" }, true);
                    }
                }
            }

            saveFileHandleConsole("finish", -1);
            EnableControls(true);
        }

        private void five_star(XmlNode people)
        {
            //"management" "morale" "piloting" "engineering" "boarding"
            SaveFileHandle.setSkillValue(people, "management", "15",true);
            SaveFileHandle.setSkillValue(people, "morale", "15", true);
            SaveFileHandle.setSkillValue(people, "piloting", "15", true);
            SaveFileHandle.setSkillValue(people, "engineering", "15", true);
            SaveFileHandle.setSkillValue(people, "boarding", "15", true);
        }

        private void btn_get_the_ship_Click(object sender, EventArgs e)
        {
            KeyShowItem item = (KeyShowItem)combo_owners_for_ship.SelectedItem;
            string faction = (string)item.Key;
            ship.Attributes["owner"].InnerText = "player";

            ship.InnerXml = ship.InnerXml.Replace(string.Format("owner=\"{0}\"", faction), string.Format("owner=\"player\""));

            foreach (XmlNode connections in ship.ChildNodes)
            {
                foreach (XmlNode con_cockpit in connections.ChildNodes)
                {
                    if (con_cockpit.LocalName != "connection")
                    {
                        continue;
                    }
                    foreach (XmlNode cockpit in con_cockpit.ChildNodes)
                    {
                        if (cockpit.LocalName != "component")
                        {
                            continue;
                        }
                        foreach (XmlNode cs in cockpit.ChildNodes)
                        {
                            if (cs.LocalName != "connections")
                                continue;
                            foreach (XmlNode entities in cs.ChildNodes)
                            {
                                if (entities.LocalName != "connection")
                                    continue;
                                foreach (XmlNode p in entities.ChildNodes)
                                {
                                    if (p.LocalName != "component")
                                        continue;
                                    try
                                    {
                                        if (p.Attributes["class"].InnerText != "computer")
                                            continue;
                                        bool hasSeed = false;
                                        foreach (XmlNode entity in p.ChildNodes)
                                        {
                                            if(entity.LocalName == "npcseed")
                                            {
                                                hasSeed = true;
                                                SaveFileHandle.setAttribute(entity, "seed", "476515255");
                                                continue;
                                            }
                                            if (entity.LocalName != "entity")
                                                continue;
                                            string officer = "";
                                            try
                                            {
                                                officer = entity.Attributes["post"].InnerText;
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            if (officer != "aipilot")
                                            {
                                                continue;
                                            }
                                            SaveFileHandle.setAttribute(p, "class", "npc");
                                            SaveFileHandle.setAttribute(p, "macro", "character_argon_female_pilot_06_macro");
                                            SaveFileHandle.setAttribute(p, "name", "AGI");
                                        }
                                        if(!hasSeed)
                                        {
                                            p.CreateNavigator().AppendChild($"<npcseed seed=\"476515255\"/>");
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

        private void btn_onwerless_Click(object sender, EventArgs e)
        {
            KeyShowItem item = (KeyShowItem)combo_owners_for_ship.SelectedItem;
            string faction = (string)item.Key;
            ship.Attributes["owner"].InnerText = "ownerless";

            ship.InnerXml = ship.InnerXml.Replace(string.Format("owner=\"{0}\"", faction), string.Format("owner=\"ownerless\""));
        }

        private void combo_owners_for_station_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_owners_for_station.SelectedItem == null)
            {
                AddItem2ListView(list_stations, null);
                group_station.Enabled = false;
            }
            else
            {
                KeyShowItem item = (KeyShowItem)combo_owners_for_station.SelectedItem;
                faction_station = (string)item.Key;
                group_station.Enabled = false;
                EnableControls(false);
                new Thread(loading_stations).Start();
            }
        }

        private void loading_stations()
        {
            AddItem2ListView(list_stations, null);
            XmlNodeList nodes = null;
            string[] station_types = new string[] { "station" };
            foreach (string station_type in station_types)
            {
                nodes = handle.selectPath(string.Format("//component[@class='{0}'][@owner='{1}']", station_type, faction_station));
                int total = nodes.Count;
                int n = 0;
                saveFileHandleConsole("loading " + station_type, 0);
                foreach (XmlNode node in nodes)
                {
                    string name = "";
                    //string name = handle.getXmlValueText(string.Format("//component[@class='npc'][@owner='{0}'][@id='{1}']", faction, node.InnerText), "name") + node.InnerText;
                    //string id = node.Attributes["id"].InnerText;
                    try
                    {
                        string rename = node.Attributes["name"].InnerText;
                        name += rename;
                    }
                    catch
                    {
                    }

                    try
                    {
                        string code = node.Attributes["code"].InnerText;
                        name += "[" + code + "]";
                    }
                    catch (Exception ex)
                    {
                        saveFileHandleConsole(ex.Message, -1);
                    }
                    //name += id;
                    //try
                    //{
                    //    string newname = node.Attributes["name"].InnerText;
                    //    name += newname;
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    KeyShowListItem listItem = new KeyShowListItem(node, name);
                    AddItem2ListView(list_stations, listItem);
                    saveFileHandleConsole("loading " + station_type, ++n*100/total);
                }
            }
            saveFileHandleConsole("loading finish", -1);
            EnableControls(true);
        }

        XmlNode station = null;

        private void list_stations_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (list_stations.SelectedItems.Count == 0)
            {
                group_station.Enabled = false;
                station = null;
            }
            else
            {
                KeyShowItem item = (KeyShowItem)combo_owners_for_station.SelectedItem;
                string faction = (string)item.Key;
                KeyShowListItem listItem = (KeyShowListItem)list_stations.SelectedItems[0];
                station = (XmlNode)listItem.Key;
                group_station.Enabled = true;
                try
                {

                    text_station_code.Text = station.Attributes["code"].InnerText;
                }
                catch
                {
                    text_station_code.Text = "";
                }
                foreach (XmlNode node in station.ChildNodes)
                {
                    if(node.LocalName == "account")
                    {
                        try
                        {

                            text_station_money.Text = node.Attributes["amount"].InnerText;
                        }
                        catch
                        {
                            text_station_money.Text = "";
                        }
                    }
                }
                //text_ship_class.Text = ship.Attributes["class"].InnerText;
                //text_ship_macro.Text = ship.Attributes["macro"].InnerText;

            }
        }

        private XmlNode locked_station = null;

        private void btn_station_lock_Click(object sender, EventArgs e)
        {
            locked_station = station;
            if (locked_station == null)
                text_locked_station_code.Text = "";
            else
            {
                text_locked_station_code.Text = locked_station.Attributes["code"].InnerText;
            }
        }

        private void btn_station_exchange_Click(object sender, EventArgs e)
        {
            if(locked_station != null && station !=null && locked_station != station)
            {
                EnableControls(false);
                new Thread(exchange_station).Start();
            }
        }

        private void exchange_station()
        {
            saveFileHandleConsole("Start exchange", 0);
            string locked_station_id = locked_station.Attributes["id"].InnerText;
            string target_station_id = station.Attributes["id"].InnerText;
            string locked_owner = locked_station.Attributes["owner"].InnerText;
            string target_owner = station.Attributes["owner"].InnerText;
            {
                XmlNode locked_offset = null;
                XmlNode target_offset = null;

                foreach (XmlNode node in locked_station.ChildNodes)
                {
                    if(node.LocalName == "offset")
                    {
                        locked_offset = node;
                        break;
                    }
                }
                foreach (XmlNode node in station.ChildNodes)
                {
                    if (node.LocalName == "offset")
                    {
                        target_offset = node;
                        break;
                    }
                }

                string temp = locked_offset.InnerXml;
                locked_offset.InnerXml = target_offset.InnerXml;
                target_offset.InnerXml = temp;
            }
            XmlNode locked_storage = null;
            XmlNode target_storage = null;
            {
                XmlNodeList storages = handle.selectPath(string.Format("//component[@class='{0}'][@owner='{1}']", "buildstorage", locked_owner));
                foreach (XmlNode storage in storages)
                {
                    int s = storage.InnerXml.IndexOf("connection=\"buildanchor\" id=\"");
                    int e = storage.InnerXml.IndexOf("\"", s + "connection=\"buildanchor\" id=\"".Length);
                    string id = storage.InnerXml.Substring(s + "connection=\"buildanchor\" id=\"".Length, e - s - "connection=\"buildanchor\" id=\"".Length);
                    if (locked_station.InnerXml.Contains(string.Format("connection=\"{0}\"",id)))
                    {
                        locked_storage = storage;
                    }
                    if (locked_owner == target_owner)
                    {
                        if (station.InnerXml.Contains(string.Format("connection=\"{0}\"", id)))
                        {
                            target_storage = storage;
                        }
                    }
                }
                if (locked_owner != target_owner)
                {
                    storages = handle.selectPath(string.Format("//component[@class='{0}'][@owner='{1}']", "buildstorage", target_owner));
                    foreach (XmlNode storage in storages)
                    {
                        int s = storage.InnerXml.IndexOf("connection=\"buildanchor\" id=\"");
                        int e = storage.InnerXml.IndexOf("\"", s + "connection=\"buildanchor\" id=\"".Length);
                        string id = storage.InnerXml.Substring(s + "connection=\"buildanchor\" id=\"".Length, e - s - "connection=\"buildanchor\" id=\"".Length);
                        if (station.InnerXml.Contains(string.Format("connection=\"{0}\"", id)))
                        {
                            target_storage = storage;
                        }
                    }
                }
            }
            saveFileHandleConsole("Start exchange", 10);

            Dictionary<string, string> attrs = new Dictionary<string, string>();
            foreach (XmlAttribute attr in locked_station.Attributes)
            {
                attrs.Add(attr.LocalName, attr.InnerText);
            }

            saveFileHandleConsole("Start exchange", 20);
            string locked_InnerXml = locked_station.InnerXml;

            locked_station.Attributes.RemoveAll();
            foreach (XmlAttribute attr in station.Attributes)
            {
                XmlAttribute new_attr = locked_station.OwnerDocument.CreateAttribute(attr.LocalName);
                new_attr.InnerText = attr.InnerText;
                locked_station.Attributes.Append(new_attr);
            }

            saveFileHandleConsole("Start exchange", 30);
            locked_station.InnerXml = station.InnerXml;
            saveFileHandleConsole("Start exchange", 40);
            station.InnerXml = locked_InnerXml;
            saveFileHandleConsole("Start exchange", 50);
            station.Attributes.RemoveAll();
            foreach(string attr in attrs.Keys)
            {
                XmlAttribute new_attr = station.OwnerDocument.CreateAttribute(attr);
                new_attr.InnerText = attrs[attr];
                station.Attributes.Append(new_attr);
            }
            saveFileHandleConsole("Exchange buildstorage", 60);
            
            if(locked_storage !=null && target_storage !=null && locked_storage != target_storage)
            {
                XmlNode l_offset = null;
                XmlNode t_offset = null;
                foreach (XmlNode node in locked_storage.ChildNodes)
                {
                    if (node.LocalName == "offset")
                    {
                        l_offset = node;
                        break;
                    }
                }
                foreach (XmlNode node in target_storage.ChildNodes)
                {
                    if (node.LocalName == "offset")
                    {
                        t_offset = node;
                        break;
                    }
                }
                if(l_offset != null && t_offset != null)
                {
                    string temp = l_offset.InnerXml;
                    l_offset.InnerXml = t_offset.InnerXml;
                    t_offset.InnerXml = temp;
                }
                XmlNode locked_par = locked_storage.ParentNode;
                XmlNode target_par = target_storage.ParentNode;
                locked_par.RemoveChild(locked_storage);
                target_par.RemoveChild(target_storage);
                locked_par.PrependChild(target_storage);
                target_par.PrependChild(locked_storage);
                
            }

            saveFileHandleConsole("Finish exchange", -1);

            EnableControls(true);
        }

        private byte[] old_1 = new byte[] {
            0x74,0x05,
            0x83,0xF8,0x03,
            0x75,0x0E,
            0x85,0xDB,
            0x7F,0x0A,
            0xC7,0x05,0x33,0xC4,0x13,0x02,0x04,0x00,0x00,0x00
        };
        private byte[] new_1 = new byte[] {
            0x74,0x05,
            0x83,0xF8,0x03,
            0x75,0x04,
            0x85,0xDB,
            0x90,0x90,
            0xC7,0x05,0x33,0xC4,0x13,0x02,0x00,0x00,0x00,0x00
        };

        private bool[] need_1 = new bool[] {
            true,true,
            true,true,true,
            true,true,
            true,true,
            true,true,
            true,true,false,false,true,true,true,true,true,true
        };

        //private byte[] old_2 = new byte[] {
        //    0xE8,0x96,0x0F,0x00,0x00, //call sub_14064C750
        //    0x85,0xC0, //test eax,eax
        //    0x0F,0x95,0x05,0x1D,0x58,0xEB,0x01 //setnz cs:byte_142500FE0
        //};

        //private byte[] new_2 = new byte[] {
        //    0x90,0x90,0x90,0x31,0xC0, //nop nop nop xor eax,eax
        //    0x85,0xC0, //test eax,eax
        //    0x0F,0x95,0x05,0x1D,0x58,0xEB,0x01 //setnz cs:byte_142500FE0 
        //};

        private byte[] old_2 = new byte[] {
            0x80,0xBB,0x12,0x02,0x00,0x00,0x00, //
            0x74,0x05, //
            0xB8,0x01,0x00,0x00,0x00 //
        };

        private byte[] new_2 = new byte[] {
            0x80,0xBB,0x12,0x02,0x00,0x00,0x00, //
            0x74,0x05, //
            0x90,0x90,0x90,0x90,0x90 //
        };

        private void patch4XMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "X4 (X4.exe)|X4.exe";
            if (openFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
                new_1[17] = (byte)int.Parse(text_patchCode.Text);
                bool patched = false;
                byte[] exe = File.ReadAllBytes(openFileDialog.FileName);
                for(int i=0;i<exe.Length - 16 ;i++)
                {
                    bool found = true;
                    for(int j = 0; j < old_1.Length;j++ )
                    {
                        if(need_1[j])
                        if(exe[i+j] != old_1[j])
                        {
                            found = false;
                            break;
                        }
                    }
                    if(found)
                    {
                        for (int j = 0; j < new_1.Length; j++)
                        {
                            if (need_1[j])
                                exe[i + j] = new_1[j];
                        }
                        patched = true;
                        break;
                    }
                }

                //for (int i = 0; i < exe.Length - 16; i++)
                //{
                //    bool found = true;
                //    for (int j = 0; j < old_2.Length; j++)
                //    {
                //        if (exe[i + j] != old_2[j])
                //        {
                //            found = false;
                //            break;
                //        }
                //    }
                //    if (found)
                //    {
                //        for (int j = 0; j < new_2.Length; j++)
                //        {
                //            exe[i + j] = new_2[j];
                //        }
                //        patched = true;
                //        break;
                //    }
                //}
                if (patched)
                {
                    Directory.CreateDirectory(string.Format("{0}\\Patched", Application.StartupPath));
                    File.WriteAllBytes(string.Format("{0}\\Patched\\X4.exe", Application.StartupPath), exe);
                    saveFileHandleConsole("Patched!", -1);
                }
                else
                    saveFileHandleConsole("Patched error!", -1);
            }
        }

        private void btn_add_ware_to_player_Click(object sender, EventArgs e)
        {
            string id = text_ware_id_for_player.Text;
            int num = 0;
            try
            {
                num = int.Parse(text_ware_count_for_player.Text);
            }catch
            {
                num = 1;
            }
            if (num == 0)
                return;
            XmlNode inventory = SaveFileHandle.findChildNode(player, "inventory", "", "", false);
            if(inventory == null)
            {
                player.CreateNavigator().PrependChild("<inventory />");
                inventory = SaveFileHandle.findChildNode(player, "inventory", "", "", false);
                if (inventory == null)
                    return;
            }
            XmlNode ware = SaveFileHandle.findChildNode(inventory, "ware", "ware", id, false);
            if (ware == null)
            {
                inventory.CreateNavigator().AppendChild($"<ware ware=\"{id}\" />");
                ware = SaveFileHandle.findChildNode(inventory, "ware", "ware", id, false);
                if (ware == null)
                    return;
            }
            else
            {
                try
                {


                    num += int.Parse(ware.Attributes["amount"].InnerText);
                }
                catch
                {
                    num++;
                }
            }
            if(num == 0)
            {
                inventory.RemoveChild(ware);
            }
            else
            {
                SaveFileHandle.setAttribute(ware, "amount", num + "");
            }
        }

        private void SuperBuffer(bool flag)
        {
            foreach(Control control in buffTab.Controls)
            {
                if(control.GetType()==typeof(TextBox))
                {
                    ((TextBox)control).ReadOnly = !flag;
                }
            }
        }
    }
}
