<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPlan_Detail.aspx.cs"
    Inherits="ZCZJ_DPF.PL_Data.MainPlan_Detail" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目进度查看</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 18%">
                            任务号:<asp:Label ID="tsaid" runat="server"></asp:Label>
                        </td>
                        <td style="width: 18%">
                            合同号:<asp:Label ID="lab_contract" runat="server"></asp:Label>
                            <input id="con_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                        </td>
                        <td style="width: 18%">
                            项目名称:<asp:Label ID="lab_proname" runat="server"></asp:Label>
                            <input id="pro_name" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                        </td>
                        <td style="width: 18%">
                            设备名称:<asp:Label ID="lab_engname" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MP_TYPE" HeaderText="计划类别" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_PLNAME" HeaderText="主计划项" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <%-- MP_ID, MP_ENGID, MP_PLNAME, MP_DAYS, MP_STARTDATE, MP_ENDTIME, MP_ISFINISH, MP_ACTURALTIME, MP_TYPE--%>
                    <asp:BoundField DataField="MP_DAYS" HeaderText="计划天数" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_WARNINGDAYS" HeaderText="预警天数" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_STARTDATE" HeaderText="计划开始时间" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_ENDTIME" HeaderText="计划截止日期" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="该项状态" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("MP_STATE").ToString()=="0"?"未开始":Eval("MP_STATE").ToString()=="1"?"进行中...":Eval("MP_STATE").ToString()=="2"?"完成":"未知错误" %>'></asp:Label>
                            <input type="hidden" runat="server" value='<%# Eval("MP_STATE") %>' id="hidState" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MP_ACTURALTIME" HeaderText="实际完成时间" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
