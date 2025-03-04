﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SCEEC.MI.TZ3310;
using System.Data;
using System.IO;

namespace SCEEC.TTM
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (new SplashScreen()).ShowDialog();
            //(new LoginWindow()).ShowDialog();

            LocationListBox.ItemsSource = WorkingSets.local.getLocationName();
            TransformerListBox.ItemsSource = WorkingSets.local.getTransformerSerialNo();


        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MinimumButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximumButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                maximumButtonImage.Source = new BitmapImage(new Uri("Resources/maximum.png", UriKind.Relative));
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                maximumButtonImage.Source = new BitmapImage(new Uri("Resources/maximum2.png", UriKind.Relative));
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Minimized:
                    break;
                default:
                    this.Show();
                    this.Activate();
                    break;
            }
        }

        private void LocationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.LocationListBox.SelectedIndex >= 0)
            {
                ModifyLocationButton.Visibility = Visibility.Visible;
                TransformerListBox.ItemsSource = WorkingSets.local.getTransformerSerialNo(this.LocationListBox.SelectedItem.ToString());
            }
            else
            {
                ModifyLocationButton.Visibility = Visibility.Hidden;
                TransformerListBox.ItemsSource = WorkingSets.local.getTransformerSerialNo();
            }
        }

        private void NewLocationButton_Click(object sender, RoutedEventArgs e)
        {
            LocationSettingWindow locationHelper = new LocationSettingWindow();
            locationHelper.ShowDialog();
            WorkingSets.local.refreshLocations();
            List<string> locations = WorkingSets.local.getLocationName();
            LocationListBox.ItemsSource = locations;
            LocationListBox.SelectedIndex = locations.IndexOf(locationHelper.name);
        }

        private void ModifyLocationButton_Click(object sender, RoutedEventArgs e)
        {
            LocationSettingWindow locationHelper = new LocationSettingWindow(this.LocationListBox.SelectedItem.ToString());
            locationHelper.ShowDialog();
            WorkingSets.local.refreshLocations();
            List<string> locations = WorkingSets.local.getLocationName();
            LocationListBox.ItemsSource = locations;
            LocationListBox.SelectedIndex = locations.IndexOf(locationHelper.name);
        }

        private void LocationListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InsertDataTodatabase.UpdataDatabase(TestListBox.SelectedItem.ToString());

            if (LocationListBox.IsMouseCaptured)
                if (LocationListBox.SelectedIndex > -1)
                {
                    ModifyLocationButton_Click(null, null);
                    return;
                }
            LocationListBox.SelectedIndex = -1;
        }

        private List<string> refreshTransformerList()
        {
            List<string> sn = new List<string>();
            if (LocationListBox.SelectedIndex >= 0)
            {
                DataRow[] rows = WorkingSets.local.Transformers.Select("location = '" + WorkingSets.local.getLocationName()[LocationListBox.SelectedIndex] + "'");
                foreach (DataRow row in rows)
                {
                    sn.Add(row["serialno"].ToString());
                }
            }
            else
            {
                foreach (DataRow row in WorkingSets.local.Transformers.Rows)
                {
                    sn.Add(row["serialno"].ToString());
                }
            }
            return sn;
        }

        private void NewTransformerButton_Click(object sender, RoutedEventArgs e)
        {
            TransformerSettingWindow transformerHelper = new TransformerSettingWindow();
            transformerHelper.ShowDialog();
            List<string> sn = refreshTransformerList();
            TransformerListBox.ItemsSource = sn;
            if (transformerHelper.serialno != string.Empty)
                TransformerListBox.SelectedIndex = sn.IndexOf(transformerHelper.serialno);
            else TransformerListBox.SelectedIndex = -1;
        }

        private void TransformerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.TransformerListBox.SelectedIndex >= 0)
            {
                ModifyTransformerButton.Visibility = Visibility.Visible;
                NewJobButton.IsEnabled = true;
                NewJobButton.Opacity = 1;
            }
            else
            {
                ModifyTransformerButton.Visibility = Visibility.Hidden;
                NewJobButton.IsEnabled = false;
                NewJobButton.Opacity = 0.3;
            }
            refreshJobList();
        }

        private void refreshJobList()
        {
            if (this.TransformerListBox.SelectedIndex > -1)
            {
                JobListBox.ItemsSource = WorkingSets.local.getJobNames(TransformerListBox.SelectedItem.ToString());
            }
            else
            {
                JobListBox.ItemsSource = new List<string>();
            }
        }

        private void TransformerListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TransformerListBox.IsMouseCaptured)
                if (TransformerListBox.SelectedIndex > -1)
                {
                    ModifyTransformerButton_Click(null, null);
                    return;
                }
            TransformerListBox.SelectedIndex = -1;
        }

        private void ModifyTransformerButton_Click(object sender, RoutedEventArgs e)
        {
            TransformerSettingWindow transformerHelper = new TransformerSettingWindow(TransformerListBox.SelectedItem.ToString());
            transformerHelper.ShowDialog();
            List<string> sn = refreshTransformerList();
            TransformerListBox.ItemsSource = sn;
            if (transformerHelper.serialno != string.Empty)
                TransformerListBox.SelectedIndex = sn.IndexOf(transformerHelper.serialno);
            else TransformerListBox.SelectedIndex = -1;
        }

        private void NewJobButton_Click(object sender, RoutedEventArgs e)
        {
            if (TransformerListBox.SelectedIndex > -1)
            {
                JobSettingWindow jobHelper = new JobSettingWindow(TransformerListBox.SelectedItem.ToString());
                jobHelper.ShowDialog();
                refreshJobList();
            }
        }

        private void NewTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (TransformerListBox.SelectedIndex < 0) return;
            if (JobListBox.SelectedIndex < 0) return;
            WindowTesting windowTesting = new WindowTesting(TransformerListBox.SelectedItem.ToString(), JobListBox.SelectedItem.ToString());
            if (windowTesting.inited == true) windowTesting.ShowDialog();
        }

        private void JobListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.JobListBox.SelectedIndex >= 0)
            {
                ModifyJobButton.Visibility = Visibility.Visible;
                NewTestButton.IsEnabled = true;
                NewTestButton.Opacity = 1;
                TestListBox.ItemsSource = WorkingSets.local.getTestResultsFromJobID(WorkingSets.local.getJob(TransformerListBox.SelectedItem.ToString(), JobListBox.SelectedItem.ToString()).id);
            }
            else
            {
                ModifyJobButton.Visibility = Visibility.Hidden;
                NewTestButton.IsEnabled = false;
                NewTestButton.Opacity = 0.3;
            }
        }

        private void JobListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (JobListBox.IsMouseCaptured)
                if (JobListBox.SelectedIndex > -1)
                {
                    ModifyJobButton_Click(null, null);
                    return;
                }
            JobListBox.SelectedIndex = -1;
        }

        private void ModifyJobButton_Click(object sender, RoutedEventArgs e)
        {
            if ((TransformerListBox.SelectedIndex > -1) && (JobListBox.SelectedIndex > -1))
            {
                JobSettingWindow jobHelper = new JobSettingWindow(TransformerListBox.SelectedItem.ToString(), JobListBox.SelectedItem.ToString());
                jobHelper.ShowDialog();
                refreshJobList();
                JobListBox.SelectedIndex = JobListBox.Items.IndexOf(jobHelper.jobName);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // var tws = WorkingSets.local.getTestResults(ResultName);
            try { InsertDataTodatabase.UpdataDatabase(TestListBox.SelectedItem.ToString()); } catch { }
            InsertDataTodatabase.ShowExport(TestListBox.SelectedItem.ToString());
            //WorkingSets.local.DeleteAllExportTable();

        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ExportButton.IsEnabled = true;
            ExportButton.Opacity = 1;
            if (TestListBox.IsMouseCaptured)
                if (TestListBox.SelectedIndex > -1)
                {
                    TestButton_Click(null, null);
                    return;
                }
            TestListBox.SelectedIndex = -1;
        }



        private void TestListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.TestListBox.SelectedIndex >= 0)
            {
                List<string> ReportNames = new List<string>();
                string reportname = TestListBox.SelectedItem.ToString();
                string[] tpd = reportname.Split('(');
                ReportNames.Add("报告： " + tpd[0]);
                NewReportButton.IsEnabled = true;
                NewReportButton.Opacity = 1;
                ReportListBox.ItemsSource = ReportNames.ToArray();
                ReportNames.Clear();
            }
            else
            {
                ReportListBox.ItemsSource = "";
                NewReportButton.IsEnabled = false;
                NewReportButton.Opacity = 0.3;
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            if ((TestListBox.SelectedIndex > -1) && (JobListBox.SelectedIndex > -1))
            {
                ExportDataWindow Exp = new ExportDataWindow(TestListBox.SelectedItem.ToString());
                Exp.ShowDialog();
            }
        }

        private void NewReportButton_click(object sender, RoutedEventArgs e)
        {
            ExportButton_Click(null, null);
        }

        private void Fbutton_click(object sender, RoutedEventArgs e)
        {
            //Insertdatabyupan inser = new Insertdatabyupan(@"E:\Ta224959");
            //inser.InsertUpandatatodatabase();
        }

        private void ExportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExportList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ReportListBox.IsMouseCaptured)
                if (ReportListBox.SelectedIndex > -1)
                {
                    ExportButton_Click(null, null);
                    return;
                }
            ReportListBox.SelectedIndex = -1;
        }
    }
}
