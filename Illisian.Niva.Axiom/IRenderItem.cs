using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Core;

namespace Illisian.Niva.AxiomEngine
{
	public interface IRenderItem
	{
		void Initialise(Root item);
		void CreateScene();
		void OnRenderFrame(long timeDelta);
		void OnUnload();
	}
}
