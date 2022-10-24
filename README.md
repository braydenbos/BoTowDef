# Bo TowerDefense
``` mermaid
    graph TD;
        A[TDShop]
            A -->|Transform|D(Shelf)
                D-->|Float|E(Timer)
                D-->|Prefab|F(Items)
                        F -->|Class|G(Stats)
                            G-->|give|H([Tower])
                        F-->|place|H(Tower)
                        F -->|Int|I(Cost)
                            I-->|less|J{Buy}
        B[Enemy]
            B -->|Int|K(Gold/Coins)
                K-->|increas|L(PlayerMoney)
                    L-->|More|J
                        J-->|new|H
        C[Player]-->|Int|L
```