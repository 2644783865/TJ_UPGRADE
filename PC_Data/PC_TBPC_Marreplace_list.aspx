<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Marreplace_list.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Marreplace_list"
    Title="代用单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    代用单管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script language="javascript" type="text/javascript">

        function redirectw(obj) {
            var sheetno;

            sheetno = obj.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
            
            window.location = "PC_TBPC_Marreplace_panel.aspx?mpno=" + sheetno + "";
//            window.open("PC_TBPC_Marreplace_panel.aspx?mpno=" + sheetno + "");
            //        window.location.href="PC_TBPC_Marreplace_panel.aspx?mpno="+sheetno;
            //window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?mp_id="+sheetno;
        }
        //弹出过滤窗口
        function PLdaochu() {
            //        window.showModalDialog('PC_TBPC_PLdaochu.aspx','',"dialogHeight:300px;dialogWidth:500px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.open("PC_Data_DYDdaochu.aspx");
            //        window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?shape="+escape(shape)+"&mp_id="+escape(mpid)+"";
        }
        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
        //     self.moveTo(0,0)
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
                    for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 2; i < j; i++) {
                        var dataRow = this.sDataTable.tBodies[0].rows[i + 1];
                        var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                        var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                        if (coltext == coltextbef) {
                            for (var k = 1; k <= 3; k++) {
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
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td width="25%">
                                物料代用清单列表
                                <asp:RadioButton ID="rad_all" runat="server" Text="全部单据" TextAlign="Right" GroupName="select0"
                                    OnCheckedChanged="rad_all_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_mypart" runat="server" Text="我的任务" TextAlign="Right" GroupName="select0"
                                    OnCheckedChanged="rad_mypart_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                            </td>
                            <td width="40%">
                                <asp:RadioButton ID="rad_weitijiao" runat="server" Text="未提交" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_weitijiao_CheckedChanged" AutoPostBack="true" />
                                <asp:Label ID="lb_dydsh3" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RadioButton ID="rad_daishen" runat="server" Text="待审核" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_daishen_CheckedChanged" AutoPostBack="true" Checked="true" />
                                <asp:Label ID="lb_dydsh1" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RadioButton ID="rad_bohui" runat="server" Text="已驳回" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_bohui_CheckedChanged" AutoPostBack="true" />
                                <asp:Label ID="lb_dydsh2" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RadioButton ID="rad_tongguo" runat="server" Text="已通过" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_tongguo_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnqrck" runat="server" Text="确认查看" OnClick="btnqrck_click" Visible="false" />
                            </td>
                            <td width="10%">
                                <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                    PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                    CancelControlID="btnClose" Y="80">
                                </asp:ModalPopupExtender>
                            </td>
                            <td width="25%" align="right">
                                <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" />&nbsp;
                                <asp:Button ID="btn_PLdaochu" runat="server" Text="批量导出" OnClientClick="PLdaochu()" />&nbsp;
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
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_EndTime">
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
                                    物料编码：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_marid" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    国标：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_gb" runat="server"></asp:TextBox>
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
                            </tr>
                            <tr>
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
                                    工程：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_eng" runat="server" OnTextChanged="tb_eng_Textchanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="tb_eng"
                                        ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="20" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetENGNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td align="right">
                                    制单人：
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="tb_zdr" runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="drp_stu" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" align="center">
                                    是否打印：
                                    <asp:RadioButton ID="rad_qb" runat="server" Text="全部" TextAlign="Right" GroupName="select1"
                                        Checked="true" />&nbsp;
                                    <asp:RadioButton ID="rad_wdy" runat="server" Text="未打印" TextAlign="Right" GroupName="select1" />&nbsp;
                                    <asp:RadioButton ID="rad_ydy" runat="server" Text="已打印" TextAlign="Right" GroupName="select1" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="height: 470px; overflow: auto; width: 100%">
                    <div class="cpbox xscroll">
                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                            <asp:Repeater ID="Marreplace_list_Repeater" runat="server" OnItemDataBound="Marreplace_list_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>单据编号</strong>
                                        </td>
                                        <td>
                                            <strong>制单人</strong>
                                        </td>
                                        <td>
                                            <strong>制单日期</strong>
                                        </td>
                                        <td>
                                            <strong>计划跟踪号</strong>
                                        </td>
                                        <td>
                                            <strong>项目</strong>
                                        </td>
                                        <td>
                                            <strong>工程</strong>
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
                                            <strong>技术员</strong>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>负责人</strong>
                                        </td>
                                        <td>
                                            <strong>储运审核人</strong>
                                        </td>
                                        <td>
                                            <strong>审核标识</strong>
                                        </td>
                                        <td>
                                            <strong>查看</strong>
                                        </td>
                                        <td>
                                            <strong>确认已查看</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                        ondblclick="redirectw(this)">
                                        <td>
                                            <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(lb_CurrentPage.Text)-1)*50%>'></asp:Label>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CODE" runat="server" Text='<%#Eval("mpcode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_FILLFMID" runat="server" Text='<%#Eval("zdrid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_FILLFMNM" runat="server" Text='<%#Eval("zdrnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_FILLFMTIME" runat="server" Text='<%#Eval("zdtime")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="ptcode" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_PJID" runat="server" Text='<%#Eval("pjid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_PJNAME" runat="server" Text='<%#Eval("pjnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_ENGID" runat="server" Text='<%#Eval("engid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_ENGNAME" runat="server" Text='<%#Eval("engnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="marid" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="marnm" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="marguige" runat="server" Text='<%#Eval("marguige")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="marcaizhi" runat="server" Text='<%#Eval("marcaizhi")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="marguobiao" runat="server" Text='<%#Eval("marguobiao")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_REVIEWAID" runat="server" Text='<%#Eval("shraid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_REVIEWANM" runat="server" Text='<%#Eval("shranm")%>'></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CHARGEID" runat="server" Text='<%#Eval("shrbid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_CHARGENM" runat="server" Text='<%#Eval("shrbnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_CKSHRID" runat="server" Text='<%#Eval("MP_CKSHRID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="MP_CKSHRNM" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                            <asp:Label ID="MP_CKSHRTIME" runat="server" Text='<%#Eval("MP_CKSHRTIME")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MP_STATE" runat="server"></asp:Label>
                                            <asp:Label ID="MP_STATE1" runat="server" Text='<%#Eval("totalstate")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink_lookup" runat="server" ForeColor="Red" NavigateUrl='<%#"~/PC_Data/PC_TBPC_Marreplace_panel.aspx?state="+Eval("totalstate")+"&mpno="+Eval("mpcode")%>'>详细信息</asp:HyperLink>
                                            <input id="lbscbd" type="hidden" runat="server" value='<%#Eval("scbd")%>' />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btn_Confirm_ck" runat="server" CommandArgument='<%#Eval("mpcode")+","+Eval("mp_ck_bt") %>'
                                                Text="确定" OnClick="btn_Confirm_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="18" align="center">
                                    <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                        没有记录！</asp:Panel>
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
                                    for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 2; i < j; i++) {
                                        var dataRow = this.sDataTable.tBodies[0].rows[i + 1];
                                        var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                                        var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerHTML;
                                        if (coltext == coltextbef) {
                                            for (var k = 1; k <= 3; k++) {
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
                </div>
                <asp:Panel ID="Pan_select" runat="server" Visible="false">
                    <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                        Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
