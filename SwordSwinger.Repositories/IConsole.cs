using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordSwinger.Repositories
{
	public interface IConsole
	{
		void WriteLine(string s);
		void Writeline(object o);
		void Write(string s);
		string ReadLine();
		void Clear();
		ConsoleKeyInfo ReadKey();
	}
}
