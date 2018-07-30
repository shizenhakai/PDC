using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace PAD
{
    public partial class frmMain : Form
    {
        Dictionary<int, PadMonster> MonsterList;
        string PadDataPath= @"E:\PadSync\";
        int curMon = 1;
        public frmMain()
        {
            InitializeComponent();
            LoadMonsterList();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            frmLoadMonster loadMonster = new frmLoadMonster();
            loadMonster.MonsterList = MonsterList;
            loadMonster.ShowDialog();
        }

        private void img1_Click(object sender, EventArgs e)
        {
            curMon++;
            SetMonsterSlot(1, curMon);
        }
        public int SetMonsterSlot(int TeamSlot, int MonsterNo)
        {
            PictureBox MonsterPortrait;
            PictureBox Type1; 
            PictureBox Type2;
            PictureBox Type3;
            Label MonsterName;
            Label HP;
            Label ATK;
            Label RCV;
            NumericUpDown LVL;
            NumericUpDown PlusHP;
            NumericUpDown PlusATK;
            NumericUpDown PlusRCV;
            string strTeamslot = TeamSlot.ToString();
            MonsterPortrait = this.Controls.Find("img" + strTeamslot, false).FirstOrDefault() as PictureBox;
            Type1 = this.Controls.Find("img" + strTeamslot + "Type1", false).FirstOrDefault() as PictureBox;
            Type2 = this.Controls.Find("img" + strTeamslot + "Type2", false).FirstOrDefault() as PictureBox;
            Type3 = this.Controls.Find("img" + strTeamslot + "Type3", false).FirstOrDefault() as PictureBox;
            MonsterName = this.Controls.Find("lblMonster" + strTeamslot, false).FirstOrDefault() as Label;
            HP = this.Controls.Find("lblHP" + strTeamslot, false).FirstOrDefault() as Label;
            ATK = this.Controls.Find("lblATK" + strTeamslot, false).FirstOrDefault() as Label;
            RCV = this.Controls.Find("lblRCV" + strTeamslot, false).FirstOrDefault() as Label;
            LVL = this.Controls.Find("nmLVL" + strTeamslot, false).FirstOrDefault() as NumericUpDown;
            PlusHP = this.Controls.Find("nmHP" + strTeamslot, false).FirstOrDefault() as NumericUpDown;
            PlusATK = this.Controls.Find("nmATK" + strTeamslot, false).FirstOrDefault() as NumericUpDown;
            PlusRCV = this.Controls.Find("nmRCV" + strTeamslot, false).FirstOrDefault() as NumericUpDown;

            MonsterName.Text = MonsterList[MonsterNo].name;
            while(MonsterName.Width < System.Windows.Forms.TextRenderer.MeasureText(MonsterName.Text, new Font(MonsterName.Font.FontFamily, MonsterName.Font.Size, MonsterName.Font.Style)).Width)
            {
                MonsterName.Font = new Font(MonsterName.Font.FontFamily, MonsterName.Font.Size - 0.1f, MonsterName.Font.Style);
            }
            HP.Text = (MonsterList[MonsterNo].max_hp + 990).ToString();
            ATK.Text = (MonsterList[MonsterNo].max_atk + 495).ToString();
            RCV.Text = (MonsterList[MonsterNo].max_rcv + 297).ToString();
            LVL.Value = 99;
            PlusATK.Value = 99;
            PlusHP.Value = 99;
            PlusRCV.Value = 99;
            MonsterPortrait.Load(PadDataPath + @"padimages\na\portrait\" + MonsterNo.ToString() + ".png");
            if (MonsterList[MonsterNo].type_1_id != -1) Type1.Load(PadDataPath + @"padimages\icons\types\" + MonsterList[MonsterNo].type_1_id.ToString() + ".png");
            if (MonsterList[MonsterNo].type_2_id != -1) Type2.Load(PadDataPath + @"padimages\icons\types\" + MonsterList[MonsterNo].type_2_id.ToString() + ".png");
            if (MonsterList[MonsterNo].type_3_id != -1) Type3.Load(PadDataPath + @"padimages\icons\types\" + MonsterList[MonsterNo].type_3_id.ToString() + ".png");
            return 0;
        }
        public void LoadMonsterList()
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader(PadDataPath + @"paddata\processed\na_cards.json"));
            string curObject="root";
            MonsterList = new Dictionary<int, PadMonster>();
            PadMonster curMonster = new PadMonster();
            int convertedInt = 0;
            double convertedDouble = 0.0;
            curMonster.card_id = 0;
            while(reader.Read())
            {
                //if (reader.ValueType != null) Console.WriteLine("Token: {0}, Value: {1} - ValueType: {2}", reader.TokenType, reader.Value, reader.ValueType);
                //else Console.WriteLine("Token: {0}", reader.TokenType);
                switch(reader.TokenType)
                {
                    case JsonToken.StartObject:
                        //create new monster
                        break;
                    case JsonToken.PropertyName:
                        switch(curObject)
                        {
                            case "active_skill":
                                switch(reader.Value)
                                {
                                    case "PROPERTY NAME":
                                        //set active.PROPERTY
                                        break;
                                    case "card":
                                        curObject = "card";
                                        break;
                                }
                                break;
                            case "card":
                                string propertyName = (string)reader.Value;
                                reader.Read();
                                if (reader.ValueType == typeof(System.Int64)) int.TryParse(reader.Value.ToString(), out convertedInt);
                                if (reader.ValueType == typeof(System.Double)) double.TryParse(reader.Value.ToString(), out convertedDouble);
                                switch (propertyName)
                                {
                                    case "active_skill_id":
                                        curMonster.active_skill_id = convertedInt;
                                        break;
                                    case "ancestor_id":
                                        curMonster.ancestor_id = convertedInt;
                                        break;
                                    case "atk_exponent":
                                        curMonster.atk_exponent = convertedDouble;
                                        break;
                                    case "attr_id":
                                        curMonster.attr_id = convertedInt;
                                        break;
                                    case "base_id":
                                        curMonster.base_id = convertedInt;
                                        break;
                                    case "card_id":
                                        curMonster.card_id = convertedInt;
                                        break;
                                    case "cost":
                                        curMonster.cost = convertedInt;
                                        break;
                                    case "evo_mat_id1":
                                        curMonster.evo_mat_id1 = convertedInt;
                                        break;
                                    case "evo_mat_id2":
                                        curMonster.evo_mat_id2 = convertedInt;
                                        break;
                                    case "evo_mat_id3":
                                        curMonster.evo_mat_id3 = convertedInt;
                                        break;
                                    case "evo_mat_id4":
                                        curMonster.evo_mat_id4 = convertedInt;
                                        break;
                                    case "evo_mat_id5":
                                        curMonster.evo_mat_id5 =  convertedInt;
                                        break;
                                    case "hp_exponent":
                                        curMonster.hp_exponent = convertedDouble;
                                        break;
                                    case "leader_skill_id":
                                        curMonster.leader_skill_id = convertedInt;
                                        break;
                                    case "limit_mult":
                                        curMonster.limit_mult = convertedInt;
                                        break;
                                    case "max_atk":
                                        curMonster.max_atk = convertedInt;
                                        break;
                                    case "max_hp":
                                        curMonster.max_hp = convertedInt;
                                        break;
                                    case "max_rcv":
                                        curMonster.max_rcv = convertedInt;
                                        break;
                                    case "min_atk":
                                        curMonster.min_atk = convertedInt;
                                        break;
                                    case "min_hp":
                                        curMonster.min_hp = convertedInt;
                                        break;
                                    case "min_rcv":
                                        curMonster.min_rcv = convertedInt;
                                        break;
                                    case "rarity":
                                        curMonster.rarity = convertedInt;
                                        break;
                                    case "rcv_exponent":
                                        curMonster.rcv_exponent = convertedDouble;
                                        break;
                                    case "sell_mp":
                                         curMonster.sell_mp = convertedInt;
                                        break;
                                    case "subb_attr_id":
                                        curMonster.subb_attr_id = convertedInt;
                                        break;
                                    case "type_1_id":
                                        curMonster.type_1_id = convertedInt;
                                        break;
                                    case "type_2_id":
                                        curMonster.type_2_id = convertedInt;
                                        break;
                                    case "type_3_id":
                                        curMonster.type_3_id = convertedInt;
                                        break;
                                    case "un_evo_mat_1":
                                        curMonster.un_evo_mat_1 = convertedInt;
                                        break;
                                    case "un_evo_mat_2":
                                        curMonster.un_evo_mat_2 = convertedInt;
                                        break;
                                    case "un_evo_mat_3":
                                        curMonster.un_evo_mat_3 = convertedInt;
                                        break;
                                    case "un_evo_mat_4":
                                        curMonster.un_evo_mat_4 = convertedInt;
                                        break;
                                    case "un_evo_mat_5":
                                        curMonster.un_evo_mat_5 = convertedInt;
                                        break;
                                    case "xp_gr":
                                        curMonster.xp_gr = convertedDouble;
                                        break;
                                    case "xp_max":
                                        curMonster.xp_max = convertedInt;
                                        break;
                                    case "name": //string
                                        curMonster.name = reader.Value.ToString();
                                        break;
                                    case "awakenings": //list
                                        break;
                                    case "super_awakenings": //list
                                        break;
                                    case "inheritable": //bool
                                        curMonster.inheritable = (reader.Value.ToString() == "true");
                                        break;
                                    case "is_collab": //bool
                                        curMonster.is_collab = (reader.Value.ToString() == "true");
                                        break;
                                    case "released_status": //bool
                                        curMonster.released_status = (reader.Value.ToString() == "true");
                                        break;
                                    case "is_ult": //bool
                                        curMonster.is_ult = (reader.Value.ToString() == "true");
                                        break;
                                    case "leader_skill":
                                        curObject = "leader_skill";
                                        break;
                                }
                                break;
                            case "leader_skill":
                                switch(reader.Value)
                                {
                                    case "PROPERTY NAME":
                                        //set leader.PROPERTY
                                        break;
                                }
                                break;
                            default:
                                curObject = reader.Value.ToString();
                                break;
                        }
                        break;
                    case JsonToken.EndObject:
                        if (curObject == "leader_skill")
                        {
                            curObject = "active_skill";
                            if(!MonsterList.ContainsKey(curMonster.card_id)) MonsterList.Add(curMonster.card_id, curMonster);
                            curMonster = new PadMonster();
                            //save card
                        }
                        break;
                }
            }
            Console.WriteLine("Added {0} monsters to dictionary.", MonsterList.Count);
        }
    }
    public class PadMonster
    {
        public int active_skill_id { get; set; }
        public int ancestor_id { get; set; }
        public double atk_exponent { get; set; }
        public int attr_id { get; set; }
        public List<int> awakenings;
        public int base_id { get; set; }
        public int card_id { get; set; }
        public int cost { get; set; }
        public int evo_mat_id1 { get; set; }
        public int evo_mat_id2 { get; set; }
        public int evo_mat_id3 { get; set; }
        public int evo_mat_id4 { get; set; }
        public int evo_mat_id5 { get; set; }
        public double hp_exponent { get; set; }
        public bool inheritable { get; set; }
        public bool is_collab { get; set; }
        public bool is_ult { get; set; }
        public int leader_skill_id { get; set; }
        public int limit_mult { get; set; }
        public int max_atk { get; set; }
        public int max_hp { get; set; }
        public int max_rcv { get; set; }
        public int min_atk { get; set; }
        public int min_hp { get; set; }
        public int min_rcv { get; set; }
        public string name { get; set; }
        public int rarity { get; set; }
        public double rcv_exponent { get; set; }
        public bool released_status { get; set; }
        public int sell_mp { get; set; }
        public int subb_attr_id { get; set; }
        public List<int> super_awakenings;
        public int type_1_id { get; set; }
        public int type_2_id { get; set; }
        public int type_3_id { get; set; }
        public int un_evo_mat_1 { get; set; }
        public int un_evo_mat_2 { get; set; }
        public int un_evo_mat_3 { get; set; }
        public int un_evo_mat_4 { get; set; }
        public int un_evo_mat_5 { get; set; }
        public double xp_gr { get; set; }
        public int xp_max { get; set; }
        public PadMonster()
        {
            awakenings = new List<int>();
            super_awakenings = new List<int>();
        }
    }
}
