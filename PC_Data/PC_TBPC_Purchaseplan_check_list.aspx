<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_check_list.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_check_list"
    Title="物料占用管理" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function PLdaochu() {
            //        window.showModalDialog('PC_TBPC_PLdaochu.aspx','',"dialogHeight:300px;dialogWidth:500px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.open("PC_Data_ZYDdaochu.aspx");
            //        window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?shape="+escape(shape)+"&mp_id="+escape(mpid)+"";
        }
        function redirectw(obj) {
            var sheetno;
            sheetno = obj.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
            window.location.href = "PC_TBPC_Purchaseplan_check_detail.aspx?sheetno=" + escape(sheetno) + "";
            //window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?mp_id="+sheetno;
        }
        function openmodewin() {
            var tablenmid_code;
            var autonum = Math.round(10000 * Math.random());
            window.showModalDialog('PC_TBPC_Itemsfilter.aspx?autonum=' + autonum + '&tablenmid_code=' + tablenmid_code + '', '', "dialogHeight:400px;dialogWidth:640px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "PC_TBPC_Purchaseplan_check_list.aspx";
        }
        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
        //    self.moveTo(0,0)
        //    self.resizeTo(screen.availWidth,screen.availHeight)
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

    <script language="javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 0,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 3; i < j; i++) {
                        var dataRow = this.sDataTable.tBodies[0].rows[i + 1];
                        var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                        var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                        if (coltext == coltextbef) {
                            for (var k = 1; k <= 5; k++) {
                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            }
                        }
                    }
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
       
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td width="120px" align="left">
                                库存占用审核清单：
                            </td>
                            <td>
                                <asp:RadioButton ID="Radio_quanbu" runat="server" Text="全部" GroupName="1" OnCheckedChanged="Radio_quanbu_CheckedChanged"
                                    AutoPostBack="True" />
                                <asp:RadioButton ID="Radio_my" runat="server" Text="我的任务" Checked="true" GroupName="1"
                                    OnCheckedChanged="Radio_my_CheckedChanged" AutoPostBack="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="radio_view0" runat="server" Text="未提交" GroupName="view"
                                    OnCheckedChanged="radio_view0_CheckedChanged" AutoPostBack="True" />
                                <asp:Label ID="lb_wlzygl1" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RadioButton ID="radio_view1" runat="server" Text="待审核"  Checked="true" GroupName="view" OnCheckedChanged="radio_view1_CheckedChanged"
                                    AutoPostBack="True" />
                                <asp:Label ID="lb_wlzygl2" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RadioButton ID="radio_view3" runat="server" Text="已驳回" GroupName="view" OnCheckedChanged="radio_view3_CheckedChanged"
                                    AutoPostBack="True" />
                                <asp:Label ID="lb_wlzygl3" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RadioButton ID="radio_view2" runat="server" Text="通过" GroupName="view" OnCheckedChanged="radio_view2_CheckedChanged"
                                    AutoPostBack="True" />
                            </td>
                            <td>
                                <asp:RadioButtonList ID="SFTZ" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="SFTZ_Changed"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="未调整" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="调整中" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="已调整" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                    CancelControlID="btnClose" PopupControlID="PanelCondition" Drag="false" Y="80" X="200">
                                </asp:ModalPopupExtender>
                            </td>
                            <td>
                                <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" />&nbsp;
                                <asp:Button ID="btn_pldc" runat="server" Text="批量导出" OnClientClick="PLdaochu()" />
                            </td>
                            <td align="right">
                                <%--<asp:Button ID="btn_fanshen" runat="server" Text="反审" Visible="false" OnClick="btn_fanshen_Click" />&nbsp;&nbsp;--%>
                                <asp:Button ID="btn_delete" runat="server" Text="删除" OnClick="btn_delete_click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="80%" Style="display: none">
                        <table width="80%" style="background-color: #CCCCFF; border: solid 1px black;">
                            <tr>
                                <td colspan="6" align="center">
                                    <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnClose" runat="server" Text="取消" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    单据编号：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_orderno" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    开始日期：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_StartTime" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                        TargetControlID="tb_StartTime">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right">
                                    结束日期：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_EndTime" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_EndTime">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    计划跟踪号：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_ptc" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    项目：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_pj" runat="server" OnTextChanged="tb_pj_Textchanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="tb_pj"
                                        ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetPJNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td align="right">
                                    设备：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_eng" runat="server" OnTextChanged="tb_eng_Textchanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="tb_eng"
                                        ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="20" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetENGNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    材质：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_cz" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    规格：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_gg" runat="server"></asp:TextBox>
                                </td>
                                <%--<td align="right">
                                        国标：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_gb" runat="server"></asp:TextBox>
                                    </td>--%>
                            </tr>
                            <tr>
                                <td align="right">
                                    制单人：
                                </td>
                                <td>
                                    <asp:DropDownList ID="drp_zdr" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    审核人：
                                </td>
                                <td>
                                    <asp:DropDownList ID="drp_shr" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    物料编码：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_marid" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="height: 500px; overflow: auto; width: 100%">
                    <div class="cpbox xscroll">
                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                            <asp:Repeater ID="tbpc_pchsplanrvwchecklistRepeater" runat="server" OnItemDataBound="tbpc_pchsplanrvwchecklistRepeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>占用单号</strong>
                                        </td>
                                        <td>
                                            <strong>项目</strong>
                                        </td>
                                        <td>
                                            <strong>工程</strong>
                                        </td>
                                        <td>
                                            <strong>制单人</strong>
                                        </td>
                                        <td>
                                            <strong>制单时间</strong>
                                        </td>
                                        <td>
                                            <strong>计划跟踪号</strong>
                                        </td>
                                        <td>
                                            <strong>材料编码</strong>
                                        </td>
                                        <td>
                                            <strong>名称</strong>
                                        </td>
                                        <td>
                                            <strong>规格</strong>
                                        </td>
                                        <td>
                                            <strong>材质</strong>
                                        </td>
                                        <td>
                                            <strong>国标</strong>
                                        </td>
                                        <td>
                                            <strong>占用库存数量</strong>
                                        </td>
                                        <td>
                                            <strong>是否MTO</strong>
                                        </td>
                                        <td runat="server" id="hcheckp">
                                            <strong>审核人</strong>
                                        </td>
                                        <td runat="server" id="hcheckt">
                                            <strong>审核时间</strong>
                                        </td>
                                        <td>
                                            <strong>申请人</strong>
                                        </td>
                                        <td>
                                            <strong>状态</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                        ondblclick="redirectw(this)">
                                        <td>
                                            <asp:Label ID="ROWSNUM" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(lb_CurrentPage.Text)-1)*100%>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false" onclick="checkme(this)"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="PR_PCODE" runat="server" Text='<%#Eval("PR_PCODE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PJ_NAME" runat="server" Text='<%#Eval("PJ_NAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="TSA_ENGNAME" runat="server" Text='<%#Eval("TSA_ENGNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PR_REVIEWANM" runat="server" Text='<%#Eval("PR_REVIEWANM")%>'></asp:Label>
                                            <asp:Label ID="PR_REVIEWA" runat="server" Text='<%#Eval("PR_REVIEWA")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PR_REVIEWATIME" runat="server" Text='<%#Eval("PR_REVIEWATIME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="ptcode" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="marid" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="marnm" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="margg" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="marcz" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="margb" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="usenum" runat="server" Text='<%#Eval("usenum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_ISSTOUSE" runat="server" Text='<%#get_state(Eval("PUR_ISSTOUSE").ToString())%>'></asp:Label>
                                        </td>
                                        <td runat="server" id="bcheckp">
                                            <asp:Label ID="PR_REVIEWBNM" runat="server" Text='<%#Eval("PR_REVIEWBNM")%>'></asp:Label>
                                            <asp:Label ID="PR_REVIEWB" runat="server" Text='<%#Eval("PR_REVIEWB")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td runat="server" id="bcheckt">
                                            <asp:Label ID="PR_REVIEWBTIME" runat="server" Text='<%#Eval("PR_REVIEWBTIME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PR_SQRNM" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PR_STATETEXT" runat="server" Text='<%#get_prlist_state(Eval("PR_STATE").ToString())%>'></asp:Label>
                                            <asp:Label ID="PR_STATE" runat="server" Text='<%#Eval("PR_STATE")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PR_NOTE" runat="server" Text='<%#Eval("PR_NOTE")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            合计:
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="LabelSumZhanYong"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="19" align="center">
                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                        没有数据！
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>

                        <script language="javascript" type="text/javascript">
                            var myST = new superTable("tab", {
                                cssSkin: "tDefault",
                                headerRows: 1,
                                fixedCols: 0,
                                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                onStart: function() {
                                    //		        this.start = new Date();
                                },
                                onFinish: function() {
                                    for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 3; i < j; i++) {
                                        var dataRow = this.sDataTable.tBodies[0].rows[i + 1];
                                        var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                                        var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                                        if (coltext == coltextbef) {
                                            for (var k = 1; k <= 5; k++) {
                                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                                            }
                                        }
                                    }

                                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                                }
                            });
                        </script>

                    </div>
                    <div>
                        <table width="100%" border="1" class="nowrap cptable fullwidth" align="center">
                            <tr>
                                <td>
                                    共<asp:Label ID="lb_count" runat="server" Text="Label"></asp:Label>条记录&nbsp; 共<asp:Label
                                        ID="lb_page" runat="server" Text="Label"></asp:Label>页 &nbsp; 当前第<asp:Label ID="lb_CurrentPage"
                                            runat="server" Text="1"></asp:Label>页&nbsp;
                                    <asp:LinkButton ID="LinkFirst" runat="server" OnClick="LinkFirst_Click">第一页</asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="LinkUp" runat="server" OnClick="LinkUp_Click">上一页</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LinkDown" runat="server" OnClick="LinkDown_Click">下一页</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="LinkLast" runat="server" OnClick="LinkLast_Click">最后一页</asp:LinkButton>&nbsp;
                                    转到第<asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    页
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:Panel ID="Panel_combine" runat="server">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        &nbsp;&nbsp;<asp:CheckBox ID="cbxselectall" runat="server" Text="全选" AutoPostBack="true"
                                            TextAlign="right" Checked="false" OnCheckedChanged="cbxselectall_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
