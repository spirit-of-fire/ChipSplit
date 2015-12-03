//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Interop;

//namespace ChipSplit
//{
//	public static class DisableMaximizeButton
//	{
//		[DllImport("user32.dll")]
//		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

//		[DllImport("user32.dll")]
//		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


//		private const int GWL_STYLE = -16;

//		private const int WS_MAXIMIZEBOX = 0x10000; //maximize button
//		private const int WS_MINIMIZEBOX = 0x20000; //minimize button

//		public static void DisableMaximize()
//		{
//			if (Process.GetCurrentProcess().MainWindowHandle == null)
//				throw new InvalidOperationException("The window has not yet been completely initialized");

//			SetWindowLong(Process.GetCurrentProcess().MainWindowHandle, GWL_STYLE, GetWindowLong(Process.GetCurrentProcess().MainWindowHandle, GWL_STYLE) & ~WS_MAXIMIZEBOX);
//		}
//	}
//}
