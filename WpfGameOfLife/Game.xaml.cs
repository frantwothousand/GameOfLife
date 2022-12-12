using GameOfLifeApi;
using GameOfLifeClient.Hubs;
using GameOfLifeClient.Utils;
using Microsoft.Win32;
using Stfu.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfGameOfLife.Utils;
using WpfGameOfLife.Utils.Faker;

using _ = GameOfLifeClient.Hubs.SessionHub;
using _color = System.Drawing.Color;

namespace WpfGameOfLife
{
    /// <summary>
    /// Lógica de interacción para Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        #region Timer :)
        DispatcherTimer dTimer;
        #endregion
        private List<Person> _objPpl = new List<Person>();
        public static string username = "";

        #region Constructor
        public Game()
        {
            InitializeComponent();
        }
        #endregion

        #region Timer methods (this will autoplay with Game rules)
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            FakeData.GenerateRelationship(FakeData.persons);
        }
        #endregion

        #region WPF Game methods
        private void Window_Closed(object sender, EventArgs e)
        {
            //Application.Current.Shutdown();
            //this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Launcher))
                    window.Close();
            }
            
            Board brd = new Board();
            brd.CreateBoard(gameBoard);
            Log.Add(Brushes.GreenYellow, "Te has unido a la partida.", messageList);
            _.BroadcastPlayerName(Launcher.userNameText);
            
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            // Abrir un archivo y cargar los datos.
            OpenFileDialog _dialogFile = new OpenFileDialog();
            Thread _bgThread;
            if (_dialogFile.ShowDialog() == true)
            {
                string _path = _dialogFile.FileName;
                int _tmpcount = new int();
                FakeData.GetGeneratedPersons(_path);

                _bgThread = new Thread(() =>
                {
                    _objPpl = new List<Person>();
                    _objPpl = FakeData.persons;
                    Dispatcher.Invoke(() =>
                    {
                        // Actualizar la lista de personas.
                        Board.LoadFromCollection(gameBoard, _objPpl);
                        _.BroadcastCells(_objPpl);
                        foreach (var ppl in _objPpl)
                        {
                            _tmpcount++;
                        }
                        peopleAlive.Text = _tmpcount.ToString();
                    });
                });
                
                _bgThread.Start();
            }
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Fix this.
            ResetPersonData();
        }

        public static void ShowPersonInfo(int x, int y)
        {
            var person = FakeData.persons.Find(p => p.Position.X == x && p.Position.Y == y);

            Launcher.game.fullNameText.Text = $"{person!.GetSymbolByGender()} {person.GetFullName()}";
            Launcher.game.emojiByGenderText.Text = person.GetEmojiByGender();

            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(person.Avatar);
            bi.EndInit();

            Launcher.game.avatarImage.Source = bi;

            Launcher.game.bloodTypeText.Text = person.GetBloodType();
            Launcher.game.moodText.Text = person.GetFullMood();
            Launcher.game.employmentText.Text = person.GetEmploymentState();
            Launcher.game.salaryText.Text = person.Salary.ToString();
            Launcher.game.ageText.Text = person.Age.ToString();
            Launcher.game.marriedText.Text = person.GetCivilState();

        }

        private static void ResetPersonData()
        {
            Launcher.game.fullNameText.Text = "-";
            Launcher.game.emojiByGenderText.Text = "-";

            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("https://i.pinimg.com/736x/c9/e3/e8/c9e3e810a8066b885ca4e882460785fa.jpg");
            bi.EndInit();

            Launcher.game.avatarImage.Source = bi;

            Launcher.game.bloodTypeText.Text = "-";
            Launcher.game.moodText.Text = "-";
            Launcher.game.employmentText.Text = "-";
            Launcher.game.salaryText.Text = "-";
            Launcher.game.ageText.Text = "-";
            Launcher.game.marriedText.Text = "-";
        }
        #endregion :)


        #region Methods directly related to the game
        void StartGame()
        {
            dTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 100) };
            dTimer.Tick += DispatcherTimer_Tick!;
            dTimer.Start();

            // TODO: Add variables to handle control.
            // IsGameStarted = true;
        }
        #endregion

        private void messageField_KeyDown(object sender, KeyEventArgs e)
        {
            // If Key is enter.
            if (e.Key == Key.Enter)
            {
                sendMessageBtn_Click(sender, null);
            }
        }

        private async void sendMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                await _.Invoke("Send", messageField.Text);
                Log.Add(Brushes.Green, $"You: {messageField.Text}", messageList);
                //chatBox.Text = "";
            }
            catch (Exception err)
            {
                Log.Add(Brushes.Red, $"Error al enviar el mensaje: {err.Message}", messageList);
            }
        }
    }
}
