﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using System.Threading;
using SCEEC.MI.TZ3310;

namespace SCEEC.TTM
{
    /// <summary>
    /// SplashScreen.xaml 的交互逻辑
    /// </summary>
    public partial class SplashScreen : Window
    {

        public SplashScreen()
        {
            InitializeComponent();
        }

        bool inited = false;

        BackgroundWorker initialWorker;

        struct initialStatusType
        {
            public string status;
            public string detail;

            public initialStatusType(string status, string detail)
            {
                this.status = status;
                this.detail = detail;
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            initialWorker = new BackgroundWorker();
            initialWorker.WorkerReportsProgress = true;
            initialWorker.WorkerSupportsCancellation = false;
            initialWorker.DoWork += new DoWorkEventHandler(initialWorker_DoWork);
            initialWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(initialWorker_RunWorkerCompleted);
            initialWorker.ProgressChanged += new ProgressChangedEventHandler(initialWorker_ProgressChanged);
            initialWorker.RunWorkerAsync();
        }

        private void initialWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            initialStatusType initialStatus;


            initialStatus = new initialStatusType("读取用户数据...", "读取登录信息...");
            initialWorker.ReportProgress(0, initialStatus);
            if (SCEEC.Config.Users.getUsers().Count < 1)
            {
                ErrorReporter.ErrorReport(10001, "初始化", "");
            }
            Thread.Sleep(300);

            initialStatus = new initialStatusType("读取本地数据库...", "连接数据库...");
            initialWorker.ReportProgress(10, initialStatus);

            WorkingSets.local = new WorkingDB();
            if (!WorkingSets.local.Connect())
            {
                ErrorReporter.ErrorReport(10002, "初始化", "");
            }
            Thread.Sleep(300);

            initialStatus = new initialStatusType("读取本地数据库...", "读取位置数据表...");
            initialWorker.ReportProgress(20, initialStatus);
            if (!WorkingSets.local.refreshLocations())
            {
                ErrorReporter.ErrorReport(10004, "初始化", WorkingSets.local.LocalSQLClient.ErrorText);
            }
            Thread.Sleep(300);

            initialStatus = new initialStatusType("读取本地数据库...", "读取变压器数据表...");
            initialWorker.ReportProgress(30, initialStatus);
            if (!WorkingSets.local.updateTransformer())
            {
                ErrorReporter.ErrorReport(10004, "初始化", WorkingSets.local.LocalSQLClient.ErrorText);
            }
            Thread.Sleep(300);

            initialStatus = new initialStatusType("读取本地数据库...", "读取试验任务单...");
            initialWorker.ReportProgress(40, initialStatus);
            if (!WorkingSets.local.updateJob())
            {
                ErrorReporter.ErrorReport(10004, "初始化", WorkingSets.local.LocalSQLClient.ErrorText);
            }
            Thread.Sleep(300);

            initialStatus = new initialStatusType("读取本地数据库...", "读取试验结果数据...");
            initialWorker.ReportProgress(50, initialStatus);
            if (!WorkingSets.local.refreshTestResults())
            {
                ErrorReporter.ErrorReport(10004, "初始化", WorkingSets.local.LocalSQLClient.ErrorText);
            }
            Thread.Sleep(300);

            WorkingSets.local.CreateLocalDatabase();

            initialStatus = new initialStatusType("正在检索串口...", "正在链接仪器...");
            initialWorker.ReportProgress(90, initialStatus);
            WorkingSets.local.Tz3310 = new ClassTz3310();
            string pn= WorkingSets.local.Tz3310.AutoConnectMe;
            if (pn != null)
            {
                WorkingSets.local.Tz3310.OpenPort(pn, 115200, 8, 1);
                Thread.Sleep(500);
                try { WorkingSets.local.MethonID = WorkingSets.local.Tz3310.ReadMethonId(); } catch { }

                if (WorkingSets.local.MethonID == null)
                    WorkingSets.local.MethonID = "370001";

            }
            else
                try { WorkingSets.local.Tz3310.OpenPort(pn, 115200, 8, 1);
                    Thread.Sleep(500);
                    try { WorkingSets.local.MethonID = WorkingSets.local.Tz3310.ReadMethonId(); } catch { }
                    if (WorkingSets.local.MethonID == null)
                        WorkingSets.local.MethonID = "370001";
                } catch { }
            Thread.Sleep(300);


            initialStatus = new initialStatusType("正在加载...", "启动管理器...");
            initialWorker.ReportProgress(100, initialStatus);
            Thread.Sleep(500);
        }

        private void initialWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            inited = true;
            this.Close();
        }

        private void initialWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.LaunchProgressBar.Value = e.ProgressPercentage;
            this.StatusTextBlock.Text = ((initialStatusType)e.UserState).status;
            this.DetailTextBlock.Text = ((initialStatusType)e.UserState).detail;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!inited)
            {
                Environment.Exit(65535);
            }
        }
    }
}
