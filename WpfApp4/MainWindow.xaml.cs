using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        private System.Timers.Timer _timer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(RefreshIntervalTextBox.Text, out int interval))
            {
                _timer = new System.Timers.Timer(interval * 1000);
                _timer.Elapsed += Timer_Elapsed;
                _timer.Start();
                RefreshProcessList();
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer?.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(RefreshProcessList);
        }

        private void RefreshProcessList()
        {
            var processes = Process.GetProcesses()
                                   .Select(p => new { p.ProcessName, p.Id })
                                   .ToList();
            ProcessListView.ItemsSource = processes;
        }

        private void ProcessListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProcessListView.SelectedItem != null)
            {
                dynamic selectedProcess = ProcessListView.SelectedItem;
                var process = Process.GetProcessById(selectedProcess.Id);
                var details = $"ID: {process.Id}\n" +
                              $"Start Time: {process.StartTime}\n" +
                              $"Total Processor Time: {process.TotalProcessorTime}\n" +
                              $"Threads: {process.Threads.Count}\n" +
                              $"Instances: {Process.GetProcessesByName(process.ProcessName).Length}";

                ProcessDetailsLabel.Content = details;
            }
        }

        private void KillProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessListView.SelectedItem != null)
            {
                dynamic selectedProcess = ProcessListView.SelectedItem;
                try
                {
                    var process = Process.GetProcessById(selectedProcess.Id);
                    process.Kill();
                    RefreshProcessList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not kill process: {ex.Message}");
                }
            }
        }
    }
}