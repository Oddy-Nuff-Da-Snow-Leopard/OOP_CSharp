using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace L14
{
    class Serializer
    {
        string path;
        public Serializer(string path)
        {
            this.path = path;
        }

        private void ColorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void BinarySerialization(Car Obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Path.Combine(this.path, "car.dat");

            ColorMessage("Binary serialization");
            try
            {
                using (var fstream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fstream, Obj);
                }

                using (var fstream = new FileStream(path, FileMode.Open))
                {
                    var Car = (Car)formatter.Deserialize(fstream);
                }

                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SOAPSerialization(Car Obj)
        {
            var formatter = new SoapFormatter();

            string path = Path.Combine(this.path, "car.soap");

            ColorMessage("SOAP serialization");
            try
            {
                using (var fstream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fstream, Obj);
                }

                using (var fstream = new FileStream(path, FileMode.Open))
                {
                    var Car = (Car)formatter.Deserialize(fstream);
                }

                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void XMLSerialization(Programmer Obj)
        {
            ColorMessage("XML serialization");
            Console.WriteLine("Object which should be serialized:");
            Obj.GetInfo();
           
            var xmlSerializer = new XmlSerializer(typeof(Programmer));

            string path = Path.Combine(this.path, "programmer.xml");

            try
            {
                using (var fstream = new FileStream(path, FileMode.Create))
                {
                    Console.WriteLine("Serializing...");
                    xmlSerializer.Serialize(fstream, Obj);
                    Console.WriteLine("Object serialized successfully!\n");
                }

                using (var fstream = new FileStream(path, FileMode.Open))
                {
                    Console.WriteLine("Deserializing...");
                    var Programmer = (Programmer)xmlSerializer.Deserialize(fstream);
                    Console.WriteLine("Object deserialized successfully!");
                    Programmer.GetInfo();
                }

                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void JSONSerialization(Computer Obj)
        {
            ColorMessage("JSON serialization");

            var jsonSerializer = new DataContractJsonSerializer(typeof(Computer));

            string path = Path.Combine(this.path, "computer.json");

            try
            {
                using (var fstream = new FileStream(path, FileMode.Create))
                {
                    jsonSerializer.WriteObject(fstream, Obj);
                }

                using (var fstream = new FileStream(path, FileMode.Open))
                {
                    var Computer = (Computer)jsonSerializer.ReadObject(fstream);
                }

                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            var Car = new Car("Nissan Skyline GT-R (R34)", "Nissan", "2003");
            var Serializer = new Serializer(@"D:\L14");
            Serializer.BinarySerialization(Car);
            Serializer.SOAPSerialization(Car);

            var Programmer = new Programmer("James", "Spleen", 18, 1000, "Macbook Pro 13 2018");
            Serializer.XMLSerialization(Programmer);

            var Computer = new Computer("Z-Tech", 1500, new SSD("Intel", 350, 512, "SLS", "SandForce"));
            Serializer.JSONSerialization(Computer);


            var Ferrari = new List<Car>()
            {
                new Car("488 Spider", "Ferrari", "2017"),
                new Car("458 Italia", "Ferrari", "2016"),
                new Car("LaFerrari", "Ferrari", "2017")
            };

            var formatter = new BinaryFormatter();

            string path = @"D:\L14\ferrari.dat";
            try
            {
                using (var fstream = new FileStream(path, FileMode.Create))
                {
                    foreach (var rari in Ferrari)
                        formatter.Serialize(fstream, rari);
                }

                using (var fstream = new FileStream(path, FileMode.Open))
                {
                    var Raris = new List<Car>();
                    for (byte i = 0; i < Ferrari.Count; i++)
                        Raris.Add((Car)formatter.Deserialize(fstream));//??
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            var xml = new XmlDocument();
            xml.Load(@"D:\L14\students.xml");
            var xmlRoot = xml.DocumentElement;

            var childNodes = xmlRoot.SelectNodes("*");
            foreach (XmlNode node in childNodes)
                Console.WriteLine(node.OuterXml);

            Console.WriteLine();

            childNodes = xmlRoot.SelectNodes("Student");
            foreach (XmlNode node in childNodes)
                Console.WriteLine(node.Name);

            Console.WriteLine();

            childNodes = xmlRoot.SelectNodes("//Student/Faculty");
            foreach (XmlNode node in childNodes)
                Console.WriteLine(node.SelectSingleNode("@name").Value);

            Console.WriteLine();

            var childNode = xmlRoot.SelectSingleNode("Student[@name='Maksim']");
            if (childNode != null)
                Console.WriteLine(childNode.OuterXml);

            Console.WriteLine();

            childNode = xmlRoot.SelectSingleNode("Student[Faculty[AverageMark='10']]");
            if (childNode != null)
                Console.WriteLine(childNode.OuterXml);


            var xmlDoc = new XDocument(new XElement("Users",
                                            new XElement("User", new XAttribute("name", "Bill Gates"),
                                            new XElement("Company", new XAttribute("name", "Microsoft"),
                                            new XElement("Position", "director"), new XElement("Salary", "300$"))),

                                            new XElement("User", new XAttribute("name", "Larry Page"),
                                            new XElement("Company", new XAttribute("name", "Google"),
                                            new XElement("Position", "director"), new XElement("Salary", "250$")))));

            xmlDoc.Save(@"D:\L14\users.xml");

            var A = xmlDoc.Root.Elements("User").GroupBy(t => t.Element("Company").Value);

            foreach (var element in A)
                Console.WriteLine(element.Key);

            Console.WriteLine();

            var B = xmlDoc.Root.Elements("User").GroupBy(t => t.Element("Company").Attribute("name"));

            foreach (var element in B)
                Console.WriteLine(element.Key);

            Console.ReadLine();
        }
    }
}