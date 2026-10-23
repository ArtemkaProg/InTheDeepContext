# InTheDeep --- World Foundation

**Status:** Foundational concept, early design\
**Last updated:** 2026-10-23\
**Purpose:** Preserve the project's core world-design decisions and
their reasoning.

## 1. Core vision

InTheDeep takes place in a **large, connected ocean with multiple
geological regions and natural transitions between them**. The world
should feel like one coherent physical environment, not a collection of
disconnected levels wearing different biome skins.

The game aims for **high realism and strong grounding in nature**.
Realism is not a separate mode from visual appeal: scientific
plausibility is the baseline, while selective artistic emphasis may
strengthen scale, atmosphere, readability, and mystery without making
the world feel arbitrary or cartoonishly divided.

Exploration is a central motivation. Players should want to investigate
unfamiliar terrain, discover unusual natural formations, and understand
places that initially seem mysterious. The ocean should also contain
**intrinsically dangerous environments**. Danger is not limited to
player mistakes or ordinary consequences such as pressure damage; some
places should be difficult to enter, navigate, or remain in because
several environmental factors combine.

## 2. Decisions

### DECIDED --- Connected ocean

-   The world is one connected oceanic setting with multiple geological
    regions.
-   Regions should connect through plausible transitions in topography,
    geology, depth, water conditions, and habitat.
-   Individual regions may have a strong identity, but should not feel
    like isolated theme-park biomes with hard borders.
-   A region must make sense as part of a broader geological system.

**Reasoning:** The world should support the feeling of travelling
through a real environment and gradually discovering how its different
parts relate.

### DECIDED --- High naturalism, with selective artistic emphasis

-   The world should be highly realistic and closely grounded in nature.
-   These are one combined goal, not two competing options.
-   Artistic exaggeration is allowed selectively: for example, to make a
    cliff's scale legible, a silhouette memorable, or a dangerous area
    readable.
-   Avoid arbitrary colour-coded regions, implausible material changes,
    or spectacular features placed only because a location needs a new
    visual theme.
-   Visual variety should emerge primarily from geology, terrain,
    sediment, light, water movement, and life.

**Reasoning:** Realistic environments can still be visually memorable.
The goal is not to reproduce the dullest possible version of nature, but
to create believable places with distinctive compositions.

### DECIDED --- Discovery and environmental danger

-   Exploration and the discovery of mysterious places are primary
    motivations.
-   Environmental danger is also an important part of exploration.
-   Some places should be difficult because of their combined
    conditions: strong currents, steep or confined terrain, unstable
    debris, poor visibility, limited routes, temperature anomalies, or
    other locally justified hazards.
-   Hazards should have an understandable physical cause and, where
    appropriate, clues the player can observe.
-   The world should not rely only on invisible instant-death rules.
    Natural laws matter, but danger should also create decisions,
    anticipation, and navigation challenges.

**Reasoning:** A plausible world becomes compelling when its physical
properties affect what the player can safely do and when discoveries
require observation and judgement.

## 3. Design principles

1.  **Geology before biome labels.** Start with geological structure and
    terrain. Do not start by assigning a large area a colour, creature
    set, and decorative theme.
2.  **Continuous transitions.** Changes may be gradual, irregular, or
    locally abrupt when a real process explains them. Avoid borders that
    exist only for game-design convenience.
3.  **Layered causes.** A place's appearance should result from multiple
    factors: bedrock, sediment, erosion, currents, depth, light,
    chemistry, and organisms.
4.  **Variety without randomness.** Distinctive locations should result
    from coherent combinations of causes, not arbitrary mixing of
    unrelated assets.
5.  **Mystery through evidence.** Let unusual formations, traces,
    material changes, sounds, silhouettes, or ecological patterns
    suggest that something is worth investigating.
6.  **Danger through context.** A hazardous place should have a physical
    explanation and ideally observable signs, even if the player cannot
    immediately understand them.
7.  **Scientific humility.** When a geological or biological detail is
    uncertain, research it or label it as a deliberate fictional
    extrapolation. Do not present a convenient invention as established
    science.
8.  **Playable selection, not total simulation.** The game does not need
    to simulate every Earth process. It should use simplified rules that
    preserve the right relationships and produce believable results.

## 4. What "biomes" mean in this project

The word *biome* may still be useful informally, but it should not be
the primary world-generation unit.

Instead, describe a location through overlapping categories:

-   **Geological region:** the broad tectonic or geological setting.
-   **Landform:** the local shape of the seafloor.
-   **Surface material:** sediment, exposed rock, rubble, crusts, or
    mixed ground.
-   **Habitat:** organisms and communities supported by local
    conditions.
-   **Water conditions:** depth, light, temperature, currents,
    visibility, and chemistry.
-   **Local feature:** a specific formation or event that makes a place
    worth investigating.
-   **Hazard profile:** the conditions that make movement, observation,
    or survival difficult.

A single place can combine several of these categories. For example, a
basaltic ridge can contain sediment-filled depressions, exposed pillow
lava, a small seep community, and a narrow passage with difficult
currents. It does not need to become four separate biomes.

## 5. Realism and visual identity

Visual identity should come from composition and physical detail as much
as from colour.

Useful sources of variety include:

-   changes in slope, scale, and silhouette;
-   exposed bedrock versus sediment-covered surfaces;
-   grain size, sediment ripples, rock fractures, and debris deposits;
-   the amount and type of biological cover;
-   light falloff, suspended particles, visibility, and the player's
    light source;
-   local water movement and its effects on sediment or navigation;
-   contrasts between open spaces and confined passages;
-   isolated formations that break an otherwise continuous landscape.

**Avoid:** making every region use a unique saturated colour palette,
making the seafloor change material without a geological reason, or
filling every location with conspicuous landmarks.

