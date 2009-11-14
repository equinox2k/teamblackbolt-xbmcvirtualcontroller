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
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

[System.SerializableAttribute()]
public class skin
{

    private string versionField;
    private string debugField;
    private skinCredits creditsField;
    private skinLayout layoutField;
    private skinControl[] controlsField;

    public string version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    public string debug
    {
        get
        {
            return this.debugField;
        }
        set
        {
            this.debugField = value;
        }
    }

    public skinCredits credits
    {
        get
        {
            return this.creditsField;
        }
        set
        {
            this.creditsField = value;
        }
    }

    public skinLayout layout
    {
        get
        {
            return this.layoutField;
        }
        set
        {
            this.layoutField = value;
        }
    }

    [System.Xml.Serialization.XmlArrayItemAttribute("control")]
    public skinControl[] controls
    {
        get
        {
            return this.controlsField;
        }
        set
        {
            this.controlsField = value;
        }
    }

    public void Save(string FileName)
    {
        Stream stream = File.Open(FileName, FileMode.Create);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(skin));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, null);
        xmlSerializer.Serialize(stream, this);        
        stream.Close();
    }

    public static skin Load(string FileName)
    {
        if (File.Exists(FileName))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(skin));
            StreamReader streamReader = new StreamReader(FileName);
            skin skin = (skin)xmlSerializer.Deserialize(streamReader);
            streamReader.Close();
            return skin;
        }
        else
            return null;
    }

}

[System.SerializableAttribute()]
public partial class skinCredits
{

    private string skinnameField;
    private string infoField;

    public string skinname
    {
        get
        {
            return this.skinnameField;
        }
        set
        {
            this.skinnameField = value;
        }
    }

    public string info
    {
        get
        {
            return this.infoField;
        }
        set
        {
            this.infoField = value;
        }
    }

}

[System.SerializableAttribute()]
public partial class skinLayout
{

    private string widthField;
    private string heightField;
    private string textureField;

    public Image LoadedTexture;

    public string width
    {
        get
        {
            return this.widthField;
        }
        set
        {
            this.widthField = value;
        }
    }

    public string height
    {
        get
        {
            return this.heightField;
        }
        set
        {
            this.heightField = value;
        }
    }

    public string texture
    {
        get
        {
            return this.textureField;
        }
        set
        {
            this.textureField = value;
        }
    }

}

[System.SerializableAttribute()]
public partial class skinControl
{

    private string xposField;
    private string yposField;
    private string widthField;
    private string heightField;
    private string texturehoverField;
    private string textureclickField;
    private string textureField;
    private string buttoncommandField;
    private string typeField;
    private string idField;

    public Image LoadedTexture;
    public Image LoadedTextureHover;
    public Image LoadedTextureClick;

    public string xpos
    {
        get
        {
            return this.xposField;
        }
        set
        {
            this.xposField = value;
        }
    }

    public string ypos
    {
        get
        {
            return this.yposField;
        }
        set
        {
            this.yposField = value;
        }
    }

    public string width
    {
        get
        {
            return this.widthField;
        }
        set
        {
            this.widthField = value;
        }
    }

    public string height
    {
        get
        {
            return this.heightField;
        }
        set
        {
            this.heightField = value;
        }
    }

    public string texturehover
    {
        get
        {
            return this.texturehoverField;
        }
        set
        {
            this.texturehoverField = value;
        }
    }

    public string textureclick
    {
        get
        {
            return this.textureclickField;
        }
        set
        {
            this.textureclickField = value;
        }
    }

    public string texture
    {
        get
        {
            return this.textureField;
        }
        set
        {
            this.textureField = value;
        }
    }

    public string buttoncommand
    {
        get
        {
            return this.buttoncommandField;
        }
        set
        {
            this.buttoncommandField = value;
        }
    }

    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

}

