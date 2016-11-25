/*
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Data;
using System.Drawing; //bitmap
using System.Drawing.Imaging; //bitmap data
using System.Runtime.InteropServices; //marshal copy
using LabelingFramework.Utility;

// import SimpleITK library
using itk.simple;

using System.Diagnostics; // *for testing purposes*


//*********************************************************************************************************//
// converts an image from medical imaging format (*.mhd, *.dcm, ...) to *.png
//*********************************************************************************************************//

namespace LabelingFramework.managementImage
{
    /// <summary>
    /// Summary description for ShowImage
    /// </summary>
    public class ShowImage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)   
        {
            // image ID, brightness and contrast value
            Int32 iID, iBVal, iRot, iNoFiles;
            Double dCVal;
            String sPath;

            // response type
            context.Response.ContentType = "image/png";

            // get and convert image parameters
            if (context.Request.QueryString["NqC3ke"] != null)  // ID
                iID = Convert.ToInt32(context.Request.QueryString["NqC3ke"]);
            else
                throw new ArgumentException("No image id specified");

            if (context.Request.QueryString["tXt9X3"] != null)  // brightness
                iBVal = Convert.ToInt32(context.Request.QueryString["tXt9X3"]);
            else
                throw new ArgumentException("No image brightness specified");

            if (context.Request.QueryString["XwjRGm"] != null)  // contrast
                dCVal = Convert.ToDouble(context.Request.QueryString["XwjRGm"]);
            else
                throw new ArgumentException("No image contrast specified");

            if (context.Request.QueryString["WkYTCe"] != null)  // path
            {

                string obscuredPath = Convert.ToString(context.Request.QueryString["WkYTCe"]);
                string unobscuredPath = "";

                // old obfuscation
                //for (int z = 0; z < obscuredPath.Length; z++)
                //{
                //    int add = Constant.ceasarOdd;

                //    if (z % 2 == 0)
                //    {
                //        add =  Constant.ceasarEven;
                //    }
                //    char c = (char)(obscuredPath[z] - add);
                //    unobscuredPath += c.ToString();

                //}

                // new obfuscation
                string allowedChars = Constant.allowedCharacters;

                for (int p = 0; p < obscuredPath.Length; p++)
                {
                    for (int c = 0; c < allowedChars.Length; c++)
                    {
                        if (obscuredPath[p] == allowedChars[c])
                        { // character is allowed

                            int add = Constant.ceasarOdd;
                            if (p % 2 == 0)
                            {
                                add = Constant.ceasarEven;
                            }

                            // remove unneccessary loops
                            add = add % (allowedChars.Length - 1);

                            int shift = c - add;

                            if (shift > (allowedChars.Length - 1))
                            {
                                shift = shift - (allowedChars.Length - 1);
                            }
                            else if ((shift) < 0)
                            {
                                shift = shift + (allowedChars.Length - 1);
                            }

                            char character = allowedChars[shift];


                            unobscuredPath += character.ToString();

                            break;
                        }
                    }

                }



                //Debug.Print("deobfuscation\t\t" + obscuredPath + "  ->  " + unobscuredPath);

                sPath = unobscuredPath;
            }
            else
                sPath = "";

            if (context.Request.QueryString["Hsfke2"] != null){ // rotate
                iRot = Convert.ToInt32(context.Request.QueryString["Hsfke2"]);
            }
            else
            {
                iRot = 0;
            }

            if (context.Request.QueryString["yAR8st"] != null)  // length
                iNoFiles = Convert.ToInt32(context.Request.QueryString["yAR8st"]);
            else
                iNoFiles = 1;

            // load image in byte array "bmVal"
            //var bOld = false;
            byte[] bmVal;
            int iWidth;
            int iHeight;
            int iDepth;
            itk.simple.Image itkImage;

            if(!File.Exists(sPath)){
                // file was not found

   
            }

            try
            {
                itkImage = SimpleITK.ReadImage(sPath, PixelIDValueEnum.sitkFloat32);
            }
            catch (System.StackOverflowException)
            {
                itk.simple.ImageFileReader reader = new itk.simple.ImageFileReader();
                reader.SetFileName(sPath);
                itkImage = reader.Execute();
            }

            // get image direction
            var iDir = itkImage.GetDirection();

            // get spacing
            VectorDouble dSpacing = itkImage.GetSpacing();
            
            // get size and number of dimension
            VectorUInt32 iSize = itkImage.GetSize();
            int len = 1;
            for (int iDim = 0; iDim < itkImage.GetDimension(); iDim++)
            {
                len *= (int)iSize[iDim];
            }
            iWidth = unchecked((int)iSize[0]);
            iHeight = unchecked((int)iSize[1]);
            iDepth = unchecked((int)iSize[2]);

            // convert mm->dpi
            float fResolutionX = System.Convert.ToSingle(System.Convert.ToDouble(iWidth) * 25.4 / dSpacing[0]);
            float fResolutionY = System.Convert.ToSingle(System.Convert.ToDouble(iHeight) * 25.4 / dSpacing[1]);
      
            // 2D image size
            int iLength = iWidth * iHeight;

            // copy buffer to new array
            IntPtr ipBuffer = itkImage.GetBufferAsFloat();
            float[] fArray = new float[iLength];
                
            // no negative indices
            if (iID < 0)
            {
                iID = 0;
            }

            // multiple DICOM files
            int iOffset = 0;
            if (iNoFiles > 1)
            {
                if (iID >= (iNoFiles-1))
                {
                    iID = iNoFiles-1;
                }
                iOffset = 0;
            }

            else if (iNoFiles == 1)
            {
                // one DICOM file
                if (iDepth == 1)
                {
                    iID = 1;
                    iOffset = 0;
                }

                // one 3D MHD file
                else if (iDepth > 1)
                {
                    if (iID > (iDepth-1))
                    {
                        iID = iDepth-1;
                    }
                    iOffset = (iID - 1) * iLength;
                }
            }              

            // new pointer to data (due to offset)
            Marshal.Copy(new IntPtr(ipBuffer.ToInt64() + sizeof(float) * iOffset), fArray, 0, iLength);

            // convert floating point to 8 bit integer values - range:0 - 255
            float fMax = fArray.Max();
            float fMin = fArray.Min();
            bmVal = new byte[iWidth * iHeight];
            for (int iI = 0; iI < iLength; iI++)
            {
                var tmp = (float)(((fArray[iI] - fMin) / (fMax - fMin) * 255) + iBVal) * (float)dCVal;
                if (tmp > 255) tmp = 255;
                if (tmp < 0) tmp = 0;
                bmVal[iI] = (byte)tmp;
            }
            Bitmap bmOutput = fCreateBitmap(bmVal, new int[] { iWidth, iHeight }, new float[] {fResolutionX, fResolutionY});

            // rotate image and flip in X direction due to different image orientation convention

            // corrects *.mhd orientation

            List<String> lsExtensions = new List<String> { ".mhd", ".MHD" };
            foreach (var ext in lsExtensions)
            {
                if (sPath.EndsWith(ext))
                {                                                // *.mhd files have a different orientation (compared to *.ima)
                    bmOutput.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
            }

            switch (iRot)
            {
                case 0:
                    bmOutput.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 90:
                    bmOutput.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 180:
                    bmOutput.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 270:
                        bmOutput.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                default:
                    break;
            }

            // save new image to response stream
            bmOutput.Save(context.Response.OutputStream, ImageFormat.Png);
            bmOutput.Dispose();
        }

        // convert byte array to 8 bit grayscale bitmap
        protected Bitmap fCreateBitmap(byte[] bArray, int[] iSize, float[] fRes)
        {
            int iWidth = iSize[0];
            int iHeight = iSize[1];

            // create new bitmap
            System.Drawing.Bitmap bmBitmap = new System.Drawing.Bitmap(iWidth, iHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            bmBitmap.SetResolution(fRes[0], fRes[1]);

            // create new bitmap data
            BitmapData bmData = bmBitmap.LockBits(new System.Drawing.Rectangle(0, 0, iWidth, iHeight), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            // copy data
            Marshal.Copy(bArray, 0, bmData.Scan0, (int)(iWidth * iHeight));
            bmBitmap.UnlockBits(bmData);

            // create grayscale color palette
            ColorPalette _palette = bmBitmap.Palette;
            System.Drawing.Color[] _entries = _palette.Entries;
            for (int i = 0; i < 256; i++)
            {
                System.Drawing.Color b = new System.Drawing.Color();
                b = System.Drawing.Color.FromArgb((byte)i, (byte)i, (byte)i);
                _entries[i] = b;
            }
            bmBitmap.Palette = _palette;

            // return bitmap
            return bmBitmap;
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
