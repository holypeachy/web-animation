using System.Numerics;

namespace Web
{
	class Node
	{
		public Vector2 Position;
		public Node[] Targets;
		public Vector2 MovementDirection;
		
		public Node(Vector2 Position)
		{
			this.Position = Position;
			Targets = new Node[Program.NumberOfLinks];
			MovementDirection = Vector2.Zero;
		}
	}
}