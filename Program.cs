using Timers = System.Timers;
using Raylib_cs;
using System.Timers;
using System.Numerics;

namespace Web
{
	class Program
	{
		const int vx = 1000;
		const int vy = 800;

		const int Margin = 100;
		
		const int FrameTime = 7;
		const int ChangeDirectionTime = 500;

		const int NumberOfNodes = 20; // ! min 3 nodes
		const int NodeRadius = 15;
		static Color NodeColor = Color.GREEN;
		static Color NodeInnerColor = Color.WHITE;
		static Color LineColor = Color.WHITE;
		static Color TextColor = Color.BLACK;


		public static  Node[] NodeArr = new Node[NumberOfNodes];

		static Random rand = new();
		
		public static void Main()
		{
			// Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
			Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
			
			Raylib.InitWindow(vx, vy, "Web Animation");
			// Raylib.SetWindowIcon(Raylib.LoadImage("icon.png"));
			
			Raylib.SetTargetFPS(144);
			
			
			for (int i = 0; i < NodeArr.Length; i++)
			{
				NodeArr[i] = new Node( new Vector2(rand.Next(Margin, vx - Margin), rand.Next(Margin, vy - Margin)));
			}
			
			foreach (Node node in NodeArr)
			{
				while(true)
				{
					Node target = NodeArr[rand.Next(0, NodeArr.Length)];
					// ! Ensures no duplicate connections
					if(!target.Targets.Contains(node) && !node.Targets.Contains(target) && target != node)
					{
						target.Targets.Add(node);
                        node.Targets.Add(target);
						break;
					}
					else if(NodeArr.Length < 3){
						break;
					}
				}

				double x = rand.NextDouble();
				double y = rand.NextDouble();
				x = rand.Next(0, 11) <= 5 ? -x : x;
				y = rand.Next(0, 11) <= 5 ? -y : y;

				node.MovementDirection = new Vector2((float)x, (float)y);
			}			

			Timers.Timer frameTimer = new(FrameTime);
			frameTimer.Elapsed += CalculateNextFrame;
			frameTimer.Start();

			Timers.Timer ChangeDirectionTimer = new(ChangeDirectionTime);
			ChangeDirectionTimer.Elapsed += ChangeDirection;
			ChangeDirectionTimer.Start();
			

			while (!Raylib.WindowShouldClose())
			{
				Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.BLACK);

				DrawLines();
				DrawNodes();
				Raylib.DrawFPS(20, 20);

				Raylib.EndDrawing();
			}

			Raylib.CloseWindow();
		
		}

		private static void CalculateNextFrame(object? sender, ElapsedEventArgs e)
		{
			foreach (Node node in NodeArr)
			{
				node.Position = new Vector2(node.Position.X + node.MovementDirection.X, node.Position.Y + node.MovementDirection.Y);
				if (node.Position.X >= vx - Margin || node.Position.X <= Margin)
				{
					node.MovementDirection = new Vector2(-node.MovementDirection.X, node.MovementDirection.Y);
				}
				if (node.Position.Y >= vy - Margin || node.Position.Y <= Margin)
				{
					node.MovementDirection = new Vector2(node.MovementDirection.X, -node.MovementDirection.Y);
				}
			}
		}

		private static void ChangeDirection(object? sender, ElapsedEventArgs e)
		{
			foreach (Node node in NodeArr)
			{
				double x = rand.NextDouble();
				double y = rand.NextDouble();
				x = rand.Next(1, 11) <= 5 ? -x : x;
				y = rand.Next(1, 11) <= 5 ? -y : y;

				node.MovementDirection = new Vector2((float)x, (float)y);
			}	
		}
		
		private static void DrawLines()
		{
			for (int i = 0; i < NodeArr.Length; i++)
			{
				Node currentNode = NodeArr[i];
				for (int j = 0; j < currentNode.Targets.Count; j++)
				{
					Raylib.DrawLine((int)currentNode.Position.X, (int)currentNode.Position.Y, (int)currentNode.Targets[j].Position.X, (int)currentNode.Targets[j].Position.Y, LineColor);
				}
			}
		}
		
		private static void DrawNodes()
		{
			foreach (Node node in NodeArr)
			{
				Raylib.DrawCircle((int)node.Position.X, (int)node.Position.Y, NodeRadius, NodeInnerColor);
				Raylib.DrawCircleLines((int)node.Position.X, (int)node.Position.Y, NodeRadius, NodeColor);

				Raylib.DrawText(node.Targets.Count + "", (int)node.Position.X, (int)node.Position.Y, 8, TextColor);
			}
		}
	}
}