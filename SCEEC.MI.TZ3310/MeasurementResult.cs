﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCEEC.Numerics;

namespace SCEEC.MI.TZ3310
{
    [Serializable]
    public class MeasurementResult : IComparable<MeasurementResult>
    {
        public readonly MeasurementFunction Function;
        public readonly PhysicalVariable[] values;
        public readonly DateTime recordTime;
        public readonly bool processing;
        public readonly short[] waves;
        public readonly bool readyToTrigger;
        public readonly short[] Usermessage;

        public bool isFinalResult
        {
            get
            {
                return !processing;
            }
        }

        internal MeasurementResult(MeasurementFunction function, PhysicalVariable[] values, bool processing)
        {
            this.Function = function;
            this.values = values;
            this.processing = processing;
            this.recordTime = DateTime.Now;
        }

        internal MeasurementResult(MeasurementFunction function, PhysicalVariable[] values, short[] waves, bool processing, DateTime time)
        {
            this.Function = function;
            this.values = values;
            this.processing = processing;
            this.recordTime = time;
            this.waves = waves;
        }

        internal MeasurementResult(MeasurementFunction function, PhysicalVariable[] values, short[] waves, bool readyToTrigger, bool processing)
        {
            this.Function = function;
            this.values = values;
            this.waves = waves;
            this.processing = processing;
            this.readyToTrigger = readyToTrigger;
            this.recordTime = DateTime.Now;
        }

        internal MeasurementResult(MeasurementFunction function, PhysicalVariable[] values, short[] waves, bool readyToTrigger, bool processing, DateTime recordTime)
        {
            this.Function = function;
            this.values = values;
            this.waves = waves;
            this.processing = processing;
            this.readyToTrigger = readyToTrigger;
            this.recordTime = recordTime;
        }

        internal MeasurementResult(MeasurementFunction function, short[] values, bool processing)
        {
            this.Function = function;
            this.Usermessage = values;
            this.processing = processing;
            this.recordTime = DateTime.Now;
        }

        /// <summary>
        /// 创建测量结果（绕组绝缘电阻项目）
        /// </summary>
        /// <param name="testingItem">测量项目</param>
        /// <param name="voltage">电压</param>
        /// <param name="resistance">电阻</param>
        /// <param name="absorption">吸收比</param>
        /// <param name="polarization">极化指数</param>
        /// <param name="isOutputResult">是否为最终输出结果</param
        /// <returns>测量结果</returns>
        public static MeasurementResult NewDCInsulationResult(MeasurementItemStruct testingItem, PhysicalVariable voltage, PhysicalVariable resistance, PhysicalVariable absorption, PhysicalVariable polarization, bool isOutputResult)
        {
            if ((testingItem == null) || (testingItem.Function != MeasurementFunction.DCInsulation)) throw new ArgumentException("测试结果与测试项目不符");
            //if ((voltage == null) || (voltage.PhysicalVariableType != Numerics.Quantities.QuantityName.Voltage)) throw new ArgumentException("电压参数错误");
            //if ((resistance == null) || (resistance.PhysicalVariableType != Numerics.Quantities.QuantityName.Resistance)) throw new ArgumentException("电阻参数错误");
            return new MeasurementResult(MeasurementFunction.DCInsulation, new PhysicalVariable[4] { voltage, resistance, absorption, polarization }, !isOutputResult);
        }

        /// <summary>
        /// 创建测量结果（套管绝缘电阻项目）
        /// </summary>
        /// <param name="testingItem">测量项目</param>
        /// <param name="voltage">电压</param>
        /// <param name="resistance">电阻</param>
        /// <param name="absorption">吸收比</param>
        /// <param name="polarization">极化指数</param>
        /// <param name="isOutputResult">是否为最终输出结果</param
        /// <returns>测量结果</returns>
        public static MeasurementResult NewBushingDCInsulationResult(MeasurementItemStruct testingItem, PhysicalVariable voltage, PhysicalVariable resistance, PhysicalVariable absorption, PhysicalVariable polarization, bool isOutputResult)
        {
            if ((testingItem == null) || (testingItem.Function != MeasurementFunction.BushingDCInsulation)) throw new ArgumentException("测试结果与测试项目不符");
            //if ((voltage == null) || (voltage.PhysicalVariableType != Numerics.Quantities.QuantityName.Voltage)) throw new ArgumentException("电压参数错误");
            //if ((resistance == null) || (resistance.PhysicalVariableType != Numerics.Quantities.QuantityName.Resistance)) throw new ArgumentException("电阻参数错误");
            return new MeasurementResult(MeasurementFunction.BushingDCInsulation, new PhysicalVariable[4] { voltage, resistance, absorption, polarization }, !isOutputResult);
        }

