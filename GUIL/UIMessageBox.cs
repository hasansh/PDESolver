using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEMProject.GUIL
{
    public partial class UIMessageBox : Form
    {
        public UIMessageBox(string message)
        {
            InitializeComponent();
            this.lblErrorText.Text = message;

        }
    }
}
