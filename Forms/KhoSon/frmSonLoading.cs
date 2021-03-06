using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSonLoading : _Forms
    {
        public bool _IsManual=true;
        
        public frmSonLoading()
        {
            InitializeComponent();
        }
        public frmSonLoading(bool IsProcess)
        {
            InitializeComponent();
            label1.Text = "Đang xử lí dữ liệu.....";
        }

        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static frmSonLoading splashForm;

        static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.    
            if (splashForm != null) return;
            splashForm = new frmSonLoading();
            Thread thread = new Thread(new ThreadStart(frmSonLoading.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            if (splashForm != null) Application.Run(splashForm);
        }

        static public void CloseForm()
        {
            splashForm?.Invoke(new CloseDelegate(frmSonLoading.CloseFormInternal));
        }

        static private void CloseFormInternal()
        {
            if (splashForm != null)
            {
                splashForm.Close();
                splashForm = null;
            };
        }

        private void frmSonLoading_Load(object sender, EventArgs e)
        {
            //picLoad.Image = Utilities.Properties.Resources.Processing;
        }

        private void frmSonLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_IsManual == true)
                e.Cancel = true;
        }
        public void CloseFormOld()
        {
            _IsManual = false;
            this.Close();
        }


    }
}