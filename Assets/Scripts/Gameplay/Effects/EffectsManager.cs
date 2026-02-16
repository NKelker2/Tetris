using System.Collections.Generic;
using UnityEngine;

public static class EffectsManager {
    public static void AddOnClearEffect(Effect effect) {
        // checks if the item type already has a list to add to, otherwise create one
        if (!PlayerData.onClearEffects.ContainsKey(effect.type))
            PlayerData.onClearEffects.Add(effect.type, new List<Effect>());

        // checks if there already exist an effect of the object type to increase the count of to save space, otherwise create one
        for (int i = 0; i < PlayerData.onClearEffects[effect.type].Count; i++) {
            if (PlayerData.onClearEffects[effect.type][i].GetType() == effect.GetType()) {
                PlayerData.onClearEffects[effect.type][i].CountUp();
                return;
            }
        }

        PlayerData.onClearEffects[effect.type].Add(effect);
    }
}
