﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace PAD
{
    [FlagsAttribute] public enum MULTI_COLOR : short
    {
        FIRE = 1,
        WATER = 2,
        WOOD = 4,
        LIGHT = 8,
        DARK = 16,
        HEAL = 32,
        JAMMER = 64,
        POISON = 128,
        LETHAL_POISON = 256
    };

    [FlagsAttribute] public enum MULTI_TYPE : short
    {
        //NOT VERIFIED
        EVO = 1,
        BALANCE=2,
        PHYSICAL=4,
        HEALER=8,
        DRAGON=16,
        GOD=32,
        ATTACKER=64,
        DEVIL=128,
        MACHINE=256,
        AWAKENING=512,
        ENHANCE=1024,
        REDEEMABLE=2048
    };
    public enum SINGLE_COLOR : short
    {
        FIRE=0,
        WATER=1,
        WOOD=2,
        LIGHT=3,
        DARK=4
    };
    public enum SINGLE_TYPE : short
    {
        EVO=0,
        BALANCE=1,
        PHYSICAL=2,
        HEALER=3,
        DRAGON=4,
        GOD=5,
        ATTACKER=6,
        DEVIL=7,
        MACHINE=8,
        AWAKENING=12,
        ENHANCE=14,
        REDEEMABLE=16
    };
    public enum SKILL_TYPES : short
    {
        /// <summary>
        /// Attack on all enemies of chosen color Arguments: SINGLE_COLOR, damage
        /// </summary>
        active_aoe_nuke = 1,
        /// <summary>
        /// (own_attack_mult)
        /// </summary>
        active_single_nuke=2,
        /// <summary>
        /// (turns, shield%)
        /// </summary>
        active_shield=3,
        active_poison=4,
        active_change_the_world=5,
        active_gravity=6,
        active_rcv_based_recovery = 7,
        active_flat_recovery = 8,
        active_change_one_orbtype_to_another = 9,
        active_shuffle_orbs = 10,
        ls_color_atk_mult = 11,
        ls_bonus_attack = 12,
        ls_bonus_heal = 13,
        ls_resolve = 14,
        ls_flat_movetime = 15,
        ls_unconditional_shield = 16,
        ls_color_damage_reduce = 17,
        active_delay = 18,
        active_reduce_defense = 19,
        active_double_orb_change = 20,
        active_color_reduce = 21,
        ls_type_atk_mult = 22,
        ls_type_hp_mult = 23,
        ls_type_rcv_mult = 24,
        ls_generic_atk_mult = 26,
        ls_color_atk_and_rcv = 28,
        ls_color_all_stats = 29,
        ls_double_type_hp = 30,
        ls_double_type_atk = 31,
        ls_drum_sounds = 33,
        active_attack_and_drain = 35,
        ls_double_color_damage_reduce = 36,
        active_attack_single_target_atkmult = 37,
        ls_low_hp_conditional_shield = 38,
        ls_low_hp_mult = 39,
        ls_double_color_atkmult = 40,
        ls_counter_attack = 41,
        active_color_targetted_nuke = 42,
        ls_high_hp_conditional_shield = 43,
        ls_high_hp_stat_mult = 44,
        ls_HP_and_ATK_mult_for_color = 45,
        ls_HP_double_color = 46,
        ls_hp_color = 48,
        ls_rcv_color = 49,
        active_stat_boost = 50,
        active_mass_attack = 51,
        active_enhance_1_color_orbs = 52,
        ls_coin_modifier = 54,
        active_single_target_flat_damage_ignore_defense = 55,
        active_aoe_flat_damage_ignore_defense = 56,
        active_aoe_color_random_damage = 58,
        active_single_target_damage = 59,
        active_color_counter = 60,
        ls_3_color_activation = 61,
        ls_ATK_and_HP_type_mult = 62,
        ls_HP_and_RCV_type_mult = 63,
        ls_ATK_and_RCV_type_mult = 64,
        ls_All_Stats_for_Type = 65,
        ls_combo = 66,
        ls_HP_and_RCV_color = 67,
        ls_ATK_color_and_type = 69,
        active_board_change = 71,
        ls_ATK_and_HP_color_and_type = 73,
        ls_ATK_and_RCV_color_and_type = 75,
        ls_All_stats_color_and_type = 76,
        ls_ATK_and_HP_double_type = 77,
        ls_ATK_and_RCV_double_type = 79,
        active_color_atk_single_target_nuke_and_self_harm = 84,
        active_color_atk_aoe_and_self_harm = 85,
        active_single_target_damage_and_self_harm = 86,
        active_aoe_damage_and_self_harm = 87,
        active_type_atk_mult = 88,
        active_double_color_spike = 90,
        active_double_color_orb_enhance = 91,
        active_double_type_spike = 92,
        active_lead_swap = 93,
        ls_low_hp_color_atk_mult = 94,
        ls_low_hp_type_atk_mult = 95,
        ls_high_hp_color_atk_mult = 96,
        ls_high_hp_type_atk_mult = 97,
        ls_scaling_combo = 98,
        ls_skill_use = 100,
        ls_exact_combo = 101,
        ls_double_att_combo = 103,
        ls_color_atk_combo = 104,
        ls_lower_rcv_increase_atk = 105,
        ls_lower_max_hp_increases_atk = 106,
        ls_lower_max_hp = 107,
        ls_lower_max_hp_and_type_atk = 108,
        ls_linked_orbs_atk = 109,
        active_hp_conditional_nuke = 110,
        ls_HP_and_ATK_double_color = 111,
        ls_all_stats_double_color = 114,
        active_single_target_color_nuke_and_drain = 115,
        multi_part_active_ = 116,
        active_heal_and_bind_reduction = 117,
        active_random_skill = 118,
        ls_scaling_linked_orb = 119,
        ls_mult_stat = 121,
        ls_low_hp_atk = 122,
        ls_high_hp_mult_stat = 123,
        ls_multiple_specific_combos = 124,
        ls_required_monster_on_team = 125,
        active_skyfall = 126,
        active_change_column = 127,
        active_change_row = 128,
        ls_new_stat = 129,
        ls_low_hp_stat = 130,
        ls_hight_hp_stat = 131,
        active_time_extend = 132,
        ls_skilluse = 133,
        ls_split_color_ls = 136,
        ls_split_type_ls = 137,
        ls_multi_part_skill = 138,
        ls_hp_scaling = 139,
        active_new_enhance = 140,
        active_create_orbs_randomly = 141,
        active_change_own_att = 142,
        active_team_hp_based_nuke = 143,
        active_team_color_based_nuke = 144,
        active_recover_team_rcv = 145,
        active_haste = 146,
        ls_increased_xp = 148,
        ls_rcv_increased_with_4_hearts = 149,
        ls_atk_with_5o1e = 150,
        ls_heart_cross = 151,
        active_lock_orbs = 152,
        active_change_enemy_att = 153,
        active_many_orb_to_1_type = 154,
        ls_multiplayer = 155,
        active_awakening_based = 156,
        ls_cross_mult = 157,
        ls_min_orbs_match = 158,
        ls_scaling_linked_match = 159,
        active_add_combo = 160,
        active_true_gravity = 161,
        ls_7x6_board = 162,
        ls_no_skyfall = 163,
        ls_multiple_color_activation = 164,
        ls_mult_att_match = 165,
        ls_new_combo = 166,
        ls_linked_orbs = 167,
        ls_combo_and_shield = 169,
        ls_multiple_color_activation_and_shield = 170,
        ls_new_multiple_color_activation = 171,
        active_remove_locks = 172,
        active_void_absorb = 173,
        ls_all_subs_of_a_type = 175,
        ls_orbs_left_on_board = 177,
        ls_fixed_move_time = 178,
        active_heal_over_time = 179,
        active_enhance_skyfall = 180,
        ls_linked_orb_with_shield = 182,
        ls_high_hp_gives_shield = 183,
        active_no_skyfall = 184,
        ls_movetime_with_mult = 185,
        ls_7x6_with_mult = 186,
        active_single_target_true_nuke = 188
    }
    public enum STAT_TYPE : int
    {
        HP=0,
        ATK=1,
        RCV=2
    };
    class LS_Results
    {
        public List<double> atk_mults;
        public List<double> rcv_mults;
        public List<double> hp_mults;
        public LS_Results()
        {
            atk_mults = new List<double>();
            rcv_mults = new List<double>();
            hp_mults = new List<double>();
        }
    }
    class Skill
    {
        public SKILL_TYPES skill_type { get; set; }
        public string skill_description { get; set; }
        public int turns_max { get; set; }
        public int turns_min { get; set; }
        public int skill_id { get; set; }
        public bool is_leaderskill { get; set; }
        public List<int> Arguments;
        private string _json_path { get; set; }
        public Skill()
        {
            Arguments = new List<int>();
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
        }
        public LS_Results GetLeaderskillMultiplier(List<List<int>> Combos, List<List<int>> OrbEnhances, int hp_percentage, bool skill_used, PadMonster monster)
        {
            double rcv_mult = 1;
            double atk_mult = 1;
            double hp_mult = 1;
            double shield = 1;
            LS_Results res = new LS_Results();
            int numCombos = 0;
            //if (is_leaderskill == false) return 0;
            for (int i = 0; i < Combos.Count; i++)
            {
                for (int j = 0; j < Combos[i].Count; j++)
                {
                    if (Combos[i][j] > 0)
                    {
                        numCombos++;
                    }
                }
            }
            switch (skill_type)
            {
                case SKILL_TYPES.ls_color_atk_mult:
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0])) res.atk_mults.Add(((double)Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_bonus_attack:
                    break;
                case SKILL_TYPES.ls_bonus_heal:
                    break;
                case SKILL_TYPES.ls_resolve:
                    break;
                case SKILL_TYPES.ls_flat_movetime:
                    break;
                case SKILL_TYPES.ls_unconditional_shield:
                    break;
                case SKILL_TYPES.ls_color_damage_reduce:
                    break;
                case SKILL_TYPES.ls_type_atk_mult:
                    if ((monster.type_1_id == Arguments[0]) || (monster.type_2_id == Arguments[0]) || (monster.type_3_id == Arguments[0])) res.atk_mults.Add(((double)Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_type_hp_mult:
                    break;
                case SKILL_TYPES.ls_type_rcv_mult:
                    break;
                case SKILL_TYPES.ls_generic_atk_mult:
                    res.atk_mults.Add(((double)Arguments[0]/100));
                    break;
                case SKILL_TYPES.ls_color_atk_and_rcv:
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0])) res.atk_mults.Add(((double)Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_color_all_stats:
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0])) res.atk_mults.Add(((double)Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_double_type_hp:
                    break;
                case SKILL_TYPES.ls_double_type_atk:
                    if ((monster.type_1_id == Arguments[0]) || (monster.type_2_id == Arguments[0]) || (monster.type_3_id == Arguments[0]) ||
                        (monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1])) res.atk_mults.Add(((double)Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_drum_sounds:
                    //DRUM SOUNDS!
                    break;
                case SKILL_TYPES.ls_double_color_damage_reduce:
                    //Reduces damage of the two colors
                    break;
                case SKILL_TYPES.ls_low_hp_conditional_shield:
                    break;
                case SKILL_TYPES.ls_low_hp_mult:
                    if (hp_percentage <= Arguments[0])
                    {
                        if ((Arguments[1] == (int)STAT_TYPE.ATK) || (Arguments[2] == (int)STAT_TYPE.ATK)) res.atk_mults.Add(((double)Arguments[3]/100));
                        if ((Arguments[1] == (int)STAT_TYPE.HP) || (Arguments[2] == (int)STAT_TYPE.HP)) hp_mult = hp_mult * Arguments[3];
                        if ((Arguments[1] == (int)STAT_TYPE.RCV) || (Arguments[2] == (int)STAT_TYPE.RCV)) rcv_mult = rcv_mult * Arguments[3];
                    }
                    break;
                case SKILL_TYPES.ls_double_color_atkmult:
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]) ||
                        (monster.attr_id == Arguments[1]) || (monster.sub_attr_id == Arguments[1])) res.atk_mults.Add(((double)Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_counter_attack:
                    break;
                case SKILL_TYPES.ls_high_hp_conditional_shield:
                    break;
                case SKILL_TYPES.ls_high_hp_stat_mult:
                    if ((Arguments[1] == (int)STAT_TYPE.ATK) || (Arguments[2] == (int)STAT_TYPE.ATK)) res.atk_mults.Add(((double)Arguments[3]/100));
                    if ((Arguments[1] == (int)STAT_TYPE.RCV) || (Arguments[2] == (int)STAT_TYPE.RCV)) rcv_mult = rcv_mult * Arguments[3];
                    if ((Arguments[1] == (int)STAT_TYPE.HP) || (Arguments[2] == (int)STAT_TYPE.HP)) hp_mult = hp_mult * Arguments[3];
                    break;
                case SKILL_TYPES.ls_HP_and_ATK_mult_for_color:
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]))
                    {
                        res.atk_mults.Add(((double)Arguments[1]/100));
                        hp_mult = hp_mult * Arguments[1];
                    }
                    break;
                case SKILL_TYPES.ls_HP_double_color:
                    break;
                case SKILL_TYPES.ls_hp_color:
                    break;
                case SKILL_TYPES.ls_rcv_color:
                    break;
                case SKILL_TYPES.ls_coin_modifier:
                    break;
                case SKILL_TYPES.ls_3_color_activation:
                    //colors, num_requirec,mult
                    int num_met = 0;
                    if ((Arguments[0] & (int)MULTI_COLOR.FIRE) == (int)MULTI_COLOR.FIRE) if (Combos[(int)SINGLE_COLOR.FIRE][0] > 0) num_met++;
                    if ((Arguments[0] & (int)MULTI_COLOR.WATER) == (int)MULTI_COLOR.WATER) if (Combos[(int)SINGLE_COLOR.WATER][0] > 0) num_met++;
                    if ((Arguments[0] & (int)MULTI_COLOR.WOOD) == (int)MULTI_COLOR.WOOD) if (Combos[(int)SINGLE_COLOR.WOOD][0] > 0) num_met++;
                    if ((Arguments[0] & (int)MULTI_COLOR.LIGHT) == (int)MULTI_COLOR.LIGHT) if (Combos[(int)SINGLE_COLOR.LIGHT][0] > 0) num_met++;
                    if ((Arguments[0] & (int)MULTI_COLOR.DARK) == (int)MULTI_COLOR.DARK) if (Combos[(int)SINGLE_COLOR.DARK][0] > 0) num_met++;
                    if (num_met >= Arguments[1]) res.atk_mults.Add(((double) Arguments[2]/100));
                    break;
                case SKILL_TYPES.ls_ATK_and_HP_type_mult:
                    //type,mult
                    if ((monster.type_1_id == Arguments[0]) || (monster.type_2_id == Arguments[0]) || (monster.type_3_id == Arguments[1]))
                    {
                        res.atk_mults.Add(((double) Arguments[1]/100));
                        hp_mult = hp_mult * Arguments[1];
                    }
                    break;
                case SKILL_TYPES.ls_HP_and_RCV_type_mult:
                    //type,mult
                    if ((monster.type_1_id == Arguments[0]) || (monster.type_2_id == Arguments[0]) || (monster.type_3_id == Arguments[1]))
                    {
                        rcv_mult = rcv_mult * Arguments[1];
                        hp_mult = hp_mult * Arguments[1];
                    }

                    break;
                case SKILL_TYPES.ls_ATK_and_RCV_type_mult:
                    //type,mult
                    if ((monster.type_1_id == Arguments[0]) || (monster.type_2_id == Arguments[0]) || (monster.type_3_id == Arguments[1]))
                    {
                        res.atk_mults.Add(((double)Arguments[1]/100));
                        rcv_mult = rcv_mult * Arguments[1];
                    }
                    break;
                case SKILL_TYPES.ls_All_Stats_for_Type:
                    //type,mult
                    if ((monster.type_1_id == Arguments[0]) || (monster.type_2_id == Arguments[0]) || (monster.type_3_id == Arguments[1]))
                    {
                        res.atk_mults.Add(((double)Arguments[1]/100));
                        hp_mult = hp_mult * Arguments[1];
                        rcv_mult = rcv_mult * Arguments[1];
                    }
                    break;
                case SKILL_TYPES.ls_combo:
                    //combo_req,mult
                    if (numCombos >= Arguments[0]) res.atk_mults.Add(((double)Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_HP_and_RCV_color:
                    //color,mult
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]))
                    {
                        hp_mult = hp_mult * Arguments[1];
                        rcv_mult = rcv_mult * Arguments[1];
                    }
                    break;
                case SKILL_TYPES.ls_ATK_color_and_type:
                    //color,type,mult
                        if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]) ||
                            (monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1]))
                            res.atk_mults.Add(((double) Arguments[2]/100));
                        break;
                case SKILL_TYPES.ls_ATK_and_HP_color_and_type:
                    //color,type,mult
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]) ||
                        (monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1]))
                    {
                        res.atk_mults.Add(((double) Arguments[2]/100));
                        hp_mult = hp_mult * Arguments[2];
                    }
                    break;
                case SKILL_TYPES.ls_ATK_and_RCV_color_and_type:
                    //color,type,mult
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]) ||
                        (monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1]))
                    {
                        res.atk_mults.Add(((double) Arguments[2]/100));
                        rcv_mult = rcv_mult * Arguments[2];
                    }
                    break;
                case SKILL_TYPES.ls_All_stats_color_and_type:
                    //color,type,mult
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]) ||
                        (monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1]))
                    {
                        res.atk_mults.Add(((double) Arguments[2]/100));
                        hp_mult = hp_mult * Arguments[2];
                        rcv_mult = rcv_mult * Arguments[2];
                    }
                    break;
                case SKILL_TYPES.ls_ATK_and_HP_double_type:
                    //type, type, mult
                    if ((monster.type_1_id == Arguments[0]) || (monster.type_2_id == Arguments[0]) || (monster.type_3_id == Arguments[0]) ||
                        (monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1])) res.atk_mults.Add(((double) Arguments[2]/100));
                    break;
                case SKILL_TYPES.ls_ATK_and_RCV_double_type:
                    //type,type,mult
                    if ((monster.type_1_id == Arguments[0]) || (monster.type_2_id == Arguments[0]) || (monster.type_3_id == Arguments[0]) ||
                        (monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1]))
                    {
                        res.atk_mults.Add(((double) Arguments[2]/100));
                        rcv_mult = rcv_mult * Arguments[2];
                    }
                    break;
                case SKILL_TYPES.ls_low_hp_color_atk_mult:
                    //hp,  color,1?,0?,mult
                    if(hp_percentage<=Arguments[0])
                    {
                        if ((monster.attr_id == Arguments[1]) || (monster.sub_attr_id == Arguments[1])) res.atk_mults.Add(((double) Arguments[4]/100));
                    }
                    break;
                case SKILL_TYPES.ls_low_hp_type_atk_mult:
                    //hp, type,1?,0?,mult
                    if (hp_percentage <= Arguments[0])
                    {
                        if ((monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1])) res.atk_mults.Add(((double) Arguments[4]/100));
                    }
                    break;
                case SKILL_TYPES.ls_high_hp_color_atk_mult:
                    //hp,  color,1?,0?,mult
                    if (hp_percentage >= Arguments[0])
                    {
                        if ((monster.attr_id == Arguments[1]) || (monster.sub_attr_id == Arguments[1])) res.atk_mults.Add(((double) Arguments[4]/100));
                    }
                    break;
                case SKILL_TYPES.ls_high_hp_type_atk_mult:
                    //hp,  type,1?,0?,mult
                    if (hp_percentage >= Arguments[0])
                    {
                        if ((monster.type_1_id == Arguments[1]) || (monster.type_2_id == Arguments[1]) || (monster.type_3_id == Arguments[1])) res.atk_mults.Add(((double) Arguments[4]/100));
                    }
                    break;
                case SKILL_TYPES.ls_scaling_combo:
                    //start combo, start mult, scale, max combo
                    if(numCombos >= Arguments[0])
                    {
                        int total_mult = Arguments[1];
                        if (numCombos > Arguments[3]) total_mult = total_mult + (Arguments[2] * (Arguments[3] - Arguments[0]));
                        else total_mult = total_mult + (Arguments[2] * (numCombos - Arguments[0]));
                        res.atk_mults.Add(((double) total_mult/100));
                    }
                    break;
                case SKILL_TYPES.ls_skill_use:
                    if (skill_used)
                    {
                        if ((Arguments[0] == 1) || (Arguments[1] == 1)) res.atk_mults.Add(((double) Arguments[2]/100));
                        if ((Arguments[0] == 2) || (Arguments[1] == 2)) rcv_mult = rcv_mult * Arguments[2];
                    }
                    break;
                case SKILL_TYPES.ls_exact_combo:
                    if (numCombos == Arguments[0]) res.atk_mults.Add(((double) Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_double_att_combo:
                    //combo,stat,stat,mult
                    if(numCombos >= Arguments[0])
                    {
                        if ((Arguments[0] == 1) || (Arguments[1] == 1)) res.atk_mults.Add(((double) Arguments[2]/100));
                        if ((Arguments[0] == 2) || (Arguments[1] == 2)) rcv_mult = rcv_mult * Arguments[2];
                    }
                    break;
                case SKILL_TYPES.ls_color_atk_combo:
                    //combo,colors,1?,0?,mult
                    if(numCombos >= Arguments[0])
                    {
                        if ((monster.attr_id == Arguments[1]) || (monster.sub_attr_id == Arguments[1])) res.atk_mults.Add(((double) Arguments[4]/100));
                    }
                    break;
                case SKILL_TYPES.ls_lower_rcv_increase_atk:
                    //rcv reduction%, atk_mult
                    res.atk_mults.Add(((double) Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_lower_max_hp_increases_atk:
                    //hp%, atk
                    res.atk_mults.Add(((double) Arguments[1]/100));
                    break;
                case SKILL_TYPES.ls_lower_max_hp:
                    //hp%
                    break;
                case SKILL_TYPES.ls_lower_max_hp_and_type_atk:
                    //hp%, type, mult
                    if((monster.type_1_id==Arguments[1])||(monster.type_2_id==Arguments[1])||(monster.type_3_id==Arguments[1])) res.atk_mults.Add(((double) Arguments[2]/100));
                    break;
                case SKILL_TYPES.ls_linked_orbs_atk:
                    //color, num_orbs, mult
                    {
                        int max_link = 0;
                        for (int i = 0; i < Combos[Arguments[0]].Count; i++)
                        {
                            if (Combos[Arguments[0]][i] > max_link) max_link = Combos[Arguments[0]][i];
                        }
                        if (max_link >= Arguments[1]) res.atk_mults.Add(((double) Arguments[2]/100));
                    }
                    break;
                case SKILL_TYPES.ls_HP_and_ATK_double_color:
                    //color, color, mult
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]) || (monster.attr_id == Arguments[1]) || (monster.sub_attr_id == Arguments[1])) res.atk_mults.Add(((double) Arguments[2]/100));
                    break;
                case SKILL_TYPES.ls_all_stats_double_color:
                    //color, color, mult
                    if ((monster.attr_id == Arguments[0]) || (monster.sub_attr_id == Arguments[0]) || (monster.attr_id == Arguments[1]) || (monster.sub_attr_id == Arguments[1]))
                    {
                        res.atk_mults.Add(((double) Arguments[2]/100));
                        hp_mult = hp_mult * Arguments[2];
                        rcv_mult = rcv_mult * Arguments[2];
                    }
                    break;
                case SKILL_TYPES.ls_scaling_linked_orb:
                    //mult-color, start_orbs, mult, scale, max-orbs
                    {
                        int max_link = 0;
                        List<int> ColorsToCheck = new List<int>();
                        if ((Arguments[0] & (int)MULTI_COLOR.FIRE) == (int)MULTI_COLOR.FIRE) ColorsToCheck.Add((int)SINGLE_COLOR.FIRE);
                        if ((Arguments[0] & (int)MULTI_COLOR.WOOD) == (int)MULTI_COLOR.WOOD) ColorsToCheck.Add((int)SINGLE_COLOR.WOOD);
                        if ((Arguments[0] & (int)MULTI_COLOR.WATER) == (int)MULTI_COLOR.WATER) ColorsToCheck.Add((int)SINGLE_COLOR.WATER);
                        if ((Arguments[0] & (int)MULTI_COLOR.LIGHT) == (int)MULTI_COLOR.LIGHT) ColorsToCheck.Add((int)SINGLE_COLOR.LIGHT);
                        if ((Arguments[0] & (int)MULTI_COLOR.DARK) == (int)MULTI_COLOR.DARK) ColorsToCheck.Add((int)SINGLE_COLOR.DARK);
                        foreach (int item in ColorsToCheck)
                        {
                            for (int i = 0; i < Combos[item].Count; i++)
                            {
                                if (Combos[item][i] > max_link) max_link = Combos[item][i];
                            }
                        }
                        if (max_link >= Arguments[0])
                        {
                            int total_mult = Arguments[2];
                            int scale_mult = 0;
                            if (max_link >= Arguments[4]) scale_mult = Arguments[4] - Arguments[0];
                            else scale_mult = max_link - Arguments[0];
                            total_mult = total_mult + (scale_mult * Arguments[3]);
                            res.atk_mults.Add(((double) total_mult/100));
                        }
                    }
                    break;
                case SKILL_TYPES.ls_mult_stat:
                    //color, type, hpt, atk, rcv
                    {
                        List<int> ColorsToCheck = new List<int>();
                        List<int> TypesToCheck = new List<int>();
                        bool applicable = false;
                        if ((Arguments[0] & (int)MULTI_COLOR.FIRE) == (int)MULTI_COLOR.FIRE) ColorsToCheck.Add((int)SINGLE_COLOR.FIRE);
                        if ((Arguments[0] & (int)MULTI_COLOR.WOOD) == (int)MULTI_COLOR.WOOD) ColorsToCheck.Add((int)SINGLE_COLOR.WOOD);
                        if ((Arguments[0] & (int)MULTI_COLOR.WATER) == (int)MULTI_COLOR.WATER) ColorsToCheck.Add((int)SINGLE_COLOR.WATER);
                        if ((Arguments[0] & (int)MULTI_COLOR.LIGHT) == (int)MULTI_COLOR.LIGHT) ColorsToCheck.Add((int)SINGLE_COLOR.LIGHT);
                        if ((Arguments[0] & (int)MULTI_COLOR.DARK) == (int)MULTI_COLOR.DARK) ColorsToCheck.Add((int)SINGLE_COLOR.DARK);
                        if ((Arguments[1] & (int)MULTI_TYPE.ATTACKER) == (int)MULTI_TYPE.ATTACKER) TypesToCheck.Add((int)SINGLE_TYPE.ATTACKER);
                        if ((Arguments[1] & (int)MULTI_TYPE.AWAKENING) == (int)MULTI_TYPE.ATTACKER) TypesToCheck.Add((int)SINGLE_TYPE.ATTACKER);
                        if ((Arguments[1] & (int)MULTI_TYPE.BALANCE) == (int)MULTI_TYPE.BALANCE) TypesToCheck.Add((int)SINGLE_TYPE.BALANCE);
                        if ((Arguments[1] & (int)MULTI_TYPE.DEVIL) == (int)MULTI_TYPE.DEVIL) TypesToCheck.Add((int)SINGLE_TYPE.DEVIL);
                        if ((Arguments[1] & (int)MULTI_TYPE.DRAGON) == (int)MULTI_TYPE.DRAGON) TypesToCheck.Add((int)SINGLE_TYPE.DRAGON);
                        if ((Arguments[1] & (int)MULTI_TYPE.ENHANCE) == (int)MULTI_TYPE.ENHANCE) TypesToCheck.Add((int)SINGLE_TYPE.ENHANCE);
                        if ((Arguments[1] & (int)MULTI_TYPE.EVO) == (int)MULTI_TYPE.EVO) TypesToCheck.Add((int)SINGLE_TYPE.EVO);
                        if ((Arguments[1] & (int)MULTI_TYPE.GOD) == (int)MULTI_TYPE.GOD) TypesToCheck.Add((int)SINGLE_TYPE.GOD);
                        if ((Arguments[1] & (int)MULTI_TYPE.HEALER) == (int)MULTI_TYPE.HEALER) TypesToCheck.Add((int)SINGLE_TYPE.HEALER);
                        if ((Arguments[1] & (int)MULTI_TYPE.MACHINE) == (int)MULTI_TYPE.MACHINE) TypesToCheck.Add((int)SINGLE_TYPE.MACHINE);
                        if ((Arguments[1] & (int)MULTI_TYPE.PHYSICAL) == (int)MULTI_TYPE.PHYSICAL) TypesToCheck.Add((int)SINGLE_TYPE.PHYSICAL);
                        if ((Arguments[1] & (int)MULTI_TYPE.REDEEMABLE) == (int)MULTI_TYPE.REDEEMABLE) TypesToCheck.Add((int)SINGLE_TYPE.REDEEMABLE);
                        foreach (int item in ColorsToCheck)
                        {
                            if ((monster.attr_id == ColorsToCheck[item]) || (monster.sub_attr_id == ColorsToCheck[item])) applicable = true;
                        }
                        foreach (int item in TypesToCheck)
                        {
                            if ((monster.type_1_id == TypesToCheck[item]) || (monster.type_2_id == TypesToCheck[item]) || (monster.type_3_id == TypesToCheck[item])) applicable = true;
                        }
                        if(applicable)
                        {
                            if (Arguments[2] > 0) hp_mult = hp_mult * Arguments[2];
                            if (Arguments[3] > 0) res.atk_mults.Add(((double) Arguments[3]/100));
                            if (Arguments[4] > 0) rcv_mult = rcv_mult * Arguments[4];
                        }
                    }
                    break;
                case SKILL_TYPES.ls_low_hp_atk:
                    //percent, color, type, mult
                    if(hp_percentage<=Arguments[0])
                    {
                        List<int> ColorsToCheck = new List<int>();
                        List<int> TypesToCheck = new List<int>();
                        bool applicable = false;
                        if ((Arguments[0] & (int)MULTI_COLOR.FIRE) == (int)MULTI_COLOR.FIRE) ColorsToCheck.Add((int)SINGLE_COLOR.FIRE);
                        if ((Arguments[0] & (int)MULTI_COLOR.WOOD) == (int)MULTI_COLOR.WOOD) ColorsToCheck.Add((int)SINGLE_COLOR.WOOD);
                        if ((Arguments[0] & (int)MULTI_COLOR.WATER) == (int)MULTI_COLOR.WATER) ColorsToCheck.Add((int)SINGLE_COLOR.WATER);
                        if ((Arguments[0] & (int)MULTI_COLOR.LIGHT) == (int)MULTI_COLOR.LIGHT) ColorsToCheck.Add((int)SINGLE_COLOR.LIGHT);
                        if ((Arguments[0] & (int)MULTI_COLOR.DARK) == (int)MULTI_COLOR.DARK) ColorsToCheck.Add((int)SINGLE_COLOR.DARK);
                        if ((Arguments[1] & (int)MULTI_TYPE.ATTACKER) == (int)MULTI_TYPE.ATTACKER) TypesToCheck.Add((int)SINGLE_TYPE.ATTACKER);
                        if ((Arguments[1] & (int)MULTI_TYPE.AWAKENING) == (int)MULTI_TYPE.ATTACKER) TypesToCheck.Add((int)SINGLE_TYPE.ATTACKER);
                        if ((Arguments[1] & (int)MULTI_TYPE.BALANCE) == (int)MULTI_TYPE.BALANCE) TypesToCheck.Add((int)SINGLE_TYPE.BALANCE);
                        if ((Arguments[1] & (int)MULTI_TYPE.DEVIL) == (int)MULTI_TYPE.DEVIL) TypesToCheck.Add((int)SINGLE_TYPE.DEVIL);
                        if ((Arguments[1] & (int)MULTI_TYPE.DRAGON) == (int)MULTI_TYPE.DRAGON) TypesToCheck.Add((int)SINGLE_TYPE.DRAGON);
                        if ((Arguments[1] & (int)MULTI_TYPE.ENHANCE) == (int)MULTI_TYPE.ENHANCE) TypesToCheck.Add((int)SINGLE_TYPE.ENHANCE);
                        if ((Arguments[1] & (int)MULTI_TYPE.EVO) == (int)MULTI_TYPE.EVO) TypesToCheck.Add((int)SINGLE_TYPE.EVO);
                        if ((Arguments[1] & (int)MULTI_TYPE.GOD) == (int)MULTI_TYPE.GOD) TypesToCheck.Add((int)SINGLE_TYPE.GOD);
                        if ((Arguments[1] & (int)MULTI_TYPE.HEALER) == (int)MULTI_TYPE.HEALER) TypesToCheck.Add((int)SINGLE_TYPE.HEALER);
                        if ((Arguments[1] & (int)MULTI_TYPE.MACHINE) == (int)MULTI_TYPE.MACHINE) TypesToCheck.Add((int)SINGLE_TYPE.MACHINE);
                        if ((Arguments[1] & (int)MULTI_TYPE.PHYSICAL) == (int)MULTI_TYPE.PHYSICAL) TypesToCheck.Add((int)SINGLE_TYPE.PHYSICAL);
                        if ((Arguments[1] & (int)MULTI_TYPE.REDEEMABLE) == (int)MULTI_TYPE.REDEEMABLE) TypesToCheck.Add((int)SINGLE_TYPE.REDEEMABLE);
                        foreach (int item in ColorsToCheck)
                        {
                            if ((monster.attr_id == ColorsToCheck[item]) || (monster.sub_attr_id == ColorsToCheck[item])) applicable = true;
                        }
                        foreach (int item in TypesToCheck)
                        {
                            if ((monster.type_1_id == TypesToCheck[item]) || (monster.type_2_id == TypesToCheck[item]) || (monster.type_3_id == TypesToCheck[item])) applicable = true;
                        }
                        if (applicable)
                        {
                            res.atk_mults.Add(((double) Arguments[3]/100));
                        }
                    }
                    break;
                case SKILL_TYPES.ls_high_hp_mult_stat:
                    //percent, color, type, atk, rcv
                    if (hp_percentage >= Arguments[0])
                    {
                        List<int> ColorsToCheck = new List<int>();
                        List<int> TypesToCheck = new List<int>();
                        bool applicable = false;
                        if ((Arguments[0] & (int)MULTI_COLOR.FIRE) == (int)MULTI_COLOR.FIRE) ColorsToCheck.Add((int)SINGLE_COLOR.FIRE);
                        if ((Arguments[0] & (int)MULTI_COLOR.WOOD) == (int)MULTI_COLOR.WOOD) ColorsToCheck.Add((int)SINGLE_COLOR.WOOD);
                        if ((Arguments[0] & (int)MULTI_COLOR.WATER) == (int)MULTI_COLOR.WATER) ColorsToCheck.Add((int)SINGLE_COLOR.WATER);
                        if ((Arguments[0] & (int)MULTI_COLOR.LIGHT) == (int)MULTI_COLOR.LIGHT) ColorsToCheck.Add((int)SINGLE_COLOR.LIGHT);
                        if ((Arguments[0] & (int)MULTI_COLOR.DARK) == (int)MULTI_COLOR.DARK) ColorsToCheck.Add((int)SINGLE_COLOR.DARK);
                        if ((Arguments[1] & (int)MULTI_TYPE.ATTACKER) == (int)MULTI_TYPE.ATTACKER) TypesToCheck.Add((int)SINGLE_TYPE.ATTACKER);
                        if ((Arguments[1] & (int)MULTI_TYPE.AWAKENING) == (int)MULTI_TYPE.ATTACKER) TypesToCheck.Add((int)SINGLE_TYPE.ATTACKER);
                        if ((Arguments[1] & (int)MULTI_TYPE.BALANCE) == (int)MULTI_TYPE.BALANCE) TypesToCheck.Add((int)SINGLE_TYPE.BALANCE);
                        if ((Arguments[1] & (int)MULTI_TYPE.DEVIL) == (int)MULTI_TYPE.DEVIL) TypesToCheck.Add((int)SINGLE_TYPE.DEVIL);
                        if ((Arguments[1] & (int)MULTI_TYPE.DRAGON) == (int)MULTI_TYPE.DRAGON) TypesToCheck.Add((int)SINGLE_TYPE.DRAGON);
                        if ((Arguments[1] & (int)MULTI_TYPE.ENHANCE) == (int)MULTI_TYPE.ENHANCE) TypesToCheck.Add((int)SINGLE_TYPE.ENHANCE);
                        if ((Arguments[1] & (int)MULTI_TYPE.EVO) == (int)MULTI_TYPE.EVO) TypesToCheck.Add((int)SINGLE_TYPE.EVO);
                        if ((Arguments[1] & (int)MULTI_TYPE.GOD) == (int)MULTI_TYPE.GOD) TypesToCheck.Add((int)SINGLE_TYPE.GOD);
                        if ((Arguments[1] & (int)MULTI_TYPE.HEALER) == (int)MULTI_TYPE.HEALER) TypesToCheck.Add((int)SINGLE_TYPE.HEALER);
                        if ((Arguments[1] & (int)MULTI_TYPE.MACHINE) == (int)MULTI_TYPE.MACHINE) TypesToCheck.Add((int)SINGLE_TYPE.MACHINE);
                        if ((Arguments[1] & (int)MULTI_TYPE.PHYSICAL) == (int)MULTI_TYPE.PHYSICAL) TypesToCheck.Add((int)SINGLE_TYPE.PHYSICAL);
                        if ((Arguments[1] & (int)MULTI_TYPE.REDEEMABLE) == (int)MULTI_TYPE.REDEEMABLE) TypesToCheck.Add((int)SINGLE_TYPE.REDEEMABLE);
                        foreach (int item in ColorsToCheck)
                        {
                            if ((monster.attr_id == ColorsToCheck[item]) || (monster.sub_attr_id == ColorsToCheck[item])) applicable = true;
                        }
                        foreach (int item in TypesToCheck)
                        {
                            if ((monster.type_1_id == TypesToCheck[item]) || (monster.type_2_id == TypesToCheck[item]) || (monster.type_3_id == TypesToCheck[item])) applicable = true;
                        }
                        if (applicable)
                        {
                            if (Arguments[3] > 0) res.atk_mults.Add(((double) Arguments[3]/100));
                            if (Arguments[4] > 0) rcv_mult = rcv_mult * Arguments[4];
                        }
                    }
                    break;
                case SKILL_TYPES.ls_multiple_specific_combos:
                    //TODO: color,color,color,color,color,min_required, base mult, scale mult
                    {

                    }
                    break;
                case SKILL_TYPES.ls_required_monster_on_team:
                    //TODO: Probably need to pass entire team to check
                    break;
                case SKILL_TYPES.ls_new_stat:
                    //color,type,hp,atk,rcv
                    break;
                case SKILL_TYPES.ls_low_hp_stat:
                    //hp,color,type,atk,rcv
                    break;
                case SKILL_TYPES.ls_hight_hp_stat:
                    //hp,color,type,atc,rcv,, colorshield, percent
                    break;
                case SKILL_TYPES.ls_skilluse:
                    //color,type,atk,rcv
                    break;
                case SKILL_TYPES.ls_split_color_ls:
                    //colora, hp, atk, rcv, colorb, hp, atk, rcv
                    break;
                case SKILL_TYPES.ls_split_type_ls:
                    //typea, hp, atk, rcv, typeb, hp, atk, rcv
                    break;
                case SKILL_TYPES.ls_multi_part_skill:
                    {
                        for(int i = 0; i < Arguments.Count; i++)
                        {
                            if (Arguments[i] != 0)
                            {
                                Skill dummy_skill = new Skill(_json_path, Arguments[i]);
                                LS_Results dummy_res = new LS_Results();
                                dummy_res = dummy_skill.GetLeaderskillMultiplier(Combos, OrbEnhances, hp_percentage, skill_used, monster);
                                foreach (double item in dummy_res.atk_mults)
                                {
                                    res.atk_mults.Add(item);
                                }
                            }
                        }
                    }
                    break;
                case SKILL_TYPES.ls_hp_scaling:
                    break;
                case SKILL_TYPES.ls_increased_xp:
                    break;
                case SKILL_TYPES.ls_rcv_increased_with_4_hearts:
                    break;
                case SKILL_TYPES.ls_atk_with_5o1e:
                    break;
                case SKILL_TYPES.ls_heart_cross:
                    break;
                case SKILL_TYPES.ls_multiplayer:
                    break;
                case SKILL_TYPES.ls_cross_mult:
                    break;
                case SKILL_TYPES.ls_min_orbs_match:
                    {
                        List<int> ColorsToCheck = new List<int>();
                        List<int> TypesToCheck = new List<int>();
                        bool applicable = false;
                        if ((Arguments[1] & (int)MULTI_COLOR.FIRE) == (int)MULTI_COLOR.FIRE) ColorsToCheck.Add((int)SINGLE_COLOR.FIRE);
                        if ((Arguments[1] & (int)MULTI_COLOR.WOOD) == (int)MULTI_COLOR.WOOD) ColorsToCheck.Add((int)SINGLE_COLOR.WOOD);
                        if ((Arguments[1] & (int)MULTI_COLOR.WATER) == (int)MULTI_COLOR.WATER) ColorsToCheck.Add((int)SINGLE_COLOR.WATER);
                        if ((Arguments[1] & (int)MULTI_COLOR.LIGHT) == (int)MULTI_COLOR.LIGHT) ColorsToCheck.Add((int)SINGLE_COLOR.LIGHT);
                        if ((Arguments[1] & (int)MULTI_COLOR.DARK) == (int)MULTI_COLOR.DARK) ColorsToCheck.Add((int)SINGLE_COLOR.DARK);
                        if ((Arguments[2] & (int)MULTI_TYPE.ATTACKER) == (int)MULTI_TYPE.ATTACKER) TypesToCheck.Add((int)SINGLE_TYPE.ATTACKER);
                        if ((Arguments[2] & (int)MULTI_TYPE.AWAKENING) == (int)MULTI_TYPE.ATTACKER) TypesToCheck.Add((int)SINGLE_TYPE.ATTACKER);
                        if ((Arguments[2] & (int)MULTI_TYPE.BALANCE) == (int)MULTI_TYPE.BALANCE) TypesToCheck.Add((int)SINGLE_TYPE.BALANCE);
                        if ((Arguments[2] & (int)MULTI_TYPE.DEVIL) == (int)MULTI_TYPE.DEVIL) TypesToCheck.Add((int)SINGLE_TYPE.DEVIL);
                        if ((Arguments[2] & (int)MULTI_TYPE.DRAGON) == (int)MULTI_TYPE.DRAGON) TypesToCheck.Add((int)SINGLE_TYPE.DRAGON);
                        if ((Arguments[2] & (int)MULTI_TYPE.ENHANCE) == (int)MULTI_TYPE.ENHANCE) TypesToCheck.Add((int)SINGLE_TYPE.ENHANCE);
                        if ((Arguments[2] & (int)MULTI_TYPE.EVO) == (int)MULTI_TYPE.EVO) TypesToCheck.Add((int)SINGLE_TYPE.EVO);
                        if ((Arguments[2] & (int)MULTI_TYPE.GOD) == (int)MULTI_TYPE.GOD) TypesToCheck.Add((int)SINGLE_TYPE.GOD);
                        if ((Arguments[2] & (int)MULTI_TYPE.HEALER) == (int)MULTI_TYPE.HEALER) TypesToCheck.Add((int)SINGLE_TYPE.HEALER);
                        if ((Arguments[2] & (int)MULTI_TYPE.MACHINE) == (int)MULTI_TYPE.MACHINE) TypesToCheck.Add((int)SINGLE_TYPE.MACHINE);
                        if ((Arguments[2] & (int)MULTI_TYPE.PHYSICAL) == (int)MULTI_TYPE.PHYSICAL) TypesToCheck.Add((int)SINGLE_TYPE.PHYSICAL);
                        if ((Arguments[2] & (int)MULTI_TYPE.REDEEMABLE) == (int)MULTI_TYPE.REDEEMABLE) TypesToCheck.Add((int)SINGLE_TYPE.REDEEMABLE);
                        foreach (int item in ColorsToCheck)
                        {
                            if ((monster.attr_id == ColorsToCheck[item]) || (monster.sub_attr_id == ColorsToCheck[item])) applicable = true;
                        }
                        foreach (int item in TypesToCheck)
                        {
                            if ((monster.type_1_id == TypesToCheck[item]) || (monster.type_2_id == TypesToCheck[item]) || (monster.type_3_id == TypesToCheck[item])) applicable = true;
                        }
                        if (applicable)
                        {
                            if (Arguments[3] > 0) hp_mult = hp_mult * Arguments[3];
                            if (Arguments[4] > 0) res.atk_mults.Add(((double) Arguments[4]/100));
                            if (Arguments[5] > 0) rcv_mult = rcv_mult * Arguments[5];
                        }
                    }
                    break;
                case SKILL_TYPES.ls_scaling_linked_match:
                    break;
                case SKILL_TYPES.ls_7x6_board:
                    break;
                case SKILL_TYPES.ls_no_skyfall:
                    break;
                case SKILL_TYPES.ls_multiple_color_activation:
                    break;
                case SKILL_TYPES.ls_mult_att_match:
                    break;
                case SKILL_TYPES.ls_new_combo:
                    break;
                case SKILL_TYPES.ls_linked_orbs:
                    break;
                case SKILL_TYPES.ls_combo_and_shield:
                    break;
                case SKILL_TYPES.ls_multiple_color_activation_and_shield:
                    break;
                case SKILL_TYPES.ls_new_multiple_color_activation:
                    break;
                case SKILL_TYPES.ls_all_subs_of_a_type:
                    break;
                case SKILL_TYPES.ls_orbs_left_on_board:
                    //?,?,?,?,?,min_activation,mult,scale
                    {
                        int num_orbs_matched = 0;
                        int board_size = 30;
                        for (int i = 0; i < Combos.Count; i++)
                        {
                            for (int j = 0; j < Combos[i].Count; j++)
                            {
                                num_orbs_matched += Combos[i][j];
                            }
                        }
                        if((board_size - num_orbs_matched) <= Arguments[5])
                        {
                            int total_mult = Arguments[6];
                            total_mult = total_mult + (Arguments[7] * (board_size - num_orbs_matched));
                            res.atk_mults.Add(((double) total_mult/100));
                        }
                    }
                    break;
                case SKILL_TYPES.ls_fixed_move_time:
                    break;
                case SKILL_TYPES.ls_linked_orb_with_shield:
                    break;
                case SKILL_TYPES.ls_high_hp_gives_shield:
                    break;
                case SKILL_TYPES.ls_movetime_with_mult:
                    break;
                case SKILL_TYPES.ls_7x6_with_mult:
                    break;
            }
            return res;
        }
        public void load_skill(string skill_json_path, int Skill_id)
        {
            _json_path = skill_json_path;
            JsonTextReader reader = new JsonTextReader(new StreamReader(_json_path));
            while (reader.Read())
            {
                int cur_skill = 0;
                int converted_int = 0;
                switch (reader.TokenType)
                {
                    case JsonToken.StartObject:
                        break;
                    case JsonToken.PropertyName:
                        string property_name = reader.Value.ToString();
                        reader.Read();
                        if(reader.ValueType==typeof(System.Int64)) int.TryParse(reader.Value.ToString(), out converted_int);
                        switch (property_name)
                        {
                            case "clean_description":
                                skill_description = reader.Value.ToString();
                                break;
                            case "skill_type":
                                skill_type = (SKILL_TYPES)converted_int;
                                break;
                            case "turn_max":
                                break;
                            case "turn_min":
                                break;
                            case "skill_id":
                                skill_id = converted_int;
                                break;
                            case "other_fields":
                                Arguments[0] = 0;
                                Arguments[1] = 0;
                                Arguments[2] = 0;
                                Arguments[3] = 0;
                                Arguments[4] = 0;
                                Arguments[5] = 0;
                                Arguments[6] = 0;
                                Arguments[7] = 0;
                                Arguments[8] = 0;
                                Arguments[9] = 0;
                                if (reader.TokenType == JsonToken.StartArray)
                                {
                                    reader.Read();
                                    int arg_num = 0;
                                    while (reader.TokenType != JsonToken.EndArray)
                                    {
                                        if (reader.Value != null)
                                        {
                                            //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                                            if (reader.TokenType == JsonToken.Integer)
                                            {
                                                int.TryParse(reader.Value.ToString(), out converted_int);
                                                Arguments[arg_num++] = converted_int;
                                            }
                                        }
                                        reader.Read();
                                    }
                                }
                                break;
                        }
                        break;
                    case JsonToken.EndObject:
                        if (skill_id == Skill_id) return;
                        break;
                }
            }
        }
        public Skill(string skill_json_path, int skill_id)
        {
            Arguments = new List<int>();
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            Arguments.Add(0);
            load_skill(skill_json_path, skill_id);
        }
    }
}
