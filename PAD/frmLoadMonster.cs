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
        public Dictionary<int, CombinedCard> MonsterList;
        public bool InheritSelect=false;
        Dictionary<int, CombinedCard> filteredList;
        public int SelectedMonster = 0;
        public frmLoadMonster()
        {
            InitializeComponent();
        }

        private void frmLoadMonster_Load(object sender, EventArgs e)
        {
            bool toAdd = true;
            filteredList = new Dictionary<int, CombinedCard>();
            foreach (KeyValuePair<int, CombinedCard> entry in MonsterList)
            {
                toAdd = true;
                if ((InheritSelect) && (MonsterList[entry.Key].inheritable == false)) toAdd = false;
                
                if (MonsterList[entry.Key].card_id > 9999) toAdd = false;
                if (MonsterList[entry.Key].released_status == false) toAdd = false;
                if (MonsterList[entry.Key].type_1_id != -1)
                {
                    CheckBox Type1;
                    Type1 = this.Controls.Find("chk" + MonsterList[entry.Key].type_1_id.ToString(), false).FirstOrDefault() as CheckBox;
                    if (Type1.Checked == false) toAdd = false; ;
                }
                if (MonsterList[entry.Key].type_2_id != -1)
                {
                    CheckBox Type2;
                    Type2 = this.Controls.Find("chk" + MonsterList[entry.Key].type_2_id.ToString(), false).FirstOrDefault() as CheckBox;
                    if (Type2.Checked == false) toAdd = false; ;
                }
                if (MonsterList[entry.Key].type_3_id != -1)
                {
                    CheckBox Type3;
                    Type3 = this.Controls.Find("chk" + MonsterList[entry.Key].type_3_id.ToString(), false).FirstOrDefault() as CheckBox;
                    if (Type3.Checked == false) toAdd = false; ;
                }

                if (toAdd == true)
                {
                    filteredList.Add(MonsterList[entry.Key].card_id, MonsterList[entry.Key]);
                    lstMonsters.Items.Add(MonsterList[entry.Key].name);
                }
                lstMonsters.Sorted = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, CombinedCard> entry in filteredList)
            {
                if (filteredList[entry.Key].name == lstMonsters.Text) SelectedMonster = entry.Key;
            }
            this.Hide();
        }
    }
}
