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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace XBMCVirtualController
{

    public class SkinEngine
    {

        private bool initialized = false;
        private string lastButton = "";
        private bool lastHover = false;
        private bool lastShowLed = false;
        private skin skinfile;
        private float scale = 1.0f;
        private PerPixelAlphaForm appPerPixelAlphaForm;

        public SkinEngine(PerPixelAlphaForm AppPerPixelAlphaForm)
        {
            appPerPixelAlphaForm = AppPerPixelAlphaForm;
        }

        public skin Skin
        {
            get
            {
                return this.skinfile;
            }
            set
            {
                this.skinfile = value;
            }
        }

        public float Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
                UpdateScale();
            }
        }

        private string ValidateSkin(skin skinfile, string SkinPath)
        {

            ushort temp;

            //Validate layout section
            if (skinfile.layout.width == null || skinfile.layout.width == "")
                return "layout width is either missing or blank.";
            else if (!ushort.TryParse(skinfile.layout.width, out temp))
                return "layout width should be numeric.";
            else if (skinfile.layout.height == null || skinfile.layout.height == "")
                return "layout height is either missing or blank.";
            else if (!ushort.TryParse(skinfile.layout.height, out temp))
                return "layout height should be numeric.";
            else if (skinfile.layout.texture != null && skinfile.layout.texture != "" && !File.Exists(SkinPath + skinfile.layout.texture))
                return "layout texture does not appear to exist.";

            //Validate controls section
            foreach (skinControl skincontrol in skinfile.controls)
            {
                if (skincontrol.id == null || skincontrol.type == "")
                    return "control type id is either missing or blank.";
                else if (skincontrol.id == null || skincontrol.id == "")
                    return "control '" + skincontrol.type.ToLower() + "' id is either missing or blank.";
                else if (skincontrol.xpos == null || skincontrol.xpos == "")
                    return "control '" + skincontrol.type.ToLower() + "'  xpos for '" + skincontrol.id + "'  is either missing or blank.";
                else if (!ushort.TryParse(skincontrol.xpos, out temp))
                    return "control '" + skincontrol.type.ToLower() + "'  xpos for '" + skincontrol.id + "'  should be numeric.";
                else if (skincontrol.ypos == null || skincontrol.ypos == "")
                    return "control '" + skincontrol.type.ToLower() + "'  ypos for '" + skincontrol.id + "'  is either missing or blank.";
                else if (!ushort.TryParse(skincontrol.ypos, out temp))
                    return "control '" + skincontrol.type.ToLower() + "'  ypos for '" + skincontrol.id + "'  should be numeric.";
                else if (skincontrol.width == null || skincontrol.width == "")
                    return "control '" + skincontrol.type.ToLower() + "'  width for '" + skincontrol.id + "'  is either missing or blank.";
                else if (!ushort.TryParse(skincontrol.width, out temp))
                    return "control '" + skincontrol.type.ToLower() + "'  width for '" + skincontrol.id + "'  should be numeric.";
                else if (skincontrol.height == null || skincontrol.height == "")
                    return "control '" + skincontrol.type.ToLower() + "'  height for '" + skincontrol.id + "'  is either missing or blank.";
                else if (!ushort.TryParse(skincontrol.width, out temp))
                    return "control '" + skincontrol.type.ToLower() + "'  height for '" + skincontrol.id + "'  should be numeric.";
                else if (skincontrol.type.ToLower() == "image" && skincontrol.texture != null && skincontrol.texture != "" && !File.Exists(SkinPath + skincontrol.texture))
                    return "control '" + skincontrol.type.ToLower() + "'  texture for '" + skincontrol.id + "' does not appear to exist.";
                else if (skincontrol.type.ToLower() == "button" && skincontrol.texturehover != null && skincontrol.texturehover != "" && !File.Exists(SkinPath + skincontrol.texturehover))
                    return "control '" + skincontrol.type.ToLower() + "'  texturehover for '" + skincontrol.id + "' does not appear to exist.";
                else if (skincontrol.type.ToLower() == "button" && skincontrol.textureclick != null && skincontrol.textureclick != "" && !File.Exists(SkinPath + skincontrol.textureclick))
                    return "control '" + skincontrol.type.ToLower() + "'  textureclick for '" + skincontrol.id + "' does not appear to exist.";

                if (skinfile.layout.texture != null && skinfile.layout.texture != "")
                    skinfile.layout.LoadedTexture = Image.FromFile(SkinPath + skinfile.layout.texture);
               
                if (skincontrol.type.ToLower() == "image")
                {
                    if (skincontrol.texture != null && skincontrol.texture != "")
                        skincontrol.LoadedTexture = Image.FromFile(SkinPath + skincontrol.texture);
                }

                if (skincontrol.type.ToLower() == "button")
                {
                    if (skincontrol.texturehover != null && skincontrol.texturehover != "")
                        skincontrol.LoadedTextureHover = Image.FromFile(SkinPath + skincontrol.texturehover);
                    if (skincontrol.textureclick != null && skincontrol.textureclick != "")
                        skincontrol.LoadedTextureClick = Image.FromFile(SkinPath + skincontrol.textureclick);
                }

            }

            return "";

        }

        public string LoadSkin(string SkinName)
        {

            try
            {
                skinfile = skin.Load(Helper.SkinPath + SkinName + @"\Skin.xml");
                if (skinfile == null) throw new Exception("Skin '" + SkinName + "' did not appear to exist");
                string errorMessage = ValidateSkin(skinfile, Helper.SkinPath + SkinName + @"\");
                if (errorMessage != "") throw new Exception(errorMessage);
                UpdateScale();
                return "";
            }
            catch (Exception ex)
            {
                return "Error occured opening skin: \n\n" + ex.Message;
            }

        }

        private void UpdateScale()
        {
            initialized = false;
            int width = (int)(int.Parse(skinfile.layout.width) * Scale);
            int height = (int)(int.Parse(skinfile.layout.height) * Scale);
            appPerPixelAlphaForm.Size = new Size(width, height);
            appPerPixelAlphaForm.SetBitmap(RenderSkin(skinfile));
        }

        public Bitmap RenderSkin(skin skinfile)
        {
            return RenderSkin(skinfile, "", false, false);
        }

        public Bitmap RenderSkin(skin skinfile, string buttonid, bool hover, bool showLed)
        {

            if (initialized && lastButton == buttonid && lastHover == hover && lastShowLed == showLed)
                return null;
            
            lastButton = buttonid;
            lastHover = hover;
            lastShowLed = showLed;

            initialized = true;
            
            int renderWidth = int.Parse(skinfile.layout.width);
            int renderHeight = int.Parse(skinfile.layout.height);
            int outputWidth = (int)(int.Parse(skinfile.layout.width) * scale);
            int outputHeight = (int)(int.Parse(skinfile.layout.height) * scale);

            Bitmap renderBitmap = new Bitmap(renderWidth, renderHeight, PixelFormat.Format32bppArgb);
            Bitmap outputBitmap = new Bitmap(outputWidth, outputHeight, PixelFormat.Format32bppArgb);

            Graphics graphicsRender = Graphics.FromImage(renderBitmap);
            graphicsRender.InterpolationMode = InterpolationMode.HighQualityBicubic;

            graphicsRender.DrawImage(skinfile.layout.LoadedTexture, 0, 0, renderWidth, renderHeight);

            foreach (skinControl skincontrol in skinfile.controls)
            {

                int xpos = int.Parse(skincontrol.xpos);
                int ypos = int.Parse(skincontrol.ypos);
                int width = int.Parse(skincontrol.width);
                int height = int.Parse(skincontrol.height);

                if (skincontrol.type.ToLower() == "image")
                {
                    if (showLed && skincontrol.id.ToLower() == "led")
                        graphicsRender.DrawImage(skincontrol.LoadedTexture, xpos, ypos, width, height);
                    else if (skincontrol.id.ToLower() != "led")
                        graphicsRender.DrawImage(skincontrol.LoadedTexture, xpos, ypos, width, height);
                }
                else if (skincontrol.type.ToLower() == "button")
                {
                    if (hover && skincontrol.id == buttonid && skincontrol.LoadedTextureHover != null)
                    {
                        graphicsRender.DrawImage(skincontrol.LoadedTextureHover, xpos, ypos, width, height);
                        break;
                    }
                    else if (!hover && skincontrol.id == buttonid && skincontrol.LoadedTextureClick != null)
                    {
                        graphicsRender.DrawImage(skincontrol.LoadedTextureClick, xpos, ypos, width, height);
                        break;
                    }
                }

            }

            if (skinfile.debug.ToLower() == "true")
            {

                DrawDashedRectangle(graphicsRender, Color.CornflowerBlue, 0, 0, renderWidth, renderHeight);

                foreach (skinControl skincontrol in skinfile.controls)
                {
                    int xpos = int.Parse(skincontrol.xpos);
                    int ypos = int.Parse(skincontrol.ypos);
                    int width = int.Parse(skincontrol.width);
                    int height = int.Parse(skincontrol.height);
                    if (skincontrol.type.ToLower()=="image")
                        DrawDashedRectangle(graphicsRender, Color.Tomato, xpos, ypos, width, height);
                    else
                        DrawDashedRectangle(graphicsRender, Color.White, xpos, ypos, width, height);
                }

            }
            
            Graphics graphicsOutput = Graphics.FromImage(outputBitmap);
            graphicsOutput.DrawImage(renderBitmap, 0, 0, outputWidth, outputHeight);
            return outputBitmap;

        }

        private void DrawDashedRectangle(Graphics graphics,  Color color, int xpos, int ypos, int width, int height)
        {
            graphics.DrawRectangle(Pens.Black, xpos, ypos, width - 1, height - 1);
            Pen pen = new Pen(color);
            pen.DashStyle = DashStyle.Custom;
            pen.DashPattern = new float[] { 2.0f, 2.0f };
            graphics.DrawRectangle(pen, xpos, ypos, width-1, height-1);
        }

    }

}
