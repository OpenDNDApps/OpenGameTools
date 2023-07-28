[![GitHub Issues](https://img.shields.io/github/issues/OpenDNDApps/OpenGameTools)](https://github.com/OpenDNDApps/OpenGameTools/issues) 
![GitHub last commit](https://img.shields.io/github/last-commit/OpenDNDApps/OpenGameTools) 
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/OpenDNDApps/OpenGameTools)](https://github.com/OpenDNDApps/OpenGameTools/releases) 
![GitHub repo size](https://img.shields.io/github/repo-size/OpenDNDApps/OpenGameTools) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/OpenDNDApps/OpenGameTools)

# OpenGameTools

![Logo](https://assetstorev1-prd-cdn.unity3d.com/key-image/70235caf-9a54-4c6b-8986-8fc63ca34b6c.webp)

**OGT** is a compilation of several systems, scripts and extensions aiming to be a lightweight 'framework'.
We, as a team, use this asset every day on production apps and games.
The UI system alone has -collectively- being used for more than 800k users on those apps and games.

Check the [Documentation](https://github.com/OpenDNDApps/OpenGameTools/tree/master/Documentation) for more details.

&nbsp;
  
---

&nbsp;

# **Tech things - TLDR**  
• The UI system focuses on easy to use and performance, self managed instantiation, canvases, sorting layers, etc.  
• An ScriptableObject architecture and event system, the core of the asset. Based on [Ryan Hipple's 2017 Unite talk](https://www.youtube.com/watch?v=raQ3iHhE_Kk) and [Richard Fine's 2016 Unite talk](https://www.youtube.com/watch?v=6vmRwLYWNRo)  
• Event System compatible with ScriptableObjects and MonoBehaviours on runtime  
• Hundreds of C# Extensions  
• Many Scripts and Components to use and "extend" Unity.  

&nbsp;

### **Why would I want to use this?**  
We use this asset as a framework and as a base on every project, feel free to try it, its free.  

### **Do you like this asset?**  
Consider supporting us on [Github Sponsors](https://github.com/sponsors/EduardoU24) - [Patreon](https://www.patreon.com/EduardoU24)  

&nbsp;

&nbsp;

---

&nbsp;
  
# **Features**  

## Managed **Runtime** extensions

### Smart and self managed **Coroutines**


### `ActionAfterFrame`
```csharp
void MyMethod()
{
    this.ActionAfterFrame(DelayedMethod);
}

void DelayedMethod()
{
    Debug.Log("This is printed in the next frame");
}
```

> Why? This is a managed, uninterrupted coroutine. For example, if a GameObject is disabled or destroyed, coroutines may break in many ways. This prevents that.

&nbsp;

### `ActionAfterSeconds`
```csharp
void MyMethod()
{
    this.ActionAfterSeconds(1.5f, DelayedMethod);
}

void DelayedMethod()
{
    Debug.Log("This is printed after 1.5 seconds.");
}
```

&nbsp;

### `ActionAfterCondition`
```csharp
void MyMethod()
{
    this.ActionAfterCondition(IsMyGameCondition, DelayedMethod);
}

bool IsMyGameCondition()
{
    return m_gameReady && m_playerReady;
}

void DelayedMethod()
{
    Debug.Log("This is printed after the condition is met.");
}
```

> More tools and info in [/Documentation/**Runtime.md**](https://github.com/OpenDNDApps/OpenGameTools/tree/master/Documentation)

&nbsp;

--- 

## **UI** Management 

### One line and ready to run:
```csharp
private UIMainMenu m_uiMainMenu = null;
private const string kUIMainMenu = "UIMainMenu";

void OnSceneLoad()
{
    // All TryXX() return null if the logic is not met.
    if (!UIRuntime.TryCreateWindow(kUIMainMenu, out m_uiMainMenu))
        return;
    
    // Show the Window using the Procedural UI Animations.
    m_uiMainMenu.AnimatedShow();

    m_uiMainMenu.CustomLogic();
}
```

<details>

<summary>Setup - How to register UI elements</summary>

> Just a few steps:

1. Make your Window script anywhere you like. Use any of the Base Window elements, such as `UIWindow`, `UITabWindow`, etc.  
2. Make a prefab out of your Window script.
3. Select the `"UIGameResources"` asset on `"/CoreUI/Resources"` or use the `"OGT/Module Resources/Select UI"` menu item on the top the editor.
  
![SelectMenuToSelectUI](https://user-images.githubusercontent.com/1507317/234743919-1ce94888-9aaa-4d94-bda1-9be9e4bf1383.png)

4. Add your Window Prefab to the UIWindows list.

![UIMainMenuSetup](https://user-images.githubusercontent.com/1507317/234743920-f6e75071-2d6f-4548-a9d6-32d2ca16ecc0.png)

5. That's it. Go and use your new window.

> More info: [/Documentation/**UIResources.md**](https://github.com/OpenDNDApps/OpenGameTools/tree/master/Documentation)

</details>

&nbsp;

--- 

## **UI** Animations 

> No code needed.

With procedural animations stored in ScriptableObjects the `UIAnimation` system can be extended and customized in any way you may need, just change the parameters on each animation file as you see fit.

Here we can see a UITabWindow, with 4 different animations. A `ScaleInFadeIn` to show, `ScaleOutFadeOut` to hide, and each tab with `FadeIn` and `FadeOut`.

&nbsp;

> Multiple animations in different elements.

![UITabWindow](https://user-images.githubusercontent.com/1507317/235189949-5c3c4ebd-bdd8-4696-8985-dc179bf8510b.gif)

&nbsp;

> Sibling related animations.

![UIAnimation_List](https://user-images.githubusercontent.com/1507317/235253164-5c2e2c61-9522-4855-8173-29849605e62f.gif)


&nbsp;

> Mouse/Touch event animations.

![UIAnimation_PunchOnPointerEnter](https://user-images.githubusercontent.com/1507317/235216355-897fdfbe-6c1d-4845-ae62-62b672904425.gif)

&nbsp;

> Multiple animations.

![Unity_tepmoisvzW](https://user-images.githubusercontent.com/1507317/235211829-91ebda96-5d4c-4e26-a060-ce509ca2a574.gif)


<details>

<summary>How it works? - How to customize</summary>

Each animation is a file. For example here is `FadeIn`.

![FadeIn](https://user-images.githubusercontent.com/1507317/235195736-2466c40d-ea42-4fa8-a49b-12b79de79b86.png)

To customize this, just add and sort each step in the animation.

![UIAnimationOptions](https://user-images.githubusercontent.com/1507317/235197672-e3184bcc-3837-4762-89ea-cfbdf8b29501.png)
![SortingUIAnimationSteps](https://user-images.githubusercontent.com/1507317/235200443-5add7519-fff9-465d-88c1-dbc1aaeb4c26.gif)

</details>

&nbsp;

> Notice that this is the code for that window. You only need to worry about your logic.

```csharp
using OGT;

public class ExampleWindow : UITabWindow
{
    [Header("Example Window")]
    [SerializeField] private TMP_Text m_title;
    [SerializeField] private TMP_Text m_message;

    protected override void OnInit()
    {
        base.OnInit();

        m_title.SetLocalizedText("My Custom Tab Window");
        m_message.SetLocalizedText("This window was created in runtime and is using UI Animations.");
    }
}
```

&nbsp;

--- 

&nbsp;

# Random

UI Gradients, usable on any UI element, using TMP Gradients
![UIGradient](https://user-images.githubusercontent.com/1507317/235382060-91589fe1-a65c-4b48-b3b3-4e8b14e434bc.gif)

--- 

&nbsp;

# **Installation**

&nbsp;

1. Make sure you have the `Assets/OpenGameTools` folder in your game.

&nbsp;

2. In your initialization scene you might want to add the `GameRuntime` and the `UIRuntime` prefabs, they will auto generate anyway, but to assure 100% reliability make sure they are somewhere.

&nbsp;

3. Setup the UI: 

   Go to the `GameSettings`, that you can access with the top MenuItem `OGT/Module Resources/Select Settings`

   ![SelectMenuToSelectSettings](https://user-images.githubusercontent.com/1507317/234758959-e761114c-7c7f-4925-a3e6-4160f77cc78b.png)

   In here you can configure your desired Sorting Layers for the UI, we recommend the default values, you might want to add more.

   ![UISettings_Sorting](https://user-images.githubusercontent.com/1507317/234758456-e4cf39fc-3a70-41ac-b585-7099710fdb07.png)

   These values must match the actual Sorting Layers, so make sure to add them.

   ![UITagsAndLayers](https://user-images.githubusercontent.com/1507317/234758208-93b21c9b-5447-411c-9135-3f0ec55e6a53.png)

&nbsp;

4. At this point **you're ready**, but to harness the full potential of the asset, we recommend the following optional steps: 

&nbsp;

## Optional and recommended:

1. **Add the `GameRuntime` prefab** to your InitialScene or similar to ensure features such as Coroutine extensions.

&nbsp;

2. **Create your own Module** and move it outside the OpenGameTools folder, use that Module as a base for you to start extending the functionality.

   You can do this easily with a few clicks.

   Open the CreateModule window

   ![CreateModuleMenuItem](https://user-images.githubusercontent.com/1507317/234743915-1ff558ed-ec6e-4dd3-8c13-cc02bf96319d.png) 

   ![CreateModuleWindow](https://user-images.githubusercontent.com/1507317/234743917-359f2fb0-4716-4519-b6ee-09906f01940c.png)

   With this, you can have things like a separated file for Settings but keeping the actual data in the same ScriptableObject. This new module will have its own `MyModule`**`SettingsCollection.cs`** that might have new variables to use and you can find them in the same file from the `Select Settings` menu item.

   ![GameSettingsScriptableObject](https://user-images.githubusercontent.com/1507317/234746116-7cd5eab5-4c9d-446d-88cc-d2ec3e5fc2f1.png)

   For example in the Image above, the `User`, `CoreUI` and `Spotify Integration` Modules are in the same `Settings Collection` but keeping their code in the corresponding files `UISettingsCollection.cs`, `UserSettingsCollection.cs` and `SpotifySettingsCollection.cs`

&nbsp;

3. Extend the `Base<>` scripts as a parent behaviour for your scripts. For example `BaseBehaviour` instead of `MonoBehaviour`, `BaseScriptableObject` instead of `ScriptableObject` and for any script that should be in UI, use any variation of `UIItem`, check `UIItem.md` for more info.

   ```csharp
   public class MyScript : BaseBehaviour { /*...*/ }
   ```

&nbsp;

---

### Notes:

> `GameResources.cs` acts as a main definitions file in static constants. If you want to personalize your OGT feel free to do so. For example: `kPluginName`

> Only `/Core` and `/CoreUI` are needed for the plugin to work, feel free to delete any other `Module` you don't need.

&nbsp;

&nbsp;

---
