using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChipSplit
{
	public enum Position
	{
		[Description("Top Left")]
		topLeft,
		[Description("Top Right")]
		topRight,
		[Description("Bottom Left")]
		botLeft,
		[Description("Bottom Right")]
		botRight,
		[Description("Top Center")]
		topCenter,
		[Description("Bottom Center")]
		botCenter,
		[Description("Left Center")]
		leftCenter,
		[Description("Right Center")]
		rightCenter,
		[Description("Middle")]
		middle,
	}

	[Serializable]
	public class ProcessInfo
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public string Args { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public Position WindowPosition { get; set; }


		public static string GetEnumDescription(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes =
				(DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
				return attributes[0].Description;
			else
				return value.ToString();
		}

		//usage: GetEnumDescription(Position.botCenter);
	}
}
