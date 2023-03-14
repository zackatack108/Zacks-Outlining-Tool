using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using Tesseract;

namespace Zacks_Outlining_Tool.Utilities;

public class CalculatePoint
{
    public async Task<string> FindPoint()
    {

        IntPtr hWnd = User32.FindWindow(null, "Ruler");

        ScreenCapture screen = new ScreenCapture();
        var img = screen.CaptureWindow(hWnd);
        Bitmap image = new Bitmap(img);

        int newWidth = 2 * image.Width;
        int newHeight = 2 * image.Height;

        Bitmap resizedImg = new Bitmap(newWidth, newHeight);
        using (Graphics g = Graphics.FromImage(resizedImg))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, new Rectangle(0, 0, newWidth, newHeight));
        }

        string text = null;
        using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
        {
            using(var page = engine.Process(resizedImg, PageSegMode.Auto))
            {
                text = page.GetText();
            }
        }

        if (text == null)
        {
            return "Error: Unable to read values";
        }

        var mapLength = getBetween(text, "Length:", "Meters");
        var heading = getBetween(text, "Heading:", "degrees");

        if(mapLength == null && heading == null)
        {
            return "Error: Unable to read values";
        } 
        else
        {
            mapLength = mapLength.Replace(",", "");
            mapLength = mapLength.Replace("|", "");
            heading = heading.Replace(",", "");

            var rulerData = $"{mapLength} {heading}";

            return rulerData;
        }
    }

    public string getBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }

        return null;
    }

}