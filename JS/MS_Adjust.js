
    
    /*插入行提示*/
    function insert()
    {
        table=document.getElementById(getClientId().Grv);
        tr=table.getElementsByTagName("tr");
        objstr = '';
        for(var i=1;i<tr.length;i++)
        {
            obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
            if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
            {
                if(obj.checked)
                {
                    objstr="checked";
                    break;
                }
            }
        }
        if(objstr=="checked")
        {
            document.getElementById(getClientId().istid).value=i ;
        }
        else            
        {
            alert("请指定要插入行的位置!");
            return false;
        }
    }
    
    
    //CheckBox状态控制
    function CheckSelected(obj)
    {
       var id=obj.id;
       switch(id)
       {
           case "ctl00_PrimaryContent_cbxXuhaoCopy":
                 if(obj.checked)
                 {
                     document.getElementById(getClientId().cbxAutoXuhao).checked=false;
                     document.getElementById(getClientId().cbxXuhaoSame).checked=false;
                     document.getElementById(getClientId().cbxAutoTuhao).checked=false;
                     document.getElementById(getClientId().cbxAutoMsXuhao).checked=false;
                     document.getElementById(getClientId().lblshortcut).value="(当前:序号复制数据)";
                 }
                 else
                 {
                     document.getElementById(getClientId().lblshortcut).value="(当前:无)";
                 }
                 break;
           case "ctl00_PrimaryContent_cbxAutoXuhao":
                 if(obj.checked)
                 {
                     document.getElementById(getClientId().cbxXuhaoSame).checked=false;
                     document.getElementById(getClientId().cbxAutoTuhao).checked=false;
                     document.getElementById(getClientId().cbxXuhaoCopy).checked=false;
                     document.getElementById(getClientId().cbxAutoMsXuhao).checked=false;
                     document.getElementById(getClientId().lblshortcut).value="(当前:自动添加序号)";
                 }
                 else
                 {
                     document.getElementById(getClientId().lblshortcut).value="(当前:无)";
                 }
                 break;
           case "ctl00_PrimaryContent_cbxXuhaoSame":
                 if(obj.checked)
                 {
                     document.getElementById(getClientId().cbxAutoXuhao).checked=false;
                     document.getElementById(getClientId().cbxAutoTuhao).checked=false;
                     document.getElementById(getClientId().cbxXuhaoCopy).checked=false;
                     document.getElementById(getClientId().cbxAutoMsXuhao).checked=false;
                     document.getElementById(getClientId().lblshortcut).value="(当前:序号总序一致)";
                 }
                 else
                 {
                     document.getElementById(getClientId().lblshortcut).value="(当前:无)";
                 }
                 break;
           case "ctl00_PrimaryContent_cbxAutoTuhao":
                 if(obj.checked)
                 {
                     document.getElementById(getClientId().cbxAutoXuhao).checked=false;
                     document.getElementById(getClientId().cbxXuhaoSame).checked=false;
                     document.getElementById(getClientId().cbxXuhaoCopy).checked=false;
                     document.getElementById(getClientId().cbxAutoMsXuhao).checked=false;
                     document.getElementById(getClientId().lblshortcut).value="(当前:自动添加图号)";
                 }
                 else
                 {
                     document.getElementById(getClientId().lblshortcut).value="(当前:无)";
                 }
                 break;
          case "ctl00_PrimaryContent_cbxAutoMsXuhao":
                 if(obj.checked)
                 {
                     document.getElementById(getClientId().cbxAutoXuhao).checked=false;
                     document.getElementById(getClientId().cbxXuhaoSame).checked=false;
                     document.getElementById(getClientId().cbxXuhaoCopy).checked=false;
                     document.getElementById(getClientId().cbxAutoTuhao).checked=false;
                     document.getElementById(getClientId().lblshortcut).value="(当前:自动明细序号)";
                 }
                 else
                 {
                     document.getElementById(getClientId().lblshortcut).value="(当前:无)";
                 }
                 break;       
          default:
                     document.getElementById(getClientId().lblshortcut).value="(当前:无)";
                 break;
       }
    }
    //总序与序号一致
    function XuHaoSame(obj)
    {
       var checkbox=document.getElementById(getClientId().cbxXuhaoSame);
       var table=document.getElementById(getClientId().Grv);
       var rowNum=obj.parentNode.parentNode.rowIndex;
       
       if(checkbox.checked)
       {
          obj.value=table.rows[rowNum].cells[5].getElementsByTagName("input")[0].value;
       }
    }
    
    //默认序号带出
    function DefaultXuHao(obj)
    {
        var checkbox=document.getElementById(getClientId().cbxAutoXuhao);
        var tablerows=document.getElementById(getClientId().Grv).rows.length;
        var index=obj.parentNode.parentNode.rowIndex;

        if(checkbox.checked)
        {
            if(index==1)
            {
               return;
            }
            var xuhao=document.getElementById(getClientId().Grv).rows[index-1].cells[3].getElementsByTagName("input")[0].value;
            if(xuhao=="")
            {
               return;
            }
            if(obj.value==""&&xuhao!="")
            {
              if(!/^([0-9]+|[0-9]+(\.[1-9]{1}[0-9]*)*)$/g.test(xuhao))
              {
                    alert( "您输入的序号为"+xuhao+"；输入格式有误,请重新输入！！！\r\r提示：\r\r正确的输入格式为：m.n...(m、n为正整数) ");
                    return;
              }
              else
              {
                  if(index<=tablerows)
                  {
                    if(xuhao.indexOf(".")>0)
                    {
                        var tt=xuhao.split('.');
                        var now=parseInt(tt[tt.length-1])+1;
                        var temp=xuhao.substring(0,xuhao.lastIndexOf('.'));
                        var now_index=temp+"."+now;
                        document.getElementById(getClientId().Grv).rows[index].cells[3].getElementsByTagName("input")[0].value=now_index;
                    }
                  }
              }
            }
        }
    }
    
         //自动添加明细序号
     function AutoMsXuhao(obj)
     {
        var checkbox=document.getElementById(getClientId().cbxAutoMsXuhao);
        var tablerows=document.getElementById(getClientId().Grv).rows.length;
        var index=obj.parentNode.parentNode.rowIndex;

        if(checkbox.checked)
        {
            if(index==1)
            {
               return;
            }
            var xuhao=document.getElementById(getClientId().Grv).rows[index-1].cells[2].getElementsByTagName("input")[0].value;
            if(xuhao=="")
            {
               return;
            }
            if(obj.value==""&&xuhao!="")
            {
              if(index<=tablerows)
              {
                if(xuhao.indexOf(".")>0)
                {
                    var tt=xuhao.split('.');
                    var now=parseInt(tt[tt.length-1])+1;
                    var temp=xuhao.substring(0,xuhao.lastIndexOf('.'));
                    var now_index=temp+"."+now;
                    document.getElementById(getClientId().Grv).rows[index].cells[2].getElementsByTagName("input")[0].value=now_index;
                }
                else if(!isNaN(xuhao))
                {
                    document.getElementById(getClientId().Grv).rows[index].cells[2].getElementsByTagName("input")[0].value=parseInt(xuhao)+1;
                }
              }
            }
        }
     }
     //选择库时，体现在制作明细中
     function Ku_ShowInMs(obj)
     {
        var table=document.getElementById(getClientId().Grv);
        var index=obj.parentNode.parentNode.rowIndex;
        if(obj.value=="库")
        {
           table.rows[index].cells[7].getElementsByTagName("select")[0].selectedIndex=1;
        }

     }
     
    
    /*确定是否删除*/
    function check()
    {
        table=document.getElementById(getClientId().Grv);
        tr=table.getElementsByTagName("tr");
        objstr = '';
        for(var i=1;i<tr.length;i++)
        {
            obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
            if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
            {
                if(obj.checked)
                {
                    objstr="checked";
                    break;
                }
            }
        }
        if(objstr=="checked")
        {
           var j=confirm('确定删除吗？\r\r提示：页面上删除，该条记录存在！！！');
           if(j==true)
           {
                document.getElementById(getClientId().txtid).value=i ;
           }
           else
           {
                return false;
           }
        }
        else            
        {
            alert("请选择要删除的行！");
            return false;
        }
    }

    //选择一行
    function SelectOne(obj_ck)
    {
        var state=obj_ck.checked;
        table=document.getElementById(getClientId().Grv);
        tr=table.getElementsByTagName("tr");
        for(var i=1;i<tr.length;i++)
        {
            obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
            obj.checked=false;
        }
        if(state)
        {
          obj_ck.checked=true;
        }
        else
        {
           obj_ck.checked=false;
        }
    }

    //自动带出下一行图号
    function msAutoTuHao(obj)
    {
        if(document.getElementById(getClientId().cbxAutoTuhao).checked)
        {
            if(obj.value!="")
            {
                var table=document.getElementById(getClientId().Grv);
                var tablerows=table.rows.length;
                var tr=table.getElementsByTagName("tr");
                var index=obj.parentNode.parentNode.rowIndex;
                var t=parseInt(index)+1;
                if(t<tablerows)
                {
                   if(document.getElementById(getClientId().Grv).rows[t].cells[4].getElementsByTagName("input")[0].value=="")
                   {
                      document.getElementById(getClientId().Grv).rows[t].cells[4].getElementsByTagName("input")[0].value=obj.value;
                   }
                }
            }
        }
     }
     
     //清空序号列
     function ClearXuhaoColumn(index)
     {
        var ok=confirm("确认清空吗？");
        if(ok=true)
        {
            var grv=document.getElementById(getClientId().Grv);
            for(var i=1;i<grv.rows.length;i++)
            {
                grv.rows[i].cells[index].getElementsByTagName("input")[0].value="";
            }
        }
     }
     
     function ChangeGYLC(obj)
     {
        if(obj.value!="")
        {
           table=document.getElementById(getClientId().Grv);
           var index=obj.parentNode.parentNode.rowIndex;
           table.rows[index].cells[7].getElementsByTagName("select")[0].selectedIndex=1;
        }
     }


