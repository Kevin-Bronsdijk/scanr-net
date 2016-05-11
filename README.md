NET library for the ScanR REST API 
=============

This library interacts with the ScanR REST API allowing you to utilize ScanR's features using a .NET interface. 

* [Getting Started](#getting-started)
* [How To Use](#how-to-use)
* [Authentication](#authentication)
* [Direct upload images](#direct-upload-images)
* [Direct upload Pdfs](#direct-upload-pdfs)
* [Public url images](#public-url-images)
* [Public url Pdfs](#public-url-pdfs)

## Getting Started

First you need to sign up for the [ScanR API](https://scanr.xyz/) and obtain your unique **API Token**. Once you have set up your account, you can start using ScanR.

## How to use

You can convert/can your documents by providing the URL of the document you want to convert or upload the image directly instead. Just keep in mind that the image URL must be accessible for ScanR. 

## Authentication

The first step is to authenticate to ScanR API by providing your unique AAPI Token while creating a new connection.

```C#
 var connection = ScanRConnection.Create("token");
```

## Direct upload images
ScanR allows you to easily upload your images as can be seen within the example below:

**File path** (Byte array also supported)

```C#
var client = new ScanRClient(connection);

var response = await client.Scan(
		"document-location-on-disk.png",
                Language.English
                );

if (response.Success)
    var text = response.Body.Text
```

## Direct upload Pfds
Pdfs can have multiple results which is based on the amount of pages included within the document. Therefore, the call returns a different result as shown within the examples below:

**File path** (Byte array also supported)

```C#
var client = new ScanRClient(connection);

var response = await client.ScanPdf(
		"document-location-on-disk.pdf",
                Language.English
                );

if (response.Success)
    var text = response.Body.Text // Array of items for each page
```

## Public url
If the resource is already available on the web, it's not necessary to upload the document. Just add the public URL as included within this sample. 


**Public Url Images** 

```C#
var client = new ScanRClient(connection);

var response = await client.Scan(
		new Uri("http://blah.blah/image.png")
                Language.English
                );

if (response.Success)
    var text = response.Body.Text
```

**Public Url Pdfs** 

```C#
var client = new ScanRClient(connection);

var response = await client.ScanPdf(
		new Uri("http://blah.blah/image.pfs")
                Language.English
                );

if (response.Success)
    var text = response.Body.Text // Array of items for each page
```

## LICENSE - MIT

Copyright (c) 2016 Kevin Bronsdijk - http://devslice.net/

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
