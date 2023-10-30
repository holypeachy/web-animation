using System.Numerics;

namespace Web
{
	class Node
	{
		public Vector2 Position;
		public List<Node> Targets;
		public Vector2 MovementDirection;

		public Node(Vector2 Position)
		{
			this.Position = Position;
			Targets = new List<Node>();
			MovementDirection = Vector2.Zero;
		}
	}
}