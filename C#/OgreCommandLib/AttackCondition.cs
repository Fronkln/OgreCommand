using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreCommandLib
{
    [Flags]
    public enum AttackCondition : ushort
    {
        None = 0,
        FacingEnemyBack = 1,
        CounterAttackTiming = 2,
        Unknown3 = 4,
        NotLockedToEnemy = 8,
        PreviousAttackHit = 16,
        HaveWeapon = 32,
        Running = 64,
        LockedToEnemy = 128,
        EnemyIsDown = 256,
        Charging = 512,
        Unknown11 = 1024,
        UnlockedTigerDrop = 2048,
        Unknown13 = 4096,
        UnlockedDoubleFinishingBlowSkill = 8192,
        LongCharging= 16384,
        Unknown16 = 32768      
    }
}
