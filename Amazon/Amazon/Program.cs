using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Amazon
{
    public class Program
    {
        private static int _numRows;
        private static int _numColumns;
        private List<int> _totalDistances = new List<int>();

        static void Main(string[] args)
        {
            test2();
        }

        static void test2()
        {           
            _numRows = 5;
            _numColumns = 2;
            int[,] area = { { 1, 1, 1, 1, 0 },
                            { 1, 1, 1, 9, 0 } };
            int[] position = { 0, 0};

            //VerifyProperty();

            //Debug.WriteLine(area[0, 1]); // first row, second column
            //Debug.WriteLine(area[0, 4]); // first row, fourth column

            var total = Right(area, position);

            Debug.WriteLine(total);
        }

        static private void VerifyProperty ()
        {
            int[,] area = { { 1, 1, 3, 1, 0 },
                            { 1, 4, 1, 9, 0 } };

            int[] position = { 1, 2 };

            var startingPosition = new Position(area, position);

            Debug.WriteLine("Right: " + startingPosition.NextRightPositionResult);
            Debug.WriteLine("Left: " + startingPosition.NextLeftPositionResult);
            Debug.WriteLine("Down: " + startingPosition.NextDownPositionResult);
            Debug.WriteLine("Up: " + startingPosition.NextUpPositionResult);

        }

        static private int Right(int[,] area, int[] currentPosition)
        {
            var position = new Position(area, currentPosition);

            var isDeadEnd = (!position.canGoRight
                            && !position.canGoDown
                            && !position.canGoUp);

            if (isDeadEnd)
            {
                return -100;
            }

            if (position.canGoRight)
            {
                if (area[next_right[0], next_right[1]] == 9)
                {
                    return 1;
                }

                if (area[next_right[0], next_right[1]] == 1)
                {
                    return 1 + Right(area, next_right);
                }

                //if (currentPosition[0] == 0 && currentPosition[1] == 0)// if (0,0)
                //{
                //    _totalDistances.Add();
                //}
            }

            if (CanGoDown(currentPosition))
            {
                if (area[next_down[0], next_down[1]] == 9)
                {
                    return 1;
                }

                if (area[next_down[0], next_down[1]] == 1)
                {
                    return 1 + Down(area, next_down);
                }

                if (area[next_right[0], next_right[1]] == 0 && isDeadEnd)
                {
                    return -100;
                }
            }

            return -1000;
        }



        static private int Down(int[,] area, int[] currentPosition)
        {
            var isDeadEnd = (!CanGoRight(currentPosition) && !CanGoDown(currentPosition) && !CanGoLeft(currentPosition));

            var next_right = GetRightPosition(currentPosition);
            var next_left = GetLeftPosition(currentPosition);
            var next_down = GetDownPosition(currentPosition);



            return 1;
        }

        static private int Up(int[,] area, int current_X, int current_Y)
        {
            return 1;
        }

        static private int Left(int[,] area, int current_X, int current_Y)
        {
            return 1;
        }



        static void test1()
        {
            int numDestinations = 4;
            int[,] allLocations = { { 1, 2, 3 }, { 3, 4, 4 }, { 5, 6, 5 }, { 7, 8, 5 } };
            int numDeliveries = 2;

            double[,,] calculatedDistanceWithLocation = new double[numDestinations, 3, 1]; //[x,y,distance]
            List<double> distances = new List<double>();


            List<List<int>> allOrderedCoordinates = new List<List<int>>();




            //Debug.WriteLine(allLocations.GetLength(0)); // 4: rows
            //Debug.WriteLine(allLocations.GetLength(1)); // 2: columns
            //Debug.WriteLine($"{allLocations[0, 0]} {allLocations[0,1]}"); // first row, column 1 and 2

            for (int locationCoordinate = 0; locationCoordinate < allLocations.GetLength(0); locationCoordinate++)
            {
                double x = allLocations[locationCoordinate, 0];
                double y = allLocations[locationCoordinate, 1];
                double distance = Math.Round(Math.Sqrt((x * x) + (y * y)), 2);

                //Debug.WriteLine(distance);


                calculatedDistanceWithLocation[locationCoordinate, 0, 0] = x; //set x coordinate
                calculatedDistanceWithLocation[locationCoordinate, 1, 0] = y; //set y coordinate
                calculatedDistanceWithLocation[locationCoordinate, 2, 0] = distance;

                distances.Add(distance);
            }

            distances.Sort();

            foreach (var distance in distances)
            {
                Debug.WriteLine(distance);
                List<int> resultCoordinate = new List<int>();

                for (int row = 0; row < allLocations.GetLength(0); row++)
                {
                    var targetDistance = calculatedDistanceWithLocation[row, 2, 0];

                    if (distance == targetDistance)
                    {
                        Debug.WriteLine($"{calculatedDistanceWithLocation[row, 0, 0]},{calculatedDistanceWithLocation[row, 1, 0]}");
                        resultCoordinate.Add(Convert.ToInt32(calculatedDistanceWithLocation[row, 0, 0]));
                        resultCoordinate.Add(Convert.ToInt32(calculatedDistanceWithLocation[row, 1, 0]));
                        allOrderedCoordinates.Add(resultCoordinate);

                        break;
                    }
                }
            }

            //foreach (var coord in allOrderedCoordinates)
            //{
            //    Debug.WriteLine(coord[0] + " " + coord[1]);
            //}




            //foreach (var location in calculatedDistanceWithLocation)
            //{
            //    Debug.WriteLine(location);
            //}


        }

        static void demo1()
        {
            //test case 1
            //int[] states = { 1, 0, 0, 0, 0, 1, 0, 0 };
            //int days = 1;

            //test case 2
            int[] states = { 1, 1, 1, 0, 1, 1, 1, 1 };
            int days = 2;

            int[] newStates = new int[8];

            for (int currentDay = 1; currentDay <= days; currentDay++)
            {
                for (int i = 0; i < states.Length; i++)
                {
                    int previousNeighbor;
                    int nextNeighbor;

                    if (i == 0) //if beginning
                    {
                        previousNeighbor = 0;
                        nextNeighbor = states[i + 1];
                    }
                    else if (i == states.Length - 1) //if beginning or ending
                    {
                        previousNeighbor = states[i - 1];
                        nextNeighbor = 0;
                    }
                    else
                    {
                        previousNeighbor = states[i - 1];
                        nextNeighbor = states[i + 1];
                    }

                    if (previousNeighbor == nextNeighbor)
                    {
                        newStates[i] = 0;
                    }
                    else
                    {
                        newStates[i] = 1;
                    }

                }

                foreach (var state in newStates)
                {
                    Debug.Write(state);
                }

                Debug.WriteLine("");

                Array.Copy(newStates, states, states.Length);

            }
        }
    }
}
