﻿using System;
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
                SCORIZQ = ToTerm("["),
                SCORDER = ToTerm("]"),
                //relacional
                SMAYOR = ToTerm(">"),
                SMENOR = ToTerm("<"),
                SIGUAL = ToTerm("="),

                SMAYORIGUAL = ToTerm(">="),
                SMENORIGUAL = ToTerm("<="),
                SDIFERENTE = ToTerm("<>"),
                //
                SPYCOMA = ToTerm(";"),
                SCOMA = ToTerm(","),
                SDOSPUNTOS = ToTerm(":"),
                SPUNTO = ToTerm("."),
                SASIGNAR = ToTerm(":="),
                //logicos reservadas
                LAND = ToTerm("and"),
                LOR = ToTerm("or"),
                LNOT = ToTerm("not"),
                //tipos y valores Reservadas
                VTRUE = ToTerm("true"),
                VFALSE = ToTerm("false"),
                VINTEGER = ToTerm("integer"),
                VREAL = ToTerm("real"),
                VBOOLEAN = ToTerm("boolean"),
                VVOID = ToTerm("void"),
                VSTRING = ToTerm("string"),
                //reservadas
                RTO = ToTerm("to"),
                RVAR = ToTerm("var"),
                RTYPE = ToTerm("type"),
                REND = ToTerm("end"),
                RPROGRAM = ToTerm("program"),
                ROBJECT = ToTerm("object"),
                RCONST = ToTerm("const"),
                RARRAY = ToTerm("array"),
                ROF = ToTerm("of"),
                RBEGIN = ToTerm("begin"),
                RIF = ToTerm("if"),
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
                PDeclaracion = new NonTerminal("PDeclaracion"),
                Pprogram = new NonTerminal("Pprogram"),
                Ptype = new NonTerminal("Ptype"),

                //declaracion
                Operacion_relacional = new NonTerminal("Operacion_relacional"),
                Tipo = new NonTerminal("Tipo"),
                Declaraciones = new NonTerminal("Declaraciones"),
                Pids = new NonTerminal("Pids"),
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
                // array
                Parray = new NonTerminal("Parray"),
                Pindice = new NonTerminal("Pindice"),
                // begin
                PBegin = new NonTerminal("PBegin"),
                SMain = new NonTerminal("SMain"),
                //sentencias
                Sentencia = new NonTerminal("Sentencia"),
                Sentencias = new NonTerminal("Sentencias"),
                // lista de sentencias
                PAsignacion = new NonTerminal("PAsignacion"),
                SentenciaIf = new NonTerminal("SentenciaIf"),
                SentenciaSwitch = new NonTerminal("SentenciaSwitch"),
                SentenciaWhile = new NonTerminal("SentenciaWhile"),
                SentenciaFor = new NonTerminal("SentenciaFor"),
                SentenciaRepeat = new NonTerminal("SentenciaRepeat"),
                SentenciasBreak = new NonTerminal("SentenciasBreak"),
                SentenciaContinue = new NonTerminal("SentenciaContinue"),
                //Asignacion
                Asignacionaux = new NonTerminal("AsignacionAux"),
                //Sentencias If
                else_if = new NonTerminal("else_if"),
                elif = new NonTerminal("elif"),
                //Sentencias Switch
                Pcasos = new NonTerminal("Pcasos"),
                Pcaso = new NonTerminal("Pcaso"),
                Caso = new NonTerminal("Caso"),
                //sentencias For
                inicializacion = new NonTerminal("inicializacion")

            ;
            #endregion

            #region Gramatica
            //-----------
            S.Rule = Instrucciones;
            //---------
            Instrucciones.Rule = Instrucciones + Instruccion
                | Instruccion
                | Empty;
            //----------
            Instruccion.Rule = Pprogram
                | PDeclaracion
                | Ptype
                | PBegin
               ;
            //-------
            Pprogram.Rule = RPROGRAM + ID + SPYCOMA;
            //-------
            PDeclaracion.Rule = RCONST + ID + SIGUAL + Operacion_relacional + SPYCOMA
                | RVAR + Pids + SDOSPUNTOS + Tipo + Declaraciones
                | Pids + SDOSPUNTOS + Tipo + Declaraciones
                ;

            Declaraciones.Rule = SIGUAL + Operacion_relacional + SPYCOMA
                | SPYCOMA;

            Pids.Rule = Pids + SCOMA + ID
                | ID;
            //----------------------
            Ptype.Rule = RTYPE + Objeto;

            Objeto.Rule = Decla + Declaraciones2 + REND + SPYCOMA
                        | ID + SIGUAL + RARRAY + Parray + SPYCOMA;

            Declaraciones2.Rule = Declaraciones2 + PDeclaracion
                | PDeclaracion;

            Decla.Rule = ID + SIGUAL + ROBJECT + SPYCOMA;
            //--------------
            Parray.Rule = SCORIZQ + Pindice + SCORDER + ROF + Pindice
                        | ROF + SCORIZQ + Pindice + SCORDER + ROF + Pindice;

            Pindice.Rule = Tipo
                         | Operacion_relacional + SPUNTO + SPUNTO + Operacion_relacional;
            //-----------------------
            Sentencias.Rule = Sentencias + Sentencia
                            | Sentencia
                            | Empty;
            //-----------------
            Sentencia.Rule = PAsignacion
                | SentenciaIf
                | SentenciaSwitch
                | SentenciaWhile
                | SentenciaFor
                | SentenciaRepeat
                | SentenciasBreak
                | SentenciaContinue;

            //-------------
            PAsignacion.Rule = ID + Asignacionaux
                            ;

            Asignacionaux.Rule = SASIGNAR + Operacion_relacional + SPYCOMA
                                | SPUNTO + ID + SASIGNAR + Operacion_relacional + SPYCOMA
                                ;

            //--------------------
            PBegin.Rule = RBEGIN + Sentencias + REND + SMain;

            SMain.Rule = SPYCOMA
                       | SPUNTO;

            //---------------------
            SentenciaIf.Rule = RIF + SPARIZQ + Operacion + SPARDER + RTHEN + Sentencias
                             | RIF + SPARIZQ + Operacion + SPARDER + RTHEN + Sentencias + RELSE + Sentencias
                             | RIF + SPARIZQ + Operacion + SPARDER + RTHEN + Sentencias + else_if
                             | RIF + SPARIZQ + Operacion + SPARDER + RTHEN + Sentencias + else_if + RELSE + Sentencias
                             ;

            else_if.Rule = else_if + elif
                        | elif
                        | Empty
                        ;

            elif.Rule = RIF + SPARIZQ + Operacion + SPARDER + RTHEN + Sentencias;

            //----------------

            SentenciaSwitch.Rule = RCASE + SPARIZQ + Operacion + SPARDER + ROF + Pcasos + REND + SPYCOMA;

            Pcasos.Rule = Pcasos + Pcaso
                        | Pcaso
                        | Empty;

            Pcaso.Rule = Caso + SDOSPUNTOS + Sentencias
                    | Caso + SDOSPUNTOS + RBEGIN + Sentencias + REND + SPYCOMA;

            Caso.Rule = Caso + SCOMA + Operacion
                | Operacion
                | Empty;

            //-------------

            SentenciaWhile.Rule = RWHILE + Operacion + RDO + RBEGIN + Sentencias + REND + SPYCOMA;

            //-----------------------

            SentenciaFor.Rule = RFOR + inicializacion + RTO + Operacion + RDO + Sentencias
                              | RFOR + inicializacion + RTO + Operacion + RDO + RBEGIN + Sentencias + REND + SPYCOMA;

            inicializacion.Rule = ID + SASIGNAR + Operacion_relacional;

            //--------------------------------

            SentenciaRepeat.Rule = RREPEAT + Sentencias + RUNTIL + Operacion + SPYCOMA
                                 | RREPEAT + RBEGIN + Sentencias + REND + SPYCOMA + RUNTIL + Operacion + SPYCOMA;

            //-------------------------------------
            SentenciasBreak.Rule = RBREAK + SPYCOMA;

            SentenciaContinue.Rule = RCONTINUE + SPYCOMA;


            //--------------------------------------

            Operacion.Rule = Operacion + LAND + Operacion
                | Operacion + LOR + Operacion
                | Operacion_relacional;

            //------------
            Operacion_relacional.Rule = Operacion_numerica + SDIFERENTE + Operacion_numerica
                | Operacion_numerica + SMAYOR + Operacion_numerica
                | Operacion_numerica + SMENOR + Operacion_numerica
                | Operacion_numerica + SMAYORIGUAL + Operacion_numerica
                | Operacion_numerica + SMENORIGUAL + Operacion_numerica
                | Operacion_numerica + SIGUAL+ Operacion_numerica
                | Operacion_numerica;
            //--------
            Operacion_numerica.Rule = Operacion_numerica + SMAS + Operacion_numerica
                | Operacion_numerica + SMENOS + Operacion_numerica
                | Operacion_numerica + SDIVISION + Operacion_numerica
                | Operacion_numerica + SMULTIPLICACION + Operacion_numerica
                | Operacion_numerica + SMODULO + Operacion_numerica
                | SPARIZQ + Operacion + SPARDER
                | ID + SPARIZQ + SPARDER
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
                        | REAL
                        | CADENA
                        | VFALSE
                        | VTRUE;


            this.Root = S;
            #endregion
            #region Preferencias
            String[] reservadas = {
                RPROGRAM.Text,
                RVAR.Text,
                RTYPE.Text,
                REND.Text,
                ROBJECT.Text,
                RCONST.Text,
                VTRUE.Text,
                VFALSE.Text,
                VINTEGER.Text,
                VREAL.Text,
                VBOOLEAN.Text,
                VVOID.Text,
                VSTRING.Text,
                LAND.Text,
                LOR.Text,
            };
            MarkReservedWords(reservadas);
            #endregion


        }
    }
}
