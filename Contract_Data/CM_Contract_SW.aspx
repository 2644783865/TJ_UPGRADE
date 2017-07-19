<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="CM_Contract_SW.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Contract_SW" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="lblContractTypeBT" runat="server" Text=""></asp:Label></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

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

    <script type="text/javascript" language="javascript">
        //检验日期格式如：2012-01-01
        function dateCheck(obj) {
            var value = obj.value;
            if (value != "") {
                var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
                m = re.exec(value)
                if (m == null) {
                    obj.style.background = "yellow";
                    obj.value = "";
                    alert('请输入正确的时间格式如：2012-01-01');
                }
            }
        }
        //判断金额的格式
        function check_num(obj) {
            var num = obj.value;
            var patten = /^[0-9][0-9]{0,9}(\.[0-9]{1,2})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0";
                obj.focus();
            }
        }

        function reset() {
            $("#<%=txtHTH.ClientID%>").val("");
            $("#<%=txt_YZHTH.ClientID%>").val("");
            $("#<%=txt_PJNAME.ClientID%>").val("");
            $("#<%=txt_KH.ClientID%>").val("");
            $("#<%=fkbl.ClientID%>").val("");
            $("#<%=txt_SheBei.ClientID%>").val("");
            $("#<%=txt_Map.ClientID%>").val("");
            $("#<%=sta_time.ClientID%>").val("");
            $("#<%=fhend_time.ClientID%>").val("");
            $("#<%=fhsta_time.ClientID%>").val("");
            $("#<%=fhend_time.ClientID%>").val("");
        }
        
          function aa() {
            $("#<%=GridView1.ClientID%> tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });
        }
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-wrapper">
            <div style="height: 8px" class="box_top">
            </div>
            <div class="box-outer">
                <table width="100%">
                    <%--操作按钮--%>
                    <tr>
                        <td>
                            <asp:Button ID="btn_search" runat="server" Text="查 询" OnClick="btn_search_Click" />
                            &nbsp;|&nbsp;<asp:Button ID="btn_reset" runat="server" Text="重 置" OnClientClick="reset()"
                                OnClick="btn_reset_Click" />
                            &nbsp;|&nbsp;<asp:Button ID="btn_ViewHT" runat="server" Text="查 看" OnClick="btn_ViewHT_Click" />
                            &nbsp;|&nbsp;<asp:Button ID="btn_EditHT" runat="server" Text="编 辑" OnClick="btn_EditHT_Click" />
                            &nbsp;|&nbsp;<asp:Button ID="btn_DelHT" runat="server" Text="删 除" OnClick="btn_DelHT_Click"
                                OnClientClick="javascript:return confirm('确定删除吗？')" />
                            <%-- &nbsp;|&nbsp;<asp:Button ID="btn_AddQK" runat="server" Text="添加要款" OnClick="btn_AddYK_Click" />--%>
                            <%--&nbsp;|&nbsp;<asp:Button ID="btn_AddHT" runat="server" Text="添加合同" OnClick="btn_AddHT_Click" />--%>
                            &nbsp;|&nbsp;<asp:Button ID="btn_Export" runat="server" Text="导 出" OnClick="btn_Export_Click" />
                            &nbsp;|&nbsp;<asp:Button ID="btn_Marker_Export" runat="server" Text="市场部导出" OnClick="btn_Marker_Export_Click" />
                            &nbsp;|&nbsp;<asp:Button ID="btn_ShouKuan" runat="server" Text="查看收款明细" OnClientClick="OpenShouKuan()" />
                            &nbsp;|&nbsp;<asp:Button ID="Button1" runat="server" Text="查看发票明细" OnClientClick="OpenFaPiao()" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            合 同 号&nbsp;：<asp:TextBox ID="txtHTH" runat="server" Width="120px"></asp:TextBox>
                            <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="txtHTH"
                                ServicePath="Contract_Autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                CompletionInterval="10" ServiceMethod="GetHTBH" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                            <asp:DropDownList runat="server" ID="ddl_HTH" AutoPostBack="true" Width="205px" Visible="false"
                                OnSelectedIndexChanged="btn_search_Click">
                            </asp:DropDownList>
                        </td>
                        <td>
                            业主合同：<asp:TextBox ID="txt_YZHTH" runat="server" Width="120px"></asp:TextBox>
                            <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender3" TargetControlID="txt_YZHTH"
                                ServicePath="Contract_Autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                CompletionInterval="10" ServiceMethod="GetYZHTBH" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                            <asp:DropDownList runat="server" ID="ddl_YZHTH" Visible="false" AutoPostBack="true"
                                Width="205px" OnSelectedIndexChanged="btn_search_Click">
                            </asp:DropDownList>
                        </td>
                        <%--<td>
                                        合同名称：<asp:TextBox ID="txt_HTNAME" runat="server" Width="205px"></asp:TextBox>
                                    </td>--%>
                        <td width="200px">
                            合同年份：<asp:DropDownList ID="htnf" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                            </asp:DropDownList>
                        </td>
                        <td>
                            项目名称：<asp:TextBox ID="txt_PJNAME" runat="server" Width="120px"></asp:TextBox>
                            <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender4" TargetControlID="txt_PJNAME"
                                ServicePath="Contract_Autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                CompletionInterval="10" ServiceMethod="GetXMMC" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                            <asp:DropDownList runat="server" ID="ddl_PJNAME" AutoPostBack="true" Visible="false"
                                OnSelectedIndexChanged="btn_search_Click">
                            </asp:DropDownList>
                        </td>
                        <td>
                            负 责 人：<asp:DropDownList ID="dplFZR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            客户名称：<asp:TextBox ID="txt_KH" runat="server" Width="120px"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_KH"
                                ServicePath="Contract_Autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                CompletionInterval="10" ServiceMethod="GetGKMC" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                            <asp:DropDownList runat="server" ID="ddl_KH" AutoPostBack="true" Visible="false"
                                Width="205px" OnSelectedIndexChanged="btn_search_Click">
                            </asp:DropDownList>
                        </td>
                        <td>
                            回款比例：<asp:DropDownList ID="ddlfkbl" runat="server">
                                <asp:ListItem Text="等于" Value="1"></asp:ListItem>
                                <asp:ListItem Text="大于" Value="2"></asp:ListItem>
                                <asp:ListItem Text="小于" Value="3"></asp:ListItem>
                                <asp:ListItem Text="大于等于" Value="4"></asp:ListItem>
                                <asp:ListItem Text="小于等于" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="fkbl" runat="server" Width="30px" onchange="javascript:check_num(this);"></asp:TextBox>
                            %
                        </td>
                        <td width="180px">
                            图号：<asp:TextBox ID="txt_Map" runat="server" Width="100px"></asp:TextBox>
                            <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender6" TargetControlID="txt_Map"
                                ServicePath="Contract_Autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                CompletionInterval="10" ServiceMethod="GetTH" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td>
                            设备名称：<asp:TextBox ID="txt_SheBei" runat="server" Width="120px"></asp:TextBox>
                            <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender5" TargetControlID="txt_SheBei"
                                ServicePath="Contract_Autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                CompletionInterval="10" ServiceMethod="GetSBMC" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                            </asp:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            签订时间：<asp:TextBox ID="sta_time" runat="server" Width="90px" class="easyui-datebox"
                                onfocus="this.blur()"></asp:TextBox>
                            至&nbsp;
                            <asp:TextBox ID="end_time" runat="server" Width="90px" class="easyui-datebox" onfocus="this.blur()"></asp:TextBox>
                            <%--<asp:CalendarExtender ID="calender_sta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="sta_time">
                                        </asp:CalendarExtender>
                                        <asp:CalendarExtender ID="calender_end" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="end_time">
                                        </asp:CalendarExtender>--%>
                        </td>
                        <td>
                            发货时间：<asp:TextBox ID="fhsta_time" runat="server" Width="90px" class="easyui-datebox"
                                onfocus="this.blur()"></asp:TextBox>
                            至&nbsp;
                            <asp:TextBox ID="fhend_time" runat="server" Width="90px" class="easyui-datebox" onfocus="this.blur()"></asp:TextBox>
                            <%--<asp:CalendarExtender ID="Calendarfhsta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="fhsta_time">
                                        </asp:CalendarExtender>
                                        <asp:CalendarExtender ID="Calendarfhend" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="fhend_time">
                                        </asp:CalendarExtender>--%>
                        </td>
                        <%--<td>
                                        任务号：<asp:TextBox ID="txtSCH" runat="server" Width="205px"></asp:TextBox>
                                    </td>--%>
                        <td>
                            合同评审状态：<asp:DropDownList ID="htps_state" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <asp:ListItem Text="无" Value=""></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="审批通过" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            合同是否签订：<asp:DropDownList runat="server" ID="ddlQD" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <asp:ListItem Text="未签订" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已签订" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbxConWarn" runat="server" AutoPostBack="true" OnCheckedChanged="rblIfZaizhi_OnSelectedIndexChanged" />未签订合同已入库<asp:Label
                                ForeColor="Red" ID="lblConWarn" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelBody" runat="server" Style="overflow: auto; position: relative;
                            margin: 2px" Width="100%">
                            <asp:GridView ID="GridView1" Width="1600px" CssClass="toptable grid" runat="server"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="grv_RowDataBound"
                                Style="white-space: normal">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <div style="width: 80px">
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                <asp:Label ID="lbl_id" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="lbl_htbh" runat="server" Text='<%# Eval("PCON_BCODE") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_fzr" runat="server" Text='<%#Eval("PCON_RESPONSER")%>' Visible="false"></asp:Label></div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PCON_CUSTMNAME" HeaderText="业主名称" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="PCON_BCODE" HeaderText="合同号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="PCON_YZHTH" HeaderText="业主合同号" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="PCON_ENGNAME" HeaderText="项目名称" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <%-- <asp:BoundField DataField="CM_ENGNAME" HeaderText="产品名称" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_MAP" HeaderText="图号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_MATERIAL" HeaderText="阀板材质" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_PRICE" HeaderText="单价（元）" DataFormatString="{0:c}"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_COUNT" HeaderText="合同金额（元）" DataFormatString="{0:c}"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_NUMBER" HeaderText="数量" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_UNIT" HeaderText="单位" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_WEIGHT" HeaderText="单重（吨）" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_ALL" HeaderText="总重（吨）" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_SIGN" HeaderText="合同签订时间" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_JIAO" HeaderText="要求交货期" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_NOTE" HeaderText="合同特殊要求及说明" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_DTSJ" HeaderText="到图时间" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_CPRK" HeaderText="成品入库时间" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_TZFH" HeaderText="通知要求发货时间" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CM_CKSJ" HeaderText="出库时间（发货时间）" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />--%>
                                    <asp:BoundField DataField="CM_MAP" HeaderText="图号" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <%--<asp:BoundField DataField="PCON_ENGTYPE" HeaderText="设备名称" ItemStyle-Wrap="true"
                                        ItemStyle-Width="150px" HeaderStyle-Wrap="false" />--%>
                                    <asp:TemplateField HeaderText="设备名称" ItemStyle-Wrap="true" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="PCON_ENGTYPE" Text='<%#Eval("PCON_ENGTYPE") %>' BorderStyle="None"
                                                ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                ToolTip='<%#Eval("PCON_ENGTYPE")%>' Width="100px">
                                            </asp:TextBox></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CM_DUTY" HeaderText="负责人" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="PCON_JINE" HeaderText="合同总额（万元）" ItemStyle-HorizontalAlign="Right"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="PCON_RESPONSER" HeaderText="制单人" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" />
                                    <%--  <asp:TemplateField HeaderText="其他币种" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_qtbz" runat="server" Text='<%# Convert.ToDouble(Eval("OTHER_MONUNIT"))==0?"——":Eval("OTHER_MONUNIT").ToString()+Eval("PCON_MONUNIT").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--      <asp:TemplateField HeaderText="已付款比例" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_skbl" runat="server" Text='<%#string.Format("{0:N2}",(Convert.ToDouble(Eval("PCON_YFK"))/Convert.ToDouble(Convert.ToDouble(Eval("PCON_HTZJ"))==0?"1":Eval("PCON_HTZJ")))*100)+"%" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="已付款比例" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_skbl" runat="server" Text='<%# Eval("PCON_YFKBL")==""?"0":Eval("PCON_YFKBL")+"%" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PCON_YFK" HeaderText="已付款金额（万元）" ItemStyle-HorizontalAlign="Right"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <%--    <asp:TemplateField HeaderText="未付款比例" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_wkbl" runat="server" Text='<%#string.Format("{0:N2}",(1-Convert.ToDouble(Eval("PCON_YFK"))/Convert.ToDouble(Convert.ToDouble(Eval("PCON_HTZJ"))==0?"1":Eval("PCON_HTZJ")))*100)+"%" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="未付款比例" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_wkbl" runat="server" Text='<%# Eval("PCON_WFKBL")==""?"100":Eval("PCON_WFKBL")+"%"%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="未付款金额（万元）" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_wfk" runat="server" Text='<%#(Convert.ToDouble(Eval("PCON_JINE"))-Convert.ToDouble(Eval("PCON_YFK"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="质保金(万元)" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_zbj" runat="server" Text='<%# Eval("PCON_ZBJ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="质保金比例" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_zbjbl" runat="server" Text='<%# Eval("PCON_ZBJBL")==""?"0": Eval("PCON_ZBJBL") +"%"%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="PCON_BALANCEACNT" HeaderText="结算金额" DataFormatString="{0:c}"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />--%>
                                    <%--<asp:BoundField DataField="PCON_KPJE" HeaderText="开票金额" DataFormatString="{0:c}"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />--%>
                                    <asp:TemplateField HeaderText="开票金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_kpje" runat="server" Text='<%#Eval("PCON_KPJE")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="应付金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_yfje" runat="server" Text='<%#string.Format("{0:C}",(Convert.ToDouble(Eval("PCON_KPJE"))-Convert.ToDouble(Eval("PCON_YFK")))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PCON_FHSJ" HeaderText="发货情况" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="是否索赔" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_state" runat="server" Text='<%#(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>'
                                                ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.FromName("#333333") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="索赔金额" DataField="PCON_SPJE" DataFormatString="{0:c}"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right"
                                        ItemStyle-ForeColor="Red" />
                                    <asp:TemplateField HeaderText="合同变更"  ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_change" runat="server" Text='<%#"JE("+Eval("PCON_JECHG").ToString()+")"+"-"+"QT("+Eval("PCON_QTCHG").ToString()+")"%>'
                                                ForeColor='<%#Eval("PCON_QTCHG").ToString()!="0"?System.Drawing.Color.Red:Eval("PCON_JECHG").ToString()!="0"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="合同评审状态" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="htps_state1" runat="server" Text='<%#(Eval("CR_PSZT").ToString()==""?"":Eval("CR_PSZT").ToString()=="0"?"未提交":Eval("CR_PSZT").ToString()=="1"?"审批中":Eval("CR_PSZT").ToString()=="2"?"审批通过":"已驳回") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注" ItemStyle-Wrap="true" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="PCON_NOTE" Text='<%#Eval("PCON_NOTE") %>' BorderStyle="None"
                                                ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                ToolTip='<%#Eval("PCON_NOTE")%>' Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#A8DCF8" Font-Bold="True" ForeColor="White" Wrap="False" />
                                <RowStyle BackColor="#ffffff" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            </asp:GridView>
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                没有记录!</asp:Panel>
                        </asp:Panel>
                        <table width="100%">
                            <tr>
                                <td style="text-align: right">
                                    筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>
                                    条记录&nbsp;&nbsp; 合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;
                                    <%--合计索赔：--%><asp:Label ID="lb_select_sp" runat="server" Text="" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                    总重：<asp:Label ID="lb_all_weight" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <asp:UCPaging ID="UCPaging1" runat="server" />
                                </td>
                                <td>
                                    每页：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;行
                                </td>
                            </tr>
                        </table>
                        </div> </div> </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_search" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_ViewHT" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_EditHT" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_DelHT" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btn_reset" EventName="Click" />
                      <%--  <asp:AsyncPostBackTrigger ControlID="btn_AddHT" EventName="Click" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btn_ShouKuan" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                        <asp:PostBackTrigger ControlID="btn_Export" />
                        <asp:PostBackTrigger ControlID="btn_Marker_Export" />
                    </Triggers>
                </asp:UpdatePanel>

                <script type="text/javascript">
                    //单击行变色
                    function RowClick(obj) {
                        //判断是否单击的已选择的行，如果是则取消该行选择
                        if (obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked == false) {
                            obj.style.backgroundColor = '#ffffff';
                            obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
                        }
                        else {
                            var table = obj.parentNode.parentNode;
                            var tr = table.getElementsByTagName("tr");

                            var ss = tr.length;
                            for (var i = 1; i < ss - 1; i++) {
                                tr[i].style.backgroundColor = (tr[i].style.backgroundColor == '#87CEFF') ? '#ffffff' : '#ffffff';
                                tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
                            }
                            obj.style.backgroundColor = (obj.style.backgroundColor == '#ffffff') ? '#87CEFF' : '#ffffff';
                            obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
                        }
                    }

                    //查看合同
                    function ViewHT(ID) {
                        var autonum = Math.round(10000 * Math.random());
                        window.open("CM_Contract_SW_Add.aspx?Action=View&autonum=" + autonum + "&condetail_id=" + ID);
                    }

                    //编辑合同
                    function EditHT(ID) {
                        var autonum = Math.round(10000 * Math.random());
                        window.open("CM_Contract_SW_Add.aspx?Action=Edit&autonum=" + autonum + "&condetail_id=" + ID);
                    }

                    //添加要款
                    function Add_YK(ID) {
                        var autonum = Math.round(10000 * Math.random());
                        var sRet = window.showModalDialog("CM_SW_Payment.aspx?Action=Add&autonum=" + autonum + "&condetail_id=" + ID, obj, "dialogWidth=750px;dialogHeight=520px;status:no;");
                        if (sRet == "refresh") {
                            window.location.reload();
                        }
                    }

                    //添加合同
                    function Add_HT() {
                        var autonum = Math.round(10000 * Math.random());
                        window.open("CM_Contract_SW_Add.aspx?Action=Add&autonum=" + autonum);
                    }

                    function OpenShouKuan() {
                        window.open("CM_Contract_SW_SKDETAIL.aspx");
                    }

                    function OpenFaPiao() {
                        window.open("CM_AllBill.aspx");
                    }
        
                </script>

            </div>
        </div>
    </div>
</asp:Content>
