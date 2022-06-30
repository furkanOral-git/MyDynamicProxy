using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace AutoProxy.Tools
{
    public class AutoProxyEmitter
    {

        public static void EmitType(TypeBuilder type,ConstructorBuilder constructor,FieldBuilder field,MethodBuilder[] methodBuilders)
        {
            //Constructor emitting
            var cil = constructor.GetILGenerator();
            cil.Emit(OpCodes.Ldarg_0);
            //Every class must call to type of Object's constructor method
            var superObj = typeof(Object).GetConstructor(new Type[0]);
            cil.Emit(OpCodes.Call,superObj);
            cil.Emit(OpCodes.Nop);
            cil.Emit(OpCodes.Nop);
            cil.Emit(OpCodes.Ldarg_0);
            var handlerMethod = typeof(AutoProxyMethodHandler).GetMethod("GetHandler");
            cil.Emit(OpCodes.Call,handlerMethod);
            cil.Emit(OpCodes.Stfld,field);

            for (int i = 0; i < methodBuilders.Length; i++)
            {
                var mil = methodBuilders[i].GetILGenerator();
                

            }




        }
        
        
    }
}