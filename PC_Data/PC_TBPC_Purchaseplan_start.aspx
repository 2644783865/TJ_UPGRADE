<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_start.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_start"
    Title="物料需用计划" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function PLdaochu() {
            window.open("PC_TBPC_XYJHdaochu.aspx");
        }
        function redirectw(obj) {
            var sheetno;
            var shape;
            sheetno = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            shape = obj.getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML;
//          window.location.href = "PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + escape(shape) + "&mp_id=" + escape(sheetno);
            window.open("PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + escape(shape) + "&mp_id=" + escape(sheetno));
            //window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?mp_id="+sheetno;
        }
        function openmodewin() {
            var tablenmid_code;
            var autonum = Math.round(10000 * Math.random());
            tablenmid_code = document.getElementById("<%=hid_filter.ClientID %>").value;
            window.showModalDialog('PC_TBPC_Itemsfilter.aspx?autonum=' + autonum + '&tablenmid_code=' + tablenmid_code + '', '', "dialogHeight:400px;dialogWidth:640px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "PC_TBPC_Purchaseplan_start.aspx?sf=1";
        }
        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
        function viewCondition2() {
            document.getElementById("<%=PanelCondition2.ClientID%>").style.display = 'block';
        }
//        self.moveTo(0, 0)
//        self.resizeTo(screen.availWidth, screen.availHeight)
    </script>

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
                        var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                        var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                        if (coltext == coltextbef) {
                            for (var k = 2; k <= 2; k++) {
                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            }
                        }
                    }
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
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
    <%-- <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <div style="height: 12px">
        <table width="98%">
            <tr>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                    <asp:Button ID="btn_BGdata" runat="server" ForeColor="Red" Font-Bold="true" Text="物料减少"
                        OnClick="btn_bgdata_click" Visible="false" />
                    <asp:Button ID="btn_editbeizhu" runat="server" ForeColor="Red" Font-Bold="true" Text="变更备注"
                        OnClientClick="viewCondition2()" Visible="false" />
                    <asp:ModalPopupExtender ID="ModalPopupExtenderchange" runat="server" TargetControlID="btn_editbeizhu"
                        PopupControlID="PanelCondition2" Drag="false" Enabled="True" DynamicServicePath=""
                        Y="60" X="100">
                    </asp:ModalPopupExtender>
                </td>
                <td align="right">
                    <%--<asp:label id="caution" runat="server" Text="提示：默认查询最近10000条数据，如需查询更早的数据，请在数据范围选择所有数据" ForeColor="Red"></asp:label>
                 
                 &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp--%>
                    <asp:Button ID="btn_daochu" runat="server" Text="导出数据" OnClick="btn_daochu_click" />&nbsp;&nbsp;&nbsp;&nbsp
                    <asp:Button ID="btnsendemail" runat="server" Text="发送邮件" OnClick="btn_email_click"
                        Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp
                </td>
            </tr>
        </table>
        <asp:Panel ID="PanelCondition2" runat="server" Width="300px" Style="display: none">
                <table width="280px" style="background-color: #CCCCFF; border: solid 1px black;">
                    <tr>
                        <td colspan="8" align="center">
                            <asp:Button ID="btn_queren" runat="server" OnClick="btn_queren_click" Text="确认" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnquxiao" runat="server" OnClick="btnquxiao_Click" Text="取消" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            变更备注内容：
                        </td>
                        <td>
                            <asp:TextBox ID="tbcontent" runat="server" Width="220px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
        </asp:Panel>
    </div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                材料需用计划下推
                            </td>
                            <td>
                                <asp:RadioButton ID="radio_CSH" runat="server" Text="未下推" GroupName="xiatui" OnCheckedChanged="radio_CSH_CheckedChanged"
                                    AutoPostBack="True" Checked="true" />
                                <asp:RadioButton ID="radio_YXT" runat="server" Text="已下推" GroupName="xiatui" OnCheckedChanged="radio_YXT_CheckedChanged"
                                    AutoPostBack="True" />
                                <asp:RadioButton ID="rad_YGB" runat="server" Text="已关闭" GroupName="xiatui" OnCheckedChanged="rad_YGB_CheckedChanged"
                                    AutoPostBack="True" />
                                &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkiffast" runat="server" AutoPostBack="true"
                                    OnCheckedChanged="QueryButton_Click" />加急物料
                            </td>
                            <td>
                                <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                    PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                    Y="80">
                                </asp:ModalPopupExtender>
                                <asp:Button ID="btn_filter" runat="server" Text="过滤" OnClientClick="openmodewin()" />
                                <asp:TextBox ID="hid_filter" runat="server" Style="display: none"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btn_PLDC" runat="server" Text="批量导出" OnClientClick="PLdaochu()" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="95%" Style="display: none">
                        <%--<asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                        <table width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
                            <tr>
                                <td colspan="8" align="center">
                                    <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                                    <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    批号：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_orderno" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    计划号：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_supply" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    申请开始日期：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_StartTime" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                        ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_StartTime">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right">
                                    结束日期：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_EndTime" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                        ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_EndTime">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <%--<td align="right">
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
                                                </td>--%>
                                <td align="right">
                                    图号：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_th" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    材料类型：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_shape" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    申请人：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_zdr" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    物料编码：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_marid" runat="server"></asp:TextBox>
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
                                <td align="right">
                                    国标：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_gb" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    关闭日期从：
                                </td>
                                <td>
                                    <asp:TextBox ID="txtclosestartdate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                        ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtclosestartdate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    到
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcloseenddate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                        ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd" TargetControlID="txtcloseenddate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    关闭类型：
                                </td>
                                <td>
                                    <asp:DropDownList ID="Drpclosetype" runat="server">
                                        <asp:ListItem Text="-请选择-" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="意外关闭" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="相似代用关闭" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="占用关闭" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="任务暂停" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    关闭人：
                                </td>
                                <td>
                                    <asp:DropDownList ID="Drpcloseper" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    申请部门：
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList3" runat="server">
                                        <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="公司领导" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="综合办公室" Value="02"></asp:ListItem>
                                        <asp:ListItem Text="技术部" Value="03"></asp:ListItem>
                                         <asp:ListItem Text="质量部" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="生产管理部" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="采购部" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="财务部" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="市场部" Value="07"></asp:ListItem>
                                        <asp:ListItem Text="机加工车间" Value="08"></asp:ListItem>
                                        <asp:ListItem Text="结构车间" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="安全环保部" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="工程师办公室" Value="11"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    每页显示：
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList5" runat="server">
                                        <asp:ListItem Text="50条记录" Value="50"></asp:ListItem>
                                        <asp:ListItem Text="100条记录" Value="100"></asp:ListItem>
                                        <asp:ListItem Text="200条记录" Value="200"></asp:ListItem>
                                        <asp:ListItem Text="300条记录" Value="300"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <%--<td align="right">
                                                    数据范围：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListrange" runat="server">
                                                        <asp:ListItem Text="最近10000条" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="所有数据" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>--%>
                            </tr>
                        </table>
                        <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div style="height: 500px; overflow: auto; width: 100%">
                    <div class="cpbox4 xscroll">
                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                            <asp:Repeater ID="purchaseplan_start_list_Repeater" runat="server" OnItemDataBound="purchaseplan_start_list_Repeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>批号</strong>
                                        </td>
                                        <td>
                                            <strong>计划跟踪号</strong>
                                        </td>
                                        <td>
                                            <strong>类型</strong>
                                        </td>
                                        <td>
                                            <strong>图号/标识号</strong>
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
                                            <strong>长度</strong>
                                        </td>
                                        <td>
                                            <strong>宽度</strong>
                                        </td>
                                        <td>
                                            <strong>计划数量</strong>
                                        </td>
                                        <td>
                                            <strong>使用部门</strong>
                                        </td>
                                        <td>
                                            <strong>申请人</strong>
                                        </td>
                                        <td>
                                            <strong>日期</strong>
                                        </td>
                                        <td>
                                            <strong>占用</strong>
                                        </td>
                                        <td>
                                            <strong>相似占用</strong>
                                        </td>
                                        <td>
                                            <strong>代用</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td>
                                            <strong>是否生成订单</strong>
                                        </td>
                                        <td>
                                            <strong>是否分工</strong>
                                        </td>
                                        <td>
                                            <strong>返回仓库备注</strong>
                                        </td>
                                        <td>
                                            <strong>关闭人</strong>
                                        </td>
                                        <td>
                                            <strong>关闭时间</strong>
                                        </td>
                                        <td>
                                            <strong>关闭类型</strong>
                                        </td>
                                        <td>
                                            <strong>下推人</strong>
                                        </td>
                                        <td>
                                            <strong>下推时间</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="row" runat="server" class="baseGadget" ondblclick="redirectw(this)" onclick="MouseClick1(this)">
                                        <td>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(DropDownList5.SelectedValue))%>'></asp:Label>
                                            <asp:Label ID="PUR_ID" runat="server" Text='<%#Eval("PUR_ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PCODE" runat="server" Text='<%#Eval("PCODE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="PTCODE" runat="server" Text='<%#Eval("ptcode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MASHAPE" runat="server" Text='<%#Eval("MASHAPE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MARID" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MARNM" runat="server" Text='<%#Eval("marnm")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MARGG" runat="server" Text='<%#Eval("margg")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MARCZ" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MARGB" runat="server" Text='<%#Eval("margb")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="LENGTH" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="WIDTH" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="NUM" runat="server" Text='<%#Eval("num")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="DEPID" runat="server" Text='<%#Eval("DEPID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="DEPNAME" runat="server" Text='<%#Eval("DEPNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="SUBMITID" runat="server" Text='<%#Eval("SQREID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="SUBMITNM" runat="server" Text='<%#Eval("SQRENAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="SUBMITTM" runat="server" Text='<%#Eval("SUBMITTM")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank">
                                                <asp:Label ID="PUR_ZY" runat="server"></asp:Label></asp:HyperLink>
                                            <asp:Label ID="PUR_CSTATE" runat="server" Text='<%#Eval("PUR_CSTATE")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">
                                                <asp:Label ID="PUR_SSDY" runat="server"></asp:Label></asp:HyperLink>
                                            <%-- <asp:Label ID="XTSTATE" runat="server" Text='<%#get_plan_xtstate(Eval("STATE").ToString())%>'></asp:Label>--%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank">
                                                <asp:Label ID="PUR_DY" runat="server"></asp:Label></asp:HyperLink>
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%#get_plan_xtstate(Eval("STATE").ToString())%>'></asp:Label>--%>
                                        </td>
                                        <td>
                                            <asp:Label ID="PUR_ZYDY" ForeColor="red" runat="server" Text='<%#Eval("PUR_ZYDY")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="purstate" runat="server" Text='<%#Eval("purstate")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="fgstate" runat="server" Text='<%#Eval("fgstate")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="fhnote" runat="server" Text='<%#Eval("PUR_BACKNOTE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Pur_ClosePer" runat="server" Text='<%#Eval("Pur_ClosePer")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Pur_ColseTime" runat="server" Text='<%#Eval("Pur_ColseTime")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Pue_Closetype" runat="server" Text='<%#Eval("Pue_Closetype")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbxtr" runat="server" Text='<%#Eval("PUR_XTR")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbxttime" runat="server" Text='<%#Eval("PUR_XTTIME")%>'></asp:Label>
                                            <asp:Label ID="PUR_IFFAST" runat="server" Text='<%#Eval("PUR_IFFAST")%>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="23" align="center">
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
                                                var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                                                var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                                                if (coltext == coltextbef) {
                                                    for (var k = 2; k <= 2; k++) {
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
                        <table width="100%">
                            <tr>
                                <td width="50%">
                                    共查出<asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>批计划/
                                    <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red"></asp:Label>条记录
                                </td>
                                <td align="right" width="50%">
                                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--        </ContentTemplate>
   </asp:UpdatePanel>--%>
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 50%; right: 40%">
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
    </asp:UpdateProgress>--%>
</asp:Content>
