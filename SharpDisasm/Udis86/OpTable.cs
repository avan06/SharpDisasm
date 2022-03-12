// --------------------------------------------------------------------------------
// SharpDisasm (File: SharpDisasm\optable.tt)
// Copyright (c) 2014-2015 Justin Stenning
// http://spazzarama.com
// https://github.com/spazzarama/SharpDisasm
// https://sharpdisasm.codeplex.com/
//
// SharpDisasm is distributed under the 2-clause "Simplified BSD License".
//
// Portions of SharpDisasm are ported to C# from udis86 a C disassembler project
// also distributed under the terms of the 2-clause "Simplified BSD License" and
// Copyright (c) 2002-2012, Vivek Thampi <vivek.mt@gmail.com>
// All rights reserved.
// UDIS86: https://github.com/vmt/udis86
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// 
// 1. Redistributions of source code must retain the above copyright notice, 
//    this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice, 
//    this list of conditions and the following disclaimer in the documentation 
//    and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// --------------------------------------------------------------------------------

// Do not edit. File generated from OpTableGeneration/optable.xml on 12-March-2022 11:30 PM
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

#pragma warning disable 1591
namespace SharpDisasm.Udis86
{
    internal static class InstructionTables
    {
        #region Lookup Tables
        public const int INVALID = 0;