//添加库标识号
function AddKuBiaoshi()
{
   var ok=confirm("确认继续吗?\r\r提示:自动添加的标识号仅供参考，请您仔细核对！！！\r\r库标识号添加原则:\r\r（1）、含“库”项，【项目代号-】+图号+【N序号后缀】\r\r（2）、已添加项不会重复添加")
   if(ok)
   {
      var proid=document.getElementById(getClientId().lblproid).value;
      var table=document.getElementById(getClientId().Grv);
      var _tuhaobiaoshi;
      var _ku;
      var _xuhao;
      var _lastxuhao;
      var _tablerows=table.rows.length;
      if(_tablerows==0)
      {
         alert("页面无数据，无法操作！！！");
         return false;
      }
      
      var pattem=/^\d+$/;//数量验证
   

      var startindex=window.prompt('请输入添加【库标识号】的【起始行】:','1');  
      if(!pattem.test(startindex))
      {
          alert("输入起始行有误，程序已终止！！！");
          return false;
      }
      else if(startindex==0)
      {
          alert("输入起始行不能为0，程序已终止！！！");
          return false;
      }
      else if(parseInt(startindex)+1>parseInt(_tablerows))
      {
          alert("输入起始行超出最大行数，程序已终止！！！");
          return false;
      }
           
      var endindex=window.prompt('请输入添加【库标识号】的【结束行】:',parseInt(_tablerows)-1);  
      if(!pattem.test(endindex))
      {
          alert("输入结束行有误，程序已终止！！！");
          return false;
      }
      else if(endindex==0)
      {
          alert("输入结束行不能为0，程序已终止！！！");
          return false;
      }
      else if(parseInt(endindex)+1>parseInt(_tablerows))
      {
          alert("输入结束行超出最大行数，程序已终止！！！");
          return false;
      }
      
      for(var i=startindex;i<=endindex;i++)
      {
         _tuhaobiaoshi=table.rows[i].cells[4].getElementsByTagName("input")[0].value;
         _ku=table.rows[i].cells[8].getElementsByTagName("input")[0].value;
         _xuhao=table.rows[i].cells[5].getElementsByTagName("input")[0].value;//使用总序
         if(table.rows[i].cells[4].getElementsByTagName("input")[0].readOnly==false)
         {
             if(_ku=="库")
             {
                if(_tuhaobiaoshi.indexOf(proid+"-")!=0)
                {
                    _lastxuhao=_xuhao.substring(_xuhao.lastIndexOf(".")+1,_xuhao.length);
                    table.rows[i].cells[4].getElementsByTagName("input")[0].value=proid+"-"+_tuhaobiaoshi+"N"+_lastxuhao;
                }
             }
         }
      }
      
   }

}

