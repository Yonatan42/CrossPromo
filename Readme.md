Installation Instructions:

Import CrossPromo.unitypackage.
Access the CrossPromo prefab from within the package's Prefabs folder.
Load the prefab into your app as you would any other prefab.
The following symbol can be added in the player settings to enable logs: CROSSPROMO_LOG_ENABLED
Note that without this symbol, logs/warning/error instigated by the package will not be printed.

The Sample scene under the package's Examples folder may be used as a reference.


Edge cases:
There were many edge cases that had to do with an invalid URL or server error.
There in general I dealt with by logging an error. In the case that a video URL was invalid, I skipped the video and automatically called Next for the next video to take its place.
Other edge cases I considered were:
Should the video be paused when the app opens a browser or app loses focus.
If a video is pause and Next or Previous are called, should the new video start playing or should it wait until Resume is called. 
I decided not to handle the cases above because it seems in some cases that the wording of the exercise did not demand it and in some cases I was told that that it wasn't necessary.