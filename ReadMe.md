TODO: make proper layout for this
Going to keep track of the design patterns used here:
Constructor Injection
Architecture
Dependency Injection => Singleton vs Scoped vs Transient 
Lazy loading vs eager loading 
Repository => EF
DTO for easy data transfer between front & back-end

*19/03*: 
Creation of the project
*20/03*: 
normalisation
*21/03**: 
Creating the different objects, put them in different folders because it was annoying to find stuff when they were just scattered in the project. 
		  Also created this README file
		  I originally was going to put customer details, adress & customer ID together but that just seemed kind of messy, after asking Yoni for feedback he demonstrated the usefulness of doing it like this. 
		  Not only prevents this from having duplicate data in the database, it also makes it simple to remove userdata in case it gets requested. 
		  This way I can just get a customerID and remove the contents of their respective customerdetails. 
		  Same story goes for the userID and the Userdetails. 
		  In order to prevent having to save a postal code multiple times, a postal code object is created. 
		  Not entirely sure whether I have to save the password as an int or a string because we haven't seen how C# does hashing yet, for now it is just saved as a string but this will likely change later.
		  Another thing, I will be making the database code-first, I trust entityframework more than I do myself in the creation of a database.
		  Successfully created the database through EF
*23/03*

Slight oversight, forgot to make classes that combine the Details & id class. I kept things simple and just combined the two classes into a bigger class, with the classes being instantiated in the constructor and then tied to properties. Kind of similar to property injection really. 

*24/03*
Finished the first service, wanted to make CustomerService but realised I'd have to make address & postalcode services first. Didn't mention this yesterday, but I made a customer DTO to pass along, seeing as the front-end doesn't need to bother with ID generation. Unit testing will show if this works. 