//添加明细序号
function AddMsXuhao()
{
   var ok=confirm("确认继续吗?\r\r提示:自动添加的明细序号仅供参考，请您仔细核对！！！\r\r明细序号添加原则:\r\r（1）、【父级明细序号+当前序号后缀】\r\r（2）、无明细序号项添加")
   if(ok)
   {
      var table=document.getElementById(getClientId().Grv);
      var tablename=document.getElementById(getClientId().tablename).value;
      var tsaid=document.getElementById(getClientId().Taskid).value;
      var _xuhao;
      var _fjxuhao;
      var _lastxuhao;
      var _msxuhao;
      var _fjmsxuhao;
      var _tablerows=table.rows.length;
      
      
      if(_tablerows==0)
      {
         alert("页面无数据，无法操作！！！");
         return false;
      }
      
      var pattem=/^\d+$/;//数量验证
   

      var startindex=window.prompt("请输入添加【库标识号】的【起始行】:","1"); 
      if(!pattem.test(startindex))
      {
          alert("输入起始行有误，程序已终止！！！");
          return false;
      }
      else if(startindex==0)
      {
          alert("输入起始行不能为0，程序已终止！！！");
          return false;
      }
      else if(parseInt(startindex)+1>parseInt(_tablerows))
      {
          alert("输入起始行超出最大行数，程序已终止！！！");
          return false;
      }
      
      var endindex=window.prompt("请输入添加【库标识号】的【结束行】:",parseInt(_tablerows)-1);  
      if(!pattem.test(endindex))
      {
          alert("输入结束行有误，程序已终止！！！");
          return false;
      }
      else if(endindex==0)
      {
          alert("输入结束行不能为0，程序已终止！！！");
          return false;
      }
      else if(parseInt(endindex)+1>parseInt(_tablerows))
      {
          alert("输入结束行超出最大行数，程序已终止！！！");
          return false;
      }
      
      for(var i=startindex;i<=endindex;i++)
      {
          _msxuhao=table.rows[i].cells[2].getElementsByTagName("input")[0].value;
          _xuhao=table.rows[i].cells[3].getElementsByTagName("input")[0].value;
          if(_msxuhao!=""||_xuhao=="1")
          {
             continue;
          }
          _lastxuhao=_xuhao.substring(_xuhao.lastIndexOf("."),_xuhao.length);
          _fjxuhao=_xuhao.substring(0,_xuhao.lastIndexOf("."));
          
          var flag=0;
          for(var j=1;j<_tablerows;j++)
          {
             var _currentxuhao=table.rows[j].cells[3].getElementsByTagName("input")[0].value;
             if(_currentxuhao==_fjxuhao)
             {
                 _fjmsxuhao=table.rows[j].cells[2].getElementsByTagName("input")[0].value;
                 flag=1;
                 continue;
             }
          }
          
         if(flag==0)
         {
             _fjmsxuhao=GetFjMsXuHao(tablename,tsaid,_fjxuhao);
         }
          
          if(_fjmsxuhao!="")
          {
             table.rows[i].cells[2].getElementsByTagName("input")[0].value=_fjmsxuhao+_lastxuhao;
          }   
      }
   }
}

