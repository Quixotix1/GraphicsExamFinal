# GraphicsExamFinal

The project outlined above is a demonstration for a few shaders intended for a modern Pac-Man experience. The shaders in the project are: a hologram, a scrolling texture, a rotating toon texture, and a transparent glass + custom rim texture. Each shader was produced in ShaderGraph and can be found in Assets/Shaders. The materials used can be found in Assets/Materials and the 3D models can be found in Assets/Models. The only model used in the project is a ghost model created and used by myself for GDW III: Mini-Interactive Project 1. Outlined below is the logic behind each shader, its usage in the project, and why it is effective in this case.

## Project Vision

This project is intended to bring Pac-Man into the modern world with a sci-fi lens. Visually, the bright neon colors of Pac-Man contrasting with the dark, moody backdrop create a nice contrast found in the cyberpunk aesthetic. With this in mind, the plan for this project is to graphically enhance Pac-Man assets through the artistic style of sci-fi cyberpunk. This means a black backdrop, bright neon colors, and uniquely sci-fi elements that are represented as primitives in the original release of the game.

## Hologram Shader

<img width="291" height="198" alt="image" src="https://github.com/user-attachments/assets/508b5291-e074-4431-be7f-c95d3867b0e9" />

The hologram shader is a transparent shader which uses a fresnel effect coupled with an array of lines flowing in some direction (in this case, downwards). It is meant to represent the visual effect of a 3D projection as seen in many sci-fi movies. We are using the hologram shader as our powerup pellets, the first of many sci-fi changes coming up. This makes the pellet look more modern and effectively conveys why it flashes in the original game. As a point to that flashing, the hologram shader used in this project has an additional flashing effect wherein the alpha transparency flashes from the "transparency" input value to 0 over some "blink speed" time. The full graph is listed below.

<img width="2100" height="1006" alt="image" src="https://github.com/user-attachments/assets/b06597e7-37a2-4ddc-a18e-e3312e3d93e9" />

The following is the Fresnel section. Shadergraph has the extremely useful component of simplifying the fresnel calculation (1 - view direction dot normal direction) and even allows you to input a simple "power" float to increase manipulation potential even further. Thus, the only additions I added were changing the color by multiplying the fresnel output by some color input "fresnel color" and multiplying that by some float input "fresnel intensity" to get the final result for the fresnel component.

<img width="1183" height="643" alt="image" src="https://github.com/user-attachments/assets/6cdc6316-f5f9-4250-afa8-0188ab33ed4f" />

