<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_SHUIDFSQ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SHUIDFSQ" Title="住宿水电费管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    住宿水电费管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp; 楼层：<asp:DropDownList ID="drp_state" runat="server" OnSelectedIndexChanged="drp_state_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                            <asp:ListItem Value="1">一楼</asp:ListItem>
                            <asp:ListItem Value="2">二楼</asp:ListItem>
                            <asp:ListItem Value="3">三楼</asp:ListItem>
                            <asp:ListItem Value="4">四楼</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp; 房间号：<asp:TextBox ID="tbfjnum" runat="server" Width="70px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp; 姓名：<asp:TextBox ID="tbname" runat="server" Width="70px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp; 截止日期从:&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtqsrq" class="easyui-datebox"
                            onfocus="this.blur()" Width="100px" Height="18px"></asp:TextBox>&nbsp;&nbsp;
                        到:&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtjzrq" class="easyui-datebox"
                            onfocus="this.blur()" Width="100px" Height="18px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btncx" runat="server" Text="查询" OnClick="btncx_click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div align="center" id="div_statistcs" style="width: 100%; height: auto; overflow: scroll;
                display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptsushe" runat="server">
                        <HeaderTemplate>
                            <tr style="background-color: #B9D3EE;" height="30px">
                                <td align="center">
                                    序号
                                </td>
                                <td align="center">
                                    宿舍号
                                </td>
                                <td align="center">
                                    起始日期
                                </td>
                                <td align="center">
                                    截止日期
                                </td>
                                <td align="center">
                                    姓名
                                </td>
                                <td align="center">
                                    人均费用
                                </td>
                                <td align="center">
                                    实际费用
                                </td>
                                <td align="center">
                                    备注
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" height="30px">
                                <td>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                        Onclick="checkme(this)" Text="<%# Container.ItemIndex + 1%>" />
                                    <asp:Label ID="IDsdmx" runat="server" Text='<%#Eval("IDsdmx")%>' Visible="false"></asp:Label>
                                </td>
                                <td id="td_IDSDF" runat="server" visible="false">
                                    <%#Eval("IDSDF")%>'
                                </td>
                                <td id="td_ssnum" runat="server" align="center">
                                    <%#Eval("ssnum")%>
                                </td>
                                <td id="td_startdate" runat="server" align="center">
                                    <%#Eval("startdate")%>
                                </td>
                                <td id="td_enddate" runat="server" align="center">
                                    <%#Eval("enddate")%>
                                </td>
                                <td id="td_stratdf" runat="server" align="center" visible="false">
                                    <%#Eval("stratdf")%>
                                </td>
                                <td id="td_enddf" runat="server" align="center" visible="false">
                                    <%#Eval("enddf")%>
                                </td>
                                <td id="td_gscddl" runat="server" align="center" visible="false">
                                    <%#Eval("gscddl")%>
                                </td>
                                <td id="td_shiydl" runat="server" align="center" visible="false">
                                    <%#Eval("shiydl")%>
                                </td>
                                <td id="td_pricedf" runat="server" align="center" visible="false">
                                    <%#Eval("pricedf")%>
                                </td>
                                <td id="td_dianfje" runat="server" align="center" visible="false">
                                    <%#Eval("dianfje")%>
                                </td>
                                <td id="td_startsf" runat="server" align="center" visible="false">
                                    <%#Eval("startsf")%>
                                </td>
                                <td id="td_endsf" runat="server" align="center" visible="false">
                                    <%#Eval("endsf")%>
                                </td>
                                <td id="td_gscdsl" runat="server" align="center" visible="false">
                                    <%#Eval("gscdsl")%>
                                </td>
                                <td id="td_shiysl" runat="server" align="center" visible="false">
                                    <%#Eval("shiysl")%>
                                </td>
                                <td id="td_pricesf" runat="server" align="center" visible="false">
                                    <%#Eval("pricesf")%>
                                </td>
                                <td id="td_shuifje" runat="server" align="center" visible="false">
                                    <%#Eval("shuifje")%>
                                </td>
                                <td runat="server" align="center">
                                    <asp:Label ID="ST_NAME" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                </td>
                                <td id="td_renjunfy" runat="server" align="center">
                                    <%#Eval("renjunfy")%>
                                </td>
                                <td align="center">
                                    <%#Eval("realmoney")%>
                                </td>
                                <td id="td_note" runat="server" align="center">
                                    <%#Eval("note")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