        internal static readonly ushort[][] ud_itabs = new ushort[][]
        {
            new ushort[] { //ud_itab__0
              /* 00 */          15,          16,          17,          18,
              /* 04 */          19,          20,    0x8000|1,    0x8000|2,
              /* 08 */         964,         965,         966,         967,
              /* 0c */         968,         969,    0x8000|3,    0x8000|4,
              /* 10 */           5,           6,           7,           8,
              /* 14 */           9,          10,  0x8000|284,  0x8000|285,
              /* 18 */        1337,        1338,        1339,        1340,
              /* 1c */        1341,        1342,  0x8000|286,  0x8000|287,
              /* 20 */          49,          50,          51,          52,
              /* 24 */          53,          54,     INVALID,  0x8000|288,
              /* 28 */        1408,        1409,        1410,        1411,
              /* 2c */        1412,        1413,     INVALID,  0x8000|289,
              /* 30 */        1488,        1489,        1490,        1491,
              /* 34 */        1492,        1493,     INVALID,  0x8000|290,
              /* 38 */         100,         101,         102,         103,
              /* 3c */         104,         105,     INVALID,  0x8000|291,
              /* 40 */         699,         700,         701,         702,
              /* 44 */         703,         704,         705,         706,
              /* 48 */         175,         176,         177,         178,
              /* 4c */         179,         180,         181,         182,
              /* 50 */        1247,        1248,        1249,        1250,
              /* 54 */        1251,        1252,        1253,        1254,
              /* 58 */        1101,        1102,        1103,        1104,
              /* 5c */        1105,        1106,        1107,        1108,
              /* 60 */  0x8000|292,  0x8000|295,  0x8000|298,  0x8000|299,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */        1255,         697,        1257,         698,
              /* 6c */         709,  0x8000|300,         982,  0x8000|301,
              /* 70 */         726,         728,         730,         732,
              /* 74 */         734,         736,         738,         740,
              /* 78 */         742,         744,         746,         748,
              /* 7c */         750,         752,         754,         756,
              /* 80 */  0x8000|302,  0x8000|303,  0x8000|304,  0x8000|313,
              /* 84 */        1434,        1435,        1476,        1477,
              /* 88 */         828,         829,         830,         831,
              /* 8c */         832,         770,         833,  0x8000|314,
              /* 90 */        1478,        1479,        1480,        1481,
              /* 94 */        1482,        1483,        1484,        1485,
              /* 98 */  0x8000|315,  0x8000|316,  0x8000|317,        1471,
              /* 9c */  0x8000|318,  0x8000|322,        1311,         766,
              /* a0 */         834,         835,         836,         837,
              /* a4 */         922,  0x8000|326,         114,  0x8000|327,
              /* a8 */        1436,        1437,        1403,  0x8000|328,
              /* ac */         790,  0x8000|329,        1347,  0x8000|330,
              /* b0 */         838,         839,         840,         841,
              /* b4 */         842,         843,         844,         845,
              /* b8 */         846,         847,         848,         849,
              /* bc */         850,         851,         852,         853,
              /* c0 */  0x8000|331,  0x8000|332,        1302,        1303,
              /* c4 */  0x8000|333,  0x8000|403,  0x8000|405,  0x8000|406,
              /* c8 */         200,         776,        1304,        1305,
              /* cc */         713,         714,  0x8000|407,  0x8000|408,
              /* d0 */  0x8000|409,  0x8000|410,  0x8000|411,  0x8000|412,
              /* d4 */  0x8000|413,  0x8000|414,  0x8000|415,        1487,
              /* d8 */  0x8000|416,  0x8000|419,  0x8000|422,  0x8000|425,
              /* dc */  0x8000|428,  0x8000|431,  0x8000|434,  0x8000|437,
              /* e0 */         794,         795,         796,  0x8000|440,
              /* e4 */         690,         691,         978,         979,
              /* e8 */          72,         763,  0x8000|441,         765,
              /* ec */         692,         693,         980,         981,
              /* f0 */         789,         712,        1300,        1301,
              /* f4 */         687,          83,  0x8000|442,  0x8000|443,
              /* f8 */          77,        1396,          81,        1399,
              /* fc */          78,        1397,  0x8000|444,  0x8000|445,
            },

            new ushort[] { //ud_itab__1
              /* 00 */        1241,     INVALID,
            },

            new ushort[] { //ud_itab__2
              /* 00 */        1096,     INVALID,
            },

            new ushort[] { //ud_itab__3
              /* 00 */        1242,     INVALID,
            },

            new ushort[] { //ud_itab__4
              /* 00 */    0x8000|5,    0x8000|6,         767,         797,
              /* 04 */     INVALID,        1427,          82,        1432,
              /* 08 */         716,        1472,     INVALID,        1445,
              /* 0c */     INVALID,   0x8000|27,         430,   0x8000|28,
              /* 10 */   0x8000|29,   0x8000|30,   0x8000|31,   0x8000|34,
              /* 14 */   0x8000|35,   0x8000|36,   0x8000|37,   0x8000|40,
              /* 18 */   0x8000|41,         955,         956,         957,
              /* 1c */         958,         959,         960,         961,
              /* 20 */         854,         855,         856,         857,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */   0x8000|42,   0x8000|43,   0x8000|44,   0x8000|45,
              /* 2c */   0x8000|46,   0x8000|47,   0x8000|48,   0x8000|49,
              /* 30 */        1473,        1298,        1296,        1297,
              /* 34 */   0x8000|50,   0x8000|52,     INVALID,        1515,
              /* 38 */   0x8000|54,     INVALID,  0x8000|116,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 40 */          84,          85,          86,          87,
              /* 44 */          88,          89,          90,          91,
              /* 48 */          92,          93,          94,          95,
              /* 4c */          96,          97,          98,          99,
              /* 50 */  0x8000|143,  0x8000|144,  0x8000|145,  0x8000|146,
              /* 54 */  0x8000|147,  0x8000|148,  0x8000|149,  0x8000|150,
              /* 58 */  0x8000|151,  0x8000|152,  0x8000|153,  0x8000|154,
              /* 5c */  0x8000|155,  0x8000|156,  0x8000|157,  0x8000|158,
              /* 60 */  0x8000|159,  0x8000|160,  0x8000|161,  0x8000|162,
              /* 64 */  0x8000|163,  0x8000|164,  0x8000|165,  0x8000|166,
              /* 68 */  0x8000|167,  0x8000|168,  0x8000|169,  0x8000|170,
              /* 6c */  0x8000|171,  0x8000|172,  0x8000|173,  0x8000|176,
              /* 70 */  0x8000|177,  0x8000|178,  0x8000|182,  0x8000|186,
              /* 74 */  0x8000|191,  0x8000|192,  0x8000|193,         199,
              /* 78 */  0x8000|194,  0x8000|195,     INVALID,     INVALID,
              /* 7c */  0x8000|196,  0x8000|197,  0x8000|198,  0x8000|201,
              /* 80 */         727,         729,         731,         733,
              /* 84 */         735,         737,         739,         741,
              /* 88 */         743,         745,         747,         749,
              /* 8c */         751,         753,         755,         757,
              /* 90 */        1351,        1352,        1353,        1354,
              /* 94 */        1355,        1356,        1357,        1358,
              /* 98 */        1359,        1360,        1361,        1362,
              /* 9c */        1363,        1364,        1365,        1366,
              /* a0 */        1246,        1100,         131,        1671,
              /* a4 */        1376,        1377,  0x8000|202,  0x8000|207,
              /* a8 */        1245,        1099,        1306,        1676,
              /* ac */        1378,        1379,  0x8000|215,         694,
              /* b0 */         122,         123,         775,        1674,
              /* b4 */         772,         773,         940,         941,
              /* b8 */  0x8000|221,     INVALID,  0x8000|222,        1672,
              /* bc */        1660,        1661,         930,         931,
              /* c0 */        1474,        1475,  0x8000|223,         904,
              /* c4 */  0x8000|224,  0x8000|225,  0x8000|226,  0x8000|227,
              /* c8 */        1662,        1663,        1664,        1665,
              /* cc */        1666,        1667,        1668,        1669,
              /* d0 */  0x8000|236,  0x8000|237,  0x8000|238,  0x8000|239,
              /* d4 */  0x8000|240,  0x8000|241,  0x8000|242,  0x8000|243,
              /* d8 */  0x8000|244,  0x8000|245,  0x8000|246,  0x8000|247,
              /* dc */  0x8000|248,  0x8000|249,  0x8000|250,  0x8000|251,
              /* e0 */  0x8000|252,  0x8000|253,  0x8000|254,  0x8000|255,
              /* e4 */  0x8000|256,  0x8000|257,  0x8000|258,  0x8000|259,
              /* e8 */  0x8000|260,  0x8000|261,  0x8000|262,  0x8000|263,
              /* ec */  0x8000|264,  0x8000|265,  0x8000|266,  0x8000|267,
              /* f0 */  0x8000|268,  0x8000|269,  0x8000|270,  0x8000|271,
              /* f4 */  0x8000|272,  0x8000|273,  0x8000|274,  0x8000|275,
              /* f8 */  0x8000|277,  0x8000|278,  0x8000|279,  0x8000|280,
              /* fc */  0x8000|281,  0x8000|282,  0x8000|283,     INVALID,
            },

            new ushort[] { //ud_itab__5
              /* 00 */        1385,        1407,         786,         798,
              /* 04 */        1454,        1455,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__6
              /* 00 */    0x8000|7,    0x8000|8,
            },

            new ushort[] { //ud_itab__7
              /* 00 */        1375,        1384,         785,         774,
              /* 04 */        1386,     INVALID,         787,         719,
            },

            new ushort[] { //ud_itab__8
              /* 00 */    0x8000|9,   0x8000|14,   0x8000|15,   0x8000|16,
              /* 04 */        1387,     INVALID,         788,   0x8000|25,
            },

            new ushort[] { //ud_itab__9
              /* 00 */     INVALID,   0x8000|10,   0x8000|11,   0x8000|12,
              /* 04 */   0x8000|13,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__10
              /* 00 */     INVALID,        1456,     INVALID,
            },

            new ushort[] { //ud_itab__11
              /* 00 */     INVALID,        1462,     INVALID,
            },

            new ushort[] { //ud_itab__12
              /* 00 */     INVALID,        1463,     INVALID,
            },

            new ushort[] { //ud_itab__13
              /* 00 */     INVALID,        1464,     INVALID,
            },

            new ushort[] { //ud_itab__14
              /* 00 */         824,         952,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__15
              /* 00 */        1486,        1509,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__16
              /* 00 */   0x8000|17,   0x8000|18,   0x8000|19,   0x8000|20,
              /* 04 */   0x8000|21,   0x8000|22,   0x8000|23,   0x8000|24,
            },

            new ushort[] { //ud_itab__17
              /* 00 */        1467,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__18
              /* 00 */        1468,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__19
              /* 00 */        1469,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__20
              /* 00 */        1470,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__21
              /* 00 */        1398,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__22
              /* 00 */          80,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__23
              /* 00 */        1400,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__24
              /* 00 */         720,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__25
              /* 00 */        1426,   0x8000|26,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__26
              /* 00 */        1299,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__27
              /* 00 */        1120,        1121,        1122,        1123,
              /* 04 */        1124,        1125,        1126,        1127,
            },

            new ushort[] { //ud_itab__28
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 0c */        1217,        1218,     INVALID,     INVALID,
              /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 1c */        1219,        1220,     INVALID,     INVALID,
              /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,        1221,     INVALID,
              /* 8c */     INVALID,     INVALID,        1222,     INVALID,
              /* 90 */        1223,     INVALID,     INVALID,     INVALID,
              /* 94 */        1224,     INVALID,        1225,        1226,
              /* 98 */     INVALID,     INVALID,        1227,     INVALID,
              /* 9c */     INVALID,     INVALID,        1228,     INVALID,
              /* a0 */        1229,     INVALID,     INVALID,     INVALID,
              /* a4 */        1230,     INVALID,        1231,        1232,
              /* a8 */     INVALID,     INVALID,        1233,     INVALID,
              /* ac */     INVALID,     INVALID,        1234,     INVALID,
              /* b0 */        1235,     INVALID,     INVALID,     INVALID,
              /* b4 */        1236,     INVALID,        1237,        1238,
              /* b8 */     INVALID,     INVALID,     INVALID,        1239,
              /* bc */     INVALID,     INVALID,     INVALID,        1240,
              /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__29
              /* 00 */         936,         925,         928,         932,
            },

            new ushort[] { //ud_itab__30
              /* 00 */         938,         926,         929,         934,
            },

            new ushort[] { //ud_itab__31
              /* 00 */   0x8000|32,   0x8000|33,
            },

            new ushort[] { //ud_itab__32
              /* 00 */         892,        1564,        1572,         888,
            },

            new ushort[] { //ud_itab__33
              /* 00 */         896,        1562,        1570,     INVALID,
            },

            new ushort[] { //ud_itab__34
              /* 00 */         894,     INVALID,     INVALID,         890,
            },

            new ushort[] { //ud_itab__35
              /* 00 */        1450,     INVALID,     INVALID,        1452,
            },

            new ushort[] { //ud_itab__36
              /* 00 */        1448,     INVALID,     INVALID,        1446,
            },

            new ushort[] { //ud_itab__37
              /* 00 */   0x8000|38,   0x8000|39,
            },

            new ushort[] { //ud_itab__38
              /* 00 */         882,     INVALID,        1568,         878,
            },

            new ushort[] { //ud_itab__39
              /* 00 */         886,     INVALID,        1566,     INVALID,
            },

            new ushort[] { //ud_itab__40
              /* 00 */         884,     INVALID,     INVALID,         880,
            },

            new ushort[] { //ud_itab__41
              /* 00 */        1128,        1129,        1130,        1131,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__42
              /* 00 */         862,     INVALID,     INVALID,         858,
            },

            new ushort[] { //ud_itab__43
              /* 00 */         864,     INVALID,     INVALID,         860,
            },

            new ushort[] { //ud_itab__44
              /* 00 */         141,         152,         154,         142,
            },

            new ushort[] { //ud_itab__45
              /* 00 */         907,     INVALID,     INVALID,         905,
            },

            new ushort[] { //ud_itab__46
              /* 00 */         165,         166,         168,         162,
            },

            new ushort[] { //ud_itab__47
              /* 00 */         147,         148,         158,         138,
            },

            new ushort[] { //ud_itab__48
              /* 00 */        1443,     INVALID,     INVALID,        1441,
            },

            new ushort[] { //ud_itab__49
              /* 00 */         129,     INVALID,     INVALID,         127,
            },

            new ushort[] { //ud_itab__50
              /* 00 */        1428,   0x8000|51,
            },

            new ushort[] { //ud_itab__51
              /* 00 */     INVALID,        1429,     INVALID,
            },

            new ushort[] { //ud_itab__52
              /* 00 */        1430,   0x8000|53,
            },

            new ushort[] { //ud_itab__53
              /* 00 */     INVALID,        1431,     INVALID,
            },

            new ushort[] { //ud_itab__54
              /* 00 */   0x8000|55,   0x8000|56,   0x8000|57,   0x8000|58,
              /* 04 */   0x8000|59,   0x8000|60,   0x8000|61,   0x8000|62,
              /* 08 */   0x8000|63,   0x8000|64,   0x8000|65,   0x8000|66,
              /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 10 */   0x8000|67,     INVALID,     INVALID,     INVALID,
              /* 14 */   0x8000|68,   0x8000|69,     INVALID,   0x8000|70,
              /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 1c */   0x8000|71,   0x8000|72,   0x8000|73,     INVALID,
              /* 20 */   0x8000|74,   0x8000|75,   0x8000|76,   0x8000|77,
              /* 24 */   0x8000|78,   0x8000|79,     INVALID,     INVALID,
              /* 28 */   0x8000|80,   0x8000|81,   0x8000|82,   0x8000|83,
              /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 30 */   0x8000|84,   0x8000|85,   0x8000|86,   0x8000|87,
              /* 34 */   0x8000|88,   0x8000|89,     INVALID,   0x8000|90,
              /* 38 */   0x8000|91,   0x8000|92,   0x8000|93,   0x8000|94,
              /* 3c */   0x8000|95,   0x8000|96,   0x8000|97,   0x8000|98,
              /* 40 */   0x8000|99,  0x8000|100,     INVALID,     INVALID,
              /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 80 */  0x8000|101,  0x8000|105,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d8 */     INVALID,     INVALID,     INVALID,  0x8000|109,
              /* dc */  0x8000|110,  0x8000|111,  0x8000|112,  0x8000|113,
              /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f0 */  0x8000|114,  0x8000|115,     INVALID,     INVALID,
              /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__55
              /* 00 */        1583,     INVALID,     INVALID,        1584,
            },

            new ushort[] { //ud_itab__56
              /* 00 */        1586,     INVALID,     INVALID,        1587,
            },

            new ushort[] { //ud_itab__57
              /* 00 */        1589,     INVALID,     INVALID,        1590,
            },

            new ushort[] { //ud_itab__58
              /* 00 */        1592,     INVALID,     INVALID,        1593,
            },

            new ushort[] { //ud_itab__59
              /* 00 */        1595,     INVALID,     INVALID,        1596,
            },

            new ushort[] { //ud_itab__60
              /* 00 */        1598,     INVALID,     INVALID,        1599,
            },

            new ushort[] { //ud_itab__61
              /* 00 */        1601,     INVALID,     INVALID,        1602,
            },

            new ushort[] { //ud_itab__62
              /* 00 */        1604,     INVALID,     INVALID,        1605,
            },

            new ushort[] { //ud_itab__63
              /* 00 */        1607,     INVALID,     INVALID,        1608,
            },

            new ushort[] { //ud_itab__64
              /* 00 */        1613,     INVALID,     INVALID,        1614,
            },

            new ushort[] { //ud_itab__65
              /* 00 */        1610,     INVALID,     INVALID,        1611,
            },

            new ushort[] { //ud_itab__66
              /* 00 */        1616,     INVALID,     INVALID,        1617,
            },

            new ushort[] { //ud_itab__67
              /* 00 */     INVALID,     INVALID,     INVALID,        1622,
            },

            new ushort[] { //ud_itab__68
              /* 00 */     INVALID,     INVALID,     INVALID,        1658,
            },

            new ushort[] { //ud_itab__69
              /* 00 */     INVALID,     INVALID,     INVALID,        1657,
            },

            new ushort[] { //ud_itab__70
              /* 00 */     INVALID,     INVALID,     INVALID,        1712,
            },

            new ushort[] { //ud_itab__71
              /* 00 */        1574,     INVALID,     INVALID,        1575,
            },

            new ushort[] { //ud_itab__72
              /* 00 */        1577,     INVALID,     INVALID,        1578,
            },

            new ushort[] { //ud_itab__73
              /* 00 */        1580,     INVALID,     INVALID,        1581,
            },

            new ushort[] { //ud_itab__74
              /* 00 */     INVALID,     INVALID,     INVALID,        1686,
            },

            new ushort[] { //ud_itab__75
              /* 00 */     INVALID,     INVALID,     INVALID,        1688,
            },

            new ushort[] { //ud_itab__76
              /* 00 */     INVALID,     INVALID,     INVALID,        1690,
            },

            new ushort[] { //ud_itab__77
              /* 00 */     INVALID,     INVALID,     INVALID,        1692,
            },

            new ushort[] { //ud_itab__78
              /* 00 */     INVALID,     INVALID,     INVALID,        1694,
            },

            new ushort[] { //ud_itab__79
              /* 00 */     INVALID,     INVALID,     INVALID,        1696,
            },

            new ushort[] { //ud_itab__80
              /* 00 */     INVALID,     INVALID,     INVALID,        1623,
            },

            new ushort[] { //ud_itab__81
              /* 00 */     INVALID,     INVALID,     INVALID,        1709,
            },

            new ushort[] { //ud_itab__82
              /* 00 */     INVALID,     INVALID,     INVALID,        1682,
            },

            new ushort[] { //ud_itab__83
              /* 00 */     INVALID,     INVALID,     INVALID,        1684,
            },

            new ushort[] { //ud_itab__84
              /* 00 */     INVALID,     INVALID,     INVALID,        1697,
            },

            new ushort[] { //ud_itab__85
              /* 00 */     INVALID,     INVALID,     INVALID,        1699,
            },

            new ushort[] { //ud_itab__86
              /* 00 */     INVALID,     INVALID,     INVALID,        1701,
            },

            new ushort[] { //ud_itab__87
              /* 00 */     INVALID,     INVALID,     INVALID,        1703,
            },

            new ushort[] { //ud_itab__88
              /* 00 */     INVALID,     INVALID,     INVALID,        1705,
            },

            new ushort[] { //ud_itab__89
              /* 00 */     INVALID,     INVALID,     INVALID,        1707,
            },

            new ushort[] { //ud_itab__90
              /* 00 */     INVALID,     INVALID,     INVALID,        1718,
            },

            new ushort[] { //ud_itab__91
              /* 00 */     INVALID,     INVALID,     INVALID,        1625,
            },

            new ushort[] { //ud_itab__92
              /* 00 */     INVALID,     INVALID,     INVALID,        1627,
            },

            new ushort[] { //ud_itab__93
              /* 00 */     INVALID,     INVALID,     INVALID,        1629,
            },

            new ushort[] { //ud_itab__94
              /* 00 */     INVALID,     INVALID,     INVALID,        1631,
            },

            new ushort[] { //ud_itab__95
              /* 00 */     INVALID,     INVALID,     INVALID,        1633,
            },

            new ushort[] { //ud_itab__96
              /* 00 */     INVALID,     INVALID,     INVALID,        1635,
            },

            new ushort[] { //ud_itab__97
              /* 00 */     INVALID,     INVALID,     INVALID,        1639,
            },

            new ushort[] { //ud_itab__98
              /* 00 */     INVALID,     INVALID,     INVALID,        1637,
            },

            new ushort[] { //ud_itab__99
              /* 00 */     INVALID,     INVALID,     INVALID,        1641,
            },

            new ushort[] { //ud_itab__100
              /* 00 */     INVALID,     INVALID,     INVALID,        1643,
            },

            new ushort[] { //ud_itab__101
              /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|102,
            },

            new ushort[] { //ud_itab__102
              /* 00 */  0x8000|103,  0x8000|104,
            },

            new ushort[] { //ud_itab__103
              /* 00 */     INVALID,         717,     INVALID,
            },

            new ushort[] { //ud_itab__104
              /* 00 */     INVALID,         718,     INVALID,
            },

            new ushort[] { //ud_itab__105
              /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|106,
            },

            new ushort[] { //ud_itab__106
              /* 00 */  0x8000|107,  0x8000|108,
            },

            new ushort[] { //ud_itab__107
              /* 00 */     INVALID,         721,     INVALID,
            },

            new ushort[] { //ud_itab__108
              /* 00 */     INVALID,         722,     INVALID,
            },

            new ushort[] { //ud_itab__109
              /* 00 */     INVALID,     INVALID,     INVALID,          45,
            },

            new ushort[] { //ud_itab__110
              /* 00 */     INVALID,     INVALID,     INVALID,          41,
            },

            new ushort[] { //ud_itab__111
              /* 00 */     INVALID,     INVALID,     INVALID,          43,
            },

            new ushort[] { //ud_itab__112
              /* 00 */     INVALID,     INVALID,     INVALID,          37,
            },

            new ushort[] { //ud_itab__113
              /* 00 */     INVALID,     INVALID,     INVALID,          39,
            },

            new ushort[] { //ud_itab__114
              /* 00 */        1724,        1726,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__115
              /* 00 */        1725,        1727,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__116
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 08 */  0x8000|117,  0x8000|118,  0x8000|119,  0x8000|120,
              /* 0c */  0x8000|121,  0x8000|122,  0x8000|123,  0x8000|124,
              /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 14 */  0x8000|125,  0x8000|126,  0x8000|127,  0x8000|129,
              /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 20 */  0x8000|130,  0x8000|131,  0x8000|132,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 40 */  0x8000|134,  0x8000|135,  0x8000|136,     INVALID,
              /* 44 */  0x8000|137,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 60 */  0x8000|138,  0x8000|139,  0x8000|140,  0x8000|141,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* dc */     INVALID,     INVALID,     INVALID,  0x8000|142,
              /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__117
              /* 00 */     INVALID,     INVALID,     INVALID,        1645,
            },

            new ushort[] { //ud_itab__118
              /* 00 */     INVALID,     INVALID,     INVALID,        1647,
            },

            new ushort[] { //ud_itab__119
              /* 00 */     INVALID,     INVALID,     INVALID,        1649,
            },

            new ushort[] { //ud_itab__120
              /* 00 */     INVALID,     INVALID,     INVALID,        1651,
            },

            new ushort[] { //ud_itab__121
              /* 00 */     INVALID,     INVALID,     INVALID,        1655,
            },

            new ushort[] { //ud_itab__122
              /* 00 */     INVALID,     INVALID,     INVALID,        1653,
            },

            new ushort[] { //ud_itab__123
              /* 00 */     INVALID,     INVALID,     INVALID,        1678,
            },

            new ushort[] { //ud_itab__124
              /* 00 */        1619,     INVALID,     INVALID,        1620,
            },

            new ushort[] { //ud_itab__125
              /* 00 */     INVALID,     INVALID,     INVALID,        1045,
            },

            new ushort[] { //ud_itab__126
              /* 00 */     INVALID,     INVALID,     INVALID,        1056,
            },

            new ushort[] { //ud_itab__127
              /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|128,
            },

            new ushort[] { //ud_itab__128
              /* 00 */        1047,        1049,        1051,
            },

            new ushort[] { //ud_itab__129
              /* 00 */     INVALID,     INVALID,     INVALID,         201,
            },

            new ushort[] { //ud_itab__130
              /* 00 */     INVALID,     INVALID,     INVALID,        1058,
            },

            new ushort[] { //ud_itab__131
              /* 00 */     INVALID,     INVALID,     INVALID,        1558,
            },

            new ushort[] { //ud_itab__132
              /* 00 */     INVALID,     INVALID,     INVALID,  0x8000|133,
            },

            new ushort[] { //ud_itab__133
              /* 00 */        1062,        1063,        1064,
            },

            new ushort[] { //ud_itab__134
              /* 00 */     INVALID,     INVALID,     INVALID,         197,
            },

            new ushort[] { //ud_itab__135
              /* 00 */     INVALID,     INVALID,     INVALID,         195,
            },

            new ushort[] { //ud_itab__136
              /* 00 */     INVALID,     INVALID,     INVALID,        1680,
            },

            new ushort[] { //ud_itab__137
              /* 00 */     INVALID,     INVALID,     INVALID,        1513,
            },

            new ushort[] { //ud_itab__138
              /* 00 */     INVALID,     INVALID,     INVALID,        1716,
            },

            new ushort[] { //ud_itab__139
              /* 00 */     INVALID,     INVALID,     INVALID,        1714,
            },

            new ushort[] { //ud_itab__140
              /* 00 */     INVALID,     INVALID,     INVALID,        1722,
            },

            new ushort[] { //ud_itab__141
              /* 00 */     INVALID,     INVALID,     INVALID,        1720,
            },

            new ushort[] { //ud_itab__142
              /* 00 */     INVALID,     INVALID,     INVALID,          47,
            },

            new ushort[] { //ud_itab__143
              /* 00 */         900,     INVALID,     INVALID,         898,
            },

            new ushort[] { //ud_itab__144
              /* 00 */        1388,        1392,        1394,        1390,
            },

            new ushort[] { //ud_itab__145
              /* 00 */        1307,     INVALID,        1309,     INVALID,
            },

            new ushort[] { //ud_itab__146
              /* 00 */        1292,     INVALID,        1294,     INVALID,
            },

            new ushort[] { //ud_itab__147
              /* 00 */          61,     INVALID,     INVALID,          59,
            },

            new ushort[] { //ud_itab__148
              /* 00 */          65,     INVALID,     INVALID,          63,
            },

            new ushort[] { //ud_itab__149
              /* 00 */         976,     INVALID,     INVALID,         974,
            },

            new ushort[] { //ud_itab__150
              /* 00 */        1500,     INVALID,     INVALID,        1498,
            },

            new ushort[] { //ud_itab__151
              /* 00 */          27,          29,          31,          25,
            },

            new ushort[] { //ud_itab__152
              /* 00 */         946,         948,         950,         944,
            },

            new ushort[] { //ud_itab__153
              /* 00 */         145,         150,         156,         139,
            },

            new ushort[] { //ud_itab__154
              /* 00 */         134,     INVALID,         163,         143,
            },

            new ushort[] { //ud_itab__155
              /* 00 */        1420,        1422,        1424,        1418,
            },

            new ushort[] { //ud_itab__156
              /* 00 */         818,         820,         822,         816,
            },

            new ushort[] { //ud_itab__157
              /* 00 */         189,         191,         193,         187,
            },

            new ushort[] { //ud_itab__158
              /* 00 */         802,         804,         806,         800,
            },

            new ushort[] { //ud_itab__159
              /* 00 */        1210,     INVALID,     INVALID,        1208,
            },

            new ushort[] { //ud_itab__160
              /* 00 */        1213,     INVALID,     INVALID,        1211,
            },

            new ushort[] { //ud_itab__161
              /* 00 */        1216,     INVALID,     INVALID,        1214,
            },

            new ushort[] { //ud_itab__162
              /* 00 */         987,     INVALID,     INVALID,         985,
            },

            new ushort[] { //ud_itab__163
              /* 00 */        1038,     INVALID,     INVALID,        1036,
            },

            new ushort[] { //ud_itab__164
              /* 00 */        1041,     INVALID,     INVALID,        1039,
            },

            new ushort[] { //ud_itab__165
              /* 00 */        1044,     INVALID,     INVALID,        1042,
            },

            new ushort[] { //ud_itab__166
              /* 00 */         993,     INVALID,     INVALID,         991,
            },

            new ushort[] { //ud_itab__167
              /* 00 */        1201,     INVALID,     INVALID,        1199,
            },

            new ushort[] { //ud_itab__168
              /* 00 */        1204,     INVALID,     INVALID,        1202,
            },

            new ushort[] { //ud_itab__169
              /* 00 */        1207,     INVALID,     INVALID,        1205,
            },

            new ushort[] { //ud_itab__170
              /* 00 */         990,     INVALID,     INVALID,         988,
            },

            new ushort[] { //ud_itab__171
              /* 00 */     INVALID,     INVALID,     INVALID,        1548,
            },

            new ushort[] { //ud_itab__172
              /* 00 */     INVALID,     INVALID,     INVALID,        1546,
            },

            new ushort[] { //ud_itab__173
              /* 00 */  0x8000|174,     INVALID,     INVALID,  0x8000|175,
            },

            new ushort[] { //ud_itab__174
              /* 00 */         866,         867,         910,
            },

            new ushort[] { //ud_itab__175
              /* 00 */         868,         870,         911,
            },

            new ushort[] { //ud_itab__176
              /* 00 */         920,     INVALID,        1523,        1518,
            },

            new ushort[] { //ud_itab__177
              /* 00 */        1135,        1538,        1536,        1540,
            },

            new ushort[] { //ud_itab__178
              /* 00 */     INVALID,     INVALID,  0x8000|179,     INVALID,
              /* 04 */  0x8000|180,     INVALID,  0x8000|181,     INVALID,
            },

            new ushort[] { //ud_itab__179
              /* 00 */        1160,     INVALID,     INVALID,        1164,
            },

            new ushort[] { //ud_itab__180
              /* 00 */        1153,     INVALID,     INVALID,        1151,
            },

            new ushort[] { //ud_itab__181
              /* 00 */        1139,     INVALID,     INVALID,        1138,
            },

            new ushort[] { //ud_itab__182
              /* 00 */     INVALID,     INVALID,  0x8000|183,     INVALID,
              /* 04 */  0x8000|184,     INVALID,  0x8000|185,     INVALID,
            },

            new ushort[] { //ud_itab__183
              /* 00 */        1166,     INVALID,     INVALID,        1170,
            },

            new ushort[] { //ud_itab__184
              /* 00 */        1154,     INVALID,     INVALID,        1158,
            },

            new ushort[] { //ud_itab__185
              /* 00 */        1143,     INVALID,     INVALID,        1142,
            },

            new ushort[] { //ud_itab__186
              /* 00 */     INVALID,     INVALID,  0x8000|187,  0x8000|188,
              /* 04 */     INVALID,     INVALID,  0x8000|189,  0x8000|190,
            },

            new ushort[] { //ud_itab__187
              /* 00 */        1172,     INVALID,     INVALID,        1176,
            },

            new ushort[] { //ud_itab__188
              /* 00 */     INVALID,     INVALID,     INVALID,        1544,
            },

            new ushort[] { //ud_itab__189
              /* 00 */        1147,     INVALID,     INVALID,        1146,
            },

            new ushort[] { //ud_itab__190
              /* 00 */     INVALID,     INVALID,     INVALID,        1542,
            },

            new ushort[] { //ud_itab__191
              /* 00 */        1027,     INVALID,     INVALID,        1028,
            },

            new ushort[] { //ud_itab__192
              /* 00 */        1030,     INVALID,     INVALID,        1031,
            },

            new ushort[] { //ud_itab__193
              /* 00 */        1033,     INVALID,     INVALID,        1034,
            },

            new ushort[] { //ud_itab__194
              /* 00 */     INVALID,        1465,     INVALID,
            },

            new ushort[] { //ud_itab__195
              /* 00 */     INVALID,        1466,     INVALID,
            },

            new ushort[] { //ud_itab__196
              /* 00 */     INVALID,        1552,     INVALID,        1550,
            },

            new ushort[] { //ud_itab__197
              /* 00 */     INVALID,        1556,     INVALID,        1554,
            },

            new ushort[] { //ud_itab__198
              /* 00 */  0x8000|199,     INVALID,         916,  0x8000|200,
            },

            new ushort[] { //ud_itab__199
              /* 00 */         872,         873,         913,
            },

            new ushort[] { //ud_itab__200
              /* 00 */         874,         876,         914,
            },

            new ushort[] { //ud_itab__201
              /* 00 */         921,     INVALID,        1525,        1516,
            },

            new ushort[] { //ud_itab__202
              /* 00 */     INVALID,  0x8000|203,
            },

            new ushort[] { //ud_itab__203
              /* 00 */  0x8000|204,  0x8000|205,  0x8000|206,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__204
              /* 00 */         825,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__205
              /* 00 */        1510,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__206
              /* 00 */        1511,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__207
              /* 00 */     INVALID,  0x8000|208,
            },

            new ushort[] { //ud_itab__208
              /* 00 */  0x8000|209,  0x8000|210,  0x8000|211,  0x8000|212,
              /* 04 */  0x8000|213,  0x8000|214,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__209
              /* 00 */        1512,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__210
              /* 00 */        1502,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__211
              /* 00 */        1503,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__212
              /* 00 */        1504,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__213
              /* 00 */        1505,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__214
              /* 00 */        1506,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__215
              /* 00 */  0x8000|216,  0x8000|217,
            },

            new ushort[] { //ud_itab__216
              /* 00 */         683,         682,         768,        1401,
              /* 04 */        1508,        1507,     INVALID,          79,
            },

            new ushort[] { //ud_itab__217
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,  0x8000|218,  0x8000|219,  0x8000|220,
            },

            new ushort[] { //ud_itab__218
              /* 00 */         777,         778,         779,         780,
              /* 04 */         781,         782,         783,         784,
            },

            new ushort[] { //ud_itab__219
              /* 00 */         808,         809,         810,         811,
              /* 04 */         812,         813,         814,         815,
            },

            new ushort[] { //ud_itab__220
              /* 00 */        1367,        1368,        1369,        1370,
              /* 04 */        1371,        1372,        1373,        1374,
            },

            new ushort[] { //ud_itab__221
              /* 00 */     INVALID,     INVALID,        1711,     INVALID,
            },

            new ushort[] { //ud_itab__222
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */        1670,        1677,        1675,        1673,
            },

            new ushort[] { //ud_itab__223
              /* 00 */         112,         117,         120,         110,
            },

            new ushort[] { //ud_itab__224
              /* 00 */        1059,     INVALID,     INVALID,        1060,
            },

            new ushort[] { //ud_itab__225
              /* 00 */        1055,     INVALID,     INVALID,        1053,
            },

            new ushort[] { //ud_itab__226
              /* 00 */        1382,     INVALID,     INVALID,        1380,
            },

            new ushort[] { //ud_itab__227
              /* 00 */  0x8000|228,  0x8000|235,
            },

            new ushort[] { //ud_itab__228
              /* 00 */     INVALID,  0x8000|229,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,  0x8000|230,  0x8000|234,
            },

            new ushort[] { //ud_itab__229
              /* 00 */         124,         125,         126,
            },

            new ushort[] { //ud_itab__230
              /* 00 */  0x8000|231,     INVALID,  0x8000|232,  0x8000|233,
            },

            new ushort[] { //ud_itab__231
              /* 00 */     INVALID,        1460,     INVALID,
            },

            new ushort[] { //ud_itab__232
              /* 00 */     INVALID,        1459,     INVALID,
            },

            new ushort[] { //ud_itab__233
              /* 00 */     INVALID,        1458,     INVALID,
            },

            new ushort[] { //ud_itab__234
              /* 00 */     INVALID,        1461,     INVALID,
            },

            new ushort[] { //ud_itab__235
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,        1457,     INVALID,
            },

            new ushort[] { //ud_itab__236
              /* 00 */     INVALID,          35,     INVALID,          33,
            },

            new ushort[] { //ud_itab__237
              /* 00 */        1161,     INVALID,     INVALID,        1162,
            },

            new ushort[] { //ud_itab__238
              /* 00 */        1167,     INVALID,     INVALID,        1168,
            },

            new ushort[] { //ud_itab__239
              /* 00 */        1173,     INVALID,     INVALID,        1174,
            },

            new ushort[] { //ud_itab__240
              /* 00 */        1528,     INVALID,     INVALID,        1529,
            },

            new ushort[] { //ud_itab__241
              /* 00 */        1093,     INVALID,     INVALID,        1094,
            },

            new ushort[] { //ud_itab__242
              /* 00 */     INVALID,        1522,        1527,         918,
            },

            new ushort[] { //ud_itab__243
              /* 00 */        1086,     INVALID,     INVALID,        1084,
            },

            new ushort[] { //ud_itab__244
              /* 00 */        1193,     INVALID,     INVALID,        1194,
            },

            new ushort[] { //ud_itab__245
              /* 00 */        1196,     INVALID,     INVALID,        1197,
            },

            new ushort[] { //ud_itab__246
              /* 00 */        1083,     INVALID,     INVALID,        1081,
            },

            new ushort[] { //ud_itab__247
              /* 00 */        1017,     INVALID,     INVALID,        1015,
            },

            new ushort[] { //ud_itab__248
              /* 00 */        1009,     INVALID,     INVALID,        1010,
            },

            new ushort[] { //ud_itab__249
              /* 00 */        1012,     INVALID,     INVALID,        1013,
            },

            new ushort[] { //ud_itab__250
              /* 00 */        1075,     INVALID,     INVALID,        1076,
            },

            new ushort[] { //ud_itab__251
              /* 00 */        1020,     INVALID,     INVALID,        1018,
            },

            new ushort[] { //ud_itab__252
              /* 00 */        1023,     INVALID,     INVALID,        1021,
            },

            new ushort[] { //ud_itab__253
              /* 00 */        1148,     INVALID,     INVALID,        1149,
            },

            new ushort[] { //ud_itab__254
              /* 00 */        1157,     INVALID,     INVALID,        1155,
            },

            new ushort[] { //ud_itab__255
              /* 00 */        1026,     INVALID,     INVALID,        1024,
            },

            new ushort[] { //ud_itab__256
              /* 00 */        1087,     INVALID,     INVALID,        1088,
            },

            new ushort[] { //ud_itab__257
              /* 00 */        1092,     INVALID,     INVALID,        1090,
            },

            new ushort[] { //ud_itab__258
              /* 00 */     INVALID,         136,         132,         160,
            },

            new ushort[] { //ud_itab__259
              /* 00 */         909,     INVALID,     INVALID,         902,
            },

            new ushort[] { //ud_itab__260
              /* 00 */        1187,     INVALID,     INVALID,        1188,
            },

            new ushort[] { //ud_itab__261
              /* 00 */        1190,     INVALID,     INVALID,        1191,
            },

            new ushort[] { //ud_itab__262
              /* 00 */        1080,     INVALID,     INVALID,        1078,
            },

            new ushort[] { //ud_itab__263
              /* 00 */        1119,     INVALID,     INVALID,        1117,
            },

            new ushort[] { //ud_itab__264
              /* 00 */        1003,     INVALID,     INVALID,        1004,
            },

            new ushort[] { //ud_itab__265
              /* 00 */        1006,     INVALID,     INVALID,        1007,
            },

            new ushort[] { //ud_itab__266
              /* 00 */        1074,     INVALID,     INVALID,        1072,
            },

            new ushort[] { //ud_itab__267
              /* 00 */        1267,     INVALID,     INVALID,        1265,
            },

            new ushort[] { //ud_itab__268
              /* 00 */     INVALID,        1560,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__269
              /* 00 */        1137,     INVALID,     INVALID,        1136,
            },

            new ushort[] { //ud_itab__270
              /* 00 */        1141,     INVALID,     INVALID,        1140,
            },

            new ushort[] { //ud_itab__271
              /* 00 */        1145,     INVALID,     INVALID,        1144,
            },

            new ushort[] { //ud_itab__272
              /* 00 */        1534,     INVALID,     INVALID,        1535,
            },

            new ushort[] { //ud_itab__273
              /* 00 */        1069,     INVALID,     INVALID,        1070,
            },

            new ushort[] { //ud_itab__274
              /* 00 */        1134,     INVALID,     INVALID,        1132,
            },

            new ushort[] { //ud_itab__275
              /* 00 */     INVALID,  0x8000|276,
            },

            new ushort[] { //ud_itab__276
              /* 00 */         799,     INVALID,     INVALID,        1520,
            },

            new ushort[] { //ud_itab__277
              /* 00 */        1180,     INVALID,     INVALID,        1178,
            },

            new ushort[] { //ud_itab__278
              /* 00 */        1183,     INVALID,     INVALID,        1181,
            },

            new ushort[] { //ud_itab__279
              /* 00 */        1184,     INVALID,     INVALID,        1185,
            },

            new ushort[] { //ud_itab__280
              /* 00 */        1533,     INVALID,     INVALID,        1531,
            },

            new ushort[] { //ud_itab__281
              /* 00 */         996,     INVALID,     INVALID,         994,
            },

            new ushort[] { //ud_itab__282
              /* 00 */         997,     INVALID,     INVALID,         998,
            },

            new ushort[] { //ud_itab__283
              /* 00 */        1000,     INVALID,     INVALID,        1001,
            },

            new ushort[] { //ud_itab__284
              /* 00 */        1243,     INVALID,
            },

            new ushort[] { //ud_itab__285
              /* 00 */        1097,     INVALID,
            },

            new ushort[] { //ud_itab__286
              /* 00 */        1244,     INVALID,
            },

            new ushort[] { //ud_itab__287
              /* 00 */        1098,     INVALID,
            },

            new ushort[] { //ud_itab__288
              /* 00 */         173,     INVALID,
            },

            new ushort[] { //ud_itab__289
              /* 00 */         174,     INVALID,
            },

            new ushort[] { //ud_itab__290
              /* 00 */           1,     INVALID,
            },

            new ushort[] { //ud_itab__291
              /* 00 */           4,     INVALID,
            },

            new ushort[] { //ud_itab__292
              /* 00 */  0x8000|293,  0x8000|294,     INVALID,
            },

            new ushort[] { //ud_itab__293
              /* 00 */        1258,     INVALID,
            },

            new ushort[] { //ud_itab__294
              /* 00 */        1259,     INVALID,
            },

            new ushort[] { //ud_itab__295
              /* 00 */  0x8000|296,  0x8000|297,     INVALID,
            },

            new ushort[] { //ud_itab__296
              /* 00 */        1110,     INVALID,
            },

            new ushort[] { //ud_itab__297
              /* 00 */        1111,     INVALID,
            },

            new ushort[] { //ud_itab__298
              /* 00 */        1659,     INVALID,
            },

            new ushort[] { //ud_itab__299
              /* 00 */          67,          68,
            },

            new ushort[] { //ud_itab__300
              /* 00 */         710,         711,     INVALID,
            },

            new ushort[] { //ud_itab__301
              /* 00 */         983,         984,     INVALID,
            },

            new ushort[] { //ud_itab__302
              /* 00 */          21,         970,          11,        1343,
              /* 04 */          55,        1414,        1494,         106,
            },

            new ushort[] { //ud_itab__303
              /* 00 */          23,         971,          13,        1344,
              /* 04 */          57,        1415,        1495,         108,
            },

            new ushort[] { //ud_itab__304
              /* 00 */  0x8000|305,  0x8000|306,  0x8000|307,  0x8000|308,
              /* 04 */  0x8000|309,  0x8000|310,  0x8000|311,  0x8000|312,
            },

            new ushort[] { //ud_itab__305
              /* 00 */          22,     INVALID,
            },

            new ushort[] { //ud_itab__306
              /* 00 */         972,     INVALID,
            },

            new ushort[] { //ud_itab__307
              /* 00 */          12,     INVALID,
            },

            new ushort[] { //ud_itab__308
              /* 00 */        1345,     INVALID,
            },

            new ushort[] { //ud_itab__309
              /* 00 */          56,     INVALID,
            },

            new ushort[] { //ud_itab__310
              /* 00 */        1416,     INVALID,
            },

            new ushort[] { //ud_itab__311
              /* 00 */        1496,     INVALID,
            },

            new ushort[] { //ud_itab__312
              /* 00 */         107,     INVALID,
            },

            new ushort[] { //ud_itab__313
              /* 00 */          24,         973,          14,        1346,
              /* 04 */          58,        1417,        1497,         109,
            },

            new ushort[] { //ud_itab__314
              /* 00 */        1109,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__315
              /* 00 */          74,          75,          76,
            },

            new ushort[] { //ud_itab__316
              /* 00 */         170,         171,         172,
            },

            new ushort[] { //ud_itab__317
              /* 00 */          73,     INVALID,
            },

            new ushort[] { //ud_itab__318
              /* 00 */  0x8000|319,  0x8000|320,  0x8000|321,
            },

            new ushort[] { //ud_itab__319
              /* 00 */        1260,        1261,
            },

            new ushort[] { //ud_itab__320
              /* 00 */        1262,        1263,
            },

            new ushort[] { //ud_itab__321
              /* 00 */     INVALID,        1264,
            },

            new ushort[] { //ud_itab__322
              /* 00 */  0x8000|323,  0x8000|324,  0x8000|325,
            },

            new ushort[] { //ud_itab__323
              /* 00 */        1112,        1113,
            },

            new ushort[] { //ud_itab__324
              /* 00 */        1114,        1115,
            },

            new ushort[] { //ud_itab__325
              /* 00 */     INVALID,        1116,
            },

            new ushort[] { //ud_itab__326
              /* 00 */         923,         924,         927,
            },

            new ushort[] { //ud_itab__327
              /* 00 */         115,         116,         119,
            },

            new ushort[] { //ud_itab__328
              /* 00 */        1404,        1405,        1406,
            },

            new ushort[] { //ud_itab__329
              /* 00 */         791,         792,         793,
            },

            new ushort[] { //ud_itab__330
              /* 00 */        1348,        1349,        1350,
            },

            new ushort[] { //ud_itab__331
              /* 00 */        1280,        1287,        1268,        1276,
              /* 04 */        1328,        1335,        1319,        1314,
            },

            new ushort[] { //ud_itab__332
              /* 00 */        1285,        1288,        1269,        1275,
              /* 04 */        1324,        1331,        1320,        1316,
            },

            new ushort[] { //ud_itab__333
              /* 00 */  0x8000|334,  0x8000|335,     INVALID,     INVALID,
              /* 04 */     INVALID,  0x8000|341,  0x8000|357,  0x8000|369,
              /* 08 */     INVALID,  0x8000|394,     INVALID,     INVALID,
              /* 0c */     INVALID,  0x8000|399,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__334
              /* 00 */         771,     INVALID,
            },

            new ushort[] { //ud_itab__335
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 10 */         937,         939,  0x8000|336,         895,
              /* 14 */        1451,        1449,  0x8000|337,         885,
              /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */         863,         865,     INVALID,         908,
              /* 2c */     INVALID,     INVALID,        1444,         130,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 50 */         901,        1389,        1308,        1293,
              /* 54 */          62,          66,         977,        1501,
              /* 58 */          28,         947,         146,         135,
              /* 5c */        1421,         819,         190,         803,
              /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 74 */     INVALID,     INVALID,     INVALID,  0x8000|338,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ac */     INVALID,     INVALID,  0x8000|339,     INVALID,
              /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c0 */     INVALID,     INVALID,         113,     INVALID,
              /* c4 */     INVALID,     INVALID,        1383,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__336
              /* 00 */         893,         897,
            },

            new ushort[] { //ud_itab__337
              /* 00 */         883,         887,
            },

            new ushort[] { //ud_itab__338
              /* 00 */        1743,        1744,
            },

            new ushort[] { //ud_itab__339
              /* 00 */  0x8000|340,     INVALID,
            },

            new ushort[] { //ud_itab__340
              /* 00 */     INVALID,     INVALID,     INVALID,        1402,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__341
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 10 */         933,         935,  0x8000|342,         891,
              /* 14 */        1453,        1447,  0x8000|343,         881,
              /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */         859,         861,     INVALID,         906,
              /* 2c */     INVALID,     INVALID,        1442,         128,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 50 */         899,        1391,     INVALID,     INVALID,
              /* 54 */          60,          64,         975,        1499,
              /* 58 */          26,         945,         140,         144,
              /* 5c */        1419,         817,         188,         801,
              /* 60 */        1209,        1212,        1215,         986,
              /* 64 */        1037,        1040,        1043,         992,
              /* 68 */        1200,        1203,        1206,         989,
              /* 6c */        1549,        1547,  0x8000|344,        1519,
              /* 70 */        1541,  0x8000|345,  0x8000|347,  0x8000|349,
              /* 74 */        1029,        1032,        1035,     INVALID,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */        1551,        1555,  0x8000|351,        1517,
              /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c0 */     INVALID,     INVALID,         111,     INVALID,
              /* c4 */        1061,        1054,        1381,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */          34,        1163,        1169,        1175,
              /* d4 */        1530,        1095,         919,  0x8000|352,
              /* d8 */        1195,        1198,        1082,        1016,
              /* dc */        1011,        1014,        1077,        1019,
              /* e0 */        1022,        1150,        1156,        1025,
              /* e4 */        1089,        1091,         161,         903,
              /* e8 */        1189,        1192,        1079,        1118,
              /* ec */        1005,        1008,        1073,        1266,
              /* f0 */     INVALID,  0x8000|353,  0x8000|354,  0x8000|355,
              /* f4 */     INVALID,        1071,        1133,  0x8000|356,
              /* f8 */        1179,        1182,        1186,        1532,
              /* fc */         995,         999,        1002,     INVALID,
            },

            new ushort[] { //ud_itab__342
              /* 00 */         889,     INVALID,
            },

            new ushort[] { //ud_itab__343
              /* 00 */         879,     INVALID,
            },

            new ushort[] { //ud_itab__344
              /* 00 */         869,         871,         912,
            },

            new ushort[] { //ud_itab__345
              /* 00 */     INVALID,     INVALID,        1165,     INVALID,
              /* 04 */        1152,     INVALID,  0x8000|346,     INVALID,
            },

            new ushort[] { //ud_itab__346
              /* 00 */        1757,     INVALID,
            },

            new ushort[] { //ud_itab__347
              /* 00 */     INVALID,     INVALID,        1171,     INVALID,
              /* 04 */        1159,     INVALID,  0x8000|348,     INVALID,
            },

            new ushort[] { //ud_itab__348
              /* 00 */        1759,     INVALID,
            },

            new ushort[] { //ud_itab__349
              /* 00 */     INVALID,     INVALID,        1177,        1545,
              /* 04 */     INVALID,     INVALID,  0x8000|350,        1543,
            },

            new ushort[] { //ud_itab__350
              /* 00 */        1761,     INVALID,
            },

            new ushort[] { //ud_itab__351
              /* 00 */         875,         877,         915,
            },

            new ushort[] { //ud_itab__352
              /* 00 */        1085,     INVALID,
            },

            new ushort[] { //ud_itab__353
              /* 00 */        1756,     INVALID,
            },

            new ushort[] { //ud_itab__354
              /* 00 */        1758,     INVALID,
            },

            new ushort[] { //ud_itab__355
              /* 00 */        1760,     INVALID,
            },

            new ushort[] { //ud_itab__356
              /* 00 */     INVALID,        1521,
            },

            new ushort[] { //ud_itab__357
              /* 00 */        1585,        1588,        1591,        1594,
              /* 04 */        1597,        1600,        1603,        1606,
              /* 08 */        1609,        1615,        1612,        1618,
              /* 0c */  0x8000|358,  0x8000|359,  0x8000|360,  0x8000|361,
              /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 14 */     INVALID,     INVALID,     INVALID,        1713,
              /* 18 */  0x8000|362,  0x8000|363,     INVALID,     INVALID,
              /* 1c */        1576,        1579,        1582,     INVALID,
              /* 20 */        1687,        1689,        1691,        1693,
              /* 24 */        1695,     INVALID,     INVALID,     INVALID,
              /* 28 */        1624,        1710,        1683,        1685,
              /* 2c */  0x8000|365,  0x8000|366,  0x8000|367,  0x8000|368,
              /* 30 */        1698,        1700,        1702,        1704,
              /* 34 */        1706,        1708,     INVALID,        1719,
              /* 38 */        1626,        1628,        1630,        1632,
              /* 3c */        1634,        1636,        1640,        1638,
              /* 40 */        1642,        1644,     INVALID,     INVALID,
              /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d8 */     INVALID,     INVALID,     INVALID,          46,
              /* dc */          42,          44,          38,          40,
              /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__358
              /* 00 */        1738,     INVALID,
            },

            new ushort[] { //ud_itab__359
              /* 00 */        1736,     INVALID,
            },

            new ushort[] { //ud_itab__360
              /* 00 */        1741,     INVALID,
            },

            new ushort[] { //ud_itab__361
              /* 00 */        1742,     INVALID,
            },

            new ushort[] { //ud_itab__362
              /* 00 */        1728,     INVALID,
            },

            new ushort[] { //ud_itab__363
              /* 00 */  0x8000|364,     INVALID,
            },

            new ushort[] { //ud_itab__364
              /* 00 */     INVALID,        1729,
            },

            new ushort[] { //ud_itab__365
              /* 00 */        1732,     INVALID,
            },

            new ushort[] { //ud_itab__366
              /* 00 */        1734,     INVALID,
            },

            new ushort[] { //ud_itab__367
              /* 00 */        1733,     INVALID,
            },

            new ushort[] { //ud_itab__368
              /* 00 */        1735,     INVALID,
            },

            new ushort[] { //ud_itab__369
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */  0x8000|370,  0x8000|371,  0x8000|372,     INVALID,
              /* 08 */        1646,        1648,        1650,        1652,
              /* 0c */        1656,        1654,        1679,        1621,
              /* 10 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 14 */  0x8000|374,        1057,  0x8000|375,         202,
              /* 18 */  0x8000|379,  0x8000|381,     INVALID,     INVALID,
              /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 20 */  0x8000|383,        1559,  0x8000|385,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 40 */         198,         196,        1681,     INVALID,
              /* 44 */        1514,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,  0x8000|391,  0x8000|392,
              /* 4c */  0x8000|393,     INVALID,     INVALID,     INVALID,
              /* 50 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 58 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 5c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 60 */        1717,        1715,        1723,        1721,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 70 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* dc */     INVALID,     INVALID,     INVALID,          48,
              /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__370
              /* 00 */        1739,     INVALID,
            },

            new ushort[] { //ud_itab__371
              /* 00 */        1737,     INVALID,
            },

            new ushort[] { //ud_itab__372
              /* 00 */  0x8000|373,     INVALID,
            },

            new ushort[] { //ud_itab__373
              /* 00 */     INVALID,        1740,
            },

            new ushort[] { //ud_itab__374
              /* 00 */        1046,     INVALID,
            },

            new ushort[] { //ud_itab__375
              /* 00 */  0x8000|376,  0x8000|377,  0x8000|378,
            },

            new ushort[] { //ud_itab__376
              /* 00 */        1048,     INVALID,
            },

            new ushort[] { //ud_itab__377
              /* 00 */        1050,     INVALID,
            },

            new ushort[] { //ud_itab__378
              /* 00 */     INVALID,        1052,
            },

            new ushort[] { //ud_itab__379
              /* 00 */  0x8000|380,     INVALID,
            },

            new ushort[] { //ud_itab__380
              /* 00 */     INVALID,        1731,
            },

            new ushort[] { //ud_itab__381
              /* 00 */  0x8000|382,     INVALID,
            },

            new ushort[] { //ud_itab__382
              /* 00 */     INVALID,        1730,
            },

            new ushort[] { //ud_itab__383
              /* 00 */  0x8000|384,     INVALID,
            },

            new ushort[] { //ud_itab__384
              /* 00 */        1065,     INVALID,
            },

            new ushort[] { //ud_itab__385
              /* 00 */  0x8000|386,  0x8000|388,
            },

            new ushort[] { //ud_itab__386
              /* 00 */  0x8000|387,     INVALID,
            },

            new ushort[] { //ud_itab__387
              /* 00 */        1066,     INVALID,
            },

            new ushort[] { //ud_itab__388
              /* 00 */  0x8000|389,  0x8000|390,
            },

            new ushort[] { //ud_itab__389
              /* 00 */        1067,     INVALID,
            },

            new ushort[] { //ud_itab__390
              /* 00 */        1068,     INVALID,
            },

            new ushort[] { //ud_itab__391
              /* 00 */        1746,     INVALID,
            },

            new ushort[] { //ud_itab__392
              /* 00 */        1745,     INVALID,
            },

            new ushort[] { //ud_itab__393
              /* 00 */        1755,     INVALID,
            },

            new ushort[] { //ud_itab__394
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 10 */  0x8000|395,  0x8000|396,  0x8000|397,     INVALID,
              /* 14 */     INVALID,     INVALID,  0x8000|398,     INVALID,
              /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */     INVALID,     INVALID,         155,     INVALID,
              /* 2c */         169,         159,     INVALID,     INVALID,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 50 */     INVALID,        1395,        1310,        1295,
              /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 58 */          32,         951,         157,         164,
              /* 5c */        1425,         823,         194,         807,
              /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 6c */     INVALID,     INVALID,     INVALID,        1524,
              /* 70 */        1537,     INVALID,     INVALID,     INVALID,
              /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */     INVALID,     INVALID,         917,        1526,
              /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c0 */     INVALID,     INVALID,         121,     INVALID,
              /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e4 */     INVALID,     INVALID,         133,     INVALID,
              /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__395
              /* 00 */        1752,        1751,
            },

            new ushort[] { //ud_itab__396
              /* 00 */        1754,        1753,
            },

            new ushort[] { //ud_itab__397
              /* 00 */        1573,        1571,
            },

            new ushort[] { //ud_itab__398
              /* 00 */        1569,        1567,
            },

            new ushort[] { //ud_itab__399
              /* 00 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 08 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 0c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 10 */  0x8000|400,  0x8000|401,  0x8000|402,     INVALID,
              /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 18 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */     INVALID,     INVALID,         153,     INVALID,
              /* 2c */         167,         149,     INVALID,     INVALID,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 40 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 44 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 48 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 4c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 50 */     INVALID,        1393,     INVALID,     INVALID,
              /* 54 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 58 */          30,         949,         151,     INVALID,
              /* 5c */        1423,         821,         192,         805,
              /* 60 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 64 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 68 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 6c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 70 */        1539,     INVALID,     INVALID,     INVALID,
              /* 74 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 78 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 7c */        1553,        1557,     INVALID,     INVALID,
              /* 80 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 84 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 88 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 8c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 90 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 94 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 98 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 9c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* a8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ac */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* b8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* bc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c0 */     INVALID,     INVALID,         118,     INVALID,
              /* c4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* c8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* cc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d0 */          36,     INVALID,     INVALID,     INVALID,
              /* d4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* d8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* dc */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e0 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* e4 */     INVALID,     INVALID,         137,     INVALID,
              /* e8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* ec */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f0 */        1561,     INVALID,     INVALID,     INVALID,
              /* f4 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* f8 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* fc */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__400
              /* 00 */        1748,        1747,
            },

            new ushort[] { //ud_itab__401
              /* 00 */        1750,        1749,
            },

            new ushort[] { //ud_itab__402
              /* 00 */        1565,        1563,
            },

            new ushort[] { //ud_itab__403
              /* 00 */  0x8000|404,  0x8000|335,     INVALID,     INVALID,
              /* 04 */     INVALID,  0x8000|341,  0x8000|357,  0x8000|369,
              /* 08 */     INVALID,  0x8000|394,     INVALID,     INVALID,
              /* 0c */     INVALID,  0x8000|399,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__404
              /* 00 */         769,     INVALID,
            },

            new ushort[] { //ud_itab__405
              /* 00 */         826,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__406
              /* 00 */         827,     INVALID,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__407
              /* 00 */         715,     INVALID,
            },

            new ushort[] { //ud_itab__408
              /* 00 */         723,         724,         725,
            },

            new ushort[] { //ud_itab__409
              /* 00 */        1281,        1286,        1270,        1274,
              /* 04 */        1327,        1334,        1321,        1315,
            },

            new ushort[] { //ud_itab__410
              /* 00 */        1282,        1289,        1273,        1277,
              /* 04 */        1326,        1333,        1330,        1313,
            },

            new ushort[] { //ud_itab__411
              /* 00 */        1283,        1290,        1271,        1278,
              /* 04 */        1325,        1332,        1322,        1317,
            },

            new ushort[] { //ud_itab__412
              /* 00 */        1284,        1291,        1272,        1279,
              /* 04 */        1329,        1336,        1323,        1318,
            },

            new ushort[] { //ud_itab__413
              /* 00 */           3,     INVALID,
            },

            new ushort[] { //ud_itab__414
              /* 00 */           2,     INVALID,
            },

            new ushort[] { //ud_itab__415
              /* 00 */        1312,     INVALID,
            },

            new ushort[] { //ud_itab__416
              /* 00 */  0x8000|417,  0x8000|418,
            },

            new ushort[] { //ud_itab__417
              /* 00 */         206,         503,         307,         357,
              /* 04 */         587,         630,         387,         413,
            },

            new ushort[] { //ud_itab__418
              /* 00 */         215,         216,         217,         218,
              /* 04 */         219,         220,         221,         222,
              /* 08 */         504,         505,         506,         507,
              /* 0c */         508,         509,         510,         511,
              /* 10 */         309,         310,         311,         312,
              /* 14 */         313,         314,         315,         316,
              /* 18 */         359,         360,         361,         362,
              /* 1c */         363,         364,         365,         366,
              /* 20 */         589,         590,         591,         592,
              /* 24 */         593,         594,         595,         596,
              /* 28 */         614,         615,         616,         617,
              /* 2c */         618,         619,         620,         621,
              /* 30 */         388,         389,         390,         391,
              /* 34 */         392,         393,         394,         395,
              /* 38 */         414,         415,         416,         417,
              /* 3c */         418,         419,         420,         421,
            },

            new ushort[] { //ud_itab__419
              /* 00 */  0x8000|420,  0x8000|421,
            },

            new ushort[] { //ud_itab__420
              /* 00 */         476,     INVALID,         573,         540,
              /* 04 */         493,         492,         584,         583,
            },

            new ushort[] { //ud_itab__421
              /* 00 */         477,         478,         479,         480,
              /* 04 */         481,         482,         483,         484,
              /* 08 */         658,         659,         660,         661,
              /* 0c */         662,         663,         664,         665,
              /* 10 */         522,     INVALID,     INVALID,     INVALID,
              /* 14 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 18 */         549,         550,         551,         552,
              /* 1c */         553,         554,         555,         556,
              /* 20 */         233,         204,     INVALID,     INVALID,
              /* 24 */         639,         657,     INVALID,     INVALID,
              /* 28 */         485,         486,         487,         488,
              /* 2c */         489,         490,         491,     INVALID,
              /* 30 */         203,         685,         529,         526,
              /* 34 */         684,         528,         377,         454,
              /* 38 */         527,         686,         537,         536,
              /* 3c */         530,         534,         535,         376,
            },

            new ushort[] { //ud_itab__422
              /* 00 */  0x8000|423,  0x8000|424,
            },

            new ushort[] { //ud_itab__423
              /* 00 */         456,         520,         448,         450,
              /* 04 */         462,         464,         460,         458,
            },

            new ushort[] { //ud_itab__424
              /* 00 */         235,         236,         237,         238,
              /* 04 */         239,         240,         241,         242,
              /* 08 */         243,         244,         245,         246,
              /* 0c */         247,         248,         249,         250,
              /* 10 */         251,         252,         253,         254,
              /* 14 */         255,         256,         257,         258,
              /* 18 */         259,         260,         261,         262,
              /* 1c */         263,         264,         265,         266,
              /* 20 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */     INVALID,         656,     INVALID,     INVALID,
              /* 2c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__425
              /* 00 */  0x8000|426,  0x8000|427,
            },

            new ushort[] { //ud_itab__426
              /* 00 */         453,         471,         467,         470,
              /* 04 */     INVALID,         474,     INVALID,         538,
            },

            new ushort[] { //ud_itab__427
              /* 00 */         267,         268,         269,         270,
              /* 04 */         271,         272,         273,         274,
              /* 08 */         275,         276,         277,         278,
              /* 0c */         279,         280,         281,         282,
              /* 10 */         283,         284,         285,         286,
              /* 14 */         287,         288,         289,         290,
              /* 18 */         291,         292,         293,         294,
              /* 1c */         295,         296,         297,         298,
              /* 20 */         524,         523,         234,         455,
              /* 24 */         525,         532,     INVALID,     INVALID,
              /* 28 */         299,         300,         301,         302,
              /* 2c */         303,         304,         305,         306,
              /* 30 */         333,         334,         335,         336,
              /* 34 */         337,         338,         339,         340,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__428
              /* 00 */  0x8000|429,  0x8000|430,
            },

            new ushort[] { //ud_itab__429
              /* 00 */         205,         494,         308,         358,
              /* 04 */         588,         613,         378,         404,
            },

            new ushort[] { //ud_itab__430
              /* 00 */         207,         208,         209,         210,
              /* 04 */         211,         212,         213,         214,
              /* 08 */         495,         496,         497,         498,
              /* 0c */         499,         500,         501,         502,
              /* 10 */         317,         318,         319,         320,
              /* 14 */         321,         322,         323,         324,
              /* 18 */         325,         326,         327,         328,
              /* 1c */         329,         330,         331,         332,
              /* 20 */         622,         623,         624,         625,
              /* 24 */         626,         627,         628,         629,
              /* 28 */         597,         598,         599,         600,
              /* 2c */         601,         602,         603,         604,
              /* 30 */         405,         406,         407,         408,
              /* 34 */         409,         410,         411,         412,
              /* 38 */         379,         380,         381,         382,
              /* 3c */         383,         384,         385,         386,
            },

            new ushort[] { //ud_itab__431
              /* 00 */  0x8000|432,  0x8000|433,
            },

            new ushort[] { //ud_itab__432
              /* 00 */         475,         472,         574,         539,
              /* 04 */         531,     INVALID,         533,         585,
            },

            new ushort[] { //ud_itab__433
              /* 00 */         431,         432,         433,         434,
              /* 04 */         435,         436,         437,         438,
              /* 08 */         666,         667,         668,         669,
              /* 0c */         670,         671,         672,         673,
              /* 10 */         575,         576,         577,         578,
              /* 14 */         579,         580,         581,         582,
              /* 18 */         541,         542,         543,         544,
              /* 1c */         545,         546,         547,         548,
              /* 20 */         640,         641,         642,         643,
              /* 24 */         644,         645,         646,         647,
              /* 28 */         648,         649,         650,         651,
              /* 2c */         652,         653,         654,         655,
              /* 30 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 34 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__434
              /* 00 */  0x8000|435,  0x8000|436,
            },

            new ushort[] { //ud_itab__435
              /* 00 */         457,         521,         447,         449,
              /* 04 */         463,         465,         461,         459,
            },

            new ushort[] { //ud_itab__436
              /* 00 */         223,         224,         225,         226,
              /* 04 */         227,         228,         229,         230,
              /* 08 */         512,         513,         514,         515,
              /* 0c */         516,         517,         518,         519,
              /* 10 */         367,         368,         369,         370,
              /* 14 */         371,         372,         373,         374,
              /* 18 */     INVALID,         375,     INVALID,     INVALID,
              /* 1c */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 20 */         631,         632,         633,         634,
              /* 24 */         635,         636,         637,         638,
              /* 28 */         605,         606,         607,         608,
              /* 2c */         609,         610,         611,         612,
              /* 30 */         422,         423,         424,         425,
              /* 34 */         426,         427,         428,         429,
              /* 38 */         396,         397,         398,         399,
              /* 3c */         400,         401,         402,         403,
            },

            new ushort[] { //ud_itab__437
              /* 00 */  0x8000|438,  0x8000|439,
            },

            new ushort[] { //ud_itab__438
              /* 00 */         451,         473,         466,         468,
              /* 04 */         231,         452,         232,         469,
            },

            new ushort[] { //ud_itab__439
              /* 00 */         439,         440,         441,         442,
              /* 04 */         443,         444,         445,         446,
              /* 08 */         674,         675,         676,         677,
              /* 0c */         678,         679,         680,         681,
              /* 10 */         557,         558,         559,         560,
              /* 14 */         561,         562,         563,         564,
              /* 18 */         565,         566,         567,         568,
              /* 1c */         569,         570,         571,         572,
              /* 20 */         586,     INVALID,     INVALID,     INVALID,
              /* 24 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 28 */         341,         342,         343,         344,
              /* 2c */         345,         346,         347,         348,
              /* 30 */         349,         350,         351,         352,
              /* 34 */         353,         354,         355,         356,
              /* 38 */     INVALID,     INVALID,     INVALID,     INVALID,
              /* 3c */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__440
              /* 00 */         758,         759,         760,
            },

            new ushort[] { //ud_itab__441
              /* 00 */         764,     INVALID,
            },

            new ushort[] { //ud_itab__442
              /* 00 */        1433,        1438,         962,         953,
              /* 04 */         942,         695,         186,         689,
            },

            new ushort[] { //ud_itab__443
              /* 00 */        1439,        1440,         963,         954,
              /* 04 */         943,         696,         185,         688,
            },

            new ushort[] { //ud_itab__444
              /* 00 */         708,         183,     INVALID,     INVALID,
              /* 04 */     INVALID,     INVALID,     INVALID,     INVALID,
            },

            new ushort[] { //ud_itab__445
              /* 00 */         707,         184,  0x8000|446,          71,
              /* 04 */         761,         762,        1256,     INVALID,
            },

            new ushort[] { //ud_itab__446
              /* 00 */          69,          70,
            },

        };
        #endregion

