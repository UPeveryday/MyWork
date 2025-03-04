﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCEEC.MI.TZ3310
{
    public  class Parameter
    {
        public enum TestKind
        {
            介质损耗 = 0,//介质损耗
            绝缘电阻 = 1,//绝缘电阻
            直流电阻 = 2,//直流电阻
            有载分接 = 3,//有载分解
            读取放电数据 = 4

        }

        public enum JSstation
        {
            高压绕组 = 0, 中压绕组 = 1, 低压绕组 = 2, 高压套管A = 3, 高压套管B = 4, 高压套管C = 5, 高压套管0 = 6,
            中压套管A = 7, 中压套管B = 8, 中压套管C = 9, 中压套管0 = 10, 高中绕组 = 11, 所有绕组 = 12

        }
        public enum JSstyle
        {
            内接正接 = 0, 内接反接 = 1

        }


        public enum JSVoilt
        {
            // [Description("0.5KV")]
            _0_5KV = 0, _0_6KV = 1, _0_7KV = 2, _0_8KV = 3, _0_9KV = 4, _1_0KV = 5, _1_5KV = 6, _2_0KV = 7, _2_5KV = 8, _3_0KV = 9, _3_5KV = 10, _4_0KV = 11, _4_5KV = 12, _5_0KV = 13,
            _5_5KV = 14, _6_0KV = 15, _6_5KV = 16, _7_0KV = 17, _7_5KV = 18, _8_0KV = 19, _8_5KV = 20, _9_0KV = 21, _9_5KV = 22, _10_0KV = 23

        }

        public enum JSFrequency
        {
            _45To_55HZ = 0, _49To_51HZ = 1, _55To_65HZ = 2, _59To_61HZ = 3, _45HZ = 4, _46HZ = 5, _47HZ = 6, _48HZ = 7, _49HZ = 8, _50HZ = 9, _51HZ = 10, _52HZ = 11,
            _53HZ = 12, _54HZ = 13, _55HZ = 14, _56HZ = 15, _57HZ = 16, _58HZ = 17, _59HZ = 18, _60HZ = 19, _61HZ = 20, _62HZ = 21, _63HZ = 22, _64HZ = 23, _65HZ = 24
        }

        public enum JYDZstation
        {
            高压绕组 = 0, 中压绕组 = 1, 低压绕组 = 2, 高压套管A = 3, 高压套管B = 4, 高压套管C = 5, 高压套管0 = 6,
            中压套管A = 7, 中压套管B = 8, 中压套管C = 9, 中压套管0 = 10, 高中绕组 = 11, 所有绕组 = 12

        }

        public enum JYDZVoilt
        {
            // [Description("0.5KV")]
            _0_25KV = 0, _0_5KV = 1, _0_6KV = 2, _0_7KV = 3, _0_8KV = 4, _0_9KV = 5, _1_0KV = 6, _1_5KV = 7, _2_0KV = 8, _2_5KV = 9, _3_0KV = 10, _3_5KV = 11, _4_0KV = 12, _4_5KV = 13,
            _5_0KV = 14, _5_5KV = 15, _6_0KV = 16, _6_5KV = 17, _7_0KV = 18, _7_5KV = 19, _8_0KV = 20, _8_5KV = 21, _9_0KV = 22, _9_5KV = 23, _10_0KV = 24
        }

        public enum ZldzWindingKind
        {
            Yn型 = 0, Y型 = 1, D型 = 2, Zn型 = 3
        }
        public enum ZldzStation
        {
            高压全部 = 0, 高压AB_A = 1, 高压BC_B = 2, 高压CA_C = 3, 中压全部 = 4, 中压AB_A = 5, 中压BC_B = 6, 中压CA_C = 7, 低压全部 = 8, 低压AB_A = 9, 低压BC_B = 10, 低压CA_C = 11,
            注磁抵押 = 12, 注磁AB_A = 13, 注磁BC_B = 14, 注磁CA_C = 15
        }

        public enum ZldzCurrent
        {
            _1A = 0, _3A = 1, _10A = 2
        }

        public enum YzfjWindingKind
        {
            Yn型 = 0, Y型 = 1, D型 = 2, Zn型 = 3
        }

        public enum YzfjStation
        {
            高压侧 = 0, 中压侧 = 1, 低压侧 = 2
        }

        public enum YzfjCurrent
        {
            _1_A=0,_3_A=1,_10_A
        }
        public enum yzfjTap
        {
            _1To_2 = 0, _2To_1 = 1, _2To_3 = 2, _3To_2 = 3, _3To_4 = 4, _4To_3 = 5, _4To_5 = 6, _5To_4 = 7, _5To_6 = 8, _6To_5 = 9, _6To_7 = 10, _17To_6 = 11,
            _7To_8 = 12, _8To_7 = 13, _8To_9 = 14, _9To_8 = 15, _9To_10 = 16, _10To_9 = 17, _10To_11 = 18, _11To_10 = 19, _11To_12 = 20, _12To_11 = 21, _12To_13 = 22, _13To_12 = 23,
            _13To_14 = 24, _14To_13 = 25, _14To_15 = 26, _15To_14 = 27, _15To_16 = 28, _16To_15 = 29, _16To_17 = 30, _17To_16 = 31, _17To_18 = 32, _18To_17 = 33, _18To_19 = 34,
            _19To_20 = 35
        }

        public enum CommanTest
        {
            模拟按键中断测量 = 0, 仪器复位 = 1, 判断直流电阻稳定状态 = 2
        }


        
    }
}
