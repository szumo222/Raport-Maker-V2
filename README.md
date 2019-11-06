# Raport-Maker-V2

This is an application used for creating reports based on XML trees from DigAIRange by David Systems. It is using external XSLT files, to transfrom all XML files in between desired dates range.

## Configuration file

Application need an file called "config_raport_maker.xml", that will be placed in the same folder as an .exe file (in next time if application will not see this files, it will show a windows to creeate it). The "config_raport_maker.xml" file has to have path to folders with two desired XML trees and path to destination forlder to created reports files.

Example of "config_raport_maker.xml" file:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<root>
<!--An empty folder raport_maker_help is required to application work-->
<!--Paths requie char \ at the end-->
<zaiks>
    <name>Zaiks</name>
	<!--Path to the XML tree nr 1 -->
    <get_folder_szczecin>E:\Example\00000066\</get_folder_szczecin>
	<!--Path to the XML tree nr 2 -->
	<get_folder_x>E:\Example\00000069\</get_folder_x>
	<!--Path to the folders to save reports files for Szczecin-->
    <destination_folder>C:\Users\Example\SavedFiles\</destination_folder>
	<!--Path to the folders to save reports files for Szczecin Ekstra-->
    <destination_folder_ekstra>C:\Users\Example\SavedFiles\</destination_folder_ekstra>
</zaiks>
<stoart>
    <name>Stoart</name>
	<!--Path to the XML tree nr 1 -->
    <get_folder_szczecin>E:\Example\00000066\</get_folder_szczecin>
	<!--Path to the XML tree nr 2 -->
	<get_folder_x>E:\Example\00000069\</get_folder_x>
	<!--Path to the folders to save reports files for Szczecin-->
    <destination_folder>C:\Users\Example\SavedFiles\</destination_folder>
	<!--Path to the folders to save reports files for Szczecin Ekstra-->
    <destination_folder_ekstra>C:\Users\Example\SavedFiles\</destination_folder_ekstra>
</stoart>
</root>
```

## XML trees folder structure

Inside XML tree folder
```
E:\ExamplePath\00000066
```
every XML file has to be in a folder with such a path structure:
```
E:\ExamplePath\00000066\YEAR\MONTH\DAY\Shows
```
It means that the application is looking for all XML files that are stored in Shows folder.

Exmaple of XML file path:
```
E:\ExamplePath\00000066\2019\6\1\Shows\0000006C.XML
```

## Basic usage

User have to choose month date, choose a desired report ("Który raport") and an audition ("Która audycja"), and click "Wykonaj". Audition "Szczecin" will create report from XML tree nr. 1, audition "Szczecin Ekstra" will create report from XML tree nr. 2.

<p align="center">
  <img src="https://i.ibb.co/jgvNXpw/Raport-Maker-V2-Start-Page.png"/>
</p>

When clicked "Wykonaj", application will start creating report.
When it finished there will show up windows with information that report file from desired month has been created.
The file will be in the folder that user configured as destination folder in "config_raport_maker.xml".

### Using application as exe file
If user want to use application as an .exe file all is needed to do is to copy files:
```
Folder: raport_maker_help
config_raport_maker.xml
Raport Maker V2.exe
all XSLT files
```

that are stored in 
```
Raport-Maker-V2/direporter/bin/Debug/
```
to another folder.
