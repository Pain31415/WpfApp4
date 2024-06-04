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
            string filePath = FilePathTextBox.Text;
            string searchWord = SearchWordTextBox.Text;

            if (string.IsNullOrWhiteSpace(processName) || string.IsNullOrWhiteSpace(filePath) ||
                string.IsNullOrWhiteSpace(searchWord))
            {
                MessageBox.Show("Please enter valid process name and arguments.");
                return;
            }

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = processName;
                process.StartInfo.Arguments = $"{filePath} {searchWord}";
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