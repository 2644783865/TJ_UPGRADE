var ptcode;
var colno=0;
var oldcolor;
function checkme(obj) 
     { 
        if(obj.checked) 
        { 
           obj.parentNode.parentNode.parentNode.style.backgroundColor ='LawnGreen'; 
           oldcolor='LawnGreen'; 
        } 
        else 
        { 
           obj.parentNode.parentNode.parentNode.style.backgroundColor=''; 
           oldcolor=''; 
        } 
      }
      function mover(obj)
      {
//         if(obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
//         {
//            obj.style.backgroundColor ='LightBlue'; 
//         }
//         else
//         {
//            obj.style.backgroundColor ='LightBlue'; 
//         }
           oldcolor=obj.style.backgroundColor;
           
           obj.style.backgroundColor ='LightBlue'; 
      }
    function mout(obj)
      {
//         if(obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
//         {
//            obj.style.backgroundColor ='LawnGreen'; 
//         }
//         else
//         {
//           if(obj.getElementsByTagName("td")[colno].getElementsByTagName("span")[0].innerHTML==ptcode)
//           {
//              obj.style.backgroundColor ='Orange'; 
//           }
//           else
//           {
//             obj.style.backgroundColor =''; 
//           }
//         }
           obj.style.backgroundColor=oldcolor;
      }
//    function rowclick(obj)
//    {
//        table=document.getElementById("tab");//        tr=table.getElementsByTagName("tr");
//        for(i=1;i <tr.length-1;i++) 
//        { 
//            tr[i].style.backgroundColor=''; 
//        }
//        obj.style.backgroundColor ='LightCoral';
//        oldcolor='LightCoral';
//    }

      function xbjptcfind(ptc,col)
      {
        ptcode=ptc;
        colno=col;
        var table=document.getElementById("tab"); 
        var tr=table.getElementsByTagName("tr"); 
        for( i=4;i <tr.length-1;i++) 
            { 
               if(tr[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML==ptc)
                { 
                    tr[i].style.backgroundColor ='Orange'; 
                   
                }
                else 
                {
                    tr[i].style.backgroundColor ='';
                }
             }
      }
      function othptcfind(ptc,col)
      {
        ptcode=ptc;
        colno=col;
        var table=document.getElementById("tab"); 
        var tr=table.getElementsByTagName("tr"); 
        for( i=1;i <tr.length-2;i++) 
        {                 
            if(tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML==ptc)
            { 
                tr[i].style.backgroundColor ='Orange'; 
               
            }
            else 
            {
                tr[i].style.backgroundColor ='';
            }
         }
      }
      
      function othptcfind1(ptc,col)
      {
        ptcode=ptc;
        colno=col;
        var table=document.getElementById("tab"); 
        var tr=table.getElementsByTagName("tr"); 
        for( i=4;i <tr.length-2;i++) 
            { 
               if(tr[i].getElementsByTagName("td")[col].getElementsByTagName("span")[0].innerHTML==ptc)
                { 
                    tr[i].style.backgroundColor ='Orange'; 
                   
                }
                else 
                {
                    tr[i].style.backgroundColor ='';
                }
             }
      }