using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace X4Tool
{

    class SaveFileHandle : CommonDef
    {
        private string realFilePath;
        //private string fileName;
        private static string backupPath = Application.StartupPath + "\\Backup";
        private static string workingPath = Application.StartupPath + "\\Working";
        private string xmlFilePath = workingPath + "\\save.xml";
        //private byte[] signature;
        public ConsoleOut consoleUpdate;
        private XmlDocument xml;
        public string player_id = "";

        MemoryStream inStream = new MemoryStream();

        public SaveFileHandle(string filePath)
        {
            realFilePath = filePath;
            
        }

        public bool Loadding()
        {
            if (!File.Exists(realFilePath))
            {
                consoleUpdate(LanguageHelp.notFoundFile, -1);
                return false;
            }

            //Directory.CreateDirectory(backupPath);
            //File.Copy(realFilePath, backupPath + "\\" + fileName +"_" +DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_")+".gz");

            //Directory.CreateDirectory(workingPath);
            consoleUpdate(LanguageHelp.startDecompress, 1);
            //if (File.Exists(xmlFilePath))
            //    File.Delete(xmlFilePath);
            {
                try
                {
                    using (GZipInputStream zipFile = new GZipInputStream(File.OpenRead(realFilePath)))
                    {
                        //using (FileStream destFile = File.Open(xmlFilePath, FileMode.CreateNew))
                        {
                            int buffersize = 2048;
                            byte[] FileData = new byte[buffersize];
                            while (buffersize > 0)
                            {
                                buffersize = zipFile.Read(FileData, 0, buffersize);
                                if (buffersize > 0)
                                    //destFile.Write(FileData, 0, buffersize);
                                    inStream.Write(FileData, 0, buffersize);
                            }
                        }
                    }
                    consoleUpdate(LanguageHelp.loadXml, 5);
                    xml = new XmlDocument();
                    inStream.Position = 0;
                    xml.Load(inStream);
                    inStream = new MemoryStream();
                    string xpath = string.Format("//account[@amount='{0}']/@id", getXmlValueText("/savegame/info/player", "money"));
                    XmlNodeList nodes = xml.SelectNodes(xpath);
                    if (nodes.Count > 0)
                    {
                        player_id = nodes[0].InnerText;
                    }
                    else
                        player_id = "";
                    //string sign = xml.SelectSingleNode(string.Format("/savegame/signature")).InnerText;
                    //signature = Convert.FromBase64String(sign);
                }
                catch (Exception ex)
                {
                    consoleUpdate(LanguageHelp.loadFailed + ex.Message, -1);
                    
                    return false;
                }
            }


            return true;
        }

        public void save()
        {
            save(realFilePath);
        }

        public void save(string savePath)
        {
            consoleUpdate(LanguageHelp.startBackup, 1);
            int n = savePath.LastIndexOf("\\");
            string saveFileName = savePath.Substring(n + 1).Replace(".gz", "");
            if (File.Exists(savePath))
            {
                Directory.CreateDirectory(backupPath);
                File.Copy(realFilePath, backupPath + "\\" + saveFileName + "_" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_") + ".gz");
            }

            xml.PreserveWhitespace = true;
            //Directory.CreateDirectory(workingPath);
            //if (File.Exists(xmlFilePath))
            //    File.Delete(xmlFilePath);
            string text;
            {
                MemoryStream sStream = new MemoryStream();
                xml.Save(sStream);
                sStream.Position = 0;
                StreamReader reader = new StreamReader(sStream);
                text = reader.ReadToEnd();
            }

            //text = File.ReadAllText(xmlFilePath);
            //if (File.Exists(xmlFilePath))
            //    File.Delete(xmlFilePath);
            text = text.Replace("\"us-ascii\"?", "\"UTF-8\"?")
                .Replace(" />", "/>")
                .Replace("><", ">\n<")
                .Replace("?xml version=\"1.0\" encoding=\"iso-8859-1\"", "?xml version=&quot;1.0&quot; encoding=&quot;iso-8859-1&quot;")
                .Replace("</signature>\n</savegame>", "</signature></savegame>\n");
            MemoryStream outStream = new MemoryStream();
            //inStream.Position = 0;
            //do
            //{
            //    int b = inStream.ReadByte();
            //    if (b == 60)
            //        break;
            //    outStream.WriteByte((byte)b);
            //} while (true);
            byte[] byteArray = Encoding.UTF8.GetBytes(text);
            outStream.Write(byteArray, 0, byteArray.Length);
            //if(false)
            //{
            //    inStream.Position = 0;
            //    outStream.Position = 0;
            //    LinkedList<byte> inBuffer = new LinkedList<byte>();
            //    LinkedList<byte> outBuffer = new LinkedList<byte>();
            //    for (int i = 0; i < outStream.Length; i++)
            //    {
            //        int ol = inStream.ReadByte();
            //        int nl = outStream.ReadByte();
            //        inBuffer.AddLast((byte)ol);
            //        outBuffer.AddLast((byte)nl);
            //        if (inBuffer.Count > 20)
            //        {
            //            inBuffer.RemoveFirst();
            //            outBuffer.RemoveFirst();
            //        }
            //        if (ol != nl)
            //        {
            //            int j = 0;
            //            do
            //            {
            //                inBuffer.AddLast((byte)inStream.ReadByte());
            //                outBuffer.AddLast((byte)outStream.ReadByte());
            //            } while (++j < 30);
            //            string intext = Encoding.UTF8.GetString(inBuffer.ToArray());
            //            string outtext = Encoding.UTF8.GetString(outBuffer.ToArray());

            //        }
            //    }
            //}


            outStream.Position = 0;
            if (File.Exists(realFilePath))
                File.Delete(realFilePath);
            int buffersize = 2048;
            long total = outStream.Length / buffersize + 1;
            long done = 0;
            consoleUpdate(LanguageHelp.startCompress, 10);
            using (GZipOutputStream zipFile = new GZipOutputStream(File.OpenWrite(realFilePath)))
            {
                {
                    byte[] FileData = new byte[buffersize];
                    while (buffersize > 0)
                    {
                        buffersize = outStream.Read(FileData, 0, buffersize);
                        if (buffersize > 0)
                        {
                            zipFile.Write(FileData, 0, buffersize);
                        }
                        consoleUpdate(LanguageHelp.startCompress, (int)(++done * 90 / total + 10));
                    }
                }
            }
            consoleUpdate(LanguageHelp.saveSuccess, -1);
        }

        public void setAccountAmount(string value)
        {
            if (IsEmpty(player_id))
                return;
            string xpath = string.Format("//account[@id='{0}']/@amount", player_id);
            XmlNodeList nodes = xml.SelectNodes(xpath);
            foreach(XmlNode node in nodes)
            {
                node.InnerText = value;
            }
        }

        public string getXmlValueText(string path,string key)
        {
            XmlNodeList listNodes = null;
            if(IsEmpty(key))
                listNodes= xml.DocumentElement.SelectNodes(path);
            else
                listNodes = xml.DocumentElement.SelectNodes(path + "/@" + key);
            if (listNodes.Count > 0)
            {
                return listNodes[0].InnerText;
            }
            return "";
        }

        public void setXmlValueText(string path,string key,string value)
        {
            XmlNodeList listNodes = null;
            if (IsEmpty(key))
                listNodes = xml.DocumentElement.SelectNodes(path);
            else
                listNodes = xml.DocumentElement.SelectNodes(path + "/@" + key);
            if (listNodes.Count > 0)
            {
                listNodes[0].InnerText = value;
            }
        }

        public LinkedList<string> getFactions()
        {
            LinkedList<string> factions = new LinkedList<string>();
            //string xpath = string.Format();
            XmlNodeList nodes = xml.SelectNodes("/savegame/universe/factions/faction/@id");
            foreach(XmlNode node in nodes)
            {
                if(!node.InnerText.StartsWith("visitor"))
                    factions.AddLast(node.InnerText);
            }
            return factions;
        }

        public XmlNodeList selectPath(string path)
        {
            return xml.SelectNodes(path);
        }

        public XmlNode selectNode(string path)
        {
            return xml.SelectSingleNode(path);
        }

        public static XmlNode setChildNode(XmlNode node, string localname, string attrName = "", string attrValue = "", string[] attrNameModify = null, string[] attrValueModify = null, bool notlowwer = false)
        {
            XmlNode child = null;
            child = findChildNode(node, localname, attrName, attrValue, false);
            if(child == null)
            {
                child = node.OwnerDocument.CreateNode(XmlNodeType.Element, localname, null);
                if(!IsEmpty(attrName))
                {
                    XmlAttribute attr = child.OwnerDocument.CreateAttribute(attrName);
                    if (!IsEmpty(attrValue))
                        attr.InnerText = attrValue;
                    child.Attributes.Append(attr);
                }
            }
            if (attrNameModify == null)
                return child;
            for(int i=0;i<attrNameModify.Length;i++)
            {
                XmlAttribute attr = null;
                try
                {
                    attr = child.Attributes[attrNameModify[i]];
                }catch
                {
                    attr = null;
                }

                if(attr == null)
                {
                    attr = child.OwnerDocument.CreateAttribute(attrNameModify[i]);
                    child.Attributes.Append(attr);
                }

                if (attrValueModify != null && attrValueModify.Length > i)
                {
                    if(notlowwer)
                    {
                        int n = -1;
                        try
                        {
                            n = int.Parse(attr.InnerText);
                            if (n > int.Parse(attrValueModify[i]))
                                continue;
                        }catch
                        {

                        }
                    }
                    attr.InnerText = attrValueModify[i];
                }
            }
            return child;
        }

        public static void setSkillValue(XmlNode npc, string skill, string value, bool notlowwer = false)
        {
            if (npc != null && !IsEmpty(value))
            {
                XPathNavigator nav = npc.CreateNavigator();
                XPathNodeIterator iter_a = nav.SelectChildren("traits", "");
                while (iter_a.MoveNext())
                {
                    XPathNodeIterator iter = iter_a.Current.Clone().SelectChildren("skill", "");
                    while (iter.MoveNext())
                    {
                        string type = iter.Current.GetAttribute("type", "");
                        XPathNavigator xPathNavigator = iter.Current.Clone();
                        if (type == skill)
                        {
                            if (!xPathNavigator.MoveToAttribute("value", "") && !"0".Equals(value))
                            {
                                xPathNavigator.CreateAttribute("", "value", "", "");
                                xPathNavigator.MoveToAttribute("value", "");
                            }
                            //else
                            {
                                int v = 0;
                                try
                                {
                                    v = int.Parse(xPathNavigator.Value);
                                    if(notlowwer && v > int.Parse(value))
                                    {
                                        return;
                                    }
                                }
                                catch
                                {

                                }
                                xPathNavigator.SetValue(value);
                            }
                            return;
                        }
                    }
                    iter_a.Current.AppendChild($"<skill type=\"{skill}\"/>");
                    iter = iter_a.Current.SelectChildren("skill", "");
                    while (iter.MoveNext())
                    {
                        string type = iter.Current.GetAttribute("type", "");
                        XPathNavigator xPathNavigator = iter.Current.Clone();
                        if (type == skill)
                        {
                            if (!xPathNavigator.MoveToAttribute("value", "") && !"0".Equals(value))
                            {
                                xPathNavigator.CreateAttribute("", "value", "", "");
                                xPathNavigator.MoveToAttribute("value", "");
                            }
                            //else
                            {
                                xPathNavigator.SetValue(value);
                            }
                            return;
                        }
                    }
                }
            }
        }

        public static void setAttribute(XmlNode node,string name,string value)
        {
            try
            {
                node.Attributes[name].InnerText = value;
            }
            catch
            {
                XmlAttribute new_attr = node.OwnerDocument.CreateAttribute(name);
                new_attr.InnerText = value;
                node.Attributes.Append(new_attr);
            }
        }

        public static XmlNode findChildNode(XmlNode parent, string localname, string attrName = "", string attrValue = "", bool childchild = true)
        {
            List<XmlNode> list = findChildNodes(parent, localname, attrName, attrValue, childchild, true);
            if (list.Count > 0)
                return list[0];
            return null;
        }

        public static XmlNode findChildNode(List<XmlNode> parents, string localname, string attrName = "", string attrValue = "", bool childchild = true)
        {
            List<XmlNode> list = findChildNodes(parents, localname, attrName, attrValue, childchild, true);
            if (list.Count > 0)
                return list[0];
            return null;
        }

        public static List<XmlNode> findChildNodes(XmlNode parent, string localname, string attrName = "", string attrValue = "", bool childchild = true,bool single = false)
        {
            List<XmlNode> list = new List<XmlNode>();
            
            findChildNodes(parent, list, localname, attrName, attrValue,childchild, single);

            return list;
        }

        public static List<XmlNode> findChildNodes(List<XmlNode> parents, string localname, string attrName = "", string attrValue = "", bool childchild = true, bool single = false)
        {
            List<XmlNode> list = new List<XmlNode>();
            foreach(XmlNode parent in parents)
                findChildNodes(parent, list, localname, attrName, attrValue, childchild, single);

            return list;
        }

        public static void findChildNodes(XmlNode parent, List<XmlNode> list, string localname, string attrName = "", string attrValue = "", bool childchild = true, bool single = false)
        {
            foreach (XmlNode node in parent.ChildNodes)
            {
                if ((IsEmpty(localname) || node.LocalName == localname))
                {
                    if (!IsEmpty(attrName))
                    {
                        try
                        {
                            string value = node.Attributes[attrName].InnerText;
                            if (!IsEmpty(attrValue) && value != attrValue)
                                continue;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    list.Add(node);
                    if (single)
                        return;
                }
                if(childchild)
                    findChildNodes(node, list, localname, attrName, attrValue,childchild,single);
            }
        }
    }
}
