using OgreCommandLib;

namespace OgreCommander
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;

        private string m_ofcPath = null;
        private bool m_curOfcIsY3 = false;

        private TreeNode m_selected;

        private TabPage attackPage;
        private TabPage commandPage;


        private object CopiedNode;

        public Form1()
        {
            InitializeComponent();
            Instance = this;

            foreach (string str in Enum.GetNames<AttackType>())
                attackTypeCombobox.Items.Add(str.SplitOnCapitals());

            commandPage = tabControl1.TabPages[0];
            attackPage = tabControl1.TabPages[1];

            tabControl1.TabPages.Clear();
        }

        public void AddNewCommandSet()
        {
            TreeNodeCommandset commandset = new TreeNodeCommandset(new CFCTable());
            commandset.ContextMenuStrip = tableContext;

            commandsTree.Nodes.Add(commandset);
            commandset.Text += $" ({commandsTree.Nodes.Count - 1})";

            commandsTree.SelectedNode = commandset;

            UpdateFollowupDropDown();
        }

        public void AddNewAttackToCommandSet(TreeNodeCommandset command)
        {
            CFCAttack attack = new CFCAttack();
            attack.Type = AttackType.LightAttack;

            TreeNodeCommandAttack attackNode = new TreeNodeCommandAttack(attack);
            attackNode.ContextMenuStrip = attackContext;

            command.Table.Attacks.Add(attack);
            command.Nodes.Add(attackNode);
            ;
        }

        public void DeleteAttackFromCommandSet(TreeNodeCommandAttack attack)
        {
            if (attack == null)
                return;

            TreeNodeCommandset set = attack.Parent as TreeNodeCommandset;

            set.Table.Attacks.Remove(attack.Attack);
            set.Nodes.Remove(attack);
        }

        public void MoveAttackOnCommandSet(TreeNodeCommandAttack attack, bool up)
        {
            if (up)
                attack.MoveUp();
            else
                attack.MoveDown();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();

            if (string.IsNullOrEmpty(dialog.FileName))
                return;

            commandsTree.Nodes.Clear();

            OFCFile ofc = OFCFile.ReadOFC(dialog.FileName);

            for (int i = 0; i < ofc.Tables.Count; i++)
            {
                TreeNodeCommandset commandNode = new TreeNodeCommandset(ofc.Tables[i]);
                commandNode.ContextMenuStrip = tableContext;
                commandNode.Text += $" ({i + 1})";

                for (int k = 0; k < ofc.Tables[i].Attacks.Count; k++)
                {
                    TreeNodeCommandAttack attackNode = new TreeNodeCommandAttack(ofc.Tables[i].Attacks[k]);
                    attackNode.ContextMenuStrip = attackContext;
                    commandNode.Nodes.Add(attackNode);
                }

                commandsTree.Nodes.Add(commandNode);
            }

            m_curOfcIsY3 = ofc.IsY3;
            m_ofcPath = dialog.FileName;

            y4UnkBox.Enabled = !ofc.IsY3;
            y4UnkLbl.Enabled = !ofc.IsY3;

            UpdateFollowupDropDown();
        }

        private void saveOFCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CFCTable> m_generatedList = new List<CFCTable>();

            foreach (TreeNodeCommandset set in commandsTree.Nodes)
            {
                CFCTable table = new CFCTable();
                table.Name = set.Table.Name;

                foreach (TreeNodeCommandAttack attack in set.Nodes)
                    table.Attacks.Add(attack.Attack);

                m_generatedList.Add(table);
            }

            OFCFile file = new OFCFile();
            file.IsY3 = m_curOfcIsY3;
            file.Tables = m_generatedList;

            File.WriteAllBytes(m_ofcPath, OFCFile.WriteBuffer(file));
        }

        private void addCommandsetBtn_Click(object sender, EventArgs e)
        {
            AddNewCommandSet();
        }

        private void commandsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (commandsTree.SelectedNode == null)
                return;

            TreeNode node = commandsTree.SelectedNode;
            m_selected = node;

            tabControl1.Enabled = false;
            tabControl1.TabPages.Clear();

            if (node is TreeNodeCommandset)
            {
                TreeNodeCommandset commandSetNode = node as TreeNodeCommandset;
                commandSetName.Text = commandSetNode.Table.Name;

                tabControl1.TabPages.Add(commandPage);
            }
            else if (node is TreeNodeCommandAttack)
            {
                TreeNodeCommandAttack commandAttackNode = node as TreeNodeCommandAttack;

                CheckAttackType(commandAttackNode.Attack);

                attackAnimationFlagBox.Text = commandAttackNode.Attack.AnimationFlag.ToString();
                attackAnimationIDBox.Text = commandAttackNode.Attack.AnimationID.ToString();
                attackTypeCombobox.SelectedIndex = (int)commandAttackNode.Attack.Type;

                UpdateCondField(commandAttackNode);

                attackFollowupBox.SelectedIndex = commandAttackNode.Attack.FollowupID;
                attackUnkBox.Text = commandAttackNode.Attack.Unk1.ToString();

                if (!m_curOfcIsY3)
                    y4UnkBox.Text = commandAttackNode.Attack.Y4Unk.ToString();

                UpdateFollowupDropDown();

                tabControl1.TabPages.Add(attackPage);
            }

            tabControl1.Enabled = true;
            tabControl1.Refresh();
        }

        private void UpdateFollowupDropDown()
        {
            attackFollowupBox.Items.Clear();

            attackFollowupBox.Items.Add("None");

            for (int i = 0; i < commandsTree.Nodes.Count; i++)
                attackFollowupBox.Items.Add($"{(commandsTree.Nodes[i] as TreeNodeCommandset).Table.Name} ({i + 1})");
        }

        private void UpdateCondField(TreeNodeCommandAttack commandAttackNode)
        {
            attackConditionBox.Items.Clear();

            foreach (var flag in commandAttackNode.Attack.Condition.GetFlags())
                attackConditionBox.Items.Add(flag.ToString().SplitOnCapitals());
        }

        private void CheckAttackType(CFCAttack attack)
        {
            int attackType = (int)attack.Type;

            if (attackType >= attackTypeCombobox.Items.Count)
            {
                while (attackTypeCombobox.Items.Count - 1 != attackType)
                    attackTypeCombobox.Items.Add("Unknown");
            }
        }

        private void commandSetName_TextChanged(object sender, EventArgs e)
        {
            if (m_selected == null)
                return;

            TreeNodeCommandset commandSet = (commandsTree.SelectedNode as TreeNodeCommandset);

            commandSet.Table.Name = commandSetName.Text;
            commandSet.Text = commandSetName.Text;
            UpdateFollowupDropDown();
        }

        private void addAttackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selected == null)
                return;

            AddNewAttackToCommandSet(m_selected as TreeNodeCommandset);
        }

        private void deleteAttackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selected == null)
                return;

            DeleteAttackFromCommandSet(m_selected as TreeNodeCommandAttack);
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selected == null)
                return;

            TreeNodeCommandAttack atk = m_selected as TreeNodeCommandAttack;
            MoveAttackOnCommandSet(atk, true);
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selected == null)
                return;

            TreeNodeCommandAttack atk = m_selected as TreeNodeCommandAttack;
            MoveAttackOnCommandSet(atk, false);
        }

        //used for context menu compat
        private void commandsTree_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void commandsTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                m_selected = e.Node;
        }

        private void attackAnimationIDBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar);
        }

        private void attackAnimationFlagBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar);
        }

        private void attackConditionBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar);
        }

        private void attackConditionBox_TextChanged(object sender, EventArgs e)
        {
            TreeNodeCommandAttack attack = (commandsTree.SelectedNode as TreeNodeCommandAttack);

            if (attack == null)
                return;
            /*
            try
            {
                attack.Attack.Condition = (AttackCondition)ushort.Parse(attackConditionBox.Text);
            }
            catch
            {
                attackConditionBox.Text = "0";
            }
            */
        }

        private void attackAnimationIDBox_TextChanged(object sender, EventArgs e)
        {
            TreeNodeCommandAttack attack = (commandsTree.SelectedNode as TreeNodeCommandAttack);

            if (attack == null)
                return;

            try
            {
                attack.Attack.AnimationID = ushort.Parse(attackAnimationIDBox.Text);
            }
            catch
            {
                attackAnimationIDBox.Text = "0";
            }
        }

        private void attackFollowupBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            (m_selected as TreeNodeCommandAttack).Attack.FollowupID = attackFollowupBox.SelectedIndex;
        }

        private void attackTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNodeCommandAttack atk = (m_selected as TreeNodeCommandAttack);
            atk.Attack.Type = (AttackType)attackTypeCombobox.SelectedIndex;
            atk.Update();
        }

        private void attackAnimationFlagBox_TextChanged(object sender, EventArgs e)
        {
            TreeNodeCommandAttack attack = (commandsTree.SelectedNode as TreeNodeCommandAttack);

            if (attack == null)
                return;

            try
            {
                attack.Attack.AnimationFlag = short.Parse(attackAnimationFlagBox.Text);
            }
            catch
            {
                attackAnimationFlagBox.Text = "0";
            }
        }

        private void attackUnkBox_TextChanged(object sender, EventArgs e)
        {
            TreeNodeCommandAttack attack = (commandsTree.SelectedNode as TreeNodeCommandAttack);

            if (attack == null)
                return;

            try
            {
                attack.Attack.Unk1 = ushort.Parse(attackUnkBox.Text);
            }
            catch
            {
                attackUnkBox.Text = "0";
            }
        }

        private void editFlagsButton_Click(object sender, EventArgs e)
        {
            Enabled = false;

            TreeNodeCommandAttack atk = m_selected as TreeNodeCommandAttack;


            AttackFlagEditor form = new AttackFlagEditor();
            form.Show();
            form.Init(atk.Attack.Condition, delegate (ushort val) { atk.Attack.Condition = (AttackCondition)val; UpdateCondField(atk); });
        }

        private void openOFCJsonDELETEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();

            if (string.IsNullOrEmpty(dialog.SelectedPath))
                return;

            commandsTree.Nodes.Clear();

            List<CFCTable> tables = OFCFile.ReadJson(dialog.SelectedPath);
            m_curOfcIsY3 = false;

            for (int i = 0; i < tables.Count; i++)
            {
                TreeNodeCommandset commandNode = new TreeNodeCommandset(tables[i]);
                commandNode.ContextMenuStrip = tableContext;
                commandNode.Text += $" ({i + 1})";

                for (int k = 0; k < tables[i].Attacks.Count; k++)
                {
                    TreeNodeCommandAttack attackNode = new TreeNodeCommandAttack(tables[i].Attacks[k]);
                    attackNode.ContextMenuStrip = attackContext;
                    commandNode.Nodes.Add(attackNode);
                }

                commandsTree.Nodes.Add(commandNode);
            }

            m_ofcPath = Path.Combine(dialog.SelectedPath, "ogre_command.ofc");

            UpdateFollowupDropDown();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the ID of the animation you want to find from String Data section of Landlord",
           "Animation ID",
           "",
           0,
           0);

            if (string.IsNullOrEmpty(input))
                return;

            int val = 0;

            try
            {
                val = int.Parse(input) - 1;
            }
            catch
            {
                return;
            }

            foreach (var commandset in commandsTree.Nodes)
            {
                var commandSetNode = commandset as TreeNodeCommandset;

                foreach (TreeNodeCommandAttack atk in commandSetNode.Nodes)
                    if (atk.Attack.AnimationID == val)
                    {
                        atk.ExpandAll();
                        commandsTree.SelectedNode = atk;
                        break;
                    }
            }
        }


        private void PasteNode(TreeNode copiedNode)
        {
            if (copiedNode == null)
                return;

            if (copiedNode is TreeNodeCommandset)
                commandsTree.Nodes.Add((TreeNode)copiedNode.Clone());
            else if(copiedNode is TreeNodeCommandAttack)
            {
                if (m_selected != null)
                {
                    if (m_selected is TreeNodeCommandset)
                        m_selected.Nodes.Add((TreeNodeCommandAttack)copiedNode.Clone());
                    else if(m_selected is TreeNodeCommandAttack)
                        m_selected.Parent.Nodes.Add((TreeNodeCommandAttack)copiedNode.Clone());
                }
            }
        }

        private void commandsTree_KeyDown(object sender, KeyEventArgs e)
        {

            if (ModifierKeys.HasFlag(Keys.Control))
            {
                //Copy/Paste
                if (e.KeyCode == Keys.C || e.KeyCode == Keys.V)
                {
                    if (e.KeyCode == Keys.C)
                        CopiedNode = commandsTree.SelectedNode;
                    else
                        PasteNode((TreeNode)CopiedNode);
                }
                else if (e.KeyCode == Keys.S)
                {
                    saveOFCToolStripMenuItem_Click(sender, e);
                }
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (m_selected == null)
                        return;

                    DeleteAttackFromCommandSet(m_selected as TreeNodeCommandAttack);
                }
            }
        }
    }
}