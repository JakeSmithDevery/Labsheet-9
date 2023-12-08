using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exam_2019
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Player> allPlayers = new List<Player>();
        List<Player> selectedPlayers = new List<Player>();
        int selectedGoalkeepers, selectedDefenders, selectedMidfielders, selectedForwards;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(selectedPlayers.Count > 10)
            {
                MessageBox.Show("You have too many players selected");
            }

            else
            {
                Player selected = lbxAllPlayers.SelectedItem as Player;

                if (selected != null)
                {

                    if (IsValidPlayer(selected))
                    {
                        selectedPlayers.Add(selected);
                        allPlayers.Remove(selected);
                        RefreshScreen();
                    }
                    else
                    {
                        MessageBox.Show("Player not in formation");
                    }
                }
            }
            
        }

        private void RefreshScreen()
        {
            selectedPlayers.Sort();
            allPlayers.Sort();

            lbxAllPlayers.ItemsSource = null;
            lbxAllPlayers.ItemsSource = allPlayers;

            lbxSelectedPlayers.ItemsSource = null;
            lbxSelectedPlayers.ItemsSource = selectedPlayers;

            txtblk_Spaces.Text = (11 - selectedPlayers.Count).ToString();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private List<Player> CreatePlayers()
        {
            List <Player> players = new List<Player>();
            Random r = new Random();
            string[] firstNames = {
                "Adam", "Amelia", "Ava", "Chloe", "Conor", "Daniel", "Emily",
                "Emma", "Grace", "Hannah", "Harry", "Jack", "James",
                "Lucy", "Luke", "Mia", "Michael", "Noah", "Sean", "Sophie"};
            string[] lastNames = {
                "Brennan", "Byrne", "Daly", "Doyle", "Dunne", "Fitzgerald", "Kavanagh",
                "Kelly", "Lynch", "McCarthy", "McDonagh", "Murphy", "Nolan", "O'Brien",
                "O'Connor", "O'Neill", "O'Reilly", "O'Sullivan", "Ryan", "Walsh"
            };

            Position currentPosition = Position.Goalkeeper;
            //Create 18 players
            for (int i = 0; i < 18; i++)
            {

                DateTime date1 = DateTime.Now.AddYears(-30);
                DateTime date2 = DateTime.Now.AddYears(-20);
                TimeSpan t = date2 - date1;
                int numberOfDays = t.Days;
                DateTime newDate = date1.AddDays(r.Next(numberOfDays));

                

                switch(i)
                {
                    case 2:
                        currentPosition = Position.Defender;
                        break;
                    case 8:
                        currentPosition = Position.Midfielder;
                        break;
                    case 14:
                        currentPosition = Position.Forward;
                        break;
                }


                Player p = new Player()
                {
                    FirstName = firstNames[r.Next(20)],
                    SurName = lastNames[r.Next(20)],
                    DateOfBirth = newDate,
                    PreferedPosition = currentPosition
                };

                players.Add(p); 
            }
            return players;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void cbxFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Player player in selectedPlayers)
            {
                allPlayers.Add(player);
            }

            selectedPlayers.Clear();

            selectedGoalkeepers = 0;
            selectedDefenders = 0;
            selectedMidfielders = 0;
            selectedForwards = 0;

            RefreshScreen();
        }

        private void btn_Remove_Players_Click(object sender, RoutedEventArgs e)
        {
            Player selected = lbxSelectedPlayers.SelectedItem as Player;

            if (selected != null)
            {
                allPlayers.Add(selected);
                selectedPlayers.Remove(selected);

                RefreshScreen();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allPlayers = CreatePlayers();

            allPlayers.Sort();

            lbxAllPlayers.ItemsSource = allPlayers;

            cbxFormation.ItemsSource = new string[] { "4-4-2", "4-3-3", "4-5-1" };
        }

        private bool IsValidPlayer(Player player)
        {
            bool valid = false;

            string selectedFormation = cbxFormation.SelectedItem as string;
            string[] formation = selectedFormation.Split('-');


            int allowedGoalkeepers = 1;
            int allowedDefenders = int.Parse(formation[0]);
            int allowedMidfielders = int.Parse(formation[1]);
            int allowedForwards = int.Parse(formation[2]);


            switch(player.PreferedPosition)
            {
                case Position.Goalkeeper:
                    if (selectedGoalkeepers < allowedGoalkeepers)
                    {
                        selectedGoalkeepers++;
                        valid = true;
                    }

                    break;

                case Position.Defender:
                    if (selectedDefenders < allowedMidfielders)
                    { 
                        selectedDefenders++;
                        valid = true;
                    }
                    break;

                case Position.Midfielder:
                    if (selectedMidfielders < allowedMidfielders)
                    {
                        selectedMidfielders++;
                        valid = true;
                    }
                    break;

                case Position.Forward:
                    if (selectedForwards < allowedForwards)
                    {
                        selectedForwards++;
                        valid = true;
                    }
                    break;
            }


            return valid;
        }
    }
}
