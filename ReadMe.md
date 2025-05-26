TODO: make proper layout for this
Going to keep track of the design patterns used here:
Constructor Injection
Architecture
Dependency Injection => Singleton vs Scoped vs Transient 
Lazy loading vs eager loading 
Repository => EF
DTO for easy data transfer between front & back-end => If a method requires more than three parameters I used a DTO.

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

*26/04*
Realised that I completely messed up the database trying to make it through EF, so I decided to recreate it through SQL instead, but that means that I'm basically starting from scratch again. 
*29/04*
Finished the objects layer, I sorted the classes into folders based on how my DB is structured. 
*04/05*
Created some of the logic layer alongside a bunch of DTO objects, so now Address-, Order-, PostalCode- & ProductService have some CRUD methods. Just need to make the customer one which required the AddressService & PostalCodeService to be made first. Oh and I also had a change with the Address & PostalCode tables in the database, because I mixed up the foreign key between them which means that I actually had then also go change it in the objects. Thankfully I caught that before I made any of those services.
*16/05*
Between now and the last time I remembered to update this file I finished building the customerservice which was the trickiest one. I made a createUserId method which grabs the highest id from the db and increases it by 1. I was looking at SQL triggers for how to concatenate a 'k' char to the insert when saving a customer to the DB but that seemed just a bit too complicated. Then I remembered I could just concatenate the 'k' in the constructor and not have to worry about it anymore. I save the customerId as an nvarchar anyway. I also made that method private, because frankly, it should only be used in the createcustomer method anyway. 
Also, when I was making the customerservice I realised I need to grab the addressId somehow when I make it with addressservice. I didn't think of that beforehand, but the fix was simple enough. CreateAddress now returns the Id instead of being void. When I started working on the CreateCustomer method I realised that the CustomerDTO also required the information from AddressDTO, so now CustomerDTO also has the AddressDTO class inside itself. 
Furthermore, when I created postalcodeservice I made an updatepostalcode method, which was an oopsie on my end. Because I use the actual postalcode as a primary key (they're supposed to be unique anyway). Which means you should NOT be able to update that. If there's an typo in there, it will have to be fixed in the db itself.
*25/05*
Continued work on StockActionService after creating the object & db table earlier this week. I completely forgot about StockAction as an object because my first instinct was to just store stockamount in the product object and then update that value through front-end. But seeing as a stock action object was requested I made one against my better judgement. I understand why you would use it, but I personally wouldn't have done it this way. I was at first a bit confused at not having a way to edit or delete them but I guess they also count as logs so you want to keep track of them.
Am I behind on the schedule I arbitrarly made up in my head? Definitely, but we'll see how much can be done until the deadline.
Also recreated the interface for AddressService after the change from last week. And created the interface for stockActionService. Turns out you can just use CTRL + R + I if the 'extract' interface option doesn't show up on the quick actions part.
*26/05*
I only today realised that the deadline got pushed up to next week, giving me even less time to get this out on time. It's not happening. 
Anyway, unit testing has been kicking my ass. Had to get rid of the constructor in Customer, I just put what it did inside the CreateCustomer method now. 
Also added dummy data through sql. While doing that I realised I fucked up by making address have customerId as a foreign key, while also having a addressId as a foreign key in Customer. I could've done a many to many table, but I went for the easier option of getting rid of the CustomerId in Address. 
While unit testing (or rather, integration testing I suppose) I have ran into multiple issues which have required fixing and me learning how to properly use Async methods.
I also learned that apparently you can't do constructor injection inside a testclass, so I had to use property injection instead.
While making the CustomerServiceTests I realised that I apparently hadn't registered it to my Dependency injection? Got that fixed at the least.
I've been whittling away at logic errors due to my unit testing. The details of which you can find in the commits.
At least I can tell I'm improving as a programmer because I look at my earlier written logic and see ways in which I could've done it much better.
Good to know, a list of strings gets sorted alphabetically, which means that k comes before t, which messed up my logic and my tests. Had to go into the database and change that because annoyingly that's the PK. That was a fucking mess to deal with. 