Going to keep track of the design patterns used here:
Constructor Injection
Architecture
Dependency Injection => Singleton vs Scoped vs Transient 
Lazy loading vs eager loading 
Repository => EF
DTO for easy data transfer => If a method requires more than three parameters I used a DTO.
ViewModels
Do enums count as a design pattern?

GET STARTED: 
1. Import the database onto your computer
   if you don't know how to import a backpac file, check this website https://learn.microsoft.com/en-us/sql/tools/sql-database-projects/concepts/data-tier-applications/import-bacpac-file-create-new-database?view=sql-server-ver17&tabs=import-wizard
2. Go to Soup.OrderSystem.Data, go to the OrderContext file, in there, go to the OnConfiguring method and change DatabaseKey.database key to your own database key.
3. run the project and pray it works 

Some extra information: 
- Do NOT delete the customer with Id "k1", this is the id that is used by users (admins) to perform certain functions in the program.
- If you delete every admin, you will have to insert them into the database, you cannot create an admin through UI. 
- There are two admins currently saved in the database, one of them has the following credentials 

Credentials 
First name: Johnny
Last name : Tester
Password: 123Test







KNOWN ISSUES: 
The unit tests sometimes run in the wrong order when you run all of them, they worked perfectly fine individually though(at least when I checked last time)
The unit tests & api might be broken on some methods.  

Diary/Documentation (You can use this to see when I worked on the program): 
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

*30/05*
A bunch of changes because I'm bad at committing when I'm the only one working. 
Uhhh, bunch of bugfixes due to Unit testing, created AddressServiceTest. CustomerTest, PostalTest, UserTest. Tests work but somehow they don't go off in the correct order when the entire class is ran. Created another Xunit project to see if it was an MsTest issue. It was not. 
Got rid of the DTO's to see if it was an object problem, did not fix it. Changed the services from async to non-async to see if it would fix the problem, did not fix it. Might make async copies of the services after testing anyway.
Also checked to see if it was an issue of having multiple copies of EF open, so every Ef method is now put inside a using(), this way it will immediately shut after it performed its task. Was also not the fix. I wanted to see if using the same instance of the service across multiple tests was the issue. Was also not the fix. I also couldn't do constructor injection in the MsTest project classes so I swapped to property injection instead.

*31/05*
finished unit testing, will swap back to DTO usage if I have time. I also still want to go back to making these methods async because I'm fairly certain they would run just fine.
Enums were being annoying but I got it working, added an orderdetails PK to the orderdetails table because it got upset that I'm using the same values multiple times for pk.
Still have not fixed the tests just doing the delete test right after the create but the tests run individually. 
Also because of unit testing, I reimplemented the OrderDTO because I needed multiple values, like CustomerId from it. This prevents me from having to remember like 7 different parameters for CreateOrder.
While trying to fix the enum I also told EF that it should convert the property into an int if it didn't do that before. I'm pretty sure it did already but it doesn't hurt to type it explicitly. I also casted the switch case in the UpdateOrderStatus to ints just in case that was the problem (it wasn't but I'm scared to touch it again because it might just break on me again).
Also, turns out that data doesn't get updated if you don't do a context.update on them, crazy stuff really.
Anyway, I'm not getting this done in time between the group project and internship, but we'll just do our best and honestly that's all you can really ask of me. 

*02/06*
Well I created the UI project and then went back to logic and unit testing because task switching is hard. I took copies of AddressService & CustomerService and made it so that they are Async and use DTO's instead. I also created the interfaces for them and added them to the DI. Then I tested them in the unit tests using the same tests and they seem to work without hitch so there's that. Still can't seem to fix the problem of making them run in the correct order, but they work individually.
Are you supposed to put comments in unit tests to explain what they do? Probably right? I'll get on that. 
*03/06*
The passage of time scares me, note to self: it seems like I hadn't made an StockActionDTO when I made the StockAction.Except for the fact that I'm blind and realised I made it already right after typing the previous sentence. Uhh, I finished the API controllers and it boots. So yippee on that one at the least. I also created an MVC project but I need to look at previous projects again to see how that shit worked again. 
Oh right I also did some more testing to make sure that all of it worked properly in unit testing if I swapped to the async methods. Stuff seemed to work properly so that's good. Once more I'm scared to dig too deep in case it breaks even more. But my methods work so it's an Xunit problem if that happens.
Is it overkill to leave the normal services in the program when I'm not even using them? Probably but there's no way to access them so they're basically gathering dust (some are probably still tied to unit tests though but that's beside the point). They're also not as refined(?) as the async methods, they don't make use of DTO's and some even have logic errors in them so yeah definitely not using them. 
Also while I remember, some of the names I gave in this project are frankly really bad, like the ones for address. I don't know why I struggled so hard to give it a proper name but I'm not touching that anymore. I'm aware it's bad. Same with the OrderProducts table in the database. I can see how that would be confusing. Orderdetails isn't an exactly clear name either but at least I give proper explanation of that one in OrderService.
It annoys me how much time I spend trying to remember how to do something, but I can't really feel bad about when I think about how much bloody stuff I've learnt/saw these last few months. I just wish I did a better job of archiving projects. It's annoying have to trudge through so many projects with ambiguous names on Github. But at least I uploaded them to github,usually with comments on the stuff introduced.

