export function objectToQueryString(obj: any)
{
  var queryString = "?";
  for(var property in obj)
  {
      var value = obj[property];

      if(value && typeof(value) != "function")
         queryString += property + "=" + encodeURIComponent(value) + "&";
  } 
  return queryString;
}
