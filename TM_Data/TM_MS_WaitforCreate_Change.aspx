<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MS_WaitforCreate_Change.aspx.cs"
    Inherits="ZCZJ_DPF.TM_Data.TM_MS_WaitforCreate_Change" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<base target="_self" />
<head id="Head1" runat="server">
    <title>待提交变更制作明细(未生成)</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 12%">
                                    任务号：
                                    <asp:Label ID="tsaid" runat="server"></asp:Label>
                                </td>
                                <td style="width: 12%">
                                    合同号：
                                    <asp:Label ID="lblContract" runat="server"></asp:Label>
                                </td>
                                <td style="width: 12%">
                                    项目名称：
                                    <asp:Label ID="proname" runat="server"></asp:Label>
                                </td>
                       <%--         <td style="width: 12%">
                                   设备名称：
                                    <asp:Label ID="engname" runat="server"></asp:Label>
                                </td>--%>
                                <td align="right" style="width: 5%">
                                    制作明细:
                                </td>
                                <td style="width: 13%">
                                    <asp:RadioButtonList ID="rblInMs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblInMs_OnSelectedIndexChanged"
                                        RepeatColumns="3" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="全部" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="是" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="N"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 10%">
                                    <asp:RadioButtonList ID="rblChange" runat="server" AutoPostBack="true" RepeatColumns="2"
                                        OnSelectedIndexChanged="rblInMs_OnSelectedIndexChanged" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="变更后" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="变更前" Value="B"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="center">
                                    <%--   <asp:Button ID="btnBack" runat="server" Text="返回调整" OnClick="btnBack_OnClick" />--%>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlShebei">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnMSCreate" runat="server" Text="生成制作明细" OnClientClick="return confirm('确认提交制作明细吗？\r\r提示：\r\r提交后将下推审核,制作明细无法修改！\r\r如果确认无误,请单击【确定】,否则单击【取消】！！！');"
                                        ToolTip="点击完成后,制作明细无法修改,请确认无误后再点击！" OnClick="btnMSCreate_OnClick" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <yyc:SmartGridView ID="grv" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                        CellPadding="4" ForeColor="#333333" Width="100%">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="BM_TUHAO" HeaderStyle-Wrap="false" HeaderText="图号">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_ZONGXU" HeaderStyle-Wrap="false" HeaderText="总序">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_CHANAME" HeaderStyle-Wrap="false" HeaderText="名称">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MAGUIGE" HeaderText="规格">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_TUUNITWGHT" DataFormatString="{0:N2}" HeaderText="图纸单重(kg)">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_TUTOTALWGHT" DataFormatString="{0:N2}" HeaderText="图纸总重(kg)">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MASHAPE" HeaderText="材料类别">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_TECHUNIT" HeaderText="单位">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MATOTALWGHT" HeaderText="重量（Kg）">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MALENGTH" HeaderText="长度">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_MAWIDTH" HeaderText="宽度">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_NOTE" HeaderText="下料备注">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_XIALIAO" HeaderText="下料方式">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_KU" HeaderText="库">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="备注">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="变更状态" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblChangeState" runat="server" Text='<%#Eval("BM_MSSTATUS").ToString()=="1"?"删除":Eval("BM_MSSTATUS").ToString()=="2"?"增加":Eval("BM_MSSTATUS").ToString()=="3"?"修改":Eval("BM_MSSTATUS").ToString()=="0"?"正常":"" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <FixRowColumn FixRowType="Header,Pager" TableHeight="600px" TableWidth="100%" FixColumns="0,1,2" />
                    </yyc:SmartGridView>
                    <uc1:UCPaging ID="UCPagingMS" runat="server" />
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有数据！
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 30%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