        /// <summary>
        /// 创建测量结果（绕组电容量及介质损耗项目）
        /// </summary>
        /// <param name="testingItem">测量项目</param>
        /// <param name="voltage">电压</param>
        /// <param name="capacitance">电容量</param>
        /// <param name="tanDelta">介质损耗</param>
        /// <param name="frequency">频率</param>
        /// <param name="isOutputResult">是否为最终输出结果</param>
        /// <returns>测量结果</returns>
        public static MeasurementResult NewCapacitanceResult(MeasurementItemStruct testingItem, PhysicalVariable voltage, PhysicalVariable capacitance, PhysicalVariable tanDelta, PhysicalVariable frequency, PhysicalVariable current, bool isOutputResult)
        {
            if ((testingItem == null) || (testingItem.Function != MeasurementFunction.Capacitance)) throw new ArgumentException("测试结果与测试项目不符");
            //if ((voltage == null) || (voltage.PhysicalVariableType != Numerics.Quantities.QuantityName.Voltage)) throw new ArgumentException("电压参数错误");
            //if ((capacitance == null) || (capacitance.PhysicalVariableType != Numerics.Quantities.QuantityName.Capacitance)) throw new ArgumentException("电容参数错误");
            //if (isOutputResult && ((tanDelta == null) || (tanDelta.PhysicalVariableType != Numerics.Quantities.QuantityName.None))) throw new ArgumentException("介损参数错误");
            //if (!isOutputResult && ((frequency == null) || (frequency.PhysicalVariableType != Numerics.Quantities.QuantityName.Frequency))) throw new ArgumentException("介损参数错误");
            return new MeasurementResult(MeasurementFunction.Capacitance, new PhysicalVariable[5] { frequency, voltage, capacitance, tanDelta,  current }, !isOutputResult);
        }

        /// <summary>
        /// 创建测量结果（套管电容量及介质损耗项目）
        /// </summary>
        /// <param name="testingItem">测量项目</param>
        /// <param name="voltage">电压</param>
        /// <param name="capacitance">电容量</param>
        /// <param name="tanDelta">介质损耗</param>
        /// <param name="frequency">频率</param>
        /// <param name="isOutputResult">是否为最终输出结果</param>
        /// <returns>测量结果</returns>
        public static MeasurementResult NewBushingCapacitanceResult(MeasurementItemStruct testingItem, PhysicalVariable voltage, PhysicalVariable capacitance, PhysicalVariable tanDelta, PhysicalVariable frequency, PhysicalVariable current, bool isOutputResult)
        {
            if ((testingItem == null) || (testingItem.Function != MeasurementFunction.BushingCapacitance)) throw new ArgumentException("测试结果与测试项目不符");
            //if ((voltage == null) || (voltage.PhysicalVariableType != Numerics.Quantities.QuantityName.Voltage)) throw new ArgumentException("电压参数错误");
            //if ((capacitance == null) || (capacitance.PhysicalVariableType != Numerics.Quantities.QuantityName.Capacitance)) throw new ArgumentException("电容参数错误");
            //if (isOutputResult && ((tanDelta == null) || (tanDelta.PhysicalVariableType != Numerics.Quantities.QuantityName.None))) throw new ArgumentException("介损参数错误");
            //if (!isOutputResult && ((frequency == null) || (frequency.PhysicalVariableType != Numerics.Quantities.QuantityName.Frequency))) throw new ArgumentException("介损参数错误");
            return new MeasurementResult(MeasurementFunction.BushingCapacitance, new PhysicalVariable[5] { frequency, voltage, capacitance, tanDelta, current }, !isOutputResult);
        }

