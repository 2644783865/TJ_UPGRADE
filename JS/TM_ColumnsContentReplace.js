var obj_table;//目标表
var obj_ContentDiv;//承载Div
var obj_selecttd;//待清空列所在行

//调用的函数，清空的是哪一种类型
function CallfunctionOfReplace(type)
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
    obj_selecttd=document.getElementById("td_select_replace");
    if(document.getElementById("mySelect_Replace")==null)
    {
       AddReplaceControls(type);
    }
}

//添加待清空列列名及列号
function AddReplaceControls(type)
{
    switch(type)
    { 
        case 1:
            EasyCopyEdit();
         break;
        case 2:
            EasyMSAdjust();
            break;
        case 3:
            EasyEdit();
            break;
        default:
         break;
    }
}

//批量复制界面
function EasyCopyEdit()
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
   
   var mySelect_Replace = document.createElement("select");
   mySelect_Replace.id = "mySelect_Replace"; 
   obj_selecttd.appendChild(mySelect_Replace);
   
   var my_selectReplace=document.getElementById('mySelect_Replace');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_selectReplace.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}
//批量复制界面
function EasyEdit()
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
   
   var mySelect_Replace = document.createElement("select");
   mySelect_Replace.id = "mySelect_Replace"; 
   obj_selecttd.appendChild(mySelect_Replace);
   
   var my_selectReplace=document.getElementById('mySelect_Replace');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_selectReplace.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}

//明细调整界面
function EasyMSAdjust()
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
   
   var mySelect_Replace = document.createElement("select");
   mySelect_Replace.id = "mySelect_Replace"; 
   obj_selecttd.appendChild(mySelect_Replace);
   
   var my_selectReplace=document.getElementById('mySelect_Replace');
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
function AddControls_ReplaceSrtRow(obj)
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
function AddControls_ReplaceEndRow(obj)
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
function AddControls_ReplaceConfirm()
{
    var row_start=document.getElementById("txtReplaceStrIndex").value;
    var row_end=document.getElementById("txtReplaceEndIndex").value;
    var my_select=document.getElementById('mySelect_Replace');
    var ColumnIndex=my_select.options[my_select.selectedIndex].value;
    var ColumnText=my_select.options[my_select.selectedIndex].text;
    var ReplaceOldContent=document.getElementById("txtOldContent").value;
    var ReplaceNewContent=document.getElementById("txtNewContent").value;
    
//////////    if(ReplaceOldContent=="")
//////////    {
//////////       alert("请输入要替换的内容！！！");
//////////       document.getElementById("txtOldContent").focus();
//////////       return false;
//////////    }
    
    var cfrm=window.confirm("确认替换【"+ColumnText+"】列吗？\r\r提示:从第【"+row_start+"】行至第【"+row_end+"】行");
    if(cfrm)
    {
        for(var i=row_start;i<=row_end;i++)
        {
             if(obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0]!=null&&obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0]!="")
             {
                var obj_replace=obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0];
                if(obj_replace.value.trim()=="")
                {
                   obj_replace.value=ReplaceNewContent;
                }
                else
                {
                   obj_replace.value=obj_replace.value.replace(ReplaceOldContent,ReplaceNewContent);
                }
             }
        }
    }
    else
    {
       return false;
    }
}