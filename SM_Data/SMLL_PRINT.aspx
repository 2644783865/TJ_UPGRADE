<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMLL_PRINT.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SMLL_PRINT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>生产领料单打印</title>
</head>
<body style="width:98%;float:left">
    <form id="form1" runat="server">
    <div id="div1">
         <table align="center" width="90%">
           <tr>
                <td align="right" style="font-size:x-large">
                    <strong>生产领料单</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td style="width:45%;">
                    班组：
                    <asp:Label ID="lbllbz" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    编号：
                    <asp:Label ID="lblldh" runat="server"></asp:Label>
                </td>
                &nbsp;&nbsp;&nbsp;
           </tr>
         </table>
      </div>   
    <div id="div2">          
        <table align="center" width="90%">
            <tr style="font-size:15px">
                <td align="center">
                    <strong>项目:</strong>
                </td>
                <td style="width:48%">
                    <asp:Label ID="lbxmmc" runat="server" Enabled="false"></asp:Label>
                </td>
                <td align="center">
                    <strong>任务号:</strong>
                </td>
                <td style="width:15%">
                    <asp:Label ID="lbrwh" runat="server" Enabled="false"></asp:Label>
                </td>
                <td align="center">
                    <strong>制单人:</strong>
                </td>
                <td style="width:9%">
                    <asp:Label ID="lbzdr" runat="server" Enabled="false"></asp:Label>
                </td>
            </tr>
            <tr style="font-size:15px">
                <td align="left">
                    <strong>备注:</strong>
                </td>
                <td colspan="5">
                    <asp:Label ID="lbnote" runat="server" Enabled="false"></asp:Label>
                </td>
            </tr>
       </table>
    </div>
    <div id="div3">
        <table id="table1" align="center" width="90%" border="1" cellpadding="0" cellspacing="0" style="border-collapse:collapse;">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                                <tr align="center" style="font-size:15px">
                                    <th>
                                        名称规格
                                    </th>
                                    <th align="center">
                                        国标
                                    </th>
                                    <th align="center">
                                        材料
                                    </th>
                                    <th align="center">
                                        单位
                                    </th>
                                    <th align="center">
                                        图号
                                    </th>
                                    <th align="center">
                                        实额数量
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'" style="font-size:15px">
                                <td id="Td1" runat="server" align="left">
                                    <asp:Label ID="nameguige" runat="server"
                                        Text='<%#Eval("MS")%>'></asp:Label>
                                </td>
                                <td id="Td2" runat="server" align="center">
                                    <asp:Label ID="gb" runat="server"
                                        Text='<%#Eval("GB")%>'></asp:Label>
                                </td>
                                <td id="Td3" runat="server" align="center">
                                    <asp:Label ID="cl" runat="server"
                                        Text='<%#Eval("Attribute")%>'></asp:Label>
                                </td>
                                <td id="Td4" runat="server" align="center">
                                    <asp:Label ID="dw" runat="server"
                                        Text='<%#Eval("Unit")%>'></asp:Label>
                                </td>
                                <td id="Td5" runat="server" align="center">
                                    <asp:Label ID="desl" runat="server"
                                        Text='<%#Eval("OP_BSH")%>'></asp:Label>
                                </td>
                                <td id="Td6" runat="server" align="center">
                                    <asp:Label ID="sesl" runat="server"
                                        Text='<%#Eval("RealNumber")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                   </table>
    </div>
    <div id="div4">
        <table align="center" width="90%" style="font-size:15px">
           <tr>
             <td style="width:22%">
                负责人：
             </td>
             <td style="width:22%">
                领料：
             </td>
             <td style="width:23%">
                发料人：
             </td>
             <td style="width:23%">
                日期：
                <asp:Label ID="lbrq" runat="server"></asp:Label>
             </td>
           </tr>
        </table>
    </div>
    <a href="javascript:printme()" target="_self">打印</a>
      <script type="text/javascript" language="javascript">
            function printme()
            {
            document.body.innerHTML=document.getElementById('div1').innerHTML+'<br/>'+document.getElementById('div2').innerHTML+'<br/>'+document.getElementById('div3').innerHTML+'<br/>'+document.getElementById('div4').innerHTML;
            window.print();
            }
   </script>
    </form>
</body>
</html>
