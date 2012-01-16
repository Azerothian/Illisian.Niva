using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiom.Core;
using Axiom.Graphics;
using Axiom.Math;

namespace Illisian.Niva.AxiomEngine
{
	public class BasicCube : IRenderItem
	{
		private Root _root;
		private Entity _entity;
		private SceneNode _sceneNode;

		public void Initialise(Axiom.Core.Root item)
		{

			_root = item;
		}

		public void CreateScene()
		{
			_entity = _root.SceneManager.CreateEntity("BasicCube", PrefabEntity.Cube);
			_entity.MaterialName = "Examples/TestImage";
			
			_sceneNode = Root.Instance.SceneManager.RootSceneNode.CreateChildSceneNode();
			_sceneNode.Position = new Vector3(150, 0, -300);
			_sceneNode.AttachObject(_entity);
			_sceneNode.Yaw(45);
		}

		public void OnRenderFrame(long timeDelta)
		{
			_sceneNode.Roll(timeDelta * 0.02f);
		}


		public void OnUnload()
		{

		}


		public void OnUpdateInput(Axiom.Input.InputReader inputReader)
		{

		}
	}
}