        /// <summary>
        /// 创建测量结果（绕组直流电阻项目）
        /// </summary>
        /// <param name="testingItem">测量项目</param>
        /// <param name="voltage">电压数组（A, B, C)</param>
        /// <param name="current">电流数组（A, B, C)</param>
        /// <param name="resistance">电阻数组（A, B, C)</param>
        /// <param name="isOutputResult">是否为最终输出结果</param>
        /// <returns>测量结果</returns>
        public static MeasurementResult NewDCResistanceResult(MeasurementItemStruct testingItem,PhysicalVariable[] voltage, PhysicalVariable[] current, PhysicalVariable[] resistance, bool isOutputResult)
        {
            if ((testingItem == null) || (testingItem.Function != MeasurementFunction.DCResistance)) throw new ArgumentException("测试结果与测试项目不符");
            //if (voltage.Length != 3) throw new ArgumentException("电压参数错误");
            //if (current.Length != 3) throw new ArgumentException("电压参数错误");
            //if (resistance.Length != 3) throw new ArgumentException("电压参数错误");
            //for (int i = 0; i < 3; i++)
            //{
            //    if ((voltage[i] == null) || (voltage[i].PhysicalVariableType != Numerics.Quantities.QuantityName.Voltage)) throw new ArgumentException("电压参数错误");
            //    if ((current[i] == null) || (current[i].PhysicalVariableType != Numerics.Quantities.QuantityName.Current)) throw new ArgumentException("电流参数错误");
            //    if ((resistance[i] == null) || (resistance[i].PhysicalVariableType != Numerics.Quantities.QuantityName.Resistance)) throw new ArgumentException("电阻参数错误");
            //}
            return new MeasurementResult(MeasurementFunction.DCResistance, new PhysicalVariable[9] { voltage[0], current[0], resistance[0], voltage[1], current[1], resistance[1], voltage[2], current[2], resistance[2] }, !isOutputResult);
        }

        /// <summary>
        /// 创建测量结果（绕组有载分接项目）
        /// </summary>
        /// <param name="testingItem">测量项目</param>
        /// <param name="voltage">电压数组</param>
        /// <param name="current">电流数组</param>
        /// <param name="resistance">电阻数组</param>
        /// <param name="waves">波形数组</param>
        /// <param name="readyForTrigger">是否提示触发</param>
        /// <param name="isOutputResult">是否为最终输出结果</param>
        /// <returns></returns>
        public static MeasurementResult NewOLTCSwitchingCharacterResult(MeasurementItemStruct testingItem, PhysicalVariable[] voltage, PhysicalVariable[] current, PhysicalVariable[] resistance, short[] waves, bool readyForTrigger, bool isOutputResult)
        {
            if ((testingItem == null) || (testingItem.Function != MeasurementFunction.OLTCSwitchingCharacter)) throw new ArgumentException("测试结果与测试项目不符");
            //if (waves.Length != 24008) throw new ArgumentException("波形组长度错误");
            return new MeasurementResult(MeasurementFunction.OLTCSwitchingCharacter, new PhysicalVariable[9] { voltage[0], current[0], resistance[0], voltage[1], current[1], resistance[1], voltage[2], current[2], resistance[2] }, waves, readyForTrigger, !isOutputResult);
        }
        public static MeasurementResult NewInformation(MeasurementItemStruct testingItem,short[] MessageText, bool isOutputResult)
        {
            if ((testingItem == null) || (testingItem.Function != MeasurementFunction.Information)) throw new ArgumentException("测试结果与测试项目不符");
            return new MeasurementResult(MeasurementFunction.Information, MessageText, !isOutputResult);
        }

        public int CompareTo(MeasurementResult other)
        {
            if (other == null) return 1;
            if (this.Function != other.Function)
            {
                return this.Function.CompareTo(other.Function);
            }
            if (this.processing != other.processing)
            {
                if (this.processing) return 1;
                else return -1;
            }
            return this.recordTime.CompareTo(other.recordTime);
        }

        //public override string ToString()
        //{
        //    switch (this.Function)
        //    {
        //        case MeasurementFunction.DCInsulation:
        //            if (this.values[1] == null) return measurementItemStruct.Winding.ToString() + "绝缘电阻: " + this.values[0].ToString();
        //            if (this.values[2] == null) return measurementItemStruct.Winding.ToString() + "绝缘电阻: " + this.values[0].ToString() + " 吸收比: " + this.values[1].ToString();
        //            return measurementItemStruct.Winding.ToString() + "绝缘电阻: " + this.values[0].ToString() + " 吸收比: " + this.values[1].ToString() + " 极化指数: " + this.values[2].ToString();
        //        case MeasurementFunction.Capacitance:
        //            return measurementItemStruct.Winding.ToString() + "电容量: " + this.values[2] + " 介质损耗：" + this.values[3];
        //        case MeasurementFunction.DCResistance:
        //            bool effective;
        //            if (measurementItemStruct.Terimal == null)
        //            {

        //            }
        //            else
        //            {

        //            }
        //        case MeasurementFunction.OLTCSwitchingCharacter:
        //        case MeasurementFunction.BushingDCInsulation:
        //        case MeasurementFunction.BushingCapacitance:
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}
    }

}
