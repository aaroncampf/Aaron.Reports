# Simple-WPF-Reports
A simple WPF report generation tool


Currently used at AJP Northwest and Brentwood Corp

# Report Generation
All reports are created using Aaron.Report.Basic.
To add content to the report use Add items to the report's Section property or insert custom XAML in the CustomXAML property.

##Report Features
A report starts out as a Basic

### Save As PDF
To save the report as a PDF use the method AsPDF.
AsPDF: Creates a PDF and saves it to a temporary file and returns that files name.

### Show
Shows the document inside a DocumentViewer.

### Print
Prints this document automatically or with a PrintDialog


## Sections
Sections are currently all located in the Sections namespace.

### Base
The base class for all sections.

### Basic
A section of a document for displaying text or custom XAML.

### List
A section of a document that is used to displaying a bulleted list.

### Pictures
A section of a document that is used to displaying a picture.

### Table
A section of a document that is used to displaying a Table.
To add columns use Table.Table.Columns.Add(...).
To add rows use the extension method Table.Table.AddRow(...) from Aaron.Reports.

