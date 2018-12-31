AADx APIs (.net Standard)
=========================
This project is part of the Azure AD authentication model and provides two APIs, each
in a separate project.  Each API is constucted using asp.net Standard 4.61

# version_C
Roles Integration with AAD.  This version implements roles in a hierarchy, so that users 
only need one role per API or _GlobalAdmin_ which is access to all behavior.

## web.config
Build specific Web.config files are in use for this version.
 - **Web.config**  contains AAD registration info for running locally in Visual Studio and uses
the following registrations in AAD:
    - Pastore_Test3_Api_1:  ToDo API
    - Pastore_Test3_Api_2:  Events API
  - **Web.Release.config** contains AAD registration info for deploying to Azure and uses
the following registrations in AAD:
    - Pastore_Test3_Api_1_PROD:  ToDo API
    - Pastore_Test3_Api_2_PROD:  Events API

## Roles
Roles are implemented as a simple hierarchy: 
 - GlobalAdmin
   - ToDoAdmin
     - ToDoApprover
       - ToDoWriter
         - ToDoObserver
   - EventAdmin
     - EventApprover
       - EventWriter
         - EventObserver

## Steps from version_B
Start with version_B and make the following changes

1) This version uses different app registrations 'Pastore_Test3_*'

2) Roles specified in each api application registration manifest

Roles for **Todo Api**

    "appRoles": [{
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "ToDo Observer",
		    "id": "fcac0bdb-e45d-4cfc-9733-fbea156da358",
		    "isEnabled": true,
		    "description": "Observers only have the ability to view todo itesm.",
		    "value": "ToDoObserver"
	    }, {
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "ToDo Writer",
		    "id": "d1c2ade8-98f8-45fd-aa4a-6d06b947c66f",
		    "isEnabled": true,
		    "description": "Writers Have the ability to create todo items.",
		    "value": "ToDoWriter"
	    },{
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "ToDo Approver",
		    "id": "fc803414-3c61-4ebc-a5e5-cd1675c14bbb",
		    "isEnabled": true,
		    "description": "Approvers have the ability to change todo items.",
		    "value": "ToDoApprover"
	    }, {
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "ToDo Admin",
		    "id": "81e10148-16a8-432a-b86d-ef620c3e48ef",
		    "isEnabled": true,
		    "description": "Admins can manage roles and perform all todo actions.",
		    "value": "ToDoAdmin"
	    }, {
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "Global Admin",
		    "id": "92A56C3C-5789-4FD2-98CC-26A9E2E71CA9",
		    "isEnabled": true,
		    "description": "Full control",
		    "value": "GlobalAdmin"
	    }
    ],


Roles for **Events Api**

    "appRoles": [{
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "Event Observer",
		    "id": "EA833757-E831-4D82-8430-BEE269A68703",
		    "isEnabled": true,
		    "description": "Observers only have the ability to view event itesm.",
		    "value": "EventObserver"
	    }, {
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "Event Writer",
		    "id": "161AB80A-655C-410D-894F-6433A299C995",
		    "isEnabled": true,
		    "description": "Writers Have the ability to create event items.",
		    "value": "EventWriter"
	    }, {
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "Event Approver",
		    "id": "A36FB838-B9CE-4160-880F-D2627C70C997",
		    "isEnabled": true,
		    "description": "Approvers have the ability to change event items.",
		    "value": "EventApprover"
	    }, {
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "Event Admin",
		    "id": "1500D22A-A64E-41FA-84F6-C08A999175F4",
		    "isEnabled": true,
		    "description": "Admins can manage roles and perform all event actions.",
		    "value": "EventAdmin"
	    },{
		    "allowedMemberTypes": [
			    "User"
		    ],
		    "displayName": "Global Admin",
		    "id": "92A56C3C-5789-4FD2-98CC-26A9E2E71CA9",
		    "isEnabled": true,
		    "description": "Full control",
		    "value": "GlobalAdmin"
	    }
    ],

3) Add role based authorization to each endpoint
