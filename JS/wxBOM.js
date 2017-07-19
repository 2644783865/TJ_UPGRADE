var obj;
var choose;
function wxDochk(text)
{
   var text=text;
   var table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel4_GridView5");
   var tr=table.getElementsByTagName("tr");
   switch(text)
    {
        case "全选":
            for(i=1;i<tr.length;i++)
            {
                obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                obj.checked=true;
                choose=tr[i].getElementsByTagName("td")[7].getElementsByTagName("select")[0];
                choose.selectedIndex=1;
            }
            break;
        case "取消":
            for(i=1;i<tr.length;i++)
            {
                obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                obj.checked=false;
                choose=tr[i].getElementsByTagName("td")[7].getElementsByTagName("select")[0];
                choose.selectedIndex=0;
            }
            break;
        case "勾选":
           var n=0;
           var m=0;
           for(var i=1;i<tr.length;i++)
           {
               obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
               if(obj.type.toLowerCase()=="checkbox" )
               {
                   if(obj.checked)
                   {
                      n=1;
                      obj.checked=true;
                      choose=tr[i].getElementsByTagName("td")[7].getElementsByTagName("select")[0];
                      choose.selectedIndex=1;
                      for(var j=i+1;j<tr.length;j++)
                      {
                         var nextobj=tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                         if(nextobj.type.toLowerCase()=="checkbox" )
                         {
                             if(nextobj.checked)
                             {
                                m=1;
                                for(var k=i+1;k<j+1;k++)
                                {
                                   tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                   tr[k].getElementsByTagName("td")[7].getElementsByTagName("select")[0].selectedIndex=1;
                                }
                             }
                          }  
                       }
                   }
                }
             }
             if(n==0&&m==0)
              {
                  alert("请勾选起始和结束任务！");
              }
             break;
        default:
            break;
    }
}
function wxclick(input)
{
   var checkbox=input;
   var table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel4_GridView5");
   var tr=table.getElementsByTagName("tr");
   var wxxuhao=input.parentNode.parentNode.parentNode.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
   var ctr=wxxuhao+".";
   if(checkbox.checked)
   {
      for(var i=1;i<tr.length;i++)
      {
         var wxxh=tr[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
         var choose=tr[i].getElementsByTagName("td")[7].getElementsByTagName("select")[0];

         if(wxxuhao==wxxh||wxxh.indexOf(ctr)>-1)
         {
            choose.selectedIndex=1;
            tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
         }
      }
   }
   else
   {
      for(var i=1;i<tr.length;i++)
      {
         var wxxh=tr[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
         var choose=tr[i].getElementsByTagName("td")[7].getElementsByTagName("select")[0];
         if(wxxuhao==wxxh||wxxh.indexOf(ctr)>-1)
         {
            choose.selectedIndex=0;
            tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
         }
      }
   }
}

function wxbtnout()   //生成技术外协判断是否勾选
{
   var n=0;
   var table=document.getElementById("ctl00_PrimaryContent_TabContainer1_TabPanel4_GridView5");
   var tr=table.getElementsByTagName("tr");
   for(i=1;i<tr.length;i++)
   {
      obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
      if(obj.checked)
      {
         n++;
         break;
      }
   }
   if(n>0)
   {
       return true;
   }
   else
   {
      alert('请勾选外协任务');
      return false;
   }
}