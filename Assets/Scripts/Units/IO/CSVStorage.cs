using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CSVStorage
{
    public const string STAGE_FILE_NAME = @"Stage.csv";
    public const string RANKING_FILE_NAME = @"Ranking.csv";

    public IEnumerable<T> Read<T>(string file)
        where T : class
    {
        List<T> data = new List<T>();
        var propNameType = typeof(T).GetProperties()
            .Select(p => new { Name = p.Name, Type = p.PropertyType })
            .ToArray();
        
        using (StreamReader sr = new StreamReader(File.Open(Path.Combine("Assets", file), FileMode.OpenOrCreate, FileAccess.Read)))
        {
            var table = sr.ReadLine()?.Split(',');

            if (table?.Length == 0)
                return data;
            
            var propIdx = new int[table!.Length];

            for (int i = 0; i < table.Length; i++)
                propIdx[i] = Array.IndexOf(table, propNameType.Select(p => p.Name).ToArray()[i]);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    var propValue = line.Split(',');
                    var element = Activator.CreateInstance<T>();

                    for (int i = 0; i < table.Length; i++)
                    {
                        typeof(T).GetProperty(propNameType[i].Name)?
                                 .SetValue(element, Convert.ChangeType(propValue[propIdx[i]], propNameType[i].Type));
                    }
                    
                    data.Add(element);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
        }

        return data;
    }

    public void Write<T>(IEnumerable<T> data, string fileName)
    {
        using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine("Assets", fileName), FileMode.OpenOrCreate)))
        {
            var table = string.Empty;
            
            foreach (var property in typeof(T).GetProperties())
                table += $"{property.Name},";

            table = table.Trim(',');
            sw.WriteLine(table);

            foreach (var property in data)
            {
                var line = string.Empty;

                foreach (var member in typeof(T).GetProperties())
                    line += $"{member.GetValue(property)},";

                line = line.Trim(',');
                
                sw.WriteLine(line);
            }
        }
    }
}