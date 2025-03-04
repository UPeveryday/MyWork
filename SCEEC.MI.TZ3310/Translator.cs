﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    public static class Translator
    {
        public static List<MeasurementItemStruct> JobList2MeasurementItems(JobList jobList)
        {
            List<MeasurementItemStruct> miList = new List<MeasurementItemStruct>();
            //miList.Add(MeasurementItemStruct.CreateInformation("添加使用用户信息模块"));//问题

            int TapNum = (jobList.Transformer.OLTC.TapNum - 1) / 2;
            if (jobList.Transformer.OLTC.Contained == true)
                miList.Add(MeasurementItemStruct.CreateText("将变压器有载分接开关位置切换到额定分接(分接" + (TapNum + 1).ToString() + "B)位置;"));

            if(jobList.DCInsulation.Enabled || jobList.Bushing.DCInsulation)
            {
                miList.Add(MeasurementItemStruct.CreateText("使用绝缘电阻试验模块：")); 
                if (jobList.DCInsulation.HVEnabled)
                    miList.Add(MeasurementItemStruct.CreateDCInsulationMeasurementItem(WindingType.HV));
                if ((jobList.Bushing.DCInsulation) && (jobList.Transformer.Bushing.HVContained))
                {
                    if (jobList.Transformer.WindingConfig.HV == TransformerWindingConfigName.Yn)
                        miList.Add(MeasurementItemStruct.CreateBushingDCInsulationMeasurementItem(WindingType.HV, WindingTerimal.O));
                    miList.Add(MeasurementItemStruct.CreateBushingDCInsulationMeasurementItem(WindingType.HV, WindingTerimal.A));
                    miList.Add(MeasurementItemStruct.CreateBushingDCInsulationMeasurementItem(WindingType.HV, WindingTerimal.B));
                    miList.Add(MeasurementItemStruct.CreateBushingDCInsulationMeasurementItem(WindingType.HV, WindingTerimal.C));
                }
                if (jobList.DCInsulation.MVEnabled)
                    miList.Add(MeasurementItemStruct.CreateDCInsulationMeasurementItem(WindingType.MV));
                if ((jobList.Bushing.DCInsulation) && (jobList.Transformer.Bushing.MVContained))
                {
                    if (jobList.Transformer.WindingConfig.MV == TransformerWindingConfigName.Yn)
                        miList.Add(MeasurementItemStruct.CreateBushingDCInsulationMeasurementItem(WindingType.MV, WindingTerimal.O));
                    miList.Add(MeasurementItemStruct.CreateBushingDCInsulationMeasurementItem(WindingType.MV, WindingTerimal.A));
                    miList.Add(MeasurementItemStruct.CreateBushingDCInsulationMeasurementItem(WindingType.MV, WindingTerimal.B));
                    miList.Add(MeasurementItemStruct.CreateBushingDCInsulationMeasurementItem(WindingType.MV, WindingTerimal.C));
                }
                if (jobList.DCInsulation.LVEnabled)
                    miList.Add(MeasurementItemStruct.CreateDCInsulationMeasurementItem(WindingType.LV));
            }

            if(jobList.Capacitance.Enabled || jobList.Bushing.Capacitance)
            {
                miList.Add(MeasurementItemStruct.CreateText("使用电容量及介质损耗试验模块："));
                if(jobList.Capacitance.HVEnabled)
                    miList.Add(MeasurementItemStruct.CreateCapacitanceMeasurementItem(WindingType.HV));
                if ((jobList.Bushing.Capacitance) && (jobList.Transformer.Bushing.HVContained))
                {
                    if (jobList.Transformer.WindingConfig.HV == TransformerWindingConfigName.Yn)
                        miList.Add(MeasurementItemStruct.CreateBushingCapacitanceMeasurementItem(WindingType.HV, WindingTerimal.O));
                    miList.Add(MeasurementItemStruct.CreateBushingCapacitanceMeasurementItem(WindingType.HV, WindingTerimal.A));
                    miList.Add(MeasurementItemStruct.CreateBushingCapacitanceMeasurementItem(WindingType.HV, WindingTerimal.B));
                    miList.Add(MeasurementItemStruct.CreateBushingCapacitanceMeasurementItem(WindingType.HV, WindingTerimal.C));
                }
                if (jobList.Capacitance.MVEnabled)
                    miList.Add(MeasurementItemStruct.CreateCapacitanceMeasurementItem(WindingType.MV));
                if ((jobList.Bushing.Capacitance) && (jobList.Transformer.Bushing.MVContained))
                {
                    if (jobList.Transformer.WindingConfig.MV == TransformerWindingConfigName.Yn)
                        miList.Add(MeasurementItemStruct.CreateBushingCapacitanceMeasurementItem(WindingType.MV, WindingTerimal.O));
                    miList.Add(MeasurementItemStruct.CreateBushingCapacitanceMeasurementItem(WindingType.MV, WindingTerimal.A));
                    miList.Add(MeasurementItemStruct.CreateBushingCapacitanceMeasurementItem(WindingType.MV, WindingTerimal.B));
                    miList.Add(MeasurementItemStruct.CreateBushingCapacitanceMeasurementItem(WindingType.MV, WindingTerimal.C));
                }
                if (jobList.Capacitance.LVEnabled)
                    miList.Add(MeasurementItemStruct.CreateCapacitanceMeasurementItem(WindingType.LV));
            }

            if (jobList.DCResistance.Enabled)
            {
                miList.Add(MeasurementItemStruct.CreateText("使用直流电阻试验模块："));
                if ((jobList.DCResistance.HVEnabled) && (!((jobList.OLTC.Enabled) && (jobList.Transformer.OLTC.WindingPosition == WindingType.HV) && (jobList.OLTC.DCResistance))))
                {
                    if ((jobList.Transformer.PhaseNum == 3))
                    {
                        if (jobList.Transformer.WindingConfig.HV == TransformerWindingConfigName.Yn)
                        {
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.HV));
                        }
                        else
                        {
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.HV, WindingTerimal.A, WindingTerimal.B));
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.HV, WindingTerimal.B, WindingTerimal.C));
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.HV, WindingTerimal.C, WindingTerimal.A));
                        }
                    }
                    else
                    {
                        miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.HV, WindingTerimal.A, WindingTerimal.O));
                    }
                }
                if ((jobList.DCResistance.MVEnabled) && (!((jobList.OLTC.Enabled) && (jobList.Transformer.OLTC.WindingPosition == WindingType.MV) && (jobList.OLTC.DCResistance))))
                {
                    if ((jobList.Transformer.PhaseNum == 3))
                    {
                        if (jobList.Transformer.WindingConfig.MV == TransformerWindingConfigName.Yn)
                        {
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.MV));
                        }
                        else
                        {
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.MV, WindingTerimal.A, WindingTerimal.B));
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.MV, WindingTerimal.B, WindingTerimal.C));
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.MV, WindingTerimal.C, WindingTerimal.A));
                        }
                    }
                    else
                    {
                        miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.MV, WindingTerimal.A, WindingTerimal.O));
                    }
                }
                if (jobList.DCResistance.LVEnabled)
                {
                    if ((jobList.Transformer.PhaseNum == 3))
                    {
                        if (jobList.Transformer.WindingConfig.LV == TransformerWindingConfigName.Yn)
                        {
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.LV));
                        }
                        else
                        {
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.LV, WindingTerimal.A, WindingTerimal.B));
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.LV, WindingTerimal.B, WindingTerimal.C));
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.LV, WindingTerimal.C, WindingTerimal.A));
                        }
                    }
                    else
                    {
                        miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(WindingType.LV, WindingTerimal.A, WindingTerimal.O));
                    }
                }
            }

            if (jobList.OLTC.Enabled)
            {
                int range = jobList.OLTC.Range;
                int lowest = TapNum - range + 1;
                int highest = TapNum + range + 1;
                miList.Add(MeasurementItemStruct.CreateText("使用直阻与有载分接试验模块："));

                int i = TapNum + 1;
                char j = (char)((int)'A' + (jobList.Transformer.OLTC.TapMainNum - 1) / 2);
                int k;
                string lastTapName = i.ToString();
                string currentTapName;

                if (jobList.Transformer.OLTC.TapMainNum > 1)
                {
                    for (k = (jobList.Transformer.OLTC.TapMainNum - 1) / 2; k > 0; k--)
                    {
                        j = (char)((int)'A' + k);
                        lastTapName = i.ToString() + j.ToString();
                        currentTapName = i.ToString() + ((char)(j - 1)).ToString();
                        if (jobList.OLTC.DCResistance == true)
                        {
                            miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                        }
                        if (jobList.OLTC.SwitchingCharacter)
                        {
                            if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                                miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                            if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                                miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                        }
                        lastTapName = currentTapName;
                    }
                }

                for (i = TapNum; i >= lowest; i--)
                {
                    currentTapName = i.ToString();
                    if (jobList.OLTC.DCResistance)
                    {
                        miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                    }
                    if (jobList.OLTC.SwitchingCharacter)
                    {
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                    }
                    lastTapName = currentTapName;
                }

                if (jobList.OLTC.DCResistance)
                {
                    miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                }

                for (i = lowest + 1; i <= TapNum; i++)
                {
                    currentTapName = i.ToString();
                    //if (jobList.OLTC.DCResistance)
                    //{
                    //    miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                    //}
                    if (jobList.OLTC.SwitchingCharacter)
                    {
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                    }
                    lastTapName = currentTapName;
                }

                i = TapNum + 1;

                if (jobList.Transformer.OLTC.TapMainNum > 1)
                {
                    for (k = 1; k <= jobList.Transformer.OLTC.TapMainNum; k++)
                    {
                        j = (char)((int)'A' + k - 1);
                        currentTapName = i.ToString() + j.ToString();
                        //if (jobList.OLTC.DCResistance)
                        //{
                        //    miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                        //}
                        if (jobList.OLTC.SwitchingCharacter)
                        {
                            if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                                miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                            if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                                miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                        }
                        lastTapName = currentTapName;
                    }
                }
                else
                {
                    currentTapName = i.ToString();
                    //if (jobList.OLTC.DCResistance)
                    //{
                    //    miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                    //}
                    if (jobList.OLTC.SwitchingCharacter)
                    {
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                    }
                    lastTapName = currentTapName;
                }

                for (i = TapNum + 2; i <= highest; i++)
                {
                    currentTapName = i.ToString();
                    if (jobList.OLTC.DCResistance)
                    {
                        miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                    }
                    if (jobList.OLTC.SwitchingCharacter)
                    {
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                    }
                    lastTapName = currentTapName;
                }

                if (jobList.OLTC.DCResistance)
                {
                    miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                }

                for (i = highest - 1; i > (TapNum + 1); i--)
                {
                    currentTapName = i.ToString();
                    //if (jobList.OLTC.DCResistance)
                    //{
                    //    miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                    //}
                    if (jobList.OLTC.SwitchingCharacter)
                    {
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                    }
                    lastTapName = currentTapName;
                }

                i = TapNum + 1;

                if (jobList.Transformer.OLTC.TapMainNum > 1)
                {
                    for (k = jobList.Transformer.OLTC.TapMainNum; k > ((jobList.Transformer.OLTC.TapMainNum - 1) / 2); k--)
                    {
                        j = (char)((int)'A' + k - 1);
                        currentTapName = i.ToString() + j.ToString();
                        //if (jobList.OLTC.DCResistance)
                        //{
                        //    miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                        //}
                        if (jobList.OLTC.SwitchingCharacter)
                        {
                            if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                                miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                            if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                                miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                        }
                        lastTapName = currentTapName;
                    }
                }
                else
                {
                    currentTapName = i.ToString();
                    //if (jobList.OLTC.DCResistance)
                    //{
                    //    miList.Add(MeasurementItemStruct.CreateDCResistanceMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName));
                    //}
                    if (jobList.OLTC.SwitchingCharacter)
                    {
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.HV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.HV));
                        if (jobList.Transformer.OLTC.WindingPosition == WindingType.MV)
                            miList.Add(MeasurementItemStruct.CreateOLTCSwitchingCharacterMeasurementItem(jobList.Transformer.OLTC.WindingPosition, lastTapName, currentTapName, jobList.Transformer.WindingConfig.MV));
                    }
                    lastTapName = currentTapName;
                }
            }

            return miList;
        }
    }
}
