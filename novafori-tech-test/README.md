# Aaron Buckley's code test


- How long did you spend on your solution?
I spend roughly two to three hours on the backend, and approximately the same amount of time on the front end.  

- How do you build and run your solution?
You should first run `npm ci` in the `/ClientApp` folder in the main project. Then you can either use the `dotnet cli` tool and run `dotnet run` inside the project directory, or start the project in rider/visual studio however you you normally would.

- What technical and functional assumptions did you make when implementing your solution?
From a backend perspective i assumed that an MSSQL database would be used, alongside Entity Framework. Leading me to follow the repository pattern to create a layer between the database and the business logic.
I also assumed the there would be a layer of authentication in place, something like Identity Server, where a user id could be stored in the cookie. If this was in place there would not have been a need for the user to be stubbed out in the repository. It's also likely the repository would not have been needed and the business logic could have directly to Identity Server.
From a frontend perspective, i used custom react hooks to create a shared context for the current user and their tasks. I assumed there might be more pages in project at some point and those two context would persist across the project.
I also assumed the css would need to be expanded upon, so i followed BEM naming to reduce class name conflict.

- Briefly explain your technical design and why do you think is the best approach to this problem.
From the backend perspective i followed a very simple MVC pattern, following the SOLID principals each class has a single responsibility. Repositories return data from the "database", Business logic classes are responsible for the logic of a single "slice" of logic (in this case tasks), and controllers handle the connection between the request/response of the serve and the logic of the system.
I used Interfaces between each layer in order to follow the principal of "dependency inversion", in other words it allowed me to create an abstraction between the APIs of layer and the implementation, facilitating my unit testing. I also tended toward to use of objects, where sensible, between layers in order to create extensibility.

On the frontend, I created use react hooks for state management and created components per page and split out other components where it seems sensible. in order to manage the state the would be shared across the whole application i use the `createContext` and `userContext` hooks, alongside the `useState` hook. This allowed me to have a single place to handle the tasks and current user, follow best practices.
React Router was used to manage navigation between pages and the proxy that comes bundled when you create a new solution in rider was used to handle communication between the client and the APIs.

- If you were unable to complete any user stories, outline why and how would you have liked to implement them.
N/A

- If you have used AI chatbot tooling anywhere, where was it used and how did  it help?
I didn't use a chatbot, but i did use tabnine. I used it to stub out the test cases and some of the contents of the test, as well for stubbing out my interfaces and for more advanced autocomplete functionality.
I was the free verison so you only get one line of output at a time, but it did speed up my workflow meaning that I could "tab out" a lot of boilerplate code. 