//必须同时存在“明细序号”、“体现”、“库”
function CheckMS_IN_KU()
{
      var table=document.getElementById(getClientId().Grv);
      var _ismanu;
      var _msxuhao;
      var _ku;
      var _gylc;
      var _tablerows=table.rows.length;
      for(var i=1;i<_tablerows;i++)
      {
          _ku=table.rows[i].cells[8].getElementsByTagName("input")[0].value;
          _gylc=table.rows[i].cells[11].getElementsByTagName("input")[0].value;
          _ismanu=table.rows[i].cells[7].getElementsByTagName("select")[0].selectedIndex;
          _msxuhao=table.rows[i].cells[2].getElementsByTagName("input")[0].value;
          
          if(_ku=="库")
          {
             if(_msxuhao=="")
             {
                alert("无法保存！！！\r\r提示:第【"+i+"】行中，添加了【库】，【明细序号】不能为空！！！");
                return false;
             }
             
             if(_ismanu=="0")
             {
                alert("无法保存！！！\r\r提示:第【"+i+"】行中，添加了【库】，明细必须体现！！！");
                return false;
             }
          }
          
          if(_gylc!="")
          {
             if(_msxuhao=="")
             {
                alert("无法保存！！！\r\r提示:第【"+i+"】行中，添加了【工艺流程】，【明细序号】不能为空！！！");
                return false;
             }
             
             if(_ismanu=="0")
             {
                alert("无法保存！！！\r\r提示:第【"+i+"】行中，添加了【工艺流程】，明细必须体现！！！");
                return false;
             }
          }
      }
}















