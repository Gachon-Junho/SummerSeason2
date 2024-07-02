using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CSVStorage
{
    public const string STAGE_FILE_NAME = @"Stage.csv";
    public const string RANKING_FILE_NAME = @"Ranking.csv";

    public IEnumerable<T> Read<T>(string file) 
        where T : class
    {
        List<T> data = new List<T>();
        var propInfo = typeof(T).GetProperties().Where(p => p.CanWrite).ToArray();
        
        using (StreamReader sr = new StreamReader(File.Open(Path.Combine("Assets", file), FileMode.OpenOrCreate, FileAccess.Read)))
        {
            var properties = sr.ReadLine()?.Split(',');

            if (properties == null || properties.Length == 0)
                return data;
            
            var propIdx = new int[properties.Length];

            for (int i = 0; i < propIdx.Length; i++)
                propIdx[i] = Array.IndexOf(properties, propInfo.Select(p => p.Name).ToArray()[i]);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    var propValue = line.Split(',');
                    var element = Activator.CreateInstance<T>();

                    for (int i = 0; i < propIdx.Length; i++)
                        typeof(T).GetProperty(propInfo[i].Name)?.SetValue(element, Convert.ChangeType(propValue[propIdx[i]], propInfo[i].PropertyType));
                    
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
            var properties = string.Empty;
            
            foreach (var property in typeof(T).GetProperties())
                properties += $"{property.Name},";

            sw.WriteLine(properties.Trim(','));

            foreach (var property in data)
            {
                var line = string.Empty;

                foreach (var member in typeof(T).GetProperties().Where(p => p.CanWrite))
                    line += $"{member.GetValue(property)},";

                sw.WriteLine(line.Trim(','));
            }
        }
    }
}