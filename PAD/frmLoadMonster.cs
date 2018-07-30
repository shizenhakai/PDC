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
                toAdd = true;
                if ((InheritSelect) && (MonsterList[entry.Key].inheritable == false)) toAdd = false;
                if (MonsterList[entry.Key].card_id > 9999) toAdd = false;
                if (MonsterList[entry.Key].released_status == false) toAdd = false;

                if (toAdd == true)
                {
                    filteredList.Add(MonsterList[entry.Key].card_id, MonsterList[entry.Key]);
                    cmbMonsters.Items.Add(entry.Key.ToString() + ". " + MonsterList[entry.Key].name);
                }
                cmbMonsters.Sorted = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, PadMonster> entry in MonsterList)
            {
                if (filteredList[entry.Key].name == cmbMonsters.Text) SelectedMonster = entry.Key;
            }
            this.Hide();
        }
    }
}
