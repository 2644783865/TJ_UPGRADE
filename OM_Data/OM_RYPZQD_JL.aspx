<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_RYPZQD_JL.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_RYPZQD_JL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 70%;
            border-right: solid 1px #E5E5E5;
            border-bottom: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border-left: solid 1px #E5E5E5;
            border-top: solid 1px #E5E5E5;
            text-align: center;
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
            width: 90%;
            height: 25px;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="SM_JS/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="SM_JS/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
    
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table>
                    <tr>
                        <td>
                            &nbsp;部门&nbsp;
                            <asp:DropDownList runat="server" ID="ddlBM" AutoPostBack="true" OnSelectedIndexChanged="Query">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="Query">
                            </asp:DropDownList>
                            &nbsp;年&nbsp;
                            <asp:DropDownList runat="server" ID="ddlMonth" AutoPostBack="true" OnSelectedIndexChanged="Query">
                            </asp:DropDownList>
                            &nbsp;月&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater runat="server" ID="rptRYPZ" OnItemDataBound="rptRYPZ_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE" height="30px">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>部门</strong>
                                    </td>
                                    <%--<td>
                                        <strong>班组</strong>
                                    </td>--%>
                                    <td>
                                        <strong>岗位名称</strong>
                                    </td>
                                    <td>
                                        <strong>人员编制</strong>
                                    </td>
                                    <td>
                                        <strong>已到人数</strong>
                                    </td>
                                    <td>
                                        <strong>缺编人数</strong>
                                    </td>
                                    <td>
                                        <strong>到岗人员名单</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" CssClass="checkBoxCss" />
                                        <asp:Label runat="server" ID="lbXuHao" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="ST_DEPID" Value='<%#Eval("ST_DEPID")%>' />
                                        <asp:HiddenField runat="server" ID="ST_POSITION" Value='<%#Eval("ST_POSITION")%>' />
                                    </td>
                                    <td>
                                        <%#Eval("DEP_NAME")%>
                                    </td>
                                    <%--<td>
                                        <%#Eval("ST_DEPID1")%>
                                    </td>--%>
                                    <td>
                                        <%#Eval("DEP_POSITION")%>
                                    </td>
                                    <td>
                                        <%--<asp:TextBox runat="server" ID="DEP_PZSR" onblur="CheckNum(this)" Text='<%#Eval("DEP_PZRS")%>'></asp:TextBox>--%>
                                        <%#Eval("DEP_PZRS")%>
                                    </td>
                                    <td>
                                        <%#Eval("DEP_YDRS")%>
                                    </td>
                                    <td>
                                        <%#Eval("DEP_QBRS")%>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="ST_PEAPLE" Width="98%" onfocus="this.blur()" TextMode="MultiLine"
                                            Rows="2" BorderStyle="None" Text='<%#Eval("ST_PEAPLE")%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="3">
                                        合计
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbPZRS" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbYDRS" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbWDRS" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
