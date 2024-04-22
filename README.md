

# AR-KungFu


## Game Introduction：《KungFu》


 ![image](https://img.shields.io/badge/AR-KungFu-red)  ![image](https://img.shields.io/badge/License-MIT-orange)  ![image](https://img.shields.io/badge/Rander-URP-purple) ![image](https://img.shields.io/badge/Rander-URP-yellow)
 
**☯️Project Introduction**

![image](https://s2.loli.net/2024/04/23/5Egxt2VObhd1uQK.gif)

## ☯️Video Sample
* ## [ **KungFu Concept Video**](https://youtu.be/a6mUjBHMFNM?feature=shared)


**KungFu** is a game that combines AR (augmented reality) and martial arts. It uses the Holokit module to achieve three-dimensional rendering and hand tracking. Based on the motion path obtained by the dynamic capture martial arts master, virtual reality is realized through modeling and bone binding. People lead the training, and spatial computing technology is used to realize the development of Tai Chi AR mode, including three modes: single-player follow-up training, single-player monster fighting and multi-player battle.

**☯️Story summary:** In the chaos of the global magnetic storm, the Five Elements are out of balance, and the world is in urgent need of martial artists to collect the scattered fragments of the Five Elements. As the protagonist, the player will enter a mysterious cave and meet five virtual masters representing the five elements of gold, wood, water, fire and earth. Players must learn the boxing techniques of their respective Five Elements, collect fragments through challenges and practice, and reshape the stability of the Five Elements. With continuous practice and practice, as well as conquering monsters of various elements, players will gradually understand the interdependence and restraint of the five elements, and eventually become legends in the five elements martial arts world. In the game, players will work with other martial artists to protect the peace and stability of the world and maintain the balance and harmony of the five elements.     






![image](https://s2.loli.net/2024/04/18/9sqarSUplGLygeb.png)  ![image](https://s2.loli.net/2024/04/18/LfdOsu91xYDE5eP.jpg) ![image](https://s2.loli.net/2024/04/18/GIPJhigTKe4vW8H.png)                 






                                                                                                                                                                                          
**☯️How to play:**

1. Open the KungFu (Kung Fu) app, click to select the practice mode, and select the boxing method you want to practice.
2. Click on the binocular rendering mode, insert the phone into the Holokit head-mounted display device and wear it
3. Click the start button. After the countdown is over, follow the arrow you see to the blue avatar positioning point. Move your hands to follow the positioning mark to trigger special effects. At the same time, you can refer to the avatar in front for Tai Chi practice.

![image](https://s2.loli.net/2024/04/23/nVtNT9k1MIFaDuC.gif)
![image](https://s2.loli.net/2024/04/23/1HJmtcXTCznx7wg.gif)
![image](https://s2.loli.net/2024/04/23/LAyuXM94j1VU3Qw.gif)

> Mobile phone screen in stereoscopic rendering mode


## Released——Single player follow-up mode
![image](https://s2.loli.net/2024/04/22/o7z5huGURgb8Z9I.gif)  ![image](https://s2.loli.net/2024/04/22/nOqSakQuw8jGsNY.gif) 

* [☯️ **Holokit software development kit**](https://github.com/holokit/holokit-unity-sdk)
  
HoloKit is an optically clear mixed reality headset that transforms an iPhone into a powerful stereoscopic AR device. With the help of HoloKit Unity SDK, call its two core functions of stereoscopic rendering and hand tracking to develop martial arts AR experiences in Unity, which can be viewed through HoloKit head-mounted display devices.        
Stereoscopic rendering is a core feature of the SDK that allows stereoscopic images to be displayed on the iPhone screen. By connecting your iPhone to HoloKit, you can get an immersive audio-visual experience in the world of AR martial arts. Leveraging the Apple Vision framework, the SDK detects the user's hand gestures. Together with the LiDAR depth sensor, the iPhone is used to track the 3D position of the user's hand to trigger Tai Chi special effects.

![image](https://s2.loli.net/2024/04/19/RlbIvGm3qe1SHWP.gif)      









* ☯️****Motion Capture****   

****Virtual Human Production****
In the follow-up training mode, a virtual human is used to lead the training, and [Move One](https://www.move.ai/single-camera) is used to dynamically capture the martial arts master, and the high-standard and high-standard Tai Chi movements are used as the virtual human skeleton. Based on the motion, import it into blender for animation simulation, bake the animation and then import it into unity for development. Combining traditional design with modern design, design a virtual human image, and bind it with bones to realize virtual human Tai Chi training.

![image](https://s2.loli.net/2024/04/18/V7lNFJuZcYRx6eQ.gif) ![image](https://s2.loli.net/2024/04/18/PtHi5kChjq2KOpB.png)

 ![image](https://s2.loli.net/2024/04/18/t7YuV5lyEjdDWsg.gif) ![image](https://s2.loli.net/2024/04/19/CVbWLA41Ejnxre2.gif) 
 
 

* ****☯️Spatial Operations****   

Depending on the motion capture model we are using, we have multiple anchor points set up in the game space. When the player makes hand movements during the game, we use spatial operations to identify the position of the hand, and trigger corresponding spatial effects based on changes in hand position. According to the size control of the virtual person, the spatial calculation of the physical position of the real person is realized, and the movement speed is controlled through hand guidance to achieve the operation and triggering of the game in the space.

* ****☯️Art Production****   
                                                                                                          

When playing the game, the player moves his hand following the cursor. When the cursor moves to a fixed point and stops, the position relationship between the player's hand and the cursor is calculated, OnCollisionEnter is used to trigger, and the nodes are connected to form a cursor dissipation animation.

![image](https://s2.loli.net/2024/04/20/JDgsuh2YQKz67Iv.gif)    ![image](https://s2.loli.net/2024/04/20/Zm73db1egkTCWKw.png)        
   To achieve the object trailing effect:   
```ruby
  for (int x = -1; ++x < pointCount;)
{
    Vector3 a = vertices[x];
    Vector3 b = vertices[x + nextSegmentPoint];
    Vector3 c = vertices[x + next2];
    Vector3 d = vertices[x + next3];

    for (int n = -1, index = pointCount + x; ++n < corner; index += pointCount)
    {
        float t = (n + 1) * toCornerT;
        vertices[index] = CatmullRomSpline(a, a, b, c, t);
        vertices[index + nextSegmentPoint] = CatmullRomSpline(a, b, c, d, t);
    }
}
```

* ****☯️Special Effects Production****

When the player completes the movement of his hand and triggers the cursor, the cursor dissipates and triggers the Tai Chi special effects animation. Set different special effect shapes and trigger effects according to the five elements attributes.

![image](https://s2.loli.net/2024/04/20/xPsRL83rykzGfAn.gif) ![image](https://s2.loli.net/2024/04/20/t8Gcf7e3hEgMxAw.gif) 

![image](https://s2.loli.net/2024/04/22/sXJaDlgSeEr6KGq.gif) 



## Planned——Multimodal Development

   ![image](https://s2.loli.net/2024/04/23/RSIsr2jHGyeMzOm.gif)

* ****☯️Pattern Development****   

We are currently focusing on developing a virtual human follow-up mode to help users improve their skill levels. As the project develops, we plan to expand to a single-player monster-fighting mode and a multi-player battle mode. This will involve the design and implementation of game mechanics, including the development of enemy AI, combat systems, multiplayer online functions, etc.

* ****☯️Content Production****   

We are in the planning stage, aiming to improve the teaching content of Tai Chi's full-style boxing method in the future, and further produce special effects for related movements. This plan will involve in-depth research on Tai Chi and the systematic design of teaching content, as well as the pursuit of excellence in special effects production.

 ## RoadMap

![image](https://s2.loli.net/2024/04/23/uYr8kvS7dtcbHqU.png)

 ## License
 This project is licensed under the MIT License. For details, please see the [ License](https://github.com/ar-KungFu/ar-KungFu?tab=MIT-1-ov-file#) file.



