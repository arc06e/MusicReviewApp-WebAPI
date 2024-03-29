# MusicReviewApp-WebAPI

## Description
Welcome to my Web API for music reviews! I built this Web API with ASP.NET Core in .NET 6 in order to better understand how Web APIs operate and the principles of RESTful 
web services. As a lifelong avid fan of music, I thought it would be fun to create a Web API centered around reviewing music albums. 

## Current Features
* Seeds database with sample data to demonstrate app's key features.
* CRUD functionality:
  * Allows users to create, read, update, and delete entities such as artists, albums, genres, countries, reviews, and reviewers.
* RESTful API service:
  * Utilizes HTTP verbs: GET, POST, PUT, DELETE.
  * Returns appropriate HTTP status codes.
  * Accepts requests for a resource and returns response in JSON.
* Repository Design Pattern:
  * Separates the database access code from the controller action methods in order to implement more loosely-coupled code and reduce code duplication. 

## Set-Up Guide
1. Clone project in local directory:<br/>
``` gh repo clone arc06e/MusicReviewApp-WebAPI ```
2. Create local database and add connection string to DefaultConnection in appsettings.json

![appsettingsConnectionString](https://user-images.githubusercontent.com/91097715/195206516-5327b569-4f5f-4192-9bb9-3a5ee9f2aaf7.jpg)

3. Run the following command in your directory root folder: ```dotnet run seeddata```


## Intended Improvements
I would like to incorporate ASP.NET Core's Identity in order to make my web API more secure by developing user login/registration features to 
ensure that only approved users can create and modify db data. I would also like to design a UI that would allow users to create and maintain their music reviews in a way 
that's easy to navigate and understand.
