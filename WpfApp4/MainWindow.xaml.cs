using System;
using System.Diagnostics;
using System.Windows;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartProcessButton_Click(object sender, RoutedEventArgs e)
        {
            string processName = ProcessNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(processName))
            {
                MessageBox.Show("Please enter a valid process name.");
                return;
            }

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = processName;
                process.EnableRaisingEvents = true;
                process.Start();

                await process.WaitForExitAsync();

                int exitCode = process.ExitCode;
                ProcessStatusLabel.Content = $"Process exited with code: {exitCode}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}