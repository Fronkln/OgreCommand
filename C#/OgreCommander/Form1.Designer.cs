namespace OgreCommander
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Label label1;
            Label label3;
            Label label2;
            Label label4;
            Label label5;
            Label label6;
            Label label7;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            y4UnkLbl = new Label();
            commandsTree = new TreeView();
            icons = new ImageList(components);
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveOFCToolStripMenuItem = new ToolStripMenuItem();
            toolStripButton1 = new ToolStripButton();
            addCommandsetBtn = new Button();
            commandSetName = new TextBox();
            tabControl1 = new TabControl();
            Command = new TabPage();
            Attack = new TabPage();
            y4UnkBox = new TextBox();
            editFlagsButton = new Button();
            attackConditionBox = new ListBox();
            attackUnkBox = new TextBox();
            attackAnimationFlagBox = new TextBox();
            attackFollowupBox = new ComboBox();
            attackTypeCombobox = new ComboBox();
            attackAnimationIDBox = new TextBox();
            tableContext = new ContextMenuStrip(components);
            addAttackToolStripMenuItem = new ToolStripMenuItem();
            attackContext = new ContextMenuStrip(components);
            moveUpToolStripMenuItem = new ToolStripMenuItem();
            moveDownToolStripMenuItem = new ToolStripMenuItem();
            deleteAttackToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            toolStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            Command.SuspendLayout();
            Attack.SuspendLayout();
            tableContext.SuspendLayout();
            attackContext.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 3);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 5;
            label1.Text = "Command Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 13);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 3;
            label3.Text = "Input Type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 63);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 1;
            label2.Text = "Animation ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 119);
            label4.Name = "label4";
            label4.Size = new Size(136, 15);
            label4.TabIndex = 5;
            label4.Text = "Follow up to Command:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(116, 63);
            label5.Name = "label5";
            label5.Size = new Size(88, 15);
            label5.TabIndex = 8;
            label5.Text = "Animation Flag";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(21, 177);
            label6.Name = "label6";
            label6.Size = new Size(93, 15);
            label6.TabIndex = 10;
            label6.Text = "Move Condition";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(21, 334);
            label7.Name = "label7";
            label7.Size = new Size(58, 15);
            label7.TabIndex = 12;
            label7.Text = "Unknown";
            // 
            // y4UnkLbl
            // 
            y4UnkLbl.AutoSize = true;
            y4UnkLbl.Location = new Point(126, 334);
            y4UnkLbl.Name = "y4UnkLbl";
            y4UnkLbl.Size = new Size(110, 15);
            y4UnkLbl.TabIndex = 16;
            y4UnkLbl.Text = "Unknown (Y4 Only)";
            // 
            // commandsTree
            // 
            commandsTree.ImageIndex = 0;
            commandsTree.ImageList = icons;
            commandsTree.Location = new Point(21, 28);
            commandsTree.Name = "commandsTree";
            commandsTree.SelectedImageIndex = 0;
            commandsTree.Size = new Size(223, 414);
            commandsTree.TabIndex = 0;
            commandsTree.AfterSelect += commandsTree_AfterSelect;
            commandsTree.NodeMouseClick += commandsTree_NodeMouseClick;
            commandsTree.KeyDown += commandsTree_KeyDown;
            commandsTree.MouseClick += commandsTree_MouseClick;
            // 
            // icons
            // 
            icons.ColorDepth = ColorDepth.Depth8Bit;
            icons.ImageStream = (ImageListStreamer)resources.GetObject("icons.ImageStream");
            icons.TransparentColor = Color.Transparent;
            icons.Images.SetKeyName(0, "none.png");
            icons.Images.SetKeyName(1, "square.png");
            icons.Images.SetKeyName(2, "triangle.png");
            icons.Images.SetKeyName(3, "circle.png");
            icons.Images.SetKeyName(4, "cross.png");
            icons.Images.SetKeyName(5, "nonebutton.png");
            icons.Images.SetKeyName(6, "unkbutton.png");
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(550, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveOFCToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(38, 22);
            toolStripDropDownButton1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(129, 22);
            openToolStripMenuItem.Text = "Open OFC";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveOFCToolStripMenuItem
            // 
            saveOFCToolStripMenuItem.Name = "saveOFCToolStripMenuItem";
            saveOFCToolStripMenuItem.Size = new Size(129, 22);
            saveOFCToolStripMenuItem.Text = "Save OFC";
            saveOFCToolStripMenuItem.Click += saveOFCToolStripMenuItem_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(92, 22);
            toolStripButton1.Text = "Find by GMT ID";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // addCommandsetBtn
            // 
            addCommandsetBtn.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            addCommandsetBtn.Location = new Point(21, 458);
            addCommandsetBtn.Name = "addCommandsetBtn";
            addCommandsetBtn.Size = new Size(75, 39);
            addCommandsetBtn.TabIndex = 3;
            addCommandsetBtn.Text = "+";
            addCommandsetBtn.UseVisualStyleBackColor = true;
            addCommandsetBtn.Click += addCommandsetBtn_Click;
            // 
            // commandSetName
            // 
            commandSetName.Location = new Point(6, 21);
            commandSetName.MaxLength = 32;
            commandSetName.Name = "commandSetName";
            commandSetName.Size = new Size(121, 23);
            commandSetName.TabIndex = 4;
            commandSetName.TextChanged += commandSetName_TextChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Command);
            tabControl1.Controls.Add(Attack);
            tabControl1.Location = new Point(275, 28);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(244, 413);
            tabControl1.TabIndex = 6;
            // 
            // Command
            // 
            Command.Controls.Add(label1);
            Command.Controls.Add(commandSetName);
            Command.Location = new Point(4, 24);
            Command.Name = "Command";
            Command.Padding = new Padding(3);
            Command.Size = new Size(236, 385);
            Command.TabIndex = 0;
            Command.Text = "Command";
            Command.UseVisualStyleBackColor = true;
            // 
            // Attack
            // 
            Attack.Controls.Add(y4UnkLbl);
            Attack.Controls.Add(y4UnkBox);
            Attack.Controls.Add(editFlagsButton);
            Attack.Controls.Add(attackConditionBox);
            Attack.Controls.Add(label7);
            Attack.Controls.Add(attackUnkBox);
            Attack.Controls.Add(label6);
            Attack.Controls.Add(label5);
            Attack.Controls.Add(attackAnimationFlagBox);
            Attack.Controls.Add(attackFollowupBox);
            Attack.Controls.Add(label4);
            Attack.Controls.Add(attackTypeCombobox);
            Attack.Controls.Add(label3);
            Attack.Controls.Add(label2);
            Attack.Controls.Add(attackAnimationIDBox);
            Attack.Location = new Point(4, 24);
            Attack.Name = "Attack";
            Attack.Padding = new Padding(3);
            Attack.Size = new Size(236, 385);
            Attack.TabIndex = 1;
            Attack.Text = "Attack";
            Attack.UseVisualStyleBackColor = true;
            // 
            // y4UnkBox
            // 
            y4UnkBox.Location = new Point(126, 352);
            y4UnkBox.Name = "y4UnkBox";
            y4UnkBox.Size = new Size(76, 23);
            y4UnkBox.TabIndex = 15;
            // 
            // editFlagsButton
            // 
            editFlagsButton.Location = new Point(64, 295);
            editFlagsButton.Name = "editFlagsButton";
            editFlagsButton.Size = new Size(93, 23);
            editFlagsButton.TabIndex = 14;
            editFlagsButton.Text = "Edit";
            editFlagsButton.UseVisualStyleBackColor = true;
            editFlagsButton.Click += editFlagsButton_Click;
            // 
            // attackConditionBox
            // 
            attackConditionBox.FormattingEnabled = true;
            attackConditionBox.ItemHeight = 15;
            attackConditionBox.Location = new Point(21, 195);
            attackConditionBox.Name = "attackConditionBox";
            attackConditionBox.Size = new Size(191, 94);
            attackConditionBox.TabIndex = 13;
            // 
            // attackUnkBox
            // 
            attackUnkBox.Location = new Point(21, 352);
            attackUnkBox.Name = "attackUnkBox";
            attackUnkBox.Size = new Size(76, 23);
            attackUnkBox.TabIndex = 11;
            attackUnkBox.TextChanged += attackUnkBox_TextChanged;
            // 
            // attackAnimationFlagBox
            // 
            attackAnimationFlagBox.Location = new Point(116, 81);
            attackAnimationFlagBox.Name = "attackAnimationFlagBox";
            attackAnimationFlagBox.Size = new Size(68, 23);
            attackAnimationFlagBox.TabIndex = 7;
            attackAnimationFlagBox.TextChanged += attackAnimationFlagBox_TextChanged;
            attackAnimationFlagBox.KeyPress += attackAnimationFlagBox_KeyPress;
            // 
            // attackFollowupBox
            // 
            attackFollowupBox.FormattingEnabled = true;
            attackFollowupBox.Location = new Point(21, 137);
            attackFollowupBox.Name = "attackFollowupBox";
            attackFollowupBox.Size = new Size(191, 23);
            attackFollowupBox.TabIndex = 6;
            attackFollowupBox.SelectedIndexChanged += attackFollowupBox_SelectedIndexChanged;
            // 
            // attackTypeCombobox
            // 
            attackTypeCombobox.FormattingEnabled = true;
            attackTypeCombobox.Location = new Point(21, 31);
            attackTypeCombobox.Name = "attackTypeCombobox";
            attackTypeCombobox.Size = new Size(163, 23);
            attackTypeCombobox.TabIndex = 4;
            attackTypeCombobox.SelectedIndexChanged += attackTypeCombobox_SelectedIndexChanged;
            // 
            // attackAnimationIDBox
            // 
            attackAnimationIDBox.Location = new Point(21, 81);
            attackAnimationIDBox.Name = "attackAnimationIDBox";
            attackAnimationIDBox.Size = new Size(76, 23);
            attackAnimationIDBox.TabIndex = 0;
            attackAnimationIDBox.TextChanged += attackAnimationIDBox_TextChanged;
            attackAnimationIDBox.KeyPress += attackAnimationIDBox_KeyPress;
            // 
            // tableContext
            // 
            tableContext.Items.AddRange(new ToolStripItem[] { addAttackToolStripMenuItem });
            tableContext.Name = "tableContext";
            tableContext.Size = new Size(134, 26);
            // 
            // addAttackToolStripMenuItem
            // 
            addAttackToolStripMenuItem.Name = "addAttackToolStripMenuItem";
            addAttackToolStripMenuItem.Size = new Size(133, 22);
            addAttackToolStripMenuItem.Text = "Add Attack";
            addAttackToolStripMenuItem.Click += addAttackToolStripMenuItem_Click;
            // 
            // attackContext
            // 
            attackContext.Items.AddRange(new ToolStripItem[] { moveUpToolStripMenuItem, moveDownToolStripMenuItem, deleteAttackToolStripMenuItem });
            attackContext.Name = "attackContext";
            attackContext.ShowItemToolTips = false;
            attackContext.Size = new Size(145, 70);
            // 
            // moveUpToolStripMenuItem
            // 
            moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            moveUpToolStripMenuItem.Size = new Size(144, 22);
            moveUpToolStripMenuItem.Text = "Move Up";
            moveUpToolStripMenuItem.Click += moveUpToolStripMenuItem_Click;
            // 
            // moveDownToolStripMenuItem
            // 
            moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            moveDownToolStripMenuItem.Size = new Size(144, 22);
            moveDownToolStripMenuItem.Text = "Move Down";
            moveDownToolStripMenuItem.Click += moveDownToolStripMenuItem_Click;
            // 
            // deleteAttackToolStripMenuItem
            // 
            deleteAttackToolStripMenuItem.Name = "deleteAttackToolStripMenuItem";
            deleteAttackToolStripMenuItem.Size = new Size(144, 22);
            deleteAttackToolStripMenuItem.Text = "Delete Attack";
            deleteAttackToolStripMenuItem.Click += deleteAttackToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 509);
            Controls.Add(tabControl1);
            Controls.Add(addCommandsetBtn);
            Controls.Add(toolStrip1);
            Controls.Add(commandsTree);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Ogre Commander";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            Command.ResumeLayout(false);
            Command.PerformLayout();
            Attack.ResumeLayout(false);
            Attack.PerformLayout();
            tableContext.ResumeLayout(false);
            attackContext.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView commandsTree;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveOFCToolStripMenuItem;
        private Button addCommandsetBtn;
        private TextBox commandSetName;
        private TabControl tabControl1;
        private TabPage Command;
        private TabPage Attack;
        private Label label2;
        private TextBox attackAnimationIDBox;
        private ComboBox attackTypeCombobox;
        private Label label3;
        private ComboBox attackFollowupBox;
        private TextBox attackAnimationFlagBox;
        private ContextMenuStrip tableContext;
        private ToolStripMenuItem addAttackToolStripMenuItem;
        private ContextMenuStrip attackContext;
        private ToolStripMenuItem deleteAttackToolStripMenuItem;
        private TextBox attackUnkBox;
        private ListBox attackConditionBox;
        private Button editFlagsButton;
        private ImageList icons;
        private ToolStripMenuItem moveUpToolStripMenuItem;
        private ToolStripMenuItem moveDownToolStripMenuItem;
        private TextBox y4UnkBox;
        private Label y4UnkLbl;
        private ToolStripButton toolStripButton1;
    }
}