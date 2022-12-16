# Augmented Reality Posters

## Main Files Used/Included in the Project

- Assets/Animations - Animations used for the rigged model in the prerecorded author presentation feature
- Assets/Audio - mp3 file used for the prerecorded presentation
- Assets/Images - Images used for the play and pause button materials found with in the video and prerecorded presentations
- Assets/Materials - Materials used for the play and pause buttons/overlays
- Assets/Models - Models used for virtual content, including the interactable 3D objects, the rigged model for the prerecorded presentation, and the materials/textures used for the rigged model
- Assets/Resources - VuforiaConfiguration and more animations used for the rigged model
- Assets/Scenes - Contains the Unity scene for the application
- Assets/Scripts - Scripts used for interacting with the virtual content (play/pausing audio and video, rotating and scaling the 3D figures)
- Assets/Video - Videos that get projected onto the marker

## Setting up and Running Locally

This project was edited using Unity 2021.3.13f1

1. Clone the repo locally with `git clone https://github.com/hhuan110/ar_poster.git`.

2. Ensure you have Vuforia developer account or create one [here](https://developer.vuforia.com/vui/auth/register) and download the package. The package we used for development was version 10.11.3. 

3. Move the Vuforia package to the Packages folder (File is too large to upload to GitHub).  

4. Import it into Unity Hub and open the Unity Project. This may take several minutes. 

5. The scene containing the components for the AR poster can be found under Assets/Scnes/poster.scene. Open this scene to view the parts. 

6. If the markers are not already imported and visible in the scene, the file can be found at the root directory, named `ARPosters_final.unitypackage`. Import this as a Custom Package using Assets > Import Package > Custom Package. 

4. The skull model file located at Assets/Models/skull.fbx was uploaded using GitHub's lfs. If the model is not found in the project, it can be downloaded from [this link](https://drive.google.com/file/d/1uTVcR0cIUeYg-HT1DJ3xL6QFUFOTGamq/view?usp=share_link). Download this file and replace it in the directory. 

5. Click the run button at the top to run the project locally. The targets used in the project can be found [here](https://github.com/hhuan110/ar_poster/READMEAssets/printable_markers.pdf). 

## Sources of Markers/Models
1. Abdomen model from class, used in an in class exercise
2. CT Scan of an Abdoment from [Wikipedia](https://en.wikipedia.org/wiki/Computed_tomography_of_the_abdomen_and_pelvis)
3. Skull mlodel from [Embodi3D](https://www.embodi3d.com/files/file/39-anatomical-skull/)
4. Skull diagram from [Wikipedia](https://en.wikipedia.org/wiki/Skull)
5. Genshin Impact Hu Tao model from [MiHoYo](https://ys.biligame.com/ysl/?spm_id_from=333.788.b_61637469766974795f766f7465.1)
6. Genshin Impact Hu Tao icon from [Genshin Impact Wiki](https://genshin-impact.fandom.com/wiki/Hu_Tao/Media)
7. Model animations from Mixamo

## Deployment to Android

1. Connect your Android device to your laptop with a cable.
2. Under File > Build Settings, switch the build platform to Android and select your device from the run device dropdown. 
3. Click on Player Settings in the bottom left corner. Switch to the Android tab in this menu. 	
4. In the Other Settings section, scroll down to Configuration and set Scripting Backend to IL2CPP. 
5. Make sure that ARMv7 is unchecked and ARM64 is checked. 
6. Under Identification, set the Minimum API Level to Android 8.0 'Oreo' (API level 26). 
7. Returning to the Build Settings menu, click Build And Run, saving the generated .apk file to a Builds folder that you create. 

## Deployment to iOS
