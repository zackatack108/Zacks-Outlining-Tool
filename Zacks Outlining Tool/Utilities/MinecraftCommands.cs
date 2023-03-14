using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zacks_Outlining_Tool.Utilities;

public class MinecraftCommands
{

    public async Task<string> SendPointCommand(string origin, string rulerData)
    {
        IntPtr minecraftWindow = User32.FindWindow("GLFW30", null);
        if (minecraftWindow == IntPtr.Zero)
        {
            return "Error: Unable to find Minecraft window";
        }
        User32.SetForegroundWindow(minecraftWindow);
            //var minecraft = User32.GetFocus();

        string command = $"/point plot {origin} {rulerData}";

        await Task.Delay(50);
        SendKeys.SendWait($"{command}");
        SendKeys.SendWait("{ENTER}");
        await Task.Delay(50);
        SendKeys.SendWait("t");

        return command;
    }

    public async Task<string> ConnectPoints()
    {
        IntPtr minecraftWindow = User32.FindWindow("GLFW30", null);
        if (minecraftWindow == IntPtr.Zero)
        {
            return "Error: Unable to find Minecraft window";
        }
        User32.SetForegroundWindow(minecraftWindow);
            //var minecraft = User32.GetFocus();

        string command = "/point connect";

        await Task.Delay(50);
        SendKeys.SendWait($"{command}");
        SendKeys.SendWait("{ENTER}");
        await Task.Delay(50);
        SendKeys.SendWait("t");

        return command;
    }

}
}
