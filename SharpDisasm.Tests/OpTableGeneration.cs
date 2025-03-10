﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections.Generic;

#pragma warning disable 1591
namespace SharpDisasm.Tests
{
    [TestClass]
    public class OpTableGeneration
    {
        private static string pushIndent = "";
        #region GENERATOR
        // copied from GENERATOR region in OpTable.tt

        public delegate void ErrorDelegate(string text);
        /// <summary>
        /// Represents the equivalent method available within .tt
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public delegate void WriteDelegate(string message, params object[] args);

        /// <summary>
        /// Represents the equivalent method available within .tt
        /// </summary>
        /// <param name="indent"></param>
        public delegate void PushIndentDelegate(string indent);

        /// <summary>
        /// Represents the equivalent method available within .tt
        /// </summary>
        public delegate void ClearIndentDelegate();


        /* Begin translation from ud_opcode.py */

        public class UdInsnDef
        {
            public string Mnemonic;
            public List<String> Prefixes = new List<string>();
            public List<String> Opcodes = new List<string>();
            public List<String> Operands = new List<string>();
            public List<String> CpuIds = new List<string>();
            public Dictionary<String, String> OpcExts = new Dictionary<string, string>();

            public UdInsnDef(string mnemonic, string[] prefixes, string[] opcodes, string[] operands, string[] cpuid)
            {
                Mnemonic = mnemonic;
                Prefixes.AddRange(prefixes);
                Opcodes.AddRange(opcodes);
                Operands.AddRange(operands);
                CpuIds.AddRange(cpuid);

                // Set opc exts
                foreach (var opc in this.Opcodes)
                {
                    if (opc.StartsWith("/"))
                    {
                        var split = opc.Split('=');
                        this.OpcExts[split[0]] = split[1];
                    }
                }
            }

            public UdInsnDef(string mnemonic, string[] pfx, string[] opcs, string[] opr, string vendor, string[] cpuid)
            {
                Mnemonic = mnemonic;
                Prefixes.AddRange(pfx);
                Opcodes.AddRange(opcs);
                Operands.AddRange(opr);
                CpuIds.AddRange(cpuid);
                this.Vendor = vendor;

                // Set opc exts
                foreach (var opc in this.Opcodes)
                {
                    if (opc.StartsWith("/"))
                    {
                        var split = opc.Split('=');
                        this.OpcExts[split[0]] = split[1];
                    }
                }
            }

            private string GetOpcExts(string name)
            {
                if (OpcExts.ContainsKey(name))
                    return OpcExts[name];
                else
                    return null;
            }

            string _vendor;
            public string Vendor { get { return _vendor ?? GetOpcExts("/vendor"); } private set { _vendor = value; } }

            public string Mode { get { return GetOpcExts("/m"); } }
            public string OSize { get { return GetOpcExts("/o"); } }
            public bool IsDef64()
            {
                return this.Prefixes.Contains("def64");
            }
            public override string ToString()
            {
                return Mnemonic + " " + String.Join(", ", Operands.ToArray()) + " " + String.Join(" ", Opcodes.ToArray());
            }

            public bool LookupPrefix(string prefix)
            {
                return Prefixes.Contains(prefix);
            }
        }

        /// <summary>
        /// A single table of instruction definitions, indexed by a decode field.
        /// </summary>
        public class UdOpcodeTable
        {
            public class CollisionException : Exception
            {
                public CollisionException()
                    : base()
                {

                }
                public CollisionException(string message)
                    : base(message)
                {

                }
            }

            public static ErrorDelegate Error;

            // IndexError -> IndexOutOfRangeException
            // vendor2idx -> uses OpcExtMap and OpcExtIndex instead
            // vex2idx    -> uses OpcExtMap and OpcExtIndex instead

            static Dictionary<string, Dictionary<string, string>> OpcExtIndex = new Dictionary<string, Dictionary<string, string>>()
            {
                // ssef2, ssef3, sse66
                {"sse", new Dictionary<string, string>() {
                    {"none", "00"},
                    {"f2"   , "01"}, 
                    {"f3"   , "02"}, 
                    {"66"   , "03"}
                }},
                // /mod=
                {"mod", new Dictionary<string, string>() {
                    {"!11"   , "00"}, 
                    {"11"    , "01"}
                }},

                // /m=, /o=, /a=
                {"mode", new Dictionary<string, string>() { 
                    {"16"    , "00"}, 
                    {"32"    , "01"}, 
                    {"64"    , "02"}
                }},

                {"vendor" , new Dictionary<string, string>() {
                    {"amd"   , "00"},
                    {"intel" , "01"},
                    {"any"   , "02"}
                }},

                {"vex", new Dictionary<string, string>() {
                    {"none"   , "00"}, 
                    {"0f"     , "01"}, 
                    {"0f38"   , "02"}, 
                    {"0f3a"   , "03"},
                    {"66"     , "04"}, 
                    {"66_0f"  , "05"}, 
                    {"66_0f38", "06"}, 
                    {"66_0f3a", "07"},
                    {"f3"     , "08"}, 
                    {"f3_0f"  , "09"}, 
                    {"f3_0f38", "0a"}, 
                    {"f3_0f3a", "0b"},
                    {"f2"     , "0c"}, 
                    {"f2_0f"  , "0d"}, 
                    {"f2_0f38", "0e"}, 
                    {"f2_0f3a", "0f"},
                }},
            };

            /// <summary>
            /// A mapping of opcode extensions to their representational values used in the opcode map.
            /// </summary>
            public static Dictionary<string, Func<string, string>> OpcExtMap = new Dictionary<string, Func<string, string>>
                {
                    {"/rm",     (v) => String.Format("{0:x2}", Int32.Parse(v, System.Globalization.NumberStyles.HexNumber))},
                    {"/x87",    (v) => String.Format("{0:x2}", Int32.Parse(v, System.Globalization.NumberStyles.HexNumber))},
                    {"/3dnow",  (v) => String.Format("{0:x2}", Int32.Parse(v, System.Globalization.NumberStyles.HexNumber))},
                    {"/reg",    (v) => String.Format("{0:x2}", Int32.Parse(v, System.Globalization.NumberStyles.HexNumber))},
                    // modrm.mod
                    // (!11, 11) => (00, 01)
                    {"/mod",    (v) => (v == "!11" ? "00" : "01")},
                    // Mode extensions:
                    // (16, 32, 64) => (00, 01, 02)
                    {"/o",      (v) => String.Format("{0:x2}", Int32.Parse(v) / 32)},
                    {"/a",      (v) => String.Format("{0:x2}", Int32.Parse(v) / 32)},
                    // Disassembly mode
                    // (!64, 64) => (00b, 01b)
                    {"/m",      (v) => (v == "64" ? "01" : "00")},
                    // SSE
                    // none => 0
                    // f2   => 1
                    // f3   => 2
                    // 66   => 3
                    {"/sse",    (v) => OpcExtIndex["sse"][v]},
                    // AVX
                    {"/vex",    (v) => {
                        if (!String.IsNullOrEmpty(v) && v.StartsWith("none_"))
                            v = v.Substring(5);
                        return OpcExtIndex["vex"][v];
                    }},
                    {"/vexw",   (v) => (v == "0" ? "00" : "01")},
                    {"/vexl",   (v) => (v == "0" ? "00" : "01")},
                    // Vendor
                    {"/vendor", (v) => OpcExtIndex["vendor"][v]},
                };

            struct TableInfo
            {
                public string Name;
                public int Size;

                public TableInfo(string name, int size)
                {
                    this.Name = name;
                    this.Size = size;
                }
            }

            static Dictionary<string, TableInfo> _TableInfo = new Dictionary<string, TableInfo>() {
                {"opctbl"    , new TableInfo("UD_TAB__OPC_TABLE",  256) },
                {"/sse"      , new TableInfo("UD_TAB__OPC_SSE",    4) },
                {"/reg"      , new TableInfo("UD_TAB__OPC_REG",    8) },
                {"/rm"       , new TableInfo("UD_TAB__OPC_RM",     8) },
                {"/mod"      , new TableInfo("UD_TAB__OPC_MOD",    2) },
                {"/m"        , new TableInfo("UD_TAB__OPC_MODE",   2) },
                {"/x87"      , new TableInfo("UD_TAB__OPC_X87",    64) },
                {"/a"        , new TableInfo("UD_TAB__OPC_ASIZE",  3) },
                {"/o"        , new TableInfo("UD_TAB__OPC_OSIZE",  3) },
                {"/3dnow"    , new TableInfo("UD_TAB__OPC_3DNOW",  256) },
                {"/vendor"   , new TableInfo("UD_TAB__OPC_VENDOR", 3) },
                {"/vex"      , new TableInfo("UD_TAB__OPC_VEX",    16)},
                {"/vexw"     , new TableInfo("UD_TAB__OPC_VEX_W",  2) },
                {"/vexl"     , new TableInfo("UD_TAB__OPC_VEX_L",  2) },
            };

            public UdOpcodeTable(string type)
            {
                if (!_TableInfo.ContainsKey(type))
                    Error("_TableInfo does not contain " + type);
                this.Type = type;
                this.Entries = new SortedDictionary<string, object>(new HexStringComparer());
            }

            private class HexStringComparer : IComparer<String>
            {
                public int Compare(String x, String y)
                {
                    int xInt = Int32.Parse(x, System.Globalization.NumberStyles.HexNumber);
                    int yInt = Int32.Parse(y, System.Globalization.NumberStyles.HexNumber);

                    return xInt.CompareTo(yInt);
                }
            }

            public SortedDictionary<string, object> Entries { get; private set; }

            public int Size { get { return _TableInfo[this.Type].Size; } }
            //public IEnumerable<KeyValuePair<string, object>> Entries { get { return _entries; } }
            public int NumEntries { get { return Entries.Keys.Count; } }
            public string Label { get { return _TableInfo[this.Type].Name; } }
            public string Type { get; private set; }
            public string Meta { get { return Type; } }
            public override string ToString()
            {
                return String.Format("table-{0}", Type);
            }

            public void Add(string opc, object obj)
            {
                string type = UdOpcodeTable.GetOpcodeType(opc);
                string idx = UdOpcodeTable.GetOpcodeIdx(opc);
                if (this.Type != type || Entries.ContainsKey(idx))
                    throw new CollisionException();
                Entries[idx] = obj;
            }

            public object Lookup(string opc)
            {
                string type = UdOpcodeTable.GetOpcodeType(opc);
                string idx = UdOpcodeTable.GetOpcodeIdx(opc);
                if (this.Type != type)
                    throw new CollisionException(String.Format("{0} <-> {1}", this.Type, type));
                if (Entries.ContainsKey(idx))
                    return Entries[idx];
                else
                    return null;
            }

            public object EntryAt(int index)
            {
                var key = String.Format("{0:x2}", index);
                if (Entries.ContainsKey(key))
                    return Entries[key];
                else
                    return null;
            }

            public void SetEntryAt(int index, object obj)
            {
                var keys = Entries.Keys.OrderBy(k => Int32.Parse(k, System.Globalization.NumberStyles.HexNumber)).ToArray();
                Entries[keys[index]] = obj;
            }

            public static string GetOpcodeType(string opc)
            {
                if (opc.StartsWith("/"))
                    return opc.Split('=')[0];
                else
                    return "opctbl";
            }

            public static string GetOpcodeIdx(string opc)
            {
                if (opc.StartsWith("/"))
                {
                    var split = opc.Split('=');
                    return OpcExtMap[split[0]](split[1]);
                }
                else
                {
                    return String.Format("{0:x2}", Int32.Parse(opc, System.Globalization.NumberStyles.HexNumber));
                }
            }
            public static List<(string name, string desc)> GetLabels()
            {
                return (from kv in _TableInfo
                        select (kv.Value.Name, kv.Key)).ToList();
            }
        }

        /// <summary>
        /// Collection of opcode tables
        /// </summary>
        public class UdOpcodeTables
        {
            public static ErrorDelegate Error;

            public class CollisionException : Exception
            {
                public CollisionException()
                    : base()
                {

                }
                public CollisionException(string message)
                    : base(message)
                {

                }
            }


            /// <summary>
            /// Create a new opcode table of a given type.
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            private UdOpcodeTable NewTable(string type)
            {
                var tbl = new UdOpcodeTable(type);
                _tables.Add(tbl);
                return tbl;
            }

            /// <summary>
            /// Recursively construct a tree entry mapping an array of opcodes to an object
            /// </summary>
            /// <param name="opcodes"></param>
            /// <param name="obj"></param>
            /// <returns></returns>
            private object MakeTree(string[] opcodes, object obj)
            {
                if (opcodes == null || opcodes.Length == 0)
                    return obj;

                var opc = opcodes[0];
                var tbl = NewTable(UdOpcodeTable.GetOpcodeType(opc));
                tbl.Add(opc, MakeTree(opcodes.Skip(1).ToArray(), obj));
                return tbl;
            }

            /// <summary>
            /// Walk down the opcode tree, starting at a given opcode table, given a string of opcodes.
            /// Return null if unable to walk, otherwise return the object at the leaf.
            /// </summary>
            /// <param name="tbl"></param>
            /// <param name="opcodes"></param>
            /// <returns></returns>
            private object Walk(UdOpcodeTable tbl, string[] opcodes)
            {
                var opc = opcodes[0];
                var e = tbl.Lookup(opc);
                if (e == null)
                    return null;
                else if (e is UdOpcodeTable && opcodes.Length > 1)
                    return Walk((UdOpcodeTable)e, opcodes.Skip(1).ToArray());
                return e;
            }

            /// <summary>
            /// Create a mapping from a given array of opcodes to an object in
            /// the opcode tree. Constructs tree branches as needed.
            /// </summary>
            /// <param name="tbl"></param>
            /// <param name="opcodes"></param>
            /// <param name="obj"></param>
            private void Map(UdOpcodeTable tbl, string[] opcodes, object obj)
            {
                var opc = opcodes[0];
                var e = tbl.Lookup(opc);
                if (e == null)
                    tbl.Add(opc, MakeTree(opcodes.Skip(1).ToArray(), obj));
                else
                {
                    if (opcodes.Length <= 1 || !(e is UdOpcodeTable))
                        throw new CollisionException();
                    Map((UdOpcodeTable)e, opcodes.Skip(1).ToArray(), obj);
                }
            }

            UdInsnDef _invalidInsn;
            public UdOpcodeTable Root;
            List<UdOpcodeTable> _tables = new List<UdOpcodeTable>();
            List<UdInsnDef> _insns = new List<UdInsnDef>();
            Dictionary<string, List<UdInsnDef>> _mnemonics = new Dictionary<string, List<UdInsnDef>>();

            public UdOpcodeTables(string xml)
            {
                // The root table is always a 256 entry opctbl, indexed by a plain opcode byte
                Root = new UdOpcodeTable("opctbl");

                // Add an invalid instruction entry without any mappings in the opcode tables.
                _invalidInsn = new UdInsnDef("invalid", new string[] { }, new string[] { }, new string[] { }, new string[] { });
                _insns.Add(_invalidInsn);

                // Construct UdOpcodeTables object from the given udis86 optable.xml
                foreach (UdInsnDef insn in ParseOptableXml(xml))
                {
                    AddInsnDef(insn);
                }
                PatchAvx2Byte();
                MergeSSENone();
                PrintStats();
            }

            private void PrintStats()
            {
                var tables = GetTableList();
                Log("stats:");
                Log(String.Format("  Num tables    = {0}", tables.Count));
                Log(String.Format("  Num insnDefs  = {0}", GetInsnList().Count));
                Log(String.Format("  Num insns     = {0}", GetMnemonicList().Count));

                var totalSize = 0;
                var totalEntries = 0;
                foreach (var table in tables)
                {
                    totalSize += table.Size;
                    totalEntries += table.NumEntries;
                }
                Log(String.Format("  Packing Ratio = {0}%", ((totalEntries * 100.0) / totalSize)));
                Log("--------------------");

                Pprint();
            }

