using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace TrainDisplaySystem
{
    public partial class MainWindow : Window
    {
        // Auto-refresh timer
        private DispatcherTimer refreshTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            LoadTrainData();

            // Set up auto-refresh every 60 seconds
            refreshTimer.Interval = TimeSpan.FromSeconds(60);
            refreshTimer.Tick += (s, e) => LoadTrainData();
            refreshTimer.Start();
        }

        // Load train data from JSON file
        private void LoadTrainData()
        {
            try
            {
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "train_data.json");

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var trainList = JsonConvert.DeserializeObject<List<TrainInfo>>(json);
                    TrainGrid.ItemsSource = trainList;
                }
                else
                {
                    MessageBox.Show("⚠️ train_data.json not found in output folder!", "File Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error loading train data:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
