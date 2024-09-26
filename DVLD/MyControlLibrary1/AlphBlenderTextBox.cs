using AlphaUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bank
{
    public partial class AlphBlenderTextBox : AlphaTextBox
    {
        public AlphBlenderTextBox()
        {
            InitializeComponent();
            //this.Cursor = new Cursor("D:\\Projects\\Bank_Project\\Bank\\Bank\\MyCustomTextBox.cs.cur");
        }

        // Configuration for TextBox IsRequired and InputType 
        public bool IsRequired { get; set; }
        public enum InputTypeEnum { TextInput, NumberInput }

        public InputTypeEnum InputType { get; set; } = InputTypeEnum.TextInput;

       
        private bool IsNumeric()
        {
            string s = this.Text.Trim();
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }
            return true;
        }
        public Boolean IsValid()
        {
            if (IsRequired)
            {
                if (this.Text.Trim().Length == 0)
                    return false;
            }
            if (InputType == InputTypeEnum.NumberInput)
            {
                IsNumeric();
            }
            return true;
        }
        // set Configuration about Bored Stale
        // Fields
        private Color boredColor = Color.MediumSlateBlue;
        private int boredSize = 2;
        private bool underlinedStyle = false;

        // properties 
        [Category("My Code Advance")]
        public Color BoredColor
        {
            get => boredColor;
            set
            {
                boredColor = value;
                this.Invalidate();
            }
        }

        [Category("My Code Advance")]
        public int BoredSize
        {
            get => boredSize; set
            {
                boredSize = value;
                this.Invalidate();
            }
        }

        [Category("My Code Advance")]
        public bool UnderlinedStyle
        {
            get => underlinedStyle;
            set
            {
                underlinedStyle = value;
                this.Invalidate();
            }
        }

        //// Overridden Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            // Draw border
            using (Pen penBorder = new Pen( boredColor, boredSize))
            {
                penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

                if (underlinedStyle) // Line Style 
                    graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                else// Normal Style
                    graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }
      
        private void MyCustomTextBox_Enter(object sender, EventArgs e)
        {
            ((AlphaTextBox)sender).Focus();
            Invalidate();

        }
    }
}