            private void Pprint()
            {
                Action<UdOpcodeTable, string> printWalk = null;
                printWalk = (tbl, indent) =>
                {
                    var entries = tbl.Entries;
                    foreach (var kv in entries)
                    {
                        if (kv.Value is UdOpcodeTable)
                        {
                            Log(String.Format("{0}    |-<{1}> {2}", indent, kv.Key, kv.Value));
                            printWalk(kv.Value as UdOpcodeTable, indent + "    |");
                        }
                        else if (kv.Value is UdInsnDef)
                        {
                            Log(String.Format("{0}    |-<{1}> {2}", indent, kv.Key, kv.Value));
                        }
                    }
                };
                printWalk(Root, "");
            }

            /// <summary>
            /// Merge sse tables with only one entry for /sse=none
            /// </summary>
            private void MergeSSENone()
            {
                foreach (var table in _tables)
                {
                    List<string> keys = new List<String>();
                    List<object> sse = new List<object>();

                    foreach (var kv in table.Entries)
                    {
                        var e = kv.Value as UdOpcodeTable;
                        if (e != null && e.Type == "/sse")
                        {
                            if (e.NumEntries == 1)
                            {
                                var sseTbl = e.Lookup("/sse=none");
                                if (sseTbl != null)
                                {
                                    sse.Add(sseTbl);
                                    keys.Add(kv.Key);
                                }
                            }
                        }
                    }

                    for (var i = 0; i < keys.Count; i++)
                    {
                        table.Entries[keys[i]] = sse[i];
                    }
                }
                List<UdOpcodeTable> uniqTables = new List<UdOpcodeTable>();
                Action<UdOpcodeTable> genTableList = null;
                genTableList = (tbl) =>
                {
                    if (!uniqTables.Contains(tbl))
                    {
                        _tables.Add(tbl);
                    }
                    uniqTables.Add(tbl);
                    var keys = tbl.Entries.Keys.OrderBy(k => Int32.Parse(k, System.Globalization.NumberStyles.HexNumber)).ToArray();
                    foreach (var k in keys)
                    {
                        var e = tbl.Entries[k] as UdOpcodeTable;
                        if (e != null)
                            genTableList(e);
                    }
                };
                _tables = new List<UdOpcodeTable>();
                genTableList(Root);
            }

            private void PatchAvx2Byte()
            {
                // create avx tables
                foreach (var pp in new string[] { null, "f2", "f3", "66" })
                    foreach (var m in new string[] { null, "0f", "0f38", "0f3a" })
                    {
                        string vex = String.Empty;
                        if (pp == null && m == null)
                            continue;
                        if (pp == null)
                            vex = m;
                        else if (m == null)
                            vex = pp;
                        else
                            vex = pp + "_" + m;
                        var table = Walk(Root, new string[] { "c4", "/vex=" + vex });
                        Map(Root, new string[] { "c5", "/vex=" + vex }, table);
                    }
            }

            private void AddInsn(UdInsnDef insnDef, Dictionary<string, string> opcexts)
            {
                //var opcodes = insnDef.Opcodes;

                // Re-order vex
                if (opcexts.ContainsKey("/vex"))
                {
                    if (insnDef.Opcodes[0] == "c4" || insnDef.Opcodes[0] == "c5")
                        insnDef.Opcodes.Insert(1, "/vex=" + opcexts["/vex"]);
                    else
                        Error("Expected either c$ or c5 opcode along with /vex extension");
                }

                // Add extensions. The order is important, and determines how
                // well the opcode table is packed. Also note, /sse must be
                // before /o, because /sse may consume operand size prefix and
                // affect the outcome of /o.
                foreach (var ext in new string[] {"/mod", "/x87", "/reg", "/rm", "/sse", "/o", "/a", "/m",
                    "/vexw", "/vexl", "/3dnow", "/vendor"})
                {
                    if (opcexts.ContainsKey(ext))
                    {
                        insnDef.Opcodes.Add(ext + "=" + opcexts[ext]);
                    }
                }

                // Re-create instruction now that opcodes and opcode extensions have been sorted out
                // note: Opcodes currently contains both opcodes and opcexts and will be split within
                //       the constructor
                insnDef = new UdInsnDef(
                    insnDef.Mnemonic,
                    insnDef.Prefixes.ToArray(),
                    insnDef.Opcodes.ToArray(),
                    insnDef.Operands.ToArray(),
                    insnDef.CpuIds.ToArray());

                Map(Root, insnDef.Opcodes.ToArray(), insnDef);

                _insns.Add(insnDef);
                // Add to lookup by mnemonic structure
                if (!_mnemonics.ContainsKey(insnDef.Mnemonic))
                    _mnemonics[insnDef.Mnemonic] = new List<UdInsnDef>(new UdInsnDef[] { insnDef });
                else
                    _mnemonics[insnDef.Mnemonic].Add(insnDef);
            }

            private void AddInsnDef(UdInsnDef insnDef)
            {
                List<string> opcodes = new List<string>();
                Dictionary<string, string> opcexts = new Dictionary<string, string>();

                // pack plain opcodes first, and collect opcode extensions
                foreach (var opc in insnDef.Opcodes)
                {
                    if (!opc.StartsWith("/"))
                    {
                        opcodes.Add(opc);
                    }
                    else
                    {
                        var split = opc.Split('=');
                        opcexts[split[0]] = split[1];
                    }
                }

                // Treat vendor as an opcode extension
                if (!String.IsNullOrEmpty(insnDef.Vendor))
                {
                    opcexts["/vendor"] = insnDef.Vendor;
                }

                if (insnDef.Mnemonic == "lds" || insnDef.Mnemonic == "les")
                {
                    // Massage lds and les, which share the same prefix as AVX instructions
                    // in order to work well with the opcode tree.
                    opcexts["/vex"] = "none";
                }
                else if (opcexts.ContainsKey("/vex"))
                {
                    // A proper avx instruction definition, make sure there are
                    // no legacy opcode extensions
                    if (opcexts.ContainsKey("/sse"))
                        Error("/sse can't be combined with /vex");

                    // make sure the opcode definitions don't already include
                    // the avx prefixes
                    if (opcodes.Count > 0 && (opcodes[0] == "c4" || opcodes[0] == "c5"))
                        Error("Cannot apply /vex as already includes the c4 or c5 prefix");

                    // An AVX only instruction is defined by the /vex= opcode
                    // extension. They do not include the c4 (long form) or
                    // c5 (short form) prefix. As part of opcode table generate,
                    // here we create the long form definition, and then patch
                    // the table for c5 in a later stage.
                    // Construct a long-form definition of the avx instruction
                    opcodes.Insert(0, "c4");
                }
                else if (!opcexts.ContainsKey("/sse") && (opcodes.Count > 0 && opcodes[0] == "0f") && (opcodes.Count < 2 || opcodes[1] != "0f"))
                {
                    // Make all 2-byte opcode form instructions play nice with sse opcode maps
                    opcexts["/sse"] = "none";
                }

                insnDef.Opcodes.Clear();
                insnDef.Opcodes.AddRange(opcodes);

                // Legacy sse defs that get promoted to avx
                Action<UdInsnDef, Dictionary<string, string>> fn = AddInsn;
                if (insnDef.CpuIds.Contains("avx") && opcexts.ContainsKey("/sse"))
                    fn = AddSSE2AVXInsn;

                fn(insnDef, opcexts);
            }

            /// <summary>
            /// Add an instruction definition containing an avx cpuid bit, but 
            /// declared in its legacy SSE form. The function splits the definition to create two new
            /// definitions, one for SSE and one promoted to an AVX form.
            /// </summary>
            /// <param name="insnDef"></param>
            /// <param name="opcexts"></param>
            private void AddSSE2AVXInsn(UdInsnDef insnDef, Dictionary<string, string> opcexts)
            {
                // SSE
                var sseMnemonic = insnDef.Mnemonic;
                var sseOpcodes = insnDef.Opcodes.ToArray();
                // Remove vex opcode extensions
                var sseOpcexts = new Dictionary<string, string>();
                foreach (var kv in opcexts)
                {
                    if (!kv.Key.StartsWith("/vex"))
                        sseOpcexts.Add(kv.Key, kv.Value);
                }
                // strip out avx operands, preserving relative ordering
                // of remaining operands
                var sseOperands = (from opr in insnDef.Operands
                                   where opr != "H" && opr != "L"
                                   select opr).ToArray();
                // strip out avx prefixes
                var ssePrefixes = (from pfx in insnDef.Prefixes
                                   where !pfx.StartsWith("vex")
                                   select pfx).ToArray();
                // strip out avx bits from cpuid
                var sseCpuid = (from cpu in insnDef.CpuIds
                                where !cpu.StartsWith("avx")
                                select cpu).ToArray();

                AddInsn(new UdInsnDef(sseMnemonic, ssePrefixes, sseOpcodes, sseOperands, sseCpuid), sseOpcexts);

                // AVX
                var vexMnemonic = "v" + insnDef.Mnemonic;
                var vexPrefixes = insnDef.Prefixes.ToArray();
                var vexopcodes = new string[] { "c4" };
                var vexopcexts = new Dictionary<string, string>();
                foreach (var kv in opcexts)
                {
                    if (!kv.Key.StartsWith("/sse"))
                        vexopcexts[kv.Key] = kv.Value;
                }
                vexopcexts["/vex"] = opcexts["/sse"] + "_" + "0f";
                if (insnDef.Opcodes.Count > 1 && (insnDef.Opcodes[1] == "38" || insnDef.Opcodes[1] == "3a"))
                {
                    vexopcexts["/vex"] += insnDef.Opcodes[1];
                    vexopcodes = vexopcodes.Concat(insnDef.Opcodes.Skip(2).ToArray()).ToArray();
                }
                else
                {
                    vexopcodes = vexopcodes.Concat(insnDef.Opcodes.Skip(1).ToArray()).ToArray();
                }
                var vexoperands = new List<String>();
                foreach (var o in insnDef.Operands)
                {
                    var opr = o;
                    // make the operand size explicit: x
                    if (new string[] { "V", "W", "H", "U" }.Contains(opr))
                        opr = opr + "x";
                    vexoperands.Add(opr);
                }
                var vexcpuid = (from cpu in insnDef.CpuIds
                                where !cpu.StartsWith("sse")
                                select cpu).ToArray();

                AddInsn(new UdInsnDef(vexMnemonic, vexPrefixes, vexopcodes, vexoperands.ToArray(), vexcpuid), vexopcexts);
            }

            /// <summary>
            /// Returns a list of all instructions in the collection
            /// </summary>
            /// <returns></returns>
            public List<UdInsnDef> GetInsnList()
            {
                return _insns;
            }

            /// <summary>
            /// Returns a list of all tables in the collection
            /// </summary>
            /// <returns></returns>
            public List<UdOpcodeTable> GetTableList()
            {
                return _tables;
            }

            public List<string> GetMnemonicList()
            {
                return (from kv in _mnemonics
                        orderby kv.Key ascending
                        select kv.Key).ToList();
            }

            public static bool LogEnabled = false;
            private void Log(string s)
            {
                if (LogEnabled)
                    System.IO.File.AppendAllText("opcodeTables.log", s + "\n");
            }

            /// <summary>
            /// Parse udis86 optable.xml file and return list of instruction definitions
            /// </summary>
            /// <param name="xml"></param>
            /// <returns></returns>
            private static IEnumerable<UdInsnDef> ParseOptableXml(string xml)
            {
                XDocument doc = XDocument.Parse(xml);

                XNode topNode = doc.FirstNode;

                while (topNode != null && ((topNode as XElement) == null || (topNode as XElement).Name != "x86optable"))
                    topNode = topNode.NextNode;

                if (topNode as XElement != null)
                {
                    foreach (var instruction in (topNode as XElement).Elements())
                    {
                        if (String.IsNullOrEmpty(instruction.Name.ToString()))
                            continue;

                        if (instruction.Name != "instruction")
                        {
                            Error("Invalid instruction node in XML - " + instruction.Name);
                            continue;
                        }

                        string mnemonic = (from n in instruction.Elements(XName.Get("mnemonic", ""))
                                           select n.Value).FirstOrDefault();
                        string vendor = String.Empty;
                        string[] cpuid = new string[] { };

                        foreach (var node in instruction.Elements())
                        {
                            if (node.Name == "vendor")
                                vendor = node.Value;
                            else if (node.Name == "cpuid")
                                cpuid = node.Value.Split(' ');
                        }

                        foreach (var node in instruction.Elements())
                        {
                            if (node.Name == "def")
                            {
                                string[] pfx = new string[] { };
                                string[] opc = new string[] { };
                                string[] opr = new string[] { };

                                string localVendor = null;
                                string[] localCpuid = null;
                                foreach (var def in node.Elements())
                                {
                                    if (def.Name == "pfx")
                                        pfx = def.Value.Split(' ');
                                    else if (def.Name == "opc")
                                        opc = def.Value.Split(' ');
                                    else if (def.Name == "opr")
                                        opr = def.Value.Split(' ');
                                    else if (def.Name == "mode")
                                        pfx = pfx.Concat(def.Value.Split(' ')).ToArray();
                                    else if (def.Name == "cpuid")
                                        localCpuid = def.Value.Split(' ');
                                    else if (def.Name == "vendor")
                                        localVendor = def.Value;
                                }
                                yield return new UdInsnDef(mnemonic, pfx, opc, opr, localVendor ?? vendor, localCpuid ?? cpuid);
                            }
                        }
                    }
                }
            }
        }

        /* Begin translation from ud_itab.py */

        public class UdItabGenerator
        {
            #region Dictionaries and static data

