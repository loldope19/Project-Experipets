# Project: Experipets

## 🎮 About The Project

Experipets is a virtual pet management game where players take on the role of a junior employee at a mysterious creature experimentation company. The core gameplay revolves around caring for a unique "experiment" by managing its needs, completing daily tasks, and uncovering the story, all through the interface of a retro-style desktop operating system.

The game features a single-scene structure, allowing for seamless navigation between a main desktop, the pet care application, and a virtual shop.

The game also features a unique visual style where the pet and its interface are presented on a virtual computer screen within a 3D environment.

![alt-text](https://github.com/loldope19/Project-Experipets/blob/main/image_2025-07-01_223721624.png "Experipets Sample")


### Current Features (Prototype v0.5)

* **OS-Style UI Navigation:**
    * A complete, single-scene UI managed by a `ViewManager`.
    * Features a Login Screen, a main Desktop View with app icons, and separate "applications" for Pet Care and the Shop.
* **Advanced Pet Care System:**
    * A virtual pet with Hunger, Cleanliness, and Happiness stats.
    * **Feed Interaction:** A fully functional drag-and-drop system for feeding items to the pet.
    * **Clean Interaction:** A refined drag-and-drop system where different tools have unique functions (e.g., a towel for the pet, a broom for floor dust, gloves for accidents).
    * **Play Interaction:** Unique mechanics for different toys, including drag-and-drop chew toys, a "click-to-throw" bouncing ball, and a continuous-play laser pointer.
* **Dynamic Environment & Gameplay Loop:**
    * A "Game Over" state is triggered when any stat reaches zero at the end of the day.
    * The environment dynamically becomes messy: trash appears after eating and poop appears daily.
    * The amount of mess directly impacts the pet's well-being by amplifying the end-of-day cleanliness decay, making cleanup a strategic necessity.
* **Dynamic Task System:**
    * A robust `TaskManager` that daily minor tasks and one major task per chapter.
    * Chapter progression is now tied to completing major tasks, not the day number.
    * The "Shut Down" button now correctly manages the end-of-day sequence, stat decay, and task refreshing.

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
3. Monitor the pet's needs and the daily Task List. You can switch between these panels using the arrow buttons.
4. Use the main toolbar (Feed, Clean, Play) to access your inventory.
5. Interact with items: drag cleaning tools and chew toys, but click the Ball or Laser Pointer to activate their unique modes.
6. Click the "Shop" button to buy more items as needed.
7. "Shut Down" button within the Desktop progresses the day, but pet stats will decay.

## 🗺️ Roadmap (Future Plans for v0.6 & Beyond)

This is a very early prototype. Future development will focus on expanding the core loop and adding more depth:

* **[X] Currency & Shop Systems:** *Implemented.*
* **[X] Day & Stat Systems:** *Implemented.*
* **[X] Task/To-Do System:** *Implemented.*
* **[X] Advanced Interactions:**
    * [X] Implement drag-and-drop item usage.
    * [X] Different cleaning actions.
    * [X] Unique mechanics for different toys (click-to-throw, aim-and-play, drag-and-drop).
* **[ ] Photography & Documentation Loop:** Implement the pop-ups and camera sequence that replaces the simple "End Day" button.
* **[ ] Dialogue System Integration:** Connect the existing dialogue mechanic to story triggers and events.
* **[ ] Save/Load System:** Persist all game state (day, stats, inventory, currency, task progress) between sessions.
    * [ ]Implement offline progression (needs continue to change while the game is closed).
    * [ ] Implement pet movement in response to events (e.g., chasing a thrown ball).
* **[ ] Pet Animations & Feedback:** Add animations for idle, eating, reacting, etc.
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