        #region Lookup Table List
        internal static readonly (ud_table_type ud_type, ushort[] ids)[] ud_table_type_ids = new (ud_table_type, ushort[])[]
        {
            (ud_table_type.UD_TAB__OPC_TABLE, new ushort[] {0, 4, 54, 116, 335, 341, 357, 369, 394, 399}),
            (ud_table_type.UD_TAB__OPC_SSE, new ushort[] {29, 30, 32, 33, 34, 35, 36, 38, 39, 40, 42, 43, 44, 45, 46, 47, 48, 49, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 105, 109, 110, 111, 112, 113, 114, 115, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 129, 130, 131, 132, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 176, 177, 179, 180, 181, 183, 184, 185, 187, 188, 189, 190, 191, 192, 193, 196, 197, 198, 201, 221, 223, 224, 225, 226, 230, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 276, 277, 278, 279, 280, 281, 282, 283}),
            (ud_table_type.UD_TAB__OPC_REG, new ushort[] {5, 7, 8, 27, 41, 178, 182, 186, 203, 208, 216, 217, 222, 228, 235, 302, 303, 304, 313, 314, 331, 332, 340, 345, 347, 349, 405, 406, 409, 410, 411, 412, 417, 420, 423, 426, 429, 432, 435, 438, 442, 443, 444, 445}),
            (ud_table_type.UD_TAB__OPC_RM, new ushort[] {9, 14, 15, 16, 25, 204, 205, 206, 209, 210, 211, 212, 213, 214, 218, 219, 220}),
            (ud_table_type.UD_TAB__OPC_MOD, new ushort[] {6, 31, 37, 202, 207, 215, 227, 275, 336, 337, 339, 342, 343, 356, 395, 396, 397, 398, 400, 401, 402, 416, 419, 422, 425, 428, 431, 434, 437}),
            (ud_table_type.UD_TAB__OPC_MODE, new ushort[] {1, 2, 3, 50, 52, 102, 106, 284, 285, 286, 287, 288, 289, 290, 291, 293, 294, 296, 297, 298, 299, 305, 306, 307, 308, 309, 310, 311, 312, 317, 319, 320, 321, 323, 324, 325, 334, 385, 404, 407, 413, 414, 415, 441, 446}),
            (ud_table_type.UD_TAB__OPC_X87, new ushort[] {418, 421, 424, 427, 430, 433, 436, 439}),
            (ud_table_type.UD_TAB__OPC_ASIZE, new ushort[] {440}),
            (ud_table_type.UD_TAB__OPC_OSIZE, new ushort[] {128, 133, 174, 175, 199, 200, 229, 292, 295, 300, 301, 315, 316, 318, 322, 326, 327, 328, 329, 330, 344, 351, 375, 408}),
            (ud_table_type.UD_TAB__OPC_3DNOW, new ushort[] {28}),
            (ud_table_type.UD_TAB__OPC_VENDOR, new ushort[] {10, 11, 12, 13, 17, 18, 19, 20, 21, 22, 23, 24, 26, 51, 53, 103, 104, 107, 108, 194, 195, 231, 232, 233, 234}),
            (ud_table_type.UD_TAB__OPC_VEX, new ushort[] {333, 403}),
            (ud_table_type.UD_TAB__OPC_VEX_W, new ushort[] {358, 359, 360, 361, 362, 363, 365, 366, 367, 368, 370, 371, 372, 374, 376, 377, 378, 379, 381, 383, 386, 388, 391, 392, 393}),
            (ud_table_type.UD_TAB__OPC_VEX_L, new ushort[] {338, 346, 348, 350, 352, 353, 354, 355, 364, 373, 380, 382, 384, 387, 389, 390})
        };
        internal static readonly Dictionary<ushort, ud_table_type> ud_lookup_table_type_dict = new Dictionary<ushort, ud_table_type>();
        #endregion

