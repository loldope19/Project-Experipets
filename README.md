# Project: Experipets

## 🎮 About The Project

Experipets is a virtual pet management game currently in its early prototype stage. Players take on the role of a junior employee at a creature experimentation company, tasked with the wholesome job of caring for unique "experiments." The core gameplay revolves around maintaining the well-being of these creatures by attending to their basic needs.

The game features a unique visual style where the pet and its interface are presented on a virtual computer screen within a 3D environment.

![alt-text](https://github.com/user-attachments/assets/f1838792-7f89-46d0-be72-0c1243004cd3 "Experipets Sample")


### Current Features (Prototype v0.2)

* **Virtual Pet & Core Needs:** A single "experiment" (pet) entity with depleting Hunger, Cleanliness, and Happiness stats.
* **3D Environment with World-Space UI**
    * The pet and its core interface are presented on a virtual computer monitor.
    * A **3D Shop Environment** is located behind the player, accessible via a smooth 180-degree camera rotation.
* **Shop System:**
    * Players can spend currency to purchase items.
    * The shop UI is dynamically generated from item data and presented on world-space canvases attached to 3D shelves.
    * Features a horizontally scrollable UI to accommodate many items.
* **Inventory System:**
    * **Item Stacking:** The inventory now correctly stacks items of the same type (e.g., "Apple x4").
    * **Contextual Display:** The inventory panel appears at the top of the screen when the "Feed", "Clean", or "Play" buttons are pressed.
    * **Category Filtering:** The inventory intelligently shows only the relevant items for the chosen action (e.g., only "Food" items appear when "Feed" is pressed).
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
2.  **To use an item:**
    * Click "Feed," "Clean," or "Play" to open the inventory panel with relevant items.
    * Click the "Use" button on an item in the panel to apply its effects to the pet.
3.  **To buy items:**
    * Click the "Shop" button. The camera will rotate to the shop area.
    * Click the "Buy" button on items you wish to purchase.
    * Click the "Back" button (or similar) to rotate the camera back to the pet screen.

## 🗺️ Roadmap (Future Plans for Basic Version & Beyond)

This is a very early prototype. Future development will focus on expanding the core loop and adding more depth:

* **[X] Currency System:** *Implemented. Though not shown in UI yet.*
* **[X] Shop System:** *Implemented.*
* **[ ] Task/To-Do System:** (As per original concept) Implement a list of objectives for the player to complete each day.
* **[ ] Save/Load System:** Persist pet stats and game state between sessions.
    * Implement offline progression (needs continue to change while the game is closed).
* **[ ] Basic Pet Animations:** Simple idle, eating, cleaning, playing animations.
* **[ ] Advanced Interactions:**
    * Implement drag-and-drop item usage.
    * Different cleaning actions.
    * Simple mini-games for the "Play" interaction.
* **[ ] Story Elements:** Introduce more narrative based on the original concept.
* **[ ] Sound Effects & Music.**

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
