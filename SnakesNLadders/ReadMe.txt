Description of implementation:

1) The idea is to consider the given snake and ladder board as a directed graph with number of vertices equal to the number of cells in the board. 
2) The solution if using array of snakeNLadder data matches array of destination data. 
int[]  snakeNladder = { 3, 9, 13, 25, 36, 46, 12, 20, 39, 44, 47, 54 };
int[] destination = { 10, 21, 31, 40, 51, 50, 2, 5, 22, 15, 30, 19 };


3) The input of MoveTurn() are "d" dice number, and "position" which is player in the number of cells of the board.
