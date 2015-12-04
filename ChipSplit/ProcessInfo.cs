using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSplit
{
	public enum Position
	{
		topLeft, topRight, botLeft, botRight, topCenter, bottomCenter, leftCenter, rightCenter, middle
	}

	[Serializable]
	public class ProcessInfo
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public string Args { get; set; }
		public Position Position { get; set; }
	}
}
