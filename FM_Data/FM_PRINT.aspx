<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FM_PRINT.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_PRINT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发票打印页</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="div1">          
        <table align="center"
            width="90%">
            <tr style="font-size:xx-small">
                <td align="right">
                    供应商名称:
                </td>
                <td>
                    <asp:Label ID="lbGI_SUPPLIERNM" runat="server" Enabled="false"></asp:Label>
                </td>
                <td align="right">
                    发票号码:
                </td>
                <td>
                    <asp:Label ID="lbGI_INVOICENO" runat="server" Enabled="false"></asp:Label>
                </td>
                <td align="right">
                    凭证号:
                </td>
                <td>
                    <asp:Label ID="lbGI_PZH" runat="server" Enabled="false"></asp:Label>
                </td>
                <td align="right">
                    登记日期:
                </td>
                <td>
                    <asp:Label ID="lbGI_DATE" runat="server" Enabled="false"></asp:Label>
                </td>
            </tr>
       </table>
    </div>
    <div id="div2">
        <table id="table1" align="center" width="90%" border="1" cellpadding="0" cellspacing="0" style="border-collapse:collapse;">
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                                <tr align="center" style="font-size:xx-small">
                                    <td align="center">
                                        序号
                                    </td>
                                    <td align="center">
                                        入库单号
                                    </td>
                                    <td align="center">
                                        物料编码
                                    </td>
                                    <td align="center">
                                        物料名称
                                    </td>
                                    <td align="center">
                                        规格
                                    </td>
                                    <td align="center">
                                        单位
                                    </td>
                                    <td align="center">
                                        数量
                                    </td>
                                    <td align="center">
                                        含税单价
                                    </td>
                                    <td align="center">
                                        税率
                                    </td>
                                    <td align="center">
                                        单价
                                    </td>
                                    <td align="center">
                                        金额
                                    </td>
                                    <td align="center">
                                        税额
                                    </td>
                                    <td align="center">
                                        含税金额
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'" style="font-size:xx-small">
                                <td align="center">
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_code" runat="server"
                                        Text='<%#Eval("WG_CODE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_marid" runat="server"
                                        Text='<%#Eval("WG_MARID")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbmname" runat="server"
                                        Text='<%#Eval("MNAME")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbguige" runat="server"
                                        Text='<%#Eval("GUIGE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbpurcunit" runat="server"
                                        Text='<%#Eval("PURCUNIT")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_rsnum" runat="server"
                                        Text='<%#Eval("WG_RSNUM")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_ctaxuprice" runat="server"
                                        Text='<%#Eval("WG_CTAXUPRICE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_taxrate" runat="server"
                                        Text='<%#Eval("WG_TAXRATE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_uprice" runat="server"
                                        Text='<%#Eval("WG_UPRICE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_amount" runat="server"
                                        Text='<%#Eval("WG_AMOUNT")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_se" runat="server"
                                        Text='<%#Eval("WG_SE")%>'></asp:Label>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="lbwg_ctamtmny" runat="server"
                                        Text='<%#Eval("WG_CTAMTMNY")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr style="font-size:xx-small">
                                <td>

                                </td>
                                <td align="center">
                                    合计:
                                </td>
                                <td colspan="8">
                                </td>

                                <td align="center">
                                    <asp:Label ID="lbjehj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbsehj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </FooterTemplate>
                    </asp:Repeater>
                   </table>
    </div>
    <a href="javascript:printme()" target="_self">打印</a>
      <script type="text/javascript" language="javascript">
            function printme()
            {
            document.body.innerHTML=document.getElementById('div1').innerHTML+'<br/>'+document.getElementById('div2').innerHTML;
            window.print();
            }
   </script>
    </form>
</body>
</html>
