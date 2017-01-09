#Teng - Template ENGine with Markdown and minify support

[Sample website](https://servc.eu/p/teng-sample/index.html)

##teng.exe usage:
	-generate -g -gen    Generate output from templates
    -output=<value>      Set the output directory. Default is ./output/
    -dir=<value>         Set the working directory. Default is ./
    -clean               Clear the output directory before generation
    -singlethread        Disable multithreading. Use on single-core machines
    <filename>           Loads values from a project file. Use other values after to overwrite ones in this file.

##Tips:
*	Run Associate.bat as an administrator to register any .teng file with ./teng.exe. CLI current directory must 
	contain teng.exe and all its dependencies
*   When using mixed markdown (type: pagemd) and html (type: page) pages, create empty pages/default.body and 
    pages/default.bodymd to avoid not found errors


##Compile order:
*	For each page:
	*	Load page.use if exists (if not, default.use. If that not exists, fail)
	*	Parse template file, loading page content from {{t page.asdf}} -> \<pagename\>.asdf -> default.asdf where exists
			and {{t incfile.a}} -> all items under incfile group a
	*	Minify output
	*	Copy to \<pagename\>.format in output directory
*	Copy all .project 'copy' values where exists

##fileformats
Optional file. Include this to set custom file formats for each template. The format is `<template name>:<format>`. 
Use `%n` in place of where the page name should go in the format. If `%n` is not present, the line will be rejected and template generation 
will fail. `:nomin` can be appended to each line to prevent the output being minified.

e.g.

    default:%n.htm
    php:%n.php:nomin

If there is no fileformats file, the following contents is assumed:

    default:%n.html

##project.teng
Optional file.

e.g.

	C:\OutputDirectory or relative to working
	Generate?
	Clean?

##incfile - **Not implemented yet.**
Optional file.
Contains links to other content
Append 'nomin' to not automatically minify js/css file
e.g.:

	js.nomin git jmin/jmin.min.js js/jmin.min.js

##templates/main
Main template file. Required file.

e.g.

	<!DOCTYPE html>
	<html>
		<head>
			{{t incfile.js}} <-- not implemented yet
			{{t incfile.css}} <-- not implemented yet
			<title>{{t page.title}}</title>
			{{t page.head}}
		</head>
		<body>
			{{t template.menu}} <-- load the 'menu' template
			{{t page.body}} <-- 'body' is the element relating to the current page
			{{t pagemd.body}} <-- same as above but processes in markdown instead
			<p>Compiled on: {{t t.datetimenow}}</p> <-- possibility for built-in functions
		</body>
	</html>

##templates/menu
Another example template to show inclusion and menu system.

e.g.

	<ul>
		{{t foreach page /.+/}} <-- regex included in foreach loop of all pages
		<li class="{{t p.isactivepage selected}}" href="{{t a.p}}">{{t p.title}}</li> <-- use p.asdf for 
			the <pagename>.asdf of current page in foreach loop, unlike page.asdf for the current page
		{{t endforeach}}
	</ul>

##pages/default.use
The template used for the default page

e.g.

	main

##pages/default.title
The {{t page.title}} object for the default page.

e.g.

	Page Title

##pages/default.body
The {{t page.body}} object for the default page.

e.g.

	<h1>This is the default content</h1>

##pages/index.title
e.g.

	Main Page

##pages/index.body
The body content. Can contain all php/js/css.
e.g.

	<h1>This is the homepage!</h1>
	<a href="{{t a.other}}">Other page</a>
