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
        Dictionary<int, PADMonster> MonsterList;
        Dictionary<int, NewPadMonster> NewMonsterList;
        string PadDataPath= @"E:\PadSync\";
        int curMon = 1;
        public frmMain()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //LoadMonsterList();
            NewLoadMonsterList();
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
            switch (TeamSlot)
            {
                case 1:
                    MonsterPortrait = img1;
                    Type1 = img1Type1;
                    Type2 = img1Type2;
                    Type3 = img1Type3;
                    MonsterName = lblMonster1;
                    HP = lblHP1;
                    ATK = lblATK1;
                    RCV = lblRCV1;
                    LVL = nmLVL1;
                    PlusHP = nmHP1;
                    PlusATK = nmATK1;
                    PlusRCV = nmRCV1;
                    break;
                case 2:
                    MonsterPortrait = img2;
                    Type1 = img2Type1;
                    Type2 = img2Type2;
                    Type3 = img2Type3;
                    MonsterName = lblMonster2;
                    HP = lblHP2;
                    ATK = lblATK2;
                    RCV = lblRCV2;
                    LVL = nmLVL2;
                    PlusHP = nmHP2;
                    PlusATK = nmATK2;
                    PlusRCV = nmRCV2;
                    break;
                case 3:
                    MonsterPortrait = img3;
                    Type1 = img3Type1;
                    Type2 = img3Type2;
                    Type3 = img3Type3;
                    MonsterName = lblMonster3;
                    HP = lblHP3;
                    ATK = lblATK3;
                    RCV = lblRCV3;
                    LVL = nmLVL3;
                    PlusHP = nmHP3;
                    PlusATK = nmATK3;
                    PlusRCV = nmRCV3;
                    break;
                case 4:
                    MonsterPortrait = img4;
                    Type1 = img4Type1;
                    Type2 = img4Type2;
                    Type3 = img4Type3;
                    MonsterName = lblMonster4;
                    HP = lblHP4;
                    ATK = lblATK4;
                    RCV = lblRCV4;
                    LVL = nmLVL4;
                    PlusHP = nmHP4;
                    PlusATK = nmATK4;
                    PlusRCV = nmRCV4;
                    break;
                case 5:
                    MonsterPortrait = img5;
                    Type1 = img5Type1;
                    Type2 = img5Type2;
                    Type3 = img5Type3;
                    MonsterName = lblMonster5;
                    HP = lblHP5;
                    ATK = lblATK5;
                    RCV = lblRCV5;
                    LVL = nmLVL5;
                    PlusHP = nmHP5;
                    PlusATK = nmATK5;
                    PlusRCV = nmRCV5;
                    break;
                case 6:
                    MonsterPortrait = img6;
                    Type1 = img6Type1;
                    Type2 = img6Type2;
                    Type3 = img6Type3;
                    MonsterName = lblMonster6;
                    HP = lblHP6;
                    ATK = lblATK6;
                    RCV = lblRCV6;
                    LVL = nmLVL6;
                    PlusHP = nmHP6;
                    PlusATK = nmATK6;
                    PlusRCV = nmRCV6;
                    break;
                default:
                    return 1;
            }
            //MonsterName.Text = MonsterList[MonsterNo].TM_Name_US;
            MonsterName.Text = NewMonsterList[MonsterNo].name;
            while(MonsterName.Width < System.Windows.Forms.TextRenderer.MeasureText(MonsterName.Text, new Font(MonsterName.Font.FontFamily, MonsterName.Font.Size, MonsterName.Font.Style)).Width)
            {
                MonsterName.Font = new Font(MonsterName.Font.FontFamily, MonsterName.Font.Size - 0.1f, MonsterName.Font.Style);
            }
            HP.Text = (NewMonsterList[MonsterNo].max_hp + 990).ToString();
            ATK.Text = (NewMonsterList[MonsterNo].max_atk + 495).ToString();
            RCV.Text = (NewMonsterList[MonsterNo].max_rcv + 297).ToString();
            LVL.Value = 99;
            PlusATK.Value = 99;
            PlusHP.Value = 99;
            PlusRCV.Value = 99;
            MonsterPortrait.Load(PadDataPath + @"padimages\na\portrait\" + MonsterNo.ToString() + ".png");
            if (NewMonsterList[MonsterNo].type_1_id != -1) Type1.Load(PadDataPath + @"padimages\icons\types\" + NewMonsterList[MonsterNo].type_1_id.ToString() + ".png");
            if (NewMonsterList[MonsterNo].type_2_id != -1) Type2.Load(PadDataPath + @"padimages\icons\types\" + NewMonsterList[MonsterNo].type_2_id.ToString() + ".png");
            if (NewMonsterList[MonsterNo].type_3_id != -1) Type3.Load(PadDataPath + @"padimages\icons\types\" + NewMonsterList[MonsterNo].type_3_id.ToString() + ".png");
            return 0;
        }
        public void NewLoadMonsterList()
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader(PadDataPath + @"paddata\processed\na_cards.json"));
            string curObject="root";
            NewMonsterList = new Dictionary<int, NewPadMonster>();
            NewPadMonster curMonster = new NewPadMonster();
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
                            if(!NewMonsterList.ContainsKey(curMonster.card_id)) NewMonsterList.Add(curMonster.card_id, curMonster);
                            curMonster = new NewPadMonster();
                            //save card
                        }
                        break;
                }
            }
            Console.WriteLine("Added {0} monsters to dictionary.", NewMonsterList.Count);
        }
        public void LoadMonsterList()
        {
            MonsterList = new Dictionary<int, PADMonster>();
            JsonTextReader reader = new JsonTextReader(new StreamReader(PadDataPath + @"paddata\padguide\monsterList.json"));
            bool started = false;
            string PropertyName = "";
            PADMonster curMonster = new PADMonster();
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.StartArray) started = true;
                if (started)
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.StartObject:
                            curMonster = new PADMonster();
                            break;
                        case JsonToken.EndObject:
                            if (!MonsterList.ContainsKey(curMonster.Monster_No)) MonsterList.Add(curMonster.Monster_No, curMonster);
                            break;
                        default:
                            if (reader.Value != null)
                            {
                                if (reader.TokenType == JsonToken.PropertyName)
                                {
                                    PropertyName = (string)reader.Value;
                                    int convertedInt = 0;
                                    reader.Read();
                                    switch (PropertyName)
                                    {
                                        case "EXP":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.EXP = convertedInt;
                                            break;
                                        case "COST":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Cost = convertedInt;
                                            break;
                                        case "HP_MAX":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.HP_Max = convertedInt;
                                            break;
                                        case "HP_MIN":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.HP_Min = convertedInt;
                                            break;
                                        case "LEVEL":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Level = convertedInt;
                                            break;
                                        case "LIMIT_MULT":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Limit_Mult = convertedInt;
                                            break;
                                        case "MONSTER_NO":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Monster_No = convertedInt;
                                            break;
                                        case "RARITY":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Rarity = convertedInt;
                                            break;
                                        case "TM_NAME_US":
                                            curMonster.TM_Name_US = reader.Value.ToString();
                                            break;
                                        case "RCV_MAX":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.RCV_Max = convertedInt;
                                            break;
                                        case "RCV_MIN":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.RCV_Min = convertedInt;
                                            break; ;
                                        case "ATK_MAX":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.ATK_Max = convertedInt;
                                            break;
                                        case "ATK_MIN":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.ATK_Min = convertedInt; ;
                                            break;
                                        case "MONSTER_NO_JP":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Monster_No_JP = convertedInt;
                                            break;
                                        case "MONSTER_NO_KR":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Monster_No_KR = convertedInt;
                                            break;
                                        case "MONSTER_NO_US":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Monster_No_US = convertedInt;
                                            break;
                                        case "RATIO_ATK":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Ratio_ATK = convertedInt;
                                            break;
                                        case "RATIO_HP":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Ratio_HP = convertedInt;
                                            break;
                                        case "RATIO_RCV":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.Ratio_RCV = convertedInt;
                                            break;
                                        case "TM_NAME_JP":
                                            curMonster.TM_Name_JP = reader.Value.ToString();
                                            break;
                                        case "TM_NAME_KR":
                                            curMonster.TM_Name_JP = reader.Value.ToString();
                                            break;
                                        case "TS_SEQ_LEADER":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.TS_Seq_Leader = convertedInt;
                                            break;
                                        case "TS_SEQ_SKILL":
                                            int.TryParse(reader.Value.ToString(), out convertedInt);
                                            curMonster.TS_Seq_Skill = convertedInt;
                                            break;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

    }
    public class NewPadMonster
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
        public NewPadMonster()
        {
            awakenings = new List<int>();
            super_awakenings = new List<int>();
        }
    }
    public class PADMonster
    {
        public int EXP { get; set; }
        public int Cost { get; set; }
        public int HP_Max { get; set; }
        public int HP_Min { get; set; }
        public int RCV_Max { get; set; }
        public int RCV_Min { get; set; }
        public int ATK_Max { get; set; }
        public int ATK_Min { get; set; }

        public int Level { get; set; }
        public int Limit_Mult { get; set; }
        public int Monster_No { get; set; }
        public int Monster_No_JP { get; set; }
        public int Monster_No_KR { get; set; }
        public int Monster_No_US { get; set; }
        public int Rarity { get; set; }
        public int Ratio_ATK { get; set; }
        public int Ratio_HP { get; set; }
        public int Ratio_RCV { get; set; }

        public string TM_Name_JP { get; set; }
        public string TM_Name_KR { get; set; }
        public string TM_Name_US { get; set; }
        public int TS_Seq_Leader { get; set; }
        public int TS_Seq_Skill { get; set; }

    }
}
