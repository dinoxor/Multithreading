using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon
{
    public class Position
    {
        private int[,] _area;
        private int _numRows;
        private int _numColumns;
        private int[] _currentPosition;

        public int[] NextRightPosition;
        public int[] NextDownPosition;
        public int[] NextUpPosition;
        public int[] NextLeftPosition;
              
        public int? NextRightPositionResult;
        public int? NextDownPositionResult;
        public int? NextUpPositionResult;
        public int? NextLeftPositionResult;       

        public Position(int[,] area, int[] currentPosition)
        {
            _area = area;
            _numRows = area.GetLength(0);
            _numColumns = area.GetLength(1);
            _currentPosition = currentPosition;

            SetRightPositionProperties();
            SetDownPositionProperties();
            SetUpPositionProperties();
            SetLeftPositionProperties();
        }

        private void SetRightPositionProperties()
        {
            //(x+1 , y)
            //first row, column + 1 
            NextRightPosition = new int[] { _currentPosition[0], _currentPosition[1] + 1  };

            //if x axis is less than number of rows
            //AND area(x+1 , y) does not equal 0
            var canGoRight = NextRightPosition[1] < _numColumns && _area[NextRightPosition[0], NextRightPosition[1]] != 0 ? true : false;

            NextRightPositionResult = canGoRight ? _area[NextRightPosition[0],NextRightPosition[1]] : (int?)null;
        }

        private void SetDownPositionProperties()
        {
            //(x , y+1)
            //row + 1, column
            NextDownPosition = new int[] { _currentPosition[0] + 1 , _currentPosition[1] };

            //if y axis is less than number of columns
            //AND area(x,y+1) does not equal 0
            var canGoDown = NextDownPosition[0] < _numRows && _area[NextDownPosition[0], NextDownPosition[1]] != 0 ? true : false;
        
            NextDownPositionResult = canGoDown ? _area[NextDownPosition[0], NextDownPosition[1]] : (int?)null;

        }

        private void SetUpPositionProperties()
        {
            //(x , y-1)
            NextUpPosition = new int[] { _currentPosition[0] -1, _currentPosition[1] };

            var canGoUp = NextUpPosition[0] >= 0 && _area[NextUpPosition[0], NextUpPosition[1]] != 0 ? true : false;

            NextUpPositionResult = canGoUp ? _area[NextUpPosition[0], NextUpPosition[1]] : (int?)null;
        }

        private void SetLeftPositionProperties()
        {
            //(x-1 , y)
            NextLeftPosition = new int[] { _currentPosition[0], _currentPosition[1] -1 };

            //if x axis is greater than or equal to 0
            //AND area(x-1, y) != 0
            var canGoLeft = NextLeftPosition[1] >= 0 && _area[NextLeftPosition[0], NextLeftPosition[1]] != 0 ? true : false;

            NextLeftPositionResult = canGoLeft ? _area[NextLeftPosition[0], NextLeftPosition[1]] : (int?)null;
        }

    }

}
