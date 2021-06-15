# DBDeltaWatcher
A concept for a database based difference tracking system for a passive trigger like system to update dependent data

## More detailed explanation of the idea
So you got those tables of moving data like delivery notes or something else. Or a table that you unfortunately need to join using an or-clause. And at first it was easy and fast to aggregate all those lines but with time you have more and more data and it gets increasingly difficult to write fast statistics queries on that data. 

Your users still want those statistics and they want them really fast. 

So you decide to change the processing structure.  

- In some cases you just have a troubeling table. And the data needs to be represented in a more suitable format, keeping up with the updates of the "source table". An example could be that you decided to use a JSON-column somewhere. But now there are more and more rows in the table and the performance of accessing the data in the JSON is a bottleneck. But you want to keep the base structure because X (you cannot touch it, legacy, you need the flexibility at another stage of the processing). So you need a flat copy of the table.

- In other cases you might have a bigger statistic. Calculating that statistic on the fly has become time consuming. There are a lot of views and functions involved. But the users want to see updates fast. So you need to change to a model where you keep the statistic in a permanent table and you control the changes, like, when a new delivery note is added, you want that the statistic of the associated team is increased by the n hours, which is fast and lean, instead of recalculating the complete thing.

You could use database triggers, but they come at a cost. They are synchronous and they complicate the dependencies in your database. 

At the moment, when the data for the delivery note is inserted into the database the statisic updates are performed. But what if you have several statistics to be updated. The dependencies increase and odd stuff appears. Changing the statistics becomes more difficult. Adding delivery notes becomes slower and slower...

The solution just might be: A lightweight, well defined and passive delta information system which

- stores and documents information about the connected processes, so the information does not get lost in a bigger team or softwarre (documenting)
- is processing all these change/update operations in the background (reducing the load that the user can see)
- collects information about the processing time and warns when a certain thresold is reached, so you can see when the processing duration goes crazy (self-watching)
- ensures, that every change is just processed once
- and being what it is, it helps all people on the project to solve the problem the same way, so you have 1 solution to do this kind of stuff, and not 20 more or less thought-through versions

## What does the solution consist of?

So what do you need? 
- A base table for the documentation of the processes
  - We want to define what is executed, when changes are detected. There could be an executable that needs launching or SQL statements or a stored procedure. This way, besides the base table, we will not need any other configuration.
- Difference tracking tables for each thing you want to surveil (You need information about adds, deletes and updates. Preferably updates provide you with the table row content "before" and "after") 
- Easy callable and usable SQL based commands that allow you to 
  - grab a list of changed elements
  - "commit" the changes after processing (create a log of the latest commit operations for review?)
  - to request an immediate or delayed update for one of the watched tables ("hey, we got another delivery note here..!") 
- A background worker, that politly watches if there is something to do and triggers the needed processes based on time or based on explicit request e.g. from a stored procedure.
- We need a naming convention for all the tables, that separates them from the other data in the database clearly, so we never use track of what needs to be done.
- We want support for multiple background workers, so we can scale up.
- We want connection string and multiple database support, if we can have it, so we can use the system for data transfers between databases. That would be especially awesome!

### base table

The base table needs to contain the following information:
- Source definition (table, process-Id) e.g. ("delivery_notes","dailyStatistic"), ("delivery_notes", "monthlyStatistic") 
- Processing meta data (what shall be executed?)
- IsActive-Flag for easy activation/deactivation
- LastExecutionTime
- LastExecutionRequestTime (detect if background-worker is not doing its job)
- IsUpdateExplicitlyRequested-Flag
- Description (documentation)
- A list of time infos that describe when the update shall be executed when using an update approach based on just time

### tracking table

### operations and life cycle

- a worker requests a new set of changes
- the request for changes generates a new guid, all detected changes are marked with the guid and staged, meaning that their current state is fixed in the staging area. from that point on everything that is in the staging area is not changed anymore by updates on the source table. a staging area is reserved for 4 hours. after that it is either committed or considered unfinished and dropped. maybe the worker has died and this way we get a chance to reprocess the information. within the 4 hours those changes are considered "done" by the change detection algorithm. Changes on staged rows are not returned in new calls which may be performed by other workers that are processing in parallel.
- the worker performs the neccessary work. with the guid he can always get back the consistent state of changes.
- the worker commits his work. that changes the tracking table information to the new state which has been preserved in the staging area. This is important, because the underlying information in the source table might have changed during processing in the source table.

-> the described process can be started in parallel and be repeated again and again
