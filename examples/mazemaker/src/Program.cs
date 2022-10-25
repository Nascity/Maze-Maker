using MazeMaker;
using static System.Console;

namespace Showcase
{
	public class Program
	{
		static void Main(string[] args)
		{
			try
			{
				WriteLine(new Maze(int.Parse(args[0]), int.Parse(args[1])).Generate());
			}
			catch (Exception e)
			{
				WriteLine(e.Message);
			}
		}
	}
}