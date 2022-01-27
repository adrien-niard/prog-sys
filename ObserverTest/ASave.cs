using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ObserverTest
{
	abstract class ASave
	{
		public abstract void ExecFull(List<Save> SList);

		public abstract void ExecDiff(List<Save> SList);
	}
}
