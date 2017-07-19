var obj_table;//目标表
var obj_ContentDiv;//承载Div
var obj_selecttd;//待清空列所在行

//调用的函数，清空的是哪一种类型
function CallfunctionOfConReplace(type)
{
    if(document.getElementById(getClientId().Grv)!=null)
    {
       obj_table=document.getElementById(getClientId().Grv);
    }
    else if(document.getElementById(getClientId().Id1)!=null)
    {
       obj_table=document.getElementById(getClientId().Id1);
    }
    else
    {
       obj_table=null;
    }
    
    obj_ContentDiv=document.getElementById("clear_div");
    obj_ContentDiv.className = "show";
    obj_selecttd=document.getElementById("td_select_conditionreplace");
    if(document.getElementById("mySelect_ConditionReplace")==null)
    {
       AddConReplaceControls(type);
    }
}

//添加待清空列列名及列号
function AddConReplaceControls(type)
{
    switch(type)
    { 
        case 1:
            ConEasyCopyEdit();
         break;
        case 2:
            ConEasyMSAdjust();
            break;
        case 3:
            ConEasyEdit();
            break;
        default:
         break;
    }
}

//批量复制界面
function ConEasyCopyEdit()
{
   var array_text_index=new Array(new Array());
   for(var i=0;i<4;i++)
   {
     array_text_index[i]=new Array();
   }
   array_text_index[0][0]="图号";
   array_text_index[0][1]="2";
   
   array_text_index[1][0]="总序";
   array_text_index[1][1]="4";

   array_text_index[2][0]="中文名称";
   array_text_index[2][1]="5";

   array_text_index[3][0]="备注";
   array_text_index[3][1]="6";
   
   var mySelect_ConditionReplace = document.createElement("select");
   mySelect_ConditionReplace.id = "mySelect_ConditionReplace"; 
   obj_selecttd.appendChild(mySelect_ConditionReplace);
   
   var my_selectReplace=document.getElementById('mySelect_ConditionReplace');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_selectReplace.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}
//批量复制界面
function ConEasyEdit()
{
   var array_text_index=new Array(new Array());
   for(var i=0;i<3;i++)
   {
     array_text_index[i]=new Array();
   }
   array_text_index[0][0]="图号";
   array_text_index[0][1]="2";
   
   array_text_index[1][0]="中文名称";
   array_text_index[1][1]="5";

   array_text_index[2][0]="备注";
   array_text_index[2][1]="6";
   
   var mySelect_ConditionReplace = document.createElement("select");
   mySelect_ConditionReplace.id = "mySelect_ConditionReplace"; 
   obj_selecttd.appendChild(mySelect_ConditionReplace);
   
   var my_selectReplace=document.getElementById('mySelect_ConditionReplace');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_selectReplace.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}

//明细调整界面
function ConEasyMSAdjust()
{
   var array_text_index=new Array(new Array());
   for(var i=0;i<7;i++)
   {
     array_text_index[i]=new Array();
   }
   array_text_index[0][0]="明细序号";
   array_text_index[0][1]="2";
   
   array_text_index[1][0]="序号";
   array_text_index[1][1]="3";
   
   array_text_index[2][0]="图号";
   array_text_index[2][1]="4";
   
   array_text_index[3][0]="库";
   array_text_index[3][1]="8";
   
   array_text_index[4][0]="规格";
   array_text_index[4][1]="9";
   
   array_text_index[5][0]="工艺流程";
   array_text_index[5][1]="11";
   
   array_text_index[6][0]="备注";
   array_text_index[6][1]="12";
   
   var mySelect_ConditionReplace = document.createElement("select");
   mySelect_ConditionReplace.id = "mySelect_ConditionReplace"; 
   obj_selecttd.appendChild(mySelect_ConditionReplace);
   
   var my_selectReplace=document.getElementById('mySelect_ConditionReplace');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_selectReplace.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}


function Hidden_clear_div()
{
    obj_showDiv=document.getElementById("clear_div");
    obj_showDiv.className = "hidden";
}


//添加清空行设置
function AddControls_ConReplaceSrtRow(obj)
{
   var pattem=/^\d+$/;//数量验证
   var rowindex=obj.value;
   var rowcount=obj_table.rows.length-1;
   if(!pattem.test(rowindex))
   {
      alert("输入行数错误，请重新输入!!!");
      obj.value="1";
   }
   if(rowindex>rowcount)
   {
      alert("输入行数超出最大行数["+rowcount+"]！！！");
      obj.value="1";
   }
}
function AddControls_ConReplaceEndRow(obj)
{
   var pattem=/^\d+$/;//数量验证
   var rowindex=obj.value;
   var rowcount=obj_table.rows.length-1;
   if(!pattem.test(rowindex))
   {
      alert("输入行数错误，请重新输入!!!");
      obj.value="1";
   }
   if(rowindex>rowcount)
   {
      alert("输入行数超出最大行数["+rowcount+"]！！！");
      obj.value=rowcount;
   }
}


//添加确认按钮:替换
function AddControls_ConReplaceConfirm()
{
    var row_start=document.getElementById("txtConditionReplaceStrIndex").value;
    var row_end=document.getElementById("txtConditionReplaceEndIndex").value;
    var my_select=document.getElementById('mySelect_ConditionReplace');
    var ColumnIndex=my_select.options[my_select.selectedIndex].value;
    var ColumnText=my_select.options[my_select.selectedIndex].text;
    var ReplaceOldContent=document.getElementById("txtStartContent").value;
    var ReplaceNewContent=document.getElementById("txtEndContent").value;
    var AddContent=document.getElementById("txtConditionContent").value;
    
    if(ReplaceOldContent=="")
    {
       alert("请输入替换条件！！！");
       document.getElementById("txtStartContent").focus();
       return false;
    }
    
    if(ReplaceNewContent=="")
    {
       alert("请输入替换条件！！！");
       document.getElementById("txtEndContent").focus();
       return false;
    }
    
    var cfrm=window.confirm("确认替换【"+ColumnText+"】列吗？\r\r提示:从第【"+row_start+"】行至第【"+row_end+"】行");
    if(cfrm)
    {
        for(var i=row_start;i<=row_end;i++)
        {
             if(obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0]!=null&&obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0]!="")
             {
                var obj=obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0];
                var obj_replace=obj.value;
                var totalen=obj_replace.length;
                
                var strindex=obj_replace.indexOf(ReplaceOldContent);
                if(strindex==-1)
                {
                   alert("替换至第【"+i+"】行时终止！！！\r\r提示:第【"+i+"】行中替换条件【"+ReplaceOldContent+"】不存在，请重新输入替换条件！！！");
                   document.getElementById("txtStartContent").focus();
                   return false;
                }
                
                var obj_endreplace=obj_replace.substring(strindex+1,totalen);
                
                var endindex=obj_endreplace.indexOf(ReplaceNewContent);
                
                if(endindex==-1)
                {
                   alert("替换至第【"+i+"】行时终止！！！\r\r提示:除开起始替换内容后，第【"+i+"】行中替换条件【"+ReplaceNewContent+"】不存在，请重新输入替换条件！！！");
                   document.getElementById("txtEndContent").focus();
                   return false;
                }
                
                var head=obj_replace.substring(0,strindex+ReplaceOldContent.length);
                var body=AddContent;
                var leg=obj_endreplace.substring(endindex,obj_endreplace.length);
                obj.value=head+body+leg;
             }
        }
    }
    else
    {
       return false;
    }
}