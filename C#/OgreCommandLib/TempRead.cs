using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace OgreCommandLib
{
    public static class TempRead
    {
        public static void Process(string cfcDir)
        {
            string[] str = Directory.GetFiles(cfcDir);

            List<CFCTable> m_attacks = new List<CFCTable>(new CFCTable[str.Length + 1]);

            foreach (string file in str)
            {
                int setID = int.Parse(Path.GetFileNameWithoutExtension(file));

                    List<CFCAttack> attack = JsonConvert.DeserializeObject<List<CFCAttack>>(File.ReadAllText(file));
                    m_attacks[setID] = new CFCTable() { Attacks = attack };

            }

            File.WriteAllBytes(Path.Combine(cfcDir, "..", "ogre_command.ofc"), OFCFile.WriteBuffer(m_attacks, false));

            foreach (var kv in m_attacks)
            {
                //File.WriteAllText(Path.Combine(outputPath, kv.Key.ToString() + ".json"), JsonConvert.SerializeObject(kv.Value.Attacks, Formatting.Indented));
            }



        }
    }
}
