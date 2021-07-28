using System;
using System.Drawing;
using System.Windows.Forms;
using X4Tool;

namespace X4ToolControl
{
    public partial class XmlTextBox : UserControl
    {
        public static _GetXmlValueText GetXmlValueText = null;
        public static _SetXmlValueText SetXmlValueText = null;
        public _XmlValueChecked XmlValueChecked = null;
        public _XmlValueChanged XmlValueChanged = null;

        public bool ReadOnly
        {
            get
            {
                return textBox.ReadOnly;
            }
            set
            {
                textBox.ReadOnly = value;
            }
        }

        public bool isRead
        {
            get
            {
                return read;
            }
            set
            {
                read = value;
            }
        }

        private string path;
        private string key;
        private bool read = true;
        public XmlTextBox()
        {
            InitializeComponent();
        }

        private void XmlTextBox_Resize(object sender, EventArgs e)
        {
            textBox.Size = new Size(this.Size.Width, this.Size.Height);
        }

        public void setText(string text)
        {
            if (!base.InvokeRequired)
            {
                textBox.Text = text;
            }
            else
            {
                Invoke(new _SetText(setText), new object[] { text });
            }
        }

        public void setPath(string xmlPath)
        {
            path = xmlPath;
        }

        public void setKey(string key)
        {
            this.key = key;
        }

        public void setKey(string path,string key)
        {
            this.path = path;
            this.key = key;
        }

        public void Read()
        {
            isRead = true;
            setText(getValue());
            isRead = false;
        }

        public void SetTextWithoutWrite(string text)
        {
            isRead = true;
            setText(text);
            isRead = false;
        }

        public void Read(string xmlPath,string key)
        {
            setPath(xmlPath);
            setKey(key);
            Read();
        }

        public string getValue()
        {
            if(GetXmlValueText != null)
            {
                return (string)GetXmlValueText(path, key);
            }
            return "";
        }

        public void setValue(string value)
        {
            if(SetXmlValueText != null)
            {
                bool canSet = true;
                if(XmlValueChecked != null)
                {
                    value = XmlValueChecked(value,out canSet);
                }
                if(canSet)
                    SetXmlValueText(path, key, value);
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!read)
            {
                if(XmlValueChanged != null)
                {
                    bool o = isRead;
                    isRead = true;
                    textBox.Text = XmlValueChanged(textBox.Text);
                    isRead = o;

                }
                setValue(textBox.Text);
                
            }
        }

        private void XmlTextBox_EnabledChanged(object sender, EventArgs e)
        {
            textBox.Enabled = this.Enabled;
        }
    }
}
