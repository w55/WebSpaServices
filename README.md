# WebSpaServices

<b>SPA</b> ASP.NET <b>Web API 2</b> application using frameworks: <b>Knockout</b>, <b>EntityFramewok</b>, <b>Ninject</b> and <b>Automapper</b> with <b>Unit Tests</b> project included as well.
<hr>
Some key code snippets only
<br>
This <b>SPA</b> application uses <b>Knockout</b> on frontend and <b>Web API 2</b> on backend of nain project.
<br>
Note than filtering execute at back-end side of this SPA-application (as wished by customer).
<br>
For <b>Unit</b> testing solution contains additional <b>Test</b> project as well.
<hr>

Take a look at code for <b>SPA</b> project: 

<b>App_Start</b> folder: 
<ul>
	<li>NinjectWeb Common settings: <code data-href="App_Start/NinjectWebCommon.cs">NinjectWebCommon.cs</code>.</li>
</ul>

<b>Controllers</b> folder:  
<ul>
  <li>Main Animals Controller: <code data-href="Controllers/AnimalsController.cs">AnimalsController.cs</code>,</li>
  <li>Default Home Controller: <code data-href="Controllers/HomeController.cs">HomeController.cs</code>,</li>
  <li>Helper Kinds Controller: <code data-href="Controllers/KindsController.cs">KindsController.cs</code>,</li>
  <li>Helper Locations Controller: <code data-href="Controllers/LocationsController.cs">LocationsController.cs</code>,</li>
  <li>Helper Regions Controller: <code data-href="Controllers/RegionsController.cs">RegionsController.cs</code>,</li>
  <li>Helper Skins Controller: <code data-href="Controllers/SkinsController.cs">SkinsController.cs</code>.</li>  
</ul>

<b>Models</b> folder (data models and interfaces): 
<ul>
  <li><code data-href="Models/Animal.cs">Animal.cs</code>,</li>
  <li><code data-href="Models/AnimalsContext.cs">AnimalsContext.cs</code>,</li>
  <li><code data-href="Models/AnimalsDbInitializer.cs">AnimalsDbInitializer.cs</code>,</li>
  <li><code data-href="Models/AnimalRepository.cs">AnimalRepository.cs</code>,</li>
  <li><code data-href="Models/IRepository.cs">IRepository.cs</code>,</li>
  <li><code data-href="Models/IUnitOfWork.cs">IUnitOfWork.cs</code>,</li>  
  <li><code data-href="Models/UnitOfWork.cs">UnitOfWork.cs</code>.</li>
</ul>

<b>Utils</b> folder: 
<ul>
	<li>Resolver for Ninject: <code data-href="Utils/NinjectDependencyResolver.cs">NinjectDependencyResolver.cs</code>.</li>
</ul>

<b>Views</b> folder (only single view): 
<ul>
  <li>Main view for <b>SPA</b> front-end <code data-href="Views/Index.cshtml">Index.cshtml</code>.</li>
</ul>
<hr>

Take a look at code for <b>Test</b> project: 

<b>Controllers</b> folder:  
<ul>
  <li>Tests for Animals Controller: <code data-href="Tests/Controllers/AnimalsControllerTest.cs">AnimalsControllerTest.cs</code>,</li>
  <li>Tests for Skins Controller: <code data-href="Tests/Controllers/SkinsControllerTest.cs">SkinsControllerTest.cs</code>.</li>
</ul>

<p>Here are some screenshots:</p>
<p>
<b>SPA</b> main window:
<hr>
<img width="600" src="Screenshots/index.png" alt="main view" />
<hr>
