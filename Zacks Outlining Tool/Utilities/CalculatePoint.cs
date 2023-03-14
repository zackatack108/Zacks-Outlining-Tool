using System;
using System.Threading.Tasks;
using IronOcr;

namespace Zacks_Outlining_Tool.Utilities;

public class CalculatePoint
{
    public async Task<string> FindPoint()
    {

        IntPtr hWnd = User32.FindWindow(null, "Ruler");

        ScreenCapture screen = new ScreenCapture();
        var img = screen.CaptureWindow(hWnd);

        OcrResult result = await new IronTesseract().ReadAsync(img);
        var text = result.Text;

        var mapLength = getBetween(text, "Length:", "| Meters");
        var heading = getBetween(text, "Heading:", "degrees");

        if(mapLength == null && heading == null)
        {
            return "Error: Unable to read values";
        } 
        else
        {
            mapLength = mapLength.Replace(",", "");
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
}