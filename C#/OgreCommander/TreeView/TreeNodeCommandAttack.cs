using OgreCommandLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreCommander
{
    public class TreeNodeCommandAttack : TreeNode
    {
        public CFCAttack Attack;


        public TreeNodeCommandAttack()
        {

        }
        public TreeNodeCommandAttack(CFCAttack attack)
        {
            Attack = attack;
            Update();
        }

        public void Update()
        {
            Text = GetName();
            
            switch(Attack.Type)
            {
                default:
                    ImageIndex = 6;
                    SelectedImageIndex = 6;
                    break;
                case AttackType.LightAttack:
                    ImageIndex = 1;
                    SelectedImageIndex= 1;
                    break;
                case AttackType.LightAttackBehind:
                    ImageIndex = 1;
                    SelectedImageIndex = 1;
                    break;
                case AttackType.HeavyAttack:
                    ImageIndex = 2;
                    SelectedImageIndex = 2;
                    break;
                case AttackType.HeavyAttackBehind:
                    ImageIndex = 2;
                    SelectedImageIndex = 2;
                    break;
                case AttackType.PickUpOrThrow:
                    ImageIndex = 3;
                    SelectedImageIndex = 3;
                    break;
                case AttackType.Quickstep:
                    ImageIndex = 4;
                    SelectedImageIndex = 4;
                    break;
                case AttackType.None:
                    ImageIndex = 5;
                    SelectedImageIndex = 5;
                    break;
            }
        }

        public string GetName()
        {
            string enumName = Attack.Type.ToString().SplitOnCapitals();

            if (string.IsNullOrEmpty(enumName))
                return "Attack";
            else
                return enumName;
        }

        public override object Clone()
        {
            TreeNodeCommandAttack cloned = (TreeNodeCommandAttack)base.Clone();
            cloned.Attack = Attack.Copy();

            return cloned;
        }
    }
}
