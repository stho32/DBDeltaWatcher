# DBDeltaWatcher
A concept for a database based difference tracking system for a passive trigger like system to update dependent data

## More detailed explanation of the idea
So you got those tables of moving data like delivery notes or something else. Or a table that you unfortunately need to join using an or-clause. And at first it was easy and fast to aggregate all those lines but with time you have more and more data and it gets increasingly difficult to write fast statistics queries on that data. 

Your users still want thosse statistics and they want them really fast. 

So you decide to change the processing structure.  

- In some cases you just have a troubeling table. And the data needs to be represented in a more suitable format, keeping up with the updates of the "source table". An example could be that you decided to use a JSON-column somewhere. But now there are more and more rows in the table and the performance of accessing the data in the JSON is a bottleneck. But you want to keep the base structure because X (you cannot touch it, legacy, you need the flexibility at another stage of the processing). So you need a flat copy of the table.
