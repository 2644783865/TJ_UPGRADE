<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="FM_Invoice.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Invoice" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    <table width="100%">
        <tr>
            <td style="width: 80%">
                <asp:Label ID="lblInvState" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript" language="javascript">
    function fpdy(fpbh)
   {   
    var result=showModalDialog('FM_PRINT.aspx?fpbh='+fpbh,'subpage','dialogWidth:800px;dialogHeight:400px;center:yes;help:no;resizable:no;status:no'); //打开模态子窗体,并获取返回值
    window.location.href = window.location.href;
   }
    
    
    
    function ShowViewModal() {
     
        ID= document.getElementById("<%=txtGI_CODE.ClientID %>").value;
        
        var retVal = window.showModalDialog("FM_InvoiceDetail.aspx?incode="+ID+"&&date="+Math.random(),'',"dialogWidth=1000px;dialogHeight=580px;help=no;");
        if(retVal!=null)
        window.location.reload()
    }
    
    function Check(obj) 
    {        
        var objs=obj.parentNode.parentNode.getElementsByTagName("td")[0].getElementsByTagName("input")[0];  
        if(objs.type.toLowerCase() == "checkbox" )
            objs.checked = true;
    }
    
     function amountcheck(obj) 
    {      
        Check(obj);  
        Num=obj.value;
        newchar = ""; 
        if(Num=="")
        {
           alert("金额不能为空!");
           obj.focus();
           return newchar;
        }
        for(i=Num.length-1;i>=0;i--)
        {
          Num = Num.replace(",","")//替换tomoney()中的“,”
        }
       if(isNaN(Num)) 
       { //验证输入的字符是否为数字
         alert("请检查小写金额是否正确!");
         obj.focus();
         return newchar;
       }
      
    }
    
    
    //数量变化
    function amountchecksll(obj)
    {
    
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;    
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;      
           
            if(hsdj!=null)
            {
              //单价
              dj=(hsdj/(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
              
             //金额
              je=(hsdj/(1+sl/100)*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
              
              //含税金额
              hjje=(wlsl*hsdj).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
              
              //税额
              se=(hjje-je).toFixed(2);      
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;

            }
            
            Statistic();
    
    
    }
    
    
    
    
    //含税单价变化 修改单价，金额，税额，含税金额
    function amountcheckhsdj(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;    
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;      
           
            if(hsdj!=null)
            {
              //单价
              dj=(hsdj/(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
            
             //金额
              je=(hsdj/(1+sl/100)*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
              
              //含税金额
              hjje=(wlsl*hsdj).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
             
              //税额
              se=(hjje-je).toFixed(2);      
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;

            }
            
            Statistic();
    }
      //税率变化 修改单价，金额，税额，含税金额
    function amountchecksl(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;  //数量  
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;//含税单价
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;//税率
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;//单价
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;//金额
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;//税额
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;  //含税金额    
           
            if(sl!=null)
            {
              //单价
              dj=(hsdj/(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
              //金额
              je=(hsdj/(1+sl/100)*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
              
//              //含税金额
//              hjje=(wlsl*hsdj).toFixed(2);
//              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
              se=(hjje-je).toFixed(2);      
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
             

            }
             
            Statistic();
    }
    
    //含税金额变化修改含税单价，单价，金额，税额
     function amountcheckhjje(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;    
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;        
           
            if(hjje!=null)
            {
              //含税单价
              hsdj=(hjje/wlsl).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=hsdj;
              
              //单价
              dj=(hsdj/(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
              
              //金额
              je=(dj*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
                         
              //税额
              se=(hjje-je).toFixed(2);         
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
            }
           
            Statistic();
            
    }
    
    //单价变化修改 含税单价，含税金额，金额，税额
     function amountcheckdj(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");           
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;    
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;        
           
            if(dj!=null)
            {
            
              //含税单价
              hsdj=(dj*(1+sl/100)).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=hsdj;
              
              //金额
              je=(dj*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
               
               //含税金额
              hjje=(dj*(1+sl/100)*wlsl).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
              
                //税额          
              se=(hjje-je).toFixed(2);         
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
            }
            
            Statistic();
    }
    
    //金额变化修改 含税单价，单价，含税金额，税额
    function amountcheckje(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            var tr=table.getElementsByTagName("tr");       
            //数量    
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value; 
            //含税单价  
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            //税率
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            //单价
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            //金额
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            //税额
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            //含税金额
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;        
           
            if(je!=null)
            {
            
//             //含税单价
//              hsdj=(je/wlsl*(1+sl/100)).toFixed(4);
//              obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=hsdj;
              
              //单价
              dj=(je/wlsl).toFixed(4);
              obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=dj;
              
//              //含税金额
//              hjje=(dj*(1+sl/100)*wlsl).toFixed(2);
//              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje;
              
                //税额           
              se=(hjje-je).toFixed(2);         
              obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=se;
             
            }
             
            Statistic();
    }
    
     //税额变化修改 含税单价，单价，含税金额，税额
    function amountcheckse(obj) 
    {        
       
            var table=document.getElementById("ctl00_PrimaryContent_grvInvDetail");
            
            var tr=table.getElementsByTagName("tr");       
            //数量    
            var wlsl=obj.parentNode.parentNode.getElementsByTagName("td")[6].getElementsByTagName("input")[0].value; 
            //含税单价  
            var hsdj=obj.parentNode.parentNode.getElementsByTagName("td")[7].getElementsByTagName("input")[0].value;
            //税率
            var sl=obj.parentNode.parentNode.getElementsByTagName("td")[8].getElementsByTagName("input")[0].value;
            //单价
            var dj=obj.parentNode.parentNode.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
            //金额
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            //税额
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            //含税金额
            var hjje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;        
           
            if(se!=null)
            {
              hjje=(parseFloat(je)+parseFloat(se)); 
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hjje.toFixed(2);

            }
                         
            Statistic();
    }
    
    
    
     /*数据统计函数*/
        function Statistic() {
            var je = 0;
            var se = 0;
            var hsje = 0;
            var gv1 = document.getElementById("<%=grvInvDetail.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++)
            {
                var val1 = gv1.rows[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
                je += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
                se += parseFloat(val2);
                var val3 = gv1.rows[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
                hsje += parseFloat(val3);
            }
            var lbtn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[10].getElementsByTagName("span")[0];
            lbtn.innerHTML = je.toFixed(2);
            var lbtq = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[11].getElementsByTagName("span")[0];
            lbtq.innerHTML = se.toFixed(2);
            var lbta = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[12].getElementsByTagName("span")[0];
            lbta.innerHTML = hsje.toFixed(2);
              
        }

    //修改价格
    function xgPass()
    {
     return confirm("钢材汇总后数量不可拆分，只能整条物料全部勾稽！");
    }
    
    
    //取消钩稽
    function trickoff()
    {
      return confirm("确认取消钩稽吗？\r\r提示：发票编号【"+document.getElementById("<%=txtGI_CODE.ClientID %>").value+"】进入审核状态，请重新勾稽！！！")
    }
    //审核通过
    function auditPass()
    {
      return confirm("确认审核通过吗？\r\r提示：审核通过后钩稽！！！");
    }
    //审核驳回
    function auditReject()
    {
      return confirm("确认审核驳回吗？\r\r提示：将删除发票编号【"+document.getElementById("<%=txtGI_CODE.ClientID %>").value+"】待审核的所有信息！")
    }
    //钩稽通过
    function trickPass()
    {
      return confirm("确认钩稽吗？")
    }
    //钩稽驳回
    function trickReject()
    {
      return confirm("确认钩稽驳回吗？\r\r提示：钩稽驳回后发票信息将进入审核状态！！！")
    }
         
         
     function qrgj()
    {
    
        var gv1 = document.getElementById("<%=grvInvDetail.ClientID %>");
       
        for (i = 1; i < gv1.rows.length - 1; i++)
            {
              if(gv1.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked==false)
              {
               gv1.rows[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=0;
               gv1.rows[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=0;
               gv1.rows[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=0;
               gv1.rows[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=0;
               gv1.rows[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=0;
               gv1.rows[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=0;
              
              }
            }
            
       for (i = 1; i < gv1.rows.length - 1; i++)
            {
              if(gv1.rows[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value==0)
              {
              gv1.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
              }
              else
              {
              gv1.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
              }
            }
            
            Statistic();
    }     
    
    function qx()
    {
        var gv1 = document.getElementById("<%=grvInvDetail.ClientID %>");
       
        for (i = 1; i < gv1.rows.length - 1; i++)
        {
          var obj=gv1.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
          if(obj.checked==true)
          {
            for (j = i+1; j < gv1.rows.length - 1; j++)
            {
              var nextobj=gv1.rows[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
              if(nextobj.checked==true)
              {
                 for(var k=i+1;k<j;k++)
                 {
                   gv1.rows[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                 }
              
              }
              
            }
          }
        }
    }
    
    function xc()
    {
        var gv1 = document.getElementById("<%=grvInvDetail.ClientID %>");
       
        for (i = 1; i < gv1.rows.length - 1; i++)
        {
          gv1.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
        }
     }
      
      
      //生成凭证号
     window.onload=function(){
             var gv1 = document.getElementById("<%=grvInvDetail.ClientID %>");
             var tbbaseinfo=document.getElementById("tbbaseinfo");
             var autopzh="";
             var rkcodenow="";
             if(tbbaseinfo.getElementsByTagName("tr")[1].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=="")
             {
                     for(i = 1; i < gv1.rows.length - 1; i++)
                     {
                         if(gv1.rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML!=rkcodenow)
                         {
                             rkcodenow=gv1.rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                             if(autopzh=="")
                             {
                                 autopzh=gv1.rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                             }
                             else
                             {
                                 autopzh+="/"+gv1.rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                             }
                         }
                     }
                     tbbaseinfo.getElementsByTagName("tr")[1].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=autopzh;
             }
     } 
    </script>

    <div class="RightContent">
        <div class="box-wrapper">
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="98%">
                            <tr>
                                 <td>
                                 先勾选需要勾稽的明细，然后点击【勾稽明细确认】
                                 </td>
                                 <td>
                                    <input id="btnqx" type="button" value="连选" onclick="qx()"/>
                                    <input id="btnxc" type="button" value="取消" onclick="xc()"/>
                                </td>
                                <td>
                                    <input id="btngjqr" type="button" value="勾稽明细确认" onclick="qrgj()"/>
                                </td>
                                <td align="right" >
                                    <asp:Button ID="btnsave" runat="server" Text="修改价格" OnClick="btnsave_Click" OnClientClick="javascript:return xgPass()" />
                                </td>
                                <td align="right" >
                                    <asp:Button ID="btnbaocun" runat="server" Text="保存" OnClick="btnbaocun_Click" Visible="false" />
                                </td>
                                <td align="right" >
                                    <asp:Button ID="btnAuditPass" runat="server" Text="审核通过" 
                                        OnClick="btnAuditPass_Click" />
                                </td>
                                <td align="right" >
                                    <asp:Button ID="btnAuditReject" runat="server" Text="审核驳回" OnClick="btnAuditReject_Click"
                                         />
                                </td>
                                <td align="right" >
                                    <asp:Button ID="btnTrickPass" runat="server" Text="钩稽通过" OnClick="btnTrickPass_Click"
                                        OnClientClick="javascript:return trickPass();" />
                                </td>
                                <td align="right" >
                                    <asp:Button ID="btnTrickReject" runat="server" Text="钩稽驳回" OnClick="btnTrickReject_Click"
                                        OnClientClick="javascript:return trickReject();" />
                                </td>
                                <td  align="center">
                                    <asp:Button ID="btnTrickOff" runat="server" Text="取消钩稽" OnClientClick="javascript:return trickoff()"
                                        OnClick="btnTrickOff_Click" />
                                </td>
                                
                                <td align="right" >
                                    <a href="FM_Invoice_Managemnt.aspx" title="返回到 发票管理界面">返回</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table width="98%">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnprint" runat="server" Text="到打印页面" OnClick="btnprint_click" />&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                  
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                        width="98%" id="tbbaseinfo">
                        <tr>
                            <td align="right">
                                <strong>编号</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGI_CODE" runat="server"></asp:TextBox>
                            </td>
                            <td align="right">
                                <strong>供应商名称</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGI_SUPPLIERNM" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <strong>发票号码</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGI_INVOICENO" runat="server"></asp:TextBox>
                            </td>
                            <td align="right">
                                <strong>凭证号</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGI_PZH" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <strong>勾稽标志</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblGI_GJFLAG" Enabled="false" runat="server" RepeatColumns="2"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <strong>登记日期</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGI_DATE" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:Panel ID="Panel1" runat="server" Visible="False">
                            <tr>
                                <td align="right">
                                    <strong>核算标志</strong><%--<strong>红蓝字</strong>--%>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblSSHS" Enabled="false" runat="server" RepeatColumns="2"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right">
                                    <strong>审核标志</strong>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblGI_STATE" Enabled="false" runat="server" RepeatColumns="2"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>开户银行</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGI_ACCBANK" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <strong>地址</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGI_ADDRESS" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>部门</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGI_DEPNM" Text="财务部" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <strong>记账人</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGI_JZNM" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>制单人</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGI_ZDNM" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <strong>备注</strong>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtGI_NOTE" runat="server" TextMode="MultiLine" Width="80%" Height="25px"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                 
                 
                    <yyc:SmartGridView ID="grvInvDetail" Width="98%" CssClass="toptable grid" runat="server"
                        DataKeyNames="WG_CTAXUPRICE,UNIQUEID,WG_CTYPE,WG_CODE" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        ShowFooter="True" OnRowDataBound="grvInvDetail_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" BorderStyle="None" /><%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="入库单号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbfp" runat="server" Text='<%#Eval("WG_CODE").ToString()%>'> </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbheji" runat="server" Text="合计:"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料编码" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbmid" runat="server" Text='<%#Eval("WG_MARID").ToString()%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="物料名称" DataField="MNAME" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="规格" DataField="GUIGE" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField HeaderText="单位" DataField="PURCUNIT" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="数量" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="text_sj" runat="server" Text='<%#Eval("WG_RSNUM") %>'
                                        BorderStyle="None" Width="80px" onblur="amountcheck(this)" onchange="amountchecksll(this)"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbnum" runat="server"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="含税单价" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGI_TADJ" runat="server" Text='<%#Eval("WG_CTAXUPRICE")%>' BorderStyle="None"
                                        Width="80px" onchange="amountcheck(this)" onblur="amountcheckhsdj(this)"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbhsdj" runat="server"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="税率(%)" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGI_TAXRATE" runat="server" Text='<%#Eval("WG_TAXRATE")%>' BorderStyle="None"
                                        Width="80px" onchange="amountcheck(this)" onblur="amountchecksl(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGI_dj" runat="server" Text='<%#Eval("WG_UPRICE")%>' BorderStyle="None"
                                        Width='80px' onchange="amountcheck(this)" onblur="amountcheckdj(this)"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbdj" runat="server"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="金额" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGI_je" runat="server" Text='<%#Eval("WG_AMOUNT") %>' BorderStyle="None"
                                        Width='80px' onchange="amountcheck(this)" onblur="amountcheckje(this)" ></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbje" runat="server"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="税额" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGI_se" runat="server" Text='<%#Eval("WG_SE") %>' BorderStyle="None"
                                        Width='80px' onchange="amountcheck(this)" onblur="amountcheckse(this)"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbse" runat="server"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="含税金额" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtGI_HJPRICE" runat="server"   Text='<%#Eval("WG_CTAMTMNY") %>' BorderStyle="None"
                                        Width='80px' onchange="amountcheck(this)" onblur="amountcheckhjje(this)" ></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbhsje" runat="server"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="国标" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbguobiao" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                        <FixRowColumn FixRowType="Header" TableWidth="98%" TableHeight="380px" />
                    </yyc:SmartGridView>
         
             
                    <asp:Panel ID="PanelIn" runat="server">
                        <table align="center" width="98%">
                            <tr>
                                <td align="center">
                                    <br style="height: 2px" />
                                    <h4>
                                        入库单信息</h4>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="grvInvWI" Width="98%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" ShowFooter="True" OnRowDataBound="grvInvWI_RowDataBound">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="入库编号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbfp" runat="server" Text='<%#Eval("WG_CODE").ToString()%>'> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物料编码" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbmid" runat="server" Text='<%#Eval("WG_MARID").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="物料名称" DataField="MNAME" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="规格" DataField="GUIGE" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="单位" DataField="PURCUNIT" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="实收数量" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="text_sj" runat="server" Text='<%#String.Format("{0:N4}",Convert.ToDouble(Eval("WG_RSNUM").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="含税单价" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGI_TADJ" runat="server" Text='<%#String.Format("{0:N4}",Convert.ToDouble(Eval("WG_CTAXUPRICE").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="税率(%)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGI_TAXRATE" runat="server" Text='<%#String.Format("{0:N2}",Convert.ToDouble(Eval("WG_TAXRATE").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单价" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGI_dj" runat="server" Text='<%#String.Format("{0:N4}",Convert.ToDouble(Eval("WG_UPRICE").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="金额" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGI_je" runat="server" Text='<%#String.Format("{0:N2}",Convert.ToDouble(Eval("WG_AMOUNT").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="税额" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGI_se" runat="server" Text='<%#String.Format("{0:N2}",Convert.ToDouble(Eval("WG_SE").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="含税金额" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGI_HJPRICE" runat="server" Text='<%#String.Format("{0:N2}",Convert.ToDouble(Eval("WG_CTAMTMNY").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="已勾稽数量" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgjsl" runat="server" Text='<%#String.Format("{0:N4}",Convert.ToDouble(Eval("WG_GJNUM").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="已勾稽金额" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgjamount" runat="server" Text='<%#String.Format("{0:N2}",Convert.ToDouble(Eval("WG_GJAMOUNT").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="已勾稽含税金额" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgjcvamout" runat="server" Text='<%#String.Format("{0:N2}",Convert.ToDouble(Eval("WG_GJCTAMTMNY").ToString())) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
            </div>
    </div>
    </div>
</asp:Content>
