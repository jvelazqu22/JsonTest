using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace Reports
{
    public class MainReport
    {
        public void RunReportAndSaveToFile()
        {
            var inputPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Input\code_test.json");
            var outputPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Output\Results.txt");
            var list = GetRawData(inputPath);
            var allPeople = GetAllPeople(list);
            var totalNumberOfPeople = $"total # of people: {allPeople.Count}";
            var peopleWithBlueEyesOver30 = GetActiveManWithBlueEyesOver30(list);
            var totalNumberOfpeopleWithBlueEyesOver30 = $"total # of people with blue eyes over 30: {peopleWithBlueEyesOver30.Count}";
            var peopleWithLessThanThreeFriends = GetPeopleWithLessThanThreeFriends(list);
            var finalResults = GetFinalResult(allPeople, totalNumberOfPeople, peopleWithBlueEyesOver30, totalNumberOfpeopleWithBlueEyesOver30, peopleWithLessThanThreeFriends);
            SaveResultsToFile(finalResults, outputPath);
        }

        public List<Person> GetRawData(string path)
        {
            var jsonstring = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Person>>(jsonstring);
        }

        private void SaveResultsToFile(List<string> dataToWrite, string path)
        {
            // This text is added only once to the file.
            if (!File.Exists(path)) File.WriteAllLines(path, dataToWrite);
        }

        private List<string> GetFinalResult(List<string> allPeople, string totalNumberOfPeopleString, List<string> peopleWithBlueEyesOver30, string totalNumberOfpeopleWithBlueEyesOver30String, List<string> peopleWithLessThanThreeFriends)
        {
            List<string> results = new List<string>();
            results.AddRange(allPeople);
            results.Add(totalNumberOfPeopleString);
            results.AddRange(peopleWithBlueEyesOver30);
            results.Add(totalNumberOfpeopleWithBlueEyesOver30String);
            results.AddRange(peopleWithLessThanThreeFriends);

            return results;
        }

        private List<string> GetAllPeople(List<Person> list)
        {
            var sortedList = Sort(list);
            return FormatData(list);
        }

        private List<string> GetActiveManWithBlueEyesOver30(List<Person> list)
        {
            var filterList = list.Where(w => w.eyeColor.Equals("blue", StringComparison.OrdinalIgnoreCase) && w.age > 30).ToList();
            var sortedList = Sort(filterList);
            return FormatData(sortedList);
        }

        private List<string> GetPeopleWithLessThanThreeFriends(List<Person> list)
        {
            var filterList = list.Where(w => w.friends.Count < 3).ToList();
            return FormatDataForFriendRule(filterList);
        }

        private List<string> FormatDataForFriendRule(List<Person> list)
        {
            var results = new List<string>();

            foreach (var item in list)
            {
                var record = $"{item.age} | {item.lastName} | {item.firstName} | {item.eyeColor} | {item.gender} | {item.friends.Count}";
                results.Add(record);
            }

            return results;
        }

        public List<string> FormatData(List<Person> list)
        {
            var results = new List<string>();

            foreach (var item in list)
            {
                var record = $"{item.age} | {item.lastName} | {item.firstName} | {item.eyeColor} | {item.gender}";
                results.Add(record);
            }

            return results;
        }

        public List<Person> Sort(List<Person> list)
        {
            return list.OrderByDescending(o => o.age)
                .ThenBy(o => o.lastName)
                .ThenBy(o => o.firstName)
                .ToList();
        }
    }
}
