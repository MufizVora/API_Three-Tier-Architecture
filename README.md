# Introduction:
- Welcome to WEB API PROJECT, an API project built using ASP .NET Core MVC. This project aims to provide a robust and modular solution for managing user accounts and authentication in web applications. With a focus on security, flexibility, and scalability, our API project offers a reliable solution for handling user authentication and authorization tasks.

# Project Overview:
- This project is a web API project developed using ASP .NET Core MVC, following a three-tier architecture. The project aims to provide a comprehensive solution for managing user authentication and authorization in web applications. By abstracting away the complexities of user management, This simplifies the process of authenticating users, authorizing access to protected resources, and managing user accounts.

- The project is organized into three main layers: the presentation layer, the business logic layer (BLL), and the data access layer (DAL). Each layer serves a specific purpose and contributes to the overall functionality and security of the API.

# Technologies Used:
- This project is built using a combination of modern technologies and frameworks to provide a robust and scalable solution for managing user authentication and authorization in web applications. The following technologies are used in the development of the project:

  - ASP .NET Core MVC: ASP .NET Core is an open-source, cross-platform framework for building web applications and APIs. It provides a powerful and flexible platform for developing high-performance, scalable web applications.
  - Entity Framework Core: Entity Framework Core is a lightweight and extensible ORM (Object-Relational Mapping) framework for .NET. It enables developers to work with relational databases using strongly-typed .NET objects, simplifying data access and persistence.
  - AutoMapper: AutoMapper is a popular object-to-object mapping library for .NET. It simplifies the process of mapping between different types of objects, reducing boilerplate code and improving maintainability.
  - Dependency Injection: Dependency Injection is a design pattern used to implement inversion of control in .NET applications. It allows components to be loosely coupled and promotes modular, testable code.
  - C# Programming Language: C# is a modern, object-oriented programming language developed by Microsoft. It is used extensively in .NET development for building web, desktop, and mobile applications.

# Project Structure:
- This project follows a well-organized structure to maintain clarity, separation of concerns, and ease of maintenance. The project is structured into multiple layers, each serving a specific purpose and contributing to the overall functionality and architecture of the API.

  Overview:
  - The project is organized into the following main layers:

    - Presentation Layer: This layer handles incoming HTTP requests and defines the API's interface. It consists of controllers, endpoints, and view models responsible for interacting with clients.
    - Business Logic Layer (BLL): The BLL contains the core logic and rules of the application. It implements authentication and authorization logic, user management functionality, and any other business rules specific to the application's requirements.
    - Data Access Layer (DAL): The DAL is responsible for interacting with the database and managing data persistence. It includes entities, repositories, and a database context that handle CRUD operations on user data.

  Project Structure:
  - The project structure is organized as follows:

    - Controllers/
    - Models/
    - Services/
    - Interfaces/
    - DTOs/
    - Data Access Layer/
      - DbContext/
      - Repositories/
      - Models/
    - Business Logic Layer/
      - Services/
      - Models/
    - Presentation Layer/
      - ViewModels/
      - Views/
      - Static/
    - Utilities/

# Installation:
- Visual Studio Code 2022
- .NET Core SDK