        #region Operand Definitions
        /// <summary>
        /// itab entry operand definitions (for readability)
        /// </summary>
        internal static readonly Dictionary<string, ud_itab_entry_operand> OpDefDict = new Dictionary<string, ud_itab_entry_operand>()
        {
            {"AL"  , new ud_itab_entry_operand(ud_operand_code.OP_AL   , ud_operand_size.SZ_B   )},
            {"Av"  , new ud_itab_entry_operand(ud_operand_code.OP_A    , ud_operand_size.SZ_V   )},
            {"AX"  , new ud_itab_entry_operand(ud_operand_code.OP_AX   , ud_operand_size.SZ_W   )},
            {"C"   , new ud_itab_entry_operand(ud_operand_code.OP_C    , ud_operand_size.SZ_NA  )},
            {"CL"  , new ud_itab_entry_operand(ud_operand_code.OP_CL   , ud_operand_size.SZ_B   )},
            {"CS"  , new ud_itab_entry_operand(ud_operand_code.OP_CS   , ud_operand_size.SZ_NA  )},
            {"CX"  , new ud_itab_entry_operand(ud_operand_code.OP_CX   , ud_operand_size.SZ_W   )},
            {"D"   , new ud_itab_entry_operand(ud_operand_code.OP_D    , ud_operand_size.SZ_NA  )},
            {"DL"  , new ud_itab_entry_operand(ud_operand_code.OP_DL   , ud_operand_size.SZ_B   )},
            {"DS"  , new ud_itab_entry_operand(ud_operand_code.OP_DS   , ud_operand_size.SZ_NA  )},
            {"DX"  , new ud_itab_entry_operand(ud_operand_code.OP_DX   , ud_operand_size.SZ_W   )},
            {"E"   , new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_NA  )},
            {"eAX" , new ud_itab_entry_operand(ud_operand_code.OP_eAX  , ud_operand_size.SZ_Z   )},
            {"Eb"  , new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_B   )},
            {"eCX" , new ud_itab_entry_operand(ud_operand_code.OP_eCX  , ud_operand_size.SZ_Z   )},
            {"Ed"  , new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_D   )},
            {"eDX" , new ud_itab_entry_operand(ud_operand_code.OP_eDX  , ud_operand_size.SZ_Z   )},
            {"Eq"  , new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_Q   )},
            {"ES"  , new ud_itab_entry_operand(ud_operand_code.OP_ES   , ud_operand_size.SZ_NA  )},
            {"Ev"  , new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_V   )},
            {"Ew"  , new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_W   )},
            {"Ey"  , new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_Y   )},
            {"Ez"  , new ud_itab_entry_operand(ud_operand_code.OP_E    , ud_operand_size.SZ_Z   )},
            {"FS"  , new ud_itab_entry_operand(ud_operand_code.OP_FS   , ud_operand_size.SZ_NA  )},
            {"Fv"  , new ud_itab_entry_operand(ud_operand_code.OP_F    , ud_operand_size.SZ_V   )},
            {"G"   , new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_NA  )},
            {"Gb"  , new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_B   )},
            {"Gd"  , new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_D   )},
            {"Gq"  , new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_Q   )},
            {"GS"  , new ud_itab_entry_operand(ud_operand_code.OP_GS   , ud_operand_size.SZ_NA  )},
            {"Gv"  , new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_V   )},
            {"Gw"  , new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_W   )},
            {"Gy"  , new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_Y   )},
            {"Gz"  , new ud_itab_entry_operand(ud_operand_code.OP_G    , ud_operand_size.SZ_Z   )},
            {"H"   , new ud_itab_entry_operand(ud_operand_code.OP_H    , ud_operand_size.SZ_X   )},
            {"Hqq" , new ud_itab_entry_operand(ud_operand_code.OP_H    , ud_operand_size.SZ_QQ  )},
            {"Hx"  , new ud_itab_entry_operand(ud_operand_code.OP_H    , ud_operand_size.SZ_X   )},
            {"I1"  , new ud_itab_entry_operand(ud_operand_code.OP_I1   , ud_operand_size.SZ_NA  )},
            {"I3"  , new ud_itab_entry_operand(ud_operand_code.OP_I3   , ud_operand_size.SZ_NA  )},
            {"Ib"  , new ud_itab_entry_operand(ud_operand_code.OP_I    , ud_operand_size.SZ_B   )},
            {"Iv"  , new ud_itab_entry_operand(ud_operand_code.OP_I    , ud_operand_size.SZ_V   )},
            {"Iw"  , new ud_itab_entry_operand(ud_operand_code.OP_I    , ud_operand_size.SZ_W   )},
            {"Iz"  , new ud_itab_entry_operand(ud_operand_code.OP_I    , ud_operand_size.SZ_Z   )},
            {"Jb"  , new ud_itab_entry_operand(ud_operand_code.OP_J    , ud_operand_size.SZ_B   )},
            {"Jv"  , new ud_itab_entry_operand(ud_operand_code.OP_J    , ud_operand_size.SZ_V   )},
            {"Jz"  , new ud_itab_entry_operand(ud_operand_code.OP_J    , ud_operand_size.SZ_Z   )},
            {"L"   , new ud_itab_entry_operand(ud_operand_code.OP_L    , ud_operand_size.SZ_O   )},
            {"Lx"  , new ud_itab_entry_operand(ud_operand_code.OP_L    , ud_operand_size.SZ_X   )},
            {"M"   , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_NA  )},
            {"Mb"  , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_B   )},
            {"MbRd", new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_BD  )},
            {"MbRv", new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_BV  )},
            {"Md"  , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_D   )},
            {"Mdq" , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_DQ  )},
            {"MdRy", new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_DY  )},
            {"MdU" , new ud_itab_entry_operand(ud_operand_code.OP_MU   , ud_operand_size.SZ_DO  )},
            {"Mo"  , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_O   )},
            {"Mq"  , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_Q   )},
            {"MqU" , new ud_itab_entry_operand(ud_operand_code.OP_MU   , ud_operand_size.SZ_QO  )},
            {"Ms"  , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_W   )},
            {"Mt"  , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_T   )},
            {"Mv"  , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_V   )},
            {"Mw"  , new ud_itab_entry_operand(ud_operand_code.OP_M    , ud_operand_size.SZ_W   )},
            {"MwRd", new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_WD  )},
            {"MwRv", new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_WV  )},
            {"MwRy", new ud_itab_entry_operand(ud_operand_code.OP_MR   , ud_operand_size.SZ_WY  )},
            {"MwU" , new ud_itab_entry_operand(ud_operand_code.OP_MU   , ud_operand_size.SZ_WO  )},
            {"N"   , new ud_itab_entry_operand(ud_operand_code.OP_N    , ud_operand_size.SZ_Q   )},
            {"NONE", new ud_itab_entry_operand(ud_operand_code.OP_NONE , ud_operand_size.SZ_NA  )},
            {"Ob"  , new ud_itab_entry_operand(ud_operand_code.OP_O    , ud_operand_size.SZ_B   )},
            {"Ov"  , new ud_itab_entry_operand(ud_operand_code.OP_O    , ud_operand_size.SZ_V   )},
            {"Ow"  , new ud_itab_entry_operand(ud_operand_code.OP_O    , ud_operand_size.SZ_W   )},
            {"P"   , new ud_itab_entry_operand(ud_operand_code.OP_P    , ud_operand_size.SZ_Q   )},
            {"Q"   , new ud_itab_entry_operand(ud_operand_code.OP_Q    , ud_operand_size.SZ_Q   )},
            {"R"   , new ud_itab_entry_operand(ud_operand_code.OP_R    , ud_operand_size.SZ_RDQ )},
            {"R0b" , new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_B   )},
            {"R0v" , new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_V   )},
            {"R0w" , new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_W   )},
            {"R0y" , new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_Y   )},
            {"R0z" , new ud_itab_entry_operand(ud_operand_code.OP_R0   , ud_operand_size.SZ_Z   )},
            {"R1b" , new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_B   )},
            {"R1v" , new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_V   )},
            {"R1w" , new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_W   )},
            {"R1y" , new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_Y   )},
            {"R1z" , new ud_itab_entry_operand(ud_operand_code.OP_R1   , ud_operand_size.SZ_Z   )},
            {"R2b" , new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_B   )},
            {"R2v" , new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_V   )},
            {"R2w" , new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_W   )},
            {"R2y" , new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_Y   )},
            {"R2z" , new ud_itab_entry_operand(ud_operand_code.OP_R2   , ud_operand_size.SZ_Z   )},
            {"R3b" , new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_B   )},
            {"R3v" , new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_V   )},
            {"R3w" , new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_W   )},
            {"R3y" , new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_Y   )},
            {"R3z" , new ud_itab_entry_operand(ud_operand_code.OP_R3   , ud_operand_size.SZ_Z   )},
            {"R4b" , new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_B   )},
            {"R4v" , new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_V   )},
            {"R4w" , new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_W   )},
            {"R4y" , new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_Y   )},
            {"R4z" , new ud_itab_entry_operand(ud_operand_code.OP_R4   , ud_operand_size.SZ_Z   )},
            {"R5b" , new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_B   )},
            {"R5v" , new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_V   )},
            {"R5w" , new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_W   )},
            {"R5y" , new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_Y   )},
            {"R5z" , new ud_itab_entry_operand(ud_operand_code.OP_R5   , ud_operand_size.SZ_Z   )},
            {"R6b" , new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_B   )},
            {"R6v" , new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_V   )},
            {"R6w" , new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_W   )},
            {"R6y" , new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_Y   )},
            {"R6z" , new ud_itab_entry_operand(ud_operand_code.OP_R6   , ud_operand_size.SZ_Z   )},
            {"R7b" , new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_B   )},
            {"R7v" , new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_V   )},
            {"R7w" , new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_W   )},
            {"R7y" , new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_Y   )},
            {"R7z" , new ud_itab_entry_operand(ud_operand_code.OP_R7   , ud_operand_size.SZ_Z   )},
            {"rAX" , new ud_itab_entry_operand(ud_operand_code.OP_rAX  , ud_operand_size.SZ_V   )},
            {"rCX" , new ud_itab_entry_operand(ud_operand_code.OP_rCX  , ud_operand_size.SZ_V   )},
            {"rDX" , new ud_itab_entry_operand(ud_operand_code.OP_rDX  , ud_operand_size.SZ_V   )},
            {"S"   , new ud_itab_entry_operand(ud_operand_code.OP_S    , ud_operand_size.SZ_W   )},
            {"sIb" , new ud_itab_entry_operand(ud_operand_code.OP_sI   , ud_operand_size.SZ_B   )},
            {"sIv" , new ud_itab_entry_operand(ud_operand_code.OP_sI   , ud_operand_size.SZ_V   )},
            {"sIz" , new ud_itab_entry_operand(ud_operand_code.OP_sI   , ud_operand_size.SZ_Z   )},
            {"SS"  , new ud_itab_entry_operand(ud_operand_code.OP_SS   , ud_operand_size.SZ_NA  )},
            {"ST0" , new ud_itab_entry_operand(ud_operand_code.OP_ST0  , ud_operand_size.SZ_NA  )},
            {"ST1" , new ud_itab_entry_operand(ud_operand_code.OP_ST1  , ud_operand_size.SZ_NA  )},
            {"ST2" , new ud_itab_entry_operand(ud_operand_code.OP_ST2  , ud_operand_size.SZ_NA  )},
            {"ST3" , new ud_itab_entry_operand(ud_operand_code.OP_ST3  , ud_operand_size.SZ_NA  )},
            {"ST4" , new ud_itab_entry_operand(ud_operand_code.OP_ST4  , ud_operand_size.SZ_NA  )},
            {"ST5" , new ud_itab_entry_operand(ud_operand_code.OP_ST5  , ud_operand_size.SZ_NA  )},
            {"ST6" , new ud_itab_entry_operand(ud_operand_code.OP_ST6  , ud_operand_size.SZ_NA  )},
            {"ST7" , new ud_itab_entry_operand(ud_operand_code.OP_ST7  , ud_operand_size.SZ_NA  )},
            {"U"   , new ud_itab_entry_operand(ud_operand_code.OP_U    , ud_operand_size.SZ_O   )},
            {"Ux"  , new ud_itab_entry_operand(ud_operand_code.OP_U    , ud_operand_size.SZ_X   )},
            {"V"   , new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_DQ  )},
            {"Vdq" , new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_DQ  )},
            {"Vqq" , new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_QQ  )},
            {"Vsd" , new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_Q   )},
            {"Vx"  , new ud_itab_entry_operand(ud_operand_code.OP_V    , ud_operand_size.SZ_X   )},
            {"W"   , new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_DQ  )},
            {"Wdq" , new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_DQ  )},
            {"Wqq" , new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_QQ  )},
            {"Wsd" , new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_Q   )},
            {"Wx"  , new ud_itab_entry_operand(ud_operand_code.OP_W    , ud_operand_size.SZ_X   )},
        };
        #endregion

        #region Instruction Table and Mnemonics
        internal static readonly List<ud_itab_entry> ud_itab_entrys = new List<ud_itab_entry>()
        {
            /* 0000 */ new ud_itab_entry( "invalid" ),
            /* 0001 */ new ud_itab_entry( "aaa" ),
            /* 0002 */ new ud_itab_entry( "aad", "Ib" ),
            /* 0003 */ new ud_itab_entry( "aam", "Ib" ),
            /* 0004 */ new ud_itab_entry( "aas" ),
            /* 0005 */ new ud_itab_entry( "adc", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0006 */ new ud_itab_entry( "adc", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0007 */ new ud_itab_entry( "adc", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0008 */ new ud_itab_entry( "adc", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0009 */ new ud_itab_entry( "adc", "AL;Ib" ),
            /* 0010 */ new ud_itab_entry( "adc", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 0011 */ new ud_itab_entry( "adc", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0012 */ new ud_itab_entry( "adc", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_inv64 ),
            /* 0013 */ new ud_itab_entry( "adc", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0014 */ new ud_itab_entry( "adc", "Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0015 */ new ud_itab_entry( "add", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0016 */ new ud_itab_entry( "add", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0017 */ new ud_itab_entry( "add", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0018 */ new ud_itab_entry( "add", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0019 */ new ud_itab_entry( "add", "AL;Ib" ),
            /* 0020 */ new ud_itab_entry( "add", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 0021 */ new ud_itab_entry( "add", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0022 */ new ud_itab_entry( "add", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_inv64 ),
            /* 0023 */ new ud_itab_entry( "add", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0024 */ new ud_itab_entry( "add", "Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0025 */ new ud_itab_entry( "addpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0026 */ new ud_itab_entry( "vaddpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0027 */ new ud_itab_entry( "addps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0028 */ new ud_itab_entry( "vaddps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0029 */ new ud_itab_entry( "addsd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0030 */ new ud_itab_entry( "vaddsd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0031 */ new ud_itab_entry( "addss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0032 */ new ud_itab_entry( "vaddss", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0033 */ new ud_itab_entry( "addsubpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0034 */ new ud_itab_entry( "vaddsubpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0035 */ new ud_itab_entry( "addsubps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0036 */ new ud_itab_entry( "vaddsubps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0037 */ new ud_itab_entry( "aesdec", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0038 */ new ud_itab_entry( "vaesdec", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0039 */ new ud_itab_entry( "aesdeclast", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0040 */ new ud_itab_entry( "vaesdeclast", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0041 */ new ud_itab_entry( "aesenc", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0042 */ new ud_itab_entry( "vaesenc", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0043 */ new ud_itab_entry( "aesenclast", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0044 */ new ud_itab_entry( "vaesenclast", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0045 */ new ud_itab_entry( "aesimc", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0046 */ new ud_itab_entry( "vaesimc", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0047 */ new ud_itab_entry( "aeskeygenassist", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0048 */ new ud_itab_entry( "vaeskeygenassist", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0049 */ new ud_itab_entry( "and", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0050 */ new ud_itab_entry( "and", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0051 */ new ud_itab_entry( "and", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0052 */ new ud_itab_entry( "and", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0053 */ new ud_itab_entry( "and", "AL;Ib" ),
            /* 0054 */ new ud_itab_entry( "and", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 0055 */ new ud_itab_entry( "and", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0056 */ new ud_itab_entry( "and", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_inv64 ),
            /* 0057 */ new ud_itab_entry( "and", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0058 */ new ud_itab_entry( "and", "Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0059 */ new ud_itab_entry( "andpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0060 */ new ud_itab_entry( "vandpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0061 */ new ud_itab_entry( "andps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0062 */ new ud_itab_entry( "vandps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0063 */ new ud_itab_entry( "andnpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0064 */ new ud_itab_entry( "vandnpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0065 */ new ud_itab_entry( "andnps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0066 */ new ud_itab_entry( "vandnps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0067 */ new ud_itab_entry( "arpl", "Ew;Gw", BitOps.P_aso ),
            /* 0068 */ new ud_itab_entry( "movsxd", "Gq;Ed", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexr | BitOps.P_rexb ),
            /* 0069 */ new ud_itab_entry( "call", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0070 */ new ud_itab_entry( "call", "Eq", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 0071 */ new ud_itab_entry( "call", "Fv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0072 */ new ud_itab_entry( "call", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0073 */ new ud_itab_entry( "call", "Av", BitOps.P_oso ),
            /* 0074 */ new ud_itab_entry( "cbw", BitOps.P_oso | BitOps.P_rexw ),
            /* 0075 */ new ud_itab_entry( "cwde", BitOps.P_oso | BitOps.P_rexw ),
            /* 0076 */ new ud_itab_entry( "cdqe", BitOps.P_oso | BitOps.P_rexw ),
            /* 0077 */ new ud_itab_entry( "clc" ),
            /* 0078 */ new ud_itab_entry( "cld" ),
            /* 0079 */ new ud_itab_entry( "clflush", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0080 */ new ud_itab_entry( "clgi" ),
            /* 0081 */ new ud_itab_entry( "cli" ),
            /* 0082 */ new ud_itab_entry( "clts" ),
            /* 0083 */ new ud_itab_entry( "cmc" ),
            /* 0084 */ new ud_itab_entry( "cmovo", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0085 */ new ud_itab_entry( "cmovno", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0086 */ new ud_itab_entry( "cmovb", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0087 */ new ud_itab_entry( "cmovae", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0088 */ new ud_itab_entry( "cmovz", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0089 */ new ud_itab_entry( "cmovnz", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0090 */ new ud_itab_entry( "cmovbe", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0091 */ new ud_itab_entry( "cmova", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0092 */ new ud_itab_entry( "cmovs", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0093 */ new ud_itab_entry( "cmovns", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0094 */ new ud_itab_entry( "cmovp", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0095 */ new ud_itab_entry( "cmovnp", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0096 */ new ud_itab_entry( "cmovl", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0097 */ new ud_itab_entry( "cmovge", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0098 */ new ud_itab_entry( "cmovle", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0099 */ new ud_itab_entry( "cmovg", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0100 */ new ud_itab_entry( "cmp", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0101 */ new ud_itab_entry( "cmp", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0102 */ new ud_itab_entry( "cmp", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0103 */ new ud_itab_entry( "cmp", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0104 */ new ud_itab_entry( "cmp", "AL;Ib" ),
            /* 0105 */ new ud_itab_entry( "cmp", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 0106 */ new ud_itab_entry( "cmp", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0107 */ new ud_itab_entry( "cmp", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_inv64 ),
            /* 0108 */ new ud_itab_entry( "cmp", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0109 */ new ud_itab_entry( "cmp", "Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0110 */ new ud_itab_entry( "cmppd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0111 */ new ud_itab_entry( "vcmppd", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0112 */ new ud_itab_entry( "cmpps", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0113 */ new ud_itab_entry( "vcmpps", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0114 */ new ud_itab_entry( "cmpsb", BitOps.P_strz | BitOps.P_seg ),
            /* 0115 */ new ud_itab_entry( "cmpsw", BitOps.P_strz | BitOps.P_oso | BitOps.P_rexw | BitOps.P_seg ),
            /* 0116 */ new ud_itab_entry( "cmpsd", BitOps.P_strz | BitOps.P_oso | BitOps.P_rexw | BitOps.P_seg ),
            /* 0117 */ new ud_itab_entry( "cmpsd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0118 */ new ud_itab_entry( "vcmpsd", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0119 */ new ud_itab_entry( "cmpsq", BitOps.P_strz | BitOps.P_oso | BitOps.P_rexw | BitOps.P_seg ),
            /* 0120 */ new ud_itab_entry( "cmpss", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0121 */ new ud_itab_entry( "vcmpss", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0122 */ new ud_itab_entry( "cmpxchg", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0123 */ new ud_itab_entry( "cmpxchg", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0124 */ new ud_itab_entry( "cmpxchg8b", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0125 */ new ud_itab_entry( "cmpxchg8b", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0126 */ new ud_itab_entry( "cmpxchg16b", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0127 */ new ud_itab_entry( "comisd", "Vsd;Wsd", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0128 */ new ud_itab_entry( "vcomisd", "Vsd;Wsd", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0129 */ new ud_itab_entry( "comiss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0130 */ new ud_itab_entry( "vcomiss", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0131 */ new ud_itab_entry( "cpuid" ),
            /* 0132 */ new ud_itab_entry( "cvtdq2pd", "V;Wdq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0133 */ new ud_itab_entry( "vcvtdq2pd", "Vx;Wdq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0134 */ new ud_itab_entry( "cvtdq2ps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0135 */ new ud_itab_entry( "vcvtdq2ps", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0136 */ new ud_itab_entry( "cvtpd2dq", "Vdq;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0137 */ new ud_itab_entry( "vcvtpd2dq", "Vdq;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0138 */ new ud_itab_entry( "cvtpd2pi", "P;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0139 */ new ud_itab_entry( "cvtpd2ps", "Vdq;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0140 */ new ud_itab_entry( "vcvtpd2ps", "Vdq;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0141 */ new ud_itab_entry( "cvtpi2ps", "V;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0142 */ new ud_itab_entry( "cvtpi2pd", "V;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0143 */ new ud_itab_entry( "cvtps2dq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0144 */ new ud_itab_entry( "vcvtps2dq", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0145 */ new ud_itab_entry( "cvtps2pd", "V;Wdq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0146 */ new ud_itab_entry( "vcvtps2pd", "Vx;Wdq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0147 */ new ud_itab_entry( "cvtps2pi", "P;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0148 */ new ud_itab_entry( "cvtsd2si", "Gy;MqU", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0149 */ new ud_itab_entry( "vcvtsd2si", "Gy;MqU", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0150 */ new ud_itab_entry( "cvtsd2ss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0151 */ new ud_itab_entry( "vcvtsd2ss", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0152 */ new ud_itab_entry( "cvtsi2sd", "V;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0153 */ new ud_itab_entry( "vcvtsi2sd", "Vx;Hx;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0154 */ new ud_itab_entry( "cvtsi2ss", "V;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0155 */ new ud_itab_entry( "vcvtsi2ss", "Vx;Hx;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0156 */ new ud_itab_entry( "cvtss2sd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0157 */ new ud_itab_entry( "vcvtss2sd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0158 */ new ud_itab_entry( "cvtss2si", "Gy;MdU", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0159 */ new ud_itab_entry( "vcvtss2si", "Gy;MdU", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0160 */ new ud_itab_entry( "cvttpd2dq", "Vdq;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0161 */ new ud_itab_entry( "vcvttpd2dq", "Vdq;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0162 */ new ud_itab_entry( "cvttpd2pi", "P;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0163 */ new ud_itab_entry( "cvttps2dq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0164 */ new ud_itab_entry( "vcvttps2dq", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0165 */ new ud_itab_entry( "cvttps2pi", "P;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0166 */ new ud_itab_entry( "cvttsd2si", "Gy;MqU", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0167 */ new ud_itab_entry( "vcvttsd2si", "Gy;MqU", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0168 */ new ud_itab_entry( "cvttss2si", "Gy;MdU", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0169 */ new ud_itab_entry( "vcvttss2si", "Gy;MdU", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0170 */ new ud_itab_entry( "cwd", BitOps.P_oso | BitOps.P_rexw ),
            /* 0171 */ new ud_itab_entry( "cdq", BitOps.P_oso | BitOps.P_rexw ),
            /* 0172 */ new ud_itab_entry( "cqo", BitOps.P_oso | BitOps.P_rexw ),
            /* 0173 */ new ud_itab_entry( "daa", BitOps.P_inv64 ),
            /* 0174 */ new ud_itab_entry( "das", BitOps.P_inv64 ),
            /* 0175 */ new ud_itab_entry( "dec", "R0z", BitOps.P_oso ),
            /* 0176 */ new ud_itab_entry( "dec", "R1z", BitOps.P_oso ),
            /* 0177 */ new ud_itab_entry( "dec", "R2z", BitOps.P_oso ),
            /* 0178 */ new ud_itab_entry( "dec", "R3z", BitOps.P_oso ),
            /* 0179 */ new ud_itab_entry( "dec", "R4z", BitOps.P_oso ),
            /* 0180 */ new ud_itab_entry( "dec", "R5z", BitOps.P_oso ),
            /* 0181 */ new ud_itab_entry( "dec", "R6z", BitOps.P_oso ),
            /* 0182 */ new ud_itab_entry( "dec", "R7z", BitOps.P_oso ),
            /* 0183 */ new ud_itab_entry( "dec", "Eb", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0184 */ new ud_itab_entry( "dec", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0185 */ new ud_itab_entry( "div", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0186 */ new ud_itab_entry( "div", "Eb", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0187 */ new ud_itab_entry( "divpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0188 */ new ud_itab_entry( "vdivpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0189 */ new ud_itab_entry( "divps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0190 */ new ud_itab_entry( "vdivps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0191 */ new ud_itab_entry( "divsd", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0192 */ new ud_itab_entry( "vdivsd", "Vx;Hx;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0193 */ new ud_itab_entry( "divss", "V;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0194 */ new ud_itab_entry( "vdivss", "Vx;Hx;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0195 */ new ud_itab_entry( "dppd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0196 */ new ud_itab_entry( "vdppd", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0197 */ new ud_itab_entry( "dpps", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0198 */ new ud_itab_entry( "vdpps", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0199 */ new ud_itab_entry( "emms" ),
            /* 0200 */ new ud_itab_entry( "enter", "Iw;Ib", BitOps.P_def64 ),
            /* 0201 */ new ud_itab_entry( "extractps", "MdRy;V;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0202 */ new ud_itab_entry( "vextractps", "MdRy;Vx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0203 */ new ud_itab_entry( "f2xm1" ),
            /* 0204 */ new ud_itab_entry( "fabs" ),
            /* 0205 */ new ud_itab_entry( "fadd", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0206 */ new ud_itab_entry( "fadd", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0207 */ new ud_itab_entry( "fadd", "ST0;ST0" ),
            /* 0208 */ new ud_itab_entry( "fadd", "ST1;ST0" ),
            /* 0209 */ new ud_itab_entry( "fadd", "ST2;ST0" ),
            /* 0210 */ new ud_itab_entry( "fadd", "ST3;ST0" ),
            /* 0211 */ new ud_itab_entry( "fadd", "ST4;ST0" ),
            /* 0212 */ new ud_itab_entry( "fadd", "ST5;ST0" ),
            /* 0213 */ new ud_itab_entry( "fadd", "ST6;ST0" ),
            /* 0214 */ new ud_itab_entry( "fadd", "ST7;ST0" ),
            /* 0215 */ new ud_itab_entry( "fadd", "ST0;ST0" ),
            /* 0216 */ new ud_itab_entry( "fadd", "ST0;ST1" ),
            /* 0217 */ new ud_itab_entry( "fadd", "ST0;ST2" ),
            /* 0218 */ new ud_itab_entry( "fadd", "ST0;ST3" ),
            /* 0219 */ new ud_itab_entry( "fadd", "ST0;ST4" ),
            /* 0220 */ new ud_itab_entry( "fadd", "ST0;ST5" ),
            /* 0221 */ new ud_itab_entry( "fadd", "ST0;ST6" ),
            /* 0222 */ new ud_itab_entry( "fadd", "ST0;ST7" ),
            /* 0223 */ new ud_itab_entry( "faddp", "ST0;ST0" ),
            /* 0224 */ new ud_itab_entry( "faddp", "ST1;ST0" ),
            /* 0225 */ new ud_itab_entry( "faddp", "ST2;ST0" ),
            /* 0226 */ new ud_itab_entry( "faddp", "ST3;ST0" ),
            /* 0227 */ new ud_itab_entry( "faddp", "ST4;ST0" ),
            /* 0228 */ new ud_itab_entry( "faddp", "ST5;ST0" ),
            /* 0229 */ new ud_itab_entry( "faddp", "ST6;ST0" ),
            /* 0230 */ new ud_itab_entry( "faddp", "ST7;ST0" ),
            /* 0231 */ new ud_itab_entry( "fbld", "Mt", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0232 */ new ud_itab_entry( "fbstp", "Mt", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0233 */ new ud_itab_entry( "fchs" ),
            /* 0234 */ new ud_itab_entry( "fclex" ),
            /* 0235 */ new ud_itab_entry( "fcmovb", "ST0;ST0" ),
            /* 0236 */ new ud_itab_entry( "fcmovb", "ST0;ST1" ),
            /* 0237 */ new ud_itab_entry( "fcmovb", "ST0;ST2" ),
            /* 0238 */ new ud_itab_entry( "fcmovb", "ST0;ST3" ),
            /* 0239 */ new ud_itab_entry( "fcmovb", "ST0;ST4" ),
            /* 0240 */ new ud_itab_entry( "fcmovb", "ST0;ST5" ),
            /* 0241 */ new ud_itab_entry( "fcmovb", "ST0;ST6" ),
            /* 0242 */ new ud_itab_entry( "fcmovb", "ST0;ST7" ),
            /* 0243 */ new ud_itab_entry( "fcmove", "ST0;ST0" ),
            /* 0244 */ new ud_itab_entry( "fcmove", "ST0;ST1" ),
            /* 0245 */ new ud_itab_entry( "fcmove", "ST0;ST2" ),
            /* 0246 */ new ud_itab_entry( "fcmove", "ST0;ST3" ),
            /* 0247 */ new ud_itab_entry( "fcmove", "ST0;ST4" ),
            /* 0248 */ new ud_itab_entry( "fcmove", "ST0;ST5" ),
            /* 0249 */ new ud_itab_entry( "fcmove", "ST0;ST6" ),
            /* 0250 */ new ud_itab_entry( "fcmove", "ST0;ST7" ),
            /* 0251 */ new ud_itab_entry( "fcmovbe", "ST0;ST0" ),
            /* 0252 */ new ud_itab_entry( "fcmovbe", "ST0;ST1" ),
            /* 0253 */ new ud_itab_entry( "fcmovbe", "ST0;ST2" ),
            /* 0254 */ new ud_itab_entry( "fcmovbe", "ST0;ST3" ),
            /* 0255 */ new ud_itab_entry( "fcmovbe", "ST0;ST4" ),
            /* 0256 */ new ud_itab_entry( "fcmovbe", "ST0;ST5" ),
            /* 0257 */ new ud_itab_entry( "fcmovbe", "ST0;ST6" ),
            /* 0258 */ new ud_itab_entry( "fcmovbe", "ST0;ST7" ),
            /* 0259 */ new ud_itab_entry( "fcmovu", "ST0;ST0" ),
            /* 0260 */ new ud_itab_entry( "fcmovu", "ST0;ST1" ),
            /* 0261 */ new ud_itab_entry( "fcmovu", "ST0;ST2" ),
            /* 0262 */ new ud_itab_entry( "fcmovu", "ST0;ST3" ),
            /* 0263 */ new ud_itab_entry( "fcmovu", "ST0;ST4" ),
            /* 0264 */ new ud_itab_entry( "fcmovu", "ST0;ST5" ),
            /* 0265 */ new ud_itab_entry( "fcmovu", "ST0;ST6" ),
            /* 0266 */ new ud_itab_entry( "fcmovu", "ST0;ST7" ),
            /* 0267 */ new ud_itab_entry( "fcmovnb", "ST0;ST0" ),
            /* 0268 */ new ud_itab_entry( "fcmovnb", "ST0;ST1" ),
            /* 0269 */ new ud_itab_entry( "fcmovnb", "ST0;ST2" ),
            /* 0270 */ new ud_itab_entry( "fcmovnb", "ST0;ST3" ),
            /* 0271 */ new ud_itab_entry( "fcmovnb", "ST0;ST4" ),
            /* 0272 */ new ud_itab_entry( "fcmovnb", "ST0;ST5" ),
            /* 0273 */ new ud_itab_entry( "fcmovnb", "ST0;ST6" ),
            /* 0274 */ new ud_itab_entry( "fcmovnb", "ST0;ST7" ),
            /* 0275 */ new ud_itab_entry( "fcmovne", "ST0;ST0" ),
            /* 0276 */ new ud_itab_entry( "fcmovne", "ST0;ST1" ),
            /* 0277 */ new ud_itab_entry( "fcmovne", "ST0;ST2" ),
            /* 0278 */ new ud_itab_entry( "fcmovne", "ST0;ST3" ),
            /* 0279 */ new ud_itab_entry( "fcmovne", "ST0;ST4" ),
            /* 0280 */ new ud_itab_entry( "fcmovne", "ST0;ST5" ),
            /* 0281 */ new ud_itab_entry( "fcmovne", "ST0;ST6" ),
            /* 0282 */ new ud_itab_entry( "fcmovne", "ST0;ST7" ),
            /* 0283 */ new ud_itab_entry( "fcmovnbe", "ST0;ST0" ),
            /* 0284 */ new ud_itab_entry( "fcmovnbe", "ST0;ST1" ),
            /* 0285 */ new ud_itab_entry( "fcmovnbe", "ST0;ST2" ),
            /* 0286 */ new ud_itab_entry( "fcmovnbe", "ST0;ST3" ),
            /* 0287 */ new ud_itab_entry( "fcmovnbe", "ST0;ST4" ),
            /* 0288 */ new ud_itab_entry( "fcmovnbe", "ST0;ST5" ),
            /* 0289 */ new ud_itab_entry( "fcmovnbe", "ST0;ST6" ),
            /* 0290 */ new ud_itab_entry( "fcmovnbe", "ST0;ST7" ),
            /* 0291 */ new ud_itab_entry( "fcmovnu", "ST0;ST0" ),
            /* 0292 */ new ud_itab_entry( "fcmovnu", "ST0;ST1" ),
            /* 0293 */ new ud_itab_entry( "fcmovnu", "ST0;ST2" ),
            /* 0294 */ new ud_itab_entry( "fcmovnu", "ST0;ST3" ),
            /* 0295 */ new ud_itab_entry( "fcmovnu", "ST0;ST4" ),
            /* 0296 */ new ud_itab_entry( "fcmovnu", "ST0;ST5" ),
            /* 0297 */ new ud_itab_entry( "fcmovnu", "ST0;ST6" ),
            /* 0298 */ new ud_itab_entry( "fcmovnu", "ST0;ST7" ),
            /* 0299 */ new ud_itab_entry( "fucomi", "ST0;ST0" ),
            /* 0300 */ new ud_itab_entry( "fucomi", "ST0;ST1" ),
            /* 0301 */ new ud_itab_entry( "fucomi", "ST0;ST2" ),
            /* 0302 */ new ud_itab_entry( "fucomi", "ST0;ST3" ),
            /* 0303 */ new ud_itab_entry( "fucomi", "ST0;ST4" ),
            /* 0304 */ new ud_itab_entry( "fucomi", "ST0;ST5" ),
            /* 0305 */ new ud_itab_entry( "fucomi", "ST0;ST6" ),
            /* 0306 */ new ud_itab_entry( "fucomi", "ST0;ST7" ),
            /* 0307 */ new ud_itab_entry( "fcom", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0308 */ new ud_itab_entry( "fcom", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0309 */ new ud_itab_entry( "fcom", "ST0;ST0" ),
            /* 0310 */ new ud_itab_entry( "fcom", "ST0;ST1" ),
            /* 0311 */ new ud_itab_entry( "fcom", "ST0;ST2" ),
            /* 0312 */ new ud_itab_entry( "fcom", "ST0;ST3" ),
            /* 0313 */ new ud_itab_entry( "fcom", "ST0;ST4" ),
            /* 0314 */ new ud_itab_entry( "fcom", "ST0;ST5" ),
            /* 0315 */ new ud_itab_entry( "fcom", "ST0;ST6" ),
            /* 0316 */ new ud_itab_entry( "fcom", "ST0;ST7" ),
            /* 0317 */ new ud_itab_entry( "fcom2", "ST0" ),
            /* 0318 */ new ud_itab_entry( "fcom2", "ST1" ),
            /* 0319 */ new ud_itab_entry( "fcom2", "ST2" ),
            /* 0320 */ new ud_itab_entry( "fcom2", "ST3" ),
            /* 0321 */ new ud_itab_entry( "fcom2", "ST4" ),
            /* 0322 */ new ud_itab_entry( "fcom2", "ST5" ),
            /* 0323 */ new ud_itab_entry( "fcom2", "ST6" ),
            /* 0324 */ new ud_itab_entry( "fcom2", "ST7" ),
            /* 0325 */ new ud_itab_entry( "fcomp3", "ST0" ),
            /* 0326 */ new ud_itab_entry( "fcomp3", "ST1" ),
            /* 0327 */ new ud_itab_entry( "fcomp3", "ST2" ),
            /* 0328 */ new ud_itab_entry( "fcomp3", "ST3" ),
            /* 0329 */ new ud_itab_entry( "fcomp3", "ST4" ),
            /* 0330 */ new ud_itab_entry( "fcomp3", "ST5" ),
            /* 0331 */ new ud_itab_entry( "fcomp3", "ST6" ),
            /* 0332 */ new ud_itab_entry( "fcomp3", "ST7" ),
            /* 0333 */ new ud_itab_entry( "fcomi", "ST0;ST0" ),
            /* 0334 */ new ud_itab_entry( "fcomi", "ST0;ST1" ),
            /* 0335 */ new ud_itab_entry( "fcomi", "ST0;ST2" ),
            /* 0336 */ new ud_itab_entry( "fcomi", "ST0;ST3" ),
            /* 0337 */ new ud_itab_entry( "fcomi", "ST0;ST4" ),
            /* 0338 */ new ud_itab_entry( "fcomi", "ST0;ST5" ),
            /* 0339 */ new ud_itab_entry( "fcomi", "ST0;ST6" ),
            /* 0340 */ new ud_itab_entry( "fcomi", "ST0;ST7" ),
            /* 0341 */ new ud_itab_entry( "fucomip", "ST0;ST0" ),
            /* 0342 */ new ud_itab_entry( "fucomip", "ST0;ST1" ),
            /* 0343 */ new ud_itab_entry( "fucomip", "ST0;ST2" ),
            /* 0344 */ new ud_itab_entry( "fucomip", "ST0;ST3" ),
            /* 0345 */ new ud_itab_entry( "fucomip", "ST0;ST4" ),
            /* 0346 */ new ud_itab_entry( "fucomip", "ST0;ST5" ),
            /* 0347 */ new ud_itab_entry( "fucomip", "ST0;ST6" ),
            /* 0348 */ new ud_itab_entry( "fucomip", "ST0;ST7" ),
            /* 0349 */ new ud_itab_entry( "fcomip", "ST0;ST0" ),
            /* 0350 */ new ud_itab_entry( "fcomip", "ST0;ST1" ),
            /* 0351 */ new ud_itab_entry( "fcomip", "ST0;ST2" ),
            /* 0352 */ new ud_itab_entry( "fcomip", "ST0;ST3" ),
            /* 0353 */ new ud_itab_entry( "fcomip", "ST0;ST4" ),
            /* 0354 */ new ud_itab_entry( "fcomip", "ST0;ST5" ),
            /* 0355 */ new ud_itab_entry( "fcomip", "ST0;ST6" ),
            /* 0356 */ new ud_itab_entry( "fcomip", "ST0;ST7" ),
            /* 0357 */ new ud_itab_entry( "fcomp", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0358 */ new ud_itab_entry( "fcomp", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0359 */ new ud_itab_entry( "fcomp", "ST0;ST0" ),
            /* 0360 */ new ud_itab_entry( "fcomp", "ST0;ST1" ),
            /* 0361 */ new ud_itab_entry( "fcomp", "ST0;ST2" ),
            /* 0362 */ new ud_itab_entry( "fcomp", "ST0;ST3" ),
            /* 0363 */ new ud_itab_entry( "fcomp", "ST0;ST4" ),
            /* 0364 */ new ud_itab_entry( "fcomp", "ST0;ST5" ),
            /* 0365 */ new ud_itab_entry( "fcomp", "ST0;ST6" ),
            /* 0366 */ new ud_itab_entry( "fcomp", "ST0;ST7" ),
            /* 0367 */ new ud_itab_entry( "fcomp5", "ST0" ),
            /* 0368 */ new ud_itab_entry( "fcomp5", "ST1" ),
            /* 0369 */ new ud_itab_entry( "fcomp5", "ST2" ),
            /* 0370 */ new ud_itab_entry( "fcomp5", "ST3" ),
            /* 0371 */ new ud_itab_entry( "fcomp5", "ST4" ),
            /* 0372 */ new ud_itab_entry( "fcomp5", "ST5" ),
            /* 0373 */ new ud_itab_entry( "fcomp5", "ST6" ),
            /* 0374 */ new ud_itab_entry( "fcomp5", "ST7" ),
            /* 0375 */ new ud_itab_entry( "fcompp" ),
            /* 0376 */ new ud_itab_entry( "fcos" ),
            /* 0377 */ new ud_itab_entry( "fdecstp" ),
            /* 0378 */ new ud_itab_entry( "fdiv", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0379 */ new ud_itab_entry( "fdiv", "ST0;ST0" ),
            /* 0380 */ new ud_itab_entry( "fdiv", "ST1;ST0" ),
            /* 0381 */ new ud_itab_entry( "fdiv", "ST2;ST0" ),
            /* 0382 */ new ud_itab_entry( "fdiv", "ST3;ST0" ),
            /* 0383 */ new ud_itab_entry( "fdiv", "ST4;ST0" ),
            /* 0384 */ new ud_itab_entry( "fdiv", "ST5;ST0" ),
            /* 0385 */ new ud_itab_entry( "fdiv", "ST6;ST0" ),
            /* 0386 */ new ud_itab_entry( "fdiv", "ST7;ST0" ),
            /* 0387 */ new ud_itab_entry( "fdiv", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0388 */ new ud_itab_entry( "fdiv", "ST0;ST0" ),
            /* 0389 */ new ud_itab_entry( "fdiv", "ST0;ST1" ),
            /* 0390 */ new ud_itab_entry( "fdiv", "ST0;ST2" ),
            /* 0391 */ new ud_itab_entry( "fdiv", "ST0;ST3" ),
            /* 0392 */ new ud_itab_entry( "fdiv", "ST0;ST4" ),
            /* 0393 */ new ud_itab_entry( "fdiv", "ST0;ST5" ),
            /* 0394 */ new ud_itab_entry( "fdiv", "ST0;ST6" ),
            /* 0395 */ new ud_itab_entry( "fdiv", "ST0;ST7" ),
            /* 0396 */ new ud_itab_entry( "fdivp", "ST0;ST0" ),
            /* 0397 */ new ud_itab_entry( "fdivp", "ST1;ST0" ),
            /* 0398 */ new ud_itab_entry( "fdivp", "ST2;ST0" ),
            /* 0399 */ new ud_itab_entry( "fdivp", "ST3;ST0" ),
            /* 0400 */ new ud_itab_entry( "fdivp", "ST4;ST0" ),
            /* 0401 */ new ud_itab_entry( "fdivp", "ST5;ST0" ),
            /* 0402 */ new ud_itab_entry( "fdivp", "ST6;ST0" ),
            /* 0403 */ new ud_itab_entry( "fdivp", "ST7;ST0" ),
            /* 0404 */ new ud_itab_entry( "fdivr", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0405 */ new ud_itab_entry( "fdivr", "ST0;ST0" ),
            /* 0406 */ new ud_itab_entry( "fdivr", "ST1;ST0" ),
            /* 0407 */ new ud_itab_entry( "fdivr", "ST2;ST0" ),
            /* 0408 */ new ud_itab_entry( "fdivr", "ST3;ST0" ),
            /* 0409 */ new ud_itab_entry( "fdivr", "ST4;ST0" ),
            /* 0410 */ new ud_itab_entry( "fdivr", "ST5;ST0" ),
            /* 0411 */ new ud_itab_entry( "fdivr", "ST6;ST0" ),
            /* 0412 */ new ud_itab_entry( "fdivr", "ST7;ST0" ),
            /* 0413 */ new ud_itab_entry( "fdivr", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0414 */ new ud_itab_entry( "fdivr", "ST0;ST0" ),
            /* 0415 */ new ud_itab_entry( "fdivr", "ST0;ST1" ),
            /* 0416 */ new ud_itab_entry( "fdivr", "ST0;ST2" ),
            /* 0417 */ new ud_itab_entry( "fdivr", "ST0;ST3" ),
            /* 0418 */ new ud_itab_entry( "fdivr", "ST0;ST4" ),
            /* 0419 */ new ud_itab_entry( "fdivr", "ST0;ST5" ),
            /* 0420 */ new ud_itab_entry( "fdivr", "ST0;ST6" ),
            /* 0421 */ new ud_itab_entry( "fdivr", "ST0;ST7" ),
            /* 0422 */ new ud_itab_entry( "fdivrp", "ST0;ST0" ),
            /* 0423 */ new ud_itab_entry( "fdivrp", "ST1;ST0" ),
            /* 0424 */ new ud_itab_entry( "fdivrp", "ST2;ST0" ),
            /* 0425 */ new ud_itab_entry( "fdivrp", "ST3;ST0" ),
            /* 0426 */ new ud_itab_entry( "fdivrp", "ST4;ST0" ),
            /* 0427 */ new ud_itab_entry( "fdivrp", "ST5;ST0" ),
            /* 0428 */ new ud_itab_entry( "fdivrp", "ST6;ST0" ),
            /* 0429 */ new ud_itab_entry( "fdivrp", "ST7;ST0" ),
            /* 0430 */ new ud_itab_entry( "femms" ),
            /* 0431 */ new ud_itab_entry( "ffree", "ST0" ),
            /* 0432 */ new ud_itab_entry( "ffree", "ST1" ),
            /* 0433 */ new ud_itab_entry( "ffree", "ST2" ),
            /* 0434 */ new ud_itab_entry( "ffree", "ST3" ),
            /* 0435 */ new ud_itab_entry( "ffree", "ST4" ),
            /* 0436 */ new ud_itab_entry( "ffree", "ST5" ),
            /* 0437 */ new ud_itab_entry( "ffree", "ST6" ),
            /* 0438 */ new ud_itab_entry( "ffree", "ST7" ),
            /* 0439 */ new ud_itab_entry( "ffreep", "ST0" ),
            /* 0440 */ new ud_itab_entry( "ffreep", "ST1" ),
            /* 0441 */ new ud_itab_entry( "ffreep", "ST2" ),
            /* 0442 */ new ud_itab_entry( "ffreep", "ST3" ),
            /* 0443 */ new ud_itab_entry( "ffreep", "ST4" ),
            /* 0444 */ new ud_itab_entry( "ffreep", "ST5" ),
            /* 0445 */ new ud_itab_entry( "ffreep", "ST6" ),
            /* 0446 */ new ud_itab_entry( "ffreep", "ST7" ),
            /* 0447 */ new ud_itab_entry( "ficom", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0448 */ new ud_itab_entry( "ficom", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0449 */ new ud_itab_entry( "ficomp", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0450 */ new ud_itab_entry( "ficomp", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0451 */ new ud_itab_entry( "fild", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0452 */ new ud_itab_entry( "fild", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0453 */ new ud_itab_entry( "fild", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0454 */ new ud_itab_entry( "fincstp" ),
            /* 0455 */ new ud_itab_entry( "fninit" ),
            /* 0456 */ new ud_itab_entry( "fiadd", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0457 */ new ud_itab_entry( "fiadd", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0458 */ new ud_itab_entry( "fidivr", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0459 */ new ud_itab_entry( "fidivr", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0460 */ new ud_itab_entry( "fidiv", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0461 */ new ud_itab_entry( "fidiv", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0462 */ new ud_itab_entry( "fisub", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0463 */ new ud_itab_entry( "fisub", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0464 */ new ud_itab_entry( "fisubr", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0465 */ new ud_itab_entry( "fisubr", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0466 */ new ud_itab_entry( "fist", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0467 */ new ud_itab_entry( "fist", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0468 */ new ud_itab_entry( "fistp", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0469 */ new ud_itab_entry( "fistp", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0470 */ new ud_itab_entry( "fistp", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0471 */ new ud_itab_entry( "fisttp", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0472 */ new ud_itab_entry( "fisttp", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0473 */ new ud_itab_entry( "fisttp", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0474 */ new ud_itab_entry( "fld", "Mt", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0475 */ new ud_itab_entry( "fld", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0476 */ new ud_itab_entry( "fld", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0477 */ new ud_itab_entry( "fld", "ST0" ),
            /* 0478 */ new ud_itab_entry( "fld", "ST1" ),
            /* 0479 */ new ud_itab_entry( "fld", "ST2" ),
            /* 0480 */ new ud_itab_entry( "fld", "ST3" ),
            /* 0481 */ new ud_itab_entry( "fld", "ST4" ),
            /* 0482 */ new ud_itab_entry( "fld", "ST5" ),
            /* 0483 */ new ud_itab_entry( "fld", "ST6" ),
            /* 0484 */ new ud_itab_entry( "fld", "ST7" ),
            /* 0485 */ new ud_itab_entry( "fld1" ),
            /* 0486 */ new ud_itab_entry( "fldl2t" ),
            /* 0487 */ new ud_itab_entry( "fldl2e" ),
            /* 0488 */ new ud_itab_entry( "fldpi" ),
            /* 0489 */ new ud_itab_entry( "fldlg2" ),
            /* 0490 */ new ud_itab_entry( "fldln2" ),
            /* 0491 */ new ud_itab_entry( "fldz" ),
            /* 0492 */ new ud_itab_entry( "fldcw", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0493 */ new ud_itab_entry( "fldenv", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0494 */ new ud_itab_entry( "fmul", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0495 */ new ud_itab_entry( "fmul", "ST0;ST0" ),
            /* 0496 */ new ud_itab_entry( "fmul", "ST1;ST0" ),
            /* 0497 */ new ud_itab_entry( "fmul", "ST2;ST0" ),
            /* 0498 */ new ud_itab_entry( "fmul", "ST3;ST0" ),
            /* 0499 */ new ud_itab_entry( "fmul", "ST4;ST0" ),
            /* 0500 */ new ud_itab_entry( "fmul", "ST5;ST0" ),
            /* 0501 */ new ud_itab_entry( "fmul", "ST6;ST0" ),
            /* 0502 */ new ud_itab_entry( "fmul", "ST7;ST0" ),
            /* 0503 */ new ud_itab_entry( "fmul", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0504 */ new ud_itab_entry( "fmul", "ST0;ST0" ),
            /* 0505 */ new ud_itab_entry( "fmul", "ST0;ST1" ),
            /* 0506 */ new ud_itab_entry( "fmul", "ST0;ST2" ),
            /* 0507 */ new ud_itab_entry( "fmul", "ST0;ST3" ),
            /* 0508 */ new ud_itab_entry( "fmul", "ST0;ST4" ),
            /* 0509 */ new ud_itab_entry( "fmul", "ST0;ST5" ),
            /* 0510 */ new ud_itab_entry( "fmul", "ST0;ST6" ),
            /* 0511 */ new ud_itab_entry( "fmul", "ST0;ST7" ),
            /* 0512 */ new ud_itab_entry( "fmulp", "ST0;ST0" ),
            /* 0513 */ new ud_itab_entry( "fmulp", "ST1;ST0" ),
            /* 0514 */ new ud_itab_entry( "fmulp", "ST2;ST0" ),
            /* 0515 */ new ud_itab_entry( "fmulp", "ST3;ST0" ),
            /* 0516 */ new ud_itab_entry( "fmulp", "ST4;ST0" ),
            /* 0517 */ new ud_itab_entry( "fmulp", "ST5;ST0" ),
            /* 0518 */ new ud_itab_entry( "fmulp", "ST6;ST0" ),
            /* 0519 */ new ud_itab_entry( "fmulp", "ST7;ST0" ),
            /* 0520 */ new ud_itab_entry( "fimul", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0521 */ new ud_itab_entry( "fimul", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0522 */ new ud_itab_entry( "fnop" ),
            /* 0523 */ new ud_itab_entry( "fndisi" ),
            /* 0524 */ new ud_itab_entry( "fneni" ),
            /* 0525 */ new ud_itab_entry( "fnsetpm" ),
            /* 0526 */ new ud_itab_entry( "fpatan" ),
            /* 0527 */ new ud_itab_entry( "fprem" ),
            /* 0528 */ new ud_itab_entry( "fprem1" ),
            /* 0529 */ new ud_itab_entry( "fptan" ),
            /* 0530 */ new ud_itab_entry( "frndint" ),
            /* 0531 */ new ud_itab_entry( "frstor", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0532 */ new ud_itab_entry( "frstpm" ),
            /* 0533 */ new ud_itab_entry( "fnsave", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0534 */ new ud_itab_entry( "fscale" ),
            /* 0535 */ new ud_itab_entry( "fsin" ),
            /* 0536 */ new ud_itab_entry( "fsincos" ),
            /* 0537 */ new ud_itab_entry( "fsqrt" ),
            /* 0538 */ new ud_itab_entry( "fstp", "Mt", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0539 */ new ud_itab_entry( "fstp", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0540 */ new ud_itab_entry( "fstp", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0541 */ new ud_itab_entry( "fstp", "ST0" ),
            /* 0542 */ new ud_itab_entry( "fstp", "ST1" ),
            /* 0543 */ new ud_itab_entry( "fstp", "ST2" ),
            /* 0544 */ new ud_itab_entry( "fstp", "ST3" ),
            /* 0545 */ new ud_itab_entry( "fstp", "ST4" ),
            /* 0546 */ new ud_itab_entry( "fstp", "ST5" ),
            /* 0547 */ new ud_itab_entry( "fstp", "ST6" ),
            /* 0548 */ new ud_itab_entry( "fstp", "ST7" ),
            /* 0549 */ new ud_itab_entry( "fstp1", "ST0" ),
            /* 0550 */ new ud_itab_entry( "fstp1", "ST1" ),
            /* 0551 */ new ud_itab_entry( "fstp1", "ST2" ),
            /* 0552 */ new ud_itab_entry( "fstp1", "ST3" ),
            /* 0553 */ new ud_itab_entry( "fstp1", "ST4" ),
            /* 0554 */ new ud_itab_entry( "fstp1", "ST5" ),
            /* 0555 */ new ud_itab_entry( "fstp1", "ST6" ),
            /* 0556 */ new ud_itab_entry( "fstp1", "ST7" ),
            /* 0557 */ new ud_itab_entry( "fstp8", "ST0" ),
            /* 0558 */ new ud_itab_entry( "fstp8", "ST1" ),
            /* 0559 */ new ud_itab_entry( "fstp8", "ST2" ),
            /* 0560 */ new ud_itab_entry( "fstp8", "ST3" ),
            /* 0561 */ new ud_itab_entry( "fstp8", "ST4" ),
            /* 0562 */ new ud_itab_entry( "fstp8", "ST5" ),
            /* 0563 */ new ud_itab_entry( "fstp8", "ST6" ),
            /* 0564 */ new ud_itab_entry( "fstp8", "ST7" ),
            /* 0565 */ new ud_itab_entry( "fstp9", "ST0" ),
            /* 0566 */ new ud_itab_entry( "fstp9", "ST1" ),
            /* 0567 */ new ud_itab_entry( "fstp9", "ST2" ),
            /* 0568 */ new ud_itab_entry( "fstp9", "ST3" ),
            /* 0569 */ new ud_itab_entry( "fstp9", "ST4" ),
            /* 0570 */ new ud_itab_entry( "fstp9", "ST5" ),
            /* 0571 */ new ud_itab_entry( "fstp9", "ST6" ),
            /* 0572 */ new ud_itab_entry( "fstp9", "ST7" ),
            /* 0573 */ new ud_itab_entry( "fst", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0574 */ new ud_itab_entry( "fst", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0575 */ new ud_itab_entry( "fst", "ST0" ),
            /* 0576 */ new ud_itab_entry( "fst", "ST1" ),
            /* 0577 */ new ud_itab_entry( "fst", "ST2" ),
            /* 0578 */ new ud_itab_entry( "fst", "ST3" ),
            /* 0579 */ new ud_itab_entry( "fst", "ST4" ),
            /* 0580 */ new ud_itab_entry( "fst", "ST5" ),
            /* 0581 */ new ud_itab_entry( "fst", "ST6" ),
            /* 0582 */ new ud_itab_entry( "fst", "ST7" ),
            /* 0583 */ new ud_itab_entry( "fnstcw", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0584 */ new ud_itab_entry( "fnstenv", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0585 */ new ud_itab_entry( "fnstsw", "Mw", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0586 */ new ud_itab_entry( "fnstsw", "AX" ),
            /* 0587 */ new ud_itab_entry( "fsub", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0588 */ new ud_itab_entry( "fsub", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0589 */ new ud_itab_entry( "fsub", "ST0;ST0" ),
            /* 0590 */ new ud_itab_entry( "fsub", "ST0;ST1" ),
            /* 0591 */ new ud_itab_entry( "fsub", "ST0;ST2" ),
            /* 0592 */ new ud_itab_entry( "fsub", "ST0;ST3" ),
            /* 0593 */ new ud_itab_entry( "fsub", "ST0;ST4" ),
            /* 0594 */ new ud_itab_entry( "fsub", "ST0;ST5" ),
            /* 0595 */ new ud_itab_entry( "fsub", "ST0;ST6" ),
            /* 0596 */ new ud_itab_entry( "fsub", "ST0;ST7" ),
            /* 0597 */ new ud_itab_entry( "fsub", "ST0;ST0" ),
            /* 0598 */ new ud_itab_entry( "fsub", "ST1;ST0" ),
            /* 0599 */ new ud_itab_entry( "fsub", "ST2;ST0" ),
            /* 0600 */ new ud_itab_entry( "fsub", "ST3;ST0" ),
            /* 0601 */ new ud_itab_entry( "fsub", "ST4;ST0" ),
            /* 0602 */ new ud_itab_entry( "fsub", "ST5;ST0" ),
            /* 0603 */ new ud_itab_entry( "fsub", "ST6;ST0" ),
            /* 0604 */ new ud_itab_entry( "fsub", "ST7;ST0" ),
            /* 0605 */ new ud_itab_entry( "fsubp", "ST0;ST0" ),
            /* 0606 */ new ud_itab_entry( "fsubp", "ST1;ST0" ),
            /* 0607 */ new ud_itab_entry( "fsubp", "ST2;ST0" ),
            /* 0608 */ new ud_itab_entry( "fsubp", "ST3;ST0" ),
            /* 0609 */ new ud_itab_entry( "fsubp", "ST4;ST0" ),
            /* 0610 */ new ud_itab_entry( "fsubp", "ST5;ST0" ),
            /* 0611 */ new ud_itab_entry( "fsubp", "ST6;ST0" ),
            /* 0612 */ new ud_itab_entry( "fsubp", "ST7;ST0" ),
            /* 0613 */ new ud_itab_entry( "fsubr", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0614 */ new ud_itab_entry( "fsubr", "ST0;ST0" ),
            /* 0615 */ new ud_itab_entry( "fsubr", "ST0;ST1" ),
            /* 0616 */ new ud_itab_entry( "fsubr", "ST0;ST2" ),
            /* 0617 */ new ud_itab_entry( "fsubr", "ST0;ST3" ),
            /* 0618 */ new ud_itab_entry( "fsubr", "ST0;ST4" ),
            /* 0619 */ new ud_itab_entry( "fsubr", "ST0;ST5" ),
            /* 0620 */ new ud_itab_entry( "fsubr", "ST0;ST6" ),
            /* 0621 */ new ud_itab_entry( "fsubr", "ST0;ST7" ),
            /* 0622 */ new ud_itab_entry( "fsubr", "ST0;ST0" ),
            /* 0623 */ new ud_itab_entry( "fsubr", "ST1;ST0" ),
            /* 0624 */ new ud_itab_entry( "fsubr", "ST2;ST0" ),
            /* 0625 */ new ud_itab_entry( "fsubr", "ST3;ST0" ),
            /* 0626 */ new ud_itab_entry( "fsubr", "ST4;ST0" ),
            /* 0627 */ new ud_itab_entry( "fsubr", "ST5;ST0" ),
            /* 0628 */ new ud_itab_entry( "fsubr", "ST6;ST0" ),
            /* 0629 */ new ud_itab_entry( "fsubr", "ST7;ST0" ),
            /* 0630 */ new ud_itab_entry( "fsubr", "Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0631 */ new ud_itab_entry( "fsubrp", "ST0;ST0" ),
            /* 0632 */ new ud_itab_entry( "fsubrp", "ST1;ST0" ),
            /* 0633 */ new ud_itab_entry( "fsubrp", "ST2;ST0" ),
            /* 0634 */ new ud_itab_entry( "fsubrp", "ST3;ST0" ),
            /* 0635 */ new ud_itab_entry( "fsubrp", "ST4;ST0" ),
            /* 0636 */ new ud_itab_entry( "fsubrp", "ST5;ST0" ),
            /* 0637 */ new ud_itab_entry( "fsubrp", "ST6;ST0" ),
            /* 0638 */ new ud_itab_entry( "fsubrp", "ST7;ST0" ),
            /* 0639 */ new ud_itab_entry( "ftst" ),
            /* 0640 */ new ud_itab_entry( "fucom", "ST0" ),
            /* 0641 */ new ud_itab_entry( "fucom", "ST1" ),
            /* 0642 */ new ud_itab_entry( "fucom", "ST2" ),
            /* 0643 */ new ud_itab_entry( "fucom", "ST3" ),
            /* 0644 */ new ud_itab_entry( "fucom", "ST4" ),
            /* 0645 */ new ud_itab_entry( "fucom", "ST5" ),
            /* 0646 */ new ud_itab_entry( "fucom", "ST6" ),
            /* 0647 */ new ud_itab_entry( "fucom", "ST7" ),
            /* 0648 */ new ud_itab_entry( "fucomp", "ST0" ),
            /* 0649 */ new ud_itab_entry( "fucomp", "ST1" ),
            /* 0650 */ new ud_itab_entry( "fucomp", "ST2" ),
            /* 0651 */ new ud_itab_entry( "fucomp", "ST3" ),
            /* 0652 */ new ud_itab_entry( "fucomp", "ST4" ),
            /* 0653 */ new ud_itab_entry( "fucomp", "ST5" ),
            /* 0654 */ new ud_itab_entry( "fucomp", "ST6" ),
            /* 0655 */ new ud_itab_entry( "fucomp", "ST7" ),
            /* 0656 */ new ud_itab_entry( "fucompp" ),
            /* 0657 */ new ud_itab_entry( "fxam" ),
            /* 0658 */ new ud_itab_entry( "fxch", "ST0;ST0" ),
            /* 0659 */ new ud_itab_entry( "fxch", "ST0;ST1" ),
            /* 0660 */ new ud_itab_entry( "fxch", "ST0;ST2" ),
            /* 0661 */ new ud_itab_entry( "fxch", "ST0;ST3" ),
            /* 0662 */ new ud_itab_entry( "fxch", "ST0;ST4" ),
            /* 0663 */ new ud_itab_entry( "fxch", "ST0;ST5" ),
            /* 0664 */ new ud_itab_entry( "fxch", "ST0;ST6" ),
            /* 0665 */ new ud_itab_entry( "fxch", "ST0;ST7" ),
            /* 0666 */ new ud_itab_entry( "fxch4", "ST0" ),
            /* 0667 */ new ud_itab_entry( "fxch4", "ST1" ),
            /* 0668 */ new ud_itab_entry( "fxch4", "ST2" ),
            /* 0669 */ new ud_itab_entry( "fxch4", "ST3" ),
            /* 0670 */ new ud_itab_entry( "fxch4", "ST4" ),
            /* 0671 */ new ud_itab_entry( "fxch4", "ST5" ),
            /* 0672 */ new ud_itab_entry( "fxch4", "ST6" ),
            /* 0673 */ new ud_itab_entry( "fxch4", "ST7" ),
            /* 0674 */ new ud_itab_entry( "fxch7", "ST0" ),
            /* 0675 */ new ud_itab_entry( "fxch7", "ST1" ),
            /* 0676 */ new ud_itab_entry( "fxch7", "ST2" ),
            /* 0677 */ new ud_itab_entry( "fxch7", "ST3" ),
            /* 0678 */ new ud_itab_entry( "fxch7", "ST4" ),
            /* 0679 */ new ud_itab_entry( "fxch7", "ST5" ),
            /* 0680 */ new ud_itab_entry( "fxch7", "ST6" ),
            /* 0681 */ new ud_itab_entry( "fxch7", "ST7" ),
            /* 0682 */ new ud_itab_entry( "fxrstor", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0683 */ new ud_itab_entry( "fxsave", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0684 */ new ud_itab_entry( "fxtract" ),
            /* 0685 */ new ud_itab_entry( "fyl2x" ),
            /* 0686 */ new ud_itab_entry( "fyl2xp1" ),
            /* 0687 */ new ud_itab_entry( "hlt" ),
            /* 0688 */ new ud_itab_entry( "idiv", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0689 */ new ud_itab_entry( "idiv", "Eb", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0690 */ new ud_itab_entry( "in", "AL;Ib" ),
            /* 0691 */ new ud_itab_entry( "in", "eAX;Ib", BitOps.P_oso ),
            /* 0692 */ new ud_itab_entry( "in", "AL;DX" ),
            /* 0693 */ new ud_itab_entry( "in", "eAX;DX", BitOps.P_oso ),
            /* 0694 */ new ud_itab_entry( "imul", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0695 */ new ud_itab_entry( "imul", "Eb", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0696 */ new ud_itab_entry( "imul", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0697 */ new ud_itab_entry( "imul", "Gv;Ev;Iz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0698 */ new ud_itab_entry( "imul", "Gv;Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0699 */ new ud_itab_entry( "inc", "R0z", BitOps.P_oso ),
            /* 0700 */ new ud_itab_entry( "inc", "R1z", BitOps.P_oso ),
            /* 0701 */ new ud_itab_entry( "inc", "R2z", BitOps.P_oso ),
            /* 0702 */ new ud_itab_entry( "inc", "R3z", BitOps.P_oso ),
            /* 0703 */ new ud_itab_entry( "inc", "R4z", BitOps.P_oso ),
            /* 0704 */ new ud_itab_entry( "inc", "R5z", BitOps.P_oso ),
            /* 0705 */ new ud_itab_entry( "inc", "R6z", BitOps.P_oso ),
            /* 0706 */ new ud_itab_entry( "inc", "R7z", BitOps.P_oso ),
            /* 0707 */ new ud_itab_entry( "inc", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0708 */ new ud_itab_entry( "inc", "Eb", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0709 */ new ud_itab_entry( "insb", BitOps.P_str | BitOps.P_seg ),
            /* 0710 */ new ud_itab_entry( "insw", BitOps.P_str | BitOps.P_oso | BitOps.P_seg ),
            /* 0711 */ new ud_itab_entry( "insd", BitOps.P_str | BitOps.P_oso | BitOps.P_seg ),
            /* 0712 */ new ud_itab_entry( "int1" ),
            /* 0713 */ new ud_itab_entry( "int3" ),
            /* 0714 */ new ud_itab_entry( "int", "Ib" ),
            /* 0715 */ new ud_itab_entry( "into", BitOps.P_inv64 ),
            /* 0716 */ new ud_itab_entry( "invd" ),
            /* 0717 */ new ud_itab_entry( "invept", "Gd;Mo" ),
            /* 0718 */ new ud_itab_entry( "invept", "Gq;Mo" ),
            /* 0719 */ new ud_itab_entry( "invlpg", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0720 */ new ud_itab_entry( "invlpga" ),
            /* 0721 */ new ud_itab_entry( "invvpid", "Gd;Mo" ),
            /* 0722 */ new ud_itab_entry( "invvpid", "Gq;Mo" ),
            /* 0723 */ new ud_itab_entry( "iretw", BitOps.P_oso | BitOps.P_rexw ),
            /* 0724 */ new ud_itab_entry( "iretd", BitOps.P_oso | BitOps.P_rexw ),
            /* 0725 */ new ud_itab_entry( "iretq", BitOps.P_oso | BitOps.P_rexw ),
            /* 0726 */ new ud_itab_entry( "jo", "Jb", BitOps.P_def64 ),
            /* 0727 */ new ud_itab_entry( "jo", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0728 */ new ud_itab_entry( "jno", "Jb", BitOps.P_def64 ),
            /* 0729 */ new ud_itab_entry( "jno", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0730 */ new ud_itab_entry( "jb", "Jb", BitOps.P_def64 ),
            /* 0731 */ new ud_itab_entry( "jb", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0732 */ new ud_itab_entry( "jae", "Jb", BitOps.P_def64 ),
            /* 0733 */ new ud_itab_entry( "jae", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0734 */ new ud_itab_entry( "jz", "Jb", BitOps.P_def64 ),
            /* 0735 */ new ud_itab_entry( "jz", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0736 */ new ud_itab_entry( "jnz", "Jb", BitOps.P_def64 ),
            /* 0737 */ new ud_itab_entry( "jnz", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0738 */ new ud_itab_entry( "jbe", "Jb", BitOps.P_def64 ),
            /* 0739 */ new ud_itab_entry( "jbe", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0740 */ new ud_itab_entry( "ja", "Jb", BitOps.P_def64 ),
            /* 0741 */ new ud_itab_entry( "ja", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0742 */ new ud_itab_entry( "js", "Jb", BitOps.P_def64 ),
            /* 0743 */ new ud_itab_entry( "js", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0744 */ new ud_itab_entry( "jns", "Jb", BitOps.P_def64 ),
            /* 0745 */ new ud_itab_entry( "jns", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0746 */ new ud_itab_entry( "jp", "Jb", BitOps.P_def64 ),
            /* 0747 */ new ud_itab_entry( "jp", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0748 */ new ud_itab_entry( "jnp", "Jb", BitOps.P_def64 ),
            /* 0749 */ new ud_itab_entry( "jnp", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0750 */ new ud_itab_entry( "jl", "Jb", BitOps.P_def64 ),
            /* 0751 */ new ud_itab_entry( "jl", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0752 */ new ud_itab_entry( "jge", "Jb", BitOps.P_def64 ),
            /* 0753 */ new ud_itab_entry( "jge", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0754 */ new ud_itab_entry( "jle", "Jb", BitOps.P_def64 ),
            /* 0755 */ new ud_itab_entry( "jle", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0756 */ new ud_itab_entry( "jg", "Jb", BitOps.P_def64 ),
            /* 0757 */ new ud_itab_entry( "jg", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0758 */ new ud_itab_entry( "jcxz", "Jb", BitOps.P_aso | BitOps.P_def64 ),
            /* 0759 */ new ud_itab_entry( "jecxz", "Jb", BitOps.P_aso | BitOps.P_def64 ),
            /* 0760 */ new ud_itab_entry( "jrcxz", "Jb", BitOps.P_aso | BitOps.P_def64 ),
            /* 0761 */ new ud_itab_entry( "jmp", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 0762 */ new ud_itab_entry( "jmp", "Fv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0763 */ new ud_itab_entry( "jmp", "Jz", BitOps.P_oso | BitOps.P_def64 ),
            /* 0764 */ new ud_itab_entry( "jmp", "Av", BitOps.P_oso ),
            /* 0765 */ new ud_itab_entry( "jmp", "Jb", BitOps.P_def64 ),
            /* 0766 */ new ud_itab_entry( "lahf" ),
            /* 0767 */ new ud_itab_entry( "lar", "Gv;Ew", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0768 */ new ud_itab_entry( "ldmxcsr", "Md", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0769 */ new ud_itab_entry( "lds", "Gv;M", BitOps.P_aso | BitOps.P_oso ),
            /* 0770 */ new ud_itab_entry( "lea", "Gv;M", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0771 */ new ud_itab_entry( "les", "Gv;M", BitOps.P_aso | BitOps.P_oso ),
            /* 0772 */ new ud_itab_entry( "lfs", "Gz;M", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0773 */ new ud_itab_entry( "lgs", "Gz;M", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0774 */ new ud_itab_entry( "lidt", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0775 */ new ud_itab_entry( "lss", "Gv;M", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0776 */ new ud_itab_entry( "leave" ),
            /* 0777 */ new ud_itab_entry( "lfence" ),
            /* 0778 */ new ud_itab_entry( "lfence" ),
            /* 0779 */ new ud_itab_entry( "lfence" ),
            /* 0780 */ new ud_itab_entry( "lfence" ),
            /* 0781 */ new ud_itab_entry( "lfence" ),
            /* 0782 */ new ud_itab_entry( "lfence" ),
            /* 0783 */ new ud_itab_entry( "lfence" ),
            /* 0784 */ new ud_itab_entry( "lfence" ),
            /* 0785 */ new ud_itab_entry( "lgdt", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0786 */ new ud_itab_entry( "lldt", "Ew", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0787 */ new ud_itab_entry( "lmsw", "Ew", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0788 */ new ud_itab_entry( "lmsw", "Ew", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0789 */ new ud_itab_entry( "lock" ),
            /* 0790 */ new ud_itab_entry( "lodsb", BitOps.P_str | BitOps.P_seg ),
            /* 0791 */ new ud_itab_entry( "lodsw", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 0792 */ new ud_itab_entry( "lodsd", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 0793 */ new ud_itab_entry( "lodsq", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 0794 */ new ud_itab_entry( "loopne", "Jb" ),
            /* 0795 */ new ud_itab_entry( "loope", "Jb" ),
            /* 0796 */ new ud_itab_entry( "loop", "Jb" ),
            /* 0797 */ new ud_itab_entry( "lsl", "Gv;Ew", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0798 */ new ud_itab_entry( "ltr", "Ew", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0799 */ new ud_itab_entry( "maskmovq", "P;N", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0800 */ new ud_itab_entry( "maxpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0801 */ new ud_itab_entry( "vmaxpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0802 */ new ud_itab_entry( "maxps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0803 */ new ud_itab_entry( "vmaxps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0804 */ new ud_itab_entry( "maxsd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0805 */ new ud_itab_entry( "vmaxsd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0806 */ new ud_itab_entry( "maxss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0807 */ new ud_itab_entry( "vmaxss", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0808 */ new ud_itab_entry( "mfence" ),
            /* 0809 */ new ud_itab_entry( "mfence" ),
            /* 0810 */ new ud_itab_entry( "mfence" ),
            /* 0811 */ new ud_itab_entry( "mfence" ),
            /* 0812 */ new ud_itab_entry( "mfence" ),
            /* 0813 */ new ud_itab_entry( "mfence" ),
            /* 0814 */ new ud_itab_entry( "mfence" ),
            /* 0815 */ new ud_itab_entry( "mfence" ),
            /* 0816 */ new ud_itab_entry( "minpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0817 */ new ud_itab_entry( "vminpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0818 */ new ud_itab_entry( "minps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0819 */ new ud_itab_entry( "vminps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0820 */ new ud_itab_entry( "minsd", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0821 */ new ud_itab_entry( "vminsd", "Vx;Hx;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0822 */ new ud_itab_entry( "minss", "V;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0823 */ new ud_itab_entry( "vminss", "Vx;Hx;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0824 */ new ud_itab_entry( "monitor" ),
            /* 0825 */ new ud_itab_entry( "montmul" ),
            /* 0826 */ new ud_itab_entry( "mov", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0827 */ new ud_itab_entry( "mov", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0828 */ new ud_itab_entry( "mov", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0829 */ new ud_itab_entry( "mov", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0830 */ new ud_itab_entry( "mov", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0831 */ new ud_itab_entry( "mov", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0832 */ new ud_itab_entry( "mov", "MwRv;S", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0833 */ new ud_itab_entry( "mov", "S;MwRv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0834 */ new ud_itab_entry( "mov", "AL;Ob" ),
            /* 0835 */ new ud_itab_entry( "mov", "rAX;Ov", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw ),
            /* 0836 */ new ud_itab_entry( "mov", "Ob;AL" ),
            /* 0837 */ new ud_itab_entry( "mov", "Ov;rAX", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw ),
            /* 0838 */ new ud_itab_entry( "mov", "R0b;Ib", BitOps.P_rexb ),
            /* 0839 */ new ud_itab_entry( "mov", "R1b;Ib", BitOps.P_rexb ),
            /* 0840 */ new ud_itab_entry( "mov", "R2b;Ib", BitOps.P_rexb ),
            /* 0841 */ new ud_itab_entry( "mov", "R3b;Ib", BitOps.P_rexb ),
            /* 0842 */ new ud_itab_entry( "mov", "R4b;Ib", BitOps.P_rexb ),
            /* 0843 */ new ud_itab_entry( "mov", "R5b;Ib", BitOps.P_rexb ),
            /* 0844 */ new ud_itab_entry( "mov", "R6b;Ib", BitOps.P_rexb ),
            /* 0845 */ new ud_itab_entry( "mov", "R7b;Ib", BitOps.P_rexb ),
            /* 0846 */ new ud_itab_entry( "mov", "R0v;Iv", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0847 */ new ud_itab_entry( "mov", "R1v;Iv", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0848 */ new ud_itab_entry( "mov", "R2v;Iv", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0849 */ new ud_itab_entry( "mov", "R3v;Iv", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0850 */ new ud_itab_entry( "mov", "R4v;Iv", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0851 */ new ud_itab_entry( "mov", "R5v;Iv", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0852 */ new ud_itab_entry( "mov", "R6v;Iv", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0853 */ new ud_itab_entry( "mov", "R7v;Iv", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0854 */ new ud_itab_entry( "mov", "R;C", BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0855 */ new ud_itab_entry( "mov", "R;D", BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0856 */ new ud_itab_entry( "mov", "C;R", BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0857 */ new ud_itab_entry( "mov", "D;R", BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb ),
            /* 0858 */ new ud_itab_entry( "movapd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0859 */ new ud_itab_entry( "vmovapd", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0860 */ new ud_itab_entry( "movapd", "W;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0861 */ new ud_itab_entry( "vmovapd", "Wx;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0862 */ new ud_itab_entry( "movaps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0863 */ new ud_itab_entry( "vmovaps", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0864 */ new ud_itab_entry( "movaps", "W;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0865 */ new ud_itab_entry( "vmovaps", "Wx;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0866 */ new ud_itab_entry( "movd", "P;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0867 */ new ud_itab_entry( "movd", "P;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0868 */ new ud_itab_entry( "movd", "V;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0869 */ new ud_itab_entry( "vmovd", "Vx;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0870 */ new ud_itab_entry( "movd", "V;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0871 */ new ud_itab_entry( "vmovd", "Vx;Ey", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0872 */ new ud_itab_entry( "movd", "Ey;P", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0873 */ new ud_itab_entry( "movd", "Ey;P", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0874 */ new ud_itab_entry( "movd", "Ey;V", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0875 */ new ud_itab_entry( "vmovd", "Ey;Vx", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0876 */ new ud_itab_entry( "movd", "Ey;V", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0877 */ new ud_itab_entry( "vmovd", "Ey;Vx", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0878 */ new ud_itab_entry( "movhpd", "V;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0879 */ new ud_itab_entry( "vmovhpd", "Vx;Hx;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0880 */ new ud_itab_entry( "movhpd", "M;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0881 */ new ud_itab_entry( "vmovhpd", "M;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0882 */ new ud_itab_entry( "movhps", "V;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0883 */ new ud_itab_entry( "vmovhps", "Vx;Hx;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0884 */ new ud_itab_entry( "movhps", "M;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0885 */ new ud_itab_entry( "vmovhps", "M;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0886 */ new ud_itab_entry( "movlhps", "V;U", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0887 */ new ud_itab_entry( "vmovlhps", "Vx;Hx;Ux", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0888 */ new ud_itab_entry( "movlpd", "V;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0889 */ new ud_itab_entry( "vmovlpd", "Vx;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0890 */ new ud_itab_entry( "movlpd", "M;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0891 */ new ud_itab_entry( "vmovlpd", "M;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0892 */ new ud_itab_entry( "movlps", "V;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0893 */ new ud_itab_entry( "vmovlps", "Vx;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0894 */ new ud_itab_entry( "movlps", "M;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0895 */ new ud_itab_entry( "vmovlps", "M;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0896 */ new ud_itab_entry( "movhlps", "V;U", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0897 */ new ud_itab_entry( "vmovhlps", "Vx;Ux", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0898 */ new ud_itab_entry( "movmskpd", "Gd;U", BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexb ),
            /* 0899 */ new ud_itab_entry( "vmovmskpd", "Gd;Ux", BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0900 */ new ud_itab_entry( "movmskps", "Gd;U", BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexb ),
            /* 0901 */ new ud_itab_entry( "vmovmskps", "Gd;Ux", BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexb ),
            /* 0902 */ new ud_itab_entry( "movntdq", "M;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0903 */ new ud_itab_entry( "vmovntdq", "M;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0904 */ new ud_itab_entry( "movnti", "M;Gy", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0905 */ new ud_itab_entry( "movntpd", "M;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0906 */ new ud_itab_entry( "vmovntpd", "M;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0907 */ new ud_itab_entry( "movntps", "M;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0908 */ new ud_itab_entry( "vmovntps", "M;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0909 */ new ud_itab_entry( "movntq", "M;P", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0910 */ new ud_itab_entry( "movq", "P;Eq", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0911 */ new ud_itab_entry( "movq", "V;Eq", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0912 */ new ud_itab_entry( "vmovq", "Vx;Eq", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0913 */ new ud_itab_entry( "movq", "Eq;P", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0914 */ new ud_itab_entry( "movq", "Eq;V", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0915 */ new ud_itab_entry( "vmovq", "Eq;Vx", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0916 */ new ud_itab_entry( "movq", "V;W", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0917 */ new ud_itab_entry( "vmovq", "Vx;Wx", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0918 */ new ud_itab_entry( "movq", "W;V", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0919 */ new ud_itab_entry( "vmovq", "Wx;Vx", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0920 */ new ud_itab_entry( "movq", "P;Q", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0921 */ new ud_itab_entry( "movq", "Q;P", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0922 */ new ud_itab_entry( "movsb", BitOps.P_str | BitOps.P_seg ),
            /* 0923 */ new ud_itab_entry( "movsw", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 0924 */ new ud_itab_entry( "movsd", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 0925 */ new ud_itab_entry( "movsd", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0926 */ new ud_itab_entry( "movsd", "W;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0927 */ new ud_itab_entry( "movsq", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 0928 */ new ud_itab_entry( "movss", "V;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0929 */ new ud_itab_entry( "movss", "W;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0930 */ new ud_itab_entry( "movsx", "Gv;Eb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0931 */ new ud_itab_entry( "movsx", "Gy;Ew", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0932 */ new ud_itab_entry( "movupd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0933 */ new ud_itab_entry( "vmovupd", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0934 */ new ud_itab_entry( "movupd", "W;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0935 */ new ud_itab_entry( "vmovupd", "Wx;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0936 */ new ud_itab_entry( "movups", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0937 */ new ud_itab_entry( "vmovups", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0938 */ new ud_itab_entry( "movups", "W;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0939 */ new ud_itab_entry( "vmovups", "Wx;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0940 */ new ud_itab_entry( "movzx", "Gv;Eb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0941 */ new ud_itab_entry( "movzx", "Gy;Ew", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0942 */ new ud_itab_entry( "mul", "Eb", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0943 */ new ud_itab_entry( "mul", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0944 */ new ud_itab_entry( "mulpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0945 */ new ud_itab_entry( "vmulpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0946 */ new ud_itab_entry( "mulps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0947 */ new ud_itab_entry( "vmulps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0948 */ new ud_itab_entry( "mulsd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0949 */ new ud_itab_entry( "vmulsd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0950 */ new ud_itab_entry( "mulss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0951 */ new ud_itab_entry( "vmulss", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0952 */ new ud_itab_entry( "mwait" ),
            /* 0953 */ new ud_itab_entry( "neg", "Eb", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0954 */ new ud_itab_entry( "neg", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0955 */ new ud_itab_entry( "nop", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0956 */ new ud_itab_entry( "nop", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0957 */ new ud_itab_entry( "nop", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0958 */ new ud_itab_entry( "nop", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0959 */ new ud_itab_entry( "nop", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0960 */ new ud_itab_entry( "nop", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0961 */ new ud_itab_entry( "nop", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0962 */ new ud_itab_entry( "not", "Eb", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0963 */ new ud_itab_entry( "not", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0964 */ new ud_itab_entry( "or", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0965 */ new ud_itab_entry( "or", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0966 */ new ud_itab_entry( "or", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0967 */ new ud_itab_entry( "or", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0968 */ new ud_itab_entry( "or", "AL;Ib" ),
            /* 0969 */ new ud_itab_entry( "or", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 0970 */ new ud_itab_entry( "or", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0971 */ new ud_itab_entry( "or", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0972 */ new ud_itab_entry( "or", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0973 */ new ud_itab_entry( "or", "Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0974 */ new ud_itab_entry( "orpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0975 */ new ud_itab_entry( "vorpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0976 */ new ud_itab_entry( "orps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0977 */ new ud_itab_entry( "vorps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0978 */ new ud_itab_entry( "out", "Ib;AL" ),
            /* 0979 */ new ud_itab_entry( "out", "Ib;eAX", BitOps.P_oso ),
            /* 0980 */ new ud_itab_entry( "out", "DX;AL" ),
            /* 0981 */ new ud_itab_entry( "out", "DX;eAX", BitOps.P_oso ),
            /* 0982 */ new ud_itab_entry( "outsb", BitOps.P_str | BitOps.P_seg ),
            /* 0983 */ new ud_itab_entry( "outsw", BitOps.P_str | BitOps.P_oso | BitOps.P_seg ),
            /* 0984 */ new ud_itab_entry( "outsd", BitOps.P_str | BitOps.P_oso | BitOps.P_seg ),
            /* 0985 */ new ud_itab_entry( "packsswb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0986 */ new ud_itab_entry( "vpacksswb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0987 */ new ud_itab_entry( "packsswb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0988 */ new ud_itab_entry( "packssdw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0989 */ new ud_itab_entry( "vpackssdw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0990 */ new ud_itab_entry( "packssdw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0991 */ new ud_itab_entry( "packuswb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0992 */ new ud_itab_entry( "vpackuswb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0993 */ new ud_itab_entry( "packuswb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0994 */ new ud_itab_entry( "paddb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0995 */ new ud_itab_entry( "vpaddb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 0996 */ new ud_itab_entry( "paddb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0997 */ new ud_itab_entry( "paddw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0998 */ new ud_itab_entry( "paddw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 0999 */ new ud_itab_entry( "vpaddw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1000 */ new ud_itab_entry( "paddd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1001 */ new ud_itab_entry( "paddd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1002 */ new ud_itab_entry( "vpaddd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1003 */ new ud_itab_entry( "paddsb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1004 */ new ud_itab_entry( "paddsb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1005 */ new ud_itab_entry( "vpaddsb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1006 */ new ud_itab_entry( "paddsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1007 */ new ud_itab_entry( "paddsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1008 */ new ud_itab_entry( "vpaddsw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1009 */ new ud_itab_entry( "paddusb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1010 */ new ud_itab_entry( "paddusb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1011 */ new ud_itab_entry( "vpaddusb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1012 */ new ud_itab_entry( "paddusw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1013 */ new ud_itab_entry( "paddusw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1014 */ new ud_itab_entry( "vpaddusw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1015 */ new ud_itab_entry( "pand", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1016 */ new ud_itab_entry( "vpand", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1017 */ new ud_itab_entry( "pand", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1018 */ new ud_itab_entry( "pandn", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1019 */ new ud_itab_entry( "vpandn", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1020 */ new ud_itab_entry( "pandn", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1021 */ new ud_itab_entry( "pavgb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1022 */ new ud_itab_entry( "vpavgb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1023 */ new ud_itab_entry( "pavgb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1024 */ new ud_itab_entry( "pavgw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1025 */ new ud_itab_entry( "vpavgw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1026 */ new ud_itab_entry( "pavgw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1027 */ new ud_itab_entry( "pcmpeqb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1028 */ new ud_itab_entry( "pcmpeqb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1029 */ new ud_itab_entry( "vpcmpeqb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1030 */ new ud_itab_entry( "pcmpeqw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1031 */ new ud_itab_entry( "pcmpeqw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1032 */ new ud_itab_entry( "vpcmpeqw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1033 */ new ud_itab_entry( "pcmpeqd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1034 */ new ud_itab_entry( "pcmpeqd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1035 */ new ud_itab_entry( "vpcmpeqd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1036 */ new ud_itab_entry( "pcmpgtb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1037 */ new ud_itab_entry( "vpcmpgtb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1038 */ new ud_itab_entry( "pcmpgtb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1039 */ new ud_itab_entry( "pcmpgtw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1040 */ new ud_itab_entry( "vpcmpgtw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1041 */ new ud_itab_entry( "pcmpgtw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1042 */ new ud_itab_entry( "pcmpgtd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1043 */ new ud_itab_entry( "vpcmpgtd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1044 */ new ud_itab_entry( "pcmpgtd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1045 */ new ud_itab_entry( "pextrb", "MbRv;V;Ib", BitOps.P_aso | BitOps.P_rexx | BitOps.P_rexr | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1046 */ new ud_itab_entry( "vpextrb", "MbRv;Vx;Ib", BitOps.P_aso | BitOps.P_rexx | BitOps.P_rexr | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1047 */ new ud_itab_entry( "pextrd", "Ed;V;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1048 */ new ud_itab_entry( "vpextrd", "Ed;Vx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1049 */ new ud_itab_entry( "pextrd", "Ed;V;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1050 */ new ud_itab_entry( "vpextrd", "Ed;Vx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1051 */ new ud_itab_entry( "pextrq", "Eq;V;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1052 */ new ud_itab_entry( "vpextrq", "Eq;Vx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1053 */ new ud_itab_entry( "pextrw", "Gd;U;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexb ),
            /* 1054 */ new ud_itab_entry( "vpextrw", "Gd;Ux;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexb ),
            /* 1055 */ new ud_itab_entry( "pextrw", "Gd;N;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1056 */ new ud_itab_entry( "pextrw", "MwRd;V;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexr | BitOps.P_rexb ),
            /* 1057 */ new ud_itab_entry( "vpextrw", "MwRd;Vx;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexr | BitOps.P_rexb ),
            /* 1058 */ new ud_itab_entry( "pinsrb", "V;MbRd;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1059 */ new ud_itab_entry( "pinsrw", "P;MwRy;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1060 */ new ud_itab_entry( "pinsrw", "V;MwRy;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1061 */ new ud_itab_entry( "vpinsrw", "Vx;MwRy;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1062 */ new ud_itab_entry( "pinsrd", "V;Ed;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1063 */ new ud_itab_entry( "pinsrd", "V;Ed;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1064 */ new ud_itab_entry( "pinsrq", "V;Eq;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1065 */ new ud_itab_entry( "vpinsrb", "V;H;MbRd;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1066 */ new ud_itab_entry( "vpinsrd", "V;H;Ed;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1067 */ new ud_itab_entry( "vpinsrd", "V;H;Ed;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1068 */ new ud_itab_entry( "vpinsrq", "V;H;Eq;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1069 */ new ud_itab_entry( "pmaddwd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1070 */ new ud_itab_entry( "pmaddwd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1071 */ new ud_itab_entry( "vpmaddwd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1072 */ new ud_itab_entry( "pmaxsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1073 */ new ud_itab_entry( "vpmaxsw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1074 */ new ud_itab_entry( "pmaxsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1075 */ new ud_itab_entry( "pmaxub", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1076 */ new ud_itab_entry( "pmaxub", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1077 */ new ud_itab_entry( "vpmaxub", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1078 */ new ud_itab_entry( "pminsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1079 */ new ud_itab_entry( "vpminsw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1080 */ new ud_itab_entry( "pminsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1081 */ new ud_itab_entry( "pminub", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1082 */ new ud_itab_entry( "vpminub", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1083 */ new ud_itab_entry( "pminub", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1084 */ new ud_itab_entry( "pmovmskb", "Gd;U", BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1085 */ new ud_itab_entry( "vpmovmskb", "Gd;Ux", BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1086 */ new ud_itab_entry( "pmovmskb", "Gd;N", BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1087 */ new ud_itab_entry( "pmulhuw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1088 */ new ud_itab_entry( "pmulhuw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1089 */ new ud_itab_entry( "vpmulhuw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1090 */ new ud_itab_entry( "pmulhw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1091 */ new ud_itab_entry( "vpmulhw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1092 */ new ud_itab_entry( "pmulhw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1093 */ new ud_itab_entry( "pmullw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1094 */ new ud_itab_entry( "pmullw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1095 */ new ud_itab_entry( "vpmullw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1096 */ new ud_itab_entry( "pop", "ES", BitOps.P_inv64 ),
            /* 1097 */ new ud_itab_entry( "pop", "SS", BitOps.P_inv64 ),
            /* 1098 */ new ud_itab_entry( "pop", "DS", BitOps.P_inv64 ),
            /* 1099 */ new ud_itab_entry( "pop", "GS" ),
            /* 1100 */ new ud_itab_entry( "pop", "FS" ),
            /* 1101 */ new ud_itab_entry( "pop", "R0v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1102 */ new ud_itab_entry( "pop", "R1v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1103 */ new ud_itab_entry( "pop", "R2v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1104 */ new ud_itab_entry( "pop", "R3v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1105 */ new ud_itab_entry( "pop", "R4v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1106 */ new ud_itab_entry( "pop", "R5v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1107 */ new ud_itab_entry( "pop", "R6v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1108 */ new ud_itab_entry( "pop", "R7v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1109 */ new ud_itab_entry( "pop", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1110 */ new ud_itab_entry( "popa", BitOps.P_oso | BitOps.P_inv64 ),
            /* 1111 */ new ud_itab_entry( "popad", BitOps.P_oso | BitOps.P_inv64 ),
            /* 1112 */ new ud_itab_entry( "popfw", BitOps.P_oso ),
            /* 1113 */ new ud_itab_entry( "popfw", BitOps.P_oso | BitOps.P_rexw | BitOps.P_def64 ),
            /* 1114 */ new ud_itab_entry( "popfd", BitOps.P_oso ),
            /* 1115 */ new ud_itab_entry( "popfq", BitOps.P_oso | BitOps.P_def64 ),
            /* 1116 */ new ud_itab_entry( "popfq", BitOps.P_oso | BitOps.P_def64 ),
            /* 1117 */ new ud_itab_entry( "por", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1118 */ new ud_itab_entry( "vpor", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1119 */ new ud_itab_entry( "por", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1120 */ new ud_itab_entry( "prefetch", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1121 */ new ud_itab_entry( "prefetch", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1122 */ new ud_itab_entry( "prefetch", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1123 */ new ud_itab_entry( "prefetch", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1124 */ new ud_itab_entry( "prefetch", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1125 */ new ud_itab_entry( "prefetch", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1126 */ new ud_itab_entry( "prefetch", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1127 */ new ud_itab_entry( "prefetch", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1128 */ new ud_itab_entry( "prefetchnta", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1129 */ new ud_itab_entry( "prefetcht0", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1130 */ new ud_itab_entry( "prefetcht1", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1131 */ new ud_itab_entry( "prefetcht2", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1132 */ new ud_itab_entry( "psadbw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1133 */ new ud_itab_entry( "vpsadbw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1134 */ new ud_itab_entry( "psadbw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1135 */ new ud_itab_entry( "pshufw", "P;Q;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1136 */ new ud_itab_entry( "psllw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1137 */ new ud_itab_entry( "psllw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1138 */ new ud_itab_entry( "psllw", "U;Ib", BitOps.P_rexb ),
            /* 1139 */ new ud_itab_entry( "psllw", "N;Ib" ),
            /* 1140 */ new ud_itab_entry( "pslld", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1141 */ new ud_itab_entry( "pslld", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1142 */ new ud_itab_entry( "pslld", "U;Ib", BitOps.P_rexb ),
            /* 1143 */ new ud_itab_entry( "pslld", "N;Ib" ),
            /* 1144 */ new ud_itab_entry( "psllq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1145 */ new ud_itab_entry( "psllq", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1146 */ new ud_itab_entry( "psllq", "U;Ib", BitOps.P_rexb ),
            /* 1147 */ new ud_itab_entry( "psllq", "N;Ib" ),
            /* 1148 */ new ud_itab_entry( "psraw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1149 */ new ud_itab_entry( "psraw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1150 */ new ud_itab_entry( "vpsraw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1151 */ new ud_itab_entry( "psraw", "U;Ib", BitOps.P_rexb ),
            /* 1152 */ new ud_itab_entry( "vpsraw", "Hx;Ux;Ib", BitOps.P_rexb ),
            /* 1153 */ new ud_itab_entry( "psraw", "N;Ib" ),
            /* 1154 */ new ud_itab_entry( "psrad", "N;Ib" ),
            /* 1155 */ new ud_itab_entry( "psrad", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1156 */ new ud_itab_entry( "vpsrad", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1157 */ new ud_itab_entry( "psrad", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1158 */ new ud_itab_entry( "psrad", "U;Ib", BitOps.P_rexb ),
            /* 1159 */ new ud_itab_entry( "vpsrad", "Hx;Ux;Ib", BitOps.P_rexb ),
            /* 1160 */ new ud_itab_entry( "psrlw", "N;Ib" ),
            /* 1161 */ new ud_itab_entry( "psrlw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1162 */ new ud_itab_entry( "psrlw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1163 */ new ud_itab_entry( "vpsrlw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1164 */ new ud_itab_entry( "psrlw", "U;Ib", BitOps.P_rexb ),
            /* 1165 */ new ud_itab_entry( "vpsrlw", "Hx;Ux;Ib", BitOps.P_rexb ),
            /* 1166 */ new ud_itab_entry( "psrld", "N;Ib" ),
            /* 1167 */ new ud_itab_entry( "psrld", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1168 */ new ud_itab_entry( "psrld", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1169 */ new ud_itab_entry( "vpsrld", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1170 */ new ud_itab_entry( "psrld", "U;Ib", BitOps.P_rexb ),
            /* 1171 */ new ud_itab_entry( "vpsrld", "Hx;Ux;Ib", BitOps.P_rexb ),
            /* 1172 */ new ud_itab_entry( "psrlq", "N;Ib" ),
            /* 1173 */ new ud_itab_entry( "psrlq", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1174 */ new ud_itab_entry( "psrlq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1175 */ new ud_itab_entry( "vpsrlq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1176 */ new ud_itab_entry( "psrlq", "U;Ib", BitOps.P_rexb ),
            /* 1177 */ new ud_itab_entry( "vpsrlq", "Hx;Ux;Ib", BitOps.P_rexb ),
            /* 1178 */ new ud_itab_entry( "psubb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1179 */ new ud_itab_entry( "vpsubb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1180 */ new ud_itab_entry( "psubb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1181 */ new ud_itab_entry( "psubw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1182 */ new ud_itab_entry( "vpsubw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1183 */ new ud_itab_entry( "psubw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1184 */ new ud_itab_entry( "psubd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1185 */ new ud_itab_entry( "psubd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1186 */ new ud_itab_entry( "vpsubd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1187 */ new ud_itab_entry( "psubsb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1188 */ new ud_itab_entry( "psubsb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1189 */ new ud_itab_entry( "vpsubsb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1190 */ new ud_itab_entry( "psubsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1191 */ new ud_itab_entry( "psubsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1192 */ new ud_itab_entry( "vpsubsw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1193 */ new ud_itab_entry( "psubusb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1194 */ new ud_itab_entry( "psubusb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1195 */ new ud_itab_entry( "vpsubusb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1196 */ new ud_itab_entry( "psubusw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1197 */ new ud_itab_entry( "psubusw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1198 */ new ud_itab_entry( "vpsubusw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1199 */ new ud_itab_entry( "punpckhbw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1200 */ new ud_itab_entry( "vpunpckhbw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1201 */ new ud_itab_entry( "punpckhbw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1202 */ new ud_itab_entry( "punpckhwd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1203 */ new ud_itab_entry( "vpunpckhwd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1204 */ new ud_itab_entry( "punpckhwd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1205 */ new ud_itab_entry( "punpckhdq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1206 */ new ud_itab_entry( "vpunpckhdq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1207 */ new ud_itab_entry( "punpckhdq", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1208 */ new ud_itab_entry( "punpcklbw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1209 */ new ud_itab_entry( "vpunpcklbw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1210 */ new ud_itab_entry( "punpcklbw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1211 */ new ud_itab_entry( "punpcklwd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1212 */ new ud_itab_entry( "vpunpcklwd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1213 */ new ud_itab_entry( "punpcklwd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1214 */ new ud_itab_entry( "punpckldq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1215 */ new ud_itab_entry( "vpunpckldq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1216 */ new ud_itab_entry( "punpckldq", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1217 */ new ud_itab_entry( "pi2fw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1218 */ new ud_itab_entry( "pi2fd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1219 */ new ud_itab_entry( "pf2iw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1220 */ new ud_itab_entry( "pf2id", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1221 */ new ud_itab_entry( "pfnacc", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1222 */ new ud_itab_entry( "pfpnacc", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1223 */ new ud_itab_entry( "pfcmpge", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1224 */ new ud_itab_entry( "pfmin", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1225 */ new ud_itab_entry( "pfrcp", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1226 */ new ud_itab_entry( "pfrsqrt", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1227 */ new ud_itab_entry( "pfsub", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1228 */ new ud_itab_entry( "pfadd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1229 */ new ud_itab_entry( "pfcmpgt", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1230 */ new ud_itab_entry( "pfmax", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1231 */ new ud_itab_entry( "pfrcpit1", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1232 */ new ud_itab_entry( "pfrsqit1", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1233 */ new ud_itab_entry( "pfsubr", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1234 */ new ud_itab_entry( "pfacc", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1235 */ new ud_itab_entry( "pfcmpeq", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1236 */ new ud_itab_entry( "pfmul", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1237 */ new ud_itab_entry( "pfrcpit2", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1238 */ new ud_itab_entry( "pmulhrw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1239 */ new ud_itab_entry( "pswapd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1240 */ new ud_itab_entry( "pavgusb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1241 */ new ud_itab_entry( "push", "ES", BitOps.P_inv64 ),
            /* 1242 */ new ud_itab_entry( "push", "CS", BitOps.P_inv64 ),
            /* 1243 */ new ud_itab_entry( "push", "SS", BitOps.P_inv64 ),
            /* 1244 */ new ud_itab_entry( "push", "DS", BitOps.P_inv64 ),
            /* 1245 */ new ud_itab_entry( "push", "GS" ),
            /* 1246 */ new ud_itab_entry( "push", "FS" ),
            /* 1247 */ new ud_itab_entry( "push", "R0v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1248 */ new ud_itab_entry( "push", "R1v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1249 */ new ud_itab_entry( "push", "R2v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1250 */ new ud_itab_entry( "push", "R3v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1251 */ new ud_itab_entry( "push", "R4v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1252 */ new ud_itab_entry( "push", "R5v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1253 */ new ud_itab_entry( "push", "R6v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1254 */ new ud_itab_entry( "push", "R7v", BitOps.P_oso | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1255 */ new ud_itab_entry( "push", "sIz", BitOps.P_oso | BitOps.P_def64 ),
            /* 1256 */ new ud_itab_entry( "push", "Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1257 */ new ud_itab_entry( "push", "sIb", BitOps.P_oso | BitOps.P_def64 ),
            /* 1258 */ new ud_itab_entry( "pusha", BitOps.P_oso | BitOps.P_inv64 ),
            /* 1259 */ new ud_itab_entry( "pushad", BitOps.P_oso | BitOps.P_inv64 ),
            /* 1260 */ new ud_itab_entry( "pushfw", BitOps.P_oso ),
            /* 1261 */ new ud_itab_entry( "pushfw", BitOps.P_oso | BitOps.P_rexw | BitOps.P_def64 ),
            /* 1262 */ new ud_itab_entry( "pushfd", BitOps.P_oso ),
            /* 1263 */ new ud_itab_entry( "pushfq", BitOps.P_oso | BitOps.P_rexw | BitOps.P_def64 ),
            /* 1264 */ new ud_itab_entry( "pushfq", BitOps.P_oso | BitOps.P_rexw | BitOps.P_def64 ),
            /* 1265 */ new ud_itab_entry( "pxor", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1266 */ new ud_itab_entry( "vpxor", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1267 */ new ud_itab_entry( "pxor", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1268 */ new ud_itab_entry( "rcl", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1269 */ new ud_itab_entry( "rcl", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1270 */ new ud_itab_entry( "rcl", "Eb;I1", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1271 */ new ud_itab_entry( "rcl", "Eb;CL", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1272 */ new ud_itab_entry( "rcl", "Ev;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1273 */ new ud_itab_entry( "rcl", "Ev;I1", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1274 */ new ud_itab_entry( "rcr", "Eb;I1", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1275 */ new ud_itab_entry( "rcr", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1276 */ new ud_itab_entry( "rcr", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1277 */ new ud_itab_entry( "rcr", "Ev;I1", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1278 */ new ud_itab_entry( "rcr", "Eb;CL", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1279 */ new ud_itab_entry( "rcr", "Ev;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1280 */ new ud_itab_entry( "rol", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1281 */ new ud_itab_entry( "rol", "Eb;I1", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1282 */ new ud_itab_entry( "rol", "Ev;I1", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1283 */ new ud_itab_entry( "rol", "Eb;CL", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1284 */ new ud_itab_entry( "rol", "Ev;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1285 */ new ud_itab_entry( "rol", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1286 */ new ud_itab_entry( "ror", "Eb;I1", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1287 */ new ud_itab_entry( "ror", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1288 */ new ud_itab_entry( "ror", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1289 */ new ud_itab_entry( "ror", "Ev;I1", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1290 */ new ud_itab_entry( "ror", "Eb;CL", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1291 */ new ud_itab_entry( "ror", "Ev;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1292 */ new ud_itab_entry( "rcpps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1293 */ new ud_itab_entry( "vrcpps", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1294 */ new ud_itab_entry( "rcpss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1295 */ new ud_itab_entry( "vrcpss", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1296 */ new ud_itab_entry( "rdmsr" ),
            /* 1297 */ new ud_itab_entry( "rdpmc" ),
            /* 1298 */ new ud_itab_entry( "rdtsc" ),
            /* 1299 */ new ud_itab_entry( "rdtscp" ),
            /* 1300 */ new ud_itab_entry( "repne" ),
            /* 1301 */ new ud_itab_entry( "rep" ),
            /* 1302 */ new ud_itab_entry( "ret", "Iw" ),
            /* 1303 */ new ud_itab_entry( "ret" ),
            /* 1304 */ new ud_itab_entry( "retf", "Iw" ),
            /* 1305 */ new ud_itab_entry( "retf" ),
            /* 1306 */ new ud_itab_entry( "rsm" ),
            /* 1307 */ new ud_itab_entry( "rsqrtps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1308 */ new ud_itab_entry( "vrsqrtps", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1309 */ new ud_itab_entry( "rsqrtss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1310 */ new ud_itab_entry( "vrsqrtss", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1311 */ new ud_itab_entry( "sahf" ),
            /* 1312 */ new ud_itab_entry( "salc", BitOps.P_inv64 ),
            /* 1313 */ new ud_itab_entry( "sar", "Ev;I1", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1314 */ new ud_itab_entry( "sar", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1315 */ new ud_itab_entry( "sar", "Eb;I1", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1316 */ new ud_itab_entry( "sar", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1317 */ new ud_itab_entry( "sar", "Eb;CL", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1318 */ new ud_itab_entry( "sar", "Ev;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1319 */ new ud_itab_entry( "shl", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1320 */ new ud_itab_entry( "shl", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1321 */ new ud_itab_entry( "shl", "Eb;I1", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1322 */ new ud_itab_entry( "shl", "Eb;CL", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1323 */ new ud_itab_entry( "shl", "Ev;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1324 */ new ud_itab_entry( "shl", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1325 */ new ud_itab_entry( "shl", "Eb;CL", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1326 */ new ud_itab_entry( "shl", "Ev;I1", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1327 */ new ud_itab_entry( "shl", "Eb;I1", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1328 */ new ud_itab_entry( "shl", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1329 */ new ud_itab_entry( "shl", "Ev;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1330 */ new ud_itab_entry( "shl", "Ev;I1", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1331 */ new ud_itab_entry( "shr", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1332 */ new ud_itab_entry( "shr", "Eb;CL", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1333 */ new ud_itab_entry( "shr", "Ev;I1", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1334 */ new ud_itab_entry( "shr", "Eb;I1", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1335 */ new ud_itab_entry( "shr", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1336 */ new ud_itab_entry( "shr", "Ev;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1337 */ new ud_itab_entry( "sbb", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1338 */ new ud_itab_entry( "sbb", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1339 */ new ud_itab_entry( "sbb", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1340 */ new ud_itab_entry( "sbb", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1341 */ new ud_itab_entry( "sbb", "AL;Ib" ),
            /* 1342 */ new ud_itab_entry( "sbb", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 1343 */ new ud_itab_entry( "sbb", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1344 */ new ud_itab_entry( "sbb", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1345 */ new ud_itab_entry( "sbb", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_inv64 ),
            /* 1346 */ new ud_itab_entry( "sbb", "Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1347 */ new ud_itab_entry( "scasb", BitOps.P_strz ),
            /* 1348 */ new ud_itab_entry( "scasw", BitOps.P_strz | BitOps.P_oso | BitOps.P_rexw ),
            /* 1349 */ new ud_itab_entry( "scasd", BitOps.P_strz | BitOps.P_oso | BitOps.P_rexw ),
            /* 1350 */ new ud_itab_entry( "scasq", BitOps.P_strz | BitOps.P_oso | BitOps.P_rexw ),
            /* 1351 */ new ud_itab_entry( "seto", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1352 */ new ud_itab_entry( "setno", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1353 */ new ud_itab_entry( "setb", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1354 */ new ud_itab_entry( "setae", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1355 */ new ud_itab_entry( "setz", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1356 */ new ud_itab_entry( "setnz", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1357 */ new ud_itab_entry( "setbe", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1358 */ new ud_itab_entry( "seta", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1359 */ new ud_itab_entry( "sets", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1360 */ new ud_itab_entry( "setns", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1361 */ new ud_itab_entry( "setp", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1362 */ new ud_itab_entry( "setnp", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1363 */ new ud_itab_entry( "setl", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1364 */ new ud_itab_entry( "setge", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1365 */ new ud_itab_entry( "setle", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1366 */ new ud_itab_entry( "setg", "Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1367 */ new ud_itab_entry( "sfence" ),
            /* 1368 */ new ud_itab_entry( "sfence" ),
            /* 1369 */ new ud_itab_entry( "sfence" ),
            /* 1370 */ new ud_itab_entry( "sfence" ),
            /* 1371 */ new ud_itab_entry( "sfence" ),
            /* 1372 */ new ud_itab_entry( "sfence" ),
            /* 1373 */ new ud_itab_entry( "sfence" ),
            /* 1374 */ new ud_itab_entry( "sfence" ),
            /* 1375 */ new ud_itab_entry( "sgdt", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1376 */ new ud_itab_entry( "shld", "Ev;Gv;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1377 */ new ud_itab_entry( "shld", "Ev;Gv;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1378 */ new ud_itab_entry( "shrd", "Ev;Gv;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1379 */ new ud_itab_entry( "shrd", "Ev;Gv;CL", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1380 */ new ud_itab_entry( "shufpd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1381 */ new ud_itab_entry( "vshufpd", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1382 */ new ud_itab_entry( "shufps", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1383 */ new ud_itab_entry( "vshufps", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1384 */ new ud_itab_entry( "sidt", "M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1385 */ new ud_itab_entry( "sldt", "MwRv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1386 */ new ud_itab_entry( "smsw", "MwRv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1387 */ new ud_itab_entry( "smsw", "MwRv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1388 */ new ud_itab_entry( "sqrtps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1389 */ new ud_itab_entry( "vsqrtps", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1390 */ new ud_itab_entry( "sqrtpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1391 */ new ud_itab_entry( "vsqrtpd", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1392 */ new ud_itab_entry( "sqrtsd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1393 */ new ud_itab_entry( "vsqrtsd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1394 */ new ud_itab_entry( "sqrtss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1395 */ new ud_itab_entry( "vsqrtss", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1396 */ new ud_itab_entry( "stc" ),
            /* 1397 */ new ud_itab_entry( "std" ),
            /* 1398 */ new ud_itab_entry( "stgi" ),
            /* 1399 */ new ud_itab_entry( "sti" ),
            /* 1400 */ new ud_itab_entry( "skinit" ),
            /* 1401 */ new ud_itab_entry( "stmxcsr", "Md", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1402 */ new ud_itab_entry( "vstmxcsr", "Md", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1403 */ new ud_itab_entry( "stosb", BitOps.P_str | BitOps.P_seg ),
            /* 1404 */ new ud_itab_entry( "stosw", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 1405 */ new ud_itab_entry( "stosd", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 1406 */ new ud_itab_entry( "stosq", BitOps.P_str | BitOps.P_seg | BitOps.P_oso | BitOps.P_rexw ),
            /* 1407 */ new ud_itab_entry( "str", "MwRv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1408 */ new ud_itab_entry( "sub", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1409 */ new ud_itab_entry( "sub", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1410 */ new ud_itab_entry( "sub", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1411 */ new ud_itab_entry( "sub", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1412 */ new ud_itab_entry( "sub", "AL;Ib" ),
            /* 1413 */ new ud_itab_entry( "sub", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 1414 */ new ud_itab_entry( "sub", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1415 */ new ud_itab_entry( "sub", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1416 */ new ud_itab_entry( "sub", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_inv64 ),
            /* 1417 */ new ud_itab_entry( "sub", "Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1418 */ new ud_itab_entry( "subpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1419 */ new ud_itab_entry( "vsubpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1420 */ new ud_itab_entry( "subps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1421 */ new ud_itab_entry( "vsubps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1422 */ new ud_itab_entry( "subsd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1423 */ new ud_itab_entry( "vsubsd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1424 */ new ud_itab_entry( "subss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1425 */ new ud_itab_entry( "vsubss", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1426 */ new ud_itab_entry( "swapgs" ),
            /* 1427 */ new ud_itab_entry( "syscall" ),
            /* 1428 */ new ud_itab_entry( "sysenter" ),
            /* 1429 */ new ud_itab_entry( "sysenter" ),
            /* 1430 */ new ud_itab_entry( "sysexit" ),
            /* 1431 */ new ud_itab_entry( "sysexit" ),
            /* 1432 */ new ud_itab_entry( "sysret" ),
            /* 1433 */ new ud_itab_entry( "test", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1434 */ new ud_itab_entry( "test", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1435 */ new ud_itab_entry( "test", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1436 */ new ud_itab_entry( "test", "AL;Ib" ),
            /* 1437 */ new ud_itab_entry( "test", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 1438 */ new ud_itab_entry( "test", "Eb;Ib", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1439 */ new ud_itab_entry( "test", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1440 */ new ud_itab_entry( "test", "Ev;Iz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1441 */ new ud_itab_entry( "ucomisd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1442 */ new ud_itab_entry( "vucomisd", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1443 */ new ud_itab_entry( "ucomiss", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1444 */ new ud_itab_entry( "vucomiss", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1445 */ new ud_itab_entry( "ud2" ),
            /* 1446 */ new ud_itab_entry( "unpckhpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1447 */ new ud_itab_entry( "vunpckhpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1448 */ new ud_itab_entry( "unpckhps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1449 */ new ud_itab_entry( "vunpckhps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1450 */ new ud_itab_entry( "unpcklps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1451 */ new ud_itab_entry( "vunpcklps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1452 */ new ud_itab_entry( "unpcklpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1453 */ new ud_itab_entry( "vunpcklpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1454 */ new ud_itab_entry( "verr", "Ew", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1455 */ new ud_itab_entry( "verw", "Ew", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1456 */ new ud_itab_entry( "vmcall" ),
            /* 1457 */ new ud_itab_entry( "rdrand", "R", BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1458 */ new ud_itab_entry( "vmclear", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1459 */ new ud_itab_entry( "vmxon", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1460 */ new ud_itab_entry( "vmptrld", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1461 */ new ud_itab_entry( "vmptrst", "Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1462 */ new ud_itab_entry( "vmlaunch" ),
            /* 1463 */ new ud_itab_entry( "vmresume" ),
            /* 1464 */ new ud_itab_entry( "vmxoff" ),
            /* 1465 */ new ud_itab_entry( "vmread", "Ey;Gy", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1466 */ new ud_itab_entry( "vmwrite", "Gy;Ey", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_def64 ),
            /* 1467 */ new ud_itab_entry( "vmrun" ),
            /* 1468 */ new ud_itab_entry( "vmmcall" ),
            /* 1469 */ new ud_itab_entry( "vmload" ),
            /* 1470 */ new ud_itab_entry( "vmsave" ),
            /* 1471 */ new ud_itab_entry( "wait" ),
            /* 1472 */ new ud_itab_entry( "wbinvd" ),
            /* 1473 */ new ud_itab_entry( "wrmsr" ),
            /* 1474 */ new ud_itab_entry( "xadd", "Eb;Gb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1475 */ new ud_itab_entry( "xadd", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1476 */ new ud_itab_entry( "xchg", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1477 */ new ud_itab_entry( "xchg", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1478 */ new ud_itab_entry( "xchg", "R0v;rAX", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1479 */ new ud_itab_entry( "xchg", "R1v;rAX", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1480 */ new ud_itab_entry( "xchg", "R2v;rAX", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1481 */ new ud_itab_entry( "xchg", "R3v;rAX", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1482 */ new ud_itab_entry( "xchg", "R4v;rAX", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1483 */ new ud_itab_entry( "xchg", "R5v;rAX", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1484 */ new ud_itab_entry( "xchg", "R6v;rAX", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1485 */ new ud_itab_entry( "xchg", "R7v;rAX", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1486 */ new ud_itab_entry( "xgetbv" ),
            /* 1487 */ new ud_itab_entry( "xlatb", BitOps.P_rexw | BitOps.P_seg ),
            /* 1488 */ new ud_itab_entry( "xor", "Eb;Gb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1489 */ new ud_itab_entry( "xor", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1490 */ new ud_itab_entry( "xor", "Gb;Eb", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1491 */ new ud_itab_entry( "xor", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1492 */ new ud_itab_entry( "xor", "AL;Ib" ),
            /* 1493 */ new ud_itab_entry( "xor", "rAX;sIz", BitOps.P_oso | BitOps.P_rexw ),
            /* 1494 */ new ud_itab_entry( "xor", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1495 */ new ud_itab_entry( "xor", "Ev;sIz", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1496 */ new ud_itab_entry( "xor", "Eb;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_inv64 ),
            /* 1497 */ new ud_itab_entry( "xor", "Ev;sIb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1498 */ new ud_itab_entry( "xorpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1499 */ new ud_itab_entry( "vxorpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1500 */ new ud_itab_entry( "xorps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1501 */ new ud_itab_entry( "vxorps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1502 */ new ud_itab_entry( "xcryptecb" ),
            /* 1503 */ new ud_itab_entry( "xcryptcbc" ),
            /* 1504 */ new ud_itab_entry( "xcryptctr" ),
            /* 1505 */ new ud_itab_entry( "xcryptcfb" ),
            /* 1506 */ new ud_itab_entry( "xcryptofb" ),
            /* 1507 */ new ud_itab_entry( "xrstor", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1508 */ new ud_itab_entry( "xsave", "M", BitOps.P_aso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1509 */ new ud_itab_entry( "xsetbv" ),
            /* 1510 */ new ud_itab_entry( "xsha1" ),
            /* 1511 */ new ud_itab_entry( "xsha256" ),
            /* 1512 */ new ud_itab_entry( "xstore" ),
            /* 1513 */ new ud_itab_entry( "pclmulqdq", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1514 */ new ud_itab_entry( "vpclmulqdq", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1515 */ new ud_itab_entry( "getsec" ),
            /* 1516 */ new ud_itab_entry( "movdqa", "W;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1517 */ new ud_itab_entry( "vmovdqa", "Wx;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1518 */ new ud_itab_entry( "movdqa", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1519 */ new ud_itab_entry( "vmovdqa", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1520 */ new ud_itab_entry( "maskmovdqu", "V;U", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1521 */ new ud_itab_entry( "vmaskmovdqu", "Vx;Ux", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1522 */ new ud_itab_entry( "movdq2q", "P;U", BitOps.P_aso | BitOps.P_rexb ),
            /* 1523 */ new ud_itab_entry( "movdqu", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1524 */ new ud_itab_entry( "vmovdqu", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1525 */ new ud_itab_entry( "movdqu", "W;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1526 */ new ud_itab_entry( "vmovdqu", "Wx;Vx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1527 */ new ud_itab_entry( "movq2dq", "V;N", BitOps.P_aso | BitOps.P_rexr ),
            /* 1528 */ new ud_itab_entry( "paddq", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1529 */ new ud_itab_entry( "paddq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1530 */ new ud_itab_entry( "vpaddq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1531 */ new ud_itab_entry( "psubq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1532 */ new ud_itab_entry( "vpsubq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1533 */ new ud_itab_entry( "psubq", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1534 */ new ud_itab_entry( "pmuludq", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1535 */ new ud_itab_entry( "pmuludq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1536 */ new ud_itab_entry( "pshufhw", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1537 */ new ud_itab_entry( "vpshufhw", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1538 */ new ud_itab_entry( "pshuflw", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1539 */ new ud_itab_entry( "vpshuflw", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1540 */ new ud_itab_entry( "pshufd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1541 */ new ud_itab_entry( "vpshufd", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1542 */ new ud_itab_entry( "pslldq", "U;Ib", BitOps.P_rexb ),
            /* 1543 */ new ud_itab_entry( "vpslldq", "Hx;Ux;Ib", BitOps.P_rexb ),
            /* 1544 */ new ud_itab_entry( "psrldq", "U;Ib", BitOps.P_rexb ),
            /* 1545 */ new ud_itab_entry( "vpsrldq", "Hx;Ux;Ib", BitOps.P_rexb ),
            /* 1546 */ new ud_itab_entry( "punpckhqdq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1547 */ new ud_itab_entry( "vpunpckhqdq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1548 */ new ud_itab_entry( "punpcklqdq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1549 */ new ud_itab_entry( "vpunpcklqdq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1550 */ new ud_itab_entry( "haddpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1551 */ new ud_itab_entry( "vhaddpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1552 */ new ud_itab_entry( "haddps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1553 */ new ud_itab_entry( "vhaddps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1554 */ new ud_itab_entry( "hsubpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1555 */ new ud_itab_entry( "vhsubpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1556 */ new ud_itab_entry( "hsubps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1557 */ new ud_itab_entry( "vhsubps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1558 */ new ud_itab_entry( "insertps", "V;Md;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1559 */ new ud_itab_entry( "vinsertps", "Vx;Hx;Md;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1560 */ new ud_itab_entry( "lddqu", "V;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1561 */ new ud_itab_entry( "vlddqu", "Vx;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1562 */ new ud_itab_entry( "movddup", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1563 */ new ud_itab_entry( "vmovddup", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1564 */ new ud_itab_entry( "movddup", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1565 */ new ud_itab_entry( "vmovddup", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1566 */ new ud_itab_entry( "movshdup", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1567 */ new ud_itab_entry( "vmovshdup", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1568 */ new ud_itab_entry( "movshdup", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1569 */ new ud_itab_entry( "vmovshdup", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1570 */ new ud_itab_entry( "movsldup", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1571 */ new ud_itab_entry( "vmovsldup", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1572 */ new ud_itab_entry( "movsldup", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1573 */ new ud_itab_entry( "vmovsldup", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1574 */ new ud_itab_entry( "pabsb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1575 */ new ud_itab_entry( "pabsb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1576 */ new ud_itab_entry( "vpabsb", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1577 */ new ud_itab_entry( "pabsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1578 */ new ud_itab_entry( "pabsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1579 */ new ud_itab_entry( "vpabsw", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1580 */ new ud_itab_entry( "pabsd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1581 */ new ud_itab_entry( "pabsd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1582 */ new ud_itab_entry( "vpabsd", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1583 */ new ud_itab_entry( "pshufb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1584 */ new ud_itab_entry( "pshufb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1585 */ new ud_itab_entry( "vpshufb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1586 */ new ud_itab_entry( "phaddw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1587 */ new ud_itab_entry( "phaddw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1588 */ new ud_itab_entry( "vphaddw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1589 */ new ud_itab_entry( "phaddd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1590 */ new ud_itab_entry( "phaddd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1591 */ new ud_itab_entry( "vphaddd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1592 */ new ud_itab_entry( "phaddsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1593 */ new ud_itab_entry( "phaddsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1594 */ new ud_itab_entry( "vphaddsw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1595 */ new ud_itab_entry( "pmaddubsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1596 */ new ud_itab_entry( "pmaddubsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1597 */ new ud_itab_entry( "vpmaddubsw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1598 */ new ud_itab_entry( "phsubw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1599 */ new ud_itab_entry( "phsubw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1600 */ new ud_itab_entry( "vphsubw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1601 */ new ud_itab_entry( "phsubd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1602 */ new ud_itab_entry( "phsubd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1603 */ new ud_itab_entry( "vphsubd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1604 */ new ud_itab_entry( "phsubsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1605 */ new ud_itab_entry( "phsubsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1606 */ new ud_itab_entry( "vphsubsw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1607 */ new ud_itab_entry( "psignb", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1608 */ new ud_itab_entry( "psignb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1609 */ new ud_itab_entry( "vpsignb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1610 */ new ud_itab_entry( "psignd", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1611 */ new ud_itab_entry( "psignd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1612 */ new ud_itab_entry( "vpsignd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1613 */ new ud_itab_entry( "psignw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1614 */ new ud_itab_entry( "psignw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1615 */ new ud_itab_entry( "vpsignw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1616 */ new ud_itab_entry( "pmulhrsw", "P;Q", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1617 */ new ud_itab_entry( "pmulhrsw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1618 */ new ud_itab_entry( "vpmulhrsw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1619 */ new ud_itab_entry( "palignr", "P;Q;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1620 */ new ud_itab_entry( "palignr", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1621 */ new ud_itab_entry( "vpalignr", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1622 */ new ud_itab_entry( "pblendvb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1623 */ new ud_itab_entry( "pmuldq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1624 */ new ud_itab_entry( "vpmuldq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1625 */ new ud_itab_entry( "pminsb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1626 */ new ud_itab_entry( "vpminsb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1627 */ new ud_itab_entry( "pminsd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1628 */ new ud_itab_entry( "vpminsd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1629 */ new ud_itab_entry( "pminuw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1630 */ new ud_itab_entry( "vpminuw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1631 */ new ud_itab_entry( "pminud", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1632 */ new ud_itab_entry( "vpminud", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1633 */ new ud_itab_entry( "pmaxsb", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1634 */ new ud_itab_entry( "vpmaxsb", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1635 */ new ud_itab_entry( "pmaxsd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1636 */ new ud_itab_entry( "vpmaxsd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1637 */ new ud_itab_entry( "pmaxud", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1638 */ new ud_itab_entry( "vpmaxud", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1639 */ new ud_itab_entry( "pmaxuw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1640 */ new ud_itab_entry( "vpmaxuw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1641 */ new ud_itab_entry( "pmulld", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1642 */ new ud_itab_entry( "vpmulld", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1643 */ new ud_itab_entry( "phminposuw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1644 */ new ud_itab_entry( "vphminposuw", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1645 */ new ud_itab_entry( "roundps", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1646 */ new ud_itab_entry( "vroundps", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1647 */ new ud_itab_entry( "roundpd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1648 */ new ud_itab_entry( "vroundpd", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1649 */ new ud_itab_entry( "roundss", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1650 */ new ud_itab_entry( "vroundss", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1651 */ new ud_itab_entry( "roundsd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1652 */ new ud_itab_entry( "vroundsd", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1653 */ new ud_itab_entry( "blendpd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1654 */ new ud_itab_entry( "vblendpd", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1655 */ new ud_itab_entry( "blendps", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1656 */ new ud_itab_entry( "vblendps", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1657 */ new ud_itab_entry( "blendvpd", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1658 */ new ud_itab_entry( "blendvps", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1659 */ new ud_itab_entry( "bound", "Gv;M", BitOps.P_aso | BitOps.P_oso ),
            /* 1660 */ new ud_itab_entry( "bsf", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1661 */ new ud_itab_entry( "bsr", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1662 */ new ud_itab_entry( "bswap", "R0y", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1663 */ new ud_itab_entry( "bswap", "R1y", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1664 */ new ud_itab_entry( "bswap", "R2y", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1665 */ new ud_itab_entry( "bswap", "R3y", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1666 */ new ud_itab_entry( "bswap", "R4y", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1667 */ new ud_itab_entry( "bswap", "R5y", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1668 */ new ud_itab_entry( "bswap", "R6y", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1669 */ new ud_itab_entry( "bswap", "R7y", BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexb ),
            /* 1670 */ new ud_itab_entry( "bt", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1671 */ new ud_itab_entry( "bt", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1672 */ new ud_itab_entry( "btc", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1673 */ new ud_itab_entry( "btc", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1674 */ new ud_itab_entry( "btr", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1675 */ new ud_itab_entry( "btr", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1676 */ new ud_itab_entry( "bts", "Ev;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1677 */ new ud_itab_entry( "bts", "Ev;Ib", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexw | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1678 */ new ud_itab_entry( "pblendw", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1679 */ new ud_itab_entry( "vpblendw", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1680 */ new ud_itab_entry( "mpsadbw", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1681 */ new ud_itab_entry( "vmpsadbw", "Vx;Hx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1682 */ new ud_itab_entry( "movntdqa", "V;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1683 */ new ud_itab_entry( "vmovntdqa", "Vx;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1684 */ new ud_itab_entry( "packusdw", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1685 */ new ud_itab_entry( "vpackusdw", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1686 */ new ud_itab_entry( "pmovsxbw", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1687 */ new ud_itab_entry( "vpmovsxbw", "Vx;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1688 */ new ud_itab_entry( "pmovsxbd", "V;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1689 */ new ud_itab_entry( "vpmovsxbd", "Vx;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1690 */ new ud_itab_entry( "pmovsxbq", "V;MwU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1691 */ new ud_itab_entry( "vpmovsxbq", "Vx;MwU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1692 */ new ud_itab_entry( "pmovsxwd", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1693 */ new ud_itab_entry( "vpmovsxwd", "Vx;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1694 */ new ud_itab_entry( "pmovsxwq", "V;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1695 */ new ud_itab_entry( "vpmovsxwq", "Vx;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1696 */ new ud_itab_entry( "pmovsxdq", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1697 */ new ud_itab_entry( "pmovzxbw", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1698 */ new ud_itab_entry( "vpmovzxbw", "Vx;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1699 */ new ud_itab_entry( "pmovzxbd", "V;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1700 */ new ud_itab_entry( "vpmovzxbd", "Vx;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1701 */ new ud_itab_entry( "pmovzxbq", "V;MwU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1702 */ new ud_itab_entry( "vpmovzxbq", "Vx;MwU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1703 */ new ud_itab_entry( "pmovzxwd", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1704 */ new ud_itab_entry( "vpmovzxwd", "Vx;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1705 */ new ud_itab_entry( "pmovzxwq", "V;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1706 */ new ud_itab_entry( "vpmovzxwq", "Vx;MdU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1707 */ new ud_itab_entry( "pmovzxdq", "V;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1708 */ new ud_itab_entry( "vpmovzxdq", "Vx;MqU", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1709 */ new ud_itab_entry( "pcmpeqq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1710 */ new ud_itab_entry( "vpcmpeqq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1711 */ new ud_itab_entry( "popcnt", "Gv;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1712 */ new ud_itab_entry( "ptest", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1713 */ new ud_itab_entry( "vptest", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1714 */ new ud_itab_entry( "pcmpestri", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1715 */ new ud_itab_entry( "vpcmpestri", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1716 */ new ud_itab_entry( "pcmpestrm", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1717 */ new ud_itab_entry( "vpcmpestrm", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1718 */ new ud_itab_entry( "pcmpgtq", "V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1719 */ new ud_itab_entry( "vpcmpgtq", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1720 */ new ud_itab_entry( "pcmpistri", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1721 */ new ud_itab_entry( "vpcmpistri", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1722 */ new ud_itab_entry( "pcmpistrm", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1723 */ new ud_itab_entry( "vpcmpistrm", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1724 */ new ud_itab_entry( "movbe", "Gv;Mv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1725 */ new ud_itab_entry( "movbe", "Mv;Gv", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1726 */ new ud_itab_entry( "crc32", "Gy;Eb", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1727 */ new ud_itab_entry( "crc32", "Gy;Ev", BitOps.P_aso | BitOps.P_oso | BitOps.P_rexr | BitOps.P_rexw | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1728 */ new ud_itab_entry( "vbroadcastss", "V;Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1729 */ new ud_itab_entry( "vbroadcastsd", "Vqq;Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1730 */ new ud_itab_entry( "vextractf128", "Wdq;Vqq;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1731 */ new ud_itab_entry( "vinsertf128", "Vqq;Hqq;Wdq;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1732 */ new ud_itab_entry( "vmaskmovps", "V;H;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1733 */ new ud_itab_entry( "vmaskmovps", "M;H;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1734 */ new ud_itab_entry( "vmaskmovpd", "V;H;M", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1735 */ new ud_itab_entry( "vmaskmovpd", "M;H;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1736 */ new ud_itab_entry( "vpermilpd", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1737 */ new ud_itab_entry( "vpermilpd", "V;W;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1738 */ new ud_itab_entry( "vpermilps", "Vx;Hx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1739 */ new ud_itab_entry( "vpermilps", "Vx;Wx;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1740 */ new ud_itab_entry( "vperm2f128", "Vqq;Hqq;Wqq;Ib", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1741 */ new ud_itab_entry( "vtestps", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1742 */ new ud_itab_entry( "vtestpd", "Vx;Wx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1743 */ new ud_itab_entry( "vzeroupper" ),
            /* 1744 */ new ud_itab_entry( "vzeroall" ),
            /* 1745 */ new ud_itab_entry( "vblendvpd", "Vx;Hx;Wx;Lx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1746 */ new ud_itab_entry( "vblendvps", "Vx;Hx;Wx;Lx", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb | BitOps.P_vexl ),
            /* 1747 */ new ud_itab_entry( "vmovsd", "V;H;U", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1748 */ new ud_itab_entry( "vmovsd", "V;Mq", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1749 */ new ud_itab_entry( "vmovsd", "U;H;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1750 */ new ud_itab_entry( "vmovsd", "Mq;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1751 */ new ud_itab_entry( "vmovss", "V;H;U", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1752 */ new ud_itab_entry( "vmovss", "V;Md", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1753 */ new ud_itab_entry( "vmovss", "U;H;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1754 */ new ud_itab_entry( "vmovss", "Md;V", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1755 */ new ud_itab_entry( "vpblendvb", "V;H;W;L", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1756 */ new ud_itab_entry( "vpsllw", "V;H;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1757 */ new ud_itab_entry( "vpsllw", "H;V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1758 */ new ud_itab_entry( "vpslld", "V;H;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1759 */ new ud_itab_entry( "vpslld", "H;V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1760 */ new ud_itab_entry( "vpsllq", "V;H;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
            /* 1761 */ new ud_itab_entry( "vpsllq", "H;V;W", BitOps.P_aso | BitOps.P_rexr | BitOps.P_rexx | BitOps.P_rexb ),
        };
        #endregion

        static InstructionTables()
        {
            foreach ((ud_table_type ud_type, ushort[] ids) in ud_table_type_ids)
            {
                for (int idx = 0; idx < ids.Length; idx++)
                {
                    ushort id = ids[idx];
                    ud_lookup_table_type_dict.Add(id, ud_type);
                }
            }
        }
    }

    #region Enums
    public enum ud_table_type
    {
        [Description("opctbl")]
        UD_TAB__OPC_TABLE,
        [Description("/sse")]
        UD_TAB__OPC_SSE,
        [Description("/reg")]
        UD_TAB__OPC_REG,
        [Description("/rm")]
        UD_TAB__OPC_RM,
        [Description("/mod")]
        UD_TAB__OPC_MOD,
        [Description("/m")]
        UD_TAB__OPC_MODE,
        [Description("/x87")]
        UD_TAB__OPC_X87,
        [Description("/a")]
        UD_TAB__OPC_ASIZE,
        [Description("/o")]
        UD_TAB__OPC_OSIZE,
        [Description("/3dnow")]
        UD_TAB__OPC_3DNOW,
        [Description("/vendor")]
        UD_TAB__OPC_VENDOR,
        [Description("/vex")]
        UD_TAB__OPC_VEX,
        [Description("/vexw")]
        UD_TAB__OPC_VEX_W,
        [Description("/vexl")]
        UD_TAB__OPC_VEX_L
    }
    #endregion
}
#pragma warning restore 1591
