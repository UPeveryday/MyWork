﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    [Serializable]
    public struct WindingTerimal: IComparable<WindingTerimal>
    {
        
        public static readonly WindingTerimal O = new WindingTerimal(0);
        public static readonly WindingTerimal A = new WindingTerimal(1);
        public static readonly WindingTerimal B = new WindingTerimal(2);
        public static readonly WindingTerimal C = new WindingTerimal(3);

        private readonly int TerimalType;

        public WindingTerimal(int TerimalType)
        {
            if (TerimalType > 3) throw new Exception();
            this.TerimalType = TerimalType;
        }

        public static explicit operator int(WindingTerimal value)
        {
            return value.TerimalType;
        }

        public static implicit operator string(WindingTerimal value)
        {
            return value.ToString();
        }

        public static bool operator ==(WindingTerimal a, WindingTerimal b)
        {
            return (a.TerimalType == b.TerimalType);
        }

        public static bool operator !=(WindingTerimal a, WindingTerimal b)
        {
            return (a.TerimalType != b.TerimalType);
        }

        public static WindingTerimal[] FromList(List<int> list)
        {
            List<WindingTerimal> wlist = new List<WindingTerimal>();
            foreach(var i in list)
            {
                wlist.Add(new WindingTerimal(i));
            }
            return wlist.ToArray();
        }

        public override string ToString()
        {
            switch (this.TerimalType)
            {
                case 0:
                    return "O";
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                default:
                    throw new Exception();
            }
        }

        public bool Equals(WindingTerimal value)
        {
            return (this.TerimalType == value.TerimalType);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
                return (((WindingTerimal)obj).TerimalType == this.TerimalType);
            return false;
        }

        public override int GetHashCode()
        {
            return this.TerimalType.GetHashCode();
        }

        public int CompareTo(WindingTerimal other)
        {
            return (this.TerimalType.CompareTo(other.TerimalType));
        }
    }
    [Serializable]
    public struct WindingType : IComparable<WindingType>
    {
        public static readonly WindingType HV = new WindingType(0);
        public static readonly WindingType MV = new WindingType(1);
        public static readonly WindingType LV = new WindingType(2);

        private readonly int windingType;

        public WindingType(int windingType)
        {
            if (windingType > 2) throw new Exception();
            this.windingType = windingType;
        }

        public static explicit operator int(WindingType value)
        {
            return value.windingType;
        }

        public static implicit operator string(WindingType value)
        {
            return value.ToString();
        }

        public static bool operator ==(WindingType a, WindingType b)
        {
            try
            {
                return (a.windingType == b.windingType);
            }
            catch
            {
                return false;
            }
        }

        public static bool operator !=(WindingType a, WindingType b)
        {
            return !(a == b);
        }

      
        public Parameter.JYDZstation ToJYDZstation()
        {
            switch (this.windingType)
            {
                case 0: return Parameter.JYDZstation.高压绕组;
                case 1: return Parameter.JYDZstation.中压绕组;
                case 2: return Parameter.JYDZstation.低压绕组;
                default: throw new Exception();
            }
        }
       

        public Parameter.ZldzStation TozldzStation()
        {
            switch (this.windingType)
            {
                case 0: return Parameter.ZldzStation.高压全部;
                case 1: return Parameter.ZldzStation.中压全部;
                case 2: return Parameter.ZldzStation.低压全部;
                default: throw new Exception();
            }
        }
        public Parameter.JSstation ToJSstation()
        {
            switch (this.windingType)
            {
                case 0: return Parameter.JSstation.高压绕组;
                case 1: return Parameter.JSstation.中压绕组;
                case 2: return Parameter.JSstation.低压绕组;
                default: throw new Exception();
            }
        }
        public override string ToString()
        {
            switch (windingType)
            {
                case 0:
                    return "高压绕组";
                case 1:
                    return "中压绕组";
                case 2:
                    return "低压绕组";
                default:
                    throw new Exception();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
                return (((WindingType)obj).windingType == this.windingType);
            return false;
        }

        public override int GetHashCode()
        {
            return this.windingType.GetHashCode();
        }

        public int CompareTo(WindingType other)
        {
            return (this.windingType.CompareTo(other.windingType));
        }
    }
    [Serializable]
    public struct DCInsulationParameterList
    {
        public int TestingVoltage_HV;
        public int TestingVoltage_MV;
        public int TestingVoltage_LV;
        public double ResistorPassThreshold;
        public double AbsorptionRatioPassThreshold;
        public double AbsorptionRatioSkipThreshold;
        public double PolarizationSkipThreshold;
    }
    [Serializable]
    public struct CapacitanceParameterList
    {
        public int TestingVoltage_HV;
        public int TestingVoltage_MV;
        public int TestingVoltage_LV;
    }
    [Serializable]
    public struct DCWindingResistorParameterList
    {
        public int MaxTestingCurrent_HV;
        public int MaxTestingCurrent_MV;
        public int MaxTestingCurrent_LV;
    }

    public struct BushingTestingParameterList
    {
        public int DCInsulationTestingVoltage;
        public int DCInsulationTestingTime;

    }
    [Serializable]
    public struct WindingJobList
    {
        public bool HVEnabled;
        public bool MVEnabled;
        public bool LVEnabled;
        public bool Enabled
        {
            get
            {
                return (HVEnabled | MVEnabled | LVEnabled);
            }
            set
            {
                HVEnabled = value;
                MVEnabled = value;
                LVEnabled = value;
            }
        }
    }
    [Serializable]
    public struct BushingJobList
    {
        public bool DCInsulation;
        public bool Capacitance;
    }
    [Serializable]
    public struct OLTCJobList
    {
        public int Range;
        public bool DCResistance;
        public bool SwitchingCharacter;
        public bool Enabled
        {
            get
            {
                return ((DCResistance | SwitchingCharacter) && (Range > 0));
            }
            set
            {
                DCResistance = value;
                SwitchingCharacter = value;
            }
        }
    }
    [Serializable]
    public struct JobParameter
    {
        public int DCInsulationVoltage;
        public double DCInsulationResistance;
        public double DCInsulationAbsorptionRatio;
        public int CapacitanceVoltage;
        public int DCResistanceCurrent;
        public int BushingDCInsulationVoltage;
        public int BushingCapacitanceVoltage;
    }
    [Serializable]
    public struct JobInformation
    {
        private string _testingName;
        private string _tester;
        private string _testingAgency;
        private string _auditor;
        private string _approver;
        private string _weather;
        private string _temperature;
        private string _humidity;
        private string _principal;

        public DateTime testingTime;
        public string testingName
        {
            get
            {
                 if (this._testingName.Length > 0)
                    return this._testingName;
                else
                    return "--未指定--";
            }
            set
            {
                this._testingName = value;
            }
        }
        public string tester
        {
            get
            {
                if (this._tester.Length > 0)
                    return this._tester;
                else
                    return "--未指定--";
            }
            set
            {
                this._tester = value;
            }
        }
        public string testingAgency
        {
            get
            {
                if (this._testingAgency.Length > 0)
                    return this._testingAgency;
                else
                    return "--未指定--";
            }
            set
            {
                this._testingAgency = value;
            }
        }
        public string auditor
        {
            get
            {
                if (this._auditor.Length > 0)
                    return this._auditor;
                else
                    return "--未指定--";
            }
            set
            {
                this._auditor = value;
            }
        }
        public string approver
        {
            get
            {
                if (this._approver.Length > 0)
                    return this._approver;
                else
                    return "--未指定--";
            }
            set
            {
                this._approver = value;
            }
        }
        public string weather
        {
            get
            {
                if (this._weather.Length > 0)
                    return this._weather;
                else
                    return "--未指定--";
            }
            set
            {
                this._weather = value;
            }
        }
        public string temperature
        {
            get
            {
                if (this._temperature.Length > 0)
                    return this._temperature;
                else
                    return "--未指定--";
            }
            set
            {
                this._temperature = value;
            }
        }
        public string humidity
        {
            get
            {
                if (this._humidity.Length > 0)
                    return this._humidity;
                else
                    return "--未指定--";
            }
            set
            {
                this._humidity = value;
            }
        }
        public string principal
        {
            get
            {
                if (this._principal.Length > 0)
                    return this._principal;
                else
                    return "--未指定--";
            }
            set
            {
                this._principal = value;
            }
        }
        public double oilTemperature;

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            var sb = new System.IO.StringWriter();
            sb.WriteLine(Convert.ToBase64String(BitConverter.GetBytes(testingTime.ToBinary())));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(testingName)));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(tester)));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(testingAgency)));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(auditor)));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(approver)));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(weather)));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(temperature)));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(humidity)));
            sb.WriteLine(Convert.ToBase64String(Encoding.UTF32.GetBytes(principal)));
            sb.WriteLine(Convert.ToBase64String(BitConverter.GetBytes(oilTemperature)));
            return sb.ToString();
        }

        public static JobInformation FromString(string s)
        {
            JobInformation info = new JobInformation();
            var sr = new System.IO.StringReader(s);
            string base64;

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.testingTime = DateTime.FromBinary(BitConverter.ToInt64(Convert.FromBase64String(base64), 0));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.testingName = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.tester = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.testingAgency = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.auditor = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.approver = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.weather = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.temperature = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.humidity = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.principal = Encoding.UTF32.GetString(Convert.FromBase64String(base64));

            base64 = sr.ReadLine();
            if (base64 == string.Empty) throw new ArgumentException("JobInformation字段信息缺失");
            info.oilTemperature = BitConverter.ToDouble(Convert.FromBase64String(base64), 0);

            return info;
        }

        public System.Data.DataRow ToDataRow(JobList jobInfo)
        {
            System.Data.DataRow row = WorkingSets.local.TestResults.NewRow();

            row["transformerid"] = jobInfo.Transformer.ID;
            row["mj_id"] = jobInfo.id;
            row["testid"] = jobInfo.GetHashCode();
            row["testname"] = this.testingName;
            row["function"] = (int)MeasurementFunction.Information;
            row["failed"] = false;
            row["completed"] = true;
            row["waves"] = this.ToString();
            return row;
        }
    }
    [Serializable]
    public struct JobList
    {
        public int id;
        public string Name;
        public Transformer Transformer;
        public WindingJobList DCInsulation;
        public WindingJobList Capacitance;
        public WindingJobList DCResistance;
        public BushingJobList Bushing;
        public OLTCJobList OLTC;
        public DCInsulationParameterList DCInsulationParameter;
        public CapacitanceParameterList CapacitanceParameter;
        public DCWindingResistorParameterList DCWindingResistorParameter;
        public JobParameter Parameter;
        public JobInformation Information;
    }
    [Serializable]
    public enum MeasurementFunction
    {
        Null = 0,
        DCInsulation = 1,
        Capacitance = 2,
        DCResistance = 3,
        BushingDCInsulation = 4,
        BushingCapacitance = 5,
        OLTCSwitchingCharacter = 6,
        DCCharge = 7,
        OilTemperature = 8,

        Description = 10,

        Information = 20,

        InternalData = 100

    }

    [Serializable]
    public class MeasurementItemStruct
    {
        public readonly MeasurementFunction Function;
        public WindingType Winding;
        public WindingTerimal[] Terimal;

        public TransformerWindingConfigName WindingConfig;
        public string[] TapLabel;
        public string Text;
        public bool completed = false;
        public bool failed = false;
        internal int state = 0;
        internal string stateText = string.Empty;
        private MeasurementResult result;

        internal static byte[] Array2Bytes(short[] array)
        {

            if (array.Length < 1) throw new NullReferenceException("转换数组为空");
            Type type = array[0].GetType();
            List<byte> bytesCollection = new List<byte>();
            for (int i = 0; i < array.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(array[i]);
                bytesCollection.AddRange(bytes);
            }
            return bytesCollection.ToArray();
        }

        internal static short[] Bytes2Shorts(byte[] bytes)
        {
            List<short> sList = new List<short>();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                sList.Add(BitConverter.ToInt16(bytes, i));
            }
            return sList.ToArray();
        }

        public static MeasurementItemStruct FromDataRow(System.Data.DataRow row)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct((MeasurementFunction)(int)row["function"]);
            if(mi.Winding!=null)
              mi.Winding = new WindingType((int)row["windingtype"]);
            if ((row["terimal"]).ToString().Length > 0)
            {
                List<WindingTerimal> wts = new List<WindingTerimal>();
                foreach(var s in ((string)row["terimal"]).Split(';'))
                {
                    wts.Add(new WindingTerimal(int.Parse(s)));
                }
                mi.Terimal = wts.ToArray();
            }
            mi.WindingConfig = (TransformerWindingConfigName)(int)row["windingconfig"];
            if ((row["taplabel"]).ToString().Length > 0)
            {
                List<string> ss = new List<string>();
                foreach (var s in ((string)row["taplabel"]).Split(';'))
                {
                    ss.Add(new WindingTerimal(int.Parse(s)));
                }
                mi.TapLabel = ss.ToArray();
            }
            mi.Text = row["text"].ToString();
            try { mi.failed = (bool)row["failed"]; } catch { }
            mi.completed = (bool)row["completed"];
            SCEEC.Numerics.PhysicalVariable[] pv = new Numerics.PhysicalVariable[9];
            for (int i = 0; i < 9; i++)
            {
                string pvs = row["result_pv" + (i + 1).ToString()].ToString();
                pv[i] = SCEEC.Numerics.NumericsConverter.Text2Value(pvs);
            }
            short[] waves = Bytes2Shorts(System.Convert.FromBase64String(Convert.ToString( row["waves"])));
            mi.result = new MeasurementResult(mi.Function, pv, waves, false, (DateTime)row["recordtime"]);
            
            return mi;
        }

        public System.Data.DataRow ToDataRow(JobList jobInfo)
        {
            System.Data.DataRow row = WorkingSets.local.TestResults.NewRow();
            try { row["testname"] = jobInfo.Information.testingName; } catch { }
            try { row["testid"] = jobInfo.Information.GetHashCode(); } catch { }
            try { row["transformerid"] = jobInfo.Transformer.ID; } catch { }
            try { row["mj_id"] = jobInfo.id; } catch { }
            //row["testname"] =
            try { row["function"] = (int)Function; } catch { }
            try { row["windingtype"] = (int)Winding; } catch { }
            if(Terimal!=null)
            {
                if (Terimal.Length == 2)
                    try { row["terimal"] = ((int)Terimal[0]).ToString() + ";" + ((int)Terimal[1]).ToString(); } catch { }
                else if (Terimal.Length == 1)
                    try { row["terimal"] = ((int)Terimal[0]).ToString(); } catch { }
                else
                    try { row["terimal"] = string.Empty; } catch { }
            }

            try { row["windingconfig"] = (int)WindingConfig; } catch { }
            if(TapLabel!=null)
            {
                if (TapLabel.Length == 2)
                    try { row["taplabel"] = TapLabel[0] + ";" + TapLabel[1]; } catch { }
                else if (Terimal.Length == 1)
                    try { row["taplabel"] = TapLabel[0]; } catch { }
                else
                    try { row["taplabel"] = string.Empty; } catch { }
            }

            try { row["text"] = Text; } catch { }
            try { row["failed"] = failed; } catch { }
            try { row["completed"] = completed; } catch { }
            if(result!=null)
            {
                if (result.values != null)
                {
                    for (int i = 0; i < result.values.Length; i++)
                    {
                        var pv = result.values[i];
                        try { row["result_pv" + (i + 1).ToString()] = pv.ToString(); } catch { }
                    }
                }

                try { row["recordtime"] = result.recordTime; } catch { }
                if (result.waves != null)
                {
                    if (result.waves.Length > 0)
                        try { row["waves"] = System.Convert.ToBase64String(Array2Bytes(result.waves)); } catch { }
                }
            }
            
           
            return row;
        }

        internal MeasurementItemStruct() { }

        internal MeasurementItemStruct(MeasurementFunction function)
        {
            this.Function = function;
        }

        public MeasurementResult Result
        {
            get
            {
                return result;
            }
            set
            {
                if (value.Function == this.Function)
                {
                    this.result = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public static MeasurementItemStruct CreateDCInsulationMeasurementItem(WindingType winding)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.DCInsulation);
            mi.Winding = winding;
            return mi;
        }

        public static MeasurementItemStruct CreateCapacitanceMeasurementItem(WindingType winding)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.Capacitance);
            mi.Winding = winding;
            return mi;
        }

        public static MeasurementItemStruct CreateDCResistanceMeasurementItem(WindingType winding)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.DCResistance);
            mi.Winding = winding;
            return mi;
        }

        public static MeasurementItemStruct CreateDCResistanceMeasurementItem(WindingType winding, WindingTerimal terimal1,WindingTerimal terimal2)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.DCResistance);
            mi.Winding = winding;
            mi.Terimal = new WindingTerimal[2] { terimal1, terimal2 };
            return mi;
        }

        public static MeasurementItemStruct CreateDCResistanceMeasurementItem(WindingType winding, string ChangerLabel)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.DCResistance);
            mi.Winding = winding;
            mi.TapLabel = new string[1] { ChangerLabel };
            return mi;
        }

        public static MeasurementItemStruct CreateDCResistanceMeasurementItem(WindingType winding, string ChangerLabel, WindingTerimal terimal1, WindingTerimal terimal2)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.DCResistance);
            mi.Winding = winding;
            mi.Terimal = new WindingTerimal[2] { terimal1, terimal2 };
            mi.TapLabel = new string[1] { ChangerLabel };
            return mi;
        }

        public static MeasurementItemStruct CreateBushingDCInsulationMeasurementItem(WindingType winding, WindingTerimal terimal)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.BushingDCInsulation);
            mi.Winding = winding;
            mi.Terimal = new WindingTerimal[1] { terimal };
            return mi;
        }

        public static MeasurementItemStruct CreateBushingCapacitanceMeasurementItem(WindingType winding, WindingTerimal terimal)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.BushingCapacitance);
            mi.Winding = winding;
            mi.Terimal = new WindingTerimal[] { terimal };
            return mi;
        }

        public static MeasurementItemStruct CreateOLTCSwitchingCharacterMeasurementItem(WindingType winding, string LastTapLabel, string NextTapLabel, TransformerWindingConfigName windingConfig)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.OLTCSwitchingCharacter);
            mi.Winding = winding;
            mi.WindingConfig = windingConfig;
            mi.TapLabel = new string[2] { LastTapLabel, NextTapLabel };
            return mi;
        }

        public static MeasurementItemStruct CreateText(string Text)
        {
            MeasurementItemStruct mi = new MeasurementItemStruct(MeasurementFunction.Description);
            mi.Text = Text;
            return mi;
        }

        public string Description
        {
            get
            {
                try
                {
                    switch (Function)
                    {
                        case MeasurementFunction.DCInsulation://绝缘电阻
                            return "测量" + Winding + "绝缘电阻、吸收比、极化指数;";
                        case MeasurementFunction.Capacitance:
                            return "测量" + Winding + "对地的电容量及介质损耗因数;";
                        case MeasurementFunction.DCResistance:
                            string changerDescription = "";
                            if (TapLabel != null) changerDescription = ("在分接" + TapLabel[0] + "位置");
                            if (Terimal != null)
                            {
                                return "测量" + Winding + changerDescription + Terimal[0].ToString() + "-" + Terimal[1].ToString() + "的直流电阻;";
                            }
                            return "测量" + Winding + changerDescription + "的直流电阻;";
                        case MeasurementFunction.BushingDCInsulation:
                            return "测量" + Winding + "套管" + Terimal[0] + "末屏的绝缘电阻;";//套管四个通道
                        case MeasurementFunction.BushingCapacitance:
                            return "测量" + Winding + "绕组对套管" + Terimal[0] + "末屏的电容量及介质损耗因数;";
                        case MeasurementFunction.OLTCSwitchingCharacter:
                            return "测量" + Winding + TapLabel[0] + "分接->" + TapLabel[1] + "分接的过渡波形";
                        case MeasurementFunction.DCCharge:
                            return "对" + Winding + "进行预充电";
                        case MeasurementFunction.Description:
                            return Text;
                    }
                }
                catch
#if DEBUG 
                (Exception ex)
#endif
                {
#if DEBUG
                    throw ex;
#endif
                }
                return "试验项目无效!";
            }
        }

        public string ResultText
        {
            get
            {
                if (result == null) return string.Empty;
                string rtn = string.Empty;
                if (failed)
                    switch(result.Function)
                    {
                        case MeasurementFunction.DCInsulation:
                            return Winding.ToString() + "绝缘电阻测量错误";
                        case MeasurementFunction.Capacitance:
                            return Winding.ToString() + "电容量及介质损耗因数测量错误";
                        case MeasurementFunction.DCResistance:
                            if (this.Terimal == null)
                            {
                                return this.Winding.ToString() + "直流电阻三相同测测量错误";
                            }
                            else
                            {
                                return this.Winding.ToString() + this.Terimal[0].ToString() + this.Terimal[1].ToString() + "直流电阻测量错误";
                            }
                        case MeasurementFunction.OLTCSwitchingCharacter:
                            return this.Winding.ToString() + "有载分接测量错误";
                        case MeasurementFunction.BushingDCInsulation:
                            return Winding.ToString() + this.Terimal[0] + "套管绝缘电阻测量错误";
                        case MeasurementFunction.BushingCapacitance:
                            return Winding.ToString() + this.Terimal[0] + "套管电容量及介质损耗因数测量错误";
                        default:
                            throw new NotImplementedException();
                    }
                else
                    switch (result.Function)
                    {
                        case MeasurementFunction.DCInsulation:
                            if (result.values[2].value == null) return this.Winding.ToString() + "绝缘电阻: " + result.values[1].ToString();
                            if (result.values[3].value == null) return this.Winding.ToString() + "绝缘电阻: " + result.values[1].ToString() + " 吸收比: " + result.values[2].ToString();
                            return this.Winding.ToString() + "绝缘电阻: " + result.values[1].ToString() + " 吸收比: " + result.values[2].ToString() + " 极化指数: " + result.values[3].ToString();
                        case MeasurementFunction.Capacitance:
                            return this.Winding.ToString() + "电容量: " + result.values[2] + " 介质损耗：" + SCEEC.Numerics.NumericsConverter.Value2Text(result.values[3], true, true);
                        case MeasurementFunction.DCResistance:
                            if (this.Terimal == null)
                            {
                                rtn = this.Winding.ToString() + "直流电阻三相同测: \n";
                                rtn += "绕组\t电流\t电阻\n";
                                rtn += "AO\t";
                                rtn += result.values[1].ToString();
                                rtn += "\t";
                                rtn += result.values[2].ToString();
                                rtn += "\n";
                                rtn += "BO\t";
                                rtn += result.values[4].ToString();
                                rtn += "\t";
                                rtn += result.values[5].ToString();
                                rtn += "\n";
                                rtn += "CO\t";
                                rtn += result.values[7].ToString();
                                rtn += "\t";
                                rtn += result.values[8].ToString();
                                return rtn;
                            }
                            else
                            {
                                if (this.Terimal[0] == WindingTerimal.A)
                                    rtn = this.Winding.ToString() + this.Terimal[0].ToString() + this.Terimal[1].ToString() + "电流: " + result.values[1].ToString() + "\t直流电阻: " + result.values[2].ToString();
                                else if (this.Terimal[0] == WindingTerimal.B)
                                    rtn = this.Winding.ToString() + this.Terimal[0].ToString() + this.Terimal[1].ToString() + "电流: " + result.values[4].ToString() + "\t直流电阻: " + result.values[5].ToString();
                                else
                                    rtn = this.Winding.ToString() + this.Terimal[0].ToString() + this.Terimal[1].ToString() + "电流: " + result.values[7].ToString() + "\t直流电阻: " + result.values[8].ToString();
                                return rtn;
                            }
                        case MeasurementFunction.OLTCSwitchingCharacter:
                            return this.Winding.ToString() + "有载分接";
                        case MeasurementFunction.BushingDCInsulation:
                            if (result.values[1].value == null) return this.Winding.ToString() + "套管" + this.Terimal[0] + "末屏绝缘电阻: " + result.values[1].ToString();
                            if (result.values[2].value == null) return this.Winding.ToString() + "套管" + this.Terimal[0] + "末屏绝缘电阻: " + result.values[1].ToString() + " 吸收比: " + result.values[2].ToString();
                            return this.Winding.ToString() + "套管" + this.Terimal[0] + "末屏绝缘电阻: " + result.values[1].ToString() + " 吸收比: " + result.values[2].ToString() + " 极化指数: " + result.values[3].ToString();
                        case MeasurementFunction.BushingCapacitance:
                            return this.Winding.ToString() + "套管" + this.Terimal[0].ToString() + "末屏的电容量: " + result.values[2] + " 介质损耗：" + result.values[3];
                        default:
                            throw new NotImplementedException();
                    }
            }
        }

        public override string ToString()
        {
            return Description;
        }
    }
    
}


