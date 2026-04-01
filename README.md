**Included:**
- Networked Multi Animator Slider Driver
- Animator Slider Relay

**How to Use:**
- Import all 4 files into project.
- Find and click **USharp** Assets, and drag respective **C#** into **Source Script**.
- Click **Compile All UdonSharp Programs** to fix any compile errors before continuing.
- Create Master/Parent object that will hold both the animated object, and the **Canvas** + **Sliders**.
- Add a **Animator** Component to the Master/Parent object.
- Add the **NetworkedMultiAnimatorSliderDriver** U# as a UdonBehaviour Component to the Master/Parent object.
- Set Synchronization Method: **MANUAL**.


***SETTING UP YOUR CANVAS AND ADDING YOUR FIRST SLIDER***
- Create Canvas
- Set **Render Mode** of Canvas to **World Space**.
- Reset XYZ Position of **Canvas**
- Adjust scale of Canvas to appropriate size
- Add UI Slider by right clicking **Canvas** -> UI -> Slider.
- Add VRCUIShape Component to **Canvas**
- Change Layer of Canvas to **Default**
- Add **AnimatorSliderRelay** U# as UdonBehaviour Component to the new Slider.
- Drag the Master/Parent Object into **Driver**
- In Slider settings, change **OnValueChanged** to **Editor And Runtime**
- Drag the new slider from the hierarchy into the **OnValueChanged** section (left input).
- Find **No Function**, should be right of **Editor And Runtime**, select **SendCustomEvent**, and then type in the bottom field: **OnSliderValueChanged**
- Set Synchronization Method: **MANUAL** inside **Animator Slider Relay**.

Quick Note:
*Slider Index, starts at **0**, if you have **1 Slider**, the index is still **0**!*
*If you have 2 or more sliders, it will follow this: Slider 1 = 0, Slider 2 = 1, Slider 3 = 2, etc..*


***SETTING UP THE MASTER/PARENT SCRIPT***  "NetworkedMultiAnimatorSliderDriver"
- Assign the Animator that we setup prior into **Target Animator**.
- Change value of **UI Sliders** to the number of sliders being used, if you add more later, change this number, drag in new slider to new element.
- Change value of **Float Parameter Names**, name these whatever you want! Remember them!
*This is changed in the Master/Parent script*


***SETTING UP THE ANIMATOR***
- Select **Master/Parent** object, open **Animation Tab** via Window -> Animation -> Animation, Drag near Project/Console, or Inspector area for easy accessibility.
- Create a Animation Clip, this will be your first animation & will create your Animator.
- Open Animator
- Create a Layer for each value you want to change.
*Example: Slider1 Layer, Slider2 Layer, Slider3 Layer, etc*
- Ensure each Layer has a Weight of 1; do this by clicking the cogwheel to the right of the layer, and dragging the weight to 1, or setting the number from 0 to 1.

- Create Float Parameter named from previous **Float Parameter Names**.
*Example: layer1param*

- Create/Use Animation file, this could be moving a cube, rotating, changing a value on a shader, etc..
- Select Animation file in assets, ensure **Loop Time** is **OFF**!
- Drag Animation File into Layer, it should become ORANGE.
*If one already exists that is orange, delete that one, it should make the one you dragged in default*
- Select the default (orange) animation inside the layer, change **Speed** to **0**, and enable **Motion Time** Parameter by checking the Boolean box on the right side.
*Ensure you set the Motion Time value to the Parameter you want it to drive; example: layer1param*
