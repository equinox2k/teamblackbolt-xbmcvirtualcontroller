//
//  Copyright (C) 2009 Team Blackbolt
//  http://www.teamblackbolt.co.uk/
//
//  This Program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2, or (at your option)
//  any later version.
//
//  This Program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XBMCVirtualController
{
    public partial class Connection : Form
    {

        public string Address;
        public int Port;

        public Connection()
        {
            InitializeComponent();
        }

        private void textPort_Validating(object sender, CancelEventArgs e)
        {
            int result;
            if (int.TryParse(textPort.Text, out result)) textPort.Text = "9777";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Address = textAddress.Text;
            int.TryParse(textPort.Text, out Port);
            Properties.Settings.Default.Address = textAddress.Text;
            Properties.Settings.Default.Port = Port;
            Properties.Settings.Default.AutoConnect = checkBoxAutoConnect.Checked;
            Properties.Settings.Default.Save();
        }

        private void Connection_Load(object sender, EventArgs e)
        {
            textAddress.Text = Properties.Settings.Default.Address;
            textPort.Text = Properties.Settings.Default.Port.ToString();
            checkBoxAutoConnect.Checked = Properties.Settings.Default.AutoConnect;
        }

    }
}
