How to make a quest for the Cyber game Cafe Quest system.

1. All Quests\Missions are comprised of a set of objectives which must be compleated by the player. 

2. Objectives (class name -MissionObjective) are comprised of several key variables:
   - Event Type
   - Event Tag 
   - Next Objective To Retrieve
   - Start Value
   - Finish Value
   - currentValue
   - deltaProgressValue
   
These Values:
   - Start Value
   - Finish Value
   - currentValue
   - deltaProgressValue
   
Are used to track the state of an objective. Such as if an action has occured or the number of items collected. 
The deltaProgressValue is the change in the current compleation of the objective when the said event occurs. 

When an in game event occurs such as entering an area, a function call can be made to the players quest system in order to
update the objectives. This is what thew event type and event tag fields are for. These are used to identify the specific event.

When an objective update is called the deltaProgressValue is added to the currentValue variable.

An objective is registered as compleated if the start value is equal to the finish value.

Once all objectives have been compleated the system registeres the quest as complete. 

To create an objective you simply need to copy the objective template and set the event type and tag.

Event type can have one of two values:
There are two types of event:
- OBJ_EVENT - In game actions/events 
- OBJ_ITEM  - The Pickup of any item. 

The event tag is simply a string to identify a perticular objective and can be set to any string value.

In the case of an in-game event if you wish to update the system all you need to do is make a call to this function when the in game event occurs:

******  public void updateMissionState(MissionObjectiveTypes missionType, string missionTag) ******

This will then update the quest system accordingly. 


Once you have created your objectives you simply then place them in the Mission objectives list of the quest your creating in your disired order of compleation.

Once you have done this simply set the varible noOfStartingObjectives to set how many of the objectives you initialy want to be active.

Next Objective To Retrieve is simply the number of objectives that should be loaded upon the compleation of the set of active objectives. 

Hope this Helps, Just message me if you need any more assistance.

Nathan Butt
