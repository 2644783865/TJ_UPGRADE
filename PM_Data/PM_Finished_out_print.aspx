<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_Finished_out_print.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Finished_out_print" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>成品出库单打印</title>
    <style type="text/css">
        .tab
        {
            width: 80%;
            border: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border: solid 1px #E5E5E5;
            border-collapse: collapse;
            text-align: center;
            font-size: small;
            height: 30px;
        }
        .tab tr td input
        {
            width: 90%;
            height: 25px;
            font-size: small;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function btnDY_OnClientClick() {
            document.body.innerHTML = document.getElementById('divMain').innerHTML;
            window.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnDY" OnClientClick="btnDY_OnClientClick()" Text="打印" />
    </div>
    <div id="divMain" align="center" style="width:100%;">        
        <table class="tab" style="width:80%;">
            <tr>
                <td align="center" style="font-size:x-large;">
                    <strong>成&nbsp;&nbsp;品&nbsp;&nbsp;出&nbsp;&nbsp;库&nbsp;&nbsp;单</strong>
                </td>
            </tr>
        </table>
        <table class="tab" >
            <td align="left" width="10%">出库&nbsp;&nbsp;&nbsp;<br/>单号：</td>
            <td align="left" width="20%"><asp:TextBox ID="txt_docnum" runat="server" ReadOnly="true"/></td>
            <td align="center" width="10%">日期：</td>
            <td align="center" width="20%"><asp:TextBox ID="Tb_shijian" runat="server" ReadOnly="true"/></td>
            <td align="right" width="10%">备注：</td>        
            <td align="right" width="20%"><asp:TextBox ID="txt_note" runat="server" ReadOnly="true"/></td>
        </table>
        <table cellpadding="4" cellspacing="1" border="1" style="width:80%;">
            <asp:Repeater ID="PM_Finished_PrintRepeater" runat="server">
                <HeaderTemplate>
                    <tr style="text-align:center" >
                        <td style="font-size:xx-small;">
                            <strong>序号</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>编号</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>项目名称</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>合同号</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>任务单号</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>总序</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>图号</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>设备名称</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>出库数量</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>交货期</strong>
                        </td>
                        <td style="font-size:xx-small;">
                            <strong>出库时间</strong>
                        </td>           
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="font-size:xx-small;">
                        <td align="center" >
                            <div>
                                <%# Convert.ToInt32(Container.ItemIndex +1)%>
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="lblbianhao" runat="server" Text='<%#Eval("CM_BIANHAO")%>' ></asp:Label>
                        <td>
                            <asp:Label ID="lblProj" runat="server" Text='<%#Eval("KC_PROJ")%>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblContr" runat="server" Text='<%#Eval("CM_CONTR")%>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTsa" runat="server" Text='<%#Eval("TSA_ID")%>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="TFO_ZONGXU" runat="server" Text='<%#Eval("TFO_ZONGXU")%>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMap" runat="server" Text='<%#Eval("KC_MAP")%>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEngname" runat="server" Text='<%#Eval("KC_NAME")%>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNumber" runat="server" Text='<%#Eval("TFO_CKNUM")%>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFHdate" runat="server" Text='<%#Eval("CM_JHTIME")%>' ></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblIndate" runat="server" Text='<%#Eval("OUTDATE")%>' ></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <table class="tab" >
            <td align="left" width="10%">负责人：</td>
            <td align="left" width="20%"><asp:TextBox ID="Tb_fuziren" runat="server" ReadOnly="true"/></td>
            <td align="center" width="10%">申请人：</td>
            <td align="center" width="20%"><asp:TextBox ID="Tb_shenqingren" runat="server" ReadOnly="true"/></td>
            <td align="right" width="10%">制单人：</td>        
            <td align="right" width="20%"><asp:TextBox ID="tb_executor" runat="server" ReadOnly="true"/></td>
        </table>    
    </div>  
    </form>
</body>
</html>
