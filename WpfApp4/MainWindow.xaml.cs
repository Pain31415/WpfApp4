using System;
using System.Diagnostics;
using System.Windows;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        private Process _process;

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
                _process = new Process();
                _process.StartInfo.FileName = processName;
                _process.EnableRaisingEvents = true;
                _process.Start();

                await _process.WaitForExitAsync();

                int exitCode = _process.ExitCode;
                ProcessStatusLabel.Content = $"Process exited with code: {exitCode}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void KillProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null && !_process.HasExited)
            {
                try
                {
                    _process.Kill();
                    ProcessStatusLabel.Content = "Process killed.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("No running process to kill.");
            }
        }
    }
}