using System;

public abstract class Effect {

    protected int count;
    public String type;
    protected String time;
    protected int combo;

    /* to be implemented later when working on shop/inventory
    texture
    private String description;
    */

    public Effect(String type, String time) {
        count = 1;
        this.type = type;
        this.time = time;
    }

    public void CountUp() {
        count++;
    }
    public abstract double ApplyEffect(int val);
    public abstract void Reset();
}

public class RedCombo : Effect {
    public RedCombo() : base("red", "onClear") {
        combo = 0;
    }

    public override double ApplyEffect(int val) {
        combo++;
        Log.PrintToGame("RedCombo gave " + count * (val * (combo - 1)) + " extra points");
        return count * (val * (combo - 1));
    }

    public override void Reset() {
        combo = 0;
    }
}