            #region OperandDict
            public static Dictionary<string, string[]> OperandDict = new Dictionary<string, string[]>() {
                {"Av"       , new string[] {    "OP_A"        , "SZ_V"     }},
                {"E"        , new string[] {    "OP_E"        , "SZ_NA"    }},
                {"Eb"       , new string[] {    "OP_E"        , "SZ_B"     }},
                {"Ew"       , new string[] {    "OP_E"        , "SZ_W"     }},
                {"Ev"       , new string[] {    "OP_E"        , "SZ_V"     }},
                {"Ed"       , new string[] {    "OP_E"        , "SZ_D"     }},
                {"Ey"       , new string[] {    "OP_E"        , "SZ_Y"     }},
                {"Eq"       , new string[] {    "OP_E"        , "SZ_Q"     }},
                {"Ez"       , new string[] {    "OP_E"        , "SZ_Z"     }},
                {"Fv"       , new string[] {    "OP_F"        , "SZ_V"     }},
                {"G"        , new string[] {    "OP_G"        , "SZ_NA"    }},
                {"Gb"       , new string[] {    "OP_G"        , "SZ_B"     }},
                {"Gw"       , new string[] {    "OP_G"        , "SZ_W"     }},
                {"Gv"       , new string[] {    "OP_G"        , "SZ_V"     }},
                {"Gy"       , new string[] {    "OP_G"        , "SZ_Y"     }},
                {"Gd"       , new string[] {    "OP_G"        , "SZ_D"     }},
                {"Gq"       , new string[] {    "OP_G"        , "SZ_Q"     }},
                {"Gz"       , new string[] {    "OP_G"        , "SZ_Z"     }},
                {"M"        , new string[] {    "OP_M"        , "SZ_NA"    }},
                {"Mb"       , new string[] {    "OP_M"        , "SZ_B"     }},
                {"Mw"       , new string[] {    "OP_M"        , "SZ_W"     }},
                {"Ms"       , new string[] {    "OP_M"        , "SZ_W"     }},
                {"Md"       , new string[] {    "OP_M"        , "SZ_D"     }},
                {"Mq"       , new string[] {    "OP_M"        , "SZ_Q"     }},
                {"Mdq"      , new string[] {    "OP_M"        , "SZ_DQ"    }},
                {"Mv"       , new string[] {    "OP_M"        , "SZ_V"     }},
                {"Mt"       , new string[] {    "OP_M"        , "SZ_T"     }},
                {"Mo"       , new string[] {    "OP_M"        , "SZ_O"     }},
                {"MbRd"     , new string[] {    "OP_MR"       , "SZ_BD"    }},
                {"MbRv"     , new string[] {    "OP_MR"       , "SZ_BV"    }},
                {"MwRv"     , new string[] {    "OP_MR"       , "SZ_WV"    }},
                {"MwRd"     , new string[] {    "OP_MR"       , "SZ_WD"    }},
                {"MwRy"     , new string[] {    "OP_MR"       , "SZ_WY"    }},
                {"MdRy"     , new string[] {    "OP_MR"       , "SZ_DY"    }},
                {"I1"       , new string[] {    "OP_I1"       , "SZ_NA"    }},
                {"I3"       , new string[] {    "OP_I3"       , "SZ_NA"    }},
                {"Ib"       , new string[] {    "OP_I"        , "SZ_B"     }},
                {"Iw"       , new string[] {    "OP_I"        , "SZ_W"     }},
                {"Iv"       , new string[] {    "OP_I"        , "SZ_V"     }},
                {"Iz"       , new string[] {    "OP_I"        , "SZ_Z"     }},
                {"sIb"      , new string[] {    "OP_sI"       , "SZ_B"     }},
                {"sIz"      , new string[] {    "OP_sI"       , "SZ_Z"     }},
                {"sIv"      , new string[] {    "OP_sI"       , "SZ_V"     }},
                {"Jv"       , new string[] {    "OP_J"        , "SZ_V"     }},
                {"Jz"       , new string[] {    "OP_J"        , "SZ_Z"     }},
                {"Jb"       , new string[] {    "OP_J"        , "SZ_B"     }},
                {"R"        , new string[] {    "OP_R"        , "SZ_RDQ"   }}, 
                {"C"        , new string[] {    "OP_C"        , "SZ_NA"    }},
                {"D"        , new string[] {    "OP_D"        , "SZ_NA"    }},
                {"S"        , new string[] {    "OP_S"        , "SZ_W"     }},
                {"Ob"       , new string[] {    "OP_O"        , "SZ_B"     }},
                {"Ow"       , new string[] {    "OP_O"        , "SZ_W"     }},
                {"Ov"       , new string[] {    "OP_O"        , "SZ_V"     }},
                {"U"        , new string[] {    "OP_U"        , "SZ_O"     }},
                {"Ux"       , new string[] {    "OP_U"        , "SZ_X"     }},
                {"V"        , new string[] {    "OP_V"        , "SZ_DQ"    }},
                {"Vdq"      , new string[] {    "OP_V"        , "SZ_DQ"    }},
                {"Vqq"      , new string[] {    "OP_V"        , "SZ_QQ"    }},
                {"Vsd"      , new string[] {    "OP_V"        , "SZ_Q"     }},
                {"Vx"       , new string[] {    "OP_V"        , "SZ_X"     }},
                {"H"        , new string[] {    "OP_H"        , "SZ_X"     }},
                {"Hx"       , new string[] {    "OP_H"        , "SZ_X"     }},
                {"Hqq"      , new string[] {    "OP_H"        , "SZ_QQ"    }},
                {"W"        , new string[] {    "OP_W"        , "SZ_DQ"    }},
                {"Wdq"      , new string[] {    "OP_W"        , "SZ_DQ"    }},
                {"Wqq"      , new string[] {    "OP_W"        , "SZ_QQ"    }},
                {"Wsd"      , new string[] {    "OP_W"        , "SZ_Q"     }},
                {"Wx"       , new string[] {    "OP_W"        , "SZ_X"     }},
                {"L"        , new string[] {    "OP_L"        , "SZ_O"     }},
                {"Lx"       , new string[] {    "OP_L"        , "SZ_X"     }},
                {"MwU"      , new string[] {    "OP_MU"       , "SZ_WO"    }},
                {"MdU"      , new string[] {    "OP_MU"       , "SZ_DO"    }},
                {"MqU"      , new string[] {    "OP_MU"       , "SZ_QO"    }},
                {"N"        , new string[] {    "OP_N"        , "SZ_Q"     }},
                {"P"        , new string[] {    "OP_P"        , "SZ_Q"     }},
                {"Q"        , new string[] {    "OP_Q"        , "SZ_Q"     }},
                {"AL"       , new string[] {    "OP_AL"       , "SZ_B"     }},
                {"AX"       , new string[] {    "OP_AX"       , "SZ_W"     }},
                {"eAX"      , new string[] {    "OP_eAX"      , "SZ_Z"     }},
                {"rAX"      , new string[] {    "OP_rAX"      , "SZ_V"     }},
                {"CL"       , new string[] {    "OP_CL"       , "SZ_B"     }},
                {"CX"       , new string[] {    "OP_CX"       , "SZ_W"     }},
                {"eCX"      , new string[] {    "OP_eCX"      , "SZ_Z"     }},
                {"rCX"      , new string[] {    "OP_rCX"      , "SZ_V"     }},
                {"DL"       , new string[] {    "OP_DL"       , "SZ_B"     }},
                {"DX"       , new string[] {    "OP_DX"       , "SZ_W"     }},
                {"eDX"      , new string[] {    "OP_eDX"      , "SZ_Z"     }},
                {"rDX"      , new string[] {    "OP_rDX"      , "SZ_V"     }},
                {"R0b"      , new string[] {    "OP_R0"       , "SZ_B"     }},
                {"R1b"      , new string[] {    "OP_R1"       , "SZ_B"     }},
                {"R2b"      , new string[] {    "OP_R2"       , "SZ_B"     }},
                {"R3b"      , new string[] {    "OP_R3"       , "SZ_B"     }},
                {"R4b"      , new string[] {    "OP_R4"       , "SZ_B"     }},
                {"R5b"      , new string[] {    "OP_R5"       , "SZ_B"     }},
                {"R6b"      , new string[] {    "OP_R6"       , "SZ_B"     }},
                {"R7b"      , new string[] {    "OP_R7"       , "SZ_B"     }},
                {"R0w"      , new string[] {    "OP_R0"       , "SZ_W"     }},
                {"R1w"      , new string[] {    "OP_R1"       , "SZ_W"     }},
                {"R2w"      , new string[] {    "OP_R2"       , "SZ_W"     }},
                {"R3w"      , new string[] {    "OP_R3"       , "SZ_W"     }},
                {"R4w"      , new string[] {    "OP_R4"       , "SZ_W"     }},
                {"R5w"      , new string[] {    "OP_R5"       , "SZ_W"     }},
                {"R6w"      , new string[] {    "OP_R6"       , "SZ_W"     }},
                {"R7w"      , new string[] {    "OP_R7"       , "SZ_W"     }},
                {"R0v"      , new string[] {    "OP_R0"       , "SZ_V"     }},
                {"R1v"      , new string[] {    "OP_R1"       , "SZ_V"     }},
                {"R2v"      , new string[] {    "OP_R2"       , "SZ_V"     }},
                {"R3v"      , new string[] {    "OP_R3"       , "SZ_V"     }},
                {"R4v"      , new string[] {    "OP_R4"       , "SZ_V"     }},
                {"R5v"      , new string[] {    "OP_R5"       , "SZ_V"     }},
                {"R6v"      , new string[] {    "OP_R6"       , "SZ_V"     }},
                {"R7v"      , new string[] {    "OP_R7"       , "SZ_V"     }},
                {"R0z"      , new string[] {    "OP_R0"       , "SZ_Z"     }},
                {"R1z"      , new string[] {    "OP_R1"       , "SZ_Z"     }},
                {"R2z"      , new string[] {    "OP_R2"       , "SZ_Z"     }},
                {"R3z"      , new string[] {    "OP_R3"       , "SZ_Z"     }},
                {"R4z"      , new string[] {    "OP_R4"       , "SZ_Z"     }},
                {"R5z"      , new string[] {    "OP_R5"       , "SZ_Z"     }},
                {"R6z"      , new string[] {    "OP_R6"       , "SZ_Z"     }},
                {"R7z"      , new string[] {    "OP_R7"       , "SZ_Z"     }},
                {"R0y"      , new string[] {    "OP_R0"       , "SZ_Y"     }},
                {"R1y"      , new string[] {    "OP_R1"       , "SZ_Y"     }},
                {"R2y"      , new string[] {    "OP_R2"       , "SZ_Y"     }},
                {"R3y"      , new string[] {    "OP_R3"       , "SZ_Y"     }},
                {"R4y"      , new string[] {    "OP_R4"       , "SZ_Y"     }},
                {"R5y"      , new string[] {    "OP_R5"       , "SZ_Y"     }},
                {"R6y"      , new string[] {    "OP_R6"       , "SZ_Y"     }},
                {"R7y"      , new string[] {    "OP_R7"       , "SZ_Y"     }},
                {"ES"       , new string[] {    "OP_ES"       , "SZ_NA"    }},
                {"CS"       , new string[] {    "OP_CS"       , "SZ_NA"    }},
                {"DS"       , new string[] {    "OP_DS"       , "SZ_NA"    }},
                {"SS"       , new string[] {    "OP_SS"       , "SZ_NA"    }},
                {"GS"       , new string[] {    "OP_GS"       , "SZ_NA"    }},
                {"FS"       , new string[] {    "OP_FS"       , "SZ_NA"    }},
                {"ST0"      , new string[] {    "OP_ST0"      , "SZ_NA"    }},
                {"ST1"      , new string[] {    "OP_ST1"      , "SZ_NA"    }},
                {"ST2"      , new string[] {    "OP_ST2"      , "SZ_NA"    }},
                {"ST3"      , new string[] {    "OP_ST3"      , "SZ_NA"    }},
                {"ST4"      , new string[] {    "OP_ST4"      , "SZ_NA"    }},
                {"ST5"      , new string[] {    "OP_ST5"      , "SZ_NA"    }},
                {"ST6"      , new string[] {    "OP_ST6"      , "SZ_NA"    }},
                {"ST7"      , new string[] {    "OP_ST7"      , "SZ_NA"    }},
                {"NONE"     , new string[] {    "OP_NONE"     , "SZ_NA"    }}
            };
            #endregion

            // opcode prefix dictionary
            public static Dictionary<string, string> PrefixDict = new Dictionary<string, string>() 
            { 
                {"rep"      , "P_str"},   
                {"repz"     , "P_strz"},   
                {"aso"      , "P_aso"},   
                {"oso"      , "P_oso"},   
                {"rexw"     , "P_rexw"}, 
                {"rexb"     , "P_rexb"},  
                {"rexx"     , "P_rexx"},  
                {"rexr"     , "P_rexr"},
                {"vexl"     , "P_vexl"},
                {"vexw"     , "P_vexw"},
                {"seg"      , "P_seg"},
                {"inv64"    , "P_inv64"}, 
                {"def64"    , "P_def64"}, 
                {"cast"     , "P_cast"}
            };

            static List<String> MnemonicAliases = new List<string>(new string[] { "invalid", "3dnow", "none", "db", "pause" });

            #endregion

            #region Emit delegates
            public static ClearIndentDelegate ClearIndent;
            public static PushIndentDelegate PushIndent;
            public static WriteDelegate Write;
            public static ErrorDelegate Error;
            #endregion

            UdOpcodeTables _tables;
            Dictionary<UdInsnDef, int> _insnIndexMap = new Dictionary<UdInsnDef, int>();
            Dictionary<UdOpcodeTable, int> _tableIndexMap = new Dictionary<UdOpcodeTable, int>();
            public UdItabGenerator(UdOpcodeTables tables)
            {
                _tables = tables;

                int i = 0;
                foreach (var insn in tables.GetInsnList())
                {
                    _insnIndexMap[insn] = i++;
                }
                i = 0;
                foreach (var table in tables.GetTableList())
                {
                    _tableIndexMap[table] = i++;
                }
            }

            int GetInsnIndex(UdInsnDef insn)
            {
                return _insnIndexMap[insn];
            }

            int GetTableIndex(UdOpcodeTable table)
            {
                return _tableIndexMap[table];
            }

            string GetTableName(UdOpcodeTable table)
            {
                return String.Format("ud_itab__{0}", GetTableIndex(table));
            }

            /// <summary>
            /// Emit Opcode Table in C#
            /// </summary>
            /// <param name="table"></param>
            /// <param name="isGlobal"></param>
            private void GenOpcodeTable(UdOpcodeTable table)
            {
                Write(pushIndent + "new ushort[] {{ //{0}\n", GetTableName(table));
                for (var i = 0; i < table.Size; i++)
                {
                    if (i > 0 && i % 4 == 0) Write("\n");
                    if (i % 4 == 0) Write(pushIndent + "  /* {0:x2} */", i);
                    var e = table.EntryAt(i);
                    if (e == null) Write("{0,12},", "INVALID");
                    else if (e is UdOpcodeTable) Write("{0,12},", String.Format("0x8000|{0}", GetTableIndex(e as UdOpcodeTable)));
                    else if (e is UdInsnDef) Write("{0,12},", GetInsnIndex(e as UdInsnDef));
                }
                Write("\n");
                Write(pushIndent + "},\n\n");
            }

            public void GenOpcodeTables()
            {
                pushIndent = "    ";
                Write(pushIndent + "internal static readonly ushort[][] ud_itabs = new ushort[][]\n");
                Write(pushIndent + "{\n");
                pushIndent = "        ";
                var tables = _tables.GetTableList();
                foreach (var table in tables) GenOpcodeTable(table);
                pushIndent = "    ";
                Write(pushIndent + "};\n");
            }

            public void GenOpcodeTablesLookupIndex()
            {
                Write(pushIndent + "internal static readonly (ud_table_type ud_type, ushort[] ids)[] ud_table_type_ids = new (ud_table_type, ushort[])[]\n");
                Write(pushIndent + "{\n");
                Dictionary<string, List<ushort>> tableDict = new Dictionary<string, List<ushort>>();
                foreach (var table in _tables.GetTableList())
                {
                    var label = table.Label;
                    ushort index = (ushort)GetTableIndex(table);
                    if (!tableDict.ContainsKey(label)) tableDict[label] = new List<ushort>();
                    tableDict[label].Add(index);
                }

                pushIndent = "        ";
                Write(String.Join(",\n", (from l in UdOpcodeTable.GetLabels()
                                            select string.Format(pushIndent + "(ud_table_type.{0}, new ushort[] {{{1}}})", l.name, string.Join(", ", tableDict[l.name]))).ToArray()));
                pushIndent = "    ";
                Write(pushIndent + "\n");
                Write(pushIndent + "};\n");
                Write(pushIndent + "internal static readonly Dictionary<ushort, ud_table_type> ud_lookup_table_type_dict = new Dictionary<ushort, ud_table_type>();\n");
            }

            public void GenInsnTable()
            {
                Write(pushIndent + "internal static readonly List<ud_itab_entry> ud_itab_entrys = new List<ud_itab_entry>()\n");
                Write(pushIndent + "{\n");
                pushIndent = "        ";
                foreach (var e in _tables.GetInsnList())
                {
                    string opr_s = string.Join(";", e.Operands);
                    List<string> pfx_c = new List<string>();

                    foreach (var p in e.Prefixes)
                    {
                        if (!PrefixDict.ContainsKey(p))
                            Error(String.Format("Error: invalid prefix specification: {0}", p));
                        else pfx_c.Add("BitOps." + PrefixDict[p]);
                    }
                    var pfx_s = String.Join(" | ", pfx_c.ToArray());
                    opr_s = opr_s.Length > 0 ? ", \"" + opr_s + "\"" : "";
                    pfx_s = pfx_s.Length > 0 ? ", " + pfx_s : "";

                    Write(pushIndent + "/* {0:D4} */ new ud_itab_entry( \"{1}\"{2}{3} ),\n", GetInsnIndex(e), e.Mnemonic, opr_s, pfx_s);
                }

                pushIndent = "    ";
                Write(pushIndent + "};\n");
            }

            public List<String> GetMnemonicsList()
            {
                var mnemonics = _tables.GetMnemonicList();
                return mnemonics.Concat(MnemonicAliases).ToList();
            }

