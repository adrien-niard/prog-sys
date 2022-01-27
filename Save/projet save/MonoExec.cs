using System;
using System.Collections.Generic;
using System.Text;

namespace projet_save
{
	class MonoExec : ASave
	{
		public void TriggerSave(Progression progression) { }

		public void AddSaveListener() { }

		public override void Exe(Save save) 
		{
			//Execution séquentielle
		}
	}
}
