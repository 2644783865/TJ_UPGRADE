/*外购加工JS文件*/
function ChangeZongxu(input)  //改变总序
{
   var zongxu=document.getElementById(input.id);
   var tr=zongxu.parentNode.parentNode;
   var pattem=/^\d+(\.\d+)?$/;//数量验证
   if(zongxu.value.trim()!="")            
   {
       if(!/^([0-9]+|[0-9]+(\.[1-9]{1}[0-9]*)*)$/g.test(zongxu.value.trim()))
       {
           alert( "您输入的总序为"+zongxu.value.trim()+"；输入格式有误,请重新输入！！！\r\r提示：\r\r正确的输入格式为：A、A.m或A.m.n(m、n为整数，0<m<100,n>0,A为生产制号\"-\"后的数值) ");
           zongxu.focus();
       }
       else
       {
           var dw=tr.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
           if(dw=="")
           {
              tr.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value="个";//单位
           }   
       }
    }
    else
    {
       alert("总序不能为空，请输入总序！");
       zongxu.focus();
    }
}

function WG_ChangeTuhao(input)
{
   var Num=document.getElementById(input.id);
   var tr=Num.parentNode.parentNode;
   var tuhao=tr.getElementsByTagName("td")[2].getElementsByTagName("input")[0].value;
   var zjm=tr.getElementsByTagName("td")[2].getElementsByTagName("input")[1].value;
   if(zjm=="")
   {
      tr.getElementsByTagName("td")[2].getElementsByTagName("input")[1].value=tuhao;
   }
}

function ChangeNum(input)  //改变数量
{
   var Num=document.getElementById(input.id);
   var tr=Num.parentNode.parentNode;
   var pattem=/^[1-9][0-9]*$/;//数量验证(整数)
   if(Num.value.trim()!="")
   {
       var text=Num.value.trim();
       if(!pattem.test(text))
       {
          alert("输入的数量格式有误，请输入正确的数字格式！");
          input.value="1";
          ChangeNum(input);
          Num.style.background="yellow";
          Num.focus();
       }
       else
       {
           var number=1;
           if(document.getElementById(getClientId().Id4)!=null)
           {
             number=document.getElementById(getClientId().Id4).value;
           }
          text=text*number;
          tr.getElementsByTagName("td")[6].getElementsByTagName("input")[1].value=text;
          tr.getElementsByTagName("td")[6].getElementsByTagName("input")[2].value=text;
          var danzhong=tr.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.trim();//单重
          tr.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=danzhong*(text);//总重
          Num.style.background="white";
       }
   }
   else
   {
       alert("数量不能为空，请输入！！！")
       Num.style.background="yellow";
       Num.focus();
   }
}

function ChangeP_Num(input)  //改变计划数量
{
   var Num=document.getElementById(input.id);
   var tr=Num.parentNode.parentNode;
   var pattem=/^[1-9][0-9]*$/;//数量验证(整数)
   if(Num.value.trim()!="")
   {
       var text=Num.value.trim();
       if(!pattem.test(text))
       {
          alert("输入的数量格式有误，请输入正确的数字格式！");
          var obj=tr.getElementsByTagName("td")[6].getElementsByTagName("input")[0];
          ChangeNum(obj);
          Num.style.background="yellow";
          Num.focus();
       }
       else
       {
          var totalshuliang=tr.getElementsByTagName("td")[6].getElementsByTagName("input")[1].value;
          if(totalshuliang=="")
          {
              alert("请输入【单台数量】！！！");
              input.value="";
              return;
          }
          if(text<totalshuliang)
          {
             alert("计划数量不能小于总数量");
             var obj=tr.getElementsByTagName("td")[6].getElementsByTagName("input")[0];
             ChangeNum(obj);
          }
          else
          {
              var danzhong=tr.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.trim();//单重
              tr.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=danzhong*(text);//总重
              Num.style.background="white";
          }
       }
   }
   else
   {
       alert("数量不能为空，请输入！！！")
       Num.style.background="yellow";
       Num.focus();
   }
}


function ChangeFloatNum(input)  //浮点数验证
{
   var Num=document.getElementById(input.id);
   var tr=Num.parentNode.parentNode;
   var pattem=/^\d+(\.\d+)?$/;
   if(Num.value.trim()!="")
   {
       var text=Num.value.trim();
       if(!pattem.test(text))
       {
          alert("输入的数量格式有误，请输入正确的数字格式！");
          input.value="0";
          Num.style.background="yellow";
          Num.focus();
       }
       else
       {
          Num.style.background="white";
       }
   }
   else
   {
       alert("面域不能为空，请输入！！！")
       Num.style.background="yellow";
       Num.focus();
   }
}
function ChangeUweigh(input)
{
   var danzhong=document.getElementById(input.id);
   var tr=danzhong.parentNode.parentNode;
   var pattem=/^\d+(\.\d+)?$/;//实际单重
   if(danzhong.value.trim()!="")
   {
      var text=danzhong.value.trim();
      if(!pattem.test(text))
      {
          alert("输入的单重格式有误，请输入正确的单重格式！");
          danzhong.style.background="yellow";
          danzhong.focus();
      }
      else
      {
          var shuliang=tr.getElementsByTagName("td")[6].getElementsByTagName("input")[1].value;//数量
          tr.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=shuliang*(text);//总重
          danzhong.style.background="white";
      }
   }
   else
   {
       alert("单重不能为空，请输入单重！")
       danzhong.style.background="yellow";
       danzhong.focus();
   }
}

function ChangeUnit(input)
{
   var unit=input.value.trim();
   if(unit=="")
   {
      input.style.background="yellow";
   }
   else
   {
      input.style.background="white";
      if(unit=="米"||unit=="m"||unit=="M"||unit=="吨"||unit=="T"||unit=="t")
      {
         alert("不能输入长度和重量单位！！！");
         input.value="个";
         input.focus();
      }
      else if(unit.indexOf("/")>-1)
      {
         alert("请输入正确的单位！！！");
         input.value="个";
         input.focus();
      }
      else if(unit!="个"&&unit!="根"&&unit!="支"&&unit!="套"&&unit!="片"&&unit!="台"&&unit!="块"&&unit!="立方"&&unit!="立方米")
      {
         alert("请确认【单位】是否正确！！！");
         input.style.background="yellow";
         input.focus();
      }
      else
      {
         input.style.background="white";
      }
   }
}
