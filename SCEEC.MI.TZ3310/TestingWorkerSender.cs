﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data;

namespace SCEEC.MI.TZ3310
{
    public class TestingWorkerSender
    {
        public JobList job;
        public Transformer Transformer;
        public MeasurementItemStruct[] MeasurementItems;

        public int ProgressPercent;
        public int CurrentItemIndex;

        public string StatusText;

        public int RemainingItemsCount
        {
            get
            {
                return MeasurementItems.Length - CurrentItemIndex;
            }
        }

        private List<WrapPanel> list;
        public List<WrapPanel> GetList()
        {
            if (list != null) list.Clear();
            list = new List<WrapPanel>();
            for (int i = 0; i < MeasurementItems.Length; i++)
            {
                WrapPanel wp = new WrapPanel();
                if (i < CurrentItemIndex)
                {
                    if (MeasurementItems[i].Function == MeasurementFunction.Description)
                    {
                        wp.Children.Add(new Image() { Margin = new Thickness(5), Source = new BitmapImage(new Uri("Resources/Successed.png", UriKind.Relative)), Height = 18 });
                    }
                    else if (!MeasurementItems[i].failed)
                        wp.Children.Add(new Image() { Margin = new Thickness(5), Source = new BitmapImage(new Uri("Resources/Successed.png", UriKind.Relative)), Height = 18 });
                    else
                        wp.Children.Add(new Image() { Margin = new Thickness(5), Source = new BitmapImage(new Uri("Resources/Failed.png", UriKind.Relative)), Height = 18 });
                }
                else if (i == CurrentItemIndex)
                    wp.Children.Add(new Image() { Margin = new Thickness(5), Source = new BitmapImage(new Uri("Resources/Working.png", UriKind.Relative)), Height = 18 });
                else
                    wp.Children.Add(new Image() { Margin = new Thickness(5), Source = new BitmapImage(new Uri("Resources/Pending.png", UriKind.Relative)), Height = 18 });
                wp.Children.Add(new TextBlock() { Margin = new Thickness(5), Text = MeasurementItems[i].Description });
                list.Add(wp);
            }
            return list;
        }

        public System.Data.DataRow[] getDatabaseRows()
        {
            List<System.Data.DataRow> rows = new List<System.Data.DataRow>();
            foreach(var mi in MeasurementItems)
            {
                rows.Add(mi.ToDataRow(job));
            }
            return rows.ToArray();
        }

        public static TestingWorkerSender FromDatabaseRows(int testID)
        {
            TestingWorkerSender sender = new TestingWorkerSender();
            var rows = WorkingSets.local.TestResults.Select("testid = '" + testID.ToString() + "'");
            if (rows.Length > 0)
            {
                sender.Transformer = WorkingSets.local.getTransformer((int)rows[0]["transformerid"]);
                sender.job = WorkingSets.local.getJob((int)rows[0]["mj_id"]);
                List<MeasurementItemStruct> mlist = new List<MeasurementItemStruct>();
                MeasurementItemStruct m;
                foreach(var row in rows)
                {
                    m = new MeasurementItemStruct((MeasurementFunction)((int)row["function"]))
                    {
                        Winding = new WindingType((int)row["windingtype"]),
                        Terimal = WindingTerimal.FromList((List<int>)
                            (new SCEEC.Converter.GenericListTypeConverter<int>().ConvertFrom((string)row["terimal"]))),
                        WindingConfig = (TransformerWindingConfigName)(int)row["windingconfig"],
                        TapLabel = ((string)row["taplabel"]).Split(';'),
                        Text = (string)row["text"],
                        failed = (bool)row["failed"],
                        completed = (bool)row["completed"]
                    };
                    List<SCEEC.Numerics.PhysicalVariable> pvList = new List<Numerics.PhysicalVariable>();
                    for (int i = 0; i < 9; i++)
                    {
                        string pvstr = (string)row["result_pv" + (i + 1).ToString()];
                        if (pvstr.Length > 0)
                        {
                            pvList.Add(SCEEC.Numerics.NumericsConverter.Text2Value(pvstr));
                        }
                        else
                            i = 9;
                    }
                    m.Result = new MeasurementResult(
                        m.Function, 
                        pvList.ToArray(),
                        MeasurementItemStruct.Bytes2Shorts(Convert.FromBase64String((string)row["waves"])),
                        readyToTrigger: false, processing: false);
                    mlist.Add(m);
                }
                sender.MeasurementItems = mlist.ToArray();
            }
            return sender;
        }
    }

    public static class TestingWorkerUtility
    {
       // static TestingWorkUtility() { }

        /// <summary>
        /// 筛选测试反馈中最终输出结果的数组
        /// </summary>
        /// <param name="sender">测试传递结构</param>
        /// <returns>最终输出结果的数组</returns>
        public static MeasurementResult[] getFinalResults(TestingWorkerSender sender)
        {
            List<MeasurementResult> list = new List<MeasurementResult>();
            foreach (MeasurementItemStruct m in sender.MeasurementItems)
            {
                if ((m != null) && ((m.failed) || (m.completed == true)))
                {
                    list.Add(m.Result);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 筛选测试反馈中最终输出结果的数组的结果文本
        /// </summary>
        /// <param name="sender">测试传递结构</param>
        /// <returns>最终输出结果文本的数组</returns>
        public static string[] getFinalResultsText(TestingWorkerSender sender)
        {
            List<string> strlist = new List<string>();
            foreach (MeasurementItemStruct m in sender.MeasurementItems)
            {
                if ((m != null) && ((m.failed) || (m.completed == true)) && (m.Function != MeasurementFunction.Description))
                {
                    strlist.Add(m.ResultText);
                }
            }
            return strlist.ToArray();
        }
    }
}
