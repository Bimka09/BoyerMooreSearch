using BoyerMooreSearch;

Search search = new Search();
string text = "SO}STR{SIMPLESTRING{";
string mask = "STRING";
int x = search.SearchBMHSuffix(text, mask);
Console.WriteLine(x);