//Table的←↑→↓控制
function grControlFocus(input)
{
  var e=event.srcElement;
  var rowIndex=e.parentNode.parentNode.rowIndex ; //获取行号
  var cellIndex=e.parentNode.cellIndex;  //获取焦点的列号
  var tr=e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
  var rowcount=tr.length;  //行数
  var td=e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
  var cellcount=td.length;    //列数
  /////  alert('共'+cellcount+'列;当前列'+cellIndex);
 ///// alert(td);
 /////var hmtlid=input.id.substring(input.id.lastIndexOf('_'),input.id.length); 
 /////alert(hmtlid);
 /////alert(cellIndex);
  var key=window.event.keyCode;   //获得按钮的编号
 
  if(key==37)   //向左 
  {
  //是否为第一列
      for(var i=cellIndex-1;i>0;i--)
      {
          if( tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=="hidden")
          {
              continue;
          }
          else
          {
              tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].focus();
              tr[rowIndex].style.backgroundColor ='#55DF55';
              break;
          }
      }
  }
  
  if(key==38)  //向上
  {
        for(var i=rowIndex-1;i>0;i--)
        {
           if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null||tr[i].getElementsByTagName("td")[cellIndex].className=='hidden')
            {
                tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
                tr[rowIndex].style.backgroundColor ='#EFF3FB';
                tr[i].style.backgroundColor ='#55DF55';
                break;
            }
            else
            {
               continue;
            }
        }
  }
  
  if(key==39)  //向右
  {
        for(var i=cellIndex+1;i<cellcount;i++)
        {
            if(i<cellcount)
            {
                 if(tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0]==null||tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].readOnly||tr[rowIndex].getElementsByTagName("td")[i].className=='hidden')//
                 {
                    continue;
                 }
                 else
                 {
                   tr[rowIndex].getElementsByTagName("td")[i].getElementsByTagName("input")[0].select();
                   tr[rowIndex].style.backgroundColor ='#55DF55';
                   break;
                 }
             }
             else
             {
               break;
             }
        } 
   } 
  
  if(key==40)   //向下
  {
     for(var i=rowIndex+1;i<rowcount;i++)
     {
       if(tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0]!=null||tr[i].getElementsByTagName("td")[cellIndex].className=='hidden')
       {
            tr[i].getElementsByTagName("td")[cellIndex].getElementsByTagName("input")[0].select();
            tr[rowIndex].style.backgroundColor ='#EFF3FB';//原来的行变回原来的颜色
            tr[i].style.backgroundColor ='#55DF55';//下一行变色
            break;
        }
        else
        {
           continue;
        }
      }
  }
}