            public void GenCSharpCode()
            {
                #region static class InstructionTables
                pushIndent = "    ";
                Write("internal static class InstructionTables\n");
                Write("{\n");
                Write(pushIndent + "#region Lookup Tables\n");
                Write(pushIndent + "public const int INVALID = 0;\n\n");

                GenOpcodeTables();
                Write(pushIndent + "#endregion\n\n");

                Write(pushIndent + "#region Lookup Table List\n");
                GenOpcodeTablesLookupIndex();
                Write(pushIndent + "#endregion\n\n");

                Write(pushIndent + "#region Operand Definitions\n");
                Write(pushIndent + "/// <summary>\n");
                Write(pushIndent + "/// itab entry operand definitions (for readability)\n");
                Write(pushIndent + "/// </summary>\n");
                Write(pushIndent + "internal static readonly Dictionary<string, ud_itab_entry_operand> OpDefDict = new Dictionary<string, ud_itab_entry_operand>()\n");
                Write(pushIndent + "{\n");
                // OpDefs (short-names for operands)
                var operands = (from k in OperandDict.Keys
                                orderby k ascending
                                select k).ToArray();

                pushIndent = "        ";
                foreach (var op in operands)
                {
                    Write(pushIndent + "{{{0,-6}, new ud_itab_entry_operand(ud_operand_code.{1,-8}, ud_operand_size.{2,-7})}},\n", "\"" + op + "\"", OperandDict[op][0], OperandDict[op][1]);
                }
                pushIndent = "    ";
                Write(pushIndent + "};\n");
                Write(pushIndent + "#endregion\n\n");

                Write(pushIndent + "#region Instruction Table and Mnemonics\n");
                GenInsnTable();
                Write(pushIndent + "#endregion\n\n");

                Write(pushIndent + "static InstructionTables()\n");
                Write(pushIndent + "{\n");
                pushIndent = "        ";
                Write(pushIndent + "foreach ((ud_table_type ud_type, ushort[] ids) in ud_table_type_ids)\n");
                Write(pushIndent + "{\n");
                Write(pushIndent + "    for (int idx = 0; idx < ids.Length; idx++)\n");
                Write(pushIndent + "    {\n");
                Write(pushIndent + "        ushort id = ids[idx];\n");
                Write(pushIndent + "        ud_lookup_table_type_dict.Add(id, ud_type);\n");
                Write(pushIndent + "    }\n");
                Write(pushIndent + "}\n");
                pushIndent = "    ";
                Write(pushIndent + "}\n");
                ClearIndent();
                Write("}\n\n"); // end class InstructionTables
                #endregion

                Write("#region Enums");
                Write("\n");
                Write("public enum ud_table_type\n");
                Write("{\n");
                pushIndent = "    ";
                Write(String.Join(",\n", (from l in UdOpcodeTable.GetLabels()
                                            select string.Format(pushIndent + "[Description(\"{0}\")]\n" + pushIndent  + "{1}", l.desc, l.name)).ToArray()));
                Write("\n");
                Write("}\n");
                Write("#endregion\n");
            }
        }

        #endregion

        [TestMethod]
        public void OpTableGenTest()
        {
            UdOpcodeTable.Error = Error;
            UdOpcodeTables.Error = Error;
            UdItabGenerator.Error = Error;
            UdItabGenerator.ClearIndent = ClearIndent;
            UdItabGenerator.PushIndent = PushIndent;
            UdItabGenerator.Write = Write;

            UdOpcodeTables.LogEnabled = false; // outputs summary to log file if true
            UdOpcodeTables tables = new UdOpcodeTables(xml);
            
            UdItabGenerator gen = new UdItabGenerator(tables);

            gen.GenCSharpCode();
        }

        /// <summary>
        /// Represents the equivalent method available within .tt
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Write(string message, params object[] args)
        {
            if (args == null || args.Length == 0) Debug.Write(message);
            else Debug.Write(String.Format(message, args));
        }

        /// <summary>
        /// Represents the equivalent method available within .tt
        /// </summary>
        /// <param name="indent"></param>
        public static void PushIndent(string indent)
        {

        }

        /// <summary>
        /// Represents the equivalent method available within .tt
        /// </summary>
        public static void ClearIndent()
        {
        }

        /// <summary>
        /// Represents the equivalent method available within .tt
        /// </summary>
        /// <param name="text"></param>
        public static void Error(string text)
        {
            throw new Exception(text);
        }

        #region XML Doc
        string xml = @"<?xml version=""1.0""?>

<!--
SharpDisasm (File: SharpDisasm\optable.xml)
Copyright (c) 2014-2015 Justin Stenning
http://spazzarama.com
https://github.com/spazzarama/SharpDisasm
https://sharpdisasm.codeplex.com/

SharpDisasm is distributed under the 2-clause ""Simplified BSD License"".

Portions of SharpDisasm are ported to C# from udis86 a C disassembler project
also distributed under the terms of the 2-clause ""Simplified BSD License"" and
Copyright (c) 2002-2012, Vivek Thampi <vivek.mt@gmail.com>
All rights reserved.
UDIS86: https://github.com/vmt/udis86

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, 
   this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, 
   this list of conditions and the following disclaimer in the documentation 
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS ""AS IS"" AND 
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR 
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
-->

<?xml-stylesheet href=""optable.xsl"" type=""text/xsl""?>
<x86optable>

  <!--
      The most important elements of each instruction definition are the
      pfx (prefix), opc (opcode), and opr (operand) elements.  Each is a
      CDATA element consisting of blank-separated words.  Upper and lower
      case are equivalent.

      <pfx></pfx>

      pfx describes the set of valid prefixes that can precede the main
      opcode without turning it into a different instruction. These may
      be:

      aso   accepts address size override
      oso   accepts operand size override
      seg   accepts a segment override
      rexw, rexr, rexx, rexb
            uses the indicated REX bit
      vexl  accepts the vex.L prefix bit, in other words, the vexl
            bit can be used in the decoding of the avx instruction.

      <opr></opr>

      [T][s]

      Size Suffix
      ===========

      x     - If vex.L = 1 => m256/YMM
                 vex.L = 0 => m128/XMM

      opc words may be actual byte values (two hex digits), or may be one of
      the following:
      /sse=66,f3,f2 - required prefix (always first, and always
        followed by 0f)
      /3dnow=00-ff - this is a 3DNow opcode (only in a definition of the
        form 0f 0f 3dnow=<byte>)
      /a=16,32,64 - has this address size
      /m=16,32,64,!64 - applicable only when the CPU is in this mode
      /o=16,32,64 - has this operand size
      /mod=11,!11 - has ModR/M with 11 or not-11 in the Mod field
      /reg=0-7 - has ModR/M with this value in the reg field
      /rm=0-7 - has ModR/M with this value in the R/M field (only with
        /mod=11)
      /x87=00-3f - X87 opcode with this value in the low 6 bits of the
        following ""ModR/M"" byte (only with /mod=11 and no other modifiers)

      opr words follow the Intel documentation somewhat, and specify the
      location and the size of the operand.  The OperandDict table in
      ud_itab.py maps these words to named OP_ and SZ_ constants for the
      location and size respectively.  These constants are defined in
      decode.h, q.v. for details.

      The mode element affects instruction semantics but not decoding:
          inv64 - invalid in 64-bit mode
      def64 - default operand size is 64 bits in 64-bit mode

      cpuid

        The cpuid element maybe applied to an instruction or a specific
        definition of the instruction. One ore more strings define the
        cpuid features that the instruction (or a definition belongs to)

        Values are: sse, sse2, sse3, sse4, sse4.1, sse4.2, avx

      AVX Instructions

      AVX instructions can be described in two ways. One, the explicit
      form, and the other that promotes an existing sse instruction
      definition to its avx form.

      If an instruction is defined to be in cpuid=avx, but is defined in
      the legacy form (using /sse= extensions), then the opcode generator
      will infer that as two definitions, one the see instruction and the
      other, an inferred avx instruction.

      In generating the sse definition from the above, the following
      transformations happen,

        - /vexw and /vexl extensions (if any) are removed
        - The operands H and L are removed. Operands specified on
          the right to removed operands are shifted to the left
          position.
        - The vexl prefix is removed.
        - ""avx"" is removed form the cpuid definition.

     In generating the avx definition from the above, the following
     transformations happen,

        - c4 is inserted in the 0th position of the opcode string
        - /sse extension is removed
        - A new /vex extension is constructed using /sse, 0f, 38 and
          3a opcodes (if any).
        - Operands V, W, H, and U are marked explicitly to have the
          size suffix ""x""

     If the above transformations do not generate the required
     definitions, the instructions will need to be defined separately.
  -->

    <instruction>
        <mnemonic>aaa</mnemonic>
        <def>
            <opc>37 /m=!64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aad</mnemonic>
        <def>
            <opc>d5 /m=!64</opc>
            <opr>Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aam</mnemonic>
        <def>
            <opc>d4 /m=!64</opc>
            <opr>Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aas</mnemonic>
        <def>
            <opc>3f /m=!64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>adc</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>10</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>11</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>12</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>13</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <opc>14</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>15</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>80 /reg=2</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>82 /reg=2 /m=!64</opc>
            <opr>Eb Ib</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>81 /reg=2</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>83 /reg=2</opc>
            <opr>Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>add</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>00</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>01</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>02</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>03</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <opc>04</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>05</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>80 /reg=0</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>82 /reg=0 /m=!64</opc>
            <opr>Eb Ib</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>81 /reg=0</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>83 /reg=0</opc>
            <opr>Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>addpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 58</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>addps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 58</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>addsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 58</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>addss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 58</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>addsubpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f d0</opc>
            <opr>V H W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>addsubps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f d0</opc>
            <opr>V H W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aesdec</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 de</opc>
            <opr>V H W</opr>
            <cpuid>aesni avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aesdeclast</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 df</opc>
            <opr>V W</opr>
            <cpuid>aesni avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aesenc</mnemonic>
        <cpuid>aesni</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 dc</opc>
            <opr>V W</opr>
            <cpuid>aesni avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aesenclast</mnemonic>
        <cpuid>aesni avx</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 dd</opc>
            <opr>V H W</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aesimc</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 db</opc>
            <opr>V W</opr>
            <cpuid>aesni avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>aeskeygenassist</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a df</opc>
            <opr>V W Ib</opr>
            <cpuid>aesni avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>and</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>20</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>21</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>22</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>23</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <opc>24</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>25</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>80 /reg=4</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>82 /reg=4 /m=!64</opc>
            <opr>Eb Ib</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>81 /reg=4</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>83 /reg=4</opc>
            <opr>Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>andpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 54</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>andps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 54</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>andnpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 55</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>andnps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 55</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>arpl</mnemonic>
        <def>
            <pfx>aso</pfx>
            <opc>63 /m=!64</opc>
            <opr>Ew Gw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movsxd</mnemonic>
        <def>
            <pfx>aso oso rexw rexx rexr rexb</pfx>
            <opc>63 /m=64</opc>
            <opr>Gq Ed</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>call</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>ff /reg=2 /m=!64</opc>
            <opr>Ev</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>ff /reg=2 /m=64</opc>
            <opr>Eq</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>ff /reg=3</opc>
            <opr>Fv</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>e8</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>9a /m=!64</opc>
            <opr>Av</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cbw</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>98 /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cwde</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>98 /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cdqe</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>98 /o=64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>clc</mnemonic>
        <def>
            <opc>f8</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cld</mnemonic>
        <def>
            <opc>fc</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>clflush</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f ae /reg=7 /mod=!11</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>clgi</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=3 /mod=11 /rm=5</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cli</mnemonic>
        <def>
            <opc>fa</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>clts</mnemonic>
        <def>
            <opc>0f 06</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmc</mnemonic>
        <def>
            <opc>f5</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovo</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 40</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovno</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 41</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovb</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 42</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovae</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 43</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovz</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 44</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovnz</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 45</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovbe</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 46</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmova</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 47</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovs</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 48</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovns</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 49</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovp</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 4a</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovnp</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 4b</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovl</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 4c</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovge</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 4d</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovle</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 4e</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmovg</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 4f</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmp</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>38</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>39</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>3a</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>3b</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <opc>3c</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>3d</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>80 /reg=7</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>82 /reg=7 /m=!64</opc>
            <opr>Eb Ib</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>81 /reg=7</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>83 /reg=7</opc>
            <opr>Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmppd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f c2</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f c2</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpsb</mnemonic>
        <def>
            <pfx>repz seg</pfx>
            <opc>a6</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpsw</mnemonic>
        <def>
            <pfx>repz oso rexw seg</pfx>
            <opc>a7 /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpsd</mnemonic>
        <def>
            <pfx>repz oso rexw seg</pfx>
            <opc>a7 /o=32</opc>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f c2</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpsq</mnemonic>
        <def>
            <pfx>repz oso rexw seg</pfx>
            <opc>a7 /o=64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f c2</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpxchg</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f b0</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f b1</opc>
            <opr>Ev Gv</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpxchg8b</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f c7 /mod=!11 /reg=1 /o=16</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f c7 /mod=!11 /reg=1 /o=32</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cmpxchg16b</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f c7 /mod=!11 /reg=1 /o=64</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>comisd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 2f</opc>
            <opr>Vsd Wsd</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>comiss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 2f</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cpuid</mnemonic>
        <def>
            <opc>0f a2</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtdq2pd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f3 0f e6</opc>
            <opr>V Wdq</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtdq2ps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 5b</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtpd2dq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f2 0f e6</opc>
            <opr>Vdq W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtpd2pi</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 2d</opc>
            <opr>P W</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtpd2ps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 5a</opc>
            <opr>Vdq W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtpi2ps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 2a</opc>
            <opr>V Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtpi2pd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 2a</opc>
            <opr>V Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtps2dq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 5b</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtps2pd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 5a</opc>
            <opr>V Wdq</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtps2pi</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 2d</opc>
            <opr>P MqU</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtsd2si</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 2d</opc>
            <opr>Gy MqU</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtsd2ss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 5a</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtsi2sd</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 2a</opc>
            <opr>V H Ey</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtsi2ss</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 2a</opc>
            <opr>V H Ey</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtss2sd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 5a</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvtss2si</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 2d</opc>
            <opr>Gy MdU</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvttpd2dq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f e6</opc>
            <opr>Vdq W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvttpd2pi</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 2c</opc>
            <opr>P W</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvttps2dq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f3 0f 5b</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvttps2pi</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 2c</opc>
            <opr>P W</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvttsd2si</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 2c</opc>
            <opr>Gy MqU</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cvttss2si</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 2c</opc>
            <opr>Gy MdU</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cwd</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>99 /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cdq</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>99 /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>cqo</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>99 /o=64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>daa</mnemonic>
        <def>
            <opc>27 /m=!64</opc>
            <mode>inv64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>das</mnemonic>
        <def>
            <opc>2f /m=!64</opc>
            <mode>inv64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>dec</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>48</opc>
            <opr>R0z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>49</opc>
            <opr>R1z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>4a</opc>
            <opr>R2z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>4b</opc>
            <opr>R3z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>4c</opc>
            <opr>R4z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>4d</opc>
            <opr>R5z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>4e</opc>
            <opr>R6z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>4f</opc>
            <opr>R7z</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>fe /reg=1</opc>
            <opr>Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>ff /reg=1</opc>
            <opr>Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>div</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>f7 /reg=6</opc>
            <opr>Ev</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>f6 /reg=6</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>divpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 5e</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>divps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 5e</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>divsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 5e</opc>
            <opr>V H MqU</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>divss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 5e</opc>
            <opr>V H MdU</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>dppd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 41</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>dpps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 3a 40</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>emms</mnemonic>
        <def>
            <opc>0f 77</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>enter</mnemonic>
        <def>
            <opc>c8</opc>
            <opr>Iw Ib</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>extractps</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 3a 17</opc>
            <opr>MdRy V Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>f2xm1</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=30</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fabs</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=21</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fadd</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dc /mod=!11 /reg=0</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d8 /mod=!11 /reg=0</opc>
            <opr>Md</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=00</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=01</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=02</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=03</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=04</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=05</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=06</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=07</opc>
            <opr>ST7 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=00</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=01</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=02</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=03</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=04</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=05</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=06</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=07</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>faddp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>de /mod=11 /x87=00</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=01</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=02</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=03</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=04</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=05</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=06</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=07</opc>
            <opr>ST7 ST0</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fbld</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>df /mod=!11 /reg=4</opc>
            <opr>Mt</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fbstp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>df /mod=!11 /reg=6</opc>
            <opr>Mt</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fchs</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=20</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fclex</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=22</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcmovb</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>da /mod=11 /x87=00</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=01</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=02</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=03</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=04</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=05</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=06</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=07</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcmove</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>da /mod=11 /x87=08</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=09</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=0a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=0b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=0c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=0d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=0e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=0f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcmovbe</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>da /mod=11 /x87=10</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=11</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=12</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=13</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=14</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=15</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=16</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=17</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcmovu</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>da /mod=11 /x87=18</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=19</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=1a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=1b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=1c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=1d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=1e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>da /mod=11 /x87=1f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcmovnb</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=00</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=01</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=02</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=03</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=04</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=05</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=06</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=07</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcmovne</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=08</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=09</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=0a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=0b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=0c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=0d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=0e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=0f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcmovnbe</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=10</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=11</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=12</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=13</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=14</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=15</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=16</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=17</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcmovnu</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=18</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=19</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=1a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=1b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=1c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=1d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=1e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=1f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fucomi</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=28</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=29</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=2a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=2b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=2c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=2d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=2e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=2f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcom</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d8 /mod=!11 /reg=2</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dc /mod=!11 /reg=2</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=10</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=11</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=12</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=13</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=14</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=15</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=16</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=17</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcom2</mnemonic>
        <cpuid>X87 UNDOC</cpuid>
        <def>
            <opc>dc /mod=11 /x87=10</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=11</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=12</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=13</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=14</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=15</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=16</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=17</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcomp3</mnemonic>
        <cpuid>X87 UNDOC</cpuid>
        <def>
            <opc>dc /mod=11 /x87=18</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=19</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=1a</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=1b</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=1c</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=1d</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=1e</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=1f</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcomi</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=30</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=31</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=32</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=33</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=34</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=35</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=36</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>db /mod=11 /x87=37</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fucomip</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>df /mod=11 /x87=28</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=29</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=2a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=2b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=2c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=2d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=2e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=2f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcomip</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>df /mod=11 /x87=30</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=31</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=32</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=33</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=34</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=35</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=36</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=37</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcomp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d8 /mod=!11 /reg=3</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dc /mod=!11 /reg=3</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=18</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=19</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=1a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=1b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=1c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=1d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=1e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=1f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcomp5</mnemonic>
        <cpuid>X87 UNDOC</cpuid>
        <def>
            <opc>de /mod=11 /x87=10</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=11</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=12</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=13</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=14</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=15</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=16</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=17</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcompp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>de /mod=11 /x87=19</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fcos</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=3f</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fdecstp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=36</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fdiv</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dc /mod=!11 /reg=6</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=38</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=39</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=3a</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=3b</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=3c</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=3d</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=3e</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=3f</opc>
            <opr>ST7 ST0</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d8 /mod=!11 /reg=6</opc>
            <opr>Md</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=30</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=31</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=32</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=33</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=34</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=35</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=36</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=37</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fdivp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>de /mod=11 /x87=38</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=39</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=3a</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=3b</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=3c</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=3d</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=3e</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=3f</opc>
            <opr>ST7 ST0</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fdivr</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dc /mod=!11 /reg=7</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=30</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=31</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=32</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=33</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=34</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=35</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=36</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=37</opc>
            <opr>ST7 ST0</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d8 /mod=!11 /reg=7</opc>
            <opr>Md</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=38</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=39</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=3a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=3b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=3c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=3d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=3e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=3f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fdivrp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>de /mod=11 /x87=30</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=31</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=32</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=33</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=34</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=35</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=36</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=37</opc>
            <opr>ST7 ST0</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>femms</mnemonic>
        <def>
            <opc>0f 0e</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ffree</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>dd /mod=11 /x87=00</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=01</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=02</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=03</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=04</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=05</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=06</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=07</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ffreep</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>df /mod=11 /x87=00</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=01</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=02</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=03</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=04</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=05</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=06</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=07</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ficom</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>de /mod=!11 /reg=2</opc>
            <opr>Mw</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>da /mod=!11 /reg=2</opc>
            <opr>Md</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ficomp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>de /mod=!11 /reg=3</opc>
            <opr>Mw</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>da /mod=!11 /reg=3</opc>
            <opr>Md</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fild</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>df /mod=!11 /reg=0</opc>
            <opr>Mw</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>df /mod=!11 /reg=5</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>db /mod=!11 /reg=0</opc>
            <opr>Md</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fincstp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=37</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fninit</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=23</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fiadd</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>da /mod=!11 /reg=0</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>de /mod=!11 /reg=0</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fidivr</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>da /mod=!11 /reg=7</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>de /mod=!11 /reg=7</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fidiv</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>da /mod=!11 /reg=6</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>de /mod=!11 /reg=6</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fisub</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>da /mod=!11 /reg=4</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>de /mod=!11 /reg=4</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fisubr</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>da /mod=!11 /reg=5</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>de /mod=!11 /reg=5</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fist</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>df /mod=!11 /reg=2</opc>
            <opr>Mw</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>db /mod=!11 /reg=2</opc>
            <opr>Md</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fistp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>df /mod=!11 /reg=3</opc>
            <opr>Mw</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>df /mod=!11 /reg=7</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>db /mod=!11 /reg=3</opc>
            <opr>Md</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fisttp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>db /mod=!11 /reg=1</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dd /mod=!11 /reg=1</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>df /mod=!11 /reg=1</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fld</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>db /mod=!11 /reg=5</opc>
            <opr>Mt</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dd /mod=!11 /reg=0</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d9 /mod=!11 /reg=0</opc>
            <opr>Md</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=00</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=01</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=02</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=03</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=04</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=05</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=06</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=07</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fld1</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=28</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fldl2t</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=29</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fldl2e</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=2a</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fldpi</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=2b</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fldlg2</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=2c</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fldln2</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=2d</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fldz</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=2e</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fldcw</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d9 /mod=!11 /reg=5</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fldenv</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d9 /mod=!11 /reg=4</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fmul</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dc /mod=!11 /reg=1</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=08</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=09</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=0a</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=0b</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=0c</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=0d</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=0e</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=0f</opc>
            <opr>ST7 ST0</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d8 /mod=!11 /reg=1</opc>
            <opr>Md</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=08</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=09</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=0a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=0b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=0c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=0d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=0e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=0f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fmulp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>de /mod=11 /x87=08</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=09</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=0a</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=0b</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=0c</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=0d</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=0e</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=0f</opc>
            <opr>ST7 ST0</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fimul</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>da /mod=!11 /reg=1</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>de /mod=!11 /reg=1</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fnop</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=10</opc>
        </def>
    </instruction>
	
