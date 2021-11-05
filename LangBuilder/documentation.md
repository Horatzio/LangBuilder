
- User creates custom grammar
- User creates a symbol table
- ANTLR for generating parser from grammar
- C++ application
    - uses the parser and the symbol table
    - outputs translated code
    - compile application
    - serve to user


Implementation details:
    - Vue for front-end UI
    - ASP.NET MVC back-end
    - C# code generates parser using ANTLR executable
    - C# code generates C++ code for the application
    - Compile C++ code into an executable