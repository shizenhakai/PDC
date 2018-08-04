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
using System.Resources;
using Newtonsoft.Json;


namespace PAD
{
    public partial class frmMain : Form
    {
        Dictionary<int, CombinedCard> CombinedList;
        Dictionary<int, PadMonster> NAList;
        Dictionary<int, PadMonster> JPList;
        List<CombinedCard>Team;
        string PadDataPath= @"E:\PadSync\";
        public frmMain()
        {
            InitializeComponent();
            NAList = new Dictionary<int, PadMonster>();
            JPList = new Dictionary<int, PadMonster>(); ;
            CombinedList = new Dictionary<int, CombinedCard>();
            LoadMonsterList(PadDataPath + @"paddata\processed\na_cards.json", NAList);
            LoadMonsterList(PadDataPath + @"paddata\processed\jp_cards.json", JPList);
            CombineLists();
            Team = new List<CombinedCard>();
            Team.Add(CombinedList[3305]);
            Team.Add(CombinedList[3305]);
            Team.Add(CombinedList[3305]);
            Team.Add(CombinedList[3305]);
            Team.Add(CombinedList[3305]);
            Team.Add(CombinedList[3305]);
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
            Team[TeamSlot] = CombinedList[MonsterNo];
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
            Team[TeamSlot].level = 99;
            Team[TeamSlot].cur_atk = Team[TeamSlot].max_atk;
            Team[TeamSlot].cur_hp = Team[TeamSlot].max_hp;
            Team[TeamSlot].cur_rcv = Team[TeamSlot].max_rcv;
            if (CombinedList[MonsterNo].jp_only) MonsterPortrait.Load(PadDataPath + @"padimages\jp\portrait\" + MonsterNo.ToString() + ".png");
            else MonsterPortrait.Load(PadDataPath + @"padimages\na\portrait\" + MonsterNo.ToString() + ".png");
            if (CombinedList[MonsterNo].type_1_id != -1) Type1.Load(PadDataPath + @"padimages\icons\types\" + CombinedList[MonsterNo].type_1_id.ToString() + ".png");
            if (CombinedList[MonsterNo].type_2_id != -1) Type2.Load(PadDataPath + @"padimages\icons\types\" + CombinedList[MonsterNo].type_2_id.ToString() + ".png");
            if (CombinedList[MonsterNo].type_3_id != -1) Type3.Load(PadDataPath + @"padimages\icons\types\" + CombinedList[MonsterNo].type_3_id.ToString() + ".png");
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
                                    case "max_level":
                                        curMonster.max_level = convertedInt;
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
                                    case "sub_attr_id":
                                        curMonster.sub_attr_id = convertedInt;
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
                                            if (awakenings.Count > 0)
                                            {
                                                curMonster.super_awakenings = awakenings;
                                            }
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

            
            //Assume Noctis Leader skill
            //4: dark
            //3: light
            //2: green
            //1: blue
            //0: red
            List<LeaderSkillItem> LeaderSkill= new List<LeaderSkillItem>();
            List<List<int>> Combos = new List<List<int>>();
            List<List<int>> Enhance = new List<List<int>>();
            List<int> OE = new List<int>();
            List<int> Rows = new List<int>();
            List<int> Crosses = new List<int>();
            List<List<int>> Ls = new List<List<int>>();
            List<int> Boxes = new List<int>();
            List<int> TPAs = new List<int>();
            int comboCount = 0;
            OE.Add(0);
            OE.Add(0);
            OE.Add(0);
            OE.Add(0);
            OE.Add(0);
            Rows.Add(0);
            Rows.Add(0);
            Rows.Add(0);
            Rows.Add(0);
            Rows.Add(0);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < Team[i].awakenings.Count-1; j++)
                {
                    if (Team[i].awakenings[j] == 14) OE[0]++;
                    if (Team[i].awakenings[j] == 15) OE[1]++;
                    if (Team[i].awakenings[j] == 16) OE[2]++;
                    if (Team[i].awakenings[j] == 17) OE[3]++;
                    if (Team[i].awakenings[j] == 18) OE[4]++;
                    if (Team[i].awakenings[j] == 22) Rows[0]++;
                    if (Team[i].awakenings[j] == 23) Rows[1]++;
                    if (Team[i].awakenings[j] == 24) Rows[2]++;
                    if (Team[i].awakenings[j] == 25) Rows[3]++;
                    if (Team[i].awakenings[j] == 26) Rows[4]++;
                }
                List<int> colorCombo = new List<int>();
                List<int> colorEnhance = new List<int>();
                List<int> colorOE = new List<int>();
                List<int> colorL = new List<int>();
                int colorTPAs = 0;
                int colorCross = 0;
                int colorBox = 0;
                for (int j = 1; j < 5; j++)
                {
                    string colorName = "";
                    switch(i)
                    {
                        case 0: //Red
                            colorName += "Red";
                            break;
                        case 1: //Blue
                            colorName += "Blue";
                            break;
                        case 2: //Green
                            colorName += "Green";
                            break;
                        case 3: //Light
                            colorName += "Light";
                            break;
                        case 4: //Dark
                            colorName += "Dark";
                            break;
                        default:
                            colorName = "ERROR";
                            break;

                    }
                    if (colorName == "ERROR")
                        return;
                    ComboSelect cmb = this.Controls.Find(colorName + "Combo" + j.ToString(), false).FirstOrDefault() as ComboSelect;
                    if (cmb.num_orbs != 0)
                    {
                        ComboSelect enhance = this.Controls.Find(colorName + "Enhance" + j.ToString(), false).FirstOrDefault() as ComboSelect;
                        if (enhance.num_orbs > cmb.num_orbs) enhance.num_orbs = cmb.num_orbs;
                        colorEnhance.Add(enhance.num_orbs);
                        colorCombo.Add(cmb.num_orbs);
                        comboCount++;
                        if (cmb.l == true) colorL.Add(1);
                        else colorL.Add(0);
                    }
                    if (cmb.cross == true) colorCross++;
                    if (cmb.num_orbs == 4) colorTPAs++;
                    if (cmb.box == true) colorBox++;
                }
                Combos.Add(colorCombo);
                Enhance.Add(colorEnhance);
                Crosses.Add(colorCross);
                Ls.Add(colorL);
                Boxes.Add(colorBox);
                TPAs.Add(colorTPAs);
            }
            LeaderSkillItem Noctis = new LeaderSkillItem();
            Noctis.skill_type = LeaderSkillTypes.static_mult;
            Noctis.arguments.Add(1.5f);
            Noctis.arguments.Add(0);
            Noctis.arguments.Add(10);
            Noctis.arguments.Add(11);
            Noctis.arguments.Add(12);
            Noctis.arguments.Add(13);
            Noctis.arguments.Add(14);
            LeaderSkill.Add(Noctis);
            Noctis = new LeaderSkillItem();
            Noctis.skill_type = LeaderSkillTypes.combo;
            Noctis.arguments.Add(3);
            Noctis.arguments.Add(4.5f);
            Noctis.arguments.Add(1);
            Noctis.arguments.Add(7.5f);
            LeaderSkill.Add(Noctis);
            //int numRow = 0; // TODO: add row support
            List<int> MainAttDamage = new List<int>();
            List<int> SubAttDamage = new List<int>();
            for (int i = 0; i < Team.Count - 1; i++)
            {
                if (i > 0) break; //Just doing first member for now
                //Do Main Att Combos
                int numTPA = 0;
                int num7c = 0;
                int numVDP = 0;
                int numL = 0;
                int num10c = 0;
                int numLowHP = 0;
                int numHighHP = 0;
                for (int j =0;j<Team[i].awakenings.Count-1;j++)
                {
                    if (Team[i].awakenings[j] == 27) numTPA++;
                    if (Team[i].awakenings[j] == 48) numVDP++;
                    if (Team[i].awakenings[j] == 43) num7c++;
                    if (Team[i].awakenings[j] == 60) numL++;
                    if (Team[i].awakenings[j] == 61) num10c++;
                    if (Team[i].awakenings[j] == 57) numHighHP++;
                    if (Team[i].awakenings[j] == 58) numLowHP++;
                }
                double comboDamage = 0;
                int color = Team[i].attr_id;
                for (int j=0;j<Combos[color].Count;j++)
                {
                    double vdp_mult = Math.Pow(2.5, numVDP) * Boxes[color];
                    double tpa_mult = Math.Pow(1.5, numTPA) * TPAs[color];
                    double OE_mult;
                    double L_mult; 
                    if (vdp_mult == 0) vdp_mult = 1;
                    if (tpa_mult == 0) tpa_mult = 1;
                    if (Ls[color][j] == 1) L_mult = numL * 2.5;
                    else L_mult = 1;
                    if (Enhance[color][j] != 0) OE_mult = (1 + 0.06 * Enhance[color][j]) * (1 + 0.05 * OE[color]);
                    else OE_mult = 1;
                    //if (Combos[Team[i].attr_id]) ;
                    comboDamage += Team[i].cur_atk *
                        (1 + (0.25 * (Combos[color][j] - 3))) *
                        (tpa_mult) *
                        (vdp_mult) *
                        (OE_mult) *
                        (L_mult);

                }
                double low_hp_mult = 1;
                double high_hp_mult = 1;
                double row_mult = 1;
                double _7c_mult = 1;
                double SFU_mult = 1;
                double LS_mult = 1;
                bool valid = false;
                foreach (LeaderSkillItem item in LeaderSkill)
                {
                    switch(item.skill_type)
                    {
                        case LeaderSkillTypes.static_mult:
                            valid = false;
                            if (item.arguments[1] == 1) valid = true;
                            for (int j = 2; j < item.arguments.Count; j++)
                            {
                                if (item.arguments[1] == 0)
                                {
                                    if (item.arguments[j] == Team[i].attr_id + 10) valid = true;
                                }
                                else
                                {
                                    if (item.arguments[j] > 99)//check type
                                    {
                                        int item_type = (int)item.arguments[j] - 100;
                                        if ((Team[i].type_1_id != item_type) || (Team[i].type_2_id != item_type) || (Team[i].type_3_id != item_type))
                                            valid = false;
                                    }
                                    if (item.arguments[j] < 99)//check color
                                    {
                                        int item_color = (int)item.arguments[j] - 10;
                                        if ((Team[i].attr_id != item_color) || (Team[i].sub_attr_id != item_color))
                                            valid = false;
                                    }
                                }
                            }
                            if (valid) LS_mult = LS_mult * item.arguments[0];
                            break;
                        case LeaderSkillTypes.combo:
                            double combo_mult = 1;
                            if (comboCount >= item.arguments[0])
                                combo_mult = item.arguments[1] + (comboCount - item.arguments[0]) * item.arguments[2];
                            if (combo_mult > item.arguments[3]) combo_mult = item.arguments[3];
                            LS_mult = LS_mult * combo_mult;
                            break;
                    }
                }
                if (comboCount > 6) _7c_mult = Math.Pow(2, num7c);
                if (numLowHP > 0) low_hp_mult = Math.Pow(2, numLowHP);
                if (numHighHP > 0) high_hp_mult = Math.Pow(1.5, numHighHP);
                double TotalDamge = (1 + 0.255 * (comboCount - 1)) *
                    row_mult *
                    _7c_mult *
                    SFU_mult *
                    low_hp_mult *
                    high_hp_mult *
                    LS_mult;
            }
            switch (Team[1].sub_attr_id)
            {
                case 0:
                    lblTotalDamage.Text = "0";
                    break;
            }
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

        private void nmLVL1_ValueChanged(object sender, EventArgs e)
        {
            Team[0].level = (int)nmLVL1.Value;
            int card_atk = (int)Math.Round(Team[0].min_atk + ((Team[0].max_atk - Team[0].min_atk) * Math.Pow(((double)(Team[0].level - 1) / (double)(Team[0].max_level - 1)), Team[0].atk_exponent)));
            card_atk += (int)nmATK1.Value * 5;
            int card_rcv = (int)Math.Round(Team[0].min_rcv + ((Team[0].max_rcv - Team[0].min_rcv) * Math.Pow(((double)(Team[0].level - 1) / (double)(Team[0].max_level - 1)), Team[0].rcv_exponent)));
            card_rcv += (int)nmRCV1.Value * 3;
            int card_hp = (int)Math.Round(Team[0].min_hp + ((Team[0].max_hp - Team[0].min_hp) * Math.Pow(((double)(Team[0].level - 1) / (double)(Team[0].max_level - 1)), Team[0].hp_exponent)));
            card_hp += (int)nmHP1.Value * 10;
            lblATK1.Text = card_atk.ToString();
            lblHP1.Text = card_hp.ToString();
            lblRCV1.Text = card_rcv.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num_orbs = LightCombo1.num_orbs;
            lblMain1.Text = num_orbs.ToString();

        }

        private void cmbLightCombo1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void cmbLightCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void imgLightCombo1_Click(object sender, EventArgs e)
        {
        }

        private void imgLightCombo1_MouseUp(object sender, MouseEventArgs e)
        {
        }
    }

    public class ComboSelect : PictureBox
    {
        public int num_orbs;
        public bool cross = false;
        public bool l = false;
        public bool box = false;
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            PictureBox picture = new PictureBox();
            picture.Image = Properties.Resources.popup_grid;
            picture.Refresh();

            picture.Width = 206;
            picture.Height = 206;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.BorderStyle = BorderStyle.FixedSingle;
            picture.MouseUp += (object event_sender, MouseEventArgs event_e) =>
            {
                cross = false;
                l = false;
                box = false;
                int X = (event_e.X - (event_e.X % (picture.Width / 6))) / (picture.Width / 6);
                int Y = (event_e.Y - (event_e.Y % (picture.Height / 6))) / (picture.Height / 6);
                if (X == 0 && Y == 0)                    num_orbs = 0;
                if (X == 1 && Y == 0) num_orbs = 3;
                if (X == 2 && Y == 0) num_orbs = 4;
                if (X == 3 && Y == 0) num_orbs = 5;
                if (X == 4 && Y == 0)
                {
                    num_orbs = 5;
                    this.Image = (Image)Properties.Resources.cross;
                    cross = true;
                }
                if(X == 5 && Y == 0)
                {
                    this.Image = (Image)Properties.Resources.L;
                    num_orbs = 5;
                    l = true;
                }
                if (X == 0 && Y == 1) num_orbs = 6;
                if (X == 1 && Y == 1) num_orbs = 7;
                if (X == 2 && Y == 1) num_orbs = 8;
                if (X == 3 && Y == 1) num_orbs = 9;
                if (X == 4 && Y == 1)
                {
                    this.Image = (Image)Properties.Resources.box;
                    num_orbs = 9;
                    box = true;
                }
                if (X == 5 && Y == 1) num_orbs = 10;

                if (X == 0 && Y == 2) num_orbs = 11;
                if (X == 1 && Y == 2) num_orbs = 12;
                if (X == 2 && Y == 2) num_orbs = 13;
                if (X == 3 && Y == 2) num_orbs = 14;
                if (X == 4 && Y == 2) num_orbs = 15;
                if (X == 5 && Y == 2) num_orbs = 16;

                if (X == 0 && Y == 3) num_orbs = 17;
                if (X == 1 && Y == 3) num_orbs = 18;
                if (X == 2 && Y == 3) num_orbs = 19;
                if (X == 3 && Y == 3) num_orbs = 20;
                if (X == 4 && Y == 3) num_orbs = 21;
                if (X == 5 && Y == 3) num_orbs = 22;

                if (X == 0 && Y == 4) num_orbs = 23;
                if (X == 1 && Y == 4) num_orbs = 24;
                if (X == 2 && Y == 4) num_orbs = 25;
                if (X == 3 && Y == 4) num_orbs = 26;
                if (X == 4 && Y == 4) num_orbs = 27;
                if (X == 5 && Y == 4) num_orbs = 28;

                if (X == 0 && Y == 5) num_orbs = 29;
                if (X == 1 && Y == 5) num_orbs = 30;
                if(box==false&&cross==false&&l==false)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + num_orbs.ToString() + "_orbs");
                }

                PopupForm parent = (PopupForm)picture.Parent;
                parent.Close();
            };
            PopupForm popup = new PopupForm(picture,
                new Point(
                    this.PointToScreen(Point.Empty).X + e.X,
                    this.PointToScreen(Point.Empty).Y + e.Y), PopUpResponse);
            popup.Show();

        }
        private void PopUpResponse() { }
    }

