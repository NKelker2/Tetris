using System;
using UnityEngine;

public abstract class Effects {

    protected int count;
    protected String debuffType;
    protected String timeApplied;

    /* to be implemented later when working on shop/inventory
    texture
    private String description;
    */

    public Effects(String debuffType, String timeApplied) {
        count = 1;
        this.debuffType = debuffType;
        this.timeApplied = timeApplied;
    }

    public void CountUp() {
        count++;
    }
    public abstract double ApplyEffect();
}

public class RedCombo : Effects {
    private int combo;
    public RedCombo(String debuffTypeS, String timeAppliedS) : base(debuffTypeS, timeAppliedS) {
        combo = 0;
    }

    public override double ApplyEffect() {
        combo++;
        return 1;
    }

    public void reset() {
        combo = 0;
    }
}