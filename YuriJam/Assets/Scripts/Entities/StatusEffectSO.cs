using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "YuriJam/StatusEffectSO")]
public class StatusEffectSO : ScriptableObject
{
    public StatusEffects type;
    public int power;
    public float duration;

    public StatusEffect CreateEffect()
    {
        StatusEffect effect = null;

        switch (type)
        {
            case StatusEffects.Burn:
                effect = new BurnSE(power, duration);
                break;

            case StatusEffects.Harvest:
                effect = new HarvestSE(power, duration);
                break;

            case StatusEffects.Shatter:
                effect = new ShatterSE(power, duration);
                break;
        }

        return effect;
    }
}
