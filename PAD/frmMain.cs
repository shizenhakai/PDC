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
    public partial class v : Form
    {
        Dictionary<int, CombinedCard> CombinedList;
        Dictionary<int, PadMonster> NAList;
        Dictionary<int, PadMonster> JPList;
        string PadDataPath= @"E:\PadSync\";
        public v()
        {
            InitializeComponent();
            NAList = new Dictionary<int, PadMonster>();
            JPList = new Dictionary<int, PadMonster>(); ;
            CombinedList = new Dictionary<int, CombinedCard>();
            LoadMonsterList(PadDataPath + @"paddata\processed\na_cards.json", NAList);
            LoadMonsterList(PadDataPath + @"paddata\processed\jp_cards.json", JPList);
            CombineLists();
        }

        public void CombineLists()
        {
            foreach(KeyValuePair<int, PadMonster> entry in NAList)
            {
                if(!CombinedList.ContainsKey(entry.Key))
                {
                    CombinedCard newCard = new CombinedCard(NAList[entry.Key]);
                    newCard.jp_only = false;
                    CombinedList.Add(newCard.card_id, newCard);
                }
            }
            foreach (KeyValuePair<int, PadMonster> entry in JPList)
            {
                if (!CombinedList.ContainsKey(entry.Key))
                {
                    CombinedCard newCard = new CombinedCard(JPList[entry.Key]);
                    newCard.jp_only = true;
                    newCard.jp_name = JPList[entry.Key].name;
                    CombinedList.Add(newCard.card_id, newCard);
                }
                else
                {
                    CombinedList[entry.Key].jp_name = JPList[entry.Key].name;
                }
            }
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

            MonsterName.Text = CombinedList[MonsterNo].name;
            while(MonsterName.Width < System.Windows.Forms.TextRenderer.MeasureText(MonsterName.Text, new Font(MonsterName.Font.FontFamily, MonsterName.Font.Size, MonsterName.Font.Style)).Width)
            {
                MonsterName.Font = new Font(MonsterName.Font.FontFamily, MonsterName.Font.Size - 0.1f, MonsterName.Font.Style);
            }
            HP.Text = (CombinedList[MonsterNo].max_hp + 990).ToString();
            ATK.Text = (CombinedList[MonsterNo].max_atk + 495).ToString();
            RCV.Text = (CombinedList[MonsterNo].max_rcv + 297).ToString();
            LVL.Value = 99;
            PlusATK.Value = 99;
            PlusHP.Value = 99;
            PlusRCV.Value = 99;
            if(CombinedList[MonsterNo].jp_only) MonsterPortrait.Load(PadDataPath + @"padimages\jp\portrait\" + MonsterNo.ToString() + ".png");
            else MonsterPortrait.Load(PadDataPath + @"padimages\na\portrait\" + MonsterNo.ToString() + ".png");
            if (CombinedList[MonsterNo].type_1_id != -1) Type1.Load(PadDataPath + @"padimages\icons\types\" + CombinedList[MonsterNo].type_1_id.ToString() + ".png");
            if (CombinedList[MonsterNo].type_2_id != -1) Type2.Load(PadDataPath + @"padimages\icons\types\" + CombinedList[MonsterNo].type_2_id.ToString() + ".png");
            if (CombinedList[MonsterNo].type_3_id != -1) Type3.Load(PadDataPath + @"padimages\icons\types\" + CombinedList[MonsterNo].type_3_id.ToString() + ".png");
            if(TeamSlot==1)
            {
                for(int i = 0;i<CombinedList[MonsterNo].awakenings.Count;i++)
                {
                    PictureBox img = this.Controls.Find("awakening" + i.ToString(), false).FirstOrDefault() as PictureBox;
                    img.Load(PadDataPath + @"padimages\icons\awakenings\" + CombinedList[MonsterNo].awakenings[i].ToString() + ".png");
                }
            }
            return 0;
        }
        public void LoadMonsterList(string MonsterJSONPath, Dictionary<int, PadMonster> list)
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader(MonsterJSONPath));
            string curObject="root";
            PadMonster curMonster = new PadMonster();
            int convertedInt = 0;
            double convertedDouble = 0.0;
            curMonster.card_id = 0;
            while(reader.Read())
            {
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
                                    case "random_flags":
                                        curMonster.random_flags = convertedInt;
                                        break;
                                    case "unknown_009":
                                        curMonster.unknown_009 = convertedInt;
                                        break;
                                    case "name": //string
                                        curMonster.name = reader.Value.ToString();
                                        break;
                                    case "awakenings": //list
                                        if (reader.TokenType==JsonToken.StartArray)
                                        {
                                            reader.Read();
                                            List<int> awakenings = new List<int>();
                                            while (reader.TokenType != JsonToken.EndArray)
                                            {
                                                if (reader.Value != null)
                                                {
                                                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                                                    if (reader.TokenType == JsonToken.Integer)
                                                    {
                                                        int.TryParse(reader.Value.ToString(), out convertedInt);
                                                        awakenings.Add(convertedInt);
                                                    }
                                                }
                                                reader.Read();
                                            }
                                            curMonster.awakenings = awakenings;
                                        }
                                        break;
                                        if (reader.TokenType==JsonToken.StartArray)
                                        {
                                            reader.Read();
                                            List<int> awakenings = new List<int>();
                                            while (reader.TokenType != JsonToken.EndArray)
                                            {
                                                if (reader.Value != null)
                                                {
                                                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                                                    if (reader.TokenType == JsonToken.Integer)
                                                    {
                                                        int.TryParse(reader.Value.ToString(), out convertedInt);
                                                        awakenings.Add(convertedInt);
                                                    }
                                                }
                                                reader.Read();
                                            }
                                            curMonster.awakenings = awakenings;
                                        }
                                    case "super_awakenings": //list
                                        if (reader.TokenType == JsonToken.StartArray)
                                        {
                                            reader.Read();
                                            List<int> awakenings = new List<int>();
                                            while (reader.TokenType != JsonToken.EndArray)
                                            {
                                                if (reader.Value != null)
                                                {
                                                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                                                    if (reader.TokenType == JsonToken.Integer)
                                                    {
                                                        int.TryParse(reader.Value.ToString(), out convertedInt);
                                                        awakenings.Add(convertedInt);
                                                    }
                                                }
                                                reader.Read();
                                            }
                                            curMonster.super_awakenings = awakenings;
                                        }
                                        break;
                                    case "inheritable": //bool
                                        curMonster.inheritable = (reader.Value.ToString() == "True");
                                        break;
                                    case "is_collab": //bool
                                        curMonster.is_collab = (reader.Value.ToString() == "True");
                                        break;
                                    case "released_status": //bool
                                        string test = reader.Value.ToString();
                                        if (test == "True") curMonster.released_status = true;
                                        else curMonster.released_status = false;
                                        //curMonster.released_status = (reader.Value.ToString() == "True"); ;
                                        break;
                                    case "is_ult": //bool
                                        curMonster.is_ult = (reader.Value.ToString() == "True");
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
                            bool toAdd = true;
                            //if (File.Exists(PadDataPath + @"padimages\na\portrait\" + curMonster.card_id.ToString() + ".png") == false) toAdd = false;
                            if (curMonster.card_id > 6000) toAdd = false;
                            if (curMonster.name.Contains("***")) toAdd = false;
                            if (curMonster.name.Contains("???")) toAdd = false;
                            if (list.ContainsKey(curMonster.card_id)) toAdd = false;
                            if(toAdd) list.Add(curMonster.card_id, curMonster);
                            curMonster = new PadMonster();
                            //save card
                        }
                        break;
                }
            }
            Console.WriteLine("Added {0} monsters to dictionary.", list.Count);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmLoadMonster loadMonster = new frmLoadMonster();
            loadMonster.MonsterList = CombinedList;
            loadMonster.ShowDialog();
        }

        private void img1_Click(object sender, EventArgs e)
        {
            frmLoadMonster loadMonster = new frmLoadMonster();
            loadMonster.MonsterList = CombinedList;
            loadMonster.ShowDialog();
            if(loadMonster.SelectedMonster!=0) SetMonsterSlot(1, loadMonster.SelectedMonster);
        }

        private void img2_Click(object sender, EventArgs e)
        {
            frmLoadMonster loadMonster = new frmLoadMonster();
            loadMonster.MonsterList = CombinedList;
            loadMonster.ShowDialog();
            if (loadMonster.SelectedMonster != 0) SetMonsterSlot(2, loadMonster.SelectedMonster);
        }

        private void img3_Click(object sender, EventArgs e)
        {
            frmLoadMonster loadMonster = new frmLoadMonster();
            loadMonster.MonsterList = CombinedList;
            loadMonster.ShowDialog();
            if (loadMonster.SelectedMonster != 0) SetMonsterSlot(3, loadMonster.SelectedMonster);
        }

        private void img4_Click(object sender, EventArgs e)
        {
            frmLoadMonster loadMonster = new frmLoadMonster();
            loadMonster.MonsterList = CombinedList;
            loadMonster.ShowDialog();
            if (loadMonster.SelectedMonster != 0) SetMonsterSlot(4, loadMonster.SelectedMonster);
        }

        private void img5_Click(object sender, EventArgs e)
        {
            frmLoadMonster loadMonster = new frmLoadMonster();
            loadMonster.MonsterList = CombinedList;
            loadMonster.ShowDialog();
            if (loadMonster.SelectedMonster != 0) SetMonsterSlot(5, loadMonster.SelectedMonster);
        }

        private void img6_Click(object sender, EventArgs e)
        {
            frmLoadMonster loadMonster = new frmLoadMonster();
            loadMonster.MonsterList = CombinedList;
            loadMonster.ShowDialog();
            if (loadMonster.SelectedMonster != 0) SetMonsterSlot(6, loadMonster.SelectedMonster);
        }
    }
    public class CombinedCard : PadMonster
    {
        public bool jp_only { get; set; }
        public string jp_name { get; set; }
        public CombinedCard()
            : base()
        {
        }
        public CombinedCard(PadMonster toCopy)
            : base(toCopy)
        {
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
        public int random_flags { get; set; }
        public int unknown_009 { get; set; }
        public bool portrait_exists { get; set; }
        public PadMonster()
        {
            awakenings = new List<int>();
            super_awakenings = new List<int>();
        }
        public PadMonster(PadMonster toCopy)
        {
            this.active_skill_id = toCopy.active_skill_id;
            this.ancestor_id = toCopy.ancestor_id;
            this.atk_exponent = toCopy.atk_exponent;
            this.attr_id = toCopy.attr_id;
            this.awakenings = toCopy.awakenings; ;
            this.base_id = toCopy.base_id;
            this.card_id = toCopy.card_id;
            this.cost = toCopy.cost;
            this.evo_mat_id1 = toCopy.evo_mat_id1;
            this.evo_mat_id2 = toCopy.evo_mat_id2;
            this.evo_mat_id3 = toCopy.evo_mat_id3;
            this.evo_mat_id4 = toCopy.evo_mat_id4;
            this.evo_mat_id5 = toCopy.evo_mat_id5;
            this.hp_exponent = toCopy.hp_exponent;
            this.inheritable = toCopy.inheritable;
            this.is_collab = toCopy.is_collab;
            this.is_ult = toCopy.is_ult;
            this.leader_skill_id = toCopy.leader_skill_id;
            this.limit_mult = toCopy.limit_mult;
            this.max_atk = toCopy.max_atk;
            this.max_hp = toCopy.max_hp;
            this.max_rcv = toCopy.max_rcv;
            this.min_atk = toCopy.min_atk;
            this.min_hp = toCopy.min_hp;
            this.min_rcv = toCopy.min_rcv;
            this.name = toCopy.name;
            this.rarity = toCopy.rarity;
            this.rcv_exponent = toCopy.rcv_exponent;
            this.released_status = toCopy.released_status;
            this.sell_mp = toCopy.sell_mp;
            this.subb_attr_id = toCopy.subb_attr_id;
            this.super_awakenings = toCopy.super_awakenings;
            this.type_1_id = toCopy.type_1_id;
            this.type_2_id = toCopy.type_2_id;
            this.type_3_id = toCopy.type_3_id;
            this.un_evo_mat_1 = toCopy.un_evo_mat_1;
            this.un_evo_mat_2 = toCopy.un_evo_mat_2;
            this.un_evo_mat_3 = toCopy.un_evo_mat_3;
            this.un_evo_mat_4 = toCopy.un_evo_mat_4;
            this.un_evo_mat_5 = toCopy.un_evo_mat_5;
            this.xp_gr = toCopy.xp_gr;
            this.xp_max = toCopy.xp_max;
            this.random_flags = toCopy.random_flags;
            this.unknown_009 = toCopy.unknown_009;
            this.portrait_exists = toCopy.portrait_exists;
        }
    }
}
