## CornelyXML
### Description
Jet another XML abstraction
### Dependencies
Depends on Ude to detect file encoding

Includes a simple app that shows how to work with CornelyXML

### Hope it will be usefull for someone
This app is based on another work: [automatatheory-xml-parser](https://github.com/gian-te/automatatheory-xml-parser)
(files in the folders [lexer] and [Parser]) with some modifications to met my specific requierements.

## ¿Why another XML abstraction?
I built this app because I have to work with some signed XML's that has some "problems" and the MS XMLDocument allways tries to fix them, so this app does not make changes to the original XML, otherwise the original signature is invalidated.
