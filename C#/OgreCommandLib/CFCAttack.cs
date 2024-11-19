using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace OgreCommandLib
{
    //https://stackoverflow.com/a/37997635
    public class CFCAttack
    {
        [JsonProperty("Type")]
        public AttackType Type { get; set; }

        [JsonProperty("Animation Flag")]
        public short AnimationFlag { get; set; }
        [JsonProperty("Animation ID")]
        public ushort AnimationID { get; set; }


        [JsonProperty("Condition")]
        public AttackCondition Condition { get; set; }

        [JsonProperty("Followup Attack ID")]
        public int FollowupID { get; set; }

        [JsonProperty("Unknown 1")]
        public ushort Unk1 { get; set; }


        [JsonProperty("Yakuza 4 Unknown")]
        public int Y4Unk { get; set; }
    }
}
