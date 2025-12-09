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

The following is the scrolling lines section. It looks more complicated but is actually markedly simpler. We take the UV y-position and multiply it by some float input "line frequency," the impact of which will be explained in a moment. We then add the time component to make a variable of which the y-position of the UV changes over time, using some float input "line speed" to affect the rate at which this change occurs. After adding them together, we input this into a sine function. This is why the line frequency is used, as a greater value in the sine function is not represented by a bigger number, but instead differently at some point between 0 and 1. In other words, line frequency increases the amount at which the sine wave iterates over time. We plug this into a step function to make a harsh contrast between the sine function being "greater" and "less than" 0.5 (our chosen input) and multiply this by some color input "line color" which modifies the color of the white lines and achieve our intended output.

<img width="1637" height="930" alt="image" src="https://github.com/user-attachments/assets/978790d2-969f-4347-b2b8-5c0b004ee33a" />

The following is the blinking section. We multiply the time by some float input "blink speed," then floor it. This essentially means we're counting in whole integer seconds rather than some float, enabling us to transition across 0 and 1 without having an awkward smoothing effect that doesn't exist in the original. We take this value and mod it by 2, so even numbers return 0 and odd numbers return 1. This is our final alpha value, now blinking every "blink speed" seconds.

<img width="830" height="723" alt="image" src="https://github.com/user-attachments/assets/2c8f5cd2-593a-4762-ab74-25e5c646bb16" />

In the end, we add our fresnel and scrolling lines to get our color output and multiply the blink function by some float input "transparency" to get our alpha output.

<img width="1166" height="717" alt="image" src="https://github.com/user-attachments/assets/b450ef5f-6cf1-455b-bc5d-8c8cbf1204fe" />

## Scrolling Shader

<img width="591" height="418" alt="image" src="https://github.com/user-attachments/assets/110f397e-1af0-48d7-a5af-7ffef116036f" />

Though harder to represent, the shader above is applied to our floor which is scrolling a material input. The reasoning for this application is that, in a sci-fi cyberpunk world, it is not uncommon to see extreme overuse of screens decorating the walls and floor. Thus, in this neon landscape, I think it's relevant to make the floor scroll with a dark, blue-ish black texture. The method to accomplish a scrolling shader is much simpler than above, we simply need to move our UV over time. The shader above is modified by some color input "base color" to achieve the darkened results.

This shader should not require a sectional analysis. To summarize, the time input is multiplied by some float "scroll speed" and added to our UV to offset our UV map over time. We then process our sample texture using this offset UV to make the texture continually move and wrap around over time. Lastly, we multiply this texture by our "base color" color input to achieve our final results.

<img width="1625" height="773" alt="image" src="https://github.com/user-attachments/assets/359e80b4-0480-4f97-8878-968e78bdde21" />

## Rotating Toon Shader

<img width="319" height="441" alt="image" src="https://github.com/user-attachments/assets/7008f2ee-c206-4492-901f-6a2728c4c1ea" />

The idea for the rotating toon shader stems from two key observations: firstly, the ghosts in Pac-Man appear to rotate as their spiky bottom changes, and secondly, that the ghosts in Pac-Man simply appear very cartoon-esque. They flash, they turn invisible, they have huge bulging eyes, and so on. The toon shader not only enables us to capture the aesthetic of the original Pac-Man ghosts, but also allows us to bring it into a modern sci-fi lens.

<img width="1440" height="1272" alt="image" src="https://github.com/user-attachments/assets/8cecea8d-414b-411d-b2b2-8524cf33600c" />

The following is the fresnel section of the shader. We want a harder border on the outside rather than the smoothness that we used earlier with the hologram shader, so we use a step function (like we did with the scrolling lines) to forcefully shift this fresnel effect to 0 (black) or 1 (white) according to some float input "rim size." We multiply this by some color "rim color" and add this to our main color to achieve our final color output.

<img width="1334" height="1054" alt="image" src="https://github.com/user-attachments/assets/6a620905-63ac-42bd-8bd9-a5aed2b5c6dc" />

The following is the rotate section of the shader. We want to rotate the position of our target in object space, so we use the "Rotate About Axis" function, taking in our position, some vector 3 input "axis," and we construct a time variable in the same way we have done before, multiplying it by some float input "rotation speed." An important note is that the output is plugged into both the position _and_ normal outputs, as without this detail, the normals will not rotate and our toon effect will no longer be facing the camera or view position.

<img width="911" height="879" alt="image" src="https://github.com/user-attachments/assets/6d256d8f-8ed3-40ee-a7d9-055f6baa1a0f" />

These are plugged directly into the vertex and fragment nodes to get our output.

## Custom Transparent Shader

<img width="584" height="537" alt="image" src="https://github.com/user-attachments/assets/fb9d1b07-f28f-414e-baa2-5f3df43ddcac" />


This is a bit of a weird one. The goal of this shader is to be applied to the walls and create a pixel-style aesthetic square border aesthetic around the walls while keeping the walls semi-transparent as though they are glass. It is technically fair to say that Pac-Man knows where the ghosts are based on the player's knowledge and their controlling of Pac-Man. The best way to represent this is to have the walls made of glass. However, I was unsatisfied with this--the walls in Pac-Man clearly have square borders around them to denote their exterior. The shader produced is a solution to this, generating a square border manually using some tricks.

<img width="2100" height="1159" alt="image" src="https://github.com/user-attachments/assets/33521b5b-b155-4e0b-8aa3-4fd0c7be1fcc" />

The following is the square borders section of the shader, and it is likely the most complex component I've added to this project. We begin by taking the x and y-positions of our objects (it is assumed that the walls face the z-direction) and multiplying them by some float input "rim size." The output is basically just a black-and-white color that represents the position of some vertex. We then pass this into an "invert colors." A convenient side-effect of how we are using this node is that it strictly multiplies our input by its inverse, which gives us an extremely convenient single strip representing the line perpendicular to our input axes (which, again, is our x and y-positions). In other words, vertices further away from the center in the x and y-directions are represented as being brighter. We floor these two values to create a harsh contrast between the bright and dark sections and add them together to create a full square border around our box. We then re-represent this as an absolute value so that we do not have any negatives being represented in our output.

<img width="2068" height="1120" alt="image" src="https://github.com/user-attachments/assets/a4308157-a70a-4a92-9d06-10d50c8ee46e" />

The following is the remaining section of the shader. The output of the square borders is multiplied by our texture and the input color "rim color" to create our output color, and the transparency is a mix of the alpha channel of our texture and some float input "transparency."

<img width="1836" height="1092" alt="image" src="https://github.com/user-attachments/assets/416b2b6f-1052-441c-87fe-b2d927ce8bfd" />

## Summary

The power pellets are represented by a hologram, the floors are represented by a scrolling shader, the ghosts are represented by a rotating toon shader, and the walls are represented by a custom transparent shader with manually-driven square borders. The final build includes all of these components, with a moving wall in front of the camera to demonstrate its semi-transparent nature. No gameplay was provided so as to provide more time to create suitable and attractive shaders for the final product.
