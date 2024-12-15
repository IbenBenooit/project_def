using System;
using System.IO.Ports;
using System.Media;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LEDControllerApp
{
    public partial class MainWindow : Window
    {
        private SerialPort _serialPort;
        private int currentLED = 1;
        SoundPlayer _player1 = new SoundPlayer(soundLocation: @"C:\Users\ibenb\Documents\ICT\project\new_test_2\geluiden/doorbell-1.wav");
        SoundPlayer _player2 = new SoundPlayer(soundLocation: @"C:\Users\ibenb\Documents\ICT\project\new_test_2\geluiden/doorbell-6.wav");
        SoundPlayer _player3 = new SoundPlayer(soundLocation: @"C:\Users\ibenb\Documents\ICT\project\new_test_2\geluiden/doorbell-2.wav");
        SoundPlayer _player4 = new SoundPlayer(soundLocation: @"C:\Users\ibenb\Documents\ICT\project\new_test_2\geluiden/doorbell-3.wav");
        public MainWindow()
        {
            InitializeComponent();
            LoadComPorts();
            UpdateUIState();
            LoadImage();
        }

        private void LoadComPorts()
        {
            ComPortComboBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                ComPortComboBox.Items.Add(port);
            }

            if (ComPortComboBox.Items.Count > 0)
                ComPortComboBox.SelectedIndex = 0;
        }

        private void UpdateUIState()
        {
            bool isConnected = _serialPort != null && _serialPort.IsOpen;
            ConnectButton.Content = isConnected ? "Disconnect" : "Connect";
            StatusText.Text = isConnected ? "Connected" : "Disconnected";
            LEDGroup.IsEnabled = isConnected;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Close();
                    _serialPort.Dispose();
                    _serialPort = null;
                }
                else
                {
                    string selectedPort = ComPortComboBox.SelectedItem as string;
                    if (string.IsNullOrEmpty(selectedPort))
                    {
                        MessageBox.Show("Select a COM port.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    _serialPort = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                    _serialPort.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UpdateUIState();
            }
        }
        private void LoadImage()
        {
            try
            {

                string imagePath = @"C:\Users\ibenb\Documents\ICT\project\new_test_2\foto\deurbel.png";

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                canvasImage.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij laden afbeelding: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
            }
        }




        private void LEDButton_Click_2(object sender, RoutedEventArgs e)
        {
            _serialPort.Write("\u0002");
        }
        private void LEDButton_Click_1(object sender, RoutedEventArgs e)
        {
            _serialPort.Write("\u0001");
        }
        private void LEDButton_Click_3(object sender, RoutedEventArgs e)
        {
            _serialPort.Write("\u0004");
        }
        private void LEDButton_Click_4(object sender, RoutedEventArgs e)
        {
            _serialPort.Write("\u0008");
        }

        private void UpdateLEDSelection(int ledNumber)
        {
            currentLED = ledNumber;
            UpdateLEDButtons();
        }

        private void UpdateLEDButtons()
        {
            Button[] ledButtons = { LED1Button, LED2Button, LED3Button, LED4Button };

            for (int i = 0; i < ledButtons.Length; i++)
            {
                ledButtons[i].Background = (i == currentLED - 1) ? Brushes.LightGreen : Brushes.LightGray;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _player1.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _player2.Play();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _player3.Play();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _player4.Play();
        }
    }
}