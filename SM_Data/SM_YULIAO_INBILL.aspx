<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="SM_YULIAO_INBILL.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_YULIAO_INBILL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />

    <script src="../JS/ChoiceRcmd.js" type="text/javascript"></script>

    <script type="text/javascript">

        /*助记码操作*/
        function autoCode(input) {
            var marid = $(input).val();
            var tr = $(input).parent().parent();
            if (marid != null) {
                var marids = marid.split('|');
                if (marids.length < 8) { return; }
                tr.find("input[name*=Marid]").val(marids[0]);
                tr.find("input[name*=Name]").val(marids[1]);
                tr.find("input[name*=GUIGE]").val(marids[2]);
                tr.find("input[name*=CAIZHI]").val(marids[3]);
                tr.find("input[name*=MWEIGHT]").val(marids[8]);
                tr.find("input[name*=TUXING]").val("方");
                tr.find("input[name*=INNUM]").val("1");
            }
        }
        function Cal(input) {
            var $tr = $(input).parent().parent();
            var length = $tr.find("input[name*=Length]").val();
            var width = $tr.find("input[name*=Width]").val();
            var num = $tr.find("input[name*=INNUM]").val();
            var tuxing = $tr.find("select[name*=TUXING]").val();
            var lilun = $tr.find("input[name*=MWEIGHT]").val();
            var guige = $tr.find("input[name*=GUIGE]").val();
            var weight;
            if (tuxing == "方") {
                weight = (length * width * num * guige * lilun / 1000000000).toFixed(4);
            }
            else if (tuxing == "圆") {
            weight = (length * length * 3.14 * guige * num / 4 / 1000000000).toFixed(4);

            }
            console.log(weight);
            $tr.find("input[name*=Weight]").val(weight);

        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent" style="overflow: hidden">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btn_addrow" runat="server" Text="增加行" OnClick="btn_addrow_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_delectrow" runat="server" Text="删除行" OnClick="btn_delectrow_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
                                        &nbsp;
                                        <asp:Button ID="btn_back" runat="server" Text="返回" OnClientClick="history.go(-1)" />
                                        &nbsp; &nbsp;&nbsp; &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: x-large; text-align: center;" colspan="3">
                                        余料入库单
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
                                        入库单号：<asp:Label ID="lblInCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        收料员：<asp:Label ID="lblInDoc" runat="server"></asp:Label>
                                        <asp:Label ID="lblInDocID" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td align="left">
                                        收料日期：<asp:Label ID="lblInDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0" BorderStyle="None">
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="余料入库单" TabIndex="0" BorderStyle="None">
                                <ContentTemplate>
                                    <div style="border: 1px solid #000000; height: 300px; overflow: auto">
                                        <div class="cpbox6 xscroll">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <table id="tab" width="100%" class="nowrap cptable fullwidth">
                                                            <asp:Repeater ID="SM_YULIAO_List_Repeater" runat="server">
                                                                <HeaderTemplate>
                                                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                                        <td>
                                                                            <strong>行号</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>物料编码</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>名称</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>材质</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>规格</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>理论重量</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>长度/直径(mm)</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>宽度</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>数量</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>图形</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>重量(T)</strong>
                                                                        </td>
                                                                        <td>
                                                                            <strong>备注</strong>
                                                                        </td>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr id="Tr1" class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                                        runat="server" align="center">
                                                                        <td>
                                                                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                            <asp:CheckBox ID="CHK" CssClass="checkBoxCss" BorderStyle="None" runat="server" onclick="checkme(this)"
                                                                                Checked="false"></asp:CheckBox>&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Marid" name="Marid" runat="server" Text='<%#Eval("Marid")%>' Width="80px"
                                                                                onchange="autoCode(this)"></asp:TextBox>
                                                                            <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                                                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                                                                ServicePath="~/Ajax.asmx" TargetControlID="Marid" UseContextKey="True" CompletionInterval="10">
                                                                            </cc1:AutoCompleteExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Name" name="Name" runat="server" Text='<%#Eval("Name")%>' Width="120"
                                                                                Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="CAIZHI" name="CAIZHI" runat="server" Text='<%#Eval("CAIZHI")%>'
                                                                                Width="120" Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="GUIGE" name="GUIGE" runat="server" Text='<%#Eval("GUIGE")%>' Width="120"
                                                                                Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="MWEIGHT" name="MWEIGHT" Width="80" runat="server" Text='<%#Eval("MWEIGHT")%>'
                                                                                Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Length" name="Length" runat="server" Text='<%#Eval("Length")%>'
                                                                                Width="80" onkeyup="Cal(this)"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Width" name="Width" runat="server" Text='<%#Eval("Width")%>' Width="50"
                                                                                onkeyup="Cal(this)"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="INNUM" name="INNUM" runat="server" Text='<%#Eval("INNUM")%>' Width="50"
                                                                                onkeyup="Cal(this)"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="TUXING" name="TUXING" runat="server" onchange="Cal(this)" DataValueField='<%#Eval("TUXING") %>'>
                                                                                <asp:ListItem Text="方" Value="方"></asp:ListItem>
                                                                                <asp:ListItem Text="圆" Value="圆"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <div class="hidden" style="position: absolute; background-color: #f3f3f3; cursor: hand;
                                                                                border: #B9D3EE 3px solid; padding: 0px; margin: 0px;">
                                                                                <ul style="list-style-type: square; text-align: left; line-height: normal;">
                                                                                </ul>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Weight" name="Weight" runat="server" Text='<%#Eval("Weight")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="NOTE" runat="server" Text='<%#Eval("NOTE")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr>
                                                                <td colspan="16" align="center">
                                                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                                        没有数据！</asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
