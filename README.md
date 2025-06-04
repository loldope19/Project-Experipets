# Project: Experipets (Working Title)

## 🎮 About The Project

Experipets is a virtual pet management game currently in its early prototype stage. Players take on the role of a junior employee at a creature experimentation company, tasked with the wholesome job of caring for unique "experiments." The core gameplay revolves around maintaining the well-being of these creatures by attending to their basic needs.

The game features a unique visual style where the pet and its interface are presented on a virtual computer screen within a 3D environment.

![alt-text](https://github.com/user-attachments/assets/f1838792-7f89-46d0-be72-0c1243004cd3 "Experipets Sample")


### Current Basic Features (Prototype v0.1)

* **Virtual Pet:** A single "experiment" (pet) entity.
* **Core Needs System:**
    * **Hunger:** Decreases over time.
    * **Cleanliness:** Decreases over time.
    * **Happiness/Playfulness:** Decreases over time.
* **Basic Caretaking Interactions:**
    * **Feed Button:** Increases the pet's Hunger stat.
    * **Shower/Clean Button:** Increases the pet's Cleanliness stat.
    * **Play Button:** Increases the pet's Happiness stat.
* **Visual Feedback:**
    * A static 2D sprite representing the pet.
    * UI Sliders visually displaying the current levels of Hunger, Cleanliness, and Happiness.
* **Unique "Pet on Screen" Environment:**
    * A basic 3D scene with a desk and computer.
    * The pet and all its UI elements are rendered on a World Space Canvas, appearing as if they are on the 3D computer's monitor.

## 🛠️ Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

* **Unity Hub**
* **Unity Editor (Version 2022.3.9f1)**

### Installation & Setup

1.  **Clone the repository:**
2.  **Open the project in Unity Hub:**
    * Launch Unity Hub.
    * Click "Open" or "Add project from disk."
    * Navigate to the cloned repository folder and select it.
3.  **Open the main scene:**
    * Once the project is open in the Unity Editor, navigate to the `Assets/Scenes/` folder.
    * Open the primary gameplay scene (e.g., `MainScene.unity`).
4.  **Run the game:**
    * Press the Play button in the Unity Editor.

## 🕹️ How to Play (Current Basic Version)

1.  Observe the pet (by which I mean a static image of Pou) on the virtual computer screen.
2.  Notice the three status bars representing Hunger, Cleanliness, and Happiness. These will gradually decrease.
3.  Click the "Feed," "Clean," or "Play" buttons on the virtual screen to replenish the corresponding need for your experiment.

## 🗺️ Roadmap (Future Plans for Basic Version & Beyond)

This is a very early prototype. Future development will focus on expanding the core loop and adding more depth:

* **[ ] Basic Pet Animations:** Simple idle, eating, cleaning, playing animations.
* **[ ] Save/Load System:** Persist pet stats and game state between sessions.
    * Implement offline progression (needs continue to change while the game is closed).
* **[ ] More Varied Interactions:**
    * Different types of food with varying effects.
    * Different cleaning actions.
    * Simple mini-games for the "Play" interaction.
* **[ ] Currency System:** Earn currency for taking care of the pet.
* **[ ] Shop System:** Spend currency on food, items, or basic customization.
* **[ ] Task/To-Do System:** (As per original concept) Simple objectives for the player.
* **[ ] Story Elements:** (As per original concept) Introduce more narrative.
* **[ ] Sound Effects & Music.**
* **[ ] Refined 3D Environment and UI.**

## 💻 Technologies Used

* **Unity Engine**
* **C#**

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!
As this is an early-stage personal project, direct contributions might be limited, but feel free to fork the repository and experiment.

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

## 📄 License

Distributed under the MIT License. See `LICENSE.txt` for more information.

## 📧 Contact

Sydrenz Cao / Syd - sydrenz_cao@dlsu.edu.ph
Dun Baniqued / Dun - dun_baniqued@dlsu.edu.ph

Project Link: [https://github.com/loldope19/Project-Experipets](https://github.com/loldope19/Project-Experipets)
