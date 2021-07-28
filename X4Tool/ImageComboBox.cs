using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using X4Tool;

namespace X4ToolControl
{
    public class KeyShowItem
    {
        private object _key = null;
        private string _show="";
        public KeyShowItem(object key, string show)
        {
            _key = key;
            _show = show;
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
        public override string ToString()
        {
            return ShowText;
        }
    }
    public class ImageKeyShowItem : KeyShowItem
    {
        private string _default = "";
        private string _image = "";
        public ImageKeyShowItem(string key,string show,string image, string d=""):base(key,show)
        {
            _default = d;
            _image = image;
        }

        public string Default
        {
            get
            {
                return _default;
            }
        }

        public string Image
        {
            get
            {
                return _image;
            }
        }
    }
    public class ImageComboBox:ComboBox
    {
        public ImageComboBox()
        {
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (e.Index >= 0)
            {
                if ((e.State & DrawItemState.Selected) != 0)
                {
                    LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(255, 251, 237), Color.FromArgb(255, 236, 181), LinearGradientMode.Vertical);
                    Rectangle rect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);
                    e.Graphics.FillRectangle(brush, rect);
                    Pen pen = new Pen(Color.FromArgb(229, 195, 101));
                    e.Graphics.DrawRectangle(pen, rect);
                }
                else
                {
                    SolidBrush brush2 = new SolidBrush(Color.FromArgb(255, 255, 255));
                    e.Graphics.FillRectangle(brush2, e.Bounds);
                }
                Image image = null;
                Rectangle r;
                if (base.Items[e.Index].GetType() != typeof(ImageKeyShowItem))
                {
                    r = new Rectangle(6, e.Bounds.Y + 3, e.Bounds.Width, e.Bounds.Height - 2);
                }
                else
                {
                    image = ImageHandle.getImage(((ImageKeyShowItem)base.Items[e.Index]).Image, ((ImageKeyShowItem)base.Items[e.Index]).Default);
                    Rectangle rect2 = new Rectangle(6, e.Bounds.Y + 3, image.Width * base.ItemHeight / image.Height, base.ItemHeight);
                    e.Graphics.DrawImage(image, rect2);
                    r = new Rectangle(rect2.Right + 2, rect2.Y, e.Bounds.Width - rect2.Width, e.Bounds.Height - 2);
                }
                string s = base.Items[e.Index].ToString();
                StringFormat stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(s, new Font("微软雅黑", 10f), Brushes.Black, r, stringFormat);
            }
        }
    }
}
