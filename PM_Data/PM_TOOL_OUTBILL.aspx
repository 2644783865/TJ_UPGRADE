<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_TOOL_OUTBILL.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_TOOL_OUTBILL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    刀具出库单&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txt_giver.ClientID %>").val(r.st_name);
                $("#<%=giverid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button-outer" OnClick="btnSave_OnClick" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="false" OnClick="btnReturn_OnClick"
                                    CssClass="button-outer" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table width="100%">
                    <tr align="center">
                        <td align="center" colspan="5">
                            <asp:Label ID="lbltitle1" runat="server" Text="刀具领料单" Font-Bold="true" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            出库单号：<asp:Label ID="lblOutCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            发料员：<asp:Label ID="lblOutDoc" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblOutDocID" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="right">
                            领料员：
                        </td>
                        <td style="width: 20%">
                            <asp:TextBox ID="txt_giver" runat="server" Width="80px"></asp:TextBox>
                            <input id="giverid" type="text" runat="server" readonly="readonly" style="display: none" />
                            <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                        <td align="left">
                            领料日期：<asp:Label ID="lblOutDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    CellPadding="4" ForeColor="#333333" EmptyDataText="没有相关数据！">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刀具类别" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbltype" runat="server" Text='<%#Eval("TYPE")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="规格型号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <itemtemplate>
                                        <asp:Label ID="lblmodel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                    </itemtemplate>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="库存数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtstore" runat="server" Text='<%#Eval("NUMBER")%>' BorderStyle="None"
                                    Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="出库数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtoutnum" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtnote" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="04">
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 430px; height: 230px">
            <table id="dg">
            </table>
        </div>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
            保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                    data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>
