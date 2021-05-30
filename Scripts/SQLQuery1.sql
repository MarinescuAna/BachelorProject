use TeamWork_Dev1

select 	AssignedTasks.ListID,Assignments.ListID from AssignedTasks inner join Assignments on Assignments.AssignmentID=AssignedTasks.AssignmentID 

select * from List where ListID='7900fb06-638a-42a2-9bda-78d7fa34503a'
delete from List
select * from AssignedTasks where AssignedTasks.ListID= 'ab0773bf-6318-4f89-a772-622551bcad29'
inner join Assignments on AssignedTasks.AssignmentID=Assignments.AssignmentID 
inner join List on List.ListID=Assignments.ListID where
Assignments.ListID='33234159-0dcd-4f78-9f7f-cf0d29155c9a'

select List.Title,* from AssignedTasks inner join List on List.ListID=AssignedTasks.ListID where List.ListID='da8435ce-42c6-440a-a2cd-63125b192529'

select * from 
Assignments a 
inner join AssignedTasks ast on a.AssignmentID=ast.AssignmentID
inner join List l on l.ListID=a.ListID 
where a.AssignmentID='BDB67740-FC01-44EC-B35D-3F70C29F7974'