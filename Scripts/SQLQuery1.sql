select 
	List.Title
from 
	AssignedTasks 
inner join 
	Assignments 
on 
	Assignments.AssignmentID=AssignedTasks.AssignmentID
inner join 
	List 
on 
	List.ListID=AssignedTasks.ListID

select * from List
