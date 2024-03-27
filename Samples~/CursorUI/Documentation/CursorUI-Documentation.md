# UDU_SDK_package

* This document provides detailed information on how to use the cursor UI feature.
---

## Cursor UI Sample Scene

* To use the cursor UI feature, simply place the 'CursorCanvas' prefab from the prefab folder and place it into your scene.

* The cursor feature works as follow:
	* Pressing the Squeeze button will make a cursor appear at the center of the screen.
	* While pressing the Squeeze button you can now move the console around to move the cursor, and use the controller trigger button to interact with Button components.
	* The cursor is limited to screen space
	* Releasing Squeeze will make the cursor disappear
	
* Known issues:
	* When pressing trigger to interact with your Button components, it is possible for the text of your button to interfere with the cursor. To solve this, untick the 'Raycast Target' parameter in the text component of your button's text.
	


---
