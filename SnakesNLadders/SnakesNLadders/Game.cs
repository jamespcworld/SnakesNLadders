using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnakesNLadders
{
    public class Game
    {
        private Window1 _gameBoard;
        private int[] snakeNladder = { 3, 9, 13, 25, 36, 46, 12, 20, 39, 44, 47, 54 };
        private int[] destination = { 10, 21, 31, 40, 51, 50, 2, 5, 22, 15, 30, 19 };
        public Game(Window1 gameBoard)
        {
            _gameBoard = gameBoard;
        }
        public void MoveTurn(int d, int pos, int turn)
        {
            int j;

            for (j = d; j < d + pos + 1; j++)
            {
                _gameBoard.Board.Children.Remove(_gameBoard.players[turn].piece);
                _gameBoard.players[turn].piece.SetValue(Grid.RowProperty, _gameBoard.points[j, 0]);
                _gameBoard.players[turn].piece.SetValue(Grid.ColumnProperty, _gameBoard.points[j, 1]);
                MoveBorder(j, turn);
                _gameBoard.Board.Children.Add(_gameBoard.players[turn].piece);
            }
            j--;
            _gameBoard.players[turn].position = j;
            if (snakeNladder.Contains(j))
            {
                for (int i = 0; i < snakeNladder.Length; i++)
                {
                    if (j == snakeNladder[i])
                    {
                        _gameBoard.Board.Children.Remove(_gameBoard.players[turn].piece);
                        _gameBoard.players[turn].piece.SetValue(Grid.RowProperty, _gameBoard.points[destination[i], 0]);
                        _gameBoard.players[turn].piece.SetValue(Grid.ColumnProperty, _gameBoard.points[destination[i], 1]);
                        _gameBoard.players[turn].position = destination[i];
                        MoveBorder(destination[i], turn);
                        _gameBoard.Board.Children.Add(_gameBoard.players[turn].piece);
                        break;
                    }
                }

            }
        }


        public void MoveBorder(int pos, int t)
        {
            if (t == 0)
            {
                _gameBoard.Board.Children.Remove(_gameBoard.p1border);
                _gameBoard.p1border.SetValue(Grid.RowProperty, _gameBoard.points[pos, 0]);
                _gameBoard.p1border.SetValue(Grid.ColumnProperty, _gameBoard.points[pos, 1]);
                _gameBoard.Board.Children.Add(_gameBoard.p1border);
            }
            else
            {
                _gameBoard.Board.Children.Remove(_gameBoard.p2border);
                _gameBoard.p2border.SetValue(Grid.RowProperty, _gameBoard.points[pos, 0]);
                _gameBoard.p2border.SetValue(Grid.ColumnProperty, _gameBoard.points[pos, 1]);
                _gameBoard.Board.Children.Add(_gameBoard.p2border);
            }
        }

        public void populatePositions() {
            int cell = 0;
            for (int i = 6; i >= 0; i--)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        _gameBoard.points[cell, 0] = i;
                        _gameBoard.points[cell, 1] = j;
                        cell++;
                    }
                }
                else
                {
                    for (int j = 7; j >= 0; j--)
                    {
                        _gameBoard.points[cell, 0] = i;
                        _gameBoard.points[cell, 1] = j;
                        cell++;
                    }
                }
            }

        }

        public int GetRollDiceNum()
        {
            Random s = new Random(DateTime.Now.Millisecond);
            int v = s.Next(1, 7);

            return v;
        }
    }
}
