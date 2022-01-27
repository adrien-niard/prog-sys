using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
	abstract class ASave
	{
		public abstract void ExeComplet(List<Save> SList);

		public abstract void ExeDiff(List<Save> SList);
	}
}
