using BankBusinessLayer;
using BankBusinessLayer.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyControlLibrary1
{
    public partial class MyCustomComboBox : ComboBox
    {
        public MyCustomComboBox()
        {
            InitializeComponent();

            FillAllCountriesInComboBox();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        DataTable dtCountries = new DataTable();

        private event clsGlobal.CallBackEventHandler<DataTable> _CallBackCountries;
        private void OnGetAllCountries(object sender,DataTable e)
        {
            dtCountries = e;
        }

        private void FillAllCountriesInComboBox()
        {
            _CallBackCountries = OnGetAllCountries;
             clsCountry.GetAllCountries(_CallBackCountries);

            if (dtCountries.Rows.Count > 0) 
            {
                foreach(DataRow row in dtCountries.Rows)
                {
                    this.Items.Add(row[1]);
                }
            }
        }

   
        
    }
}
