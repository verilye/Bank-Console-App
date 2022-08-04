Github - 

Use of design patterns: 
• Short summary of the design patterns. 
• Briefly explain the purpose and advantage the design patterns offer to the project. 
• Succinctly discuss your implementation of the design patterns. 
• Identify where in the code, i.e., which file(s) and code within those file(s) are implementing the design patterns. 
• Include any other important points.

    Dependancy injection (IDatabaseController)- 

    Dependancy injection is used to decouple a dependancy from where it is used. I chose to implement
    this design pattern using the Database controller class. As a class with a lot of functionality
    dependancy injection is useful here for testing purposes. 

    Facade (MainMenuController) -
    A Facade simplifies a class with functionality into a more digestible interface, and controls
    the lifecycle of the functionality. The MainMenu controller is a facade for the databasecontroller,
    accessing functionality as necessary in a much more readable way. 


Justify use of and provide advantages of using async/await:

    Async/await is useful for asynchronous programming and I have chosen to use it in instances where
    there is interaction with external services. This is where async programming shines because communicating 
    with external services is not immediate and so there is a need to wait until responses are recieved from
    these services.



