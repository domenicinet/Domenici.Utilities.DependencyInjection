# CSharp.Utilities.DependencyInjection
A collection of classes to manage dependency injection in .NET projects

This library (in its proprietary form) has been used for several months in live projects in the finance sector. 
I am now sharing this work as an open source project because I believe the objects herein are useful in simplifying dependency injection in .NET applications.
Please give it a try - there's a Visual Studio sample solution in folder <b>/samples</b> that really tells it all - and feel free to comment or contribute in any way you deem appropriate. 

## Plugin Factory
The plugin factory allows you to instantiate classes at runtime. Instances are identified by a <i>"key"</i> and reflection is used to create the instances.
PluginFactory receives the list of assembly definitions in the constructor and in method Create it will retrieve and instantiate the correct assembly signature:
```
var factory = new PluginFactory(signaturesJson);
IService service = (IService)factory.Create(key);
```

There is of course a generics version of the plugin factory:
```
var factory = new PluginFactory<IService>(signaturesJson);
IService service = factory.Create(key);
```

Assemblies definitions are defined in a JSON array passed to the constructor:
```
[
	{
		'key' : 'signature1',
		'signature' : 'AssemblyA, Version=1.0.0.0',
		'namespace' : 'AssemplyA.SomeClass'
	},
	{
		'key' : 'signature2', 
		'signature' : 'AssemblyB, Version=1.0.0.0, Culture=neutral',
		'namespace' : 'AssemplyB.SomeClass'
	},
	{
		'key' : 'signature3', 
		'signature' : 'AssemblyC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
		'namespace' : 'AssemplyC.SomeClass'
	}
] 
```
In the JSON, for each signature you'll define the following:
-	<i>'key'</i> is the identifier of the signature.
-	<i>'signature'</i> is the assembly description.
-	<i>'namespace'</i> is the full name of the object that will be instantiated.

For instance, if I needed to instantiate <i>String</i> and <i>Int32</i> then the signatures would be defined as follows:
```
[
	{
		'key' : 'String',
		'signature' : 'mscorlib, Version=4.0.0.0',
		'namespace' : 'System.String'
	},
	{
		'key' : 'Int32', 
		'signature' : 'mscorlib, Version=4.0.0.0',
		'namespace' : 'System.Int32'
	}
] 
```

Here's a proper example of how to use the <i>PluginFactory</i>:
```
string signatures = @"[
    {
    'key'       : 'Service1',
    'signature' : 'Service1, Version=1.0.0.0',
    'namespace' : 'Service1.Service'
    },
    {
    'key'       : 'Service2',
    'signature' : 'Service2, Version=1.0.0.0',
    'namespace' : 'Service2.Service'
    }
]";

IService service1 = null;

using (var factory = new PluginFactory<IService>(signatures))
{
	service1 = factory.Create("Service1");
}

service1.DoSomething();
```

## Conclusions
I believe this library to be helpful when inplementing dependency injection, especially when you don't know the implementation that will be used at runtime. 

I welcome you to try this library out and to contribute to it in any way you feel comfortable with: a comment, a suggestion, a code patch or a whole tool to make it better! 

Thank You

Alex Domenici

