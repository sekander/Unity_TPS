# Unity_TPS


Based on the provided scripts, the main points of the game can be summarized as follows:

1. **Game Modes and Networking**:
   - The `GameManager` script defines different play modes (`SINGLE_PLAYER`, `MULTI_PLAYER`, `ONLINE`) and manages game state variables like scores (`p1_score`, `p2_score`), life (`total_life`), and networking details (`server_ip`, `is_player_host`). This suggests the game supports both single-player and multiplayer modes, potentially with online connectivity.

2. **Player Control and Movement**:
   - The `Player` script handles player movement using Unity's `CharacterController`. It incorporates basic movements (walk, run), jumping mechanics (with gravity and surface detection), aiming controls, and animations (`Animator`) to visually represent player actions.

3. **Combat and Weapons**:
   - The `Weapon` script manages shooting mechanics, including raycasting (`Physics.Raycast`) to detect hits on objects (`ObjectHit` script) and trigger corresponding damage effects (`ObjectHitDamage`). It uses particle effects (`muzzleSpark`) and sound effects (`shootingSoundFX`) to enhance the shooting experience.

4. **UI and Menus**:
   - The `MainMenu` script includes UI functionalities such as loading game scenes (`LoadPlayScene`) and toggling UI panels (`TogglePlayPanel`), ensuring smooth navigation between menus and gameplay.

5. **Object Interaction and Environment**:
   - The `ObjectHit` script handles object health and destruction upon taking damage, showcasing basic gameplay mechanics where objects can be interacted with and destroyed.

6. **Camera Management**:
   - The `SwitchCamera` script manages camera switching (`playerCam` and `aimCam`) based on player input (`Aim` axis), providing different perspectives and enhancing gameplay dynamics.

7. **Additional Gameplay Elements**:
   - The `TargetMove` script introduces dynamic object movement (`RotateAround`) and potential gameplay elements involving moving targets or environmental interactions.

These main points outline a game project that integrates fundamental Unity features to create a playable experience with basic movement, combat, UI management, and potential multiplayer capabilities. The scripts suggest a framework for developing a 3D action or shooter game with interactive elements and dynamic gameplay mechanics.

