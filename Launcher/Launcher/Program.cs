using System;
using Game;

namespace Launcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game.Pong.Pong p = new Game.Pong.Pong())
            {
                p.Run();
            }
        }
    }
}

