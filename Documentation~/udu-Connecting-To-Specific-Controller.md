# Only connecting to specific UDU controllers

## Summary

*Note: This documentation is intended to guide users through the process of connecting to one or severals specific UDU controllers using their MAC adress.*


## Prerequisites:
   
* The UDU_SDK_package must be already implemented and working.
  
### Retrieving MAC adress of desired UDU controllers:

* HERE PUT A LINK TO THE LISTING OF THE UDU CONTROLLER AND THEIR MAC ADRESS

### Editing Console_Manager Prefab:

* Once in possession of the MAC adress of controllers you want to be able to connect to, follow these steps:
  
1. In your desired Unity project, open the Scene you are using to connect to the controller, it should be the one containing the `Console_Manager` prefab.
2. Select the `Console_Manager` prefab, in the inspector window, locate the `UDU console Connection` script.
3. Expand the `Device Adresses To Connect To` list, you can now add an entry by clicking the `+` symbol.
4. Create as many entries as needed and paste the MAC adresses of each UDU controllers you want to be able to connect to.
5. Your application will now exclusively connect to pre-registered UDU controllers.

---
