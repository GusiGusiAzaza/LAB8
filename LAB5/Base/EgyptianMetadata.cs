using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;


namespace LAB5.Base
{
    internal partial class Egyptian
    {
        [NonSerialized]
        public ReflectionMetadata Metadata;
        [NonSerialized]
        private protected static Type MyType;

        public readonly struct ReflectionMetadata
        {
            public readonly bool HasConstructor;
            public readonly bool IsNested;
            public readonly bool IsSealed;
            public readonly string Type;
            // ReSharper disable once InconsistentNaming
            public readonly string GUID;
            public readonly string Assembly;
            public readonly string BaseType;
            public readonly List<string> PublicFields;
            public readonly List<string> PrivateFields;
            public readonly List<string> PublicProperties;
            public readonly List<string> PrivateProperties;
            public readonly List<string> PublicMethods;
            public readonly List<string> PrivateMethods;
            public ReflectionMetadata(Type reflectionBase)
            {
                MyType = reflectionBase;
                IsSealed = MyType.IsSealed;
                IsNested = MyType.IsNested;
                GUID = MyType.GUID.ToString();
                HasConstructor = (MyType.GetConstructors().Length != 0);
                Type = MyType.ToString();
                Assembly = MyType.Assembly.FullName;
                BaseType = MyType.BaseType.FullName;
                PublicFields = new List<string>();
                PrivateFields = new List<string>();
                PublicProperties = new List<string>();
                PrivateProperties = new List<string>();
                PublicMethods = new List<string>();
                PrivateMethods = new List<string>();

                foreach (var field in MyType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    PublicFields.Add(field.Name);
                }

                foreach (var field in MyType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    PrivateFields.Add(field.Name);
                }

                foreach (var property in MyType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    PublicProperties.Add(property.Name);
                }

                foreach (var property in MyType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    PrivateProperties.Add(property.Name);
                }

                foreach (var method in MyType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    PublicMethods.Add(method.Name);
                }

                foreach (var method in MyType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    PrivateMethods.Add(method.Name);
                }
            }

        }
        public void PrintMetadata()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n-------------------------METADATA INFORMATION--------------------------");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"GUID: {Metadata.GUID}");
            Console.WriteLine($"Type: {Metadata.Type}");
            Console.WriteLine($"Base Type: {Metadata.BaseType}");
            Console.WriteLine($"Assembly: {Metadata.Assembly}");
            Console.WriteLine($"Has constructor: {Metadata.HasConstructor}");
            Console.WriteLine($"IsNested: {Metadata.IsNested}");
            Console.WriteLine($"IsSealed: {Metadata.IsSealed}");
            Console.WriteLine("Fields:");
            foreach (var field in Metadata.PublicFields)
            {
                Console.WriteLine($"\t(public) {field}");
            }
            foreach (var field in Metadata.PrivateFields)
            {
                Console.WriteLine($"\t(private){field}");
            }

            Console.WriteLine("Properties:");
            foreach (var property in Metadata.PublicProperties)
            {
                Console.WriteLine($"\t(public) {property}");
            }
            foreach (var property in Metadata.PrivateProperties)
            {
                Console.WriteLine($"\t(private){property}");
            }

            Console.WriteLine("Methods:");
            foreach (var method in Metadata.PublicMethods)
            {
                Console.WriteLine($"\t(public) {method}");
            }
            foreach (var method in Metadata.PrivateMethods)
            {
                Console.WriteLine($"\t(private){method}");
            }
            Console.WriteLine("------------------------------------------------------------------\n");
            Console.ResetColor();
        }

        public void MetadataSaveToJson(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(Metadata, Formatting.Indented));
        }
    }
}