    <instruction>
        <mnemonic>fndisi</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=21</opc>
        </def>
    </instruction>
	
    <instruction>
        <mnemonic>fneni</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=20</opc>
        </def>
    </instruction>
	
    <instruction>
        <mnemonic>fnsetpm</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=24</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fpatan</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=33</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fprem</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=38</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fprem1</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=35</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fptan</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>frndint</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=3c</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>frstor</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dd /mod=!11 /reg=4</opc>
            <opr>M</opr>
        </def>
    </instruction>
	
    <instruction>
        <mnemonic>frstpm</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>db /mod=11 /x87=25</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fnsave</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dd /mod=!11 /reg=6</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fscale</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=3d</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fsin</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=3e</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fsincos</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=3b</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fsqrt</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=3a</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fstp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>db /mod=!11 /reg=7</opc>
            <opr>Mt</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dd /mod=!11 /reg=3</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d9 /mod=!11 /reg=3</opc>
            <opr>Md</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=18</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=19</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=1a</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=1b</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=1c</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=1d</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=1e</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=1f</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fstp1</mnemonic>
        <def>
            <opc>d9 /mod=11 /x87=18</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=19</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=1a</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=1b</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=1c</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=1d</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=1e</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=1f</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fstp8</mnemonic>
        <def>
            <opc>df /mod=11 /x87=10</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=11</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=12</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=13</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=14</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=15</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=16</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=17</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fstp9</mnemonic>
        <def>
            <opc>df /mod=11 /x87=18</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=19</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=1a</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=1b</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=1c</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=1d</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=1e</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=1f</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fst</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d9 /mod=!11 /reg=2</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dd /mod=!11 /reg=2</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=10</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=11</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=12</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=13</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=14</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=15</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=16</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=17</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fnstcw</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d9 /mod=!11 /reg=7</opc>
            <opr>Mw</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fnstenv</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d9 /mod=!11 /reg=6</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fnstsw</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dd /mod=!11 /reg=7</opc>
            <opr>Mw</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=20</opc>
            <opr>AX</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fsub</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d8 /mod=!11 /reg=4</opc>
            <opr>Md</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dc /mod=!11 /reg=4</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=20</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=21</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=22</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=23</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=24</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=25</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=26</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=27</opc>
            <opr>ST0 ST7</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=28</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=29</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=2a</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=2b</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=2c</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=2d</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=2e</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=2f</opc>
            <opr>ST7 ST0</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fsubp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>de /mod=11 /x87=28</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=29</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=2a</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=2b</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=2c</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=2d</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=2e</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=2f</opc>
            <opr>ST7 ST0</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fsubr</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>dc /mod=!11 /reg=5</opc>
            <opr>Mq</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=28</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=29</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=2a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=2b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=2c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=2d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=2e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d8 /mod=11 /x87=2f</opc>
            <opr>ST0 ST7</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=20</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=21</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=22</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=23</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=24</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=25</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=26</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>dc /mod=11 /x87=27</opc>
            <opr>ST7 ST0</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d8 /mod=!11 /reg=5</opc>
            <opr>Md</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fsubrp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>de /mod=11 /x87=20</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=21</opc>
            <opr>ST1 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=22</opc>
            <opr>ST2 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=23</opc>
            <opr>ST3 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=24</opc>
            <opr>ST4 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=25</opc>
            <opr>ST5 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=26</opc>
            <opr>ST6 ST0</opr>
        </def>
        <def>
            <opc>de /mod=11 /x87=27</opc>
            <opr>ST7 ST0</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ftst</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=24</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fucom</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>dd /mod=11 /x87=20</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=21</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=22</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=23</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=24</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=25</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=26</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=27</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fucomp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>dd /mod=11 /x87=28</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=29</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=2a</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=2b</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=2c</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=2d</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=2e</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=2f</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fucompp</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>da /mod=11 /x87=29</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fxam</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=25</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fxch</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=08</opc>
            <opr>ST0 ST0</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=09</opc>
            <opr>ST0 ST1</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=0a</opc>
            <opr>ST0 ST2</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=0b</opc>
            <opr>ST0 ST3</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=0c</opc>
            <opr>ST0 ST4</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=0d</opc>
            <opr>ST0 ST5</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=0e</opc>
            <opr>ST0 ST6</opr>
        </def>
        <def>
            <opc>d9 /mod=11 /x87=0f</opc>
            <opr>ST0 ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fxch4</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>dd /mod=11 /x87=08</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=09</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=0a</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=0b</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=0c</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=0d</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=0e</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>dd /mod=11 /x87=0f</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fxch7</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>df /mod=11 /x87=08</opc>
            <opr>ST0</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=09</opc>
            <opr>ST1</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=0a</opc>
            <opr>ST2</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=0b</opc>
            <opr>ST3</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=0c</opc>
            <opr>ST4</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=0d</opc>
            <opr>ST5</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=0e</opc>
            <opr>ST6</opr>
        </def>
        <def>
            <opc>df /mod=11 /x87=0f</opc>
            <opr>ST7</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fxrstor</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f ae /mod=!11 /reg=1</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fxsave</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f ae /mod=!11 /reg=0</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fxtract</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=34</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fyl2x</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=31</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>fyl2xp1</mnemonic>
        <cpuid>X87</cpuid>
        <def>
            <opc>d9 /mod=11 /x87=39</opc>
        </def>
    </instruction>

