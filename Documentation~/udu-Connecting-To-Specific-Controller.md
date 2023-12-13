# Only connecting to specific UDU controllers

## Summary

*Note: This documentation is intended to guide users through the process of connecting to one or severals specific UDU controllers using their MAC adress. Your application can connect to a **single** controller at a time.*


## Prerequisites:
   
* The UDU_SDK_package must be already implemented and working.
  
### Retrieving MAC address of desired UDU controllers:

| Controller #  | MAC address |
| ------------- | ------------- |
| 01  | 94:E6:86:C2:78:8E  |
| 02  | C0:49:EF:23:E8:02  |
| 03  | 90:38:0C:B0:7A:3A  |
| 04  | 94:B5:55:A9:F2:EE  |
| 05  | 94:B5:55:A9:EB:12  |
| 06  | 94:B5:55:AA:12:E6  |
| 07  | 94:B5:55:AA:0A:86  |
| 08  | 94:B5:55:AA:04:CE  |
| 09  | 94:E6:86:C0:69:6A  |
| 10  | Out of the office  |
| 11  | 94:B5:55:A9:FC:96  |

### Editing Controller_Manager Prefab:

* Once in possession of the MAC address of controllers you want to be able to connect to, follow these steps:
  
1. In your desired Unity project, open the Scene you are using to connect to the controller, it should be the one containing the `Controller_Manager` prefab.
2. Select the `Controller_Manager` prefab, in the inspector window, locate the `UDU Console Connection` script.
3. Expand the `Device Addresses To Connect To` list, you can now add an entry by clicking the `+` symbol.
4. Create as many entries as needed and paste the MAC addresses of each UDU controllers you want to be able to connect to.
5. Your application will now exclusively connect to pre-registered UDU controllers.

---
