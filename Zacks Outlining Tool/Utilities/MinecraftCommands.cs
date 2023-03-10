using System;
using System.Windows.Forms;

namespace Zacks_Outlining_Tool.Utilities
{
    public class MinecraftCommands
    {

        public string SendPointCommand(string origin, string rulerData)
        {
            IntPtr minecraftWindow = User32.FindWindow("GLFW30", null);
            User32.SetForegroundWindow(minecraftWindow);
            //var minecraft = User32.GetFocus();

            string command = $"/point plot {origin} {rulerData}";

            SendKeys.SendWait($"{command}");
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait("t");

            return command;
        }

        public string ConnectPoints()
        {
            IntPtr minecraftWindow = User32.FindWindow("GLFW30", null);
            User32.SetForegroundWindow(minecraftWindow);
            //var minecraft = User32.GetFocus();

            string command = "/point connect";

            SendKeys.SendWait($"{command}");
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait("t");

            return command;
        }

    }
}