     <instruction>
        <mnemonic>hlt</mnemonic>
        <def>
            <opc>f4</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>idiv</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>f7 /reg=7</opc>
            <opr>Ev</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>f6 /reg=7</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>in</mnemonic>
        <def>
            <opc>e4</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>e5</opc>
            <opr>eAX Ib</opr>
        </def>
        <def>
            <opc>ec</opc>
            <opr>AL DX</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>ed</opc>
            <opr>eAX DX</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>imul</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f af</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>f6 /reg=5</opc>
            <opr>Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>f7 /reg=5</opc>
            <opr>Ev</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>69</opc>
            <opr>Gv Ev Iz</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>6b</opc>
            <opr>Gv Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>inc</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>40</opc>
            <opr>R0z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>41</opc>
            <opr>R1z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>42</opc>
            <opr>R2z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>43</opc>
            <opr>R3z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>44</opc>
            <opr>R4z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>45</opc>
            <opr>R5z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>46</opc>
            <opr>R6z</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>47</opc>
            <opr>R7z</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>ff /reg=0</opc>
            <opr>Ev</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>fe /reg=0</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>insb</mnemonic>
        <def>
            <pfx>rep seg</pfx>
            <opc>6c</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>insw</mnemonic>
        <def>
            <pfx>rep oso seg</pfx>
            <opc>6d /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>insd</mnemonic>
        <def>
            <pfx>rep oso seg</pfx>
            <opc>6d /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>int1</mnemonic>
        <def>
            <opc>f1</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>int3</mnemonic>
        <def>
            <opc>cc</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>int</mnemonic>
        <def>
            <opc>cd</opc>
            <opr>Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>into</mnemonic>
        <def>
            <opc>ce /m=!64</opc>
            <mode>inv64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>invd</mnemonic>
        <def>
            <opc>0f 08</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>invept</mnemonic>
        <vendor>intel</vendor>
        <def>
            <opc>/sse=66 0f 38 80 /m=32</opc>
            <opr>Gd Mo</opr>
        </def>
        <def>
            <opc>/sse=66 0f 38 80 /m=64</opc>
            <opr>Gq Mo</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>invlpg</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 01 /reg=7 /mod=!11</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>invlpga</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=3 /mod=11 /rm=7</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>invvpid</mnemonic>
        <vendor>intel</vendor>
        <def>
            <opc>/sse=66 0f 38 81 /m=32</opc>
            <opr>Gd Mo</opr>
        </def>
        <def>
            <opc>/sse=66 0f 38 81 /m=64</opc>
            <opr>Gq Mo</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>iretw</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>cf /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>iretd</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>cf /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>iretq</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>cf /o=64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jo</mnemonic>
        <def>
            <opc>70</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 80</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jno</mnemonic>
        <def>
            <opc>71</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 81</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jb</mnemonic>
        <def>
            <opc>72</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 82</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jae</mnemonic>
        <def>
            <opc>73</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 83</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jz</mnemonic>
        <def>
            <opc>74</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 84</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jnz</mnemonic>
        <def>
            <opc>75</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 85</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jbe</mnemonic>
        <def>
            <opc>76</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 86</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ja</mnemonic>
        <def>
            <opc>77</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 87</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>js</mnemonic>
        <def>
            <opc>78</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 88</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jns</mnemonic>
        <def>
            <opc>79</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 89</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jp</mnemonic>
        <def>
            <opc>7a</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 8a</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jnp</mnemonic>
        <def>
            <opc>7b</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 8b</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jl</mnemonic>
        <def>
            <opc>7c</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 8c</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jge</mnemonic>
        <def>
            <opc>7d</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 8d</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jle</mnemonic>
        <def>
            <opc>7e</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 8e</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jg</mnemonic>
        <def>
            <opc>7f</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>0f 8f</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jcxz</mnemonic>
        <def>
            <pfx>aso</pfx>
            <opc>e3 /a=16</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jecxz</mnemonic>
        <def>
            <pfx>aso</pfx>
            <opc>e3 /a=32</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jrcxz</mnemonic>
        <def>
            <pfx>aso</pfx>
            <opc>e3 /a=64</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>jmp</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>ff /reg=4</opc>
            <opr>Ev</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>ff /reg=5</opc>
            <opr>Fv</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>e9</opc>
            <opr>Jz</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>ea /m=!64</opc>
            <opr>Av</opr>
        </def>
        <def>
            <opc>eb</opc>
            <opr>Jb</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lahf</mnemonic>
        <def>
            <opc>9f</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lar</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 02</opc>
            <opr>Gv Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ldmxcsr</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f ae /reg=2 /mod=!11</opc>
            <opr>Md</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lds</mnemonic>
        <def>
            <pfx>aso oso</pfx>
            <opc>c5 /vex=none /m=!64</opc>
            <opr>Gv M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lea</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>8d</opc>
            <opr>Gv M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>les</mnemonic>
        <def>
            <pfx>aso oso</pfx>
            <opc>c4 /m=!64</opc>
            <opr>Gv M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lfs</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f b4</opc>
            <opr>Gz M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lgs</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f b5</opc>
            <opr>Gz M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lidt</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 01 /reg=3 /mod=!11</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lss</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f b2</opc>
            <opr>Gv M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>leave</mnemonic>
        <def>
            <opc>c9</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lfence</mnemonic>
        <def>
            <opc>0f ae /reg=5 /mod=11 /rm=0</opc>
        </def>
        <def>
            <opc>0f ae /reg=5 /mod=11 /rm=1</opc>
        </def>
        <def>
            <opc>0f ae /reg=5 /mod=11 /rm=2</opc>
        </def>
        <def>
            <opc>0f ae /reg=5 /mod=11 /rm=3</opc>
        </def>
        <def>
            <opc>0f ae /reg=5 /mod=11 /rm=4</opc>
        </def>
        <def>
            <opc>0f ae /reg=5 /mod=11 /rm=5</opc>
        </def>
        <def>
            <opc>0f ae /reg=5 /mod=11 /rm=6</opc>
        </def>
        <def>
            <opc>0f ae /reg=5 /mod=11 /rm=7</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lgdt</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 01 /reg=2 /mod=!11</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lldt</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 00 /reg=2</opc>
            <opr>Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lmsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 01 /reg=6 /mod=!11</opc>
            <opr>Ew</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 01 /reg=6 /mod=11</opc>
            <opr>Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lock</mnemonic>
        <def>
            <opc>f0</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lodsb</mnemonic>
        <def>
            <pfx>rep seg</pfx>
            <opc>ac</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lodsw</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>ad /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lodsd</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>ad /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lodsq</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>ad /o=64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>loopne</mnemonic>
        <def>
            <opc>e0</opc>
            <opr>Jb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>loope</mnemonic>
        <def>
            <opc>e1</opc>
            <opr>Jb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>loop</mnemonic>
        <def>
            <opc>e2</opc>
            <opr>Jb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lsl</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f 03</opc>
            <opr>Gv Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ltr</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 00 /reg=3</opc>
            <opr>Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>maskmovq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f7 /mod=11</opc>
            <opr>P N</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>maxpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 5f</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>maxps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 5f</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>maxsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 5f</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>maxss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 5f</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mfence</mnemonic>
        <def>
            <opc>0f ae /reg=6 /mod=11 /rm=0</opc>
        </def>
        <def>
            <opc>0f ae /reg=6 /mod=11 /rm=1</opc>
        </def>
        <def>
            <opc>0f ae /reg=6 /mod=11 /rm=2</opc>
        </def>
        <def>
            <opc>0f ae /reg=6 /mod=11 /rm=3</opc>
        </def>
        <def>
            <opc>0f ae /reg=6 /mod=11 /rm=4</opc>
        </def>
        <def>
            <opc>0f ae /reg=6 /mod=11 /rm=5</opc>
        </def>
        <def>
            <opc>0f ae /reg=6 /mod=11 /rm=6</opc>
        </def>
        <def>
            <opc>0f ae /reg=6 /mod=11 /rm=7</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>minpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 5d</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>minps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 5d</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>minsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 5d</opc>
            <opr>V H MqU</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>minss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 5d</opc>
            <opr>V H MdU</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>monitor</mnemonic>
        <def>
            <opc>0f 01 /reg=1 /mod=11 /rm=0</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>montmul</mnemonic>
        <def>
            <opc>0f a6 /mod=11 /rm=0 /reg=0</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mov</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c6 /reg=0</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c7 /reg=0</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>88</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>89</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>8a</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>8b</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>8c</opc>
            <opr>MwRv S</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>8e</opc>
            <opr>S MwRv</opr>
        </def>
        <def>
            <opc>a0</opc>
            <opr>AL Ob</opr>
        </def>
        <def>
            <pfx>aso oso rexw</pfx>
            <opc>a1</opc>
            <opr>rAX Ov</opr>
        </def>
        <def>
            <opc>a2</opc>
            <opr>Ob AL</opr>
        </def>
        <def>
            <pfx>aso oso rexw</pfx>
            <opc>a3</opc>
            <opr>Ov rAX</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>b0</opc>
            <opr>R0b Ib</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>b1</opc>
            <opr>R1b Ib</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>b2</opc>
            <opr>R2b Ib</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>b3</opc>
            <opr>R3b Ib</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>b4</opc>
            <opr>R4b Ib</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>b5</opc>
            <opr>R5b Ib</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>b6</opc>
            <opr>R6b Ib</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>b7</opc>
            <opr>R7b Ib</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>b8</opc>
            <opr>R0v Iv</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>b9</opc>
            <opr>R1v Iv</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>ba</opc>
            <opr>R2v Iv</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>bb</opc>
            <opr>R3v Iv</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>bc</opc>
            <opr>R4v Iv</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>bd</opc>
            <opr>R5v Iv</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>be</opc>
            <opr>R6v Iv</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>bf</opc>
            <opr>R7v Iv</opr>
        </def>
        <def>
            <pfx>rexr rexw rexb</pfx>
            <opc>0f 20</opc>
            <opr>R C</opr>
        </def>
        <def>
            <pfx>rexr rexw rexb</pfx>
            <opc>0f 21</opc>
            <opr>R D</opr>
        </def>
        <def>
            <pfx>rexr rexw rexb</pfx>
            <opc>0f 22</opc>
            <opr>C R</opr>
        </def>
        <def>
            <pfx>rexr rexw rexb</pfx>
            <opc>0f 23</opc>
            <opr>D R</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movapd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 28</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 29</opc>
            <opr>W V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movaps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 28</opc>
            <opr>V W</opr>
            <cpuid>sse avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 29</opc>
            <opr>W V</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movd</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 6e /o=16</opc>
            <opr>P Ey</opr>
            <cpuid>mmx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 6e /o=32</opc>
            <opr>P Ey</opr>
            <cpuid>mmx</cpuid>
        </def>

        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 6e /o=16</opc>
            <opr>V Ey</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 6e /o=32</opc>
            <opr>V Ey</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 7e /o=16</opc>
            <opr>Ey P</opr>
            <cpuid>mmx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 7e /o=32</opc>
            <opr>Ey P</opr>
            <cpuid>mmx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 7e /o=16</opc>
            <opr>Ey V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 7e /o=32</opc>
            <opr>Ey V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movhpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 16 /mod=!11</opc>
            <opr>V H M</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 17</opc>
            <opr>M V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movhps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 16 /mod=!11</opc>
            <opr>V H M</opr>
            <cpuid>sse avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 17</opc>
            <opr>M V</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movlhps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 16 /mod=11</opc>
            <opr>V H U</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movlpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 12 /mod=!11</opc>
            <opr>V M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 13</opc>
            <opr>M V</opr>
        </def>
        <cpuid>sse2 avx</cpuid>
    </instruction>

    <instruction>
        <mnemonic>movlps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 12 /mod=!11</opc>
            <opr>V M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 13</opc>
            <opr>M V</opr>
        </def>
        <cpuid>sse avx</cpuid>
    </instruction>

    <instruction>
        <mnemonic>movhlps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 12 /mod=11</opc>
            <opr>V U</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movmskpd</mnemonic>
        <def>
            <pfx>oso rexr rexb vexl</pfx>
            <opc>/sse=66 0f 50</opc>
            <opr>Gd U</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movmskps</mnemonic>
        <def>
            <pfx>oso rexr rexb</pfx>
            <opc>0f 50</opc>
            <opr>Gd U</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movntdq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f e7</opc>
            <opr>M V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movnti</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f c3</opc>
            <opr>M Gy</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movntpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 2b</opc>
            <opr>M V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movntps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 2b</opc>
            <opr>M V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movntq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e7</opc>
            <opr>M P</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movq</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 6e /o=64</opc>
            <opr>P Eq</opr>
            <cpuid>mmx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 6e /o=64</opc>
            <opr>V Eq</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 7e /o=64</opc>
            <opr>Eq P</opr>
            <cpuid>mmx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 7e /o=64</opc>
            <opr>Eq V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 7e</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f d6</opc>
            <opr>W V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 6f</opc>
            <opr>P Q</opr>
            <cpuid>mmx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 7f</opc>
            <opr>Q P</opr>
            <cpuid>mmx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movsb</mnemonic>
        <def>
            <pfx>rep seg</pfx>
            <opc>a4</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movsw</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>a5 /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movsd</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>a5 /o=32</opc>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 10</opc>
            <opr>V MqU</opr>
            <cpuid>sse2</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 11</opc>
            <opr>W V</opr>
            <cpuid>sse2</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movsq</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>a5 /o=64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 10</opc>
            <opr>V MdU</opr>
            <cpuid>sse</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 11</opc>
            <opr>W V</opr>
            <cpuid>sse</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movsx</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f be</opc>
            <opr>Gv Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f bf</opc>
            <opr>Gy Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movupd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 10</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 11</opc>
            <opr>W V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movups</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 10</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 11</opc>
            <opr>W V</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movzx</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f b6</opc>
            <opr>Gv Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f b7</opc>
            <opr>Gy Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mul</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>f6 /reg=4</opc>
            <opr>Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>f7 /reg=4</opc>
            <opr>Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mulpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 59</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mulps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 59</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mulsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 59</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mulss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 59</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mwait</mnemonic>
        <def>
            <opc>0f 01 /reg=1 /mod=11 /rm=1</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>neg</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>f6 /reg=3</opc>
            <opr>Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>f7 /reg=3</opc>
            <opr>Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>nop</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 19</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 1a</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 1b</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 1c</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 1d</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 1e</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 1f</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>not</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>f6 /reg=2</opc>
            <opr>Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>f7 /reg=2</opc>
            <opr>Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>or</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>08</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>09</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0a</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0b</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <opc>0c</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>0d</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>80 /reg=1</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>81 /reg=1</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>82 /reg=1 /m=!64</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>83 /reg=1</opc>
            <opr>Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>orpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 56</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>orps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 56</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>out</mnemonic>
        <def>
            <opc>e6</opc>
            <opr>Ib AL</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>e7</opc>
            <opr>Ib eAX</opr>
        </def>
        <def>
            <opc>ee</opc>
            <opr>DX AL</opr>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>ef</opc>
            <opr>DX eAX</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>outsb</mnemonic>
        <def>
            <pfx>rep seg</pfx>
            <opc>6e</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>outsw</mnemonic>
        <def>
            <pfx>rep oso seg</pfx>
            <opc>6f /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>outsd</mnemonic>
        <def>
            <pfx>rep oso seg</pfx>
            <opc>6f /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>packsswb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 63</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 63</opc>
            <opr>P Q</opr>
            <cpuid>mmx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>packssdw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 6b</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 6b</opc>
            <opr>P Q</opr>
            <cpuid>mmx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>packuswb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 67</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 67</opc>
            <opr>P Q</opr>
            <cpuid>mmx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>paddb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f fc</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f fc</opc>
            <opr>P Q</opr>
            <cpuid>mmx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>paddw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f fd</opc>
            <opr>P Q</opr>
            <cpuid>mmx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f fd</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>paddd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f fe</opc>
            <opr>P Q</opr>
            <cpuid>mmx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f fe</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>


    <instruction>
        <mnemonic>paddsb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f ec</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f ec</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>paddsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f ed</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f ed</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>paddusb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f dc</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f dc</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>paddusw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f dd</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f dd</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pand</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f db</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f db</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pandn</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f df</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f df</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pavgb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f e0</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e0</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pavgw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f e3</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e3</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpeqb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 74</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 74</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpeqw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 75</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 75</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpeqd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 76</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 76</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpgtb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 64</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 64</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpgtw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 65</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 65</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpgtd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 66</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 66</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pextrb</mnemonic>
        <def>
            <pfx>aso rexx rexr rexb</pfx>
            <opc>/sse=66 0f 3a 14 /vexw=0</opc>
            <opr>MbRv V Ib</opr>
            <mode>def64</mode>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pextrd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexw rexb</pfx>
            <opc>/sse=66 0f 3a 16 /o=16 /vexw=0</opc>
            <opr>Ed V Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexw rexb</pfx>
            <opc>/sse=66 0f 3a 16 /o=32 /vexw=0</opc>
            <opr>Ed V Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pextrq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexb</pfx>
            <opc>/sse=66 0f 3a 16 /o=64 /vexw=1</opc>
            <opr>Eq V Ib</opr>
            <mode>def64</mode>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

   <instruction>
        <mnemonic>pextrw</mnemonic>
        <def>
            <pfx>aso rexw rexr rexb</pfx>
            <opc>/sse=66 0f c5</opc>
            <opr>Gd U Ib</opr>
            <cpuid>sse avx</cpuid>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f c5</opc>
            <opr>Gd N Ib</opr>
        </def>
        <def>
            <pfx>aso rexw rexx rexr rexb</pfx>
            <opc>/sse=66 0f 3a 15</opc>
            <opr>MwRd V Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pinsrb</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 20</opc>
            <opr>V MbRd Ib</opr>
            <cpuid>sse4.1</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pinsrw</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f c4</opc>
            <opr>P MwRy Ib</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f c4</opc>
            <opr>V MwRy Ib</opr>
            <mode>def64</mode>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pinsrd</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 22 /o=16</opc>
            <opr>V Ed Ib</opr>
            <cpuid>sse4.1</cpuid>
        </def>
 
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 22 /o=32</opc>
            <opr>V Ed Ib</opr>
            <cpuid>sse4.1</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pinsrq</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 22 /o=64</opc>
            <opr>V Eq Ib</opr>
            <cpuid>sse4.1</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vpinsrb</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>/vex=66_0f3a 20 /vexw=0 /vexl=0</opc>
            <opr>V H MbRd Ib</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vpinsrd</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>/vex=66_0f3a 22 /m=!64 /vexw=0 /vexl=0</opc>
            <opr>V H Ed Ib</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>/vex=66_0f3a 22 /m=64 /vexw=0 /vexl=0</opc>
            <opr>V H Ed Ib</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>


    <instruction>
        <mnemonic>vpinsrq</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>/vex=66_0f3a 22 /m=64 /vexw=1 /vexl=0</opc>
            <opr>V H Eq Ib</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>


    <instruction>
        <mnemonic>pmaddwd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f5</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f5</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmaxsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f ee</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f ee</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmaxub</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f de</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f de</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pminsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f ea</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f ea</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pminub</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f da</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f da</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovmskb</mnemonic>
        <def>
            <pfx>oso rexr rexw rexb</pfx>
            <opc>/sse=66 0f d7 /vexl=0</opc>
            <opr>Gd U</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>oso rexr rexw rexb</pfx>
            <opc>0f d7</opc>
            <opr>Gd N</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmulhuw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e4</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f e4</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmulhw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f e5</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e5</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmullw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f d5</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f d5</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pop</mnemonic>
        <def>
            <opc>07 /m=!64</opc>
            <opr>ES</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <opc>17 /m=!64</opc>
            <opr>SS</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <opc>1f /m=!64</opc>
            <opr>DS</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <opc>0f a9</opc>
            <opr>GS</opr>
        </def>
        <def>
            <opc>0f a1</opc>
            <opr>FS</opr>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>58</opc>
            <opr>R0v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>59</opc>
            <opr>R1v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>5a</opc>
            <opr>R2v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>5b</opc>
            <opr>R3v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>5c</opc>
            <opr>R4v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>5d</opc>
            <opr>R5v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>5e</opc>
            <opr>R6v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>5f</opc>
            <opr>R7v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>8f /reg=0</opc>
            <opr>Ev</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>popa</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>61 /o=16 /m=!64</opc>
            <mode>inv64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>popad</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>61 /o=32 /m=!64</opc>
            <mode>inv64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>popfw</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>9d /m=!64 /o=16</opc>
        </def>
        <def>
          <pfx>oso rexw</pfx>
          <opc>9d /m=64 /o=16</opc>
          <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>popfd</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>9d /m=!64 /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>popfq</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>9d /m=64 /o=32</opc>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>9d /m=64 /o=64</opc>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>por</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f eb</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f eb</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>prefetch</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 0d /reg=0</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 0d /reg=1</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 0d /reg=2</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 0d /reg=3</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 0d /reg=4</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 0d /reg=5</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 0d /reg=6</opc>
            <opr>M</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 0d /reg=7</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>prefetchnta</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 18 /reg=0</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>prefetcht0</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 18 /reg=1</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>prefetcht1</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 18 /reg=2</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>prefetcht2</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f 18 /reg=3</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psadbw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f6</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f6</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pshufw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 70</opc>
            <opr>P Q Ib</opr>
        </def>
    </instruction>

