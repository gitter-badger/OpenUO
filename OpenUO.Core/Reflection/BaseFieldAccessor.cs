#region License Header

// /***************************************************************************
//  *   Copyright (c) 2011 OpenUO Software Team.
//  *   All Right Reserved.
//  *
//  *   BaseFieldAccessor.cs
//  *
//  *   This program is free software; you can redistribute it and/or modify
//  *   it under the terms of the GNU General Public License as published by
//  *   the Free Software Foundation; either version 3 of the License, or
//  *   (at your option) any later version.
//  ***************************************************************************/

#endregion

#region Usings

using System;
using System.Reflection;
using System.Reflection.Emit;

#endregion

namespace OpenUO.Core.Reflection
{
    internal static class BaseFieldAccessor
    {
        public static FieldFastSetInvokeHandler<TargetType, FieldType> SetFieldInvoker<TargetType, FieldType>(string FieldName)
        {
            return SetFieldInvoker<TargetType, FieldType>(typeof(TargetType).GetField(FieldName));
        }

        public static FieldFastGetInvokeHandler<TargetType, FieldType> GetFieldInvoker<TargetType, FieldType>(string FieldName)
        {
            return GetFieldInvoker<TargetType, FieldType>(typeof(TargetType).GetField(FieldName));
        }

        public static FieldFastSetInvokeHandler<TargetType, FieldType> SetFieldInvoker<TargetType, FieldType>(FieldInfo Field)
        {
            var objectType = typeof(TargetType);

            if(Field != null)
            {
                var dm = new DynamicMethod("Set" + Field.Name, null, new[]
                                                                     {
                                                                         objectType, typeof(FieldType)
                                                                     }, objectType);
                var il = dm.GetILGenerator();

                // Load the instance of the object (argument 0) onto the stack
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);
                // Load the value of the object's field (fi) onto the stack
                il.Emit(OpCodes.Stfld, Field);
                // return the value on the top of the stack
                il.Emit(OpCodes.Ret);

                return (FieldFastSetInvokeHandler<TargetType, FieldType>)dm.CreateDelegate(typeof(FieldFastSetInvokeHandler<TargetType, FieldType>));
            }

            throw new Exception(String.Format("Member: '{0}' is not a Field of Type: '{1}'", Field.Name, objectType.Name));
        }

        public static FieldFastGetInvokeHandler<TargetType, FieldType> GetFieldInvoker<TargetType, FieldType>(FieldInfo Field)
        {
            var objectType = typeof(TargetType);

            if(Field == null)
            {
                throw new Exception(
                    String.Format(
                        "Member: '{0}' is not a Field of Type: '{1}'",
                        Field.Name,
                        objectType.Name));
            }

            var dm = new DynamicMethod(
                "Get" + Field.Name,
                typeof(FieldType),
                new[]
                {
                    objectType
                },
                objectType);
            var il = dm.GetILGenerator();

            // Load the instance of the object (argument 0) onto the stack
            il.Emit(OpCodes.Ldarg_0);
            // Load the value of the object's field (fi) onto the stack
            il.Emit(OpCodes.Ldfld, Field);
            // return the value on the top of the stack
            il.Emit(OpCodes.Ret);

            return
                (FieldFastGetInvokeHandler<TargetType, FieldType>)
                    dm.CreateDelegate(typeof(FieldFastGetInvokeHandler<TargetType, FieldType>));
        }
    }
}