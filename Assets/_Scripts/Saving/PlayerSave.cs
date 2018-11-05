using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSave
{
    public InventorySave inventory;
    public List<Skill> skills;      // Mining, alchemy, sword etc...

    public PlayerSave()
    {
        inventory = new InventorySave();
        skills = new List<Skill>();
    }

    public Skill GetSkill(SkillType skillType)
    {
        // Search for skill with specified type
        for(int i = 0; i < skills.Count; i++)
        {
            if (skills[i].skillType == skillType)
                return skills[i];
        }

        // No skill found, create default skill
        var newSkill = new Skill(skillType);
        skills.Add(newSkill);
        return newSkill;
    }
}
