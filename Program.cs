using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker
{
	internal class Program
	{
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

		static void Main(string[] args)
		{
			System.Windows.Forms.Keys keybind = System.Windows.Forms.Keys.XButton2;
			int delay = 100;
			
			if (!File.Exists("config.cfg"))
			{
				File.WriteAllText("config.cfg", $"key:{keybind}\ninterval:{delay}");
			}
			else
			{
				string[] lines = File.ReadAllLines("config.cfg");
				foreach (string line in lines)
				{
					if (line.StartsWith("key:"))
					{
						keybind = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), line.Substring(4));
					}
					else if (line.StartsWith("interval:"))
					{
						delay = int.Parse(line.Substring(9));
					}
				}
			}

			Console.WriteLine("Welcome to mrfacysus AutoClicker!");
			Console.WriteLine("Page UP to Increase Delay");
			Console.WriteLine("Page DOWN to Decrease Delay");

			while (true)
			{
				if (GetAsyncKeyState(keybind) != 0)
				{
					mouse_event(0x0002, 0, 0, 0, 0);
					System.Threading.Thread.Sleep(delay);
					mouse_event(0x0004, 0, 0, 0, 0);
				}
				if (GetAsyncKeyState(System.Windows.Forms.Keys.PageDown) != 0)
				{
					delay -= 10;
					System.Threading.Thread.Sleep(100);
				}
				if (GetAsyncKeyState(System.Windows.Forms.Keys.PageUp) != 0)
				{
					delay += 10;
					System.Threading.Thread.Sleep(100);
				}
			}
		}
	}
}
