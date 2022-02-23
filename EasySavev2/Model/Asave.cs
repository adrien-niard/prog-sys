using System;
using System.Collections.Generic;
using System.Text;

namespace EasySavev2.Model
{
	abstract class ASave
	{
		public abstract void ExecFull(List<Save> SList, Log log, State state, int NbObj, int i);

		public abstract void ExecDiff(List<Save> SList, Log log, State state, int NbObj, int i);
	}
}
