 using System.Reflection;

 var assembly = typeof(Program).Assembly;

 var latestType = assembly.GetTypes()
     .Where(t => t.Name.StartsWith('T') && t.Name.Contains('_'))
     .Aggregate((latestType, currentType) =>
     {
         var currentTypeIndex = int.Parse(currentType.Name.Split('_').ElementAt(0).Remove(0, 1));
         var latestTypeIndex = int.Parse(latestType.Name.Split('_').ElementAt(0).Remove(0, 1));

         return latestTypeIndex > currentTypeIndex ? latestType : currentType;
     });
 var methodInfo = latestType?.GetMethod("Demo", []);
 methodInfo?.Invoke(latestType, []);
 partial class Program() {}