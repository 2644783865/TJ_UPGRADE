   var mashape;//毛坯形状
   var lilunzhongliang;//理论重量
   var marname;//物料名称
   var marguige;//物料规格
   var dw;//单位
   var fixsize;//是否定尺
   var table;
   var taishu;
   var BXishu;
   var XXishu;
   var QTXishu="1";
   
   //自动带出序号
   function GetIndex(obj)
   {
       var tb=obj.parentNode.parentNode.parentNode;
       var rowno=obj.parentNode.parentNode.rowIndex;
       var cellno=obj.parentNode.cellIndex;
       var rowcount=tb.rows.length;
       if(rowno>1&&rowno<=rowcount&&obj.value=="")
       {
           var _qzxuhao=tb.rows[rowno-1].cells[cellno].getElementsByTagName("input")[0].value;
            if(_qzxuhao.indexOf(".")>0)
            {
                var tt=_qzxuhao.split('.');
                var now=parseInt(tt[tt.length-1])+1;
                var temp=_qzxuhao.substring(0,_qzxuhao.lastIndexOf('.'));
                var now_index=temp+"."+now;
                obj.value=now_index;
            }
            else
            {
                var now_index=parseInt(_qzxuhao)+1;
                obj.value=now_index;
            }
       }
   }
   
   //数量改变
   function cal_num(obj)
   {
      InitGrv();
      GetXiShu(fixsize);
      var tr=obj.parentNode.parentNode;
      var shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
      var dzh=tr.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
      var cailiaodzh=tr.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
      var len=tr.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
      var pattem=/^\d+(\.\d+)?$/;//数量验证
      
      if(!pattem.test(shuliang)||shuliang==0)
      {
         alert("数量格式不正确，请输入大于0的数值！！！");
         tr.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value="1";
         tr.getElementsByTagName("td")[8].getElementsByTagName("input")[1].value=taishu;
         tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=dzh;
         return;
      }
      
      tr.getElementsByTagName("td")[8].getElementsByTagName("input")[1].value=(shuliang*parseFloat(taishu)).toFixed(2);
      tr.getElementsByTagName("td")[8].getElementsByTagName("input")[2].value=(shuliang*parseFloat(taishu)).toFixed(2);
      shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[1].value;
      //单重验证
      if(!pattem.test(dzh))
      {
         alert("单重格式不正确！！！");
         tr.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
         tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="0";
         return;
      }
      //材料单重验证
      if(!pattem.test(cailiaodzh))
      {
         alert("单重格式不正确！！！");
         tr.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value="0";
         return;
      }
      
      tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=(dzh*shuliang).toFixed(2);
      if(mashape=="板")
      {
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*shuliang*BXishu).toFixed(2);
      }
      else if(mashape=="型"||mashape=="圆钢")
      {
         //材料总长
         tr.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=(len*shuliang*XXishu).toFixed(2);
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*shuliang*XXishu).toFixed(2);
      }
      else if(dw.indexOf("(米-")>-1||dw.indexOf("(M-")>-1||dw.indexOf("(m-")>-1)
      {
         //材料总长
         tr.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=(len*shuliang*QTXishu).toFixed(2);
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*shuliang*QTXishu).toFixed(2);
      }
      else
      {
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*shuliang*QTXishu).toFixed(2);
      }

   }
   
   //计划数量改变
   function cal_pnum(obj)
   {
      InitGrv();
      GetXiShu(fixsize);
      var tr=obj.parentNode.parentNode;
      var p_shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[2].value;
      var shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[1].value;
      var cailiaodzh=tr.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
      var len=tr.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
      var pattem=/^\d+(\.\d+)?$/;//数量验证
      
      if(!pattem.test(p_shuliang)||p_shuliang==0)
      {
         alert("数量格式不正确，请输入大于0的数值！！！");
         obj.focus();
         cal_num(tr.getElementsByTagName("td")[8].getElementsByTagName("input")[0]);
         return;
      }
      
      if(parseFloat(p_shuliang)<parseFloat(shuliang))
      {
         alert("【计划数量】不能小于【总数量】！！！");
         cal_num(tr.getElementsByTagName("td")[8].getElementsByTagName("input")[0]);
         return;
      }
            
      if(!pattem.test(cailiaodzh))
      {
         alert("【材料单重】格式不正确！！！");
         tr.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value="0";
         return;
      }
      
      if(mashape=="板")
      {
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*p_shuliang*BXishu).toFixed(2);
      }
      else if(mashape=="型"||mashape=="圆钢")
      {
         //材料总长
         tr.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=(len*p_shuliang*XXishu).toFixed(2);
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*p_shuliang*XXishu).toFixed(2);
      }
      else if(dw.indexOf("(米-")>-1||dw.indexOf("(M-")>-1||dw.indexOf("(m-")>-1)
      {
         //材料总长
         tr.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=(len*p_shuliang*QTXishu).toFixed(2);
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*p_shuliang*QTXishu).toFixed(2);
      }
      else
      {
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*p_shuliang*QTXishu).toFixed(2);
      }
      
   }
   
   //实际单重改变
   function cal_unitweight(obj)
   {
      InitGrv();
      var tr=obj.parentNode.parentNode;
      var shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[1].value;
      var dzhong=tr.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
      var pattem=/^\d+(\.\d+)?$/;//数量验证
      
      if(!pattem.test(shuliang)||shuliang==0)
      {
         alert("数量格式不正确，请输入大于0的数值！！！");
         return;
      }
            
      if(!pattem.test(dzhong))
      {
         alert("【实际单重】格式不正确！！！");
         tr.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
         tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value="0";
         return;
      }
      tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=(dzhong*shuliang).toFixed(2);;
   }
   
   //材料单重改变
   function cal_marunitweight(obj)
   {
      InitGrv();
      GetXiShu(fixsize);
      var tr=obj.parentNode.parentNode;
      var p_shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[2].value;
      var cailiaodz=tr.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
      var pattem=/^\d+(\.\d+)?$/;//数量验证
      
      if(!pattem.test(p_shuliang)||p_shuliang==0)
      {
         alert("数量格式不正确，请输入大于0的数值！！！");
         return;
      }
            
      if(!pattem.test(cailiaodz))
      {
         alert("【实际单重】格式不正确！！！");
         tr.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value="0";
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value="0";
         return;
      }
      
      if(mashape=="板")
      {
        tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodz*p_shuliang*BXishu).toFixed(2);
      }
      else if(mashape=="型"||mashape=="圆钢")
      {
        tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodz*p_shuliang*XXishu).toFixed(2);
      }
      else
      {
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodz*p_shuliang*QTXishu).toFixed(2);
      }
   }
   
   
   //面域改变
   function cal_my(obj)
   {
      InitGrv();
      GetXiShu(fixsize);
      var my=obj.value;
      var pattem=/^\d+(\.\d+)?$/;
      if(!pattem.test(my))
      {
         alert("面域格式不正确！！！");
         tr.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="0";
         return;
      }
      
      var tr=obj.parentNode.parentNode;
      var dzh=tr.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
      var shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[1].value;
      if(mashape=="板")
      {
          if(marname=="钢格板"||marname=="栅格板"||marname=="钢板网"||marname=="花纹板")
          {
             dzh=(my*lilunzhongliang).toFixed(2);
          }
          else
          {
             dzh=(my*lilunzhongliang*marguige).toFixed(2);
          }
      }
      else if(marname=="钢格板"||marname=="栅格板"||marname=="钢板网"||marname=="花纹板")
      {
         dzh=(my*lilunzhongliang).toFixed(2);
      }
      tr.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dzh;
      tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=(dzh*shuliang).toFixed(2);

   }
   //长-宽改变
   function cal_len_width(obj)
   {
      InitGrv();
      GetXiShu(fixsize);
      var tr=obj.parentNode.parentNode;
      var len=tr.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
      var width=tr.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
      var totallen=tr.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
      var shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[1].value;
      var p_shuliang=tr.getElementsByTagName("td")[8].getElementsByTagName("input")[2].value;
      var my=tr.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
      var danzhong;
      var zongzhong;
      var cailiaodz;
      var cailiaozhongzong;
      var guige;
      
      var pattem=/^\d+(\.\d+)?$/;
      
      if(!pattem.test(len))
      {
         alert("长度格式不正确！！！");
         tr.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value="0";
         return;
      }
      
      if(!pattem.test(width))
      {
         alert("宽度格式不正确！！！");
         tr.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="0";
         return;
      }
      
      if(!pattem.test(totallen))
      {
         alert("材料总长格式不正确！！！");
         tr.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value="0";
         return;
      }
      
      if(!pattem.test(my))
      {
         alert("面域格式不正确！！！");
         tr.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value="0";
         return;
      }
      
      if(mashape=="板"||marname.indexOf("花纹板")>-1||marname.indexOf("钢板网")>-1||marname.indexOf("钢格板")>-1||marname.indexOf("栅格板")>-1)
      {
          my=(len*width/1000000).toFixed(3);
          if(len!=0&&width!=0)
          {
              guige = marguige + 'x' + len + '+' + width;
              if(marname.indexOf("花纹板")>-1||marname.indexOf("钢板网")>-1||marname.indexOf("钢格板")>-1||marname.indexOf("栅格板")>-1)
              {
                 danzhong = (len * width * lilunzhongliang / 1000000).toFixed(2);
              }
              else
              {
                 danzhong = (len * width * lilunzhongliang*marguige / 1000000).toFixed(2);
              }
              cailiaodz=danzhong;
              zongzhong=(danzhong*shuliang).toFixed(2);
          }
          else
          {
              guige = marguige;
              danzhong=0;
              cailiaodz=0;
              zongzhong=(danzhong*shuliang).toFixed(2);
          }
          cailiaozhongzong=(cailiaodz*p_shuliang*BXishu).toFixed(2);
          
          tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=zongzhong;
          tr.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=danzhong;
          tr.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=my;
          tr.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=guige;
          tr.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=cailiaodz;
          tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=cailiaozhongzong;
      }
      else if(mashape=="型"||mashape=="圆钢")
      {
        if(len!=0)
        {
            guige = marguige + '+' + len;
            danzhong = (len * lilunzhongliang / 1000).toFixed(2);
            zongzhong=(danzhong*shuliang).toFixed(2);
            totallen=(len*p_shuliang*XXishu).toFixed(2);
            cailiaodz=danzhong;
            cailiaozhongzong=(cailiaodz*p_shuliang*XXishu).toFixed(2);
        }
        else
        {
            guige = marguige;
        }
        tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=zongzhong;
        tr.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=danzhong;
        tr.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=guige;
        tr.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=totallen;
        tr.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=cailiaodz;
        tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=cailiaozhongzong;
      }
      else if(dw.indexOf("(米-")>-1||dw.indexOf("(M-")>-1||dw.indexOf("(m-")>-1)
      {
         danzhong=(len * lilunzhongliang / 1000).toFixed(2);
         cailiaodz=danzhong;
         //材料总长
         tr.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=(len*p_shuliang*QTXishu).toFixed(2);
         //材料总重
         tr.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=(cailiaodzh*p_shuliang*QTXishu).toFixed(2);
         //材料总重
         tr.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=(danzhong*shuliang*QTXishu).toFixed(2);
      }
      
      if(dw.indexOf("(平米-")>-1||dw.indexOf("(平方米-")>-1||dw.indexOf("(m2-")>-1||dw.indexOf("(M2-")>-1||dw.indexOf("(㎡-")>-1)
      {
         if(len!=0&&width!=0)
         {
            tr.getElementsByTagName("td")[12].getElementsByTagName("input")[1].value=(len*width/1000000).toFixed(3);
         }
      }
   }
   
    function InitGrv()
    {
      table=document.getElementById(getClientId().GrvOrg);
      taishu=document.getElementById(getClientId().Number).innerHTML;
      mashape=table.rows[1].getElementsByTagName("td")[23].innerHTML;
      lilunzhongliang=table.rows[1].getElementsByTagName("td")[22].innerHTML;
      marname=table.rows[1].getElementsByTagName("td")[21].innerHTML;
      marguige=table.rows[1].getElementsByTagName("td")[5].innerHTML;
      dw=table.rows[1].getElementsByTagName("td")[25].innerHTML;
      fixsize=table.rows[1].getElementsByTagName("td")[24].innerHTML;
    }
   
   
function UpDownLeftRight(input)
{
  var e=event.srcElement;
  var rowIndex=e.parentNode.parentNode.rowIndex ; //获取行号
  var cellIndex=e.parentNode.cellIndex;  //获取焦点的列号
  var tr=e.parentNode.parentNode.parentNode.getElementsByTagName("tr"); //获取行
  var rowcount=tr.length;  //行数
  var td=e.parentNode.parentNode.getElementsByTagName("td"); //获取行的单元格
  var cellcount=td.length;    //列数
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

function GetXiShu(fix)
{
  if(fix=="Y")
  {
     BXishu="1";
     XXishu="1";
  }
  else
  {
     BXishu=document.getElementById(getClientId().BXishu).value;
     XXishu=document.getElementById(getClientId().XXishu).value;
  }
}