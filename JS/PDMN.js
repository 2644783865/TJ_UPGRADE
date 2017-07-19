var table;
var tr;
var obj;
var objstr;
var list;
var id;
var intIndex=0;
var max;   //最大时间
var min;   //最小时间

/*全选、取消、勾选*/
function Dochk(text)
{
    var text=text;
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr"); 
    switch(text)
    {
        case "全选":
            for(i=1;i<tr.length;i++)
            {
                obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                obj.checked=true;
            }
            break;
        case "取消":
            for(i=1;i<tr.length;i++)
            {
                obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                obj.checked=false;
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
    var dropDownList = document.getElementById("ctl00_PrimaryContent_ddloperate"); 
    dropDownList.options[0].selected = true; 
}

/*指定负责人和班组*/
function Chperson(text,m)
{ 
    var n=0;
    var text=text;
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr");
    for(i=1;i<tr.length;i++)
    {
//        var m=0;
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        objperson=tr[i].getElementsByTagName("td")[m].getElementsByTagName("select")[0];
//        objtxt=tr[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0];
        if(obj.checked)
        {
            n=1;
            for(j=1;j<objperson.options.length;j++)
            {
                if(objperson.options[j].value == text) 
                { 
                    objperson.options[j].selected = true; 
                    break; 
                } 

            }
//            if(objtxt.value=='')
//            {
//                objtxt.value=text;
//            }
//            else
//            {
//               var str=new Array();
//               str=(objtxt.value).split('、');
//               for(z=0;z<str.length;z++)
//               {
//                  if(str[z]==text)
//                  {
//                     m++;
//                     break;
//                  }
//               }
//               if(m==0)
//               {
//                  objtxt.value=objtxt.value+'、'+text;
//               }
//            }
        }
    }
    if(n==0)
    {
       alert("请勾选需要制定调度员的任务！");
    }
    var dropDownList = document.getElementById("ctl00_PrimaryContent_ddlduy"); 
    dropDownList.options[0].selected = true; 
} 
  
/*指定班组*/
function Chbanzu(text,colnum)
{
    var n=0;
    var text=text;
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr");
    for(i=1;i<tr.length;i++)
    {
        var m=0;
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        objperson=tr[i].getElementsByTagName("td")[colnum].getElementsByTagName("select")[0];
        objtxt=tr[i].getElementsByTagName("td")[colnum].getElementsByTagName("input")[0];
        if(obj.checked)
        {
            n=1;
            for(j=1;j<objperson.options.length;j++)
            {
                if(objperson.options[j].value == text) 
                { 
                    objperson.options[j].selected = true; 
                    break; 
                } 

            }
            if(objtxt.value=='')
            {
              objtxt.value=text;
            }
            else
            {
               var str=new Array();
               str=(objtxt.value).split('、');
               for(z=0;z<str.length;z++)
               {
                  if(str[z]==text)
                  {
                     m++;
                     break;
                  }
               }
               if(m==0)
               {
                 objtxt.value=objtxt.value+'、'+text;
               }
            }
        }
    }
    if(n==0)
    {
       alert("请勾选需要制定制作班组的任务！");
    }
    var dropDownList = document.getElementById("ctl00_PrimaryContent_ddlbanzu"); 
    dropDownList.options[0].selected = true; 
}

/* 生产计划下推*/
 function SelPlanDetail()
 { 
    var n=0;
    ret = confirm("你确定下推生产计划？");
    if(ret)
    {
       var table=document.getElementById("ctl00_PrimaryContent_GridView1");
       var tr=table.getElementsByTagName("tr"); 
       for(i=1;i<tr.length;i++)
       {
           var obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
           if(obj.type.toLowerCase()=="checkbox" )
           {
              if(obj.checked)
              {
                 n=1;
              }
           }
       }
       if(n==0)
       {
          alert("请勾选要下推生产计划的任务！")
          return false;
       }
       else
       {
          var dt=new Date();
          var sec=dt.getTime();
          var i=window.showModalDialog('PM_Manut_XiatuiDetail.aspx?&no='+sec,'',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
          document.getElementById("ctl00_PrimaryContent_hfpldetail").value=i;
          return true;
       }
   } 
   else
   {
      return false;
   }
 }  
 /*对调度员可以进行再指定*/
 function setperson(id,ddlid)
 {
    var n=0;
    var id=id;
    var text=ddlid.options[ddlid.selectedIndex].value;
    var textbox=document.getElementById(id);
    if(textbox.value=='')
    {
       textbox.value=text;
    }
    else
    {
       var str=new Array();
       str=(textbox.value).split('、');
       for(i=0;i<str.length;i++)
       {
          if(str[i]==text)
          {
             n++;
             break;
          }    
       }
       if(n==0)
       {
          textbox.value=textbox.value+'、'+text;
       }
       else
       {
          alert('已存在！');
       }      
    }
    ddlid.options[0].selected = true; 
 }
 
 
 /*指定邮件发送人*/
 function setyjperson(id,ddlid)
 {
    var n=0;
    var id=id;
    var text=ddlid.options[ddlid.selectedIndex].value;
    var textbox=document.getElementById(id);
   
    if(textbox.value=='')
    {
      
       textbox.value=text;
    }
    else
    {
       var str=new Array();
       str=(textbox.value).split('、');
       for(i=0;i<str.length;i++)
       {
          if(str[i]==text)
          {
             n++;
             break;
          }    
       }
       if(n==0)
       {
          textbox.value=textbox.value+'、'+text;
       }
       else
       {
          alert('已存在！');
       }      
    }
    ddlid.options[0].selected = true; 
 }
 
 
 function setffperson(id,ddlid)
 {
    var n=0;
    var id=id;
    var text=ddlid.options[ddlid.selectedIndex].value;
    var textbox=document.getElementById(id);
    textbox.value=text;
    ddlid.options[0].selected = true; 
 }
 
 /*GridView中指定多个制作班组*/
 function setcherk(select)
 {
    var n=0;
    var ddltext=select.options[select.selectedIndex].value;
    var textbox=select.parentNode.getElementsByTagName("input")[0];
    if(ddltext!='0')
    {
       if(textbox.value=='')
       {
          textbox.value=ddltext;
       }
       else
       {
          var str=new Array();
          str=(textbox.value).split('、');
          for(i=0;i<str.length;i++)
          {
             if(str[i]==ddltext)
             {
                n++;
                break;
             }
          }
          if(n==0)
          {
             textbox.value=textbox.value+'、'+ddltext;
          }
       }
    }
 }
 /*生产部主计划JS函数*/
 //检验日期格式如：2012-01-01
function dateCheck(obj)
{
    var value=obj.value;
    if(value!="")
    {
        var re = new RegExp("^([0-9]{4})(-)([0-9]{1,2})(-)([0-9]{1,2})$");
        m = re.exec(value)
        if (m == null ) 
        {
            obj.style.background="yellow";
            obj.value="";
            alert('请输入正确的时间格式如：2012-1-1或2012-01-01');
        }
        else 
        {
            obj.style.background="Transparent";
            obj.value=normDate(value);
        }
    }
 }
 //获取正规的时间格式如：2012-01-01
 function normDate(value)
 {
    var date;
    var newmonth;
    var newday;
    arrList = new Array();
    arrList=value.split("-");
    var reg=new RegExp("^[0-9]{2}$");
    var n=reg.exec(arrList[1]);
    if(n==null)
    {
        newmonth='0'+arrList[1];
    }
    else
    {
        newmonth=arrList[1];
    }
    var z=reg.exec(arrList[2]);
    if(z==null)
    {
        newday='0'+arrList[2];
    }
    else
    {
        newday=arrList[2];
    }
    date=arrList[0]+"-"+newmonth+"-"+newday;
    return date;
 }

function checkPRftime(click,tag)   //用于对实际完成时间和计划完成时间进行判断
{

  var maxarrList = new Array();  //最大数组
  
  dateCheck(click)//检验时间格式
  if(tag=="4") //表示填写实际完成时间是判断计划完成时间是否存在
  {
//     ISCheckPFtime(click,tag);
  }
  else   //表示填写计划完成时间时判断计划开始时间是否存在
  {
//     ISCheckPFPStime(click,tag)
  }
  var text=click.value;
  

  
  //获取事件的行索引
  var this_rowindex=click.parentNode.parentNode.rowIndex;
  var this_fnode=click.parentNode.parentNode.getElementsByTagName("span")[4].innerHTML;  //获得当前行的父节点
  var this_mark=click.parentNode.parentNode.getElementsByTagName("span")[7].innerHTML;  //标记号
  var this_pjid=click.parentNode.parentNode.getElementsByTagName("span")[8].innerHTML;  //获得当前行的项目ID
  var this_engid=click.parentNode.parentNode.getElementsByTagName("span")[10].innerHTML;  //获得当前行的工程ID
  var this_frowindex=0; //上级任务的行索引
  var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
  var treetr=treetable.getElementsByTagName("tr");
  //获取上级任务的索引
  if(this_fnode!="0")  
  {
      for(var j=1;j<this_rowindex;j++)
      {
          var r_node=treetr[j].getElementsByTagName("span")[3].innerHTML;   //子节点
          var r_engid=treetr[j].getElementsByTagName("span")[10].innerHTML;  //工程ID
          var r_pjid=treetr[j].getElementsByTagName("span")[8].innerHTML;  //项目ID
          
          if(this_fnode=="01")
          {
              if(r_node==this_fnode&&r_pjid==this_pjid)
              {
                  this_frowindex=j;
                  break;
              }
          }
          else
          {
              if(r_node==this_fnode&&r_engid==this_engid)
              {
                  this_frowindex=j;
                  break;
              }
          }
      }
      //判断同级的任务是否完成，完成的加入数组中
      var bool=0; //用于记录同级任务是否都完成（默认为0表示完成）
      for(var i=1;i<treetr.length;i++)
      {
          var fnode=treetr[i].getElementsByTagName("span")[4].innerHTML;   //父节点
          var obj=treetr[i].getElementsByTagName("input")[tag];
          var strtime=obj.value;
          var pjid=treetr[i].getElementsByTagName("span")[8].innerHTML;  //项目ID
          var engid=treetr[i].getElementsByTagName("span")[10].innerHTML;  //工程ID
          if(fnode==this_fnode&&pjid==this_pjid&&engid==this_engid)  //将同级的任务时间（非空）添加到数组中
          {
              if(strtime!="")
              {
                  maxarrList[maxarrList.length]=strtime;
              }
              else
              {
                 bool=1;
                 break;
              }
          }
      }
      
      //判断bool的值为1表示存在子任务没有完成，将上级任务的时间
      if(this_frowindex!=0)
      {
          if(bool==1)
          {
              treetr[this_frowindex].getElementsByTagName("input")[tag].value="";
//              treetr[this_frowindex].getElementsByTagName("input")[tag].onchange();
         }
          else
          {
              if(maxarrList.length!=0)
              {
                  //获取数组的最大值
                  var f_value=DateMax(maxarrList);
                  treetr[this_frowindex].getElementsByTagName("input")[tag].value=f_value;
//                  treetr[this_frowindex].getElementsByTagName("input")[tag].onchange();
            }
          }
         
          if(tag="4")
          {
          
           var ss= treetr[1].getElementsByTagName("span")[15].innerHTML;
           quxian(ss);
       
          }
        }

  }
}

function checkPRstime(click,tag)
{
  var minarrList = new Array();  //最小数组
  dateCheck(click);//检验时间格式
  if(tag=="5")  //表示实际开始时间（判断计划开始时间是否存在）
  {
     ISCheckPStime(click,tag);
  }
  var text=click.value;
  //获取事件的行索引
  var this_rowindex=click.parentNode.parentNode.rowIndex;
  var this_fnode=click.parentNode.parentNode.getElementsByTagName("span")[4].innerHTML;  //获得当前行的父节点
  var this_mark=click.parentNode.parentNode.getElementsByTagName("span")[7].innerHTML;  //标记号
  var this_pjid=click.parentNode.parentNode.getElementsByTagName("span")[8].innerHTML;  //获得当前行的项目ID
  var this_engid=click.parentNode.parentNode.getElementsByTagName("span")[10].innerHTML;  //获得当前行的工程ID
  var this_frowindex=0; //上级任务的行索引
  var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
  var treetr=treetable.getElementsByTagName("tr");
  //获取上级任务的索引
  if(this_fnode!="0")  
  {
      for(var j=1;j<this_rowindex;j++)
      {
          var r_node=treetr[j].getElementsByTagName("span")[3].innerHTML;   //子节点
          var r_engid=treetr[j].getElementsByTagName("span")[10].innerHTML;  //工程ID
          var r_pjid=treetr[j].getElementsByTagName("span")[8].innerHTML;  //项目ID
          
          if(this_fnode=="01")
          {
              if(r_node==this_fnode&&r_pjid==this_pjid)
              {
                  this_frowindex=j;
                  break;
              }
          }
          else
          {
              if(r_node==this_fnode&&r_engid==this_engid)
              {
                  this_frowindex=j;
                  break;
              }
          }
      }
      //判断同级的任务是否完成，完成的加入数组中
      for(var i=1;i<treetr.length;i++)
      {
          var fnode=treetr[i].getElementsByTagName("span")[4].innerHTML;   //父节点
          var obj=treetr[i].getElementsByTagName("input")[tag];
          var strtime=obj.value;
          var pjid=treetr[i].getElementsByTagName("span")[8].innerHTML;  //项目ID
          var engid=treetr[i].getElementsByTagName("span")[10].innerHTML;  //工程ID
          if(fnode==this_fnode&&pjid==this_pjid&&engid==this_engid)  //将同级的任务时间（非空）添加到数组中
          {
              if(strtime!="")
              {
                  minarrList[minarrList.length]=strtime;
              }
          }
      }
      if(this_frowindex!=0)
      {
          if(minarrList.length!=0)
          {
              //获取数组的最小值
              var f_value=DateMin(minarrList);
              treetr[this_frowindex].getElementsByTagName("input")[tag].value=f_value;
              treetr[this_frowindex].getElementsByTagName("input")[tag].onchange();
          }
          else
          {
              treetr[this_frowindex].getElementsByTagName("input")[tag].value="";
              treetr[this_frowindex].getElementsByTagName("input")[tag].onchange();
          }
      }
  }
}
//填写实际开始时间时判断计划开始时间是否存在
function ISCheckPStime(obj,tag)
{
   //获取计划开始时间
   var pstime=obj.parentNode.parentNode.getElementsByTagName("input")[tag-2].value;
   var rstime=obj.value;
   if(pstime==""&&rstime!="")
   {
//       obj.style.background="yellow";
//       alert('请先输入计划开始时间！');
//       obj.value="";
   }
}
//填写实际完成时间时判断计划完成时间是否存在
function ISCheckPFtime(obj,tag)
{
   //获取计划完成时间
   var pftime=obj.parentNode.parentNode.getElementsByTagName("input")[tag-2].value;
   //获取实际开始时间
   var rstime=obj.parentNode.parentNode.getElementsByTagName("input")[tag-1].value;
   var rftime=obj.value;
   if(rftime!=""&&(rstime=="" || pftime==""))
   {
       obj.style.background="yellow";
       alert('请先输入实际开始时间和计划完成时间！');
       obj.value="";
   }
}
//填写计划完成时间时要判断计划开始时间是否存在
function ISCheckPFPStime(obj,tag)
{
   //获取计划开始时间
   var pstime=obj.parentNode.parentNode.getElementsByTagName("input")[tag-1].value;
   var pftime=obj.value;
   if(pstime==""&&pftime!="")
   {
       obj.style.background="yellow";
       alert('请先输入计划开始时间！');
       obj.value="";
   }
}
//获取日期数组的最大日期
function DateMax(arr)
{ 
    var max=0;
    for(var i=0;i<arr.length;i++)
    {
        var maxtime=new Date(arr[max].replace(/\-/g, "\/")); 
        var time=new Date(arr[i].replace(/\-/g, "\/")); 
        if(time>maxtime)
        {
            max=i;
        }
    }
    return arr[max];
}
//获取日期数组的最小日期
function DateMin(arr)
{ 
    var min=0;
    for(var i=0;i<arr.length;i++)
    {
        var mintime=new Date(arr[min].replace(/\-/g, "\/")); 
        var time=new Date(arr[i].replace(/\-/g, "\/")); 
        if(time<mintime)
        {
            min=i;
        }
    }
    return arr[min];
}

//用于计算任务的完成百分比
function FinshPercent(click)
{

  var arrtaskqnty = new Array();  //同级工程量
  var arrpercent = new Array();   //同级百分比
  var text=click.value;
  if(text=="")
  {
      click.value="0";
  }
  else
  {
      //判断百分比格式
      PerData(click);
     
  }
  
  //获取事件的行索引
  var this_rowindex=click.parentNode.parentNode.rowIndex;
  var this_fnode=click.parentNode.parentNode.getElementsByTagName("span")[4].innerHTML;  //获得当前行的父节点
  var this_mark=click.parentNode.parentNode.getElementsByTagName("span")[7].innerHTML;  //标记号
  var this_pjid=click.parentNode.parentNode.getElementsByTagName("span")[8].innerHTML;  //获得当前行的项目ID
  var this_engid=click.parentNode.parentNode.getElementsByTagName("span")[10].innerHTML;  //获得当前行的工程ID
  var this_frowindex=0; //上级任务的行索引
  var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
  var treetr=treetable.getElementsByTagName("tr");
  //获取上级任务的索引
  if(this_fnode!="0")  
  {
      for(var j=1;j<this_rowindex;j++)
      {
          var r_node=treetr[j].getElementsByTagName("span")[3].innerHTML;   //子节点
          var r_engid=treetr[j].getElementsByTagName("span")[10].innerHTML;  //工程ID
          var r_pjid=treetr[j].getElementsByTagName("span")[8].innerHTML;  //项目ID
          
          if(this_fnode=="01")
          {
              if(r_node==this_fnode&&r_pjid==this_pjid)
              {
                  this_frowindex=j;
                  break;
              }
          }
          else
          {
              if(r_node==this_fnode&&r_engid==this_engid)
              {
                  this_frowindex=j;
                  break;
              }
          }
      }
      //判断同级的任务是否完成，完成的加入数组中
      for(var i=1;i<treetr.length;i++)
      {
          var fnode=treetr[i].getElementsByTagName("span")[4].innerHTML;   //父节点
          var taskqnty=treetr[i].getElementsByTagName("input")[8].value;   //获取工程量
          
          var percent=treetr[i].getElementsByTagName("input")[6].value;  //获取完成百分比
          var pjid=treetr[i].getElementsByTagName("span")[8].innerHTML;  //项目ID
          var engid=treetr[i].getElementsByTagName("span")[10].innerHTML;  //工程ID
          
         
          if(fnode==this_fnode&&pjid==this_pjid&&engid==this_engid)  //将同级的任务时间（非空）添加到数组中
          {
              if(taskqnty=="")
              {
                  taskqnty=0;
              }
              if(percent=="")
              {
                  percent=0;
              }
              arrtaskqnty[arrtaskqnty.length]=taskqnty;
              arrpercent[arrpercent.length]=percent;
          }
      }
      if(this_frowindex!=0)
      {
          var per=Math.round(TaskPercent(arrtaskqnty,arrpercent));
//          var prfdate=treetr[this_frowindex].getElementsByTagName("input")[6].value
//          if(per==100&&prfdate=='')
//          {
//              treetr[this_frowindex].getElementsByTagName("input")[7].value=99;
//          }
//          else
//          {
              treetr[this_frowindex].getElementsByTagName("input")[7].value=per;
//          }
          treetr[this_frowindex].getElementsByTagName("input")[7].onchange();
      }
  }
}
//验证百分比在0-100之间的整数
function PerData(obj)
{
   
    var text=obj.value;
    var pattem=/^\d+$/; 
    if(!pattem.test(text))
    {
        obj.style.background="yellow";
        obj.value="0";
        alert('请输入0~100的整数!');
        return false;
    }
    else 
    {
        if(Number(text)>100)
        {
            obj.style.background="yellow";
            obj.value="0";
            alert('请输入小于等于100的整数!');
            return false;
        }
        else if(Number(text)==100)
        {
            //判断实际完成时间是否存在
            var rftime=obj.parentNode.parentNode.getElementsByTagName("input")[6].value;
            if(rftime=="")
            {
//                obj.style.background="yellow";
//                obj.value="0";
//                alert('完成百分比为100%时，请先填写实际完成时间!');
//                return false;
            }
            else
            {
                obj.style.background="Transparent";
            }
        }
        else if(Number(text)<100&&Number(text)>0)
        {
            //判断实际开始时间是否存在
            var rftime=obj.parentNode.parentNode.getElementsByTagName("input")[5].value;
            if(rftime=="")
            {
                obj.style.background="yellow";
                obj.value="0";
//                alert('完成百分比不为0时，请先填写实际开始时间!');
//                return false;
            }
            else
            {
                obj.style.background="Transparent";
            }
        }
    }
}
//求工程加权百分比
function TaskPercent(arrtaskqnty,arrpercent)
{
     var tasksum=0;
     var taskfinsh=0;
     var per;
     var taskgx=0;
     for(var i=0;i<arrtaskqnty.length;i++)
     {
         var tasktemp;
         if(Number(arrtaskqnty[i])==0)
         {
             tasktemp=1;
         }
         else
         {
             tasktemp=Number(arrtaskqnty[i]);
         }
        
         tasksum=tasksum+tasktemp;
         taskfinsh=taskfinsh+tasktemp*Number(arrpercent[i])/100;

//         taskgx=taskgx+Number(arrpercent[i])/100;
     }
     per=taskfinsh*100/tasksum;
//     if(tasksum!=0)
//     {
//         per=taskfinsh*100/tasksum;
//     }
//     else
//     {
//         per=taskgx*100/Number(arrtaskqnty.length);
//     }
     return per;
}
function checkpftime(click)   //用于对计划开始时间进行判断
{
  var text=click.value;
  var pftime=new Date(text.replace(/\-/g, "\/"));         

  var fnode=click.parentNode.parentNode.getElementsByTagName("span")[4].innerHTML;  //获得当前行的父节点
  var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
  var treetr=treetable.getElementsByTagName("tr");
  for(i=1;i<treetr.length;i++)
  {
     var node=treetr[i].getElementsByTagName("span")[3].innerHTML;
     var pnode=treetr[i].getElementsByTagName("span")[4].innerHTML;
     var strtime=treetr[i].getElementsByTagName("input")[6].value;     
     if(node==fnode)
     {
        if(strtime!="")
        {
           var time=new Date(strtime.replace(/\-/g,"\/"));
           if(pftime>time)
           {
              click.style.background="yellow";
              click.value="";
              alert('计划完成时间不能高于上级任务的完成时间');
              return;
           }
        }
        else
        {
           click.value="";
           alert('此任务的上级任务还未下计划，不能对此任务制定计划！');
           return;
        }
        break;
     }
  }
}


function checkrstime(input)  //检验实际开始时间是否超过当前时间
{
  var text=input.value;
  var rstime=new Date(text.replace(/\-/g, "\/")); 
  var year=new Date().getFullYear();
  var month=new Date().getMonth()+1;
  var day=new Date().getDate();
  var date=year+"-"+month+"-"+day;
  var nowtime=new Date(date.replace(/\-/g, "\/"));
  if(rstime>nowtime)
  {
     input.style.background="yellow";
     input.value="";
     alert('实际开始时间不能超过当前时间！');
     return;
  } 
}
function checkrftime(input)   //检验实际完成时间是否低于实际开始时间
{
  var text=input.value;
  var rftime=new Date(text.replace(/\-/g, "\/")); 
  var rsvalue=input.parentNode.parentNode.getElementsByTagName("input")[5].value;
  var rstime=new Date(rsvalue.replace(/\-/g,"\/" ));
  if(rftime<rstime)
  {
     input.style.background="yellow";
     input.value="";
     alert('实际完成时间不能低于实际开始时间！');
     return;
  } 
}

/*验证输入总序格式*/
function verify(input)
{
    var text=input.value;
    if(text=="")
    {
       input.style.background="yellow";
       input.value="";
       alert( "提示:请输入数据! "); 
    }
    else
    {
       if(!/^[.0-9]*$/g.test(text))
       {
          input.style.background="yellow";
          input.value="";
          alert( "提示:输入格式有误! "); 
       }
    }
}

function upcheck(check)   //勾选任务，将上级任务也勾选
{ 
   var parentid=check.parentNode.parentNode.parentNode.getElementsByTagName("span")[14].innerHTML;  //父节点
   var nodeid=check.parentNode.parentNode.parentNode.getElementsByTagName("span")[13].innerHTML; //子节点
   var pjid=check.parentNode.parentNode.parentNode.getElementsByTagName("span")[15].innerHTML;  //项目ID
   var engid=check.parentNode.parentNode.parentNode.getElementsByTagName("span")[16].innerHTML; //获得工程ID
   var booled=false;
   if(check.type.toLowerCase()=="checkbox" )
   {
     if(check.checked)
     {
        booled=true;
     }
     switch(parentid)
     {
        case '0':
            checkPjallassign(pjid,booled); //所有此项目的任务操作
            break;
        case '01':
            checkEngallassign(pjid,engid,booled); //所有此工程的任务操作
            break;
         default:
            checkassign(pjid,nodeid,engid,booled);      //任务的向上操作 
            break;
     }
   }
}
function checkPjallassign(pjid,tag)  
{
   var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
   var treetr=treetable.getElementsByTagName("tr");
   for(i=1;i<treetr.length;i++)
   {
      var fpjid=treetr[i].getElementsByTagName("span")[15].innerHTML;
      var check=treetr[i].getElementsByTagName("input")[0];
      if(pjid==fpjid )
      {
          if(tag)   //勾选操作
          {
             check.checked=true;
          }
          else    //取消操作
          {
             check.checked=false;
          }
      }
   }
}
function checkEngallassign(pjid,engid,tag)
{
    var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
    var treetr=treetable.getElementsByTagName("tr");
    for(var i=1;i<treetr.length;i++)
    {
        var fpjid=treetr[i].getElementsByTagName("span")[15].innerHTML;//项目ID
        var fengid=treetr[i].getElementsByTagName("span")[16].innerHTML; //工程ID
        var code;
        if(fengid.indexOf("-")>0)
        {
           code=fengid.substr(0,fengid.indexOf("-"));
        }
        else
        {
           code=fengid;
        }
        var mark=treetr[i].getElementsByTagName("span")[17].innerHTML;  //子节点
        var check=treetr[i].getElementsByTagName("input")[0];
        if(mark=="3"&&fpjid==pjid)
        {
            if(tag)
            {
                check.checked=true;
            }
        }
        else if(fpjid==pjid&&code==engid)
        {
            if(tag)
            {
                check.checked=true;
            }
            else
            {
                check.checked=false;
            }
        }
    }
}

function checkassign(pjid,nodeid,engid,tag)
{
    var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
    var treetr=treetable.getElementsByTagName("tr");
    for(i=1;i<treetr.length;i++)
    {
        var childid=treetr[i].getElementsByTagName("span")[13].innerHTML; //子节点
        var parentid=treetr[i].getElementsByTagName("span")[14].innerHTML; //父节点
        var fpjid=treetr[i].getElementsByTagName("span")[15].innerHTML;//项目ID
        var fengid=treetr[i].getElementsByTagName("span")[16].innerHTML;
        var code;
        if(engid.indexOf("-")>0)
        {
           code=engid.substr(0,engid.indexOf("-"));
        }
        else
        {
           code=engid;
        }
        var mark=treetr[i].getElementsByTagName("span")[17].innerHTML;  //标记
        var check=treetr[i].getElementsByTagName("input")[0];
        if(mark=="3"&&fpjid==pjid)   //勾选项目
        {
            if(tag)
            {
                check.checked=true;
            }
        }
        if(mark=="2"&&code==fengid)
        {
            if(tag)
            {
                check.checked=true;
            }
        }
        if(nodeid==childid&&engid==fengid)
        {
            if(tag)
            {
                check.checked=true;
            }
            else
            {
                check.checked=false;
            }
            checkassign(pjid,parentid,engid,tag);
            break;
        }
    }
}

function imgclick(btnfold)    //页面加载时单击img，将主计划折叠展示(对于主计划的修改)
{
    var btnvalue=btnfold.value;
    var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
    var treetr=treetable.getElementsByTagName("tr");
    if(btnvalue=="折 叠")
    {
       for(i=1;i<treetr.length;i++)
       {
          var trimg=treetr[i].getElementsByTagName("img");
//          var mark=treetr[i].getElementsByTagName("span")[7].innerHTML;
          if(trimg.length=="1"&&trimg[0].onclick!=null&&trimg[0].title=="折叠")
          {
             trimg[0].click();
          }
        }
        btnfold.value="展 开";
    }
    else if(btnvalue=="展 开")
    {
       for(i=1;i<treetr.length;i++)
       {
          var trimg=treetr[i].getElementsByTagName("img");
//          var mark=treetr[i].getElementsByTagName("span")[7].innerHTML;
          if(trimg.length=="1"&&trimg[0].onclick!=null&&trimg[0].title=="展开")
          {
             trimg[0].click();
          }
        }
        btnfold.value="折 叠";
    }
}

function imgclick1(btnfold)    //页面加载时单击img，将主计划折叠展示(对于主计划的查看)
{
    var btnvalue=btnfold.value;
    var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
    var treetr=treetable.getElementsByTagName("tr");
    if(btnvalue=="折 叠")
    {
       for(i=1;i<treetr.length;i++)
       {
          var trimg=treetr[i].getElementsByTagName("img");
//          var mark=treetr[i].getElementsByTagName("span")[17].innerHTML;
          if(trimg.length=="1"&& trimg[0].onclick!=null&&trimg[0].title=="折叠")
          {
             trimg[0].click();
          }
       }
       btnfold.value="展 开";
    }
    else if(btnvalue=="展 开")
    {
       for(i=1;i<treetr.length;i++)
       {
          var trimg=treetr[i].getElementsByTagName("img");
//          var mark=treetr[i].getElementsByTagName("span")[17].innerHTML;
          if(trimg.length=="1"&&trimg[0].onclick!=null&&trimg[0].title=="展开")
          {
             trimg[0].click();
          }
       }
       btnfold.value="折 叠";
    }
    
}
function imgclick2(btnfold)    //页面加载时单击img，将主计划折叠展示(对于主计划的查看)
{
    var btnvalue=btnfold.value;
    var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
    var treetr=treetable.getElementsByTagName("tr");
    if(btnvalue=="折 叠")
    {
       for(i=1;i<treetr.length;i++)
       {
          var trimg=treetr[i].getElementsByTagName("img");
//          var mark=treetr[i].getElementsByTagName("span")[13].innerHTML;
          if(trimg.length=="1"&&trimg[0].onclick!=null&&trimg[0].title=="折叠")
          {
             trimg[0].click();
          }
       }
       btnfold.value="展 开";
    }
    else if(btnvalue=="展 开")
    {
       for(i=1;i<treetr.length;i++)
       {
          var trimg=treetr[i].getElementsByTagName("img");
//          var mark=treetr[i].getElementsByTagName("span")[13].innerHTML;
          if(trimg.length=="1"&&trimg[0].onclick!=null&&trimg[0].title=="展开")
          {
             trimg[0].click();
          }
       }
       btnfold.value="折 叠";
    }
}

function ChangeChoose(obj,n)
{
    var e=obj;
    var qualityState=e.parentNode.getElementsByTagName("span")[0].innerHTML; //质检状态
    var qualitystatus=e.parentNode.parentNode.getElementsByTagName("td")[24].getElementsByTagName("span")[0].innerHTML;//是否报检
    var Upvalue=e.parentNode.getElementsByTagName("input")[0].value; //表示dropdownlist选择前的值
    var value=e.options[e.selectedIndex].value;  //dropdownlist变后的值
    if(qualitystatus=="0")//表示没有申请报检
    {
        if(value=="2"||value=="4") //制作中和制作完毕
        {
            e.parentNode.parentNode.getElementsByTagName("td")[24].getElementsByTagName("input")[6].value="1";//具备报检的条件
        }
        else
        {
            e.parentNode.parentNode.getElementsByTagName("td")[24].getElementsByTagName("input")[6].value="0";//表示不具备报检条件
        }
    }
    else  //已提交报检申请或者已报检
    {
        if(Upvalue=="2")  //表示选择前为：制作中、合格
        {
            if(qualityState=="合格")
            {
                if(value=="4") //选择制作完毕
                {
                    e.options[4].selected = true; 
                    e.parentNode.parentNode.getElementsByTagName("td")[24].getElementsByTagName("input")[n].value="3";
                }
                else
                {
                    e.options[2].selected = true; 
                }
            }
            else  //不合格
            {
                e.options[2].selected = true; 
            }
            e.parentNode.parentNode.getElementsByTagName("td")[24].getElementsByTagName("input")[6].value="1";//具备报检的条件
            
        }
        if(Upvalue=="4")  
        {
            if(qualityState=="不合格")  //制作完毕不合格
            {
                if(value=="2")
                {
                    e.options[2].selected = true; 
                    e.parentNode.parentNode.getElementsByTagName("td")[24].getElementsByTagName("input")[n].value="3"
                }
                else
                {
                    e.options[4].selected = true; 
                }
            }
            else if(qualityState=="")
            {
                e.options[4].selected = true; 
            }
            e.parentNode.parentNode.getElementsByTagName("td")[24].getElementsByTagName("input")[6].value="1";//具备报检的条件
        }
    }
}
function checkallcheckbox()
{
    var table=document.getElementById(getClientId().Id1);
    var tr=table.getElementsByTagName("tr");
    var n=0;
    for(i=1;i<tr.length;i++)
    {
        var cb=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(cb.checked)
        {
          n++;
        }
    }
    if(n==0)
    {
        alert('请勾选需要质检的任务！')
        return false;
    }
    else
    {
        return true;
    }
}
//检验数字格式
function checkedData(obj)
{
    var text=obj.value;
    var pattem=/^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$/; 
    if(!pattem.test(text))
    {
        obj.style.background="yellow";
        obj.value="0";
        alert('输入格式有误!');
        return false;
    }
    else 
    {
        obj.style.background="Transparent";
    }
}
function CheckIsChange(obj)
{
   if(obj.checked)
   {
       var bgstate=obj.parentNode.parentNode.getElementsByTagName("td")[17].getElementsByTagName("span")[4].innerHTML;
       if(bgstate!="0")
       {
           var bgpid=obj.parentNode.parentNode.getElementsByTagName("td")[17].getElementsByTagName("span")[5].innerHTML;
           alert('该任务已发生变更不能下推主计划，请在变更批号：'+bgpid+'中下推该任务！');
           obj.checked=false;
       }
   }
}

//对于采购主计划时间的约束
function PCMPPSTime(obj)   //计划开始
{
    dateCheck(obj);
}

function PCMPPFTime(obj)   //计划完成时间
{
    dateCheck(obj);
//  ISCheckPFPStime(obj,8);
//}
}
function PCMPRSTime(obj)  //实际开始时间
{
    dateCheck(obj);
    ISCheckPStime(obj,7);
}
function PCMPRFTime(obj)  //实际完成时间
{
    dateCheck(obj);
    ISCheckPFtime(obj,9);
}

function IsAddPDate()  //判断计划时间是否添加
{
   var table=document.getElementById(getClientId().Id1);
   var tr=table.getElementsByTagName("tr");
   for(i=1;i<tr.length;i++)
   {
       var pstime=tr[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;
       var pftime=tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
       if(pstime=="")
       {
           tr[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].style.background="yellow";
           alert('请添加计划开始时间！');
           return false;
       }
       else
       {
           tr[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].style.background="";
       }
       if(pftime=="")
       {
           tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].style.background="yellow";
           alert('请添加计划完成时间！');
           return false;
       }
       else
       {
           tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].style.background="";
       }
   }
}

function IsAddCGPDate()  //判断计划时间是否添加
{
   var table=document.getElementById(getClientId().Id1);
   var tr=table.getElementsByTagName("tr");
   for(i=1;i<tr.length;i++)
   {
       var pstime=tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
       var pftime=tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
       if(pstime=="")
       {
           tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].style.background="yellow";
           alert('请添加计划开始时间！');
           return false;
       }
       else
       {
           tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].style.background="";
       }
       if(pftime=="")
       {
           tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].style.background="yellow";
           alert('请添加计划完成时间！');
           return false;
       }
       else
       {
           tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].style.background="";
       }
   }
 }
 
 function quxian(session)
 {
  var m=0;
  var treetable=document.getElementById("ctl00_PrimaryContent_TreeGridViewEx1");
  var treetr=treetable.getElementsByTagName("tr");
  for(var i=1;i<treetr.length;i++)
    {
               var obj22=treetr[i].getElementsByTagName("input")[3];               
               var yqtime=obj22.value;
               var obj1=treetr[i].getElementsByTagName("input")[4];
               var jhsj=obj1.value; 
                           
               if(yqtime<jhsj&&session!="1003")
                 {
                   treetr[i].getElementsByTagName("input")[4].value="";
                   treetr[i].getElementsByTagName("input")[4].style.background="yellow";
                   m+=1;
                
                 }
                 
     }
    
     if(m>0&&session!="1003")
      {
         m=0;
         alert("存在计划完成时间大约要求完成时间项，请联系主管领导修改")
        
         return false;
        

       }
 
 }
