using Timers = System.Timers;
using Raylib_cs;
using System.Timers;
using System.Numerics;

namespace Web
{
	class Program
	{
		const int FrameTime = 7;
		const int ChangeDirectionTime = 200;
		public const int NumberOfLinks = 2;

		static Vector2 pos = new Vector2(400, 200);
		static Vector2 vel = new Vector2(1, 1);

		static Random ran = new();
		
		public static void Main()
		{
			Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
			Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
			
			Raylib.InitWindow(800, 480, "Hello World");
			// Raylib.SetWindowIcon(Raylib.LoadImage("icon.png"));
			
			Raylib.SetTargetFPS(144);

			Timers.Timer frameTimer = new(FrameTime);
			frameTimer.Elapsed += CalculateNextFrame;
			frameTimer.Start();

			Timers.Timer ChangeDirectionTimer = new(ChangeDirectionTime);
			ChangeDirectionTimer.Elapsed += ChangeDirection;
			ChangeDirectionTimer.Start();

			while (!Raylib.WindowShouldClose())
			{
				Raylib.BeginDrawing();
				Raylib.ClearBackground(Color.WHITE);
				
				Raylib.DrawText("Hello, world!", (int)pos.X, (int)pos.Y, 20, Color.BLACK);
				
				Raylib.EndDrawing();
			}

			Raylib.CloseWindow();
		
		}

		private static void CalculateNextFrame(object? sender, ElapsedEventArgs e)
		{
			pos = new Vector2(pos.X + vel.X, pos.Y + vel.Y);
		}

		private static void ChangeDirection(object? sender, ElapsedEventArgs e)
		{
			double x = ran.NextDouble();
			x = ran.Next(0, 11) <= 5 ? -x : x;
			double y = x == 0 ? 1 : ran.NextDouble();
			y = ran.Next(0, 11) <= 5 ? -y : y;
			vel = new Vector2((float)x, (float)y);
		}
	}
}