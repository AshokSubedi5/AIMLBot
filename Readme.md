
<h2>Change Logs</h2>
#date:  <br />
   added format  <br />
  example:<br />
     < template>today is < date format="MMM dd, yyyy" /> < /template>
     <ul>
       <li> support all c# date format </li>
       <li>< date format="MMM dd, yyyy" /> </li>
       <li>< date format="dd/MM/yyyy" /> </li>
       <li>< date format="MM/dd/yyyy" /> </li>
       <li>< date format="yyyy" /> </li>
</ul>

<br /><br />
#websearch:  <br />
Added search tag  <br />
example:<br />
<pre>
< category>
    < pattern>google *< /pattern>
    < template>
      < websearch>
        < star />
      < /websearch>
    < /template>
  < /category> 
</pre>



<br /><br />
#learn:  <br />
Added Attribute fromTag & fromFile  <br />
example:<br />
<pre>
 < category>
    < pattern>RELOAD AIML< /pattern>
    < template>
      Okay.
      < think>
        < learn name="fromfile">aiml</learn>  < !-- aiml is directory -->
      < /think>
    < /template>
  < /category>
</pre>
<br />
<pre>
 < category>
    < pattern>LEARN * IS *< /pattern>
    < template>
      Ok I will learn that <star index="2"/> is <star index="1"/>.
      < learn name="fromtag">
        < star index="2"/>,
        < star index="1"/>
      < /learn>
    < /template>
  < /category>
</pre>

<br /><br />
#news:  <br />
Added news tag  <br />
example:<br />
<pre>
< category>
    < pattern>news< /pattern>
    < template>
       < news description="true"></news> 
    < /template>
  < /category> 
< !-- description="true" means long version false means short version -->
</pre>


<br /><br />
#quote:  <br />
Added quote tag  <br />
example:<br />
<pre>
< category>
    < pattern>quote< /pattern>
    < template>
       < quote />
    < /template>
  < /category> 
note: will display different quote every time
</pre>




<br /><br />
#pattern:  <br />
Now it support multiple pattern  <br />
example:<br />
<pre>
< category>
    < pattern>quote< /pattern>
    < pattern>* quote *< /pattern>
    < pattern>quote *< /pattern>
    < pattern>* quote< /pattern>
    < template>
       < quote />
    < /template>
  < /category> 
note: previously we can achieve this by useing < srai> tag
</pre>


