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
using System.IO;
using System.Windows.Forms;

namespace XBMCVirtualController
{

    public static class Helper
    {

        public static string SkinPath
        {
            get
            {
                return AppPath + @"Skins\";
            }
        }

        public static string AppPath
        {
            get
            {
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                if (!appPath.EndsWith(@"\")) appPath += @"\";
                return appPath;
            }
        }

    }

}
