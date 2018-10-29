using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace SnakesNLadders
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
                /////////////////////
               /* MY DATA MEMBERS */
              /////////////////////
        public Player[] players = new Player[2];           // Array of players
        public int turn;                                   // checks turn
        public int[,] points = new int[56, 2];             // checks Row and col of Data grid
        private Player[] savedPlayers = new Player[2];      // Save GAme Data
        private int[] wins = new int[2];                    // records wins of players
        private Game game;
  
        public Window1()
        {
            InitializeComponent();
            game = new Game(this);
        }
        public Window1(string s1,string s2)
        {
            InitializeComponent();
            players[0].name = s1;
            players[1].name = s2;

        }

        
        private Storyboard ResizeBox(float f, int num)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimationUsingKeyFrames dauk1 = new DoubleAnimationUsingKeyFrames();
            dauk1.BeginTime = new TimeSpan(0);
            dauk1.SetValue(Storyboard.TargetNameProperty, "Box"+num.ToString());
            dauk1.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("RenderTransform.ScaleX"));
            SplineDoubleKeyFrame sdkf1 = new SplineDoubleKeyFrame();
            sdkf1.KeyTime = TimeSpan.FromSeconds(0.5);
            sdkf1.Value = f;
            dauk1.KeyFrames.Add(sdkf1);
            sb.Children.Add(dauk1);

            DoubleAnimationUsingKeyFrames dauk2 = new DoubleAnimationUsingKeyFrames();
            dauk2.BeginTime = new TimeSpan(0);
            dauk2.SetValue(Storyboard.TargetNameProperty, "Box" + num.ToString());
            dauk2.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("RenderTransform.ScaleY"));
            SplineDoubleKeyFrame sdkf2 = new SplineDoubleKeyFrame();
            sdkf2.KeyTime = TimeSpan.FromSeconds(0.5);
            sdkf2.Value = f;
            dauk2.KeyFrames.Add(sdkf2);
            sb.Children.Add(dauk2);
            return sb;
        }

        private void Boxed(int num)
        {
            Storyboard s = ResizeBox(1.2f, num);
            s.Begin(this);
        }

        private void UnBoxed(int num)
        {
            Storyboard s = ResizeBox(1.0f, num);
            s.Begin(this);
        }
        public int RollDice()
        {
            int v = game.GetRollDiceNum();

            BitmapImage j = new BitmapImage(new Uri(@"../../Images/dice" + v + ".png", UriKind.RelativeOrAbsolute));
            Box4.Fill = new ImageBrush(j);
            Boxed(4);
            
            return v;
        }

        public void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            btnRoll.IsEnabled = false;
            int d = RollDice();

           
            int pos = players[turn].position;
             
            if (players[turn].readytogo)
            {
                if (pos + d < ((points.Length) / 2))
                {
                    game.MoveTurn(d, pos, turn);
                }
                else
                    MessageBox.Show(players[turn].name + " !!! You are not allowed to move this far...", "Warning !!!",MessageBoxButton.OK,MessageBoxImage.Error);
                    

            }
            else if (d == 6 && players[turn].readytogo == false)
            {
                players[turn].readytogo = true;
                MessageBox.Show(players[turn].name + " is now Ready to go....", "Start Moving !!!", MessageBoxButton.OK, MessageBoxImage.Information);
                Boxed(0);//OPens Home (increases its size)
                
            }

            if (d + pos == ((points.Length) / 2) - 1)
            {
                MessageBox.Show("Congratulations !!! " + players[turn].name + " Wins...", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Boxed(55);
                wins[turn]++;
                lblwins.Content = "Wins " + wins[0].ToString() + " - " + wins[1].ToString();
                btnRoll.IsEnabled = false;
            }
            else
                btnRoll.IsEnabled = true;
            
            UnBoxed(turn + 1);
            
            turn = (turn + 1) % 2;

            ScoreBoard();
            
            Boxed(turn + 1);
            UnBoxed(4);

        }
        private void ScoreBoard()
        {
            p1score.Content = players[0].position.ToString();
            p2score.Content = players[1].position.ToString();
            cplayer.Content = players[turn].name;
        }

        private void GameStart(object sender, EventArgs e)
        {
            game.populatePositions();

            lblwins.Content = "Wins 0 - 0";

            players[0].piece = Box1;
            players[1].piece = Box2;

            players[0].name = "Player 1";
            players[1].name = "Player 2";

            Player1.Content = players[0].name;
            Player2.Content = players[1].name;

            Board.Children.Remove(Box1);
            Board.Children.Remove(Box2);


            ResetGame(sender, e);

        }

        private void ResetGame(object sender, EventArgs e)
        {
            turn = 0;


            UnBoxed(0);
            UnBoxed(55);

            Boxed(turn + 1);

            BitmapImage j=new BitmapImage(new Uri(@"../../Images/dice0.png",UriKind.RelativeOrAbsolute));
            Box4.Fill =new ImageBrush(j);
            
            dice.Content = "Click Roll Dice to start";

            for (int i = 0; i < 2; i++)
            {
                players[i].piece.SetValue(Grid.RowProperty, points[0, 0]);
                players[i].piece.SetValue(Grid.ColumnProperty, points[0, 1]);
                players[i].position = 0;
                players[i].readytogo = false;
                game.MoveBorder(0, i);
                Board.Children.Add(players[i].piece);
            }    
        
            ScoreBoard();
        }



        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
        
    }

}
