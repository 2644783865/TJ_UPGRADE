<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Marreplace_edit.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Marreplace_edit"
    Title="物料代用管理" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script language="javascript" type="text/javascript">

        function document.onreadystatechange() {
            if (document.readyState == "complete") {
                var ptc = "<%=gloabptc%>";
                if (ptc != "") {
                    var col = 1;
                    othptcfind(ptc, col)
                }

            }
        }
        function String.prototype.Trim() {
            return this.replace(/\s+/g, "");
        }
        function numright(obj) {
            var pattem = /^\d+(\.\d+)?$/;
            var num = obj.parentNode.getElementsByTagName("input")[0].value.Trim();
            if (pattem.test(num)) {
                obj.parentNode.getElementsByTagName("input")[0].value = num;
            }
            else {
                obj.parentNode.getElementsByTagName("input")[0].value = 0;
                alert('输入格式错误！');
                obj.parentNode.getElementsByTagName("input")[0].focus();
            }
        }
    </script>

    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
                            <%-- <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" Visible="false" />--%>
                            <a href="javascript:history.go(-1);">向上一页</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="Panel_body1" runat="server">
                <div style="height: 500px; overflow: auto">
                    <div class="fixbox2 xscroll" id="dvrepeater">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: small; text-align: center;" colspan="4">
                                        材料代用表
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;单&nbsp;号:
                                        <asp:TextBox ID="Tb_Code" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;期:
                                        <asp:TextBox ID="Tb_Date" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td id="Td1" style="width: 34%;" align="left" runat="server">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;摘&nbsp;要:
                                        <asp:TextBox ID="Tb_Abstract" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;&nbsp;&nbsp;项&nbsp;目&nbsp;编&nbsp;号:
                                        <asp:TextBox ID="Tb_pjid" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 33%;" align="left">
                                        &nbsp;项&nbsp;目&nbsp;名&nbsp;称:
                                        <asp:TextBox ID="Tb_pjname" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td id="Td2" style="width: 34%;" align="left" runat="server">
                                        &nbsp;工&nbsp;程&nbsp;名&nbsp;称:
                                        <asp:TextBox ID="Tb_engname" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tab" class="nowrap fixtable fullwidth" align="center">
                            <asp:Repeater ID="Marreplace_edit_repeater" runat="server" OnItemDataBound="Marreplace_edit_repeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                        <td colspan="2">
                                            <strong>&nbsp;</strong>
                                        </td>
                                        <td colspan="12">
                                            <strong>原材料计划</strong>
                                        </td>
                                        <td colspan="12">
                                            <strong>代用材料计划</strong>
                                        </td>
                                        <td colspan="1">
                                            <strong>&nbsp;</strong>
                                        </td>
                                    </tr>
                                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>计划号</strong>
                                        </td>
                                        <td>
                                            <strong>物料编码</strong>
                                        </td>
                                        <td>
                                            <strong>物料名称</strong>
                                        </td>
                                        <td>
                                            <strong>物料规格</strong>
                                        </td>
                                        <td>
                                            <strong>物料材质</strong>
                                        </td>
                                        <td>
                                            <strong>国标</strong>
                                        </td>
                                        <td>
                                            <strong>单位</strong>
                                        </td>
                                        <td>
                                            <strong>数量</strong>
                                        </td>
                                        <td>
                                            <strong>辅助数量</strong>
                                        </td>
                                        <td>
                                            <strong>长度</strong>
                                        </td>
                                        <td>
                                            <strong>宽度</strong>
                                        </td>
                                        <td>
                                            <strong>辅助单位</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td>
                                            <strong>物料编码</strong>
                                        </td>
                                        <td>
                                            <strong>物料名称</strong>
                                        </td>
                                        <td>
                                            <strong>物料规格</strong>
                                        </td>
                                        <td>
                                            <strong>物料材质</strong>
                                        </td>
                                        <td>
                                            <strong>国标</strong>
                                        </td>
                                        <td>
                                            <strong>单位</strong>
                                        </td>
                                        <td>
                                            <strong>数量</strong>
                                        </td>
                                        <td>
                                            <strong>辅助数量</strong>
                                        </td>
                                        <td>
                                            <strong>长度</strong>
                                        </td>
                                        <td>
                                            <strong>宽度</strong>
                                        </td>
                                        <td>
                                            <strong>辅助单位</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td>
                                            <strong>审核意见</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false" onclick="checkme(this)"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDMARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDMARNAME" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDGUIGE" runat="server" Text='<%#Eval("marguige")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDCAIZHI" runat="server" Text='<%#Eval("marcaizhi")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDGUOBIAO" runat="server" Text='<%#Eval("marguobiao")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDUNIT" runat="server" Text='<%#Eval("marcgunit")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDNUMA" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDNUMB" runat="server" Text='<%#Eval("fznum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDLENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDWIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_FZUNIT" runat="server" Text='<%#Eval("fzunit")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OLDNOTE" runat="server" Text='<%#Eval("allnote")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MP_NEWMARID" runat="server" BorderStyle="None" Text='<%#Eval("detailmarid")%>'
                                                onkeydown="grControlFocus(this)" OnTextChanged="Tb_newmarid_Textchanged" AutoPostBack="True"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="MP_NEWMARID"
                                                ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                                CompletionInterval="10" ServiceMethod="GetCompletemar" FirstRowSelected="true"
                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NEWMARNAME" runat="server" Text='<%#Eval("detailmarnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NEWGUIGE" runat="server" Text='<%#Eval("detailmarguige")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NEWCAIZHI" runat="server" Text='<%#Eval("detailmarcaizhi")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NEWGUOBIAO" runat="server" Text='<%#Eval("detailmarguobiao")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NEWUNIT" runat="server" Text='<%#Eval("detailmarunit")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TMP_NEWNUMA" runat="server" Text='<%#Eval("detailmarnuma").ToString()==""?Eval("num"):Eval("detailmarnuma")%>'
                                                Width="60px" onkeydown="grControlFocus(this)" OnTextChanged="Tb_newmarnum_Textchanged" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TMP_NEWNUMB" runat="server" Text='<%#Eval("detailmarnumb").ToString()==""?Eval("fznum"):Eval("detailmarnumb")%>'
                                                Width="60px" onkeydown="grControlFocus(this)" OnTextChanged="Tb_newmarnum_Textchanged" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MP_NEWLENGTH" runat="server" Text='<%#Eval("detaillength")%>' Width="60px"
                                                onkeydown="grControlFocus(this)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MP_NEWWIDTH" runat="server" Text='<%#Eval("detailwidth")%>' Width="60px"
                                                onkeydown="grControlFocus(this)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_NEWFZUNIT" runat="server" Text='<%#Eval("detailfzunit")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="MP_NEWNOTE" runat="server" Text='<%#Eval("detailnote")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_OPTION" runat="server" Text='<%#Eval("alloption")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="27" align="center">
                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                        没有数据！</asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <%--<asp:HiddenField ID="dvscrollX" runat="server" />
                <asp:HiddenField ID="dvscrollY" runat="server" />--%>
                <asp:Panel ID="FooterPanel" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td style="width: 25%;" align="left">
                                &nbsp;&nbsp;&nbsp;负&nbsp;责&nbsp;人:
                                <asp:TextBox ID="tb_Manager" runat="server" Text=""></asp:TextBox>
                                <asp:Label ID="lb_ManagerID" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 25%;" align="left">
                                &nbsp;&nbsp;&nbsp;审&nbsp;&nbsp;&nbsp;核:
                                <asp:TextBox ID="tb_shenhe" runat="server"></asp:TextBox>
                                <asp:Label ID="lb_shenheID" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 25%;" align="left">
                                &nbsp;&nbsp;&nbsp;填&nbsp;&nbsp;&nbsp;表:
                                <asp:TextBox ID="tb_Document" runat="server"></asp:TextBox>
                                <asp:Label ID="lb_DocumentID" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 25%;" align="left" runat="server" id="ckshr">
                                仓库审核人：
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
