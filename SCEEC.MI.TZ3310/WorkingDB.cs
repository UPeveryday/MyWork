﻿using System;
using System.Collections.Generic;
using System.Data;

namespace SCEEC.MI.TZ3310
{
    public struct Location
    {
        public int id;
        public string name;
        public string company;
        public string address;
        public string operatorName;
    }

    [Serializable]
    public enum TransformerWindingConfigName
    {
        Yn = 0,
        Y = 1,
        D = 2,
        Zn = 3
    }
    [Serializable]
    public enum TransformerWindingIndex
    {
        HV = 0,
        MV = 1,
        LV = 2
    }
    [Serializable]
    public struct TransformerWindingConfigStruct
    {
        public TransformerWindingConfigName HV;
        public TransformerWindingConfigName MV;
        public int MVLabel;
        public TransformerWindingConfigName LV;
        public int LVLabel;
    }
    [Serializable]
    public struct TransformerRatingStruct
    {
        public double HV;
        public double MV;
        public double LV;
    }
    [Serializable]
    public struct OLTCStruct
    {
        public bool Contained;
        public int TapMainNum;
        public int TapNum;
        public WindingType WindingPosition;
        public string SerialNo;
        public string ModelType;
        public string Manufacturer;
        public string ProductionYear;
        public double Step;
    }
    [Serializable]
    public struct BushingStruct
    {
        public bool HVContained;
        public bool MVContained;
    }
    [Serializable]
    public struct Transformer
    {
        public int ID;
        public string SerialNo;//仪器编号
        public string Location;
        public string ApparatusID;
        public string Manufacturer;
        public string ProductionYear;
        public string AssetSystemCode;
        public string MethonID;
        public int WindingNum;
        public TransformerWindingConfigStruct WindingConfig;
        public int RatingFrequency;
        public int PhaseNum;
        public TransformerRatingStruct VoltageRating;//额定电压
        public TransformerRatingStruct PowerRating;//额定容量
        public BushingStruct Bushing;
        public OLTCStruct OLTC;

    }

    //public struct Job
    //{
    //    public string Name;
    //    public Transformer transformer;
    //    public bool WindingDCInsulation;
    //    public bool HVWindingDCInsulation;
    //    public bool MVWindingDCInsulation;
    //    public bool LVWindingDCInsulation;
    //    public bool WindingCapacitance;
    //    public bool HVWindingCapacitance;
    //    public bool MVWindingCapacitance;
    //    public bool LVWindingCapacitance;
    //    public bool WindingDCResistance;
    //    public bool HVWindingDCResistance;
    //    public bool MVWindingDCResistance;
    //    public bool LVWindingDCResistance;
    //    public bool BushingDCInsulation;
    //    public bool BushingCapacitance;
    //    public bool OLTC;
    //    public int OLTCRange;
    //    public bool OLTCDCResistance;
    //    public bool OLTCSwitching;
    //}
    public class WorkingDB : IDisposable
    {
        public string server;
        public string port;
        public string database;
        public string username;
        public string password;

        public volatile bool ShowWaveForm = false;
        public bool IsStable = false;
        public bool IsEnablestable = false;
        public short[] WaveForm;
        public volatile short[] WaveFormSwicth;

        public TestingWorkerSender status;
        public string MethonID;

        public SCEEC.Data.SQLClient LocalSQLClient;
        public System.Data.DataTable Locations;
        public System.Data.DataTable Transformers;
        public System.Data.DataTable Jobs;
        public System.Data.DataTable TestResults;
        public System.Data.DataTable Reports;

        public DataTable Casingtest_Commonbody;
        public DataTable Casingtest_Commonbodyresults;
        public DataTable Conclusion;
        public DataTable Dcresistor_Highpressure;
        public DataTable Dcresistor_Highpressureresults;
        public DataTable Dcresistor_Lowpressure;
        public DataTable Dcresistor_Lowpressureresults;
        public DataTable Dcresistor_Mediumvoltage;
        public DataTable Dcresistor_Mediumvoltageresults;
        public DataTable Dielectriclossandcapacitance_Threewinding;
        public DataTable Dielectriclossandcapacitance_Threewindingresults;
        public DataTable Insulationresistance_Threewinding;
        public DataTable Insulationresistance_Threewindingresults;
        public DataTable Parameter_Information;
        public DataTable Sample_Information;
        public DataTable Tapchangertest;
        public DataTable Tapchangertestresults;
        public DataTable Windingcoreinsulationresistance;
        public DataTable Windingcoreinsulationresistanceresults;

