using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoritoDesktop2
{
	[Serializable]
	public class processes
	{
		public string name { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string path { get; set; }
		public string args { get; set; }
	}
}
