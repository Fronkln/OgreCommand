using OgreCommandLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OgreCommander
{
    public partial class AttackFlagEditor : Form
    {
        private string[] m_enumNames;
        private Enum[] m_enumValues;

        private List<Enum> m_checkedValues = new List<Enum>();

        private Action<ushort> m_doneDeleg;

        public AttackFlagEditor()
        {
            InitializeComponent();
        }

        public void Init(AttackCondition startingVal, Action<ushort> finishDeleg)
        {
            m_doneDeleg = finishDeleg;
            m_enumNames = Enum.GetNames(startingVal.GetType());
            m_enumValues = Enum.GetValues(startingVal.GetType()).OfType<Enum>().ToArray();

            foreach (string str in m_enumNames)
                checkedListBox1.Items.Add(str.SplitOnCapitals());


           Enum[] flags = startingVal.GetFlags().ToArray();
            
            for(int i = 0; i < m_enumValues.Length; i++)
            {
                if (flags.Contains(m_enumValues[i]))
                    checkedListBox1.SetItemChecked(i, true);
            }
        }



        private void AttackFlagEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.Instance.Enabled = true;

            ushort finalVal = 0;

            foreach (int i in checkedListBox1.CheckedIndices)
            {
                finalVal += Convert.ToUInt16(m_enumValues[i]);
            }

            m_doneDeleg?.Invoke(finalVal);
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