        public SCEEC.MI.TZ3310.ClassTz3310 Tz3310;
        public WorkingDB()
        {
            server = "localhost";
            port = "3306"; 
            database = "tz3310";
            username = "ttm";
            password = "shsceecttm";
        }

        public WorkingDB(string server, string port, string database, string username, string password)
        {
            this.server = server;
            this.port = port;
            this.database = database;
            this.username = username;
            this.password = password;
        }

        public bool Connect()
        {
            if (!disposedValue)
            {
                LocalSQLClient = new SCEEC.Data.SQLClient(server, port, username, password, database);
                return LocalSQLClient.Connected;
            }
            return false;
        }

        public bool refreshLocations()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                this.Locations = LocalSQLClient.getDataTable(database, "location");
                return LocalSQLClient.Connected;
            }
            return false;
        }

        /// <summary>
        /// 获取指定的testid行集合
        /// </summary>
        /// <param name="testid"></param>
        /// <returns></returns>
        public DataRow[] rowsTestresult(string testid)
        {
            if (!WorkingSets.local.saveTestResults())
            {
                string err = WorkingSets.local.LocalSQLClient.ErrorText;
                err = err;
            }
            return WorkingSets.local.TestResults.Select("testid = '" + testid + "'");
        }


        public bool rowsIsOfResuly(string testid)
        {
            if (WorkingSets.local.TestResults.Select("testid = '" + testid + "'").Length > 0)
                return true;
            else
                return false;
        }
        public bool DeleteAllExportTable()
        {
             LocalSQLClient.deleteTable(database, "casingtest_commonbody");
             LocalSQLClient.deleteTable(database, "casingtest_commonbodyresults");
             LocalSQLClient.deleteTable(database, "conclusion");
             LocalSQLClient.deleteTable(database, "dcresistor_highpressure");
             LocalSQLClient.deleteTable(database, "dcresistor_highpressureresults");
             LocalSQLClient.deleteTable(database, "dcresistor_lowpressure");
             LocalSQLClient.deleteTable(database, "dcresistor_lowpressureresults");
             LocalSQLClient.deleteTable(database, "dcresistor_mediumvoltage");
             LocalSQLClient.deleteTable(database, "dcresistor_mediumvoltageresults");
             LocalSQLClient.deleteTable(database, "dielectriclossandcapacitance_threewinding");
             LocalSQLClient.deleteTable(database, "dielectriclossandcapacitance_threewindingresults");
             LocalSQLClient.deleteTable(database, "insulationresistance_threewinding");
             LocalSQLClient.deleteTable(database, "dielectriclossandcapacitance_threewindingresults");
             LocalSQLClient.deleteTable(database, "insulationresistance_threewinding");
             LocalSQLClient.deleteTable(database, "insulationresistance_threewindingresults");
             LocalSQLClient.deleteTable(database, "parameter_information");
             LocalSQLClient.deleteTable(database, "sample_information");
             LocalSQLClient.deleteTable(database, "tapchangertest");
             LocalSQLClient.deleteTable(database, "tapchangertestresults");
             LocalSQLClient.deleteTable(database, "windingcoreinsulationresistance");
            return LocalSQLClient.deleteTable(database, "windingcoreinsulationresistanceresults");
        }

        public void CreateDatabaseHead(string resultname)
        {
            InsertDataTodatabase.CreateTableHead(resultname);
        }
        public bool CreateLocalDatabase()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                this.Casingtest_Commonbody = LocalSQLClient.getDataTable(database, "casingtest_commonbody");
                this.Casingtest_Commonbodyresults = LocalSQLClient.getDataTable(database, "casingtest_commonbodyresults");
                this.Conclusion = LocalSQLClient.getDataTable(database, "conclusion");
                this.Dcresistor_Highpressure = LocalSQLClient.getDataTable(database, "dcresistor_highpressure");
                this.Dcresistor_Highpressureresults = LocalSQLClient.getDataTable(database, "dcresistor_highpressureresults");
                this.Dcresistor_Lowpressure = LocalSQLClient.getDataTable(database, "dcresistor_lowpressure");
                this.Dcresistor_Lowpressureresults = LocalSQLClient.getDataTable(database, "dcresistor_lowpressureresults");
                this.Dcresistor_Mediumvoltage = LocalSQLClient.getDataTable(database, "dcresistor_mediumvoltage");
                this.Dcresistor_Mediumvoltageresults = LocalSQLClient.getDataTable(database, "dcresistor_mediumvoltageresults");
                this.Dielectriclossandcapacitance_Threewinding = LocalSQLClient.getDataTable(database, "dielectriclossandcapacitance_threewinding");
                this.Dielectriclossandcapacitance_Threewindingresults = LocalSQLClient.getDataTable(database, "dielectriclossandcapacitance_threewindingresults");
                this.Insulationresistance_Threewinding = LocalSQLClient.getDataTable(database, "insulationresistance_threewinding");
                this.Insulationresistance_Threewindingresults = LocalSQLClient.getDataTable(database, "insulationresistance_threewindingresults");
                this.Parameter_Information = LocalSQLClient.getDataTable(database, "parameter_information");
                this.Sample_Information = LocalSQLClient.getDataTable(database, "sample_information");
                this.Tapchangertest = LocalSQLClient.getDataTable(database, "tapchangertest");
                this.Tapchangertestresults = LocalSQLClient.getDataTable(database, "tapchangertestresults");
                this.Windingcoreinsulationresistance = LocalSQLClient.getDataTable(database, "windingcoreinsulationresistance");
                this.Windingcoreinsulationresistanceresults = LocalSQLClient.getDataTable(database, "windingcoreinsulationresistanceresults");
                return LocalSQLClient.Connected;
            }
            return false;
        }
        public bool SaveCreateLocateDatabase()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                LocalSQLClient.updateDataTable(database, "casingtest_commonbody", Casingtest_Commonbody); Casingtest_Commonbody.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "casingtest_commonbodyresults", Casingtest_Commonbodyresults); Casingtest_Commonbodyresults.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "conclusion", Conclusion); Conclusion.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "dcresistor_highpressure", Dcresistor_Highpressure); Dcresistor_Highpressure.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "dcresistor_highpressureresults", Dcresistor_Highpressureresults); Dcresistor_Highpressureresults.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "dcresistor_lowpressure", Dcresistor_Lowpressure); Dcresistor_Lowpressure.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "dcresistor_lowpressureresults", Dcresistor_Lowpressureresults); Dcresistor_Lowpressureresults.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "dcresistor_mediumvoltage", Dcresistor_Mediumvoltage); Dcresistor_Mediumvoltage.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "dcresistor_mediumvoltageresults", Dcresistor_Mediumvoltageresults); Dcresistor_Mediumvoltageresults.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "dielectriclossandcapacitance_threewinding", Dielectriclossandcapacitance_Threewinding); Dielectriclossandcapacitance_Threewinding.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "dielectriclossandcapacitance_threewindingresults", Dielectriclossandcapacitance_Threewindingresults); Dielectriclossandcapacitance_Threewindingresults.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "insulationresistance_threewinding", Insulationresistance_Threewinding); Insulationresistance_Threewinding.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "insulationresistance_threewindingresults", Insulationresistance_Threewindingresults); Insulationresistance_Threewindingresults.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "parameter_information", Parameter_Information); Parameter_Information.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "sample_information", Sample_Information); Sample_Information.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "tapchangertest", Tapchangertest); Tapchangertest.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "tapchangertestresults", Tapchangertestresults); Tapchangertestresults.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "windingcoreinsulationresistance", Windingcoreinsulationresistance); Windingcoreinsulationresistance.AcceptChanges();
                LocalSQLClient.updateDataTable(database, "windingcoreinsulationresistanceresults", Windingcoreinsulationresistanceresults); Windingcoreinsulationresistanceresults.AcceptChanges();
                return LocalSQLClient.Connected;

            }
            return false;
        }
        public Location getLocation(string name)
        {
            Location rtn = new Location();
            rtn.id = -1;

            name = name.Trim();

            if ((this.Locations != null) && (this.Locations.Rows.Count > 0))
            {
                foreach (System.Data.DataRow row in this.Locations.Rows)
                {
                    if (row["name"].ToString() == name)
                    {
                        rtn.id = int.Parse(row["id"].ToString());
                        rtn.name = row["name"].ToString();
                        rtn.company = row["company"].ToString();
                        rtn.address = row["address"].ToString();
                        rtn.operatorName = row["operator"].ToString();
                        return rtn;
                    }
                }
            }
            return rtn;
        }

        public List<string> getLocationName()
        {
            List<string> list = new List<string>();
            if ((this.Locations != null) && (this.Locations.Rows.Count > 0))
                for (int i = this.Locations.Rows.Count - 1; i >= 0; i--)
                {
                    list.Add(this.Locations.Rows[i]["name"].ToString());
                }
            return list;
        }

        public int getLocationID(string name)
        {
            List<string> list = new List<string>();
            if ((this.Locations != null) && (this.Locations.Rows.Count > 0))
                foreach (System.Data.DataRow row in this.Locations.Rows)
                {
                    if (row["name"].ToString() == name)
                        return int.Parse(row["id"].ToString());
                }
            return -1;
        }

        public bool addLocation(string name, string company = "", string address = "", string operatorName = "", int id = -1)
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                LocalSQLClient.insertDataRow(database, "location", new string[] { "name", "company", "address", "operator" }, new string[] { name, company, address, operatorName }, id);
                return LocalSQLClient.Connected;
            }
            return false;
        }

        public bool deleteLocation(string name)
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                LocalSQLClient.deleteDataRow(database, "location", "name", name);
            }
            return false;
        }


        public bool updateTransformer()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                this.Transformers = LocalSQLClient.getDataTable(database, "transformer");
                return LocalSQLClient.Connected;
            }
            return false;
        }

        public bool saveTransformer()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                LocalSQLClient.updateDataTable(database, "transformer", Transformers);
                Transformers.AcceptChanges();
                return LocalSQLClient.Connected;
            }
            return false;
        }

        public List<string> getTransformerSerialNo(string location = "")
        {
            List<string> list = new List<string>();
            if (location == string.Empty)
            {
                foreach (DataRow row in Transformers.Rows)
                {
                    list.Add(row["serialno"].ToString());
                }
                return list;
            }
            location = location.Trim();
            foreach (DataRow row in Transformers.Rows)
            {
                if (row["location"].ToString() == location) list.Add(row["serialno"].ToString());
            }
            return list;
        }

       

        public Transformer getTransformer(string serialNo = "")
        {
            Transformer transformer = new Transformer();
            if (serialNo == string.Empty) return transformer;
            serialNo = serialNo.Trim();
            DataRow[] rows = WorkingSets.local.Transformers.Select("serialno = '" + serialNo + "'");
            if (rows.Length < 1)
                return transformer;
            DataRow r = rows[0];
            transformer.ID = (int)r["id"];
            transformer.SerialNo = (string)r["serialno"];
            transformer.Location = (string)r["location"];
            transformer.ApparatusID = (string)r["apparatusid"];
            transformer.Manufacturer = (string)r["manufacturer"];
            transformer.ProductionYear = (string)r["productionyear"];
            transformer.AssetSystemCode = (string)r["assetsystemcode"];
            transformer.PhaseNum = (int)r["phases"];
            transformer.WindingNum = (int)r["windings"];
            transformer.RatingFrequency = ((int)r["ratedfrequency"]);
            transformer.WindingConfig.HV = (TransformerWindingConfigName)(int)r["windingconfig_hv"];
            transformer.WindingConfig.MV = (TransformerWindingConfigName)(int)r["windingconfig_mv"];
            transformer.WindingConfig.MVLabel = (int)r["windingconfig_mv_label"];
            transformer.WindingConfig.LV = (TransformerWindingConfigName)(int)r["windingconfig_lv"];
            transformer.WindingConfig.LVLabel = (int)r["windingconfig_lv_label"];
            transformer.VoltageRating.HV = ((double)r["voltageratinghv"]);
            transformer.VoltageRating.MV = ((double)r["voltageratingmv"]);
            transformer.VoltageRating.LV = ((double)r["voltageratinglv"]);
            transformer.PowerRating.HV = ((double)r["powerratinghv"]);
            transformer.PowerRating.MV = ((double)r["powerratingmv"]);
            transformer.PowerRating.LV = ((double)r["powerratinglv"]);
            transformer.Bushing.HVContained = (bool)r["bushing_hv_enabled"];
            transformer.Bushing.MVContained = (bool)r["bushing_mv_enabled"];
            transformer.OLTC.Contained = (((int)r["oltc_tapnum"]) > -1);
            if (transformer.OLTC.Contained == true)
            {
                switch ((int)r["oltc_winding"])
                {
                    case 0:
                        transformer.OLTC.WindingPosition = WindingType.HV;
                        break;
                    case 1:
                        transformer.OLTC.WindingPosition = WindingType.MV;
                        break;
                    case 2:
                        transformer.OLTC.WindingPosition = WindingType.LV;
                        break;
                    default:
                        throw new Exception();
                }
                switch ((int)r["oltc_tapnum"])
                {
                    case 0:
                        transformer.OLTC.TapNum = 3;
                        break;
                    case 1:
                        transformer.OLTC.TapNum = 5;
                        break;
                    case 2:
                        transformer.OLTC.TapNum = 9;
                        break;
                    case 3:
                        transformer.OLTC.TapNum = 11;
                        break;
                    case 4:
                        transformer.OLTC.TapNum = 17;
                        break;
                    case 5:
                        transformer.OLTC.TapNum = 21;
                        break;
                    default:
                        throw new Exception();
                }
                switch (r["oltc_step"])
                {
                    case 0:
                        transformer.OLTC.Step = 0.005;
                        break;
                    case 1:
                        transformer.OLTC.Step = 0.01;
                        break;
                    case 2:
                        transformer.OLTC.Step = 0.0125;
                        break;
                    case 3:
                        transformer.OLTC.Step = 0.02;
                        break;
                    case 4:
                        transformer.OLTC.Step = 0.025;
                        break;
                    case 5:
                        transformer.OLTC.Step = 0.05;
                        break;
                    case 6:
                        transformer.OLTC.Step = 0.1;
                        break;
                    default:
                        throw new Exception();
                }
                transformer.OLTC.TapMainNum = (int)r["oltc_tapmainnum"];
                transformer.OLTC.SerialNo = (string)r["oltc_serialno"];
                transformer.OLTC.ModelType = (string)r["oltc_modeltype"];
                transformer.OLTC.Manufacturer = (string)r["oltc_manufacturer"];
                transformer.OLTC.ProductionYear = (string)r["oltcproductionyear"];
            }
            return transformer;
        }

        public Transformer getTransformer(int transformerID)
        {
            Transformer transformer = new Transformer() { ID = -1 };
            if (transformerID < 1) return transformer;
            DataRow[] rows = WorkingSets.local.Transformers.Select("id = '" + transformerID.ToString() + "'");
            if (rows.Length < 1)
                return transformer;
            DataRow r = rows[0];
            transformer.ID = (int)r["id"];
            transformer.SerialNo = (string)r["serialno"];
            transformer.Location = (string)r["location"];
            transformer.ApparatusID = (string)r["apparatusid"];
            transformer.Manufacturer = (string)r["manufacturer"];
            transformer.ProductionYear = (string)r["productionyear"];
            transformer.AssetSystemCode = (string)r["assetsystemcode"];
            transformer.PhaseNum = (int)r["phases"];
            transformer.WindingNum = (int)r["windings"];
            transformer.RatingFrequency = ((int)r["ratedfrequency"]);
            transformer.WindingConfig.HV = (TransformerWindingConfigName)(int)r["windingconfig_hv"];
            transformer.WindingConfig.MV = (TransformerWindingConfigName)(int)r["windingconfig_mv"];
            transformer.WindingConfig.MVLabel = (int)r["windingconfig_mv_label"];
            transformer.WindingConfig.LV = (TransformerWindingConfigName)(int)r["windingconfig_lv"];
            transformer.WindingConfig.LVLabel = (int)r["windingconfig_lv_label"];
            transformer.VoltageRating.HV = ((double)r["voltageratinghv"]);
            transformer.VoltageRating.MV = ((double)r["voltageratingmv"]);
            transformer.VoltageRating.LV = ((double)r["voltageratinglv"]);
            transformer.PowerRating.HV = ((double)r["powerratinghv"]);
            transformer.PowerRating.MV = ((double)r["powerratingmv"]);
            transformer.PowerRating.LV = ((double)r["powerratinglv"]);
            transformer.Bushing.HVContained = (bool)r["bushing_hv_enabled"];
            transformer.Bushing.MVContained = (bool)r["bushing_mv_enabled"];
            transformer.OLTC.Contained = (((int)r["oltc_tapnum"]) > -1);
            if (transformer.OLTC.Contained == true)
            {
                switch ((int)r["oltc_winding"])
                {
                    case 0:
                        transformer.OLTC.WindingPosition = WindingType.HV;
                        break;
                    case 1:
                        transformer.OLTC.WindingPosition = WindingType.MV;
                        break;
                    case 2:
                        transformer.OLTC.WindingPosition = WindingType.LV;
                        break;
                    default:
                        throw new Exception();
                }
                switch ((int)r["oltc_tapnum"])
                {
                    case 0:
                        transformer.OLTC.TapNum = 3;
                        break;
                    case 1:
                        transformer.OLTC.TapNum = 5;
                        break;
                    case 2:
                        transformer.OLTC.TapNum = 9;
                        break;
                    case 3:
                        transformer.OLTC.TapNum = 11;
                        break;
                    case 4:
                        transformer.OLTC.TapNum = 17;
                        break;
                    case 5:
                        transformer.OLTC.TapNum = 21;
                        break;
                    default:
                        throw new Exception();
                }
                switch (r["oltc_step"])
                {
                    case 0:
                        transformer.OLTC.Step = 0.005;
                        break;
                    case 1:
                        transformer.OLTC.Step = 0.01;
                        break;
                    case 2:
                        transformer.OLTC.Step = 0.0125;
                        break;
                    case 3:
                        transformer.OLTC.Step = 0.02;
                        break;
                    case 4:
                        transformer.OLTC.Step = 0.025;
                        break;
                    case 5:
                        transformer.OLTC.Step = 0.05;
                        break;
                    case 6:
                        transformer.OLTC.Step = 0.1;
                        break;
                    default:
                        throw new Exception();
                }
                transformer.OLTC.TapMainNum = (int)r["oltc_tapmainnum"];
                transformer.OLTC.SerialNo = (string)r["oltc_serialno"];
                transformer.OLTC.ModelType = (string)r["oltc_modeltype"];
                transformer.OLTC.Manufacturer = (string)r["oltc_manufacturer"];
                transformer.OLTC.ProductionYear = (string)r["oltcproductionyear"];
            }
            return transformer;
        }

        public bool updateJob()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                this.Jobs = LocalSQLClient.getDataTable(database, "measurementjob");
                return LocalSQLClient.Connected;
            }
            return false;
        }

        private JobList getJob(DataRow jobRow)
        {
            JobList job = new JobList();
            try
            {
                job.id = (int)jobRow["id"];
                job.Transformer = getTransformer((string)jobRow["TransformerSerialNo"]);
                job.Name = (string)jobRow["JobName"];
                job.DCInsulation.Enabled = (bool)jobRow["WindingDCInsulation"];
                job.DCInsulation.HVEnabled = (bool)jobRow["HVWindingDCInsulation"];
                job.DCInsulation.MVEnabled = (bool)jobRow["MVWindingDCInsulation"];
                job.DCInsulation.LVEnabled = (bool)jobRow["LVWindingDCInsulation"];
                job.Capacitance.Enabled = (bool)jobRow["WindingCapacitance"];
                job.Capacitance.HVEnabled = (bool)jobRow["HVWindingCapacitance"];
                job.Capacitance.MVEnabled = (bool)jobRow["MVWindingCapacitance"];
                job.Capacitance.LVEnabled = (bool)jobRow["LVWindingCapacitance"];
                job.DCResistance.Enabled = (bool)jobRow["WindingDCResistance"];
                job.DCResistance.HVEnabled = (bool)jobRow["HVWindingDCResistance"];
                job.DCResistance.MVEnabled = (bool)jobRow["MVWindingDCResistance"];
                job.DCResistance.LVEnabled = (bool)jobRow["LVWindingDCResistance"];
                job.Bushing.DCInsulation = (bool)jobRow["BushingDCInsulation"];
                job.Bushing.Capacitance = (bool)jobRow["BushingCapacitance"];
                job.OLTC.Enabled = (bool)jobRow["OLTC"];
                job.OLTC.Range = (int)jobRow["OLTCRangeTextBox"];
                job.OLTC.DCResistance = (bool)jobRow["OLTCDCResistance"];
                job.OLTC.SwitchingCharacter = (bool)jobRow["OLTCSwitching"];
                job.Parameter.DCInsulationVoltage = (int)jobRow["dci_voltage"];
                job.Parameter.DCInsulationResistance = (double)jobRow["dci_resistance"];
                job.Parameter.DCInsulationAbsorptionRatio = (double)jobRow["dci_ar"];
                job.Parameter.CapacitanceVoltage = (int)jobRow["cap_voltage"];
                job.Parameter.DCResistanceCurrent = (int)jobRow["dcr_current"];
                job.Parameter.BushingDCInsulationVoltage = (int)jobRow["bushing_dci_voltage"];
                job.Parameter.BushingCapacitanceVoltage = (int)jobRow["bushing_cap_voltage"];
            }
            catch (Exception ex)
            {
#if DEBUG
#else
                ex.ToString();
                job = new JobList() ;
#endif
            }
            return job;
        }

        public JobList getJob(string TransformerSerialNo, string JobName)
        {
            JobList job = new JobList();
            TransformerSerialNo = TransformerSerialNo.Trim();
            JobName = JobName.Trim();
            DataRow[] rows = WorkingSets.local.Jobs.Select("TransformerSerialNo = '" + TransformerSerialNo + "' and JobName = '" + JobName + "'");
            if (rows.Length > 0)
                return getJob(rows[0]);
            return job;
        }

        public JobList getJob(int jobID)
        {
            JobList job = new JobList();
            DataRow[] rows = WorkingSets.local.Jobs.Select("id = '" + jobID.ToString() + "'");
            if (rows.Length > 0)
                return getJob(rows[0]);
            return job;
        }

        public List<JobList> getJobs(string TransformerSerialNo = "")
        {
            List<JobList> JobList = new List<JobList>();
            TransformerSerialNo = TransformerSerialNo.Trim();
            if (TransformerSerialNo == string.Empty) return JobList;
            DataRow[] rows = WorkingSets.local.Jobs.Select("TransformerSerialNo = '" + TransformerSerialNo + "'");
            foreach (DataRow row in rows)
            {
                
                JobList.Add(getJob(row));
            }
            return JobList;
        }

        public List<string> getJobNames(string TransformerSerialNo)
        {
            List<string> jobNames = new List<string>();
            TransformerSerialNo = TransformerSerialNo.Trim();
            if (TransformerSerialNo == string.Empty) return jobNames;
            DataRow[] rows = WorkingSets.local.Jobs.Select("TransformerSerialNo = '" + TransformerSerialNo + "'");
            foreach (DataRow row in rows)
            {
                jobNames.Add((string)row["JobName"]);
            }
            return jobNames;
        }

        public bool saveJobs()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                LocalSQLClient.updateDataTable(database, "measurementjob", Jobs);
                Jobs.AcceptChanges();
                return LocalSQLClient.Connected;
            }
            return false;
        }

        public bool refreshTestResults()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                this.TestResults = LocalSQLClient.getDataTable(database, "testresult");
                return LocalSQLClient.Connected;
            }
            return false;
        }


        public bool saveTestResults()
        {
            if ((!disposedValue) && (LocalSQLClient != null))
            {
                LocalSQLClient.updateDataTable(database, "testresult", TestResults);
                TestResults.AcceptChanges();
                return LocalSQLClient.Connected;
            }
            return false;
        }

        public List<string> getTestResultsFromJobID(int JobID)
        {
            List<string> ls = new List<string>();
            string selectStr = "mj_id = " + JobID.ToString();
            var rows = TestResults.Select(selectStr);
            foreach(var r in rows)
            {
                if ((int)r["function"] == (int)MeasurementFunction.Information)
                {
                    var ji = JobInformation.FromString((string)r["waves"]);
                    ls.Add(ji.testingName + "(" + ji.testingTime.ToString("yyyy-MM-dd") + ")");
                }
            }
            return ls;
        }

        public TestingWorkerSender getTestResults(string s)
        {
            var rows = TestResults.Select("function = " + ((int)MeasurementFunction.Information).ToString());
            int testid = 0;
            bool valid = false;
            foreach (var r in rows)
            {
                var ji = JobInformation.FromString((string)r["waves"]);
                if ((ji.testingName + "(" + ji.testingTime.ToString("yyyy-MM-dd") + ")") == s)
                {
                    testid = (int)r["testid"];
                    valid = true;
                }
            }
            if (!valid) throw new Exception("测试结果查找为空");
            rows = TestResults.Select("testid = " + testid.ToString());
            if (rows.Length <= 0) throw new Exception("测试结果导出出错");
            var tws = new TestingWorkerSender();
            tws.job = getJob((int)rows[0]["mj_id"]);
            tws.Transformer = getTransformer((int)rows[0]["transformerid"]);
            tws.ProgressPercent = 100;
            tws.CurrentItemIndex = 0;
            List<MeasurementItemStruct> mis = new List<MeasurementItemStruct>();
            foreach(var r in rows)
            {
                if ((int)r["function"] != (int)MeasurementFunction.Information)
                    mis.Add(MeasurementItemStruct.FromDataRow(r));
                else
                    tws.job.Information = JobInformation.FromString((string)r["waves"]);
            }
            tws.MeasurementItems = mis.ToArray();
            return tws;
        }
      


        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (LocalSQLClient != null) LocalSQLClient.Dispose();
                    if (Locations != null) Locations.Dispose();
                    if (Transformers != null) Transformers.Dispose();
                    if (Jobs != null) Jobs.Dispose();
                    if (TestResults != null) TestResults.Dispose();
                    if (Reports != null) Reports.Dispose();
                    if (Casingtest_Commonbody != null) Casingtest_Commonbody.Dispose();
                    if (Casingtest_Commonbodyresults != null) Casingtest_Commonbodyresults.Dispose();
                    if (Conclusion != null) Conclusion.Dispose();
                    if (Dcresistor_Highpressure != null) Dcresistor_Highpressure.Dispose();
                    if (Dcresistor_Highpressureresults != null) Dcresistor_Highpressureresults.Dispose();
                    if (Dcresistor_Lowpressure != null) Dcresistor_Lowpressure.Dispose();
                    if (Dcresistor_Lowpressureresults != null) Dcresistor_Lowpressureresults.Dispose();
                    if (Dcresistor_Mediumvoltage != null) Dcresistor_Mediumvoltage.Dispose();
                    if (Dcresistor_Mediumvoltageresults != null) Dcresistor_Mediumvoltageresults.Dispose();
                    if (Dielectriclossandcapacitance_Threewinding != null) Dielectriclossandcapacitance_Threewinding.Dispose();
                    if (Dielectriclossandcapacitance_Threewindingresults != null) Dielectriclossandcapacitance_Threewindingresults.Dispose();
                    if (Insulationresistance_Threewinding != null) Insulationresistance_Threewinding.Dispose();
                    if (Insulationresistance_Threewindingresults != null) Insulationresistance_Threewindingresults.Dispose();
                    if (Parameter_Information != null) Parameter_Information.Dispose();
                    if (Sample_Information != null) Sample_Information.Dispose();
                    if (Tapchangertest != null) Tapchangertest.Dispose();
                    if (Tapchangertestresults != null) Tapchangertestresults.Dispose();
                    if (Windingcoreinsulationresistance != null) Windingcoreinsulationresistance.Dispose();
                    if (Windingcoreinsulationresistanceresults != null) Windingcoreinsulationresistanceresults.Dispose();
         
    }

                GC.SuppressFinalize(this);

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~WorkingDBClass() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public static class WorkingSets
    {
        public static WorkingDB local = new WorkingDB();
    }
}
