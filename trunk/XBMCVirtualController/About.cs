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

    public partial class About : PerPixelAlphaForm
    {

        public string skinname = "";
        public string info = "";

        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            labelInfo.Text = skinname + "\n" + info;
            this.Width = XBMCVirtualController.Properties.Resources.credits.Width;
            this.Height = XBMCVirtualController.Properties.Resources.credits.Height;
            SetBitmap(XBMCVirtualController.Properties.Resources.credits);
            
        }

        private void About_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void About_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }

    }

}
