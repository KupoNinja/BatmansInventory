# BatmansInventory

## Goals:
	1. To have confidence that you can complete a project in a reasonable amount of time.
	2. To learn how you find answers to your questions
	3. To help you will see the type of work you'll be doing
  
The user needs to capture the following information to track inventory. This is made up of an inventory item and multiple physical inventory. Think of this like the header record and the physical Inventory is the detail. As an example an inventory item may be a GE refrigerator model X101 and the physical inventory is the actual refrigerator with a serial number of 12345.
Here are the fields to capture:
### Inventory Items
- Id - int
- Part Name - string
- Part Number - string
- Order Lead Time (how long it takes to get inventory after ordering it in days) - int
- Quantity On Hand - int
- Safety Stock (How much quantity you want to always have on hand. Like a re-order point) - int
- Created - datetime
- CreatedBy - string
- LastUpdated - datetime
- LastUpdatedBy - string

### Physical Inventory
- Id - int
- InventoryItemId - int (Id field from Inventory Items)
- Serial Number - string
- Location (where this can be found in the warehouse) - string or int Id to a new table
- Value (dollars) - decimal
- Created - datetime
- CreatedBy - string
- LastUpdated - datetime
- LastUpdatedBy - string

--- 
## UI:
(No login screen required)
- Dashboard (graphical or just summarized text)
	- Inventory under safety stock (quantity available < safety stock)
	- Inventory by location
	- Total value of Inventory
- Inventory List
	- Table view of the inventory with appropriate fields shown I the UI.
	- Clicking on an inventory item pulls up the inventory detail.
	- Add Button above the table to add a new inventory item
- Inventory Detail
	- Form showing the details of the inventory item
	- A table of the physical inventory for this inventory item
		○ Delete button or icon to discard a physical inventory item
	- Add Button above the table lets you add a new physical inventory
		○ An input form shows up with available fields

Populate this with anything you want to track. Make it about movies, cars, something you have to track at your current job, all the cell phones you've had over time. Literally anything.

## We would like you to build this using:
- ASP.NET Core - To build a web api
- Angular 8 - For the front-end
- Sql Server - For the DB
