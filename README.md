# BookCatalogue
BookCatalogue microservice which includes the CRUD operations on books.


In this microservice user can perform CRUD operations and based on the key events the data will be published into RabbitMQ. Further actions will be taken care based on the
message type.

**Exception Handling implemented globally**. Because of this implementation try catch blocks not used inside business logic layer.

**Technologies Used : **<br />
.NET Core(5.0)<br />
EF Core(5.0)<br />
RabbitMQ(6.2.2)<br />
Swagger(SwashBuckle) - 5.6.3<br />

<br />
**Design Patterns Used**<br />
Singleton Design Pattern<br />
Dependency Injection<br/>
Repository Design Pattern<br />

**DB Scripts attached with respective folder**<br/>
Update connection strings in appsettings.json to perform testing.

