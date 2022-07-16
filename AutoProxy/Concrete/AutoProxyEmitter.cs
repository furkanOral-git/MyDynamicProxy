using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using AutoProxy.Concrete;

namespace AutoProxy.Concrete
{
    internal class AutoProxyEmitter
    {
        private static Dictionary<string, OpCode> _opCode;
        
        public static void EmitMethod(MethodBuilder proxyMethod,FieldBuilder field,MethodInfo actualMethod,MethodInfo MethodHandler)
        {
            int argsCount = actualMethod.GetParameters().ToArray().Length;
            var mil = proxyMethod.GetILGenerator();
            mil.Emit(_opCode["Nop"]);
            mil.Emit(_opCode["LdArg_0"]);
            mil.Emit(_opCode["Ldfld"], field);
            mil.Emit(_opCode[$"Ldc_i4_{argsCount}"]);
            mil.Emit(_opCode["NewArr"], typeof(Object));

            //Emit parameter value at runtime 
            if (argsCount > 0)
            {
                for (int i = 0; i < argsCount; i++)
                {
                    mil.Emit(_opCode["Dup"]);
                    mil.Emit(_opCode[$"Ldc_i4_{i}"]);
                    mil.Emit(_opCode[$"LdArg_{i + 1}"]);
                    mil.Emit(_opCode["StelemRef"]);
                }
            }
            mil.Emit(_opCode["CallVirt"], MethodHandler);
            mil.Emit(_opCode["Nop"]);
            mil.Emit(_opCode["Ret"]);
        }
        public static void EmitConstructor(ConstructorBuilder constructor, FieldBuilder field)
        {
            InitOpCodes();
            var cil = constructor.GetILGenerator();
            cil.Emit(_opCode["LdArg_0"]);

            //Every class must call to type of Object's constructor method
            var superObj = typeof(Object).GetConstructor(Type.EmptyTypes);
            cil.Emit(_opCode["Call"], superObj);
            cil.Emit(_opCode["Nop"]);
            cil.Emit(_opCode["Nop"]);
            cil.Emit(_opCode["LdArg_0"]);
            cil.Emit(_opCode["LdArg_1"]);
            cil.Emit(_opCode["Stfld"],field);
            cil.Emit(_opCode["Ret"]);
        }
        private static void InitOpCodes()
        {
            if (_opCode == null)
            {
                _opCode = new Dictionary<string, OpCode>();
                _opCode.Add("LdArg_0", OpCodes.Ldarg_0);
                _opCode.Add("LdArg_1", OpCodes.Ldarg_1);
                _opCode.Add("LdArg_2", OpCodes.Ldarg_2);
                _opCode.Add("LdArg_3", OpCodes.Ldarg_3);
                _opCode.Add("Ldc_i4_0", OpCodes.Ldc_I4_0);
                _opCode.Add("Ldc_i4_1", OpCodes.Ldc_I4_1);
                _opCode.Add("Ldc_i4_2", OpCodes.Ldc_I4_2);
                _opCode.Add("Ldc_i4_3", OpCodes.Ldc_I4_3);
                _opCode.Add("Ret", OpCodes.Ret);
                _opCode.Add("Dup", OpCodes.Dup);
                _opCode.Add("NewArr", OpCodes.Newarr);
                _opCode.Add("StelemRef", OpCodes.Stelem_Ref);
                _opCode.Add("CallVirt", OpCodes.Callvirt);
                _opCode.Add("Call", OpCodes.Call);
                _opCode.Add("Nop", OpCodes.Nop);
                _opCode.Add("Ldfld", OpCodes.Ldfld);
                _opCode.Add("Stfld", OpCodes.Stfld);
            }


        }


    }
}