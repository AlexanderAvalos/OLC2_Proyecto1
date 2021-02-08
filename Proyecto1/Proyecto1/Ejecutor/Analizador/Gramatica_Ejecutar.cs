using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace Proyecto1.Ejecutor.Analizador
{
    class Gramatica_Ejecutar : Grammar
    {
        public Gramatica_Ejecutar() : base(caseSensitive: false)
        {

            #region Expresiones Regulares
            IdentifierTerminal ID = new IdentifierTerminal("Id");
            StringLiteral CADENA = new StringLiteral("Cadena", "\'");
            NumberLiteral NUMERO = new NumberLiteral("Numero");
            var REAL = new RegexBasedTerminal("Decimal", "[0-9]+'.'+[0-9]+");

            //comentarios
            CommentTerminal comentariolinea = new CommentTerminal("ComenatrioLinea", "//", "\n", "\r\n");
            CommentTerminal comentarioBloque1 = new CommentTerminal("ComenatrioBloque", "{", "}");
            CommentTerminal comentarioBloque2 = new CommentTerminal("ComenatrioBloque", "(*", "*)");
            #endregion

            #region Terminales 
            KeyTerm
                //numericos
                SMAS = ToTerm("+"),
                SMENOS = ToTerm("-"),
                SDIVISION = ToTerm("/"),
                SMULTIPLICACION = ToTerm("*"),
                SMODULO = ToTerm("%"),
                //agrupacion
                SPARIZQ = ToTerm("("),
                SPARDER = ToTerm(")"),
                SLLAIZQ = ToTerm("["),
                SLLADER = ToTerm("]"),
                //relacional
                SMAYOR = ToTerm(">"),
                SMENOR = ToTerm("<"),
                SIGUAL = ToTerm("="),
                SMAYORIGUAL = ToTerm(">="),
                SMENORIGUAL = ToTerm("<="),
                SDIFERENTE = ToTerm("<>"),
                //
                SPUNTO = ToTerm("."),
                SPYCOMA = ToTerm(";"),
                SCOMA = ToTerm(","),
                SDOSPUNTOS = ToTerm(":"),
                SIGUALAR = ToTerm(":="),
                //logicos
                LAND = ToTerm("and"),
                LOR = ToTerm("or"),
                LNOT = ToTerm("not"),
                //tipos y valores
                VTRUE = ToTerm("true"),
                VFALSE = ToTerm("false"),
                VINTEGER = ToTerm("integer"),
                VREAL = ToTerm("real"),
                VBOOLEAN = ToTerm("boolean"),
                VVOID = ToTerm("void"),
                VSTRING = ToTerm("string"),
                //reservadas
                RVAR = ToTerm("var"),
                RTYPE = ToTerm("type"),
                RBEGIN = ToTerm("begin"),
                REND = ToTerm("end"),
                RPROGRAM = ToTerm("program"),
                ROBJECT = ToTerm("object"),
                RARRAY = ToTerm("array"),
                ROF = ToTerm("of"),
                RIF = ToTerm("if"),
                RCONST = ToTerm("const"),
                RTHEN = ToTerm("then"),
                RELSE = ToTerm("else"),
                RCASE = ToTerm("case"),
                RWHILE = ToTerm("while"),
                RDO = ToTerm("do"),
                RREPEAT = ToTerm("repeat"),
                RUNTIL = ToTerm("until"),
                RFOR = ToTerm("for"),
                RBREAK = ToTerm("break"),
                RCONTINUE = ToTerm("continue")
                ;
            #endregion

            #region No terimnales
            NonTerminal
                S = new NonTerminal("S"),
                //instrucciones
                Instrucciones = new NonTerminal("Instrucciones"),
                Instruccion = new NonTerminal("Instruccion"),
                //instruccion
                Declaracion = new NonTerminal("Declaracion"),
                Pprogram = new NonTerminal("Pprogram"),
                Ptype = new NonTerminal("Ptype"),
                Funcion = new NonTerminal("Funcion"),
                //declaracion
                Operacion_relacional = new NonTerminal("Operacion_relacional"),
                Tipo = new NonTerminal("Tipo"),
                Declaraciones = new NonTerminal("Declaraciones"),
                //Ptype
                Objeto = new NonTerminal("Objeto"),
                //objeto 
                Decla = new NonTerminal("Decla"),
                Declaraciones2 = new NonTerminal("Declaraciones2"),
                //operacione_realacional
                Operacion_numerica = new NonTerminal("Operacion_Numerica"),
                //operacion_numerica
                Operacion = new NonTerminal("Operacion"),
                Valor = new NonTerminal("Valor"),
                Valores = new NonTerminal("Valores")
                ;
            #endregion

            #region Gramatica
            //-----------
            S.Rule = Instrucciones;
            S.ErrorRule = SyntaxError + SPYCOMA;
            //---------
            Instrucciones.Rule = Instrucciones + Instruccion
                | Instruccion
                | Empty;
            //----------
            Instruccion.Rule = Pprogram
                | Declaracion
                | Ptype
                | Funcion;
            Instruccion.ErrorRule = SyntaxError + Instruccion;
            //-------
            Pprogram.Rule = RPROGRAM + ID + SPYCOMA;
            //-------
            Declaracion.Rule = RCONST + ID + SIGUAL + Operacion_relacional + SPYCOMA
                |RVAR + SDOSPUNTOS + Tipo + Declaraciones + SPYCOMA;

            Declaraciones.Rule = SIGUAL + Operacion_relacional + SPYCOMA
                | SPYCOMA;
            //----------------------
            Ptype.Rule = RTYPE + Objeto;

            Objeto.Rule = Decla + Declaraciones2 + REND + SPYCOMA;

            Declaraciones2.Rule = Declaraciones2 + SCOMA + Declaracion
                | Declaracion;

            Decla.Rule = ID + SIGUAL + ROBJECT + SPYCOMA;
            //--------------
            Operacion.Rule = Operacion + LAND + Operacion
                | Operacion + LOR + Operacion
                | Operacion_relacional;

            //------------
            Operacion_relacional.Rule = Operacion_numerica + SDIFERENTE + Operacion_numerica
                | Operacion_numerica + SMAYOR + Operacion_numerica
                | Operacion_numerica + SMENOR + Operacion_numerica
                | Operacion_numerica + SMAYORIGUAL + Operacion_numerica
                | Operacion_numerica + SMENORIGUAL + Operacion_numerica
                | Operacion_numerica;
            //--------
            Operacion_numerica.Rule = Operacion_numerica + SMAS + Operacion_numerica
                | Operacion_numerica + SMENOS + Operacion_numerica
                | Operacion_numerica + SDIVISION + Operacion_numerica
                | Operacion_numerica + SMULTIPLICACION + Operacion_numerica
                | SPARIZQ + Operacion + SPARDER
                | ID + SPARIZQ + SPARDER
                | ID + SPARIZQ + Valores + SPARDER
                | Valor;
            //-----------
            Tipo.Rule = VINTEGER
                | VREAL
                | VVOID
                | VBOOLEAN
                | ID
                | ROBJECT
                | VSTRING;
            //--------
            Valor.Rule = NUMERO
                | ID
                | CADENA
                | VFALSE
                | VTRUE;

            #endregion
        }
    }
}
