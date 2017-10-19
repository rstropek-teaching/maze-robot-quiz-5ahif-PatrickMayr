using Maze.Library;
using System.Collections.Generic;
using System.Drawing;

namespace Maze.Solver
{
    /// <summary>
    /// Moves a robot from its current position towards the exit of the maze
    /// </summary>
    public class RobotController
    {
        private IRobot robot;
        private bool reachedEnd;
        private HashSet<Point> visitedPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="RobotController"/> class
        /// </summary>
        /// <param name="robot">Robot that is controlled</param>
        public RobotController(IRobot robot)
        {
            // Store robot for later use
            this.robot = robot;
            this.reachedEnd = false;
            this.visitedPoints = new HashSet<Point>();
        }

        /// <summary>
        /// Moves the robot to the exit
        /// </summary>
        /// <remarks>
        /// This function uses methods of the robot that was passed into this class'
        /// constructor. It has to move the robot until the robot's event
        /// <see cref="IRobot.ReachedExit"/> is fired. If the algorithm finds out that
        /// the exit is not reachable, it has to call <see cref="IRobot.HaltAndCatchFire"/>
        /// and exit.
        /// </remarks>
        public void MoveRobotToExit()
        {
            robot.ReachedExit += (_, __) => this.reachedEnd = true;

            this.findExit(0, 0);
            if (this.reachedEnd == false)
            {
                this.robot.HaltAndCatchFire();
            }
        }

        public void findExit(int x, int y)
        {
            //Solution 1: Finish
            if (this.reachedEnd == false && this.visitedPoints.Add(new Point(x, y)))
            {
                //Right
                bool status = this.robot.CanIMove(Direction.Right);
                if (status && this.reachedEnd == false)
                {
                    this.robot.Move(Direction.Right);
                    this.findExit(x + 1, y);
                    if (this.reachedEnd == false)
                    {
                        this.robot.Move(Direction.Left);
                    }
                }

                //Down
                status = this.robot.CanIMove(Direction.Down);
                if (status && this.reachedEnd == false)
                {
                    this.robot.Move(Direction.Down);
                    this.findExit(x, y + 1);
                    if (this.reachedEnd == false)
                    {
                        this.robot.Move(Direction.Up);
                    }
                }

                //Left
                status = this.robot.CanIMove(Direction.Left);
                if (status && this.reachedEnd == false)
                {
                    this.robot.Move(Direction.Left);
                    this.findExit(x - 1, y);
                    if (this.reachedEnd == false)
                    {
                        this.robot.Move(Direction.Right);
                    }
                }

                //Up
                status = this.robot.CanIMove(Direction.Up);
                if (status && this.reachedEnd == false)
                {
                    this.robot.Move(Direction.Up);
                    this.findExit(x, y - 1);
                    if (this.reachedEnd == false)
                    {
                        this.robot.Move(Direction.Down);
                    }
                }
            }
        }

    }
}
