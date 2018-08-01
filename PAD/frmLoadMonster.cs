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
        }

        private void FilterList()
        {
            bool toAdd = true;
            lstMonsters.Items.Clear();
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
                if(txtFilter.Text!="")
                {
                    if (!MonsterList[entry.Key].name.ToLower().Contains(txtFilter.Text.ToLower())) toAdd = false;
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

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtFilter.Text.Length > 3) FilterList();
            else if (e.KeyCode == Keys.Enter) FilterList();
        }

        private void lstMonsters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstMonsters_DoubleClick(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, CombinedCard> entry in filteredList)
            {
                if (filteredList[entry.Key].name == lstMonsters.Text) SelectedMonster = entry.Key;
            }
            this.Hide();
        }
        public class MonsterListBox : ListBox
        {
            public MonsterListBox()
            {
                DrawMode = DrawMode.OwnerDrawFixed;
                ItemHeight = 18;
            }
            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;

                if (e.Index >= 0)
                {
                    e.DrawBackground();
                    e.Graphics.DrawRectangle(Pens.Red, 2, e.Bounds.Y + 2, 14, 14);

                    var textrect = e.Bounds;
                    textrect.X += 20;
                    textrect.Width -= 20;
                    string itemText = DesignMode ? "MonsterListBox" : Items[e.Index].ToString();
                    TextRenderer.DrawText(e.Graphics, itemText, e.Font, textrect, e.ForeColor, flags);
                    e.DrawFocusRectangle();
                }
            }
        }
    }
}
