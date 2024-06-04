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
            string arg1 = Arg1TextBox.Text;
            string arg2 = Arg2TextBox.Text;
            string operation = OperationTextBox.Text;

            if (string.IsNullOrWhiteSpace(processName) || string.IsNullOrWhiteSpace(arg1) ||
                string.IsNullOrWhiteSpace(arg2) || string.IsNullOrWhiteSpace(operation))
            {
                MessageBox.Show("Please enter valid process name and arguments.");
                return;
            }

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = processName;
                process.StartInfo.Arguments = $"{arg1} {arg2} {operation}";
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