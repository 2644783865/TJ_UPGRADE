﻿<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_YFInvoice.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_YFInvoice" Title="无标题页" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<table width="100%">
        <tr>
            <td style="width: 80%">
                <asp:Label ID="lblInvState" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<script type="text/javascript" language="javascript">
 function amountcheck(obj) 
    {        
        Num=obj.value; 
        if(Num=="")
        {
           alert("数据不能为空!");
           obj.focus();
        }
    } 
    
    

      //金额变化：含税金额，税额
    function amountcheckje(obj) 
    {        
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var shuilv=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hsje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;    
           
            if(je!=null)
            {
              //税额
              se=(hsje-je).toFixed(2);      
              obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=se;
            }
             
            Statistic();
    }
    //税率变化：金额，税额
     function amountcheckshuilv(obj) 
    {        
                  
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var shuilv=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hsje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;        
           
            if(hsje!=null)
            {
              
              //金额
              je=(hsje/(1+parseFloat(shuilv))).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
                         
              //税额
              se=(hsje-je).toFixed(2);         
              obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=se;
            }
           
            Statistic();
            
    }
    
    
    
    
    //含税金额变化:含税单价，单价，金额，税额
     function amountcheckhsje(obj) 
    {        
           
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var shuilv=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hsje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;       
           
            if(hsje!=null)
            {
              
              //金额
              je=(hsje/(1+parseFloat(shuilv))).toFixed(2);
              obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=je;
                         
              //税额
              se=(hsje-je).toFixed(2);         
              obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=se;
            }
           
            Statistic();
            
    }
    
     //税额变化:含税金额
    function amountcheckse(obj) 
    {            
            var je=obj.parentNode.parentNode.getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
            var shuilv=obj.parentNode.parentNode.getElementsByTagName("td")[11].getElementsByTagName("input")[0].value;
            var hsje=obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
            var se=obj.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;         
           
            if(se!=null)
            {
              hsje=(parseFloat(je)+parseFloat(se)).toFixed(2); 
              obj.parentNode.parentNode.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=hsje;
            }
                         
            Statistic();
    }
    
    
    
     //各项金额汇总
        function Statistic() {
            var totalje = 0;
            var totalse = 0;
            var totalhsje = 0;
            var tab = document.getElementById("table1");
            for (i = 1; i < (tab.rows.length - 1); i++)
            {
                var valje = tab.rows[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value;
                totalje += parseFloat(valje);
                var valse = tab.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;
                totalse += parseFloat(valse);
                var valhsje = tab.rows[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
                totalhsje += parseFloat(valhsje);
            }
              var $jehj=$("#table1 tr:last span:eq(0)");
              $jehj.html(totalje.toFixed(2));
              var $hsjehj=$("#table1 tr:last span:eq(1)");
              $hsjehj.html(totalhsje.toFixed(2));
              var $sehj=$("#table1 tr:last span:eq(2)");
              $sehj.html(totalse.toFixed(2)); 
        }








    //保存对单价或其他内容的修改
    function bcxg()
    {
      return confirm("确认对信息的修改吗？\r\r提示：请认真核对信息！！！");
    }
    //审核通过
    function shpass()
    {
      return confirm("确认审核通过吗？\r\r提示：审核通过后允许勾稽发票！！！");
    }
    //审核驳回
    function shreject()
    {
      return confirm("确认审核驳回吗？\r\r提示：审核驳回后将进入待审核阶段！！！")
    }
    //钩稽通过
    function gjpass()
    {
      return confirm("确认钩稽吗？")
    }
    //钩稽驳回
    function gjreject()
    {
      return confirm("确认钩稽驳回吗？\r\r提示：钩稽驳回后发票信息将进入已审核状态！！！")
    }
</script>
<div class="RightContent">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            <table width="98%">
            <tr>
            <td>
            <asp:Button ID="btnbaocunxg" runat="server" Text="保存修改" OnClick="btnbaocunxg_Click"
                                        OnClientClick="javascript:return bcxg();" />
            </td>
            <td>
            <asp:Button ID="btnshpass" runat="server" Text="审核通过" OnClick="btnshpass_Click"
                                        OnClientClick="javascript:return shpass();" />
            </td>
            <td>
            <asp:Button ID="btnshreject" runat="server" Text="审核驳回" OnClick="btnshreject_Click"
                                        OnClientClick="javascript:return shreject();" />
            </td>
            <td>
            <asp:Button ID="btngjpass" runat="server" Text="勾稽通过" OnClick="btngjpass_Click"
                                        OnClientClick="javascript:return gjpass();" />
            </td>
            <td>
            <asp:Button ID="btngjreject" runat="server" Text="勾稽驳回" OnClick="btngjreject_Click"
                                        OnClientClick="javascript:return gjreject();" />
            </td>
            <td align="right" style="width:20%">
            <a href="FM_YFInvoice_Managemnt.aspx" title="返回到发票管理界面" >返回</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            </tr>
            </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
                <div class="box-outer">
                  
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1"
                        width="98%">
                        <tr>
                            <td align="right">
                                <strong>发票编号</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfpbh" runat="server"></asp:TextBox>
                            </td>
                            <td align="right">
                                <strong>供应商名称</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtgysname" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <strong>发票号码</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfphm" runat="server"></asp:TextBox>
                            </td>
                            <td align="right">
                                <strong>凭证号</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpzh" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <strong>勾稽标志</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblgjflag" runat="server" RepeatColumns="2"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <strong>登记日期</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                                <td align="right">
                                    <strong>核算标志</strong>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblhsflag" runat="server" RepeatColumns="2"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right">
                                    <strong>审核标志</strong>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblshflag" runat="server" RepeatColumns="2"
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
                                    <asp:TextBox ID="txtkhbank" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <strong>地址</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtaddress" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>部门</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdepartment" Text="财务部" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <strong>记账人</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtjzr" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>制单人</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtzdr" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <strong>备注</strong>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtnote" runat="server" TextMode="MultiLine" Width="80%" Height="25px"></asp:TextBox>
                                </td>
                            </tr>
                    </table>
                    </div>
                    </div>
                    
     <div class="box-wrapper">
     <div class="box-outer" style="overflow:scroll">
        <table id="table1" cellpadding="2" width="100%" cellspacing="1" class="toptable grid nowrap"
                                border="1">
         <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                                <tr align="center">
                                    <th align="center">
                                        序号
                                    </th>
                                    <th align="center">
                                        勾稽日期
                                    </th>
                                    <th align="center">
                                        计划跟踪号
                                    </th>
                                    <th align="center">
                                        结算单号
                                    </th>
                                    <th align="center">
                                        供应商
                                    </th>
                                    <th align="center">
                                        图号
                                    </th>
                                    <th align="center">
                                        设备名称
                                    </th>
                                    <th align="center">
                                        制单人
                                    </th>
                                    <th align="center">
                                        数量
                                    </th>
                                    <th align="center">
                                        单重
                                    </th>
                                    <th align="center">
                                        金额
                                    </th>
                                    <th align="center">
                                        税率
                                    </th>
                                    <th align="center">
                                        含税金额
                                    </th>
                                    <th align="center">
                                        税额
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbdate" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_GJDATE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbjhgzh" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_JHGZH")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbjsdh" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGI_JSDID")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbgys" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGI_GYSNAME")%>'></asp:Label>
                                </td>
                                
                                
                                <td runat="server" align="center">
                                    <asp:Label ID="lbcpbh" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_CPBH")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbcpmc" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_CPMC")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbzdr" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGI_ZDNAME")%>'></asp:Label>
                                </td>
                                
                                <td runat="server" align="center">
                                    <asp:Label ID="lbsl" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_NUM")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbzl" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_WGHT")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbje" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_JE")%>' onblur="amountcheck(this)" onchange="amountcheckje(this)"></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbshuilv" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_SHUILV")%>' onblur="amountcheck(this)" onchange="amountcheckshuilv(this)"></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbhsje" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("YFGJ_HSJE")%>' onblur="amountcheck(this)" onchange="amountcheckhsje(this)"></asp:TextBox>
                                </td>
                                <td runat="server" align="center">
                                    <asp:TextBox ID="tbse" runat="server" BorderStyle="None" BackColor="Transparent"
                                        Text='<%#Eval("SE")%>' onblur="amountcheck(this)" onchange="amountcheckse(this)"></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <th colspan="2" align="right">
                                合计:
                                </th>
                                <th colspan="8">
                                
                                </th>
                                <th>
                                   <asp:Label ID="lbjehj" runat="server"></asp:Label>
                                </th>
                                <th>
                                
                                </th>
                                <th align="center">
                                     <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                                </th>
                                <th align="center">
                                     <asp:Label ID="lbsehj" runat="server"></asp:Label>
                                </th>
                             </tr>
                            </FooterTemplate>
                    </asp:Repeater>
                </table>
        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
            没有记录！
        </asp:Panel>
          </div>
     </div>
</div>
</asp:Content>
