using GameOfLifeApi;
using GameOfLifeApi.Models.Hubs;
using GameOfLifeClient.Hubs;
using GameOfLifeClient.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfGameOfLife.Utils.Faker;

namespace WpfGameOfLife
{
    /// <summary>
    /// Interaction logic for Launcher.xaml
    /// </summary>
    
    public partial class Launcher : Window
    {
        public static Game game = new Game(); // Instanciamos el juego.
        //public static string UserName { get; set; }
        private static bool userNameContext = new bool();
        public static bool isConnected = new bool();
        public static string userNameText = "";

        public Launcher()
        {
            InitializeComponent();

            FakeData fake = new FakeData();
            fake.Startup();

        }
       
        private readonly HubConnection _hub = SessionHub.sessionHub;
        private readonly FakePlayer _fakePlayer = new FakePlayer();


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userNameField.Text = _fakePlayer.Name;
            userNameText = userNameField.Text!;

            hubStatusText.Text = "Conectando...";
            userNameContext = false;

            try
            {
                await _hub.StartAsync();
                
                joinMatchBtn.IsHitTestVisible = true;
                newGameBtn.IsHitTestVisible = true;

                hubStatusText.Text = $"Conectado al Hub ✅";
                isConnected = true;

                await SessionHub.Start(); 
            }
            catch (Exception err)
            {
                hubStatusText.Text = $"Imposible conectar. Hub no disponible ⛔";
                isConnected = false;
                playWithoutConnectionBox.Visibility = Visibility.Visible;
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void userNameField_GotFocus(object sender, RoutedEventArgs e)
        {           
            if (!userNameContext)
            {
                userNameField.Text = string.Empty;
                userNameField.Foreground = new SolidColorBrush(Colors.White);
                userNameContext = true;
            } else
            {
                return;
            }
            
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Minimize 
            WindowState = WindowState.Minimized;
            
        }

        /// <summary>
        /// Abrimos la ventana de juego, y cerramos la ventana de lanzador.
        /// </summary>
        private void playWithoutConnectionBtn_Click(object sender, RoutedEventArgs e)
        {
            // Show game window.
            game.Show(); // Mostramos.
            Application.Current.MainWindow = game; // La hacemos main.
        }

        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            if(isConnected)
            {
                game.Show();
                Application.Current.MainWindow = game; // La hacemos main.
            } else
                hubStatusText.Text = "No conectado al Hub ❌";
        }

        private void newMatchButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isConnected)
            {
                game.Show();
                Application.Current.MainWindow = game; // La hacemos main.
            }
            else
                hubStatusText.Text = "No conectado al Hub ❌";
        }
    }
}
