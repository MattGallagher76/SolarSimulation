# MOVIS Project Base Template for Asymetric Multi-User Platform.
> Base Platform Templates and Multiple Projects
> 
Please pull this repo with Unity 2020 LTS 3.xx. 

## Please Read!
- DO NOT make any changes on **base_template(main)**.
- Automatic matching of tracker mathcing with tablet users are implemented in "Steam_VRTrackedObject" script.
- DO NOT update "Steam_VR Tracked Object" script, or reimport SteamVR package which might change the script.
- Trackers' serial nubmers should be identified and synchronized for automatic tracking.


## Network Setting
- Multiplayer setting was implemented by Photon Pun. Please use your own server by setting the Pun ID.
- In the Lobby Scene, both headset user and tablet users are connected to the server and joined the lobby.
- After hitting the "Enter the Room" button, they will be moved in toe the Room1.
- **Make sure VR headeset user join in the room first**, so that VIVE Trackers are availalbe for the tablet users.
- Unidentified trackers will be stationary in the scene. 

## How to setup a new VIVE Tracker.
1. Go to */Assets/Photon/PhotonUnityNetworking/Resources*
2. Open TrackerX (X is the number between 1 to 9).
3. Open "Steam_VRTrackedObject" script attached in TrackerX prefab.
4. At the end of the script, find ```void TrackerSetup()```.
5. Above the function, add string as serial number of the tracker. Tracker serial nubmer can be identified in SteamVR Tracker Manager. For example, ```public string Tracker3ID = "LHR-A33C2513";```
6. Inside the for loop in ```void TrackerSetup()```, add 
```ruby
if (SerialNumber == TrackerXID){ #set X to integer 1 to 10.
  if (this.gameObject.name.Contains("X")){
    SetDeviceIndex(i);
    break;
  }
}
```
5. Number X (TrackerXId && this.gameObject.name.Contains("X")) is the same number for tablet user and Tacker prefab. **X should be set in the range from 1 to 9**.

## Contribute
> Base teamplates were created by Hye Sung Moon (04/29/2022)
