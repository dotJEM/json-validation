using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.IntegrationTest.Util
{
    internal class TestDataLoader
    {
        /// <summary>
        /// This loads test data for data driven integration test cases in a convention based way.
        /// To use with e.g. a users.json source file:
        /// <para> - Use folders as namespaces.</para>
        /// <para> - Add a json file next to your test file (the file containing the test class) called users.json.</para>
        /// <para> - Add the following base structure to the file: { "source": "some source", "data": [...Json Objects...] }.</para>
        /// <para> - Add a property to your test class named Users and call this method with the class name: <c>public IEnumerable<JObject> Users => TestDataLoader.LoadFor<...TestClass...>();</c></para>
        /// 
        /// The TestDataLoader will fetch the file based on the namespace of the TestClass and the name of the property. If a file is not found for the property, then it tries using the testclass name.
        /// </summary>
        public static IEnumerable<JObject> LoadFor<T>([CallerMemberName]string caller = null)
        {
            //0         1         2         3         4         5         6         7
            //012345678901234567890123456789012345678901234567890123456789012345678901234567890
            //DotJEM.Json.Validation.IntegrationTest.DataDriven.Users.UserValidatorTest
            string name = typeof(T).FullName.Substring(39);

            string[] parts = name.Split('.');
            string directory = parts.Take(parts.Length - 1).Aggregate((agg, next) => agg + '\\' + next);

            string path = Directory.GetCurrentDirectory() + "\\" + directory + "\\" + caller + ".json";
            if (File.Exists(path))
            {
                return LoadJson(path);
            }
            string testName = parts.Last();
            string fileName = testName.Substring(0, testName.Length - 4);
            if (fileName.EndsWith("Validator"))
                fileName = fileName.Substring(0, fileName.Length - 9);

            path = Directory.GetCurrentDirectory() + "\\" + directory + "\\" + fileName + "s.json";
            if (File.Exists(path))
            {
                return LoadJson(path);
            }
            throw new FileNotFoundException($"Could not find datafile {path}.", path);
        }

        private static IEnumerable<JObject> LoadJson(string path)
        {
            JObject json = JObject.Parse(File.ReadAllText(path));
            return json["data"].AsEnumerable().OfType<JObject>();
        }
    }

    
}
