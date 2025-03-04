﻿using SCEEC.NET;
using SCEEC.Numerics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{

    public class ClassTz3310
    {

        //  Unsafe UnsafeData = new Unsafe();
        static SerialClass sc = new SerialClass();
        public List<byte> WaveformData = new List<byte>();

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="comPortName">COM口</param>
        /// <param name="baudRate">比特率</param>
        /// <param name="dataBits">数据为</param>
        /// <param name="stopBits">停止位</param>
        /// <returns></returns>
        public bool OpenPort(string comPortName, int baudRate, int dataBits, int stopBits)
        {
            bool IsSuccess = true;
            sc.closePort();
            try
            {
                sc.setSerialPort(comPortName, baudRate, dataBits, stopBits);
                sc.openPort();
                sc.DataReceived += new SerialClass.SerialPortDataReceiveEventArgs(Screceivee);


            }
            catch (Exception)
            {
                return !IsSuccess;

            }

            return IsSuccess;
        }

        public string[] GetPortNames()
        {
            return sc.getSerials();

        }
        public void Closeport()
        {
            sc.closePort();

        }
        byte[] WaveFormBuffer = new byte[48010];
        public object Lokeer = new object();
        public void Screceivee(object sender, SerialDataReceivedEventArgs e, byte[] bits)
        {
            //lock(Lokeer)
            //{
            //    File.AppendAllText("TempDataWaveFormData1.txt", Encoding.ASCII.GetString(bits));

            //}

            //FileStream fs = new FileStream("D:\\1.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            //StringBuilder sb = new StringBuilder();
            //while (!sr.EndOfStream)
            //{
            //    sb.AppendLine(sr.ReadLine());

            //}
            this.WaveformData.AddRange(bits);
            //if(WaveformData.Count==48010)
            //{
            //    WaveformData.CopyTo(WaveFormBuffer);
            //}
            //ByteToFile(bits, "D:\\1.txt");
            //await DeelWaveData(bits);
        }



       
        //通讯查询
        /// <summary>
        /// 通讯查询true通讯查询成功
        /// </summary>
        /// <param name="Mark">标志位</param>
        /// <returns>true</returns>
        public bool CommunicationQuery(byte Mark)
        {
            byte[] RecBuffer = new byte[3];
            int checkData = Mark + 0x01;
            byte[] buffer = { 0x01, Mark, (byte)checkData };
            try
            {
                int temp = sc.SendCommand(buffer, ref RecBuffer, 50);

                if (temp <= 0)
                    return false;
                if (RecBuffer[0] == 0xac && RecBuffer[1] == Mark)
                {
                    return true;

                }
                else if (RecBuffer[0] == 0xee && RecBuffer[1] == 0xee)
                {
                    return false;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
            // return RecBuffer;
            // return false;
        }


        //校验和
        /// <summary>
        /// 校验和
        /// </summary>
        /// <param name="Mark">处理数据</param>
        /// <returns>返回校验和数据</returns>
        public byte CheckData(byte[] checkdata)
        {
            byte[] tempD = new byte[checkdata.Length - 1];
            for (int i = 0; i < checkdata.Length - 1; i++)
            {
                tempD[i] = checkdata[i];
            }
            // byte[] tempD = checkdata;

            byte Endcheckdata = 0;
            foreach (byte outd in tempD)
            {
                Endcheckdata += outd;
            }

            return Endcheckdata;
        }




        //reture 介质损耗测试命令生成
        /// <summary>
        /// 介质损耗测试命令生成
        /// </summary>
        /// <param name="jsstation">介质损耗实验位置</param>
        /// <param name="jsstyle">实验模式</param>
        /// <param name="jsvoit">试验电压</param>
        /// <param name="jsfre">实验频率</param>
        /// <param name="Identification">标志位</param>
        /// <returns>返回介质损耗参数数组</returns>
        public byte[] SetPraJs(Parameter.JSstation jsstation, Parameter.JSstyle jsstyle, Parameter.JSVoilt jsvoit,
            Parameter.JSFrequency jsfre, int Identification)
        {
            byte[] Pra = new byte[7];
            Pra[0] = (byte)0x10;
            Pra[1] = (byte)jsstyle;
            Pra[2] = (byte)jsstation;
            Pra[3] = (byte)jsvoit;
            Pra[4] = (byte)jsfre;
            Pra[5] = (byte)Identification;
            Pra[6] = CheckData(Pra);
            return Pra;


        }

        public byte[] ISetPraJs(int jsstation, int jsstyle, int jsvoit,
           int jsfre, int Identification)
        {
            byte[] Pra = new byte[7];
            Pra[0] = (byte)0x10;
            Pra[1] = (byte)jsstation;
            Pra[2] = (byte)jsstyle;
            Pra[3] = (byte)jsvoit;
            Pra[4] = (byte)jsfre;
            Pra[5] = (byte)Identification;
            Pra[6] = CheckData(Pra);
            return Pra;


        }

        //设置绝缘电阻参数
        /// <summary>
        /// 设置绝缘电阻参数
        /// </summary>
        /// <param name="jYDZstation">绝缘电阻实验位置</param>
        /// <param name="jYDZVoilt">绝缘电阻实验位置</param>
        /// <param name="ProtectRes">保护电阻10K倍数小于100</param>
        /// <param name="JydzLowLimite">绝缘电阻下限（小于100）</param>
        /// <param name="AbsLowLimite">吸收比下限（100-150）</param>
        /// <returns>返回绝缘电阻参数数组</returns>
        public byte[] SetPraJydz(Parameter.JYDZstation jYDZstation, Parameter.JYDZVoilt jYDZVoilt,
            int ProtectRes, double JydzLowLimite, double AbsLowLimite, int Identification)
        {

            byte[] Pra = new byte[8];
            Pra[0] = 0x20;
            Pra[1] = (byte)jYDZstation;
            Pra[2] = (byte)jYDZVoilt;
            Pra[3] = (byte)ProtectRes;
            Pra[4] = (byte)JydzLowLimite;
            Pra[5] = (byte)AbsLowLimite;
            Pra[6] = (byte)Identification;
            Pra[7] = CheckData(Pra);
            return Pra;


            // return null;
        }

        public byte[] ISetPraJydz(int jYDZstation, int jYDZVoilt,
            int ProtectRes, int JydzLowLimite, int AbsLowLimite, int Identification)
        {
            if (ProtectRes >= 0 && ProtectRes <= 100 && JydzLowLimite >= 0 && JydzLowLimite <= 100 && AbsLowLimite >= 100 && AbsLowLimite <= 150)
            {
                byte[] Pra = new byte[8];
                Pra[0] = (byte)0x20;
                Pra[1] = (byte)jYDZstation;
                Pra[2] = (byte)jYDZVoilt;
                Pra[3] = (byte)ProtectRes;
                Pra[4] = (byte)JydzLowLimite;
                Pra[5] = (byte)AbsLowLimite;
                Pra[6] = (byte)Identification;
                Pra[7] = CheckData(Pra);
                return Pra;
            }

            return null;
        }
        //设置直流电阻参数
        /// <summary>
        /// 设置直流电阻参数
        /// </summary>
        /// <param name="zldzWindingKind">绕组类型</param>
        /// <param name="zldzStation">直流电阻测量位置</param>
        /// <param name="zldzCurrent">直流电阻实验电流</param>
        /// <returns>返回直流电阻参数数组</returns>
        public byte[] SetPraZldz(Parameter.ZldzWindingKind zldzWindingKind, Parameter.ZldzStation zldzStation,
            Parameter.ZldzCurrent zldzCurrent, int Identification)
        {
            byte[] Pra = new byte[6];
            Pra[0] = 0x30;
            Pra[1] = (byte)zldzWindingKind;
            Pra[2] = (byte)zldzStation;
            Pra[3] = (byte)zldzCurrent;
            Pra[4] = (byte)Identification;
            Pra[5] = CheckData(Pra);
            return Pra;

        }
        public byte[] ISetPraZldz(int zldzWindingKind, int zldzStation,
           int zldzCurrent, int Identification)
        {
            byte[] Pra = new byte[6];
            Pra[0] = 0x30;
            Pra[1] = (byte)zldzWindingKind;
            Pra[2] = (byte)zldzStation;
            Pra[3] = (byte)zldzCurrent;
            Pra[4] = (byte)Identification;
            Pra[5] = CheckData(Pra);
            return Pra;

        }


        /// <summary>
        /// 设置有载分接参数
        /// </summary>
        /// <param name="yzfjWindingKind">绕组类型</param>
        /// <param name="yzfjStation">有载分接测量位置</param>
        /// <param name="yzfjTap">实验分接</param>
        /// <param name="TriRes">触发电阻</param>
        /// <param name="TriTimeer">触发时间</param>
        /// <param name="">实验电流</param>
        /// <param name="Identification">标志位</param>
        /// <returns>返回有载分接参数数组</returns>
        public byte[] SetPraYzfj(Parameter.YzfjWindingKind yzfjWindingKind, Parameter.YzfjStation yzfjStation,
            Parameter.yzfjTap yzfjTap, Parameter.YzfjCurrent yzfjCurrent, int TriRes, int TriTimeer, int Identification)
        {
            if (TriRes >= 3 && TriRes <= 200 && TriTimeer >= 1 && TriTimeer <= 100)
            {
                byte[] Pra = new byte[9];
                Pra[0] = 0x40;
                Pra[1] = (byte)yzfjWindingKind;
                Pra[2] = (byte)yzfjStation;
                Pra[3] = (byte)yzfjTap;
                Pra[4] = (byte)TriRes;
                Pra[5] = (byte)TriTimeer;
                Pra[6] = (byte)yzfjCurrent;//实验电流
                Pra[7] = (byte)Identification;
                Pra[8] = CheckData(Pra);
                return Pra;
            }
            return null;
        }
        public byte[] ISetPraYzfj(int yzfjWindingKind, int yzfjStation,
           int yzfjTap, int TriRes, int TriTimeer, int Identification)
        {
            if (TriRes >= 3 && TriRes <= 200 && TriTimeer >= 1 && TriTimeer <= 100)
            {
                byte[] Pra = new byte[9];
                Pra[0] = 0x40;
                Pra[1] = (byte)yzfjWindingKind;
                Pra[2] = (byte)yzfjStation;
                Pra[3] = (byte)yzfjTap;
                Pra[4] = (byte)TriRes;
                Pra[5] = (byte)TriTimeer;
                Pra[6] = 1;//实验电流
                Pra[7] = (byte)Identification;
                Pra[8] = CheckData(Pra);
                return Pra;
            }
            return null;
        }

        //fd aa fd
        public string ReadMethonId()
        {
             
            byte[] RecMethonId = new byte[50]; ;
            sc.SendCommand(new byte[] { 0xfd, 0xaa, 0xfd }, ref RecMethonId, 20);
            string temp = Encoding.Default.GetString(RecMethonId);
            return temp.Replace("\0", ""); ;

        }


        //读取测量数据
        /// <summary>
        /// 读取测量数据和数据的处理
        /// </summary>
        /// <param name="testkind">测试类型</param>
        /// <returns>回复解析完成的数据</returns>
        public string[] ReadTestData(Parameter.TestKind testkind)
        {

            if (testkind == Parameter.TestKind.介质损耗)
            {
                //string[] RetData = new string[5];
                byte[] SendComman = { 0x12 };
                byte[] RecBuffer = new byte[31];
                sc.SendCommand(SendComman, ref RecBuffer, 50);

                if (RecBuffer[0] == 0xfa)
                {
                    string[] RetData = new string[5];
                    RetData[0] = RecBuffer[1].ToString();
                    PhysicalVariable pv = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(2).Take(7).ToArray()));
                    RetData[1] = pv.ToString();
                    PhysicalVariable pv1 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(9).Take(7).ToArray()));
                    RetData[2] = pv1.ToString();
                    PhysicalVariable pv2 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(16).Take(7).ToArray()));
                    RetData[3] = pv2.ToString();
                    RetData[4] = "0";
                    return RetData;

                }
                if (RecBuffer[0] == 0xff)
                {

                    string[] RetData = new string[5];
                    PhysicalVariable pv = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(2).Take(7).ToArray()));
                    RetData[0] = pv.ToString();
                    PhysicalVariable pv1 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(9).Take(7).ToArray()));
                    RetData[1] = pv1.ToString();
                    PhysicalVariable pv2 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(16).Take(7).ToArray()) + "%");
                    RetData[2] = pv2.ToString(percentage: true, positiveSign: true);
                    PhysicalVariable pv3 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(23).Take(7).ToArray()));
                    RetData[3] = pv3.ToString();
                    RetData[4] = "1";

                    return RetData;
                }
                if (RecBuffer[0] == 0xee)
                {
                    string[] Rd = new string[1];
                    Rd[0] = TestErrDC(RecBuffer[1].ToString());
                    return Rd;//错误类型
                }


            }
            if (testkind == Parameter.TestKind.直流电阻)
            {
                byte[] Sendbuffer = { 0x32 };
                byte[] RecBuffer = new byte[75];
                try
                {
                    sc.SendCommand(Sendbuffer, ref RecBuffer, 10);
                    //if (CheckData(RecData) == RecData[74])
                    //{
                    if (RecBuffer[0] == 0xfa)
                    {

                        string[] RetData = new string[10];
                        RetData[0] = Encoding.ASCII.GetString(RecBuffer.Skip(2).Take(8).ToArray());
                        PhysicalVariable pv = NumericsConverter.Text2Value(RetData[0]);
                        RetData[0] = pv.ToString();
                        RetData[1] = Encoding.ASCII.GetString(RecBuffer.Skip(10).Take(8).ToArray());
                        PhysicalVariable pv1 = NumericsConverter.Text2Value(RetData[1]);
                        RetData[1] = pv1.ToString();
                        RetData[2] = Encoding.ASCII.GetString(RecBuffer.Skip(18).Take(8).ToArray()).Replace("$", "Ω");
                        PhysicalVariable pv2 = NumericsConverter.Text2Value(RetData[2]);
                        RetData[2] = pv2.ToString();

                        RetData[3] = Encoding.ASCII.GetString(RecBuffer.Skip(26).Take(8).ToArray());
                        PhysicalVariable pv3 = NumericsConverter.Text2Value(RetData[3]);
                        RetData[3] = pv3.ToString();

                        RetData[4] = Encoding.ASCII.GetString(RecBuffer.Skip(34).Take(8).ToArray());
                        PhysicalVariable pv4 = NumericsConverter.Text2Value(RetData[4]);
                        RetData[4] = pv4.ToString();
                        RetData[5] = Encoding.ASCII.GetString(RecBuffer.Skip(42).Take(8).ToArray()).Replace("$", "Ω");
                        PhysicalVariable pv5 = NumericsConverter.Text2Value(RetData[5]);
                        RetData[5] = pv5.ToString();

                        RetData[6] = Encoding.ASCII.GetString(RecBuffer.Skip(50).Take(8).ToArray());
                        PhysicalVariable pv6 = NumericsConverter.Text2Value(RetData[6]);
                        RetData[6] = pv6.ToString();

                        RetData[7] = Encoding.ASCII.GetString(RecBuffer.Skip(58).Take(8).ToArray());
                        PhysicalVariable pv7 = NumericsConverter.Text2Value(RetData[7]);
                        RetData[7] = pv7.ToString();
                        RetData[8] = Encoding.ASCII.GetString(RecBuffer.Skip(66).Take(8).ToArray()).Replace("$", "Ω");
                        PhysicalVariable pv8 = NumericsConverter.Text2Value(RetData[8]);
                        RetData[8] = pv8.ToString();

                        RetData[9] = "0";
                        return RetData;


                    }
                    else if (RecBuffer[0] == 0xff)
                    {

                        string[] RetData = new string[10];
                        RetData[0] = Encoding.ASCII.GetString(RecBuffer.Skip(2).Take(8).ToArray());
                        PhysicalVariable pv = NumericsConverter.Text2Value(RetData[0]);
                        RetData[0] = pv.ToString();
                        RetData[1] = Encoding.ASCII.GetString(RecBuffer.Skip(10).Take(8).ToArray());
                        PhysicalVariable pv1 = NumericsConverter.Text2Value(RetData[1]);
                        RetData[1] = pv1.ToString();
                        RetData[2] = Encoding.ASCII.GetString(RecBuffer.Skip(18).Take(8).ToArray()).Replace("$", "Ω");
                        PhysicalVariable pv2 = NumericsConverter.Text2Value(RetData[2]);
                        RetData[2] = pv2.ToString();
                        RetData[3] = Encoding.ASCII.GetString(RecBuffer.Skip(26).Take(8).ToArray());
                        PhysicalVariable pv3 = NumericsConverter.Text2Value(RetData[3]);
                        RetData[3] = pv3.ToString();
                        RetData[4] = Encoding.ASCII.GetString(RecBuffer.Skip(34).Take(8).ToArray());
                        PhysicalVariable pv4 = NumericsConverter.Text2Value(RetData[4]);
                        RetData[4] = pv4.ToString();
                        RetData[5] = Encoding.ASCII.GetString(RecBuffer.Skip(42).Take(8).ToArray()).Replace("$", "Ω");
                        PhysicalVariable pv5 = NumericsConverter.Text2Value(RetData[5]);
                        RetData[5] = pv5.ToString();
                        RetData[6] = Encoding.ASCII.GetString(RecBuffer.Skip(50).Take(8).ToArray());
                        PhysicalVariable pv6 = NumericsConverter.Text2Value(RetData[6]);
                        RetData[6] = pv6.ToString();
                        RetData[7] = Encoding.ASCII.GetString(RecBuffer.Skip(58).Take(8).ToArray());
                        PhysicalVariable pv7 = NumericsConverter.Text2Value(RetData[7]);
                        RetData[7] = pv7.ToString();
                        RetData[8] = Encoding.ASCII.GetString(RecBuffer.Skip(66).Take(8).ToArray()).Replace("$", "Ω");
                        PhysicalVariable pv8 = NumericsConverter.Text2Value(RetData[8]);
                        RetData[8] = pv8.ToString();
                        RetData[9] = "1";

                        return RetData;

                    }
                    else if (RecBuffer[0] == 0xee)
                    {
                        string[] Rd = new string[1];
                        Rd[0] = TestErr(RecBuffer[1].ToString());
                        return Rd;//错误类型
                    }
                    //}
                }
                catch
                {

                }


            }
            if (testkind == Parameter.TestKind.绝缘电阻)
            {
                byte[] SendComman = { 0x22 };
                byte[] RecData = new byte[18];
                //  float[] RetData = new float[4];

                sc.SendCommand(SendComman, ref RecData, 50);

                if (CheckData(RecData) == RecData[17])
                {
                    if (RecData[0] == 0xfa)
                    {

                        string[] RetData = new string[4];

                        PhysicalVariable pv = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecData.Skip(1).Take(5).ToArray()) + "V");
                        RetData[0] = pv.ToString();
                        string TempData = Encoding.ASCII.GetString(RecData.Skip(6).Take(7).ToArray()).Replace("$", "Ω");
                        if (TempData.IndexOf('Ω') < 0)
                        {
                            TempData += "Ω";
                        }
                        PhysicalVariable pv1 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecData.Skip(6).Take(7).ToArray()).Replace("$", "Ω"));
                        RetData[1] = pv1.ToString();
                        PhysicalVariable pv2 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecData.Skip(13).Take(4).ToArray()));
                        RetData[2] = pv2.ToString();
                        RetData[3] = "0";
                        return RetData;
                    }
                    if (RecData[0] == 0xff)
                    {

                        string[] RetData = new string[4];
                        string TempData = Encoding.ASCII.GetString(RecData.Skip(1).Take(7).ToArray()).Replace("$", "Ω");
                        if (TempData.IndexOf('Ω') < 0)
                        {
                            TempData += "Ω";
                        }
                        PhysicalVariable pv = NumericsConverter.Text2Value(TempData);
                        RetData[0] = pv.ToString();

                        PhysicalVariable pv1 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecData.Skip(8).Take(4).ToArray()));
                        RetData[1] = pv1.ToString();
                        PhysicalVariable pv2 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecData.Skip(12).Take(4).ToArray()));
                        RetData[2] = pv2.ToString();
                        RetData[3] = "1";

                        return RetData;
                    }
                    if (RecData[0] == 0xee)
                    {
                        string[] Rd = new string[1];
                        Rd[0] = TestErrDC(RecData[1].ToString());

                        return Rd;//错误类型
                    }
                }

                else
                {

                }

            }
            if (testkind == Parameter.TestKind.有载分接)
            {
                byte[] RecBuffer = new byte[51];
                // float[] RetData = new float[7];
                byte[] SendComman = { 0x42 };
                sc.SendCommand(SendComman, ref RecBuffer, 50);
                if (RecBuffer[0] == 0xfa)
                {

                    string[] RetData = new string[7];
                    RetData[0] = RecBuffer[1].ToString();
                    PhysicalVariable pv = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(2).Take(8).ToArray()));
                    RetData[1] = pv.ToString();
                    PhysicalVariable pv1 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(10).Take(8).ToArray()));
                    RetData[2] = pv1.ToString();
                    PhysicalVariable pv2 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(18).Take(8).ToArray()));
                    RetData[3] = pv2.ToString();
                    PhysicalVariable pv3 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(26).Take(8).ToArray()));
                    RetData[4] = pv3.ToString();
                    PhysicalVariable pv4 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(34).Take(8).ToArray()));
                    RetData[5] = pv4.ToString();
                    PhysicalVariable pv5 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(42).Take(8).ToArray()));
                    RetData[6] = pv5.ToString();
                    return RetData;
                }
                else if (RecBuffer[0] == 0xff && RecBuffer[1] == 0xff)
                {
                    string[] RetData = new string[6];
                    PhysicalVariable pv = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(2).Take(8).ToArray()));
                    RetData[0] = pv.ToString();
                    PhysicalVariable pv1 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(10).Take(8).ToArray()));
                    RetData[1] = pv1.ToString();
                    PhysicalVariable pv2 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(18).Take(8).ToArray()));
                    RetData[2] = pv2.ToString();
                    PhysicalVariable pv3 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(26).Take(8).ToArray()));
                    RetData[3] = pv3.ToString();
                    PhysicalVariable pv4 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(34).Take(8).ToArray()));
                    RetData[4] = pv4.ToString();
                    PhysicalVariable pv5 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecBuffer.Skip(42).Take(8).ToArray()));
                    RetData[5] = pv5.ToString();
                    return RetData;
                }
                else if (RecBuffer[0] == 0xee)
                {
                    string[] Rd = new string[1];

                    Rd[0] = TestErr(RecBuffer[1].ToString());
                    return Rd;//错误类型
                }
                else
                {
                    return null;
                }


            }
            if (testkind == Parameter.TestKind.读取放电数据)
            {
                byte[] RecData = new byte[26];
                // float[] RetData = new float[4];
                byte[] SendComman = { 0x3e };
                if (sc.SendCommand(SendComman, ref RecData, 50) >= 0)
                {

                    string[] RetData = new string[4];
                    bool Success;
                    RetData[0] = RecData[0].ToString();
                    PhysicalVariable pv = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecData.Skip(1).Take(8).ToArray()), out Success);
                    if (Success)
                        RetData[1] = pv.ToString();
                    else
                        RetData[1] = Encoding.ASCII.GetString(RecData.Skip(1).Take(8).ToArray());
                    PhysicalVariable pv1 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecData.Skip(9).Take(8).ToArray()), out Success);
                    if (Success)
                        RetData[2] = pv1.ToString();
                    else
                        RetData[2] = Encoding.ASCII.GetString(RecData.Skip(9).Take(8).ToArray());
                    PhysicalVariable pv2 = NumericsConverter.Text2Value(Encoding.ASCII.GetString(RecData.Skip(17).Take(8).ToArray()), out Success);
                    if (Success)
                        RetData[3] = pv2.ToString();
                    else
                        RetData[3] = Encoding.ASCII.GetString(RecData.Skip(17).Take(8).ToArray());

                    return RetData;
                }


            }
            return null;
        }

        private string TestErr(string test)
        {
            if (test == "0")
                return "按键事件过短";
            if (test == "1")
                return "切换线失败";
            if (test == "2")
                return "手动终止测量";
            if (test == "238")//ee
                return "测量失败";
            if (test == "236")//ec
                return "通讯出错";
            if (test == "255")//FF
                return "测量成功";
            return string.Empty;

        }
        private string TestErrDC(string test)
        {
            if (test == "0")
                return "测量人为中断0";
            if (test == "1")
                return "切换线失败";
            if (test == "2")
                return "手动终止测量0";
            if (test == "3")
                return "电源板温度保护";
            if (test == "4")
                return "电源板电流过流";
            if (test == "5")
                return "电源板升压错误";
            if (test == "6")
                return "电源板测量过流";
            if (test == "7")
                return "电源板测量掉流";
            if (test == "8")
                return "高压允许未开启";
            if (test == "9")
                return "输入电源欠压";
            if (test == "10")
                return "输入电源过压";
            if (test == "11")
                return "接地检测出错";
            if (test == "12")
                return "功率侧出错";
            if (test == "13")
                return "测量板开始过流";
            if (test == "14")
                return "测量板测量过流";
            if (test == "15")
                return "长时间无电压输出";
            if (test == "16")
                return "电源板测量出错";
            if (test == "17")
                return "测量板通讯出错";
            if (test == "18")
                return "测量板长时间无数据";
            if (test == "19")
                return "CVT自激输出短路";
            if (test == "20")
                return "被试品测量中击穿";
            if (test == "21")
                return "被试侧无信号";
            if (test == "22")
                return "测量人为中断22";
            if (test == "23")
                return "标准测无电压";
            if (test == "24")
                return "正母线过压";
            if (test == "25")
                return "负母线过压";
            return string.Empty;

        }



        public string AutoConnectMe
        {
            get
            {
                string[] PortNames = GetPortNames();
                if (PortNames == null)
                    return null;
                else
                {
                    for (int i = 0; i < PortNames.Length; i++)
                    {
                        PortNames[i] = PortNames[PortNames.Length - 1];
                    }

                    //}
                    foreach (string Portname in PortNames)
                    {
                        try
                        {
                            if (Portname != "COM4")
                            {
                                if (true == OpenPort(Portname, 115200, 8, 1))
                                {
                                    // Thread.Sleep(300);
                                    if (true == CommunicationQuery(1))
                                    {
                                        Thread.Sleep(100);
                                        Closeport();
                                        return Portname;

                                    }
                                }
                            }



                        }
                        catch (Exception)
                        {
                        }
                    }
                    return string.Empty;

                }
            }
        }

        private short[] ParsingWaveFormData(byte[] TempData)
        {
            // byte[] T1 = 1;
            short[] ParsingDaTa = new short[24008];
            try
            {
                Thread t1 = new Thread(() =>
                {
                    byte[] T1 = TempData.Skip(1).Take(12002).ToArray();
                    short[] T2 = new short[6002];
                    for (int i = 0; i < 6000; i++)
                    {
                        T2[i] = BitConverter.ToInt16(T1, i * 2);

                    }
                    T2[6000] = T1[12000];
                    T2[6001] = T1[12001];
                    for (int j = 0; j < 6002; j++)
                    {
                        ParsingDaTa[j] = T2[j];//A

                    }


                });

                t1.Start();
                Thread t2 = new Thread(() =>
                {
                    byte[] T1 = TempData.Skip(12003).Take(12002).ToArray();
                    short[] T2 = new short[6002];
                    for (int i = 0; i < 6000; i++)
                    {
                        T2[i] = BitConverter.ToInt16(T1, i * 2);

                    }
                    T2[6000] = T1[12000];
                    T2[6001] = T1[12001];
                    for (int j = 6002; j < 12004; j++)
                    {
                        ParsingDaTa[j] = T2[j - 6002];//B

                    }

                });
                t2.Start();
                Thread t3 = new Thread(() =>
                {
                    byte[] T1 = TempData.Skip(24005).Take(12002).ToArray();
                    short[] T2 = new short[6002];
                    for (int i = 0; i < 6000; i++)
                    {
                        T2[i] = BitConverter.ToInt16(T1, i * 2);

                    }
                    T2[6000] = T1[12000];
                    T2[6001] = T1[12001];
                    for (int j = 12004; j < 18006; j++)
                    {
                        ParsingDaTa[j] = T2[j - 12004];//C

                    }

                });
                t3.Start();

                Thread t4 = new Thread(() =>
                {

                    byte[] T1 = TempData.Skip(36007).Take(12002).ToArray();
                    short[] T2 = new short[6002];
                    for (int i = 0; i < 6000; i++)
                    {
                        T2[i] = BitConverter.ToInt16(T1, i * 2);

                    }
                    T2[6000] = T1[12000];
                    T2[6001] = T1[12001];
                    for (int j = 18006; j < 24008; j++)
                    {
                        ParsingDaTa[j] = T2[j - 18006];//BC

                    }

                });
                t4.Start();

            }

            catch
            { }

            Thread.Sleep(500);
            return ParsingDaTa;

        }

        public short[] GetWaveFormData
        {
            get
            {
                byte[] RecBuffer = new byte[48010];
                var RecBuffer1 = sc.ReadPortsData(new byte[3] { 0x4f, 0X00, 0x4f }, RecBuffer, 48010, 60);
                if (RecBuffer1.Length != 48010)
                {
                    Thread.Sleep(2000);
                    RecBuffer1 = sc.ReadPortsData(new byte[3] { 0x4f, 0X00, 0x4f }, RecBuffer, 48010);
                }
                return ParsingWaveFormData(RecBuffer1);
            }
        }




        public void Sendc(byte[] sendc)
        {

            sc.SendDataByte(sendc, 0, sendc.Length);
        }

        //开始测量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DelossData">实验参数</param>
        /// <returns>提示类型</returns>
        public bool StartTest(byte[] DelossData)
        {
            byte[] RecBuffer = new byte[3];
            try
            {
                Thread.Sleep(50);
                // int ret;
                sc.SendCommand(DelossData, ref RecBuffer, 50);
                if (RecBuffer[0] == 0xac && RecBuffer[1] == DelossData[DelossData.Length - 2] && CheckData(RecBuffer) == RecBuffer[2])
                {
                    return true;

                }
                else if (RecBuffer[0] == 0xee && RecBuffer[1] == 0xee)
                {
                    return false;

                }

            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 测量状态、模拟按键中断测量、判断直流电阻稳定状态
        /// </summary>
        /// <param name="commanTest">选择模拟按键测量还是复位</param>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool InterRuptMe(Parameter.CommanTest commanTest)
        {
            if (commanTest == Parameter.CommanTest.模拟按键中断测量)
            {
                byte[] sendbuffer = { 0xff, 0x00, 0xaa, 0x55 };
                byte[] Recbuffer = new byte[2];
                sc.SendCommand(sendbuffer, ref Recbuffer, 10);
                if (Recbuffer[0] == 0xac && Recbuffer[1] == 0xff)
                {
                    return true;
                }
            }
            if (commanTest == Parameter.CommanTest.仪器复位)
            {
                byte[] sendbuffer = { 0xfe, 0xaa, 0x99, 0x55, 0x00, 0xff };
                byte[] Recbuffer = new byte[2];
                sc.SendCommand(sendbuffer, ref Recbuffer, 50);
                if (Recbuffer[0] == 0x54 && Recbuffer[1] == 0x5a)
                {
                    return true;
                }
                if (Recbuffer[0] == 0xee && Recbuffer[1] == 0xee)
                {
                    return false;
                }
            }

            if (commanTest == Parameter.CommanTest.判断直流电阻稳定状态)
            {
                byte[] sendbuffer = { 0x33, 0x44, 0xaa, 0x55 };
                byte[] Recbuffer = new byte[2];
                sc.SendCommand(sendbuffer, ref Recbuffer, 50);
                if (Recbuffer[0] == 0xac && Recbuffer[1] == 0x33)
                {
                    return true;
                }
                else if (Recbuffer[0] == 0xee && Recbuffer[1] == 0xee)
                {
                    return false;
                }

            }

            return false;

        }
        /// <summary>
        /// 关闭电流输出
        /// </summary>
        /// <param name="Identification">标志位任意设置</param>
        /// <returns>关闭是否成功</returns>
        public bool ShutDownOutCurrent(int Identification)
        {
            byte[] SendBuffer = new byte[3];
            SendBuffer[0] = 0x3f;
            SendBuffer[1] = (byte)Identification;
            SendBuffer[2] = CheckData(SendBuffer);
            byte[] RecBuffer = new byte[3];
            //  sc.SendDataByte(SendBuffer,0,3);
            if (sc.SendCommand(SendBuffer, ref RecBuffer, 50) >= 0)
            {
                if (RecBuffer[0] == 0xac)
                {
                    return true;
                }
                else if (RecBuffer[0] == 0xee && RecBuffer[1] == 0xee)
                {
                    return false;
                }
            }

            return false;

        }





        //获取数组的对应索引
        private int GetStrsIndex(string[] strs, string str)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i] == str)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