## 6. Relationship to the three geological layers

The current conceptual cross-section uses three broad layers beneath the
water:

1.  **Sediment layer:** sand, silt, clay, biogenic material, and rock
    fragments. Thickness and composition vary by location.
2.  **Oceanic crust:** predominantly basaltic igneous rocks in many
    oceanic settings, alongside other rock types depending on geological
    history.
3.  **Upper mantle:** hot solid rock that can deform slowly over
    geological timescales. It is not an open cavern or an ordinary
    explorable underground layer.

This is a simplified model for thinking and documentation, not a claim
that every location has identical layer thickness or composition. The
visible seafloor surface is often sediment; in other places, bedrock is
exposed.

## 7. What is not decided yet

The following must remain open until researched or designed:

-   the exact tectonic setting and map layout of the game's ocean;
-   whether the world is based on a specific real ocean or a fictional
    composite;
-   the exact number, size, and names of geological regions;
-   the depth range and traversal capabilities of the player;
-   the final list of rock types, sediments, habitats, and organisms;
-   which hazards are simulated and how they affect gameplay;
-   whether the world is hand-authored, procedurally generated, or a
    hybrid;
-   the degree of geological interaction available to the player;
-   how much of the subsurface is visible or explorable.

Do not treat examples in other documents as a final region list.

## 8. Decision test

When proposing a new location, ask:

1.  What geological process or setting could produce this terrain?
2.  Why does this surface material occur here?
3.  What would currents, sediment, light, and local chemistry do in this
    place?
4.  What organisms could plausibly live here, and why?
5.  What makes this location visually memorable without breaking
    naturalism?
6.  What might the player discover here?
7.  If the location is dangerous, what physical factors create that
    danger?
8.  How does this location transition into the surrounding ocean?

If the only answer is "because this is a new biome," the concept needs
more work.

---

## 4. Принципи світу

### 4.1. Різноманітність

**[Рішення]** Світ не має бути однотипним. Потрібні величезні біоми, які суттєво відрізняються один від одного, щоб гравцю не було нудно.

**[Рішення]** Біоми беруться з реальності, зокрема з реальних геологічних періодів, наприклад кембрію й ордовику, коли життя розвивалося в Панталасовому океані.

**[Рішення]** Істоти беруться з тих епох, зокрема вимерлі. Пояснення в грі: вони існували й на Kepler-22b і, на відміну від Землі, вижили.

### 4.2. Як пояснити виживання (пропозиції, не затверджено)

**[Пропозиція]** Нащадки, а не копії. Якщо лінія жила ще сотні мільйонів років, вона еволюціонувала б. Тому краще, щоб це були нащадки з тим самим планом тіла, а не ідентичні види (Клоп уже сформульований саме так). Це дає свободу змінювати дизайн.

**[Пропозиція]** Причини виживання: на Kepler-22b не було масових вимирань, що вразили ці лінії на Землі (кінець ордовика, пізній девон, кінець пермі), або вони збереглися в ізольованих «сховищах» (відокремлені глибини, особливий хімічний склад води). Земні аналоги: «живі копалини», як-от латимерія, наутилус, мечохвости.

### 4.3. Ери як теми біомів (ідеї, не затверджено)

**[Пропозиція]** Приклади тем:
- **Кембрійський біом:** дивні членистоногі, безліч форм, що не схожі на нічого сучасного. Сюди природно лягають радіодонти (Omnidens, Клоп). Інші відомі кембрійські істоти для натхнення: Anomalocaris, Opabinia, Hallucigenia.
- **Ордовицький біом:** відкрита товща води з вертикальними «голками» Endoceras над рифом з губок і криноїдів (так виглядає кадр з `Creatures/Endoceras/2.png`). Інші ідеї: трилобіти, граптоліти, інші наутилоїди.
- **Девонський біом:** броньовані риби (Dunkleosteus), рифи, мілководдя, ранні євриптериди.

### 4.4. Головна проблема біому (пропозиція, не затверджено)

**[Пропозиція]** Біоми мають відрізнятися не лише виглядом, а й тим, яку головну проблему ставлять гравцю: видимість і навігація, гази й склад води, тиск і глибина, екологія, що реагує на шум. Тоді гравець змінює поведінку й обладнання, а не просто бачить іншу картинку.

### 4.5. Глибше означає давніше (пропозиція, не затверджено)

**[Пропозиція]** Біля поверхні фауна ближча до сучасної, а що глибше спускається гравець, то давніші лінії зустрічає. Це підсилює відчуття, що гравець «пірнає в минуле».

---

## 9. Додаткові відкриті питання світу

- **[Відкрито]** Які біоми будуть і з яких епох.
- **[Відкрито]** Чи приймається ідея «глибше означає давніше».

## 10. Загальні принципи великих істот

**[Рішення]** У грі є великі підводні істоти «левіафан-класу», але це не копія Subnautica.

**[Рішення]** Великі істоти не повинні автоматично переслідувати гравця лише тому, що помітили його. Вони можуть боятися гравця й захищатися, якщо він втручається в їхню територію або поведінку.

**[Рішення]** Страх має народжуватися з невизначеності: гравець не знає, де істота перебуває та як вона поводитиметься.

**[Рішення]** Поведінка кожної великої істоти унікальна.

**[Пропозиція]** Загальні принципи:
1. Не використовувати універсальну схему «побачив — переслідує».
2. Поведінка має випливати з потреби та внутрішнього стану, а не з одного скрипту.
3. Сенсорна модель істоти (слух, зір, вібрації тощо) має бути зрозумілою й потенційно досліджуваною гравцем через прилади та експерименти.
4. Різні істоти повинні мати різні поведінкові архетипи.
5. Обов'язкові зустрічі варто обмежувати, щоб зберегти їхню напругу.