*07/06*
Thursday really took the wind out my sails having to present that miserable pile of garbage. Went home and took a nap to make myself feel better, it hardly worked. It feels like my anxiety spikes whenever I'm trying to learn something and it doesn't come to me immediately. I hate feeling like that because it means that trying to remember anything about mvc made me feel miserable. Not great. Thankfully, we've made progress. I made three controllers and some crud views. Also fixed an annoying namespace issue. I am not using my api for my UI, instead I will just call upon the repository. As for why I did it this way, all of my stuff is async, so it should be able to run multithreaded & still receive inputs from the api. I guess the api is technically just collecting dust for now, but it could be used for third party applications. 
After thursday I also feel like I might've misunderstood how you make use of DTO's, and have been shooting myself in the foot by using them. If I had known what I know now back when I made the logic, I'd immediately have written the methods to return dto's instead of actual objects. Because now my methods take dto's and return objects instead. It's not the end of the world, albeit slightly annoying having to check what inputs are required for a method, because you just see "oh this address method requires an addressDTO", like no shit it does. Thankfully, the user/customer doesn't have to worry about that, the required fields will be provided. 
Speaking of input fields, I'm surprised how easy validation is through asp & bootstrap so far. The biggest hurdle has been my anxiety and my own stupidity.
Also, because this will keep bothering me if I don't write it down, I disagree with the statement that what I make here is disconnected from me. I'm not an ai that spews out shitty code, I'm still a person and I have bad days and good days. Unfortunately, it has been mostly bad days since the start of this year. There's an argument to be made on separating personal and work life, but even then there's only so much I can do before I shut down entirely and burn out.
Oh and before I forget, I asked yoni about the db key seeing as it's still hosted on his server, and he suggested that I just export the db structure from ssms. So I'm going to put a reminder to myself to get that done.
I think that it's pretty certain that I won't get to implementing security, but I'm okay with my progress on UI so far.
Oh there's still no comments on UnitTests, but I consider that to be pretty low priority for now.
Oh one final thing, I finally got around to using the .select method from linq, which I thought was pretty cool, I took a list of one object and turned it into a list of another object.

*08/06*
Awful day, I spent most of it just fumbling with html, and being stumped on how to even make a shopping cart, it was then I realised "oh wait, I need a login for the shopping cart" because my shopping cart is me creating an order, which requires a customerId. Which means I need to find a way to get Identity working with my current user. I looked at the doc from moodle, but I'm clearly doing something wrong because it's giving an error on the override. I'm glad I at least managed to get mvc working with some bootstrap slapped onto that. 
*9-11/06*
Read github commits for work done these last few days. But apparently I didn't even NEED UNIT TESTS??? Fuck me. Anyway it was probably a good idea to have those anyway seeing as they caught a bunch of logic errors. Speaking of logic errors, not me spending 20 minutes figuring out why my method doesn't work, only to realise I put the parameters in the wrong order. Good job me. 
Ui is coming along, it's ugly for now and I'll probably won't have time to fix it anyway. Don't know if I'll get dashboard done, shouldn't be too hard with all the pieces I've already built. On that note, my Controllers are a fucking MESS. If I had more time I'd definitely would have made a utility class for UI alone. ALSO, MY DTO'S WERE WASTED(?) because I'm using Models anyway. Well they at least are somewhat functional because the models inherit from them. Once again, bad(?) name on OrderProductModel, definitely too long and still kind of vague.
Now that I'm using my own logic more, man was it a mistake to make everything take DTO'S. I constantly have to go back to my own logic to check what I actually need. Not going to do that again next time. 
I've also discovered that my most productive hours are apparently from 23:00 to 04:00. Which definitely isn't healthy but I once again fit the stereotype I guess.
OH I still need to put comments on all my controller methods as well.
And do the CRUD for Orders, thankfully that shouldn't take too long anyway.

*11/06*
Final update for this project (at least I hope)
Couldn't get the Crud or Dashboard done, because I underestimated how much time goes into a powerpoint. Oh well. I added all the things I didn't get done to a slide on the powerpoint, all in all not too bad.
For anyone reading this I'll add the unfinished items here as well: 

Api: 
- missing one or two methods that I had to add after making the UI (which is poor planning on my end I will admit)
- There's no login on API yet
- I haven't tested the API as much as I would've liked to. I don't know if the input validation on that thing is good enough yet.

UI:
- Dashboard is missing
- UI just doesn't look great in general, needs css to make it look not like this. 
- There's no check out for the shopping cart yet
- Admin can't update any orders yet, I've made the code for deleting an order but something was breaking while I was testing it out so I put it in comment for now.
- I didn't test create order in Admin so I don't know if that one actually works. 
- There's no way to create an Admin if you delete all of them. 
- Some admin functions will break if you delete the admin customer with id "k1".
- You can only delete stock actions by delete an order or remove the item from your cart.
- Admins can reserve stock actions when creating a stock action.

All in all, I'm surprised by how much work I still got done in the little time I had remaining. I got a mostly functional UI and figured out how to do the login system successfully. Figured out how to do the shopping cart as well!. 
I'm aware that there's a bunch of code that requires cleaning up, but that would take time that I do not have at the moment. Same goes for the unit tests, I'm pretty sure at least some of them will break. Same for api.
Just need to add boot instructions & export the database along with the powerpoint and we're good.