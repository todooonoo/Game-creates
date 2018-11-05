using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public SkillType skillType;
    protected Skill skill;
    
    public void InitSkill()
    {
        skill = SaveManager.Instance.GetSkill(skillType);
    }

    public void AddExp(int amount)
    {
        if (amount <= 0)
            return;

        skill.exp += amount;
        CheckLevelUp();
    }

    // Checks if level will level up
    public void CheckLevelUp()
    {
        while(skill.exp >= skill.nextLevelExp)
        {
            // Rest experience
            skill.exp -= skill.nextLevelExp;

            // Level skill up
            LevelUp();
        }
    }

    // Levels up the skill
    protected virtual void LevelUp()
    {
        skill.level += 1;
    }
}
