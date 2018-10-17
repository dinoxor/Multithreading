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

        private int[] _nextRightPosition;
        private int[] _nextDownPosition;
        private int[] _nextUpPosition;
        private int[] _nextLeftPosition;

        public int? NextRightPositionResult;
        public int? NextDownPositionResult;
        public int? NextUpPositionResult;
        public int? NextLeftPositionResult;

        public bool CanGoRight;
        public bool CanGoDown;
        public bool CanGoUp;
        public bool CanGoLeft;

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
            _nextRightPosition = new int[] { _currentPosition[0], _currentPosition[1] + 1  };

            //if x axis is less than number of rows
            //AND area(x+1 , y) does not equal 0
            CanGoRight = _nextRightPosition[1] < _numColumns && _area[_nextRightPosition[0], _nextRightPosition[1]] != 0 ? true : false;

            NextRightPositionResult = CanGoRight ? _area[_nextRightPosition[0],_nextRightPosition[1]] : (int?)null;
        }

        private void SetDownPositionProperties()
        {
            //(x , y+1)
            //row + 1, column
            _nextDownPosition = new int[] { _currentPosition[0] + 1 , _currentPosition[1] };

            //if y axis is less than number of columns
            //AND area(x,y+1) does not equal 0
            CanGoDown = _nextDownPosition[0] < _numRows && _area[_nextDownPosition[0], _nextDownPosition[1]] != 0 ? true : false;
        
            NextDownPositionResult = CanGoDown ? _area[_nextDownPosition[0], _nextDownPosition[1]] : (int?)null;

        }

        private void SetUpPositionProperties()
        {
            //(x , y-1)
            _nextUpPosition = new int[] { _currentPosition[0] -1, _currentPosition[1] };

            CanGoUp = _nextUpPosition[0] >= 0 && _area[_nextUpPosition[0], _nextUpPosition[1]] != 0 ? true : false;

            NextUpPositionResult = CanGoUp ? _area[_nextUpPosition[0], _nextUpPosition[1]] : (int?)null;
        }

        private void SetLeftPositionProperties()
        {
            //(x-1 , y)
            _nextLeftPosition = new int[] { _currentPosition[0], _currentPosition[1] -1 };

            //if x axis is greater than or equal to 0
            //AND area(x-1, y) != 0
            CanGoLeft = _nextLeftPosition[1] >= 0 && _area[_nextLeftPosition[0], _nextLeftPosition[1]] != 0 ? true : false;

            NextLeftPositionResult = CanGoLeft ? _area[_nextLeftPosition[0], _nextLeftPosition[1]] : (int?)null;
        }

    }

}
