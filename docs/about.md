# About
## Motivation
Well, for a long time I wanted to create a platform where I could exercise my kwnolage and condensate my learnings path. I finded out one way to do this creating a simple e-commerce application. A lot of headache, but finally I could organize the project structure. So, no worrie about technologie to solve problems, this is a technology approach to exercise technology thinks, may be this example has not the betters approachs to solve problems, but have the better tecnhologies.

> Ok, I know "Northwind" is a little bit "retro", because who is working with microsoft techonologies before 2000 know what is Northwind means, but for you is young Northwind that's how it was called a Microsoft old sample application a long time ago. Good, in the next scenes you be fell up with more and more about the scope, infrastrucute and a bunch of hipsters technologies.

## Scope
Let's suppose you are developing an e-commerce application for an entirelly new company (amazing right!?). This company have no idea about infrastructure, but knows that their business will grow fast. They care about security, availability and scalabilitty of theirs services. My proposal here is build four microservices modules:
1. **Backoffice Api ** is a dotnet core web api responsable for support back office operations. It's mean that client will be manager the site through this interface.
2. **Backoffice Client** is a React application to consume Backoffice Api.
3. **Commerce Api** is a dotner core web api responsable to serve HTTP requests to commerce application.
4. **Commerce Client** is a Angular application to buyers browse and order producs.
<<ArchitectureImage>>

## Technologies
a first diferents paths. beeing Front-end, Back-end, Queues, Domain-Driven Design, Command-Query-Responsability-Priciple
My proposal here is build a mini Microservice e-commerce entirely builded on ASPNET Core, RabbitMq,
