using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Json.TableToJson;
using Microsoft.ProgramSynthesis.Transformation.Json.TableToJson.Constraint;

using Microsoft.ProgramSynthesis.Transformation.Json;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Newtonsoft.Json.Linq;

namespace TableTransformationTest
{
    public class Test
    {
        private static void SimpleTest()
        {
            // Step 1: Training
            JToken jsonTrainInput = JToken.Parse(@"
            {
                ""datatype"": ""local"",
                ""data"": [
                    {
                        ""name"": ""Andrew"",
                        ""Qual1"": ""01.02.2003"",
                        ""Qual2"": ""27.06.2008"",
                        ""Qual3"": ""06.04.2007""
                    }                   
                ]
            }
            ");
            JToken jsonTrainOutput = JToken.Parse(@"
                [
                    {
                        ""Andrew"": ""01.02.2003""                      
                    },
                    {
                        ""Andrew"": ""27.06.2008""
                    },
                    {
                        ""Andrew"": ""06.04.2007""
                    }
                ]
            ");

            var session = new Session();
            session.Constraints.Add(new Example<JToken, JToken>(jsonTrainInput, jsonTrainOutput));
            Program topRankedProgram = session.Learn();

            if (topRankedProgram == null)
            {
                Console.Error.WriteLine("Error: failed to learn a program.");
                return;
            }
            else
            {
                Console.WriteLine("Success: a program is learned.");
            }
            Console.WriteLine(topRankedProgram.ToString());
        }

        static void Main(string[] args)
        {
            SimpleTest();
        }
    }
}

