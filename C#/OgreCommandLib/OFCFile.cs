using Newtonsoft.Json;
using OgreCommandLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OgreCommandLib
{
    public class OFCFile
    {
        public bool IsY3 = false;
        public List<CFCTable> Tables = new List<CFCTable>();

        public static OFCFile ReadOFC(string ofc)
        {
            using (BinaryReader br = new BinaryReader(new MemoryStream(File.ReadAllBytes(ofc))))
            {
                bool isY3 = br.ReadInt32() == 3;
                int tableCount = br.ReadInt32();

                List<CFCTable> tables = new List<CFCTable>(new CFCTable[tableCount - 1]);

                for (int i = 0; i < tableCount - 1; i++)
                {
                    CFCTable ct = new CFCTable();

                    int size = br.ReadInt32();
                    ct.Name = br.ReadString().Split(new[] { '\0' }, 2)[0];

                    for (int k = 0; k < size / 16; k++)
                    {
                        CFCAttack newAttack = new CFCAttack();
                        newAttack.Type = (AttackType)br.ReadInt32();
                        newAttack.AnimationID = br.ReadUInt16();
                        newAttack.AnimationFlag = br.ReadInt16();
                        newAttack.FollowupID = br.ReadInt32();
                        newAttack.Unk1 = br.ReadUInt16();
                        newAttack.Condition = (AttackCondition)br.ReadUInt16();

                        ct.Attacks.Add(newAttack);

                    }

                    br.BaseStream.Seek(4, SeekOrigin.Current);
                    tables[i] = ct;
                }

                OFCFile file = new OFCFile();
                file.IsY3 = isY3;
                file.Tables = tables;

                return file;
            }
        }

        public static List<CFCTable> ReadJson(string dir)
        {
            string[] jsons = Directory.GetFiles(dir, "*.json");

            List<CFCTable> list = new List<CFCTable>(new CFCTable[jsons.Length]);

            foreach (string str in jsons)
            {
                int idx = int.Parse(Path.GetFileNameWithoutExtension(str));
                CFCAttack[] attacks = JsonConvert.DeserializeObject<CFCAttack[]>(File.ReadAllText(str));

                list[idx - 1] = new CFCTable() { Attacks = new List<CFCAttack>(attacks) };
            }

            return list;
        }

        public static byte[] WriteBuffer(List<CFCTable> tables, bool isY3)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            writer.Write(isY3 ? 3 : 4);
            writer.Write(tables.Count + 1);

            for (int k = 0; k < tables.Count; k++)
            {
                CFCTable cfcTable = tables[k];

                //Size
                writer.Write(0x1337);
                writer.Write(cfcTable.Name.ToLength(31));
                long tableStart = writer.BaseStream.Position;

                for (int i = 0; i < cfcTable.Attacks.Count; i++)
                {
                    CFCAttack attack = cfcTable.Attacks[i];

                    writer.Write((int)attack.Type);
                    writer.Write(attack.AnimationID);
                    writer.Write(attack.AnimationFlag);
                    writer.Write(attack.FollowupID);
                    writer.Write(attack.Unk1);
                    writer.Write((ushort)attack.Condition);

                    if (!isY3)
                        writer.Write(attack.Y4Unk);
                }

                writer.Write(0x26);

                long tableEnd = writer.BaseStream.Position;

                writer.BaseStream.Seek(tableStart - 36, SeekOrigin.Begin);
                writer.Write((int)(tableEnd - tableStart));
                writer.BaseStream.Seek(tableEnd, SeekOrigin.Begin);
            }

            return stream.ToArray();
        }
    }
}
