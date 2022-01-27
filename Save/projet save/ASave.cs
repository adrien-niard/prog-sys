using System;
using System.Collections.Generic;
using System.Text;

namespace projet_save
{
	abstract class ASave
	{
		public ASave()
		{
			//Liste des subscribers
			string[] ISaveSubscriber;
		}

		public void AddSaveListener()
		{

		}

		public void TriggerSave(Progression progression)
		{

		}

		public abstract void Exe(Save save);


	}
}