     <instruction>
        <mnemonic>psllw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f1</opc>
            <opr>V W</opr>
            <cpuid>sse2</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f1</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 71 /reg=6</opc>
            <opr>U Ib</opr>
            <cpuid>sse2</cpuid>
        </def>
        <def>
            <opc>0f 71 /reg=6</opc>
            <opr>N Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pslld</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f2</opc>
            <opr>V W</opr>
            <cpuid>sse2</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f2</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 72 /reg=6</opc>
            <opr>U Ib</opr>
            <cpuid>sse2</cpuid>
        </def>
        <def>
            <opc>0f 72 /reg=6</opc>
            <opr>N Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psllq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f3</opc>
            <opr>V W</opr>
            <cpuid>sse2</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f3</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 73 /reg=6</opc>
            <opr>U Ib</opr>
            <cpuid>sse2</cpuid>
        </def>
        <def>
            <opc>0f 73 /reg=6</opc>
            <opr>N Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psraw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e1</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f e1</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 71 /reg=4</opc>
            <opr>H U Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <opc>0f 71 /reg=4</opc>
            <opr>N Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psrad</mnemonic>
        <def>
            <opc>0f 72 /reg=4</opc>
            <opr>N Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f e2</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e2</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 72 /reg=4</opc>
            <opr>H U Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psrlw</mnemonic>
        <def>
            <opc>0f 71 /reg=2</opc>
            <opr>N Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f d1</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f d1</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 71 /reg=2</opc>
            <opr>H U Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psrld</mnemonic>
        <def>
            <opc>0f 72 /reg=2</opc>
            <opr>N Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f d2</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f d2</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 72 /reg=2</opc>
            <opr>H U Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psrlq</mnemonic>
        <def>
            <opc>0f 73 /reg=2</opc>
            <opr>N Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f d3</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f d3</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 73 /reg=2</opc>
            <opr>H U Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psubb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f8</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f8</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psubw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f9</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f9</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psubd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f fa</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f fa</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psubsb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e8</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f e8</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psubsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f e9</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f e9</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psubusb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f d8</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f d8</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psubusw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f d9</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f d9</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>punpckhbw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 68</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 68</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>punpckhwd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 69</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 69</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>punpckhdq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 6a</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 6a</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>punpcklbw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 60</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 60</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>punpcklwd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 61</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 61</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>punpckldq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 62</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 62</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pi2fw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=0c</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pi2fd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=0d</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pf2iw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=1c</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pf2id</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=1d</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfnacc</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=8a</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfpnacc</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=8e</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfcmpge</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=90</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfmin</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=94</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfrcp</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=96</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfrsqrt</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=97</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfsub</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=9a</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfadd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=9e</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfcmpgt</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=a0</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfmax</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=a4</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfrcpit1</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=a6</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfrsqit1</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=a7</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfsubr</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=aa</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfacc</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=ae</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfcmpeq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=b0</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfmul</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=b4</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pfrcpit2</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=b6</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmulhrw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=b7</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pswapd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=bb</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pavgusb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 0f /3dnow=bf</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>push</mnemonic>
        <def>
            <opc>06 /m=!64</opc>
            <opr>ES</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <opc>0e /m=!64</opc>
            <opr>CS</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <opc>16 /m=!64</opc>
            <opr>SS</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <opc>1e /m=!64</opc>
            <opr>DS</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <opc>0f a8</opc>
            <opr>GS</opr>
        </def>
        <def>
            <opc>0f a0</opc>
            <opr>FS</opr>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>50</opc>
            <opr>R0v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>51</opc>
            <opr>R1v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>52</opc>
            <opr>R2v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>53</opc>
            <opr>R3v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>54</opc>
            <opr>R4v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>55</opc>
            <opr>R5v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>56</opc>
            <opr>R6v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexb</pfx>
            <opc>57</opc>
            <opr>R7v</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>68</opc>
            <opr>sIz</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>ff /reg=6</opc>
            <opr>Ev</opr>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso</pfx>
            <opc>6a</opc>
            <opr>sIb</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pusha</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>60 /o=16 /m=!64</opc>
            <mode>inv64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pushad</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>60 /o=32 /m=!64</opc>
            <mode>inv64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pushfw</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>9c /m=!64 /o=16</opc>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>9c /m=64 /o=16</opc>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pushfd</mnemonic>
        <def>
            <pfx>oso</pfx>
            <opc>9c /m=!64 /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pushfq</mnemonic>
        <def>
            <pfx>oso rexw</pfx>
            <opc>9c /m=64 /o=32</opc>
            <mode>def64</mode>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>9c /m=64 /o=64</opc>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pxor</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f ef</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f ef</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rcl</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c0 /reg=2</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c1 /reg=2</opc>
            <opr>Ev Ib</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d0 /reg=2</opc>
            <opr>Eb I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d2 /reg=2</opc>
            <opr>Eb CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d3 /reg=2</opc>
            <opr>Ev CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d1 /reg=2</opc>
            <opr>Ev I1</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rcr</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d0 /reg=3</opc>
            <opr>Eb I1</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c1 /reg=3</opc>
            <opr>Ev Ib</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c0 /reg=3</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d1 /reg=3</opc>
            <opr>Ev I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d2 /reg=3</opc>
            <opr>Eb CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d3 /reg=3</opc>
            <opr>Ev CL</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rol</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c0 /reg=0</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d0 /reg=0</opc>
            <opr>Eb I1</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d1 /reg=0</opc>
            <opr>Ev I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d2 /reg=0</opc>
            <opr>Eb CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d3 /reg=0</opc>
            <opr>Ev CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c1 /reg=0</opc>
            <opr>Ev Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ror</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d0 /reg=1</opc>
            <opr>Eb I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c0 /reg=1</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c1 /reg=1</opc>
            <opr>Ev Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d1 /reg=1</opc>
            <opr>Ev I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d2 /reg=1</opc>
            <opr>Eb CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d3 /reg=1</opc>
            <opr>Ev CL</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rcpps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 53</opc>
            <opr>V W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rcpss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 53</opc>
            <opr>V W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rdmsr</mnemonic>
        <def>
            <opc>0f 32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rdpmc</mnemonic>
        <def>
            <opc>0f 33</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rdtsc</mnemonic>
        <def>
            <opc>0f 31</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rdtscp</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=7 /mod=11 /rm=1</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>repne</mnemonic>
        <def>
            <opc>f2</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rep</mnemonic>
        <def>
            <opc>f3</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ret</mnemonic>
        <def>
            <opc>c2</opc>
            <opr>Iw</opr>
        </def>
        <def>
            <opc>c3</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>retf</mnemonic>
        <def>
            <opc>ca</opc>
            <opr>Iw</opr>
        </def>
        <def>
            <opc>cb</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rsm</mnemonic>
        <def>
            <opc>0f aa</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rsqrtps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 52</opc>
            <opr>V W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rsqrtss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 52</opc>
            <opr>V W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sahf</mnemonic>
        <def>
            <opc>9e</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sal</mnemonic>
    </instruction>

    <instruction>
        <mnemonic>salc</mnemonic>
        <def>
            <opc>d6 /m=!64</opc>
            <mode>inv64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sar</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d1 /reg=7</opc>
            <opr>Ev I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c0 /reg=7</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d0 /reg=7</opc>
            <opr>Eb I1</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c1 /reg=7</opc>
            <opr>Ev Ib</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d2 /reg=7</opc>
            <opr>Eb CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d3 /reg=7</opc>
            <opr>Ev CL</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>shl</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c0 /reg=6</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c1 /reg=6</opc>
            <opr>Ev Ib</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d0 /reg=6</opc>
            <opr>Eb I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d2 /reg=6</opc>
            <opr>Eb CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d3 /reg=6</opc>
            <opr>Ev CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c1 /reg=4</opc>
            <opr>Ev Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>d2 /reg=4</opc>
            <opr>Eb CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d1 /reg=4</opc>
            <opr>Ev I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d0 /reg=4</opc>
            <opr>Eb I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c0 /reg=4</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d3 /reg=4</opc>
            <opr>Ev CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d1 /reg=6</opc>
            <opr>Ev I1</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>shr</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>c1 /reg=5</opc>
            <opr>Ev Ib</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d2 /reg=5</opc>
            <opr>Eb CL</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d1 /reg=5</opc>
            <opr>Ev I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>d0 /reg=5</opc>
            <opr>Eb I1</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>c0 /reg=5</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>d3 /reg=5</opc>
            <opr>Ev CL</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sbb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>18</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>19</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>1a</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>1b</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <opc>1c</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>1d</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>80 /reg=3</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>81 /reg=3</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>82 /reg=3 /m=!64</opc>
            <opr>Eb Ib</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>83 /reg=3</opc>
            <opr>Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>scasb</mnemonic>
        <def>
            <pfx>repz</pfx>
            <opc>ae</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>scasw</mnemonic>
        <def>
            <pfx>repz oso rexw</pfx>
            <opc>af /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>scasd</mnemonic>
        <def>
            <pfx>repz oso rexw</pfx>
            <opc>af /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>scasq</mnemonic>
        <def>
            <pfx>repz oso rexw</pfx>
            <opc>af /o=64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>seto</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 90</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setno</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 91</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 92</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setae</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 93</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setz</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 94</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setnz</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 95</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setbe</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 96</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>seta</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 97</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sets</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 98</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setns</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 99</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setp</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 9a</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setnp</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 9b</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setl</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 9c</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setge</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 9d</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setle</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 9e</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>setg</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 9f</opc>
            <opr>Eb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sfence</mnemonic>
        <def>
            <opc>0f ae /reg=7 /mod=11 /rm=0</opc>
        </def>
        <def>
            <opc>0f ae /reg=7 /mod=11 /rm=1</opc>
        </def>
        <def>
            <opc>0f ae /reg=7 /mod=11 /rm=2</opc>
        </def>
        <def>
            <opc>0f ae /reg=7 /mod=11 /rm=3</opc>
        </def>
        <def>
            <opc>0f ae /reg=7 /mod=11 /rm=4</opc>
        </def>
        <def>
            <opc>0f ae /reg=7 /mod=11 /rm=5</opc>
        </def>
        <def>
            <opc>0f ae /reg=7 /mod=11 /rm=6</opc>
        </def>
        <def>
            <opc>0f ae /reg=7 /mod=11 /rm=7</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sgdt</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 01 /reg=0 /mod=!11</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>shld</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f a4</opc>
            <opr>Ev Gv Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f a5</opc>
            <opr>Ev Gv CL</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>shrd</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f ac</opc>
            <opr>Ev Gv Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f ad</opc>
            <opr>Ev Gv CL</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>shufpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f c6</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>shufps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f c6</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sidt</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 01 /reg=1 /mod=!11</opc>
            <opr>M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sldt</mnemonic>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>0f 00 /reg=0</opc>
            <opr>MwRv</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>smsw</mnemonic>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>0f 01 /reg=4 /mod=!11</opc>
            <opr>MwRv</opr>
        </def>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>0f 01 /reg=4 /mod=11</opc>
            <opr>MwRv</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sqrtps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 51</opc>
            <opr>V W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sqrtpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 51</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sqrtsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 51</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sqrtss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 51</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>stc</mnemonic>
        <def>
            <opc>f9</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>std</mnemonic>
        <def>
            <opc>fd</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>stgi</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=3 /mod=11 /rm=4</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sti</mnemonic>
        <def>
            <opc>fb</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>skinit</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=3 /mod=11 /rm=6</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>stmxcsr</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>0f ae /mod=!11 /reg=3</opc>
            <opr>Md</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>stosb</mnemonic>
        <def>
            <pfx>rep seg</pfx>
            <opc>aa</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>stosw</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>ab /o=16</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>stosd</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>ab /o=32</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>stosq</mnemonic>
        <def>
            <pfx>rep seg oso rexw</pfx>
            <opc>ab /o=64</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>str</mnemonic>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>0f 00 /reg=1</opc>
            <opr>MwRv</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sub</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>28</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>29</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>2a</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>2b</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <opc>2c</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>2d</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>80 /reg=5</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>81 /reg=5</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>82 /reg=5 /m=!64</opc>
            <opr>Eb Ib</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>83 /reg=5</opc>
            <opr>Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>subpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 5c</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>subps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>0f 5c</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>subsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 5c</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>subss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 5c</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>swapgs</mnemonic>
        <def>
            <opc>0f 01 /reg=7 /mod=11 /rm=0</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>syscall</mnemonic>
        <def>
            <opc>0f 05</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sysenter</mnemonic>
        <def>
            <opc>0f 34 /m=!64</opc>
        </def>
        <def>
            <opc>0f 34 /m=64</opc>
            <vendor>intel</vendor>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sysexit</mnemonic>
        <def>
            <opc>0f 35 /m=!64</opc>
        </def>
        <def>
            <opc>0f 35 /m=64</opc>
            <vendor>intel</vendor>
        </def>
    </instruction>

    <instruction>
        <mnemonic>sysret</mnemonic>
        <def>
            <opc>0f 07</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>test</mnemonic>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>f6 /reg=0</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>84</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>85</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <opc>a8</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>a9</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexw rexr rexx rexb</pfx>
            <opc>f6 /reg=1</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>f7 /reg=0</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>f7 /reg=1</opc>
            <opr>Ev Iz</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ucomisd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 2e</opc>
            <opr>V W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ucomiss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 2e</opc>
            <opr>V W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>ud2</mnemonic>
        <def>
            <opc>0f 0b</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>unpckhpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 15</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>unpckhps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 15</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>unpcklps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 14</opc>
            <opr>V H W</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>unpcklpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 14</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>verr</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 00 /reg=4</opc>
            <opr>Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>verw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 00 /reg=5</opc>
            <opr>Ew</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmcall</mnemonic>
        <vendor>intel</vendor>
        <def>
            <opc>0f 01 /reg=0 /mod=11 /rm=1</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>rdrand</mnemonic>
        <def>
            <pfx>oso rexr rexw rexx rexb</pfx>
            <opc>0f c7 /mod=11 /reg=6</opc>
            <opr>R</opr>
        </def>
        <cpuid>rdrand</cpuid>
    </instruction>

    <instruction>
        <mnemonic>vmclear</mnemonic>
        <vendor>intel</vendor>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f c7 /mod=!11 /reg=6</opc>
            <opr>Mq</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmxon</mnemonic>
        <vendor>intel</vendor>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f c7 /mod=!11 /reg=6</opc>
            <opr>Mq</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmptrld</mnemonic>
        <vendor>intel</vendor>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f c7 /mod=!11 /reg=6</opc>
            <opr>Mq</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmptrst</mnemonic>
        <vendor>intel</vendor>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f c7 /mod=!11 /reg=7</opc>
            <opr>Mq</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmlaunch</mnemonic>
        <vendor>intel</vendor>
        <def>
            <opc>0f 01 /reg=0 /mod=11 /rm=2</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmresume</mnemonic>
        <vendor>intel</vendor>
        <def>
            <opc>0f 01 /reg=0 /mod=11 /rm=3</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmxoff</mnemonic>
        <vendor>intel</vendor>
        <def>
            <opc>0f 01 /reg=0 /mod=11 /rm=4</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmread</mnemonic>
        <vendor>intel</vendor>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 78</opc>
            <opr>Ey Gy</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmwrite</mnemonic>
        <vendor>intel</vendor>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 79</opc>
            <opr>Gy Ey</opr>
            <mode>def64</mode>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmrun</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=3 /mod=11 /rm=0</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmmcall</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=3 /mod=11 /rm=1</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmload</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=3 /mod=11 /rm=2</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmsave</mnemonic>
        <vendor>amd</vendor>
        <def>
            <opc>0f 01 /reg=3 /mod=11 /rm=3</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>wait</mnemonic>
        <def>
            <opc>9b</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>wbinvd</mnemonic>
        <def>
            <opc>0f 09</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>wrmsr</mnemonic>
        <def>
            <opc>0f 30</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xadd</mnemonic>
        <def>
            <pfx>aso oso rexr rexx rexb</pfx>
            <opc>0f c0</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f c1</opc>
            <opr>Ev Gv</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xchg</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>86</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>87</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>90</opc>
            <opr>R0v rAX</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>91</opc>
            <opr>R1v rAX</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>92</opc>
            <opr>R2v rAX</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>93</opc>
            <opr>R3v rAX</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>94</opc>
            <opr>R4v rAX</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>95</opc>
            <opr>R5v rAX</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>96</opc>
            <opr>R6v rAX</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>97</opc>
            <opr>R7v rAX</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xgetbv</mnemonic>
    <def>
        <opc>0f 01 /mod=11 /reg=2 /rm=0</opc>
    </def>
    </instruction>

