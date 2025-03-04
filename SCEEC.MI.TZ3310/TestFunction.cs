﻿using SCEEC.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace SCEEC.MI.TZ3310
{
    public static class TestFunction
    {
        static ClassTz3310 TZ3310 = new ClassTz3310();
        public static void DoDCInsulation(ref MeasurementItemStruct mi, Transformer transformer, JobList Job)
        {

            switch (mi.state)
            {
                case 0:
                    byte[] TestKindData = TZ3310.SetPraJydz(mi.Winding.ToJYDZstation(), GetParameter.GetPraDCInsulationVoltage(Job), 50, GetParameter.GetPraDCInsulationResistance(Job), GetParameter.GetPraDCInsulationAbsorptionRatio(Job), 0); ;
                    Thread.Sleep(100);
                    TZ3310.StartTest(TestKindData);
                    mi.stateText = "开始测量" + mi.Winding + "绝缘电阻中...";
                    mi.state++;
                    Thread.Sleep(4000);
                    break;
                case 1:
                    string[] Recbuffer = TZ3310.ReadTestData(Parameter.TestKind.绝缘电阻);
                    if (Recbuffer != null)
                    {
                        PhysicalVariable[] Volate = new PhysicalVariable[1];
                        if (Recbuffer[Recbuffer.Length - 1] == "0")
                        {
                            mi.Result = MeasurementResult.NewDCInsulationResult(mi, NumericsConverter.Text2Value(Recbuffer[0]), NumericsConverter.Text2Value(Recbuffer[1]),
                                NumericsConverter.Text2Value(Recbuffer[2]), null, false);
                            Volate[0] = NumericsConverter.Text2Value(Recbuffer[0]);
                        }
                            
                        else if (Recbuffer[Recbuffer.Length - 1] == "1")
                        {
                            mi.Result = MeasurementResult.NewDCInsulationResult(mi, Volate[0], NumericsConverter.Text2Value(Recbuffer[0]),
                               NumericsConverter.Text2Value(Recbuffer[1]), NumericsConverter.Text2Value(Recbuffer[2]), true);
                            mi.completed = true;
                            mi.stateText = "读取" + mi.Winding + "绝缘电阻结果成功";

                        }
                        else
                        {
                            //mi.failed = true;
                            mi.completed = true;

                            mi.stateText = mi.Winding + "错误类型：" + Recbuffer[0];
                        }
                    }
                    break;
            }
        }
        public static void Capacitance(ref MeasurementItemStruct mi, Transformer transformer, JobList Job)
        {
            switch (mi.state)
            {
                case 0:
                    byte[] TestKindData = TZ3310.SetPraJs(mi.Winding.ToJSstation(), Parameter.JSstyle.内接反接,
                        GetParameter.GetPraCapacitanceVoltage(Job), Parameter.JSFrequency._45To_55HZ, 0);
                    Thread.Sleep(100);
                    TZ3310.StartTest(TestKindData);
                    mi.stateText = "正在测试" + mi.Winding + "介损绕组中...";
                    mi.state++;
                    Thread.Sleep(4000);
                    break;
                case 1:
                    string[] Recbuffer = TZ3310.ReadTestData(Parameter.TestKind.介质损耗);
                    if (Recbuffer != null)
                    {
                        if (Recbuffer[Recbuffer.Length - 1] == "0")
                            mi.Result = MeasurementResult.NewCapacitanceResult(mi, NumericsConverter.Text2Value(Recbuffer[1]), null, null
                               , NumericsConverter.Text2Value(Recbuffer[2]), NumericsConverter.Text2Value(Recbuffer[3]), false);
                        else if (Recbuffer[Recbuffer.Length - 1] == "1")
                        {
                            mi.Result = MeasurementResult.NewCapacitanceResult(mi, NumericsConverter.Text2Value(Recbuffer[0]),
                                NumericsConverter.Text2Value(Recbuffer[1]), NumericsConverter.Text2Value(Recbuffer[2])
                              , NumericsConverter.Text2Value(GetParameter.GetFreQuency( Parameter.JSFrequency._45To_55HZ)), NumericsConverter.Text2Value(Recbuffer[3]), true);
                            mi.completed = true;
                            mi.stateText = "读取" + mi.Winding + "绕组介损测试成功";
                        }
                        else
                        {
                          //  mi.failed = true;
                            mi.completed = true;
                            mi.stateText = mi.Winding + "错误类型为：" + Recbuffer[0].ToString();
                        }
                    }
                    break;
            }
        }

        public static void DCResistance(ref MeasurementItemStruct mi, Transformer transformer, JobList Job)
        {
            if (mi.Terimal != null)
            {
                Parameter.ZldzStation Dcposition;
                if (mi.Winding == WindingType.HV)
                {

                    Dcposition = (Parameter.ZldzStation)(Parameter.ZldzStation.高压全部 + (((int)mi.Terimal[0]) % 4));//1

                }
                else if (mi.Winding == WindingType.MV)
                {
                    Dcposition = (Parameter.ZldzStation)(Parameter.ZldzStation.中压全部 + (((int)mi.Terimal[0]) % 4));//5
                }
                else
                {
                    Dcposition = (Parameter.ZldzStation)(Parameter.ZldzStation.低压全部 + (((int)mi.Terimal[0]) % 4));//9
                }
                switch (mi.state)
                {
                    case 0:

                        byte[] TestKindData = TZ3310.SetPraZldz((Parameter.ZldzWindingKind)mi.WindingConfig, Dcposition, GetParameter.GetPraDCResistanceCurrent(Job), 0);
                        Thread.Sleep(100);
                        TZ3310.StartTest(TestKindData);
                        WorkingSets.local.IsEnablestable = true;
                        mi.stateText = "正在测试" + mi.Winding + "直流电阻中...";
                        mi.state++;
                        Thread.Sleep(4000);
                        break;
                    case 1:
                        string[] Recbuffer = TZ3310.ReadTestData(Parameter.TestKind.直流电阻);
                        if (Recbuffer != null)
                        {
                            if (Recbuffer[Recbuffer.Length - 1] == "0")
                            {
                                PhysicalVariable[] Voltage = { NumericsConverter.Text2Value(Recbuffer[0]), NumericsConverter.Text2Value(Recbuffer[3]), NumericsConverter.Text2Value(Recbuffer[6]) };
                                PhysicalVariable[] Current = { NumericsConverter.Text2Value(Recbuffer[1]), NumericsConverter.Text2Value(Recbuffer[4]), NumericsConverter.Text2Value(Recbuffer[7]) };
                                PhysicalVariable[] Resistance = { NumericsConverter.Text2Value(Recbuffer[2]), NumericsConverter.Text2Value(Recbuffer[5]), NumericsConverter.Text2Value(Recbuffer[8]) };
                                mi.Result = MeasurementResult.NewDCResistanceResult(mi, Voltage, Current, Resistance, false);

                                mi.state++;//测量结束数据需要确定
                                mi.stateText = "读取" + mi.Winding + "直流电阻数据中...";//临时
                                Thread.Sleep(500);
                            }
                            else
                            {
                              //  mi.failed = true;
                                mi.completed = true;
                                mi.stateText = mi.Winding + "错误类型：" + Recbuffer[0].ToString();
                            }
                        }
                        break;
                    case 2:
                        string[] Recbuffer1 = TZ3310.ReadTestData(Parameter.TestKind.直流电阻);
                        if (Recbuffer1 != null)
                        {
                            if (Recbuffer1[Recbuffer1.Length - 1] == "0")
                            {
                                PhysicalVariable[] Voltage = { NumericsConverter.Text2Value(Recbuffer1[0]), NumericsConverter.Text2Value(Recbuffer1[3]), NumericsConverter.Text2Value(Recbuffer1[6]) };
                                PhysicalVariable[] Current = { NumericsConverter.Text2Value(Recbuffer1[1]), NumericsConverter.Text2Value(Recbuffer1[4]), NumericsConverter.Text2Value(Recbuffer1[7]) };
                                PhysicalVariable[] Resistance = { NumericsConverter.Text2Value(Recbuffer1[2]), NumericsConverter.Text2Value(Recbuffer1[5]), NumericsConverter.Text2Value(Recbuffer1[8]) };
                                mi.Result = MeasurementResult.NewDCResistanceResult(mi, Voltage, Current, Resistance, false);
                                if (WorkingSets.local.IsStable == true)
                                {
                                    mi.Result = MeasurementResult.NewDCResistanceResult(mi, Voltage, Current, Resistance, true);
                                    TZ3310.InterRuptMe(Parameter.CommanTest.判断直流电阻稳定状态);
                                    Thread.Sleep(500);
                                    WorkingSets.local.IsStable = false;
                                    mi.stateText = "确定" + mi.Winding + "直流电阻稳定成功";//临时
                                    Thread.Sleep(500);
                                    mi.stateText = "读取" + mi.Winding + "直流电阻数据成功";//临时
                                    mi.state++;
                                }
                            }
                            else
                            {
                               // mi.failed = true;
                                mi.completed = true;
                                mi.stateText = mi.Winding + "错误类型：" + Recbuffer1[0].ToString();//临时
                            }
                        }
                        break;
                    case 3:
                        string[] Recbuffer2 = TZ3310.ReadTestData(Parameter.TestKind.直流电阻);
                        if (Recbuffer2 != null)
                        {
                            if (Recbuffer2[Recbuffer2.Length - 1] == "1")
                            {
                                mi.stateText = mi.Winding + "直流电阻测试完成：";
                                mi.completed = true;
                            }
                            else
                            {
                               // mi.failed = true;
                                mi.completed = true;
                                mi.stateText = mi.Winding + "错误类型：" + Recbuffer2[0].ToString();//临时

                            }
                        }
                        break;
                }
            }
            else
            {
                switch (mi.state)
                {
                    case 0:
                        byte[] TestKindData = TZ3310.SetPraZldz((Parameter.ZldzWindingKind)mi.WindingConfig, mi.Winding.TozldzStation(), GetParameter.GetPraDCResistanceCurrent(Job), 0);
                        Thread.Sleep(100);
                        TZ3310.StartTest(TestKindData);
                        WorkingSets.local.IsEnablestable = true;
                        mi.stateText = "正在测试" + mi.Winding.TozldzStation() + "直流电阻中...";
                        mi.state++;
                        Thread.Sleep(4000);
                        break;
                    case 1:
                        string[] Recbuffer = TZ3310.ReadTestData(Parameter.TestKind.直流电阻);
                        if (Recbuffer != null)
                        {
                            if (Recbuffer[Recbuffer.Length - 1] == "0")
                            {
                                PhysicalVariable[] Voltage = { NumericsConverter.Text2Value(Recbuffer[0]), NumericsConverter.Text2Value(Recbuffer[3]), NumericsConverter.Text2Value(Recbuffer[6]) };
                                PhysicalVariable[] Current = { NumericsConverter.Text2Value(Recbuffer[1]), NumericsConverter.Text2Value(Recbuffer[4]), NumericsConverter.Text2Value(Recbuffer[7]) };
                                PhysicalVariable[] Resistance = { NumericsConverter.Text2Value(Recbuffer[2]), NumericsConverter.Text2Value(Recbuffer[5]), NumericsConverter.Text2Value(Recbuffer[8]) };
                                mi.Result = MeasurementResult.NewDCResistanceResult(mi, Voltage, Current, Resistance, false);
                                mi.state++;
                                mi.stateText = "等待" + mi.Winding + "直流电阻稳定中...";
                            }
                            else
                            {
                               // mi.failed = true;
                                mi.completed = true;
                                mi.stateText = mi.Winding + "错误类型" + Recbuffer[0].ToString();
                            }
                        }
                        break;
                    case 2:
                        string[] Recbuffer1 = TZ3310.ReadTestData(Parameter.TestKind.直流电阻);
                        if (Recbuffer1 != null)
                        {
                            if (Recbuffer1[Recbuffer1.Length - 1] == "0")
                            {
                                PhysicalVariable[] Voltage = { NumericsConverter.Text2Value(Recbuffer1[0]), NumericsConverter.Text2Value(Recbuffer1[3]), NumericsConverter.Text2Value(Recbuffer1[6]) };
                                PhysicalVariable[] Current = { NumericsConverter.Text2Value(Recbuffer1[1]), NumericsConverter.Text2Value(Recbuffer1[4]), NumericsConverter.Text2Value(Recbuffer1[7]) };
                                PhysicalVariable[] Resistance = { NumericsConverter.Text2Value(Recbuffer1[2]), NumericsConverter.Text2Value(Recbuffer1[5]), NumericsConverter.Text2Value(Recbuffer1[8]) };
                                mi.Result = MeasurementResult.NewDCResistanceResult(mi, Voltage, Current, Resistance, false);
                                if (WorkingSets.local.IsStable == true)
                                {
                                    mi.Result = MeasurementResult.NewDCResistanceResult(mi, Voltage, Current, Resistance, true);
                                    TZ3310.InterRuptMe(Parameter.CommanTest.判断直流电阻稳定状态);
                                    WorkingSets.local.IsStable = false;
                                    mi.stateText = "确定" + mi.Winding + "直流电阻稳定成功";
                                    Thread.Sleep(500);
                                    mi.stateText = "读取" + mi.Winding + "直流电阻数据成功";
                                    mi.completed = true;
                                }
                            }
                        }
                        break;
                }
            }
        }
        public static void BushingDCInsulation(ref MeasurementItemStruct mi, Transformer transformer, JobList Job)
        {
            Parameter.JYDZstation position;
            if (mi.Winding == WindingType.HV)
            {
                position = (Parameter.JYDZstation)(Parameter.JYDZstation.高压套管A + (((int)mi.Terimal[0] + 3) % 4));
            }
            else
            {
                position = (Parameter.JYDZstation)(Parameter.JYDZstation.中压套管A + (((int)mi.Terimal[0] + 3) % 4));
            }
            switch (mi.state)
            {
                case 0:
                    byte[] TestKindData = TZ3310.SetPraJydz(position,GetParameter.GetPraDCInsulationVoltage(Job), 50, GetParameter.GetPraDCInsulationResistance(Job), GetParameter.GetPraDCInsulationAbsorptionRatio(Job), 0);
                    Thread.Sleep(100);
                    TZ3310.StartTest(TestKindData);
                    mi.stateText = "正在测试" + position.ToString() + "末屏中...";
                    mi.state++;
                    Thread.Sleep(4000);
                    break;
                case 1:
                    string[] Recbuffer = TZ3310.ReadTestData(Parameter.TestKind.绝缘电阻);
                    if (Recbuffer != null)
                    {
                        PhysicalVariable[] Volate = new PhysicalVariable[1];

                        if (Recbuffer[Recbuffer.Length - 1] == "0")
                            mi.Result = MeasurementResult.NewBushingDCInsulationResult(mi, NumericsConverter.Text2Value(Recbuffer[0]), NumericsConverter.Text2Value(Recbuffer[1]),
                                NumericsConverter.Text2Value(Recbuffer[2]), null, false);
                        else if (Recbuffer[Recbuffer.Length - 1] == "1")
                        {
                            mi.Result = MeasurementResult.NewBushingDCInsulationResult(mi, Volate[0], NumericsConverter.Text2Value(Recbuffer[0]),
                               NumericsConverter.Text2Value(Recbuffer[1]), NumericsConverter.Text2Value(Recbuffer[2]), true);
                            mi.state++;
                            mi.completed = true;
                            mi.stateText = position.ToString() + "末屏测试完成";
                        }
                        else
                        {
                            mi.completed = true;
                            mi.stateText = mi.Winding + "错误类型：" + Recbuffer[0];
                        }
                    }
                    break;
            }
        }

        public static void BushingCapacitance(ref MeasurementItemStruct mi, Transformer transformer, JobList Job)
        {
            Parameter.JSstation Jsposition;
            if (mi.Winding == WindingType.HV)
            {
                Jsposition = Parameter.JSstation.高压套管A + (((int)mi.Terimal[0] + 3) % 4);
            }
            else
            {
                Jsposition = Parameter.JSstation.中压套管A + (((int)mi.Terimal[0] + 3) % 4);
            }
            switch (mi.state)
            {
                case 0:
                    byte[] TestKindData = TZ3310.SetPraJs(Jsposition, Parameter.JSstyle.内接正接, GetParameter.GetPraCapacitanceVoltage(Job), Parameter.JSFrequency._45To_55HZ, 0);
                    Thread.Sleep(100);
                    TZ3310.StartTest(TestKindData);
                    mi.stateText = "正在测试" + mi.Winding + "末屏中...";
                    mi.state++;
                    Thread.Sleep(4000);
                    break;
                case 1:
                    string[] Recbuffer = TZ3310.ReadTestData(Parameter.TestKind.介质损耗);
                    if (Recbuffer != null)
                    {
                        if (Recbuffer[Recbuffer.Length - 1] == "0")
                            mi.Result = MeasurementResult.NewBushingCapacitanceResult(mi, NumericsConverter.Text2Value(Recbuffer[1]), null, null
                               , NumericsConverter.Text2Value(Recbuffer[2]), NumericsConverter.Text2Value(Recbuffer[3]), false);
                        else if (Recbuffer[Recbuffer.Length - 1] == "1")
                        {
                            mi.Result = MeasurementResult.NewBushingCapacitanceResult(mi, NumericsConverter.Text2Value(Recbuffer[0]),
                                NumericsConverter.Text2Value(Recbuffer[1]), NumericsConverter.Text2Value(Recbuffer[2])
                              , NumericsConverter.Text2Value(GetParameter.GetFreQuency(Parameter.JSFrequency._45To_55HZ)), NumericsConverter.Text2Value(Recbuffer[3]), true);
                            mi.completed = true;
                            mi.stateText = "读取" + mi.Winding + "末屏测试完成";
                        }
                        else
                        {
                           // mi.failed = true;
                            mi.completed = true;
                            mi.stateText = mi.Winding + "错误类型：" + Recbuffer[0].ToString();
                        }
                    }
                    break;

            }
        }
        public static void OLTCSwitchingCharacter(ref MeasurementItemStruct mi, Transformer transformer, JobList Job)
        {
            Parameter.YzfjStation yzfjStation;
            if (mi.Winding == WindingType.HV)
            {
                yzfjStation = Parameter.YzfjStation.高压侧;
            }
            else
            {
                yzfjStation = Parameter.YzfjStation.中压侧;
            }
            switch (mi.state)
            {
                case 0:
                    byte[] TestKindData = TZ3310.SetPraYzfj((Parameter.YzfjWindingKind)mi.WindingConfig, yzfjStation, Parameter.yzfjTap._1To_2, Parameter.YzfjCurrent._1_A, 5, 5, 0);
                    Thread.Sleep(100);
                    TZ3310.StartTest(TestKindData);
                    mi.stateText = "正在测试" + mi.Winding + "有载分接中...";
                    mi.state++;
                    Thread.Sleep(4000);
                    break;
                case 1:
                    string[] Recbuffer = TZ3310.ReadTestData(Parameter.TestKind.有载分接);
                    if (Recbuffer != null)
                    {
                        bool ReadforT;
                        if (Recbuffer[0] == "1") ReadforT = true;
                        else ReadforT = false;
                        if (Recbuffer.Length == 7)
                        {

                            PhysicalVariable[] Voltage = { NumericsConverter.Text2Value(Recbuffer[1]), NumericsConverter.Text2Value(Recbuffer[3]), NumericsConverter.Text2Value(Recbuffer[5]) };//135
                            PhysicalVariable[] current = { NumericsConverter.Text2Value(Recbuffer[2]), NumericsConverter.Text2Value(Recbuffer[4]), NumericsConverter.Text2Value(Recbuffer[6]) };//246
                            PhysicalVariable[] Resistans = new PhysicalVariable[3];
                            Resistans[0] = NumericsConverter.Text2Value("0.005");
                            Resistans[1] = NumericsConverter.Text2Value("0.005");
                            Resistans[2] = NumericsConverter.Text2Value("0.005");
                            mi.Result = MeasurementResult.NewOLTCSwitchingCharacterResult(mi, Voltage, current, Resistans, null, ReadforT, false);
                            mi.stateText = "读取" + mi.Winding + "有载分接充电中...";
                            if (ReadforT)//可以触发
                            {
                                mi.stateText =  mi.Winding + "有载分接等待触发。。。";
                                Thread.Sleep(500);
                                mi.state++;
                            }
                        }
                        else
                        {
                          //  mi.failed = true;
                            mi.completed = true;
                            mi.stateText = "充电时错误：" + Recbuffer[0].ToString();

                        }
                    }
                    break;
                case 2:
                    string[] Recbuffer1 = TZ3310.ReadTestData(Parameter.TestKind.有载分接);
                    if (Recbuffer1 != null)
                    {
                        bool ReadforR;
                        if (Recbuffer1[0] == "2") ReadforR = true;
                        else ReadforR = false;
                        if (Recbuffer1.Length == 7)
                        {
                            PhysicalVariable[] Voltage = { NumericsConverter.Text2Value(Recbuffer1[1]), NumericsConverter.Text2Value(Recbuffer1[3]), NumericsConverter.Text2Value(Recbuffer1[5]) };//135
                            PhysicalVariable[] current = { NumericsConverter.Text2Value(Recbuffer1[2]), NumericsConverter.Text2Value(Recbuffer1[4]), NumericsConverter.Text2Value(Recbuffer1[6]) };//246
                            if (ReadforR)//触发成功
                            {
                                mi.Result = MeasurementResult.NewOLTCSwitchingCharacterResult(mi, Voltage, current, new PhysicalVariable[3], null, ReadforR, true);

                                mi.stateText = "读取" + mi.Winding + "触发成功";
                                mi.state++;
                            }
                        }

                        else if (Recbuffer1.Length == 1)
                        {
                           // mi.failed = true;
                            mi.completed = true;
                            mi.stateText = mi.Winding + "错误类型：" + Recbuffer1[0].ToString();
                        }
                    }
                    break;
                case 3:

                    mi.stateText = "正在读取波形中。。。";
                    Thread.Sleep(8000);
                    mi.state++;
                    break;
                
                case 4:
                    var Waveform = TZ3310.GetWaveFormData;//5s
                    if (Waveform != null)
                    {
                        mi.Result = MeasurementResult.NewOLTCSwitchingCharacterResult(mi, new PhysicalVariable[3], new PhysicalVariable[3],
                            new PhysicalVariable[3], Waveform, false, true);
                        mi.state++;
                        WorkingSets.local.WaveFormSwicth = Waveform;
                        Thread.Sleep(50);
                        WorkingSets.local.ShowWaveForm = true;
                        mi.stateText = mi.Winding + "波形读取完成";
                    }
                    else
                    {
                        // mi.state++;
                    //    mi.failed = true;
                        mi.completed = true;
                        mi.stateText = mi.Winding + "未读取到波形";
                    }
                    break;
                case 5:
                    mi.completed = true;
                    break;
            }
        }

        public static void Information(ref MeasurementItemStruct mi, Transformer transformer, JobList Job)
        {
            mi.Result = MeasurementResult.NewInformation(mi, ConvertData(Job.Information.ToString()), true);
            mi.completed = true;
        }
        public static void DCCharge(ref MeasurementItemStruct mi, Transformer transformer, JobList Job)
        { }

        public static bool Closecurrent(int Idetify)
        {
              return TZ3310.ShutDownOutCurrent(Idetify);
        }

        public static bool CancalWork(ref TestingWorkerSender sender)
        {
            if (sender.MeasurementItems[sender.CurrentItemIndex].Function == MeasurementFunction.DCResistance ||
              sender.MeasurementItems[sender.CurrentItemIndex].Function == MeasurementFunction.OLTCSwitchingCharacter)
            {

                if (true == TZ3310.ShutDownOutCurrent(0))
                {
                    string[] OutData = TZ3310.ReadTestData(Parameter.TestKind.读取放电数据);
                    if (OutData != null)
                    {
                        if (OutData[0] == "2")
                        {
                            sender.StatusText = "放电完成";
                            return true;
                        }
                        else
                        {
                            sender.StatusText = "正在放电中...";
                            return false;
                        }
                    }
                }
                else
                {
                    sender.StatusText = "关闭电流输出失败";
                    return false;

                }

            }

            else
            {
                if (true == TZ3310.InterRuptMe(Parameter.CommanTest.仪器复位))
                {
                    sender.StatusText = "仪器复位成功";
                    return true;
                }
                else
                    return false;

            }

            return false;
        }
      
        public static short[] ConvertData(string text)
        {
            char[] tempdata = text.ToArray();
            short[] Data = new short[tempdata.Length];
            for (int i = 0; i < tempdata.Length; i++)
            {
                Data[i] = Convert.ToInt16(tempdata[i]);
            }
            return Data;

        }

        public static string MethonID()
        {

            return TZ3310.ReadMethonId();
            
        }
    }
}