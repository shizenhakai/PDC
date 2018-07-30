using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAD
{
    public partial class frmLoadMonster : Form
    {
        public Dictionary<int, PadMonster> MonsterList;
        public bool InheritSelect=false;
        Dictionary<int, PadMonster> filteredList;
        public int SelectedMonster = 0;
        public frmLoadMonster()
        {
            InitializeComponent();
        }

        private void frmLoadMonster_Load(object sender, EventArgs e)
        {
            bool toAdd = true;
            filteredList = new Dictionary<int, PadMonster>();
            foreach (KeyValuePair<int, PadMonster> entry in MonsterList)
            {
                if ((InheritSelect) && (MonsterList[entry.Key].inheritable == false)) toAdd = false;

                if (toAdd == true)
                {
                    filteredList.Add(MonsterList[entry.Key].card_id, MonsterList[entry.Key]);
                    cmbMonsters.Items.Add(MonsterList[entry.Key].name);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < filteredList.Count - 1; i++)
            {
                if (filteredList[i].name == cmbMonsters.Text) SelectedMonster = i;
            }
            this.Hide();
        }
    }
}
