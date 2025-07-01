# Project: Experipets

## 🎮 About The Project

Experipets is a virtual pet management game where players take on the role of a junior employee at a mysterious creature experimentation company. The core gameplay revolves around caring for a unique "experiment" by managing its needs, completing daily tasks, and uncovering the story, all through the interface of a retro-style desktop operating system.

The game features a single-scene structure, allowing for seamless navigation between a main desktop, the pet care application, and a virtual shop.

The game also features a unique visual style where the pet and its interface are presented on a virtual computer screen within a 3D environment.

![alt-text](https://github.com/loldope19/Project-Experipets/blob/main/image_2025-07-01_223721624.png "Experipets Sample")


### Current Features (Prototype v0.3)

* **OS-Style UI Navigation:**
    * A complete, single-scene UI managed by a `ViewManager`.
    * Features a Login Screen, a main Desktop View with app icons and a functional Start Menu, and separate "applications" for Pet Care and the Shop.
* **Core Pet Care System:**
    * A virtual pet with Hunger, Cleanliness, and Happiness stats on a 0-100 scale.
    * A multi-layered UI for interactions (e.g., clicking "Feed" opens a sub-menu with "Meal" and "Treat" options).
* **Day & Stat System:**
    * A `DayManager` controls the passage of time. Stats no longer decay in real-time.
    * Needs decay by a set amount (25 points) only when the player chooses to "End Day".
* **Advanced Inventory System:**
    * **Item Stacking:** The inventory correctly stacks items of the same type.
    * **Contextual & Paginated UI:** The inventory appears as a pop-up panel, filtered to show only relevant item categories (Food, Toys, etc.). Features left/right arrows to navigate through pages of items.
* **Functional Shop System:**
    * A complete Shop "application" accessible from the Pet Care view.
    * Features a vertically scrollable UI, dynamically populated from a list of all available items for purchase.
* **Dynamic Task System:**
    * A robust `TaskManager` that assigns a list of daily objectives.
    * Supports multiple goal types, including "Use Specific Item" (e.g., "Feed 2 Apples") and "Reach Amount" (e.g., "Get Happiness to 75").
    * The "End Day" button is disabled until all tasks for that day are complete, creating a structured gameplay loop.

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
    * Open the primary gameplay scene (`MainScene.unity`).
4.  **Run the game:**
    * Press the Play button in the Unity Editor.

## 🕹️ How to Play (Current Basic Version)

1. From the Login Screen, click "Log In" to access the main Desktop.
2. Click the "Care" icon to open the Pet Care application.
3. Monitor the pet's needs and the daily Task List.
4. Use the main toolbar (Feed, Clean, Play) to open sub-menus and then the contextual inventory to use items and complete tasks.
5. Click the "Shop" button to buy more items as needed.
6. Once all tasks are complete, the "End Day" button will become clickable. Press it to advance to the next day, which will decay the pet's stats and assign new tasks.

## 🗺️ Roadmap (Future Plans for Basic Version & Beyond)

This is a very early prototype. Future development will focus on expanding the core loop and adding more depth:

* **[X] Currency & Shop Systems:** *Implemented.*
* **[X] Day & Stat Systems:** *Implemented.*
* **[X] Task/To-Do System:** *Implemented.*
* **[ ] Photography & Documentation Loop:** Implement the pop-ups and camera sequence that replaces the simple "End Day" button.
* **[ ] Dialogue System Integration:** Connect the existing dialogue mechanic to story triggers and events.
* **[ ] Save/Load System:** Persist all game state (day, stats, inventory, currency, task progress) between sessions.
    * Implement offline progression (needs continue to change while the game is closed).
* **[ ] Pet Animations & Feedback:** Add animations for idle, eating, reacting, etc.
* **[ ] Advanced Interactions:**
    * Implement drag-and-drop item usage.
    * Different cleaning actions.
    * Simple mini-games for the "Play" interaction.
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

+ Sydrenz Cao / Syd - sydrenz_cao@dlsu.edu.ph
+ Dun Baniqued / Dun - dun_baniqued@dlsu.edu.ph

Project Link: [https://github.com/loldope19/Project-Experipets](https://github.com/loldope19/Project-Experipets)
