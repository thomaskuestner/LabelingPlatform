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
using System.Drawing;
using System.Drawing.Imaging;
using DicomImageViewer;
using System.IO;

namespace LabelingFramework.DicomClass
{



    public class DicomtoPNG
    {
        Bitmap bmp = null;
        byte[] lut8 = new byte[256];
        byte[] lut16 = new byte[65536];
        List<byte> pix8 = new List<byte>();
        List<ushort> pix16 = new List<ushort>();
        List<byte> pix24 = new List<byte>();
        List<byte> pixels8 = new List<byte>();
        List<ushort> pixels16 = new List<ushort>();
        List<byte> pixels24 = new List<byte>();
        int winMin = 0;
        int winMax = 65535;


        public int SaveImage(string Path,string path)
        {
           
            DirectoryInfo d = new DirectoryInfo(Path);
            FileInfo[] files = d.GetFiles();
            int i = 1;
          


            foreach (FileInfo f in files)
            {

                string PathSave = HttpContext.Current.Server.MapPath("~/DicomImage/" + path);

                if (!Directory.Exists(PathSave))
                    Directory.CreateDirectory(PathSave);
                

                Class.File file = new Class.File();
                file.NameFile = f.Name;
                file.TypeFile = f.Extension;
            
            DicomDecoder dd = new DicomDecoder();
            dd.DicomFileName = f.FullName;
            double winCentre = dd.windowCentre;
            double winWidth = dd.windowWidth;
            int samplesPerPixel = dd.samplesPerPixel;
            bool signedImage = dd.signedImage;
            int minPixelValue = 1;
            int maxPixelValue = 0;
            PathSave +=i +".png";


            TypeOfDicomFile typeOfDicomFile = dd.typeofDicomFile;

            int imageWidth = dd.width;
            int imageHeight = dd.height;

            


            bmp = new Bitmap(imageWidth, imageHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            dd.GetPixels8(ref pixels8);
            dd.GetPixels16(ref pixels16);
            dd.GetPixels24(ref pixels24);

            if (pixels8 != null)
            {

                minPixelValue = pixels8.Min();
                maxPixelValue = pixels8.Max();
                // Bug fix dated 24 Aug 2013 - for proper window/level of signed images
                // Thanks to Matias Montroull from Argentina for pointing this out.
                if (dd.signedImage)
                {
                    winCentre -= char.MinValue;
                }

                if (Math.Abs(winWidth) < 0.001)
                {
                    winWidth = maxPixelValue - minPixelValue;
                }

                if ((winCentre == 0) ||
                    (minPixelValue > winCentre) || (maxPixelValue < winCentre))
                {
                    winCentre = (maxPixelValue + minPixelValue) / 2;
                }
                ResetValues(winWidth, winCentre);
                pix8 = pixels8;
                ComputeLookUpTable8();

                CreateImage8(imageWidth, imageHeight);
            }
            else if (pixels16 != null)
            {
                minPixelValue = pixels16.Min();
                maxPixelValue = pixels16.Max();

                // Bug fix dated 24 Aug 2013 - for proper window/level of signed images
                // Thanks to Matias Montroull from Argentina for pointing this out.
                if (dd.signedImage)
                {
                    winCentre -= short.MinValue;
                }

                if (Math.Abs(winWidth) < 0.001)
                {
                    winWidth = maxPixelValue - minPixelValue;
                }

                if ((winCentre == 0) ||
                    (minPixelValue > winCentre) || (maxPixelValue < winCentre))
                {
                    winCentre = (maxPixelValue + minPixelValue) / 2;
                }

                ResetValues(winWidth, winCentre);
                pix16 = pixels16;
                ComputeLookUpTable16();

                CreateImage16(imageWidth, imageHeight);
            }
            else if (pixels24 != null)
            {

                minPixelValue = pixels8.Min();
                maxPixelValue = pixels8.Max();
                // Bug fix dated 24 Aug 2013 - for proper window/level of signed images
                // Thanks to Matias Montroull from Argentina for pointing this out.
                if (dd.signedImage)
                {
                    winCentre -= char.MinValue;
                }

                if (Math.Abs(winWidth) < 0.001)
                {
                    winWidth = maxPixelValue - minPixelValue;
                }

                if ((winCentre == 0) ||
                    (minPixelValue > winCentre) || (maxPixelValue < winCentre))
                {
                    winCentre = (maxPixelValue + minPixelValue) / 2;
                }

                ResetValues(winWidth, winCentre);
                pix24 = pixels24;
                ComputeLookUpTable8();

                CreateImage24(imageWidth, imageHeight);
            }

            i++;
            if (bmp != null)
                bmp.Save(PathSave, ImageFormat.Png);
            }

            return i;
        }

        public int SaveImageReference(string Path, string PathSave,string filename)
        {

            //DirectoryInfo d = new DirectoryInfo(Path);
            FileInfo files = new FileInfo(Path);
            int i = 1;                

                if (!Directory.Exists(PathSave))
                    Directory.CreateDirectory(PathSave);


                Class.File file = new Class.File();
                file.NameFile = files.Name;
                file.TypeFile = files.Extension;

                DicomDecoder dd = new DicomDecoder();
                dd.DicomFileName = files.FullName;
                double winCentre = dd.windowCentre;
                double winWidth = dd.windowWidth;
                int samplesPerPixel = dd.samplesPerPixel;
                bool signedImage = dd.signedImage;
                int minPixelValue = 1;
                int maxPixelValue = 0;
                PathSave += filename + ".png";


                TypeOfDicomFile typeOfDicomFile = dd.typeofDicomFile;

                int imageWidth = dd.width;
                int imageHeight = dd.height;




                bmp = new Bitmap(imageWidth, imageHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                dd.GetPixels8(ref pixels8);
                dd.GetPixels16(ref pixels16);
                dd.GetPixels24(ref pixels24);

                if (pixels8 != null)
                {

                    minPixelValue = pixels8.Min();
                    maxPixelValue = pixels8.Max();
                    // Bug fix dated 24 Aug 2013 - for proper window/level of signed images
                    // Thanks to Matias Montroull from Argentina for pointing this out.
                    if (dd.signedImage)
                    {
                        winCentre -= char.MinValue;
                    }

                    if (Math.Abs(winWidth) < 0.001)
                    {
                        winWidth = maxPixelValue - minPixelValue;
                    }

                    if ((winCentre == 0) ||
                        (minPixelValue > winCentre) || (maxPixelValue < winCentre))
                    {
                        winCentre = (maxPixelValue + minPixelValue) / 2;
                    }
                    ResetValues(winWidth, winCentre);
                    pix8 = pixels8;
                    ComputeLookUpTable8();

                    CreateImage8(imageWidth, imageHeight);
                }
                else if (pixels16 != null)
                {
                    minPixelValue = pixels16.Min();
                    maxPixelValue = pixels16.Max();

                    // Bug fix dated 24 Aug 2013 - for proper window/level of signed images
                    // Thanks to Matias Montroull from Argentina for pointing this out.
                    if (dd.signedImage)
                    {
                        winCentre -= short.MinValue;
                    }

                    if (Math.Abs(winWidth) < 0.001)
                    {
                        winWidth = maxPixelValue - minPixelValue;
                    }

                    if ((winCentre == 0) ||
                        (minPixelValue > winCentre) || (maxPixelValue < winCentre))
                    {
                        winCentre = (maxPixelValue + minPixelValue) / 2;
                    }

                    ResetValues(winWidth, winCentre);
                    pix16 = pixels16;
                    ComputeLookUpTable16();

                    CreateImage16(imageWidth, imageHeight);
                }
                else if (pixels24 != null)
                {

                    minPixelValue = pixels8.Min();
                    maxPixelValue = pixels8.Max();
                    // Bug fix dated 24 Aug 2013 - for proper window/level of signed images
                    // Thanks to Matias Montroull from Argentina for pointing this out.
                    if (dd.signedImage)
                    {
                        winCentre -= char.MinValue;
                    }

                    if (Math.Abs(winWidth) < 0.001)
                    {
                        winWidth = maxPixelValue - minPixelValue;
                    }

                    if ((winCentre == 0) ||
                        (minPixelValue > winCentre) || (maxPixelValue < winCentre))
                    {
                        winCentre = (maxPixelValue + minPixelValue) / 2;
                    }

                    ResetValues(winWidth, winCentre);
                    pix24 = pixels24;
                    ComputeLookUpTable8();

                    CreateImage24(imageWidth, imageHeight);
                }

                i++;
                if (bmp != null)
                    bmp.Save(PathSave, ImageFormat.Png);
            

            return i;
        }
        private void CreateImage8(int imgWidth, int imgHeight)
        {
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, imgWidth, imgHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;

                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;

                    for (j = 0; j < bmd.Width; ++j)
                    {
                        b = lut8[pix8[i * bmd.Width + j]];
                        j1 = j * pixelSize;
                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }
            bmp.UnlockBits(bmd);
        }

        // Create a bitmap on the fly, using 24-bit RGB pixel data
        private void CreateImage24(int imgWidth, int imgHeight)
        {
            {
                int numBytes = imgWidth * imgHeight * 3;
                int j;
                int i, i1;

                BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width,
                    bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

                int width3 = bmd.Width * 3;

                unsafe
                {
                    for (i = 0; i < bmd.Height; ++i)
                    {
                        byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                        i1 = i * bmd.Width * 3;

                        for (j = 0; j < width3; j += 3)
                        {
                            // Windows uses little-endian, so the RGB data is 
                            //  actually stored as BGR
                            row[j + 2] = lut8[pix24[i1 + j]];     // Blue
                            row[j + 1] = lut8[pix24[i1 + j + 1]]; // Green
                            row[j] = lut8[pix24[i1 + j + 2]];     // Red
                        }
                    }
                }
                bmp.UnlockBits(bmd);
            }
        }

        // Create a bitmap on the fly, using 16-bit grayscale pixel data
        private void CreateImage16(int imgWidth, int imgHeight)
        {
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, imgWidth, imgHeight),
               System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;

                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;

                    for (j = 0; j < bmd.Width; ++j)
                    {
                        b = lut16[pix16[i * bmd.Width + j]];
                        j1 = j * pixelSize;
                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }
            bmp.UnlockBits(bmd);
        }

        // We use the linear interpolation method here
        //  Nonlinear methods like sigmoid are also common, but we don't do them here.
        private void ComputeLookUpTable8()
        {
            if (winMax == 0)
                winMax = 255;

            int range = winMax - winMin;
            if (range < 1) range = 1;
            double factor = 255.0 / range;

            for (int i = 0; i < 256; ++i)
            {
                if (i <= winMin)
                    lut8[i] = 0;
                else if (i >= winMax)
                    lut8[i] = 255;
                else
                {
                    lut8[i] = (byte)((i - winMin) * factor);
                }
            }
        }

        // Linear interpolation here too
        private void ComputeLookUpTable16()
        {
            int range = winMax - winMin;
            if (range < 1) range = 1;
            double factor = 255.0 / range;
            int i;

            for (i = 0; i < 65536; ++i)
            {
                if (i <= winMin)
                    lut16[i] = 0;
                else if (i >= winMax)
                    lut16[i] = 255;
                else
                {
                    lut16[i] = (byte)((i - winMin) * factor);
                }
            }
        }

        public void ResetValues(double winWidth, double winCentre)
        {
            winMax = Convert.ToInt32(winCentre + 0.5 * winWidth);
            winMin = Convert.ToInt32(winMax - winWidth);

        }

    }
}