using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;

namespace BddShop.Tests.Infra
{
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string[] _fileNames;
        private readonly Type _type;

        public JsonFileDataAttribute(string fileNames, Type type)
        {
            _fileNames = fileNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
            _type = type;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

            var result = new List<object[]>();

            foreach (var fileName in _fileNames)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "TestData", fileName);

                if (!File.Exists(path))
                {
                    throw new ArgumentException($"Could not find file at path: {path}");
                }

                var fileData = File.ReadAllText(path);

                var records = JsonConvert.DeserializeObject<JObject[]>(fileData);

                foreach (var record in records)
                {
                    result.Add(new[] { record.ToObject(_type) });
                }

            }

            return result;
        }
    }
}
