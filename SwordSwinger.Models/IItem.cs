using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Models
{
	public interface IItem
	{
		string Name { get; set; }
		int GoldValue { get; set; }

		void Active();
	}
}
