using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace KrestikiNoliki
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        Dictionary<string, int> buttons = new Dictionary<string, int>()
            {
                { "one", 0}, { "two", 1}, {"three", 2}, { "four", 3}, {"five", 4}, {"six", 5}, {"seven", 6},
                {"eight", 7}, {"nine", 8}
            };
        Dictionary<int, string> buttons_for_ai = new Dictionary<int, string>()
            {
                {0, "one"}, {1, "two"}, {2, "three"}, {3, "four"}, {4, "five"}, {5, "six"}, {6, "seven"},
                {7, "eight"}, {8, "nine"}
            };
        int[,] win_compilation = new int[8, 3] { {0, 1, 2}, {0, 3, 6}, {0, 4, 8}, {1, 4, 7}, {2, 5, 8},
            {2, 4, 6}, {3, 4, 5}, {6, 7, 8}};
        List<int> used_buttons = new List<int>();
        List<int> used_ai_buttons = new List<int>();
        List<int> free_buttons = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        string person_operand = "0";
        string ai_operand = "x";
        bool winner_exists = false;
        List<Button> buttons_for_game= new List<Button>();
        Random rnd = new Random();

        private void Button_Clicked(object sender, RoutedEventArgs e)
        {
            int number_of_button = buttons[(string)(sender as Button).Name.ToString()];
            (sender as Button).Content = person_operand;
            (sender as Button).IsEnabled= false;
            used_buttons.Add(number_of_button);
            Winny_Winner();
            free_buttons.Remove(number_of_button);
            if (free_buttons.Count == 0 & winner_exists == false)
            {
                MessageBox.Show("There is no winner");
                Restart();
            }
            Ai_click(rnd);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            robot.IsEnabled = false;
            person.IsEnabled = false;
            if ((string)(sender as RadioButton).Content == "Вы")
            {
                if (person_operand == "0")
                {
                    person_operand = "x";
                    ai_operand = "0";
                }
                else
                {
                    person_operand = "0";
                    ai_operand = "x";
                }
                MessageBox.Show("Make your move");
            }
            else
            {
                if (ai_operand == "0")
                {
                    person_operand = "0";
                    ai_operand = "x";
                }
                else
                {
                    person_operand = "x";
                    ai_operand = "0";
                }
                MessageBox.Show("The first move will make robot");
                Ai_click(rnd);
            }
        }

        private void Ai_click(Random random)
        {
            int ind = random.Next(free_buttons.Count);
            int number__button = free_buttons[ind];
            string name_of_button = buttons_for_ai[number__button];
            foreach(Button button in buttons_for_game) 
            {
                if(button.Name == name_of_button)
                {
                    button.Content = ai_operand;
                    button.IsEnabled = false;
                    used_ai_buttons.Add(number__button);
                    Winny_Winner();
                    free_buttons.Remove(number__button);
                    if (free_buttons.Count == 0 & winner_exists == false)
                    {
                        MessageBox.Show("There is no winner");
                        Restart();
                        break;
                    }
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            buttons_for_game.Add(one);
            buttons_for_game.Add(two);
            buttons_for_game.Add(three);
            buttons_for_game.Add(four);
            buttons_for_game.Add(five);
            buttons_for_game.Add(six);
            buttons_for_game.Add(seven);
            buttons_for_game.Add(eight);
            buttons_for_game.Add(nine);
            foreach(Button i in buttons_for_game)
            {
                i.IsEnabled= true;
            }
            MessageBox.Show("Please decide who will move first");
            robot.IsEnabled= true;
            person.IsEnabled= true;
        }

        private int Winny_Winner()
        {
           for(int i=0; i<win_compilation.GetLength(0); i++)
           {
                for(int j=0; j < win_compilation.GetLength(1);)
                {
                    if (used_buttons.Contains(win_compilation[i, j]) & used_buttons.Contains(win_compilation[i, j + 1]) & used_buttons.Contains(win_compilation[i, j + 2]))
                    {
                        MessageBox.Show("You wone!!!");
                        winner_exists = true;
                        Restart();
                        return 0;

                    }
                    else if(used_ai_buttons.Contains(win_compilation[i, j]) & used_ai_buttons.Contains(win_compilation[i, j + 1]) & used_ai_buttons.Contains(win_compilation[i, j + 2]))
                    {
                        MessageBox.Show("Robot won!");
                        winner_exists = true;
                        Restart();
                        return 0;
                    }
                    break;
                }
           }
            return 0;
        }

        private void Restart()
        {
            foreach(Button i in buttons_for_game)
            {
                i.Content = "";
                i.IsEnabled= false;
            }
            buttons_for_game.Clear();
            used_buttons.Clear();
            used_ai_buttons.Clear();
            free_buttons.Clear();
            for(int i=0; i<9; i++)
            {
                free_buttons.Add(i);
            }
            robot.IsEnabled= false;
            robot.IsChecked= false;
            person.IsEnabled= false;
            person.IsChecked= false;
            winner_exists= false;

        }
    }
}
