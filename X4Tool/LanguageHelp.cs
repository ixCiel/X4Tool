using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace X4Tool
{
    class LanguageHelp
    {
        private static string language_path = Application.StartupPath + "\\resource\\t";
        private static Dictionary<int, XmlDocument> language_files = new Dictionary<int, XmlDocument>();
        public static int languageCode = 86;
        public static string menu_file
        {
            get
            {
                switch(languageCode)
                {
                    case 86:
                        return "文件(&F)";
                }
                return "&File";
            }
        }
        public static string X4Tool
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "X4Tool";
                }
                return "X4Tool";
            }
        }

        public static string menu_open
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "打开(&O)";
                }
                return "&Open";
            }
        }

        public static string menu_save
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "保存(&S)";
                }
                return "&Save";
            }
        }

        public static string menu_close
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "关闭(&C)";
                }
                return "&Close";
            }
        }

        public static string menu_exit
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "退出(&X)";
                }
                return "E&xit";
            }
        }

        public static string menu_language
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "语言(&L)";
                }
                return "&Language";
            }
        }

        public static string menu_zh
        {
            get
            {
                return "简体中文";
            }
        }

        public static string menu_en
        {
            get
            {
                return "English";
            }
        }

        public static string menu_help
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "帮助(&H)";
                }
                return "&Help";
            }
        }

        public static string menu_about
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "关于(&A)";
                }
                return "&About";
            }
        }

        public static string aboutMe
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "关于";
                }
                return "About";
            }
        }

        public static string codedText
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "ixCiel个人制作";
                }
                return "Coded by ixCiel.";
            }
        }

        public static string download
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "下载";
                }
                return "Download";
            }
        }

        public static string close_about
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "确定";
                }
                return "OK";
            }
        }
        public static string infoTab
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "信息";
                }
                return "Information";
            }
        }

        public static string label_name
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "名称";
                }
                return "Name";
            }
        }

        public static string label_boarding
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "登船";
                }
                return "Boarding";
            }
        }

        public static string label_engineering
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "工程";
                }
                return "Engineering";
            }
        }

        public static string label_management
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "管理";
                }
                return "Management";
            }
        }

        public static string label_morale
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "士气";
                }
                return "Morale";
            }
        }

        public static string label_piloting
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "驾驶";
                }
                return "Piloting";
            }
        }

        public static string group_save_file
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "存档信息";
                }
                return "Savefile Information";
            }
        }

        public static string group_player
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "玩家信息";
                }
                return "Player Information";
            }
        }

        public static string label_relation
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "声望";
                }
                return "Reputation";
            }
        }

        public static string label_relation_time
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "时间";
                }
                return "Time";
            }
        }

        public static string check_relation_modify
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "双向修改";
                }
                return "Change both side";
            }
        }

        public static string save_file_name
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "存档名称";
                }
                return "Savefie's name";
            }
        }

        public static string save_time
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "存档时间";
                }
                return "Saved time";
            }
        }

        public static string save_ver
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "游戏版本";
                }
                return "Game version";
            }
        }

        public static string game_time
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "游戏时间";
                }
                return "Game time";
            }
        }

        public static string player_name
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "名称";
                }
                return "name";
                //return getTextFromXml(1001, 2809);
            }
        }

        public static string player_location
        {
            get
            {

                switch (languageCode)
                {
                    case 86:
                        return "当前位置";
                }
                return "Location";
                //return getTextFromXml(1001, 6301);
            }
        }

        public static string player_money
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "信用币";
                }
                return "Credits";
                //return getTextFromXml(1001, 37);
            }
        }

        public static string player_id
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "玩家ID";
                }
                return "Player id";
            }
        }
        public static string tab_relation
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "关系";
                }
                return "Relations";
            }
        }
        public static string tab_ship
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "飞船";
                }
                return "Ship";
            }
        }

        public static string get_the_ship
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "打劫";
                }
                return "Get";
            }
        }

        public static string massacre_ship
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "屠杀";
                }
                return "Massacre";
            }
        }

        public static string tab_station
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "空间站";
                }
                return "Station";
            }
        }

        public static string station_lock
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "锁定";
                }
                return "Lock";
            }
        }

        public static string station_exchange
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "交换";
                }
                return "Exchange";
            }
        }

        public static string label_faction
        {
            get
            {
                switch (languageCode)
                {
                    case 86:
                        return "势力";
                }
                return "Faction";
            }
        }

        public static string notFoundFile
        {
            get
            {
                return "Can't found the file!";
            }
        }
        public static string startLoading
        {
            get
            {
                return "Start loadding savefile...";
            }
        }

        public static string startBackup
        {
            get
            {
                return "Copy savefile...";
            }
        }

        public static string startCompress
        {
            get
            {
                return "Compress savefile...";
            }
        }

        public static string startDecompress
        {
            get
            {
                return "Decompress savefile...";
            }
        }

        public static string loadXml
        {
            get
            {
                return "Loading savefile...";
            }
        }


        public static string loadInformation
        {
            get
            {
                return "Loading information";
            }
        }

        public static string loadRelation
        {
            get
            {
                return "Loading relation";
            }
        }

        public static string loadNPC
        {
            get
            {
                return "Loading relation";
            }
        }

        public static string loadSuccess
        {
            get
            {
                return "Savefile loaded";
            }
        }

        public static string loadFailed
        {
            get
            {
                return "Load Savefile failed!";
            }
        }

        public static string saveSuccess
        {
            get
            {
                return "Savefile saved";
            }
        }

        public static string information
        {
            get
            {
                return "Information";
            }
        }

        public static string getTextFromXml(int page, int t)
        {
            if (!language_files.Keys.Contains(languageCode))
            {
                if (File.Exists(language_path + "\\0001-L0" + languageCode + ".xml"))
                {
                    language_files[languageCode] = new XmlDocument();
                    language_files[languageCode].Load(language_path + "\\0001-L0" + languageCode + ".xml");
                }
            }
            XmlDocument doc = null;
            if (!language_files.Keys.Contains(languageCode) && language_files.Values.Count > 0)
            {
                doc = language_files.Values.First();
            }
            else
            {
                doc = language_files[languageCode];
            }
            if (doc != null)
            {
                try
                {
                    XmlNodeList nodes = doc.SelectNodes(string.Format("/language/page[@id='{0}']/t[@id='{1}']", page, t));
                    if (nodes.Count > 0)
                    {
                        return nodes[0].InnerText;
                    }
                }
                catch
                {
                }
            }
            return "p" + page + "t" + t;
        }

        public static string getFactionName(string id)
        {
            switch(languageCode)
            {
                case 86:
                    {
                        switch (id)
                        {
                            case "player":
                                return "玩家";
                            case "argon":
                                return "Argon联邦";
                            case "antigone":
                                return "安提戈涅共和国";
                            case "paranid":
                                return "Paranid神界";
                            case "holyorder":
                                return "教皇圣教团";
                            case "teladi":
                                return "Teladi公司";
                            case "ministry":
                                return "财政部";
                            case "alliance":
                                return "(Alliance of the Word)约言联盟";
                            case "hatikvah":
                                return "希望之歌自由联盟";
                            case "scaleplate":
                                return "鳞片公约组织";
                            case "xenon":
                                return "Xenon";
                            case "terran":
                                return "地球人托管";
                            case "boron":
                                return "Boron女王国";
                            case "1401":
                                return "扎亚斯家族";
                            case "khaak":
                                return "Kha'ak";
                            case "criminal":
                                return "罪犯";
                            case "10201":
                                return "敌人";
                            case "civilian":
                                return "平民";
                            case "visitor":
                                return "访客";
                            case "smuggler":
                                return "走私犯";
                        }
                    }
                    break;
                default:
                    {
                        switch (id)
                        {
                            case "player":
                                return "Player";
                            case "argon":
                                return "Argon Federation";
                            case "antigone":
                                return "Antigone Republic";
                            case "paranid":
                                return "Godrealm of the Paranid";
                            case "holyorder":
                                return "Holy Order of the Pontifex";
                            case "teladi":
                                return "Teladi Company";
                            case "ministry":
                                return "Ministry of Finance";
                            case "alliance":
                                return "Alliance of the Word";
                            case "hatikvah":
                                return "Hatikvah Free League";
                            case "scaleplate":
                                return "Scale Plate Pact";
                            case "xenon":
                                return "Xenon";
                            case "terran":
                                return "Terran Mandate";
                            case "boron":
                                return "Queendom of Boron";
                            case "1401":
                                return "Family Zyarth";
                            case "khaak":
                                return "Kha'ak";
                            case "criminal":
                                return "Criminal";
                            case "10201":
                                return "Enemy";
                            case "civilian":
                                return "Civilian";
                            case "visitor":
                                return "Visitor";
                            case "smuggler":
                                return "Smuggler";
                        }
                    }
                    break;
            }
            return id;
        }

    }
}
