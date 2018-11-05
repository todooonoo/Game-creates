using UnityEngine;
using System;

[Serializable]
public class Skill
{
    public SkillType skillType;
    public int level, exp, nextLevelExp;

    public Skill(SkillType skillType)
    {
        this.skillType = skillType;
    }
}