    public class PopupForm : Form
    {
        private Action _OnAccept;
        private Control _control;
        private Point _point;
        public PopupForm(Control control, int x, int y, Action onAccept)
            : this(control, new Point(x,y), onAccept)
        {}
        public PopupForm(Control control, Point point, Action onAccept)
        {
            if (control == null) throw new ArgumentNullException("control");
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.KeyPreview = true;
            _point = point;
            _control = control;
            _OnAccept = onAccept;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Controls.Add(_control);
            _control.Location = new Point(0, 0);
            this.Size = _control.Size;
            this.Location = _point;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(e.KeyCode==Keys.Enter)
            {
                _OnAccept();
                this.Close();
            }
            else if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Close();
        }
    }
    
    public enum LeaderSkillTypes : int
    {
        //Static Multiplier
        //Arguments is a list of applicable types
        //First argument is multiplier
        //Second argument is 1 for All Required 0 for Any Required
        //Third argument is:
        //  Add 10 for color
        //  Add 100 for type 
        static_mult = 0,
        //Combo Multiplier
        //Arguments are:
        //0: starting combo count
        //1: starting multiplier
        //2: scaling
        //3: max multiplier
        combo=1,
        //Sparkle Mult
        //To Implement
        sparkle=2,
        //Linked Orbs Mult
        //To Implement
        linked_orbs=3
    };
    public class LeaderSkillItem
    {
        public LeaderSkillTypes skill_type;
        public List<double> arguments;
        public LeaderSkillItem()
        {
            arguments = new List<double>();
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
        public int level { get; set; }
        public int limit_mult { get; set; }
        public int max_atk { get; set; }
        public int max_hp { get; set; }
        public int max_rcv { get; set; }
        public int max_level { get; set; }
        public int min_atk { get; set; }
        public int min_hp { get; set; }
        public int min_rcv { get; set; }
        public int cur_atk { get; set; }
        public int cur_hp { get; set; }
        public int cur_rcv { get; set; }
        public string name { get; set; }
        public int rarity { get; set; }
        public double rcv_exponent { get; set; }
        public bool released_status { get; set; }
        public int sell_mp { get; set; }
        public int sub_attr_id { get; set; }
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
            this.level = toCopy.level;
            this.limit_mult = toCopy.limit_mult;
            this.max_atk = toCopy.max_atk;
            this.max_hp = toCopy.max_hp;
            this.max_rcv = toCopy.max_rcv;
            this.max_level = toCopy.max_level;
            this.min_atk = toCopy.min_atk;
            this.min_hp = toCopy.min_hp;
            this.min_rcv = toCopy.min_rcv;
            this.cur_atk = toCopy.cur_atk;
            this.cur_hp = toCopy.cur_hp;
            this.cur_rcv = toCopy.cur_rcv;
            this.name = toCopy.name;
            this.rarity = toCopy.rarity;
            this.rcv_exponent = toCopy.rcv_exponent;
            this.released_status = toCopy.released_status;
            this.sell_mp = toCopy.sell_mp;
            this.sub_attr_id = toCopy.sub_attr_id;
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
