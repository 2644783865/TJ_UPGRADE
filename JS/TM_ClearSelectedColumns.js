var obj_table;
var obj_showDiv;
var obj_td;

//调用函数
function ClearColumns(type)
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
    obj_showDiv=document.getElementById("clear_div");
    obj_showDiv.className = "show";
    obj_td=document.getElementById("td_select");
    if(document.getElementById("mySelect")==null)
    {
       AddControls(type);
    }
}

function Hidden_clear_div()
{
    obj_showDiv=document.getElementById("clear_div");
    obj_showDiv.className = "hidden";
}

//添加控件
function AddControls(type)
{
    switch(type)
    {
        case 0:AddControls_MSAdjust();
                break;
        case 1:AddControls_BomOrgInput();
                break;
        case 2: AddControls_BomOrgEditInput();
               break;  
        case 3:AddControls_MSBomOrgInput();
               break;
        default:
        break;       
    }
}

//添加控件之制作明细调整时清空列
function AddControls_MSAdjust()
{
   var array_text_index=new Array(new Array());
   for(var i=0;i<6;i++)
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
   
   var mySelect = document.createElement("select");
   mySelect.id = "mySelect"; 
   obj_td.appendChild(mySelect);
   
   var my_select=document.getElementById('mySelect');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_select.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}
//BOM已输入，复制界面
function AddControls_BomOrgInput()
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
   
   var mySelect = document.createElement("select");
   mySelect.id = "mySelect"; 
   obj_td.appendChild(mySelect);
   
   var my_select=document.getElementById('mySelect');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_select.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}
//BOM已输入，修改界面
function AddControls_BomOrgEditInput()
{
   var array_text_index=new Array(new Array());
   for(var i=0;i<2;i++)
   {
     array_text_index[i]=new Array();
   }
   array_text_index[0][0]="图号";
   array_text_index[0][1]="2";
   
   array_text_index[1][0]="总序";
   array_text_index[1][1]="4";
   
   
   var mySelect = document.createElement("select");
   mySelect.id = "mySelect"; 
   obj_td.appendChild(mySelect);
   
   var my_select=document.getElementById('mySelect');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_select.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}
//明细录入模式界面
function AddControls_MSBomOrgInput()
{
   var array_text_index=new Array(new Array());
   for(var i=0;i<6;i++)
   {
     array_text_index[i]=new Array();
   }
   array_text_index[0][0]="明细序号";
   array_text_index[0][1]="2";
   
   array_text_index[1][0]="序号";
   array_text_index[1][1]="3";
   
   array_text_index[2][0]="图号";
   array_text_index[2][1]="4";
   
   array_text_index[3][0]="总序";
   array_text_index[3][1]="6";
   
   array_text_index[4][0]="中文名称";
   array_text_index[4][1]="7";
   
   array_text_index[5][0]="备注";
   array_text_index[5][1]="9";
   
   var mySelect = document.createElement("select");
   mySelect.id = "mySelect"; 
   obj_td.appendChild(mySelect);
   
   var my_select=document.getElementById('mySelect');
   for(var i=0;i<array_text_index.length;i++)
   {
        my_select.add(new Option(array_text_index[i][0],array_text_index[i][1])); 
   }
}
//添加清空行设置
function AddControls_ClearSrtRow(obj)
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
function AddControls_ClearEndRow(obj)
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

//添加确认按钮
function AddControls_Confirm()
{
    var row_start=document.getElementById("txtStartIndex").value;
    var row_end=document.getElementById("txtEndIndex").value;
    var my_select=document.getElementById('mySelect');
    var ColumnIndex=my_select.options[my_select.selectedIndex].value;
    var ColumnText=my_select.options[my_select.selectedIndex].text;
    var cfrm=window.confirm("确认清空【"+ColumnText+"】列吗？\r\r提示:从第【"+row_start+"】行至第【"+row_end+"】行");
    if(cfrm)
    {
        for(var i=row_start;i<=row_end;i++)
        {
             if(obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0]!=null&&obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0]!="")
             {
                obj_table.rows[i].cells[ColumnIndex].getElementsByTagName("input")[0].value="";
             }
        }
    }
    else
    {
       return false;
    }
}




