# mutantapp
you can execute this application on two ways:
## Local Environment
You must download the code and execute the .Net Framework 4.7.2 application, after that you may use postman to execute the app according to decided method, to http post use api/mutantapp/mutant how prefix on url to execute the method, and in the body of petition define:
```
{
    "dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCCTG"]
}
```
where "dna" is the string[] to evaluate, to http get use api/mutantapp/stats how prefix on url to execute the method.

## ASW Envieroment
you must consider the same request structure, but in http post case use this url : http://mutantaws.us-east-1.elasticbeanstalk.com/api/mutantapp/mutant and for http get case use this : http://mutantaws.us-east-1.elasticbeanstalk.com/api/mutantapp/stats
