/*
// @param input - Converts a .net serialized string to a javascript literal Object
// Removes the '&quot;' and replaces it with '"'
*/
function DeserializeFix(input)
{
    return input.replace(/&quot;/g,'"');
}