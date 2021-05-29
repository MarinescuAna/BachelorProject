select 	AssignedTasks.ListID,Assignments.ListID from AssignedTasks inner join Assignments on Assignments.AssignmentID=AssignedTasks.AssignmentID 

select * from Assignments where ListID='33234159-0dcd-4f78-9f7f-cf0d29155c9a'

select * from AssignedTasks  where ListID='33234159-0dcd-4f78-9f7f-cf0d29155c9a'

select List.Title,* from AssignedTasks inner join List on List.ListID=AssignedTasks.ListID where List.ListID='da8435ce-42c6-440a-a2cd-63125b192529'
