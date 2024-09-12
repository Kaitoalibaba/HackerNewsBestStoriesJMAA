HOW TO RUN THE APPLICATION
Run the app by cloning the repository, restoring dependencies, and running it via dotnet run, this api works using the swagger or using postman using the following port
http://localhost:<port>/api/HackerNews/best?numberOfStories=n being N the number of stories that want to be loaded

Assumptions made before making this api
-It’s assumed that the Hacker News API does not require authentication
-The application assumes that there is an active internet connection, and the Hacker News API is available. The app does not currently handle extended API downtimes or network failures beyond simple error messages.
-The application fetches the top N stories sequentially (or in parallel) based on user input. Is assumed that HackerNews can handle such requests within reasonable limits.

Enhancements
Due to the time given the app is not properly effienctly able to service a large number of request without risking overloading the hackernews API its recomended to implement Rate Limiting system  to control the number of requests made to the Hacker News API in a specific timeframe to avoid overloading the external API.
Also might be a good idea to consider a load balancer to enhance the system’s ability to handle high traffic to distribute incoming requests across multiple instances of the API.
Instead of fetching the latest top stories on every request, It might create a background service to fetch the top stories at regular intervals (say every 5 minutes), store them in memory or a database, and serve the cached stories to users.
Add unit test and integration tests for the controller and service layer to ensure that the API behaves correctly under different conditions