    <instruction>
        <mnemonic>xlatb</mnemonic>
        <def>
            <pfx>rexw seg</pfx>
            <opc>d7</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xor</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>30</opc>
            <opr>Eb Gb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>31</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>32</opc>
            <opr>Gb Eb</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>33</opc>
            <opr>Gv Ev</opr>
        </def>
        <def>
            <opc>34</opc>
            <opr>AL Ib</opr>
        </def>
        <def>
            <pfx>oso rexw</pfx>
            <opc>35</opc>
            <opr>rAX sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>80 /reg=6</opc>
            <opr>Eb Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>81 /reg=6</opc>
            <opr>Ev sIz</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>82 /reg=6 /m=!64</opc>
            <opr>Eb Ib</opr>
            <mode>inv64</mode>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>83 /reg=6</opc>
            <opr>Ev sIb</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xorpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 57</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xorps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 57</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xcryptecb</mnemonic>
        <def>
            <opc>0f a7 /mod=11 /rm=0 /reg=1</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xcryptcbc</mnemonic>
        <def>
            <opc>0f a7 /mod=11 /rm=0 /reg=2</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xcryptctr</mnemonic>
        <def>
            <opc>0f a7 /mod=11 /rm=0 /reg=3</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xcryptcfb</mnemonic>
        <def>
            <opc>0f a7 /mod=11 /rm=0 /reg=4</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xcryptofb</mnemonic>
        <def>
            <opc>0f a7 /mod=11 /rm=0 /reg=5</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xrstor</mnemonic>
    <def>
        <pfx>aso rexw rexr rexx rexb</pfx>
        <opc>0f ae /reg=5 /mod=!11</opc>
        <opr>M</opr>
    </def>
    </instruction>

    <instruction>
        <mnemonic>xsave</mnemonic>
    <def>
        <pfx>aso rexw rexr rexx rexb</pfx>
        <opc>0f ae /reg=4 /mod=!11</opc>
        <opr>M</opr>
    </def>
    </instruction>

    <instruction>
        <mnemonic>xsetbv</mnemonic>
    <def>
        <opc>0f 01 /mod=11 /reg=2 /rm=1</opc>
    </def>
    </instruction>

    <instruction>
        <mnemonic>xsha1</mnemonic>
        <def>
            <opc>0f a6 /mod=11 /rm=0 /reg=1</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xsha256</mnemonic>
        <def>
            <opc>0f a6 /mod=11 /rm=0 /reg=2</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>xstore</mnemonic>
        <def>
            <opc>0f a7 /mod=11 /rm=0 /reg=0</opc>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pclmulqdq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 44</opc>
            <opr>V H W Ib</opr>
            <cpuid>aesni avx</cpuid>
        </def>
    </instruction>

    <!--
    SMX
      -->

    <instruction>
        <mnemonic>getsec</mnemonic>
    <cpuid>smx</cpuid>
    <def>
        <opc>0f 37</opc>
    </def>
    </instruction>

    <!--
         SSE 2 
     -->

    <instruction>
        <mnemonic>movdqa</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 7f</opc>
            <opr>W V</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 6f</opc>
            <opr>V W</opr>
        </def>
        <cpuid>sse2 avx</cpuid>
    </instruction>

    <instruction>
        <mnemonic>maskmovdqu</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f7 /mod=11</opc>
            <opr>V U</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movdq2q</mnemonic>
        <def>
            <pfx>aso rexb</pfx>
            <opc>/sse=f2 0f d6</opc>
            <opr>P U</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movdqu</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f3 0f 6f</opc>
            <opr>V W</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f3 0f 7f</opc>
            <opr>W V</opr>
        </def>
		<cpuid>sse2 avx</cpuid>
    </instruction>

    <instruction>
        <mnemonic>movq2dq</mnemonic>
        <def>
            <pfx>aso rexr</pfx>
            <opc>/sse=f3 0f d6</opc>
            <opr>V N</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>paddq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f d4</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f d4</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psubq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f fb</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f fb</opc>
            <opr>P Q</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmuludq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f f4</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f f4</opc>
            <opr>V W</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pshufhw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f3 0f 70</opc>
            <opr>V W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pshuflw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 70</opc>
            <opr>V W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pshufd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 70</opc>
            <opr>V W Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pslldq</mnemonic>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 73 /reg=7</opc>
            <opr>H U Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psrldq</mnemonic>
        <def>
            <pfx>rexb</pfx>
            <opc>/sse=66 0f 73 /reg=3</opc>
            <opr>H U Ib</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>punpckhqdq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 6d</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>punpcklqdq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 6c</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>haddpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 7c</opc>
            <opr>V H W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>haddps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f2 0f 7c</opc>
            <opr>V H W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>hsubpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 7d</opc>
            <opr>V H W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>hsubps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f2 0f 7d</opc>
            <opr>V H W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>insertps</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 3a 21</opc>
            <opr>V H Md Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>lddqu</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f2 0f f0</opc>
            <opr>V M</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movddup</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 12 /mod=11</opc>
            <opr>V W</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=f2 0f 12 /mod=!11</opc>
            <opr>V W</opr>
        </def>
        <cpuid>sse3 avx</cpuid>
    </instruction>

    <instruction>
        <mnemonic>movshdup</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f3 0f 16 /mod=11</opc>
            <opr>V W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f3 0f 16 /mod=!11</opc>
            <opr>V W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movsldup</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f3 0f 12 /mod=11</opc>
            <opr>V W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=f3 0f 12 /mod=!11</opc>
            <opr>V W</opr>
            <cpuid>sse3 avx</cpuid>
        </def>
    </instruction>

    <!--
         SSSE 3
     -->

    <instruction>
        <mnemonic>pabsb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 1c</opc>
            <opr>P Q</opr>
            <cpuid>ssse3</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 38 1c</opc>
            <opr>V W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pabsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 1d</opc>
            <opr>P Q</opr>
            <cpuid>ssse3</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 38 1d</opc>
            <opr>V W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pabsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 1e</opc>
            <opr>P Q</opr>
            <cpuid>ssse3</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 38 1e</opc>
            <opr>V W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pshufb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 00</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 00</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>phaddw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 01</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 01</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>phaddd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 02</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 02</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>phaddsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 03</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 03</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmaddubsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 04</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 04</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>phsubw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 05</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 05</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>phsubd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 06</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 06</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>phsubsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 07</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 07</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psignb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 08</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 08</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psignd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 0a</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 0a</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>psignw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 09</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 09</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmulhrsw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 38 0b</opc>
            <opr>P Q</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 0b</opc>
            <opr>V H W</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>palignr</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>0f 3a 0f</opc>
            <opr>P Q Ib</opr>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 0f</opc>
            <opr>V H W Ib</opr>
            <cpuid>ssse3 avx</cpuid>
        </def>
    </instruction>

    <!--
         SSE 4.1
     -->

    <instruction>
        <mnemonic>pblendvb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 10</opc>
            <opr>V W</opr>
            <cpuid>sse4.1</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmuldq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 28</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pminsb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 38</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pminsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 39</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pminuw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 3a</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pminud</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 3b</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmaxsb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 3c</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmaxsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 3d</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmaxud</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 3f</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmaxuw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 3e</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmulld</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 40</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>phminposuw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 41</opc>
            <opr>V W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>roundps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 3a 08</opc>
            <opr>V W Ib</opr>
            <cpuid>sse avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>roundpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 3a 09</opc>
            <opr>V W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>roundss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 0a</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>roundsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 0b</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>blendpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 3a 0d</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>blendps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 0c</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>blendvpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 15</opc>
            <opr>V W</opr>
            <cpuid>sse4.1</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>blendvps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 38 14</opc>
            <opr>V W</opr>
            <cpuid>sse4.1</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>bound</mnemonic>
        <def>
            <pfx>aso oso</pfx>
            <opc>62 /m=!64</opc>
            <opr>Gv M</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>bsf</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f bc</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>bsr</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f bd</opc>
            <opr>Gv Ev</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>bswap</mnemonic>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>0f c8</opc>
            <opr>R0y</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>0f c9</opc>
            <opr>R1y</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>0f ca</opc>
            <opr>R2y</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>0f cb</opc>
            <opr>R3y</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>0f cc</opc>
            <opr>R4y</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>0f cd</opc>
            <opr>R5y</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>0f ce</opc>
            <opr>R6y</opr>
        </def>
        <def>
            <pfx>oso rexw rexb</pfx>
            <opc>0f cf</opc>
            <opr>R7y</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>bt</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f ba /reg=4</opc>
            <opr>Ev Ib</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f a3</opc>
            <opr>Ev Gv</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>btc</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f bb</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f ba /reg=7</opc>
            <opr>Ev Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>btr</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f b3</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f ba /reg=6</opc>
            <opr>Ev Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>bts</mnemonic>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f ab</opc>
            <opr>Ev Gv</opr>
        </def>
        <def>
            <pfx>aso oso rexw rexr rexx rexb</pfx>
            <opc>0f ba /reg=5</opc>
            <opr>Ev Ib</opr>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pblendw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/sse=66 0f 3a 0e</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>mpsadbw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/sse=66 0f 3a 42</opc>
            <opr>V H W Ib</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movntdqa</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb vexl</pfx>
            <opc>/sse=66 0f 38 2a</opc>
            <opr>V M</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>packusdw</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb vexl</pfx>
            <opc>/sse=66 0f 38 2b</opc>
            <opr>V H W</opr>
            <cpuid>sse2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovsxbw</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 20</opc>
            <opr>V MqU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovsxbd</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 21</opc>
            <opr>V MdU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovsxbq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 22</opc>
            <opr>V MwU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovsxwd</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 23</opc>
            <opr>V MqU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovsxwq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 24</opc>
            <opr>V MdU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovsxdq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 25</opc>
            <opr>V MqU</opr>
            <cpuid>sse4.1</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovzxbw</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 30</opc>
            <opr>V MqU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovzxbd</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 31</opc>
            <opr>V MdU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovzxbq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 32</opc>
            <opr>V MwU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovzxwd</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 33</opc>
            <opr>V MqU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovzxwq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 34</opc>
            <opr>V MdU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pmovzxdq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 35</opc>
            <opr>V MqU</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpeqq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 29</opc>
            <opr>V H W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

     <instruction>
        <mnemonic>popcnt</mnemonic>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>/sse=f3 0f b8</opc>
            <opr>Gv Ev</opr>
        </def>
        <cpuid>sse4.2</cpuid>
    </instruction>

    <instruction>
        <mnemonic>ptest</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb vexl</pfx>
            <opc>/sse=66 0f 38 17</opc>
            <opr>V W</opr>
            <cpuid>sse4.1 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpestri</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 3a 61</opc>
            <opr>V W Ib</opr>
            <cpuid>sse4.2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpestrm</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 3a 60</opc>
            <opr>V W Ib</opr>
            <cpuid>sse4.2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpgtq</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 38 37</opc>
            <opr>V H W</opr>
            <cpuid>sse4.2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpistri</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 3a 63</opc>
            <opr>V W Ib</opr>
            <cpuid>sse4.2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>pcmpistrm</mnemonic>
        <def>
            <pfx>aso rexr rexw rexx rexb</pfx>
            <opc>/sse=66 0f 3a 62</opc>
            <opr>V W Ib</opr>
            <cpuid>sse4.2 avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>movbe</mnemonic>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>0f 38 f0</opc>
            <opr>Gv Mv</opr>
            <cpuid>sse3 atom</cpuid>
        </def>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>0f 38 f1</opc>
            <opr>Mv Gv</opr>
            <cpuid>sse3 atom</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>crc32</mnemonic>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>/sse=f2 0f 38 f0</opc>
            <opr>Gy Eb</opr>
            <cpuid>sse4.2</cpuid>
        </def>
        <def>
            <pfx>aso oso rexr rexw rexx rexb</pfx>
            <opc>/sse=f2 0f 38 f1</opc>
            <opr>Gy Ev</opr>
            <cpuid>sse4.2</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>invalid</mnemonic>
    </instruction>

    <instruction>
        <mnemonic>vbroadcastss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 18 /vexw=0</opc>
            <opr>V Md</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vbroadcastsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 19 /vexw=0 /vexl=1</opc>
            <opr>Vqq Mq</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vextractf128</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f3a 19 /vexw=0 /vexl=1</opc>
            <opr>Wdq Vqq Ib</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vinsertf128</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f3a 18 /vexw=0 /vexl=1</opc>
            <opr>Vqq Hqq Wdq Ib</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmaskmovps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 2c /vexw=0</opc>
            <opr>V H M</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 2e /vexw=0</opc>
            <opr>M H V</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmaskmovpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 2d /vexw=0</opc>
            <opr>V H M</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 2f /vexw=0</opc>
            <opr>M H V</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vpermilpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 0d /vexw=0</opc>
            <opr>Vx Hx Wx</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f3a 05 /vexw=0</opc>
            <opr>V W Ib</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vpermilps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 0c /vexw=0</opc>
            <opr>Vx Hx Wx</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f3a 04 /vexw=0</opc>
            <opr>Vx Wx Ib</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vperm2f128</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f3a 06 /vexw=0 /vexl=1</opc>
            <opr>Vqq Hqq Wqq Ib</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vtestps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 0e /vexw=0</opc>
            <opr>Vx Wx</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vtestpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f38 0f /vexw=0</opc>
            <opr>Vx Wx</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vzeroupper</mnemonic>
        <def>
            <opc>/vex=0f 77 /vexl=0</opc>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vzeroall</mnemonic>
        <def>
            <opc>/vex=0f 77 /vexl=1</opc>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vblendvpd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f3a 4b /vexw=0</opc>
            <opr>Vx Hx Wx Lx</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vblendvps</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb vexl</pfx>
            <opc>/vex=66_0f3a 4a /vexw=0</opc>
            <opr>Vx Hx Wx Lx</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmovsd</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=f2_0f 10 /mod=11</opc>
            <opr>V H U</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=f2_0f 10 /mod=!11</opc>
            <opr>V Mq</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=f2_0f 11 /mod=11</opc>
            <opr>U H V</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=f2_0f 11 /mod=!11</opc>
            <opr>Mq V</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vmovss</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=f3_0f 10 /mod=11</opc>
            <opr>V H U</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=f3_0f 10 /mod=!11</opc>
            <opr>V Md</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=f3_0f 11 /mod=11</opc>
            <opr>U H V</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=f3_0f 11 /mod=!11</opc>
            <opr>Md V</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vpblendvb</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=66_0f3a 4c /vexw=0</opc>
            <opr>V H W L</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

    <instruction>
        <mnemonic>vpsllw</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=66_0f f1 /vexl=0</opc>
            <opr>V H W</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=66_0f 71 /reg=6 /vexl=0</opc>
            <opr>H V W</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

     <instruction>
        <mnemonic>vpslld</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=66_0f f2 /vexl=0</opc>
            <opr>V H W</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=66_0f 72 /reg=6 /vexl=0</opc>
            <opr>H V W</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>

      <instruction>
        <mnemonic>vpsllq</mnemonic>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=66_0f f3 /vexl=0</opc>
            <opr>V H W</opr>
            <cpuid>avx</cpuid>
        </def>
        <def>
            <pfx>aso rexr rexx rexb</pfx>
            <opc>/vex=66_0f 73 /reg=6 /vexl=0</opc>
            <opr>H V W</opr>
            <cpuid>avx</cpuid>
        </def>
    </instruction>
 
</x86optable>
";
        #endregion

    